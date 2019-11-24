Set-Variable -Name "SUBSCRIPTION_ID" -Value ""
Set-Variable -Name "RESOURCEGROUP_NAME" -Value "wp"
Set-Variable -Name "LOCATION" -Value "uksouth"
Set-Variable -Name "SERVICEPLAN_NAME" -Value "wp"
Set-Variable -Name "SERVICEPLAN_SKU" -Value "F1"
Set-Variable -Name "WEBAPP_NAME" -Value "wp"
Set-Variable -Name "SQL_SERVER_NAME" -Value "wp"
Set-Variable -Name "SQL_ADMIN_USERNAME" -Value "wp-admin"
Set-Variable -Name "SQL_ADMIN_PASSWORD" -Value "wp-admin-pass"
Set-Variable -Name "SQL_DATABASE_OBJECTIVE" -Value "S0"
Set-Variable -Name "WP_DB_NAME" -Value "WorkerProfiles"
Set-Variable -Name "SQL_EDITION" -Value "Standard"
Set-Variable -Name "SQL_COLLATION" -Value "SQL_Latin1_General_CP1_CI_AS"
# use \ at the end of the path
Set-Variable -Name "LOCAL_GIT_DIRECTORY" -Value "\workspace\worker-profiles-6y7o0xj3qu\worker-profile-api\WorkerProfileApi"
Set-Variable -Name "DEPLOY_USER" -Value "wp-git"
Set-Variable -Name "DEPLOY_PASS" -Value "wp-git-pass"


#az login

# Select the Subscription
az account set --subscription $SUBSCRIPTION_ID

# Create the resource group
if (-not $(az group exists --name $RESOURCEGROUP_NAME) ){
    echo 'Creating resource group...'
    az group create --name $RESOURCEGROUP_NAME --location $LOCATION
}
else {
    echo 'Resource group exists...'
}


# # Create SQL Server
if ( -not $(az sql server list --query "length([?name=='$SQL_SERVER_NAME'])>``0``") ){
    echo 'Creating sql server instance...'
    az sql server create --name $SQL_SERVER_NAME --location $LOCATION --resource-group $RESOURCEGROUP_NAME --admin-user $SQL_ADMIN_USERNAME --admin-password $SQL_ADMIN_PASSWORD
    az sql server firewall-rule create --resource-group $RESOURCEGROUP_NAME --server $SQL_SERVER_NAME -n AllowAllIps --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
}
else {
    echo 'Sql Server exists...'
}

# # Create DB

if ( -not $(az sql db list --resource-group $RESOURCEGROUP_NAME --server $SQL_SERVER_NAME --query "length([?name=='$WP_DB_NAME'])>``0``") ) 
{
    echo 'Creating database...'
    az sql db create --resource-group $RESOURCEGROUP_NAME --server $SQL_SERVER_NAME --name $WP_DB_NAME --edition $SQL_EDITION --collation $SQL_COLLATION --service-objective $SQL_DATABASE_OBJECTIVE
}
else {
    echo 'Database exists...'
}


# # # Create Service Plan
if (-not $(az appservice plan list --query "length([?name=='$SERVICEPLAN_NAME'])>``0``") ) {
    echo 'Creating service plan...'
    az appservice plan create --name $SERVICEPLAN_NAME --resource-group $RESOURCEGROUP_NAME --sku $SERVICEPLAN_SKU --is-linux
}
else {
    echo 'Service plan exists...'
}


# # Create Web App
if ( -not $(az webapp list --query "length([?name=='$WEBAPP_NAME'])>``0``") ){
    echo 'Creating webapp...'
    az webapp create --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --plan $SERVICEPLAN_NAME --runtime "DOTNETCORE|3.0" --deployment-local-git
}
else
{
    echo 'Web app already exists...'
}

# # # Set the account-level deployment credentials
az webapp deployment user set --user-name $DEPLOY_USER --password $DEPLOY_PASS

# # Configure local Git and get deployment URL
Set-Variable -Name url -Value $(az webapp deployment source config-local-git --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --query url --output tsv)

# # Add the Azure remote to your local Git respository and push your code
cd $LOCAL_GIT_DIRECTORY

git remote add azure $url
# # When prompted for password, use the value of $password that you specified


# # Reload configurations
$WP_CONNECTIONSTRING="Server=tcp:$SQL_SERVER_NAME.database.windows.net,1433;Initial Catalog=$WP_DB_NAME;Persist Security Info=False;User ID=$SQL_ADMIN_USERNAME;Password=$SQL_ADMIN_PASSWORD;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
az webapp config appsettings set --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --settings DatabaseConnection="$WP_CONNECTIONSTRING"
az webapp config appsettings set --name $WEBAPP_NAME --resource-group $RESOURCEGROUP_NAME --settings `@`./appsettings.json

# # push (don't forget to commit changes)
git push azure master

# # # Copy the result of the following command into a browser to see the web app.
echo "Tip: Use this as <API HOST> parameter in `Worker Profiles SPA` http://$WEBAPP_NAME.azurewebsites.net"
