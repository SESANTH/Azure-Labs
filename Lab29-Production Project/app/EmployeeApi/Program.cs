using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var connectionString =
    "Server=tcp:sql-day29-prod-chandru.database.windows.net,1433;" +
    "Initial Catalog=EmployeeDB;" +
    "Authentication=Active Directory Default;" +
    "Encrypt=True;" +
    "TrustServerCertificate=False;" +
    "Connection Timeout=30;";

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        application = "Employee Management API",
        environment = "Azure",
        status = "Running"
    });
});

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy"
    });
});

app.MapGet("/employees", async () =>
{
    var employees = new List<object>();

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
        employees.Add(new
        {
            id = reader.GetInt32(0),
            name = reader.GetString(1),
            department = reader.IsDBNull(2)
                ? null
                : reader.GetString(2),
            email = reader.IsDBNull(3)
                ? null
                : reader.GetString(3)
        });
    }

    return Results.Ok(employees);
});

app.Run();