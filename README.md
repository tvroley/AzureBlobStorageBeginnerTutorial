# Azure Block Blob Storage Beginner Tutorial With C# In Visual Studio

## Requirements

- Basic C# knowledge
- Install Visual Studio
- Begin a Microsoft Azure subscription or free trial
- Create a resource group in the Azure portal website
- Create a blob storage account in the Azure portal website

## Set Up Visual Studio For Azure

### Start a new C# console application project

![Visual Studio New Project C# Console](https://github.com/tvroley/AzureBlobStorageBeginnerTutorial/blob/master/images/VisualStudioNewProjectCSharpConsole.PNG)

1. Open Visual Studio, and click on "New Project".
2. In the left panel, click "Visual C#".
3. In the middle panel, click "Console Application".
4. Click the OK button.

### Install Azure packages with NuGet

![NuGet Azure Packages](https://github.com/tvroley/AzureBlobStorageBeginnerTutorial/blob/master/images/NuGetAzurePackages.PNG)

1. Right click your project in the solution explorer, and Open the NuGet Package Manager by clicking "Manage NuGet Packages".
2. Search for "Microsoft.WindowsAzure.ConfigurationManager" in the NuGet Package Manager, and click install.
3. Search for "WindowsAzure.Storage" in the NuGet Package Manager, and click install.

> **Warning**: In my experience, installing WindowsAzure.Storage before Microsoft.WindowsAzure.ConfigurationManager caused an error that would not allow me to install Microsoft.WindowsAzure.ConfigurationManager.

### Configure connection string

![Solution Explorer App Config](https://github.com/tvroley/AzureBlobStorageBeginnerTutorial/blob/master/images/SolutionExplorerAppConfig.PNG)

1. In the Visual Studio Solution Explorer, double click the "App.config" file to open it.
2. Within the "App.config" file, add the following code after the line that says "</startup>".  Replace "YourAccountName" after "AccountName=" with your Microsoft Azure account name.  Replace "YourKey" after "AccountKey=" with your Microsoft Azure account access key.  The value for the "StorageConnectionString" key is the connection string for your account.
```xml
      <appSettings>
        <add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=YourAccountName;AccountKey=YourKey" />
      </appSettings>
```
## Perform Basic Azure Blob Storage Actions With C# Code

### Open Program.cs 

* If you don't currently see Program.cs in the middle of your screen in Visual Studio, double click the file called "Program.cs" in the Solution Explorer panel.

### Enable your code to utilize Azure Libraries

* After the other using statements at the top of Program.cs, add the following using statements.  The Azure packages downloaded With NuGet before need these namespace declarations.

```C#
using Microsoft.Azure;  
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
```

### Connect your code to your Azure account

> **From now on, add the displayed code in this section in order to your Main method in Program.cs.**

* The following code logs in to your storage account.  The Cloud Configuration Manager uses the connection string added to the configuration file earlier.

```C#
// logs in to your Azure storage account
CloudStorageAccount myStorageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
```
### Create a container

#### Initialize a blob client

* The blob client accesses blobs and containers in your Azure blob storage account.

```C#
CloudBlobClient myBlobClient = myStorageAccount.CreateCloudBlobClient();
```

#### Access a container in your blob storage by name

* The string parameter of the GetContainersReference method is the name of a container in your Azure storage account, which either already exists, or the next step will create.

```C#
// Access a container in your blob storage by name
CloudBlobContainer myContainer = myBlobClient.GetContainerReference("mycontainer");
```

#### Create the container if it does not exist

* The Following code creates a container with the name given to the blob client if it does not exists yet.

```C#
myContainer.CreateIfNotExists();
```

### Upload data to a container

#### Access a blob in a container by name

* The string parameter of the GetBlockBlobReference method is the name of a blob in your Azure storage account, which either already exists, or the next step will create.

```C#
// Access a blob in a container by name
CloudBlockBlob myBlockBlob = myContainer.GetBlockBlobReference("myblockblob");
```

#### Upload a local blob to a cloud container

* When practicing your first time, you can use a text file as a blob.  Create a text file, find its file path, and replace FilePath\FileName in the OpenRead method with the file path and name of your file.  Keep the quotation marks used in the OpenRead method parameter. 

```C#
// Create a block blob 
using (var fileStream = System.IO.File.OpenRead(@"FilePath\FileName"))
{
    myBlockBlob.UploadFromStream(fileStream);
}
```

### View items in a container

* The following code lists the block blobs in a container by their URI.

```C#
// List blobs in a container by URI for the user 
foreach (IListBlobItem i in myContainer.ListBlobs(null, false))
{
	CloudBlockBlob myBlob = (CloudBlockBlob)i;
        Console.WriteLine(myBlob.Uri);
}

// The following code allows the user to view the blobs in the console before pressing enter
Console.ReadLine();
```

### Delete an item in a container

* The following code deletes the block blob in the cloud uploaded earlier.  This occurs after the user presses enter to allow the user to view the results in the Azure portal website as the program runs.

```C#
// delete the blob
myBlockBlob.Delete();
```

### Delete a container

* The following code deletes the container in the cloud created earlier in the program.  This occurs after the user presses enter to allow the user to view the results in the Azure portal website as the program runs.

```C#
// delete the container
myContainer.Delete();
```
