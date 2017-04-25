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
	
	    Console.WriteLine("Press enter to delete blob");

            /* Allows the user to view the blobs in the console or Azure portal before pressing enter */
            Console.ReadLine();

            // Delete the blob.
            myBlockBlob.Delete();
			
	    Console.WriteLine("Blob deleted");
            Console.WriteLine("Press enter to delete container");
            
            /* Allows the user to view changes to their Azure storage account 
            in the Azure portal website before pressing enter */
            Console.ReadLine();

            // Delete the container
            myContainer.Delete();
			
	    Console.WriteLine("Container deleted");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
