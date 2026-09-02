using Microsoft.Data.SqlClient;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var connectionString =
    "Server=tcp:sql-day29-prod-chandru.database.windows.net,1433;" +
    "Initial Catalog=EmployeeDB;" +
    "Authentication=Active Directory Default;" +
    "Encrypt=True;" +
    "TrustServerCertificate=False;" +
    "Connection Timeout=30;";

// -------------------------
// Root
// -------------------------

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        application = "Employee Management API",
        environment = "Azure",
        status = "Running",
        version = "Day 30 Capstone"
    });
});

// -------------------------
// Health
// -------------------------

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy"
    });
});

// -------------------------
// GET ALL EMPLOYEES
// -------------------------

app.MapGet("/employees", async () =>
{
    var employees = new List<Employee>();

    await using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    const string sql = """
        SELECT EmployeeId, Name, Department, Email
        FROM Employees
        ORDER BY EmployeeId
        """;

    await using var command = new SqlCommand(sql, connection);

    await using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        employees.Add(new Employee(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.IsDBNull(2) ? null : reader.GetString(2),
            reader.IsDBNull(3) ? null : reader.GetString(3)
        ));
    }

    return Results.Ok(employees);
});

// -------------------------
// GET EMPLOYEE BY ID
// -------------------------

app.MapGet("/employees/{id:int}", async (int id) =>
{
    await using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    const string sql = """
        SELECT EmployeeId, Name, Department, Email
        FROM Employees
        WHERE EmployeeId = @EmployeeId
        """;

    await using var command = new SqlCommand(sql, connection);

    command.Parameters.AddWithValue("@EmployeeId", id);

    await using var reader = await command.ExecuteReaderAsync();

    if (!await reader.ReadAsync())
    {
        return Results.NotFound(new
        {
            message = $"Employee with ID {id} was not found."
        });
    }

    var employee = new Employee(
        reader.GetInt32(0),
        reader.GetString(1),
        reader.IsDBNull(2) ? null : reader.GetString(2),
        reader.IsDBNull(3) ? null : reader.GetString(3)
    );

    return Results.Ok(employee);
});

// -------------------------
// CREATE EMPLOYEE
// -------------------------

app.MapPost("/employees", async (EmployeeRequest request) =>
{
    await using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    const string sql = """
        INSERT INTO Employees
        (EmployeeId, Name, Department, Email)
        VALUES
        (@EmployeeId, @Name, @Department, @Email)
        """;

    await using var command = new SqlCommand(sql, connection);

    command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
    command.Parameters.AddWithValue("@Name", request.Name);

    command.Parameters.AddWithValue(
        "@Department",
        (object?)request.Department ?? DBNull.Value
    );

    command.Parameters.AddWithValue(
        "@Email",
        (object?)request.Email ?? DBNull.Value
    );

    try
    {
        await command.ExecuteNonQueryAsync();
    }
    catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
    {
        return Results.Conflict(new
        {
            message = $"Employee with ID {request.EmployeeId} already exists."
        });
    }

    return Results.Created(
        $"/employees/{request.EmployeeId}",
        request
    );
});

// -------------------------
// UPDATE EMPLOYEE
// -------------------------

app.MapPut("/employees/{id:int}", async (
    int id,
    EmployeeRequest request) =>
{
    await using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    const string sql = """
        UPDATE Employees
        SET
            Name = @Name,
            Department = @Department,
            Email = @Email
        WHERE EmployeeId = @EmployeeId
        """;

    await using var command = new SqlCommand(sql, connection);

    command.Parameters.AddWithValue("@EmployeeId", id);
    command.Parameters.AddWithValue("@Name", request.Name);

    command.Parameters.AddWithValue(
        "@Department",
        (object?)request.Department ?? DBNull.Value
    );

    command.Parameters.AddWithValue(
        "@Email",
        (object?)request.Email ?? DBNull.Value
    );

    var rowsAffected = await command.ExecuteNonQueryAsync();

    if (rowsAffected == 0)
    {
        return Results.NotFound(new
        {
            message = $"Employee with ID {id} was not found."
        });
    }

    return Results.Ok(new
    {
        message = "Employee updated successfully.",
        employeeId = id
    });
});

// -------------------------
// DELETE EMPLOYEE
// -------------------------

app.MapDelete("/employees/{id:int}", async (int id) =>
{
    await using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    const string sql = """
        DELETE FROM Employees
        WHERE EmployeeId = @EmployeeId
        """;

    await using var command = new SqlCommand(sql, connection);

    command.Parameters.AddWithValue("@EmployeeId", id);

    var rowsAffected = await command.ExecuteNonQueryAsync();

    if (rowsAffected == 0)
    {
        return Results.NotFound(new
        {
            message = $"Employee with ID {id} was not found."
        });
    }

    return Results.Ok(new
    {
        message = "Employee deleted successfully.",
        employeeId = id
    });
});

// =====================================================
// BLOB STORAGE
// =====================================================

var blobServiceClient = new BlobServiceClient(
    new Uri("https://stday30employee31568.blob.core.windows.net"),
    new DefaultAzureCredential()
);

var blobContainerClient =
    blobServiceClient.GetBlobContainerClient("employee-documents");


// -------------------------
// UPLOAD EMPLOYEE DOCUMENT
// -------------------------

app.MapPost("/employees/{id:int}/documents", async (
    int id,
    IFormFile file) =>
{
    if (file == null || file.Length == 0)
    {
        return Results.BadRequest(new
        {
            message = "A file is required."
        });
    }

    var blobName =
        $"employee-{id}/{Guid.NewGuid()}-{file.FileName}";

    var blobClient =
        blobContainerClient.GetBlobClient(blobName);

    await using var stream = file.OpenReadStream();

    await blobClient.UploadAsync(
        stream,
        overwrite: false
    );

    return Results.Ok(new
    {
        employeeId = id,
        fileName = file.FileName,
        blobName = blobName,
        url = blobClient.Uri.ToString()
    });
})
.DisableAntiforgery();


// -------------------------
// LIST EMPLOYEE DOCUMENTS
// -------------------------

app.MapGet("/employees/{id:int}/documents", async (int id) =>
{
    var prefix = $"employee-{id}/";

    var documents = new List<object>();

    await foreach (var blob in blobContainerClient.GetBlobsAsync(
        Azure.Storage.Blobs.Models.BlobTraits.None,
        Azure.Storage.Blobs.Models.BlobStates.None,
        prefix,
        CancellationToken.None))
    {
        documents.Add(new
        {
            name = blob.Name,
            size = blob.Properties.ContentLength,
            lastModified = blob.Properties.LastModified
        });
    }

    return Results.Ok(documents);
});

// -------------------------
// APPLICATION START
// -------------------------

app.Run();


// =====================================================
// MODELS
// =====================================================

record Employee(
    int EmployeeId,
    string Name,
    string? Department,
    string? Email
);

record EmployeeRequest(
    int EmployeeId,
    string Name,
    string? Department,
    string? Email
);