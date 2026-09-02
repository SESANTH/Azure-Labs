param location string = resourceGroup().location

// =============================
// Networking Parameters
// =============================

param vnetName string = 'VNet-Production'
param vnetAddressPrefix string = '10.50.0.0/16'

param appSubnetName string = 'AppSubnet'
param appSubnetPrefix string = '10.50.1.0/24'

param privateEndpointSubnetName string = 'PrivateEndpointSubnet'
param privateEndpointSubnetPrefix string = '10.50.2.0/24'

// =============================
// Virtual Network
// =============================

resource vnet 'Microsoft.Network/virtualNetworks@2024-01-01' = {
  name: vnetName
  location: location

  properties: {
    addressSpace: {
      addressPrefixes: [
        vnetAddressPrefix
      ]
    }

    subnets: [
      {
        name: appSubnetName

        properties: {
          addressPrefix: appSubnetPrefix
        }
      }

      {
        name: privateEndpointSubnetName

        properties: {
          addressPrefix: privateEndpointSubnetPrefix
        }
      }
    ]
  }
}

// =============================
// Storage Account
// =============================

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: 'stday30employee31568'
  location: location

  sku: {
    name: 'Standard_LRS'
  }

  kind: 'StorageV2'

  properties: {
    accessTier: 'Hot'
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
    supportsHttpsTrafficOnly: true
  }
}

// =============================
// Blob Container
// =============================

resource blobContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2023-05-01' = {
  name: '${storageAccount.name}/default/employee-documents'

  properties: {
    publicAccess: 'None'
  }
}
