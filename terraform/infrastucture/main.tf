terraform {
  required_version = ">= 0.12"
  required_providers {
    azurerm = {
        source = "hashicorp/azurerm"
        version = "=2.96.0"
    }
  }
}

provider "azurerm"{
    features{

    }
}

resource "azurerm_rg" "sn_rg" {
    name = "sohatnotebookrg"
    location = "francecentral"
}