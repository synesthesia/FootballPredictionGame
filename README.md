## Readme

### Cake build script

Using [Cake](https://cakebuild.net/) build script to:
* do deployment of Azure resources (from RM template)
* ...

#### Test Cake installation

From a powershell prompt in the root of the solution 
(e.g. the Package Manager Console in Visual Studio), type:

```.\build.ps1 -Target Default ```

You should see a series of commands run through


#### Deploy resources to Azure

This requires the following environment variables to be set:

|Variable                   |Value|
|---------------------------|-----|
|DEFAULT_AZURE_SUBSCRIPTION |Guid Id of default azure subscription|
|PSAzureRMLabTool_TENANTID  |Guid Id of AD tenant|
|PSAzureRMLabTool_APPID     |Guid application Id of service principal used for deployment|
|PSAzureRMLabTool_APPSECRET |Applicaiton secret key of service principal used for deployment|

It also requires the correct resource definition in


```template\template.json```

```template\parameters.json```

And a pre-existing resource group in Azure for the application deployment

Once those are in place you can run

```.\build.ps1 -Target AzureRM -ScriptArgs '--resourceGroup=<NAMEOFRESOURCEGROUP>'|```