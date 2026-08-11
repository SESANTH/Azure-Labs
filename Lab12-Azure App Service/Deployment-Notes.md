Day 12 – Azure App Service Deployment Notes

1. Objective

Deploy a simple Python Flask application from a local Windows development environment to an existing Azure App Service using Azure CLI and ZIP deployment.

2. Architecture

Local PC
   │
   │ Flask application
   │ app.py + requirements.txt
   ▼
Local Testing
   │
   │ python app.py
   ▼
Azure CLI
   │
   │ az login
   ▼
Existing Azure App Service
   │
   ├── Linux
   ├── Python 3.12
   ├── App Service Plan
   └── Gunicorn
   │
   ▼
Public HTTPS URL
   │
   ├── /
   └── /health

3. Application Structure

Lab12-Azure App Service/
│
├── app.py
├── requirements.txt
├── README.md
├── Notes.md
├── Deployment-Notes.md
└── Images/

The application contains:

/ – main application page

/health – health-check endpoint

4. Local Flask Application

Example app.py:

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

requirements.txt:

Flask
gunicorn

5. Test Locally

Activate the virtual environment:

.\venv\Scripts\activate

Install dependencies:

pip install -r requirements.txt

Run:

python app.py

Test:

http://localhost:8000
http://localhost:8000/health

Why test locally first?

Local testing separates application problems from Azure deployment problems.

6. Azure CLI Login

az login

Check the active subscription:

az account show

List subscriptions:

az account list --output table

If necessary:

az account set --subscription "<SUBSCRIPTION_NAME_OR_ID>"

7. Existing Azure Resources

For this lab, the Web App was already created.

Resource Group:
learning-webapp_group

Web App:
learning-webapp

Operating System:
Linux

Runtime:
Python 3.12

App Service Plan:
Learning-AppServicePlan

Pricing tier:
F1

Verify:

az webapp show `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --query "{Name:name,State:state,Host:defaultHostName}" `
  --output table

Important lesson:

Application deployment and infrastructure creation are separate tasks. An existing App Service can receive new application deployments without recreating the infrastructure.

8. Configure the Startup Command

az webapp config set `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --startup-file "gunicorn --bind=0.0.0.0:8000 app:app"

Understanding app:app

app:app
│  │
│  └── Flask application object
│
└───── Python module: app.py

Because app = Flask(__name__) exists inside app.py, Gunicorn uses app:app.

If the file were main.py and the Flask object were called app, the startup target would be:

main:app

9. Why Gunicorn?

Flask's built-in server is mainly intended for development.

Gunicorn is a production WSGI server commonly used to serve Python web applications on Linux.

Internet
   │
   ▼
Azure App Service
   │
   ▼
Gunicorn
   │
   ▼
Flask
   │
   ▼
app.py

10. Prepare the Deployment Package

Compress-Archive -Path app.py,requirements.txt `
  -DestinationPath deployment.zip -Force

The package contains:

deployment.zip
├── app.py
└── requirements.txt

Do not deploy local venv/ or .venv/ directories. Azure should install dependencies from requirements.txt.

11. Enable Build Automation

az webapp config appsettings set `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --settings SCM_DO_BUILD_DURING_DEPLOYMENT=true

This enables the App Service build process during deployment, including installation of dependencies from requirements.txt.

12. Deploy to Azure

For an existing Web App:

az webapp deploy `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --src-path "deployment.zip" `
  --type zip

Important:

az webapp deploy is a deployment command. It is NOT required every time you want to open the application.

13. Access the Deployed Application

After deployment, open the App Service HTTPS URL:

https://<your-web-app-hostname>.azurewebsites.net

The deployed application should display:

Azure App Service

Hello from my Flask application!

Day 12 of my Azure 30-Day Challenge.

Health endpoint:

https://<your-web-app-hostname>.azurewebsites.net/health

Expected:

{
    "status": "healthy",
    "service": "Azure App Service"
}

14. Do I Need to Deploy Every Time I Open the App?

No.

After deployment:

Local PC
   │
   └── Deployment only

Azure
   │
   └── Runs the application

You can close VS Code, PowerShell, and the local Python process. The Azure application continues running on Azure.

Simply open the HTTPS URL.

15. When Do I Need to Deploy Again?

Only when the application changes and you want the changes reflected in Azure.

Modify app.py
      ↓
Test locally
      ↓
Create new deployment.zip
      ↓
az webapp deploy
      ↓
Azure updates application

Create a new package:

Compress-Archive -Path app.py,requirements.txt `
  -DestinationPath deployment.zip -Force

Deploy:

az webapp deploy `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --src-path "deployment.zip" `
  --type zip

The App Service does not need to be recreated.

16. Start, Stop and Restart

Stop:

az webapp stop `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp"

Start:

az webapp start `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp"

Restart:

az webapp restart `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp"

These are different from deployment.

17. Troubleshooting

If the application does not work after deployment, check logs instead of blindly redeploying.

Enable logging:

az webapp log config `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --application-logging filesystem `
  --level information

View logs:

az webapp log tail `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp"

Common problems:

ModuleNotFoundError

Check:

requirements.txt

dependency installation

build automation

Gunicorn startup error

Check:

Python filename

Flask object name

startup command

Application fails to start

Check:

runtime version

startup command

environment variables

application logs

dependencies

port/configuration

18. Future Python App Deployment Checklist

[ ] 1. Application works locally
[ ] 2. requirements.txt exists
[ ] 3. Correct Python runtime selected
[ ] 4. Azure CLI installed
[ ] 5. az login completed
[ ] 6. Correct subscription selected
[ ] 7. Correct Resource Group identified
[ ] 8. Correct Web App identified
[ ] 9. Startup command configured
[ ] 10. Deployment package prepared
[ ] 11. Build/dependency installation configured
[ ] 12. Application deployed
[ ] 13. Logs checked if necessary
[ ] 14. HTTPS URL tested
[ ] 15. Health endpoint tested

19. Deployment Flow to Memorize

CODE
 ↓
requirements.txt
 ↓
LOCAL TEST
 ↓
az login
 ↓
SUBSCRIPTION
 ↓
RESOURCE GROUP
 ↓
WEB APP
 ↓
RUNTIME
 ↓
STARTUP COMMAND
 ↓
PACKAGE
 ↓
BUILD / DEPENDENCIES
 ↓
DEPLOY
 ↓
LOGS
 ↓
HTTPS TEST

20. Infrastructure vs Application vs Deployment

Infrastructure

Resource Group
      ↓
App Service Plan
      ↓
Web App

Application

app.py
requirements.txt
Flask
Gunicorn

Deployment

Local application
      ↓
deployment.zip
      ↓
az webapp deploy
      ↓
Azure Web App

You can change the application without recreating the infrastructure.

21. Real-World CI/CD Evolution

This lab used manual deployment:

Developer
   ↓
Local machine
   ↓
ZIP
   ↓
Azure CLI
   ↓
App Service

In a real organization, this is commonly automated:

Developer
   ↓
Git push
   ↓
GitHub / Azure DevOps
   ↓
CI/CD Pipeline
   ├── Install dependencies
   ├── Run tests
   ├── Build
   └── Deploy
          ↓
    Azure App Service

Typical tools:
GitHub Actions
Azure DevOps Pipelines
Azure CLI
Bicep / Terraform
