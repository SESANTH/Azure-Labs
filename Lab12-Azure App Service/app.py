from flask import Flask

app = Flask(__name__)

@app.route("/")
def home():
    return """
    <h1>Azure App Service</h1>
    <p>Hello from my Flask application!</p>
    <p>Day 12 of my Azure 30-Day Challenge.</p>
    """

@app.route("/health")
def health():
    return {
        "status": "healthy",
        "service": "Azure App Service"
    }

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8000)