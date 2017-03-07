# Azure Block Blob Storage Beginner Tutorial With C# In Visual Studio

## Requirements

- Install Visual Studio
- Begin a Microsoft Azure subscription

## Set Up Visual Studio For Azure

### Start a new C# console application project

1. Open Visual Studio, and click on "New Project".
2. In the left panel, click "Visual C#".
3. In the middle panel, click "Console Application".
4. Click the OK button.

### Install Azure packages with NuGet

1. Right click your project in the solution explorer, and Open the NuGet Package Manager by clicking "Manage NuGet Packages".
2. Search for "Microsoft.WindowsAzure.ConfigurationManager" in the NuGet Package Manager, and click install.
3. Search for "WindowsAzure.Storage" in the NuGet Package Manager, and click install.

> **Warning**: In my experience, installing WindowsAzure.Storage before Microsoft.WindowsAzure.ConfigurationManager caused an error that would not allow me to install Microsoft.WindowsAzure.ConfigurationManager.

### Configure connection string

1. In the Visual Studio Solution Explorer, double click the "App.config" file to open it.
2. Within the "App.config" file, add the following code after the line that says "</startup>".  Replace "YourAccountName" after "AccountName=" with your Microsoft Azure account name.  Replace "YourKey" after "AccountKey=" with your Microsoft Azure account access key.  The value for the "StorageConnectionString" key is the connection string for your account.
```xml
      <appSettings>
        <add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=YourAccountName;AccountKey=YourKey" />
      </appSettings>
```
## Perform Basic Azure Blob Storage Actions With C# Code

### Add using statements for Azure packages

* After the other using statements at the top of Program.cs, add the following using statements.  The Azure packages downloaded With NuGet before need these namespace declarations.
```C#
using Microsoft.Azure;  
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
```

### Connect to your Azure account using Cloud Configuration Manager

* Add the following code to your Main method.  The Cloud Configuration Manager uses your connection string to access your account.
```C#
// logs in to your Azure storage account
CloudStorageAccount myStorageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
```

### Initialize a blob client

* The blob client accesses blobs and containers in your Azure blob storage account.  Add this code after the Cloud Configuration Manager code in the Main method.

```C#
CloudBlobClient myBlobClient = myStorageAccount.CreateCloudBlobClient();
```

### Access a container in your blob storage by name

* Add this code in the Main method after the code for initializing the blob client.

```C#
// Access a container in your blob storage by name
CloudBlobContainer myContainer = blobClient.GetContainerReference("mycontainer");
```

> **Note**: It's okay for now if a container with that name does not exist yet.

### Create a container with the name given to the blob client if it does not exist yet
```C#
container.CreateIfNotExists();
```


