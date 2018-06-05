//////////////////////////////////////////////////////////////////////
// DEPENDENCIES
//////////////////////////////////////////////////////////////////////

#addin "Cake.Azure"
#addin "Cake.FileHelpers"
#addin "Cake.Incubator"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var resourceGroup = Argument("resourceGroup", EnvironmentVariable("RESOURCEGROUP") ?? null);

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


//////////////////////////////////////////////////////////////////////
// DO SOMETHING!!!!!
//////////////////////////////////////////////////////////////////////

RunTarget(target);