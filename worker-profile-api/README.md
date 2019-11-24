# Worker Profile API

## Run locally

In order to run the project, you first need to install .NET Core. After that, you can clone this repo, go into each of the samples folders and either:

Run from source using the following commands: `dotnet run`

- The application starts at: https://localhost:5001/
- Swagger available at `/swagger`
- Under `/api/v1/` , all the routes

## Deployment

You will need to have installed:

- Git
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)

### In Linux

Make the deployment script executable: `chmod +x azure-deploy.sh`
The first time you run the script, it will attempt to create the required infrastructure on Azure. Set
Everytime you want to deploy changes run: `./azure-deploy.sh` or simply `git push azure master` and the deployment process will be triggered.

### In Windows

Use the powershell script `azure-deploy.ps1`
