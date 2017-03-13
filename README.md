# Azure Block Blob Storage Beginner Tutorial With C# In Visual Studio

## Requirements

- Basic C# knowledge
- Install Visual Studio
- Begin a Microsoft Azure subscription or [free trial](https://azure.microsoft.com/en-us/free/)
- Basic familiarity with navigating Azure portal website
- Create a resource group in the Azure portal website
- Create a blob storage account in the Azure portal website

## Set Up Visual Studio For Azure

### Start a new C# console application project

1. Open Visual Studio, and click on "New Project".
2. In the left panel, click "Visual C#".
3. In the middle panel, click "Console Application".
4. Click the OK button.

	![Visual Studio New Project C# Console](https://github.com/tvroley/AzureBlobStorageBeginnerTutorial/blob/master/images/VisualStudioNewProjectCSharpConsole.PNG)

### Install Azure packages with NuGet

1. Right click your project in the solution explorer, and open the NuGet Package Manager by clicking "Manage NuGet Packages".
2. Search for "Microsoft.WindowsAzure.ConfigurationManager" in the NuGet Package Manager, and click install.
3. Search for "WindowsAzure.Storage" in the NuGet Package Manager, and click install.

	![NuGet Azure Packages](https://github.com/tvroley/AzureBlobStorageBeginnerTutorial/blob/master/images/NuGetAzurePackages.PNG)

### Configure connection string

1. In the Visual Studio Solution Explorer, double click the "App.config" file to open it.
	
	![Solution Explorer App Config](https://github.com/tvroley/AzureBlobStorageBeginnerTutorial/blob/master/images/SolutionExplorerAppConfig.PNG)
	
2. Within the "App.config" file, after the line that says "</startup>", add the following code sample.  The value matching the "StorageConnectionString" key is the connection string for your storage account.  The connection string allows you to access your storage account using C# in a later step.

```xml
<appSettings>
  <add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=YourAccountName;AccountKey=YourKey" />
</appSettings>
```

> Replace "YourAccountName" after "AccountName=" with your actual Azure storage account name.  Replace "YourKey" after "AccountKey=" with your actual Azure storage account access key.

## Perform Basic Azure Blob Storage Actions With C# Code

### Open Program.cs 

* If you don't currently see Program.cs in the middle of your screen in Visual Studio, double click the file called "Program.cs" in the Solution Explorer panel.

### Enable your code to utilize Azure Libraries

* After the other using statements at the top of Program.cs, add the following using statements.  The Azure packages downloaded With NuGet earlier need these namespace declarations.

```C#
using Microsoft.Azure;  
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
```

### Create a connection to your Azure storage account

> **From now on, add the code samples in this section in order to your Main method in Program.cs.**

* The following code logs in to your storage account.  The Cloud Configuration Manager uses the connection string added to the configuration file earlier.

```C#
// logs in to your Azure storage account
CloudStorageAccount myStorageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
```
### Create a container

#### Create a blob client

* The blob client accesses blobs and containers in your Azure blob storage account.

```C#
CloudBlobClient myBlobClient = myStorageAccount.CreateCloudBlobClient();
```

#### Access a container in your blob storage by name

* The string parameter of the GetContainersReference method is the name of a container in your Azure storage account, which can either already exists, or the next step can create.

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

* The string parameter of the GetBlockBlobReference method is the name of a blob in your Azure storage account, which can either already exists, or the next step can create.

```C#
// Access a blob in a container by name
CloudBlockBlob myBlockBlob = myContainer.GetBlockBlobReference("myblockblob");
```

#### Upload a local blob to a cloud container

* When practicing your first time, you can use a text file as a blob.  Create a text file, find its file path, and replace "FilePath\FileName.txt" in the OpenRead method with the file path and name of your text file.  Keep the quotation marks used in the OpenRead method parameter, and remember the file extension. 

```C#
// Create a block blob 
using (var fileStream = System.IO.File.OpenRead(@"FilePath\FileName.txt"))
{
    myBlockBlob.UploadFromStream(fileStream);
}
```

### View items in a container

* The following code lists all the block blobs in a container by their URI in the console, and then waits for the user to press enter to continue.

```C#
// List blobs in a container by URI for the user 
foreach (IListBlobItem i in myContainer.ListBlobs(null, false))
{
	CloudBlockBlob myBlob = (CloudBlockBlob)i;
        Console.WriteLine(myBlob.Uri);
}

// Allows the user to view the blobs in the console before pressing enter
Console.ReadLine();
```

### Delete an item in a container

* The following code deletes the block blob in the cloud uploaded earlier in the program.  This occurs after the user presses enter once, which allows the user to view the contents of their storage account on the Azure portal website before and after the statement executes.

```C#
// Deletes the blob
myBlockBlob.Delete();

// Allows the user to view changes to their Azure storage account in the Azure portal website before pressing enter
Console.ReadLine();
```

### Delete a container

* The following code deletes the container in the cloud created earlier in the program.  This occurs after the user presses enter a second time to allow the user to view the contents of their storage account in the Azure portal website before and after the statement executes.

```C#
// Deletes the container
myContainer.Delete();
```

## Sample Code

> Remember to add a local text file and path to the OpenRead method in line 34 to experience uploading to the cloud 

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace AzureBlobStorageTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            // Logs in to your Azure storage account
            CloudStorageAccount myStorageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Initialize a blob client
            CloudBlobClient myBlobClient = myStorageAccount.CreateCloudBlobClient();

            // Access a container in your blob storage by name
            CloudBlobContainer myContainer = myBlobClient.GetContainerReference("mycontainer");

            // Creates a container with the name given to the blob client if it does not exists yet
            myContainer.CreateIfNotExists();

            // Access a blob in a container by name
            CloudBlockBlob myBlockBlob = myContainer.GetBlockBlobReference("myblockblob");

            // Create or replace a block blob
            // Add your own file and path in the OpenRead method parameters
            using (var fileStream = System.IO.File.OpenRead(@"FilePath\FileName"))
            {
                myBlockBlob.UploadFromStream(fileStream);
            }

            // List blobs in a container by URI for the user 
            foreach (IListBlobItem i in myContainer.ListBlobs(null, false))
            {
                CloudBlockBlob myBlob = (CloudBlockBlob)i;
                Console.WriteLine(myBlob.Uri);
            }

            // Allows the user to view the blobs in the console before pressing enter
            Console.ReadLine();

            // Delete the blob.
            myBlockBlob.Delete();
	    
	    // Allows the user to view changes to their Azure storage account in their Azure portal before pressing enter
            Console.ReadLine();

            // Delete the container
            myContainer.Delete();
        }
    }
}

```
