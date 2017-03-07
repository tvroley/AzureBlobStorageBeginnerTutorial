#Azure Blob Storage Beginner Tutorial Using C# In Visual Studio
##Requirements
- Install Visual Studio
- Begin a Microsoft Azure subscription
##Set Up Visual Studio
###Start A New C# Console Application Project
1. Open Visual Studio, and click on "New Project".
2. In the left panel, click "Visual C#".
3. In the middle panel, click "Console Application".
4. Click the OK button.
###Install Azure Packages With NuGet
1. Right click your project in the solution explorer, and Open the NuGet Package Manager by clicking "Manage NuGet Packages".
2. Search for "Microsoft.WindowsAzure.ConfigurationManager" in the NuGet Package Manager, and click install.
3. Search for "WindowsAzure.Storage" in the NuGet Package Manager, and click install.
>**Warning**: In my experience, installing WindowsAzure.Storage before Microsoft.WindowsAzure.ConfigurationManager caused an error that would not allow me to install Microsoft.WindowsAzure.ConfigurationManager.