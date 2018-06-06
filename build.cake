//////////////////////////////////////////////////////////////////////
// DEPENDENCIES
//////////////////////////////////////////////////////////////////////
#addin nuget:?package=Newtonsoft.Json&version=10.0.3
#addin nuget:?package=Cake.Json&version=2.0.0.27
using Newtonsoft.Json;
#addin "Cake.Azure"
#addin "Cake.FileHelpers"
#addin "Cake.Incubator"


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var resourceGroup = Argument("resourceGroup", EnvironmentVariable("RESOURCEGROUP") ?? null);
var sqlLogin = Argument("sqlLogin", EnvironmentVariable("SQLLOGIN") ?? null);
var sqlPass = Argument("sqlPass", EnvironmentVariable("SQLPASS") ?? null);

//////////////////////////////////////////////////////////////////////
///    Build Variables
/////////////////////////////////////////////////////////////////////
var tenantId        =   EnvironmentVariable("PSAzureRMLabTool_TENANTID") ?? null;
var applicationId   =   EnvironmentVariable("PSAzureRMLabTool_APPID") ?? null;
var applicationPass =   EnvironmentVariable("PSAzureRMLabTool_APPSECRET") ?? null;
var subscriptionId  =   EnvironmentVariable("DEFAULT_AZURE_SUBSCRIPTION") ?? null;
var templatePath    =   @".\template\template.json";
var parametersPath  =  @".\template\parameters.json";
var configuration   = Argument("configuration", EnvironmentVariable("CONFIGURATION")) ?? "Release";
var buildTarget     = "net461";
var sourcepath      =  @".";
var binDir          = "bin";       // Destination Binary File Directory name i.e. bin
var objDir          = "obj";       // intermediate obj directory
var projDir         = sourcepath + @"\FootballPredictionGame";      //  Project Directory
var solutionFile    = sourcepath + @"\FootballPredictionGame.sln";
var binPath = Directory(projDir) + Directory(binDir) ;
var objPath = Directory(projDir) + Directory(objDir) ;
var outputPath = Directory(projDir) + Directory(binDir);



//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Default")
  .ReportError(ex => Information(ex.Message))
  .Does(() => {
      Information("Cake is installed!");
   });

Task("Transform")
  .ReportError(ex => Information(ex.Message))
  .Does(() => {
      Information("Task 'Transform' does nothing");
   });

Task("AzureRM")
  .ReportError(ex => Information(ex.Message))
  .Does(() => {
      if (resourceGroup == null){
	      throw new Exception("ERROR: No resource group specified for AzureRm deployment"); 
	  }
	  if (tenantId == null){
	      throw new Exception("ERROR: Cannot run AzureRM deployment as no tenant Id");
	  }
	  if (applicationId == null){
	      throw new Exception("ERROR: Cannot run AzureRM deployment as no application Id");
	  }
	  if (applicationPass == null){
	      throw new Exception("ERROR: Cannot run AzureRM deployment as no application secret");
	  }
	  
	  if (!FileExists(templatePath)){
	      throw new Exception("ERROR: Cannot find Azure RM template file at" + templatePath);
	  }
	  if (!FileExists(parametersPath)){
	      throw new Exception("ERROR: Cannot find Azure RM parameters file at" + parametersPath);
	  }
	  if (sqlLogin == null){
	      throw new Exception("ERROR: Cannot run AzureRM deployment as no SQL login");
	  }
	  if (sqlPass == null){
	      throw new Exception("ERROR: Cannot run AzureRM deployment as no SQL password");
	  }
	  
      Information("Running AzureRM resource deployment task against resource group " + resourceGroup + " using SP app id " + applicationId);
	  var credentials = AzureLogin(tenantId, applicationId,applicationPass);

	  var template = FileReadText(File(templatePath));
	  
	  //var parameters = FileReadText(File(parametersPath));
	  var paramsFileJson = ParseJsonFromFile(File(parametersPath));
	  var paramsObject = (JObject)paramsFileJson["parameters"];
	  var loginObject = new JObject();
	  loginObject["value"] = sqlLogin;
	  paramsObject.Add("sqlAdministratorLogin", loginObject);
	  var pwdObject = new JObject();
	  pwdObject["value"] = sqlPass;
	  paramsObject.Add("sqlAdministratorLoginPassword", pwdObject);
	  var parameters = SerializeJson(paramsFileJson);
	  Information(parameters);

	  var deploymentName = "Template" + DateTime.UtcNow.ToString("yyyyMMddhhmmss");
	 
	  if (AzureResourceGroupExists(credentials, subscriptionId, resourceGroup)){
	  	  Information("resource group {0} exist in subscription {1}", resourceGroup, subscriptionId);
		  DeployAzureResourceGroup(credentials, subscriptionId, resourceGroup, deploymentName, template, parameters);

	  } else {
	      Information("resource group {0} does not exists in subscription {1}", resourceGroup, subscriptionId);
	  }
   });


//////////////////////////////////////////////////////////////////////
// DO SOMETHING!!!!!
//////////////////////////////////////////////////////////////////////

RunTarget(target);