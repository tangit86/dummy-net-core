#!/bin/sh

export SUBSCRIPTION_ID=
#
#
export RESOURCEGROUP_NAME=wp
#
#
export LOCATION=uksouth
#
#
export SERVICEPLAN_NAME=wp
#
#
export SERVICEPLAN_SKU=F1
#
#
export WEBAPP_NAME=wp
#
#
# ################ SQL SERVER CONFIG ####################
#
#
export SQL_SERVER_NAME=wp
#
#
export SQL_ADMIN_USERNAME=wp-admin
#
#
export SQL_ADMIN_PASSWORD=wp-admin-pass
#
#
################ SQL DATABASE CONFIG ####################
#
export SQL_DATABASE_OBJECTIVE=S0
#
#
export WP_DB_NAME=WorkerProfiles
#
#
export SQL_EDITION=Standard
#
# SQL Edition parameter
#
export SQL_COLLATION=SQL_Latin1_General_CP1_CI_AS
#
# ################ LOCAL GIT DEPLOYMENT ####################
# Set it to the project's root folder
#
export LOCAL_GIT_DIRECTORY=`pwd`/WorkerProfileApi
#
#Choose a username for the remote (azure) repository access
#
export DEPLOY_USER=wp-git
#
#Choose a password for the remote (azure) repository access
#
export DEPLOY_PASS=wp-git-pass
#
#
#############################################################

az login

# Select the Subscription
az account set --subscription $SUBSCRIPTION_ID

# Create the resource group
if [ $(az group exists --name $RESOURCEGROUP_NAME) = false ]; then
    echo 'Creating resource group...'
    az group create --name $RESOURCEGROUP_NAME --location $LOCATION
else
    echo 'Resource group exists...'
fi

# # Create SQL Server
if [ $(az sql server list --query "length([?name=='$SQL_SERVER_NAME'])>\`0\`") = false ]; then
    echo 'Creating sql server instance...'
    az sql server create --name $SQL_SERVER_NAME --location $LOCATION --resource-group $RESOURCEGROUP_NAME --admin-user $SQL_ADMIN_USERNAME --admin-password $SQL_ADMIN_PASSWORD
    az sql server firewall-rule create --resource-group $RESOURCEGROUP_NAME --server $SQL_SERVER_NAME -n AllowAllIps --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
else
    echo 'Sql Server exists...'
fi

# # Create DB
if [ $(az sql db list --resource-group $RESOURCEGROUP_NAME --server $SQL_SERVER_NAME --query "length([?name=='$WP_DB_NAME'])>\`0\`") = false ]; then
    echo 'Creating database...'
    az sql db create --resource-group $RESOURCEGROUP_NAME --server $SQL_SERVER_NAME --name $WP_DB_NAME --edition $SQL_EDITION --collation $SQL_COLLATION --service-objective $SQL_DATABASE_OBJECTIVE
else
    echo 'Database exists...'
fi


# # # Create Service Plan
if [ $(az appservice plan list --query "length([?name=='$SERVICEPLAN_NAME'])>\`0\`") = false ]; then
    echo 'Creating service plan...'
    az appservice plan create --name $SERVICEPLAN_NAME --resource-group $RESOURCEGROUP_NAME --sku $SERVICEPLAN_SKU --is-linux
else
    echo 'Service plan exists...'
fi

# # Create Web App
if [ $(az webapp list --query "length([?name=='$WEBAPP_NAME'])>\`0\`") = false ]; then
    echo 'Creating webapp...'
    az webapp create --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --plan $SERVICEPLAN_NAME --runtime "DOTNETCORE|3.0" --deployment-local-git
else
    echo 'Web app already exists...'
fi

# # # Set the account-level deployment credentials
az webapp deployment user set --user-name $DEPLOY_USER --password $DEPLOY_PASS

# # Configure local Git and get deployment URL
url=$(az webapp deployment source config-local-git --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --query url --output tsv)

# # Add the Azure remote to your local Git respository and push your code
cd $LOCAL_GIT_DIRECTORY

git remote add azure $url
# # When prompted for password, use the value of $password that you specified


# # Reload configurations
export WP_CONNECTIONSTRING="Server=tcp:$SQL_SERVER_NAME.database.windows.net,1433;Initial Catalog=$WP_DB_NAME;Persist Security Info=False;User ID=$SQL_ADMIN_USERNAME;Password=$SQL_ADMIN_PASSWORD;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
az webapp config appsettings set --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --settings DatabaseConnection="$WP_CONNECTIONSTRING"
az webapp config appsettings set --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --settings @appsettings.json

# # push (don't forget to commit changes)
git push azure master

# # # Copy the result of the following command into a browser to see the web app.
echo "Tip: Use this as <API HOST> parameter in `Worker Profiles SPA` http://$WEBAPP_NAME.azurewebsites.net"
