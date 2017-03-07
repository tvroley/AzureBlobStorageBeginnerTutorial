# Azure Blob Storage Beginner Tutorial Using C# In Visual Studio

## Requirements

- Install Visual Studio
- Begin a Microsoft Azure subscription

## Set Up Visual Studio

### Start A New C# Console Application Project

1. Open Visual Studio, and click on "New Project".
2. In the left panel, click "Visual C#".
3. In the middle panel, click "Console Application".
4. Click the OK button.

### Install Azure Packages With NuGet

1. Right click your project in the solution explorer, and Open the NuGet Package Manager by clicking "Manage NuGet Packages".
2. Search for "Microsoft.WindowsAzure.ConfigurationManager" in the NuGet Package Manager, and click install.
3. Search for "WindowsAzure.Storage" in the NuGet Package Manager, and click install.

> **Warning**: In my experience, installing WindowsAzure.Storage before Microsoft.WindowsAzure.ConfigurationManager caused an error that would not allow me to install Microsoft.WindowsAzure.ConfigurationManager.

### Configure Connection String

1. In the Visual Studio Solution Explorer, double click the "App.config" file to open it.  The file will contain code similar to the following.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
    </startup>
</configuration>
```
2. Within the "App.config" file, add the following code after the line that says "</startup>".  Replace "YourAccountName" after "AccountName=" with your Microsoft Azure account name.  Replace "YourKey" after "AccountKey=" with your Microsoft Azure account access key.  This value is the connection string for your account.
```xml
      <appSettings>
        <add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=YourAccountName;AccountKey=YourKey" />
      </appSettings>
```
## Perform Basic Azure Blob Storage Actions With C# Code

### Add Using Statements For Azure Packages

* After the other using statements at the top of Program.cs, add the following using statements.  The Azure packages downloaded With NuGet before need these namespace declarations.
```C#
using Microsoft.Azure;  
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
```

### Connect To Your Azure Account Using Cloud Configuration Manager

* Add the following code to your Main method.  The Cloud Configuration Manager uses your connection string to access your account.
```C#
CloudStorageAccount myStorageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
```

