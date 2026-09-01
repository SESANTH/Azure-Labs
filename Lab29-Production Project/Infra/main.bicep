param location string = resourceGroup().location

param vnetName string = 'VNet-Production'
param vnetAddressPrefix string = '10.50.0.0/16'

param appSubnetName string = 'AppSubnet'
param appSubnetPrefix string = '10.50.1.0/24'

param privateEndpointSubnetName string = 'PrivateEndpointSubnet'
param privateEndpointSubnetPrefix string = '10.50.2.0/24'

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
