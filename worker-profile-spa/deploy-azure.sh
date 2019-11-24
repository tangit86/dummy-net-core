export RESOURCEGROUP_NAME=teratios_rg_Windows_uksouth
export STORAGE_NAME=workerapispa

export dist=`pwd`/dist/worker-profile-spa

az storage blob service-properties update --account-name $STORAGE_NAME --static-website --404-document index.html --index-document index.html

az storage blob upload-batch -s $dist -d \$web --account-name $STORAGE_NAME

az storage account show -n $STORAGE_NAME -g $RESOURCEGROUP_NAME --query "primaryEndpoints.web" --output tsv
