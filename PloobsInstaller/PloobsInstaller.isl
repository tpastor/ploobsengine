<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<?xml-stylesheet type="text/xsl" href="is.xsl" ?>
<!DOCTYPE msi [
   <!ELEMENT msi   (summary,table*)>
   <!ATTLIST msi version    CDATA #REQUIRED>
   <!ATTLIST msi xmlns:dt   CDATA #IMPLIED
                 codepage   CDATA #IMPLIED
                 compression (MSZIP|LZX|none) "LZX">
   
   <!ELEMENT summary       (codepage?,title?,subject?,author?,keywords?,comments?,
                            template,lastauthor?,revnumber,lastprinted?,
                            createdtm?,lastsavedtm?,pagecount,wordcount,
                            charcount?,appname?,security?)>
                            
   <!ELEMENT codepage      (#PCDATA)>
   <!ELEMENT title         (#PCDATA)>
   <!ELEMENT subject       (#PCDATA)>
   <!ELEMENT author        (#PCDATA)>
   <!ELEMENT keywords      (#PCDATA)>
   <!ELEMENT comments      (#PCDATA)>
   <!ELEMENT template      (#PCDATA)>
   <!ELEMENT lastauthor    (#PCDATA)>
   <!ELEMENT revnumber     (#PCDATA)>
   <!ELEMENT lastprinted   (#PCDATA)>
   <!ELEMENT createdtm     (#PCDATA)>
   <!ELEMENT lastsavedtm   (#PCDATA)>
   <!ELEMENT pagecount     (#PCDATA)>
   <!ELEMENT wordcount     (#PCDATA)>
   <!ELEMENT charcount     (#PCDATA)>
   <!ELEMENT appname       (#PCDATA)>
   <!ELEMENT security      (#PCDATA)>                            
                                
   <!ELEMENT table         (col+,row*)>
   <!ATTLIST table
                name        CDATA #REQUIRED>

   <!ELEMENT col           (#PCDATA)>
   <!ATTLIST col
                 key       (yes|no) #IMPLIED
                 def       CDATA #IMPLIED>
                 
   <!ELEMENT row            (td+)>
   
   <!ELEMENT td             (#PCDATA)>
   <!ATTLIST td
                 href       CDATA #IMPLIED
                 dt:dt     (string|bin.base64) #IMPLIED
                 md5        CDATA #IMPLIED>
]>
<msi version="2.0" xmlns:dt="urn:schemas-microsoft-com:datatypes" codepage="65001">
	
	<summary>
		<codepage>1252</codepage>
		<title>##ID_STRING14##</title>
		<subject>PloobsEngine</subject>
		<author>##ID_STRING3##</author>
		<keywords>Installer,MSI,Database</keywords>
		<comments>Contact:  Your local administrator</comments>
		<template>Intel;1033</template>
		<lastauthor>Administrator</lastauthor>
		<revnumber>{390045EE-BE23-4F12-A5D5-174F72C57D44}</revnumber>
		<lastprinted/>
		<createdtm>06/21/1999 11:00</createdtm>
		<lastsavedtm>07/14/2000 14:50</lastsavedtm>
		<pagecount>200</pagecount>
		<wordcount>0</wordcount>
		<charcount/>
		<appname>InstallShield Express</appname>
		<security>1</security>
	</summary>
	
	<table name="ActionText">
		<col key="yes" def="s72">Action</col>
		<col def="L64">Description</col>
		<col def="L128">Template</col>
		<row><td>Advertise</td><td>##IDS_ACTIONTEXT_Advertising##</td><td/></row>
		<row><td>AllocateRegistrySpace</td><td>##IDS_ACTIONTEXT_AllocatingRegistry##</td><td>##IDS_ACTIONTEXT_FreeSpace##</td></row>
		<row><td>AppSearch</td><td>##IDS_ACTIONTEXT_SearchInstalled##</td><td>##IDS_ACTIONTEXT_PropertySignature##</td></row>
		<row><td>BindImage</td><td>##IDS_ACTIONTEXT_BindingExes##</td><td>##IDS_ACTIONTEXT_File##</td></row>
		<row><td>CCPSearch</td><td>##IDS_ACTIONTEXT_UnregisterModules##</td><td/></row>
		<row><td>CostFinalize</td><td>##IDS_ACTIONTEXT_ComputingSpace3##</td><td/></row>
		<row><td>CostInitialize</td><td>##IDS_ACTIONTEXT_ComputingSpace##</td><td/></row>
		<row><td>CreateFolders</td><td>##IDS_ACTIONTEXT_CreatingFolders##</td><td>##IDS_ACTIONTEXT_Folder##</td></row>
		<row><td>CreateShortcuts</td><td>##IDS_ACTIONTEXT_CreatingShortcuts##</td><td>##IDS_ACTIONTEXT_Shortcut##</td></row>
		<row><td>DeleteServices</td><td>##IDS_ACTIONTEXT_DeletingServices##</td><td>##IDS_ACTIONTEXT_Service##</td></row>
		<row><td>DuplicateFiles</td><td>##IDS_ACTIONTEXT_CreatingDuplicate##</td><td>##IDS_ACTIONTEXT_FileDirectorySize##</td></row>
		<row><td>FileCost</td><td>##IDS_ACTIONTEXT_ComputingSpace2##</td><td/></row>
		<row><td>FindRelatedProducts</td><td>##IDS_ACTIONTEXT_SearchForRelated##</td><td>##IDS_ACTIONTEXT_FoundApp##</td></row>
		<row><td>GenerateScript</td><td>##IDS_ACTIONTEXT_GeneratingScript##</td><td>##IDS_ACTIONTEXT_1##</td></row>
		<row><td>ISLockPermissionsCost</td><td>##IDS_ACTIONTEXT_ISLockPermissionsCost##</td><td/></row>
		<row><td>ISLockPermissionsInstall</td><td>##IDS_ACTIONTEXT_ISLockPermissionsInstall##</td><td/></row>
		<row><td>InstallAdminPackage</td><td>##IDS_ACTIONTEXT_CopyingNetworkFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize##</td></row>
		<row><td>InstallFiles</td><td>##IDS_ACTIONTEXT_CopyingNewFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize2##</td></row>
		<row><td>InstallODBC</td><td>##IDS_ACTIONTEXT_InstallODBC##</td><td/></row>
		<row><td>InstallSFPCatalogFile</td><td>##IDS_ACTIONTEXT_InstallingSystemCatalog##</td><td>##IDS_ACTIONTEXT_FileDependencies##</td></row>
		<row><td>InstallServices</td><td>##IDS_ACTIONTEXT_InstallServices##</td><td>##IDS_ACTIONTEXT_Service2##</td></row>
		<row><td>InstallValidate</td><td>##IDS_ACTIONTEXT_Validating##</td><td/></row>
		<row><td>LaunchConditions</td><td>##IDS_ACTIONTEXT_EvaluateLaunchConditions##</td><td/></row>
		<row><td>MigrateFeatureStates</td><td>##IDS_ACTIONTEXT_MigratingFeatureStates##</td><td>##IDS_ACTIONTEXT_Application##</td></row>
		<row><td>MoveFiles</td><td>##IDS_ACTIONTEXT_MovingFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize3##</td></row>
		<row><td>PatchFiles</td><td>##IDS_ACTIONTEXT_PatchingFiles##</td><td>##IDS_ACTIONTEXT_FileDirSize4##</td></row>
		<row><td>ProcessComponents</td><td>##IDS_ACTIONTEXT_UpdateComponentRegistration##</td><td/></row>
		<row><td>PublishComponents</td><td>##IDS_ACTIONTEXT_PublishingQualifiedComponents##</td><td>##IDS_ACTIONTEXT_ComponentIDQualifier##</td></row>
		<row><td>PublishFeatures</td><td>##IDS_ACTIONTEXT_PublishProductFeatures##</td><td>##IDS_ACTIONTEXT_FeatureColon##</td></row>
		<row><td>PublishProduct</td><td>##IDS_ACTIONTEXT_PublishProductInfo##</td><td/></row>
		<row><td>RMCCPSearch</td><td>##IDS_ACTIONTEXT_SearchingQualifyingProducts##</td><td/></row>
		<row><td>RegisterClassInfo</td><td>##IDS_ACTIONTEXT_RegisterClassServer##</td><td>##IDS_ACTIONTEXT_ClassId##</td></row>
		<row><td>RegisterComPlus</td><td>##IDS_ACTIONTEXT_RegisteringComPlus##</td><td>##IDS_ACTIONTEXT_AppIdAppTypeRSN##</td></row>
		<row><td>RegisterExtensionInfo</td><td>##IDS_ACTIONTEXT_RegisterExtensionServers##</td><td>##IDS_ACTIONTEXT_Extension2##</td></row>
		<row><td>RegisterFonts</td><td>##IDS_ACTIONTEXT_RegisterFonts##</td><td>##IDS_ACTIONTEXT_Font##</td></row>
		<row><td>RegisterMIMEInfo</td><td>##IDS_ACTIONTEXT_RegisterMimeInfo##</td><td>##IDS_ACTIONTEXT_ContentTypeExtension##</td></row>
		<row><td>RegisterProduct</td><td>##IDS_ACTIONTEXT_RegisteringProduct##</td><td>##IDS_ACTIONTEXT_1b##</td></row>
		<row><td>RegisterProgIdInfo</td><td>##IDS_ACTIONTEXT_RegisteringProgIdentifiers##</td><td>##IDS_ACTIONTEXT_ProgID2##</td></row>
		<row><td>RegisterTypeLibraries</td><td>##IDS_ACTIONTEXT_RegisterTypeLibs##</td><td>##IDS_ACTIONTEXT_LibId##</td></row>
		<row><td>RegisterUser</td><td>##IDS_ACTIONTEXT_RegUser##</td><td>##IDS_ACTIONTEXT_1c##</td></row>
		<row><td>RemoveDuplicateFiles</td><td>##IDS_ACTIONTEXT_RemovingDuplicates##</td><td>##IDS_ACTIONTEXT_FileDir##</td></row>
		<row><td>RemoveEnvironmentStrings</td><td>##IDS_ACTIONTEXT_UpdateEnvironmentStrings##</td><td>##IDS_ACTIONTEXT_NameValueAction2##</td></row>
		<row><td>RemoveExistingProducts</td><td>##IDS_ACTIONTEXT_RemoveApps##</td><td>##IDS_ACTIONTEXT_AppCommandLine##</td></row>
		<row><td>RemoveFiles</td><td>##IDS_ACTIONTEXT_RemovingFiles##</td><td>##IDS_ACTIONTEXT_FileDir2##</td></row>
		<row><td>RemoveFolders</td><td>##IDS_ACTIONTEXT_RemovingFolders##</td><td>##IDS_ACTIONTEXT_Folder1##</td></row>
		<row><td>RemoveIniValues</td><td>##IDS_ACTIONTEXT_RemovingIni##</td><td>##IDS_ACTIONTEXT_FileSectionKeyValue##</td></row>
		<row><td>RemoveODBC</td><td>##IDS_ACTIONTEXT_RemovingODBC##</td><td/></row>
		<row><td>RemoveRegistryValues</td><td>##IDS_ACTIONTEXT_RemovingRegistry##</td><td>##IDS_ACTIONTEXT_KeyName##</td></row>
		<row><td>RemoveShortcuts</td><td>##IDS_ACTIONTEXT_RemovingShortcuts##</td><td>##IDS_ACTIONTEXT_Shortcut1##</td></row>
		<row><td>Rollback</td><td>##IDS_ACTIONTEXT_RollingBack##</td><td>##IDS_ACTIONTEXT_1d##</td></row>
		<row><td>RollbackCleanup</td><td>##IDS_ACTIONTEXT_RemovingBackup##</td><td>##IDS_ACTIONTEXT_File2##</td></row>
		<row><td>SelfRegModules</td><td>##IDS_ACTIONTEXT_RegisteringModules##</td><td>##IDS_ACTIONTEXT_FileFolder##</td></row>
		<row><td>SelfUnregModules</td><td>##IDS_ACTIONTEXT_UnregisterModules##</td><td>##IDS_ACTIONTEXT_FileFolder2##</td></row>
		<row><td>SetODBCFolders</td><td>##IDS_ACTIONTEXT_InitializeODBCDirs##</td><td/></row>
		<row><td>StartServices</td><td>##IDS_ACTIONTEXT_StartingServices##</td><td>##IDS_ACTIONTEXT_Service3##</td></row>
		<row><td>StopServices</td><td>##IDS_ACTIONTEXT_StoppingServices##</td><td>##IDS_ACTIONTEXT_Service4##</td></row>
		<row><td>UnmoveFiles</td><td>##IDS_ACTIONTEXT_RemovingMoved##</td><td>##IDS_ACTIONTEXT_FileDir3##</td></row>
		<row><td>UnpublishComponents</td><td>##IDS_ACTIONTEXT_UnpublishQualified##</td><td>##IDS_ACTIONTEXT_ComponentIdQualifier2##</td></row>
		<row><td>UnpublishFeatures</td><td>##IDS_ACTIONTEXT_UnpublishProductFeatures##</td><td>##IDS_ACTIONTEXT_Feature##</td></row>
		<row><td>UnpublishProduct</td><td>##IDS_ACTIONTEXT_UnpublishingProductInfo##</td><td/></row>
		<row><td>UnregisterClassInfo</td><td>##IDS_ACTIONTEXT_UnregisterClassServers##</td><td>##IDS_ACTIONTEXT_ClsID##</td></row>
		<row><td>UnregisterComPlus</td><td>##IDS_ACTIONTEXT_UnregisteringComPlus##</td><td>##IDS_ACTIONTEXT_AppId##</td></row>
		<row><td>UnregisterExtensionInfo</td><td>##IDS_ACTIONTEXT_UnregisterExtensionServers##</td><td>##IDS_ACTIONTEXT_Extension##</td></row>
		<row><td>UnregisterFonts</td><td>##IDS_ACTIONTEXT_UnregisteringFonts##</td><td>##IDS_ACTIONTEXT_Font2##</td></row>
		<row><td>UnregisterMIMEInfo</td><td>##IDS_ACTIONTEXT_UnregisteringMimeInfo##</td><td>##IDS_ACTIONTEXT_ContentTypeExtension2##</td></row>
		<row><td>UnregisterProgIdInfo</td><td>##IDS_ACTIONTEXT_UnregisteringProgramIds##</td><td>##IDS_ACTIONTEXT_ProgID##</td></row>
		<row><td>UnregisterTypeLibraries</td><td>##IDS_ACTIONTEXT_UnregTypeLibs##</td><td>##IDS_ACTIONTEXT_Libid2##</td></row>
		<row><td>WriteEnvironmentStrings</td><td>##IDS_ACTIONTEXT_EnvironmentStrings##</td><td>##IDS_ACTIONTEXT_NameValueAction##</td></row>
		<row><td>WriteIniValues</td><td>##IDS_ACTIONTEXT_WritingINI##</td><td>##IDS_ACTIONTEXT_FileSectionKeyValue2##</td></row>
		<row><td>WriteRegistryValues</td><td>##IDS_ACTIONTEXT_WritingRegistry##</td><td>##IDS_ACTIONTEXT_KeyNameValue##</td></row>
	</table>

	<table name="AdminExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>InstallAdminPackage</td><td/><td>3900</td><td>InstallAdminPackage</td><td/></row>
		<row><td>InstallFiles</td><td/><td>4000</td><td>InstallFiles</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1500</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>4010</td><td>ScheduleReboot</td><td/></row>
	</table>

	<table name="AdminUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AdminWelcome</td><td/><td>1010</td><td>AdminWelcome</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>ExecuteAction</td><td/><td>1300</td><td>ExecuteAction</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>SetupCompleteError</td><td/><td>-3</td><td>SetupCompleteError</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td/><td>-1</td><td>SetupCompleteSuccess</td><td/></row>
		<row><td>SetupInitialization</td><td/><td>50</td><td>SetupInitialization</td><td/></row>
		<row><td>SetupInterrupted</td><td/><td>-2</td><td>SetupInterrupted</td><td/></row>
		<row><td>SetupProgress</td><td/><td>1020</td><td>SetupProgress</td><td/></row>
	</table>

	<table name="AdvtExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>CreateShortcuts</td><td/><td>4500</td><td>CreateShortcuts</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1500</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>MsiPublishAssemblies</td><td/><td>6250</td><td>MsiPublishAssemblies</td><td/></row>
		<row><td>PublishComponents</td><td/><td>6200</td><td>PublishComponents</td><td/></row>
		<row><td>PublishFeatures</td><td/><td>6300</td><td>PublishFeatures</td><td/></row>
		<row><td>PublishProduct</td><td/><td>6400</td><td>PublishProduct</td><td/></row>
		<row><td>RegisterClassInfo</td><td/><td>4600</td><td>RegisterClassInfo</td><td/></row>
		<row><td>RegisterExtensionInfo</td><td/><td>4700</td><td>RegisterExtensionInfo</td><td/></row>
		<row><td>RegisterMIMEInfo</td><td/><td>4900</td><td>RegisterMIMEInfo</td><td/></row>
		<row><td>RegisterProgIdInfo</td><td/><td>4800</td><td>RegisterProgIdInfo</td><td/></row>
		<row><td>RegisterTypeLibraries</td><td/><td>4910</td><td>RegisterTypeLibraries</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>6410</td><td>ScheduleReboot</td><td/></row>
	</table>

	<table name="AdvtUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="AppId">
		<col key="yes" def="s38">AppId</col>
		<col def="S255">RemoteServerName</col>
		<col def="S255">LocalService</col>
		<col def="S255">ServiceParameters</col>
		<col def="S255">DllSurrogate</col>
		<col def="I2">ActivateAtStorage</col>
		<col def="I2">RunAsInteractiveUser</col>
	</table>

	<table name="AppSearch">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="s72">Signature_</col>
		<row><td>SOFTWARE_WOW6432NODE_XNA_FRAMEWORK_V4.0</td><td>NewSignature1</td></row>
	</table>

	<table name="BBControl">
		<col key="yes" def="s50">Billboard_</col>
		<col key="yes" def="s50">BBControl</col>
		<col def="s50">Type</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="L50">Text</col>
	</table>

	<table name="Billboard">
		<col key="yes" def="s50">Billboard</col>
		<col def="s38">Feature_</col>
		<col def="S50">Action</col>
		<col def="I2">Ordering</col>
	</table>

	<table name="Binary">
		<col key="yes" def="s72">Name</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
		<row><td>ISExpHlp.dll</td><td/><td>&lt;ISProductFolder&gt;\redist\language independent\i386\ISExpHlp.dll</td></row>
		<row><td>ISSELFREG.DLL</td><td/><td>&lt;ISProductFolder&gt;\redist\language independent\i386\isregsvr.dll</td></row>
		<row><td>NewBinary1</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\banner.jpg</td></row>
		<row><td>NewBinary10</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CompleteSetupIco.ibd</td></row>
		<row><td>NewBinary11</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CustomSetupIco.ibd</td></row>
		<row><td>NewBinary12</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\DestIcon.ibd</td></row>
		<row><td>NewBinary13</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\NetworkInstall.ico</td></row>
		<row><td>NewBinary14</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\DontInstall.ico</td></row>
		<row><td>NewBinary15</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\Install.ico</td></row>
		<row><td>NewBinary16</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallFirstUse.ico</td></row>
		<row><td>NewBinary17</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallPartial.ico</td></row>
		<row><td>NewBinary18</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\InstallStateMenu.ico</td></row>
		<row><td>NewBinary19</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\banner.jpg</td></row>
		<row><td>NewBinary2</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\New.ibd</td></row>
		<row><td>NewBinary20</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\banner.jpg</td></row>
		<row><td>NewBinary21</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\banner.jpg</td></row>
		<row><td>NewBinary3</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\Up.ibd</td></row>
		<row><td>NewBinary4</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\WarningIcon.ibd</td></row>
		<row><td>NewBinary5</td><td/><td>&lt;ISProductFolder&gt;\Support\Themes\InstallShield Blue Theme\welcome.jpg</td></row>
		<row><td>NewBinary6</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\CustomSetupIco.ibd</td></row>
		<row><td>NewBinary7</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\ReinstIco.ibd</td></row>
		<row><td>NewBinary8</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\RemoveIco.ibd</td></row>
		<row><td>NewBinary9</td><td/><td>&lt;ISProductFolder&gt;\Redist\Language Independent\OS Independent\SetupIcon.ibd</td></row>
		<row><td>SetAllUsers.dll</td><td/><td>&lt;ISProductFolder&gt;\redist\language independent\i386\SetAllUsers.dll</td></row>
	</table>

	<table name="BindImage">
		<col key="yes" def="s72">File_</col>
		<col def="S255">Path</col>
	</table>

	<table name="CCPSearch">
		<col key="yes" def="s72">Signature_</col>
	</table>

	<table name="CheckBox">
		<col key="yes" def="s72">Property</col>
		<col def="S64">Value</col>
		<row><td>ISCHECKFORPRODUCTUPDATES</td><td>1</td></row>
		<row><td>LAUNCHPROGRAM</td><td>1</td></row>
		<row><td>LAUNCHREADME</td><td>1</td></row>
	</table>

	<table name="Class">
		<col key="yes" def="s38">CLSID</col>
		<col key="yes" def="s32">Context</col>
		<col key="yes" def="s72">Component_</col>
		<col def="S255">ProgId_Default</col>
		<col def="L255">Description</col>
		<col def="S38">AppId_</col>
		<col def="S255">FileTypeMask</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="S32">DefInprocHandler</col>
		<col def="S255">Argument</col>
		<col def="s38">Feature_</col>
		<col def="I2">Attributes</col>
	</table>

	<table name="ComboBox">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
	</table>

	<table name="CompLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="s38">ComponentId</col>
		<col def="I2">Type</col>
	</table>

	<table name="Complus">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="I2">ExpType</col>
	</table>

	<table name="Component">
		<col key="yes" def="s72">Component</col>
		<col def="S38">ComponentId</col>
		<col def="s72">Directory_</col>
		<col def="i2">Attributes</col>
		<col def="S255">Condition</col>
		<col def="S72">KeyPath</col>
		<col def="I4">ISAttributes</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISScanAtBuildFile</col>
		<col def="S255">ISRegFileToMergeAtBuild</col>
		<col def="S0">ISDotNetInstallerArgsInstall</col>
		<col def="S0">ISDotNetInstallerArgsCommit</col>
		<col def="S0">ISDotNetInstallerArgsUninstall</col>
		<col def="S0">ISDotNetInstallerArgsRollback</col>
		<row><td>AdvancedDemos.exe</td><td>{4FA585AA-059E-4051-8E33-B8197CC43717}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>advanceddemos.exe</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Assimp.Interop.dll</td><td>{7F4811E0-70FA-4328-9911-C8A57F115F14}</td><td>DLLS</td><td>2</td><td/><td>assimp.interop.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Assimp.dll</td><td>{C7A41AA5-1D60-461A-9DB2-3BD4603D6B7D}</td><td>DLLS</td><td>2</td><td/><td>assimp.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>BEPUphysics.dll</td><td>{7EA2D08F-CADB-4173-A533-09EB030D09A3}</td><td>PHONE7</td><td>2</td><td/><td>bepuphysics.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>BEPUphysics.dll1</td><td>{D833D258-2B8A-4E9F-9F46-DFE56C518714}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td>bepuphysics.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>DPSFPhone.dll</td><td>{45E51423-D813-4588-903B-C86D9A491A20}</td><td>PHONE7</td><td>2</td><td/><td>dpsfphone.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>DPSFPhone.dll1</td><td>{498C0B76-2634-42D9-BD24-2FB08A38F96B}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td>dpsfphone.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>FarseerPhysicsXNA.dll</td><td>{3162593E-AE35-4077-9ED7-C99F2A0CAD73}</td><td>PHONE7</td><td>2</td><td/><td>farseerphysicsxna.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>FarseerPhysicsXNA.dll1</td><td>{7AA13323-D6E7-42A0-8414-55AE37D9D6B7}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td>farseerphysicsxna.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT</td><td>{177595C9-FD18-4B21-AC9D-D7523BF8027F}</td><td>INSTALLDIR</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT1</td><td>{37C0058F-B9CE-42EB-A1C1-28ADDE92195D}</td><td>PersonalFolder</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT10</td><td>{78999A1B-B1AA-44BC-A607-7A2B826E6F80}</td><td>DUDE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT100</td><td>{A8FDA368-F077-496A-9274-EFBFC9255092}</td><td>REACHDEMOS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT101</td><td>{E0AA64BF-9FE1-49FD-AB79-DE7F2A4519CF}</td><td>REACHDEMOS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT102</td><td>{16715410-18F5-4ADC-83A0-08EC4560B362}</td><td>DLLS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT103</td><td>{0E4D8CDD-94BA-4C66-9E72-7AEDFE2D5E61}</td><td>ADVANCEDDEMOSCONTENT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT104</td><td>{B0D86881-25F4-40DC-9FA4-7E9FCB75CE4D}</td><td>EFFECTS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT105</td><td>{D40A2F2C-D2E5-418D-B3E0-135B47CF57AF}</td><td>MODEL1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT106</td><td>{0DD28562-2EDE-43EF-B207-B5AC2F7948E0}</td><td>DUDE1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT107</td><td>{8BD55F81-B231-4523-A1E1-381CB504F600}</td><td>MODELINFOS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT108</td><td>{D970CED5-928D-4F7C-AEB3-350B9F9A8A0D}</td><td>SKINS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT109</td><td>{41B08A2A-0F33-4480-8164-2FB0176BB8FB}</td><td>DEFAULT1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT11</td><td>{5D287E25-48C7-47EC-B0B6-892210129C17}</td><td>MODELINFOS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT110</td><td>{5B45EA15-4295-4CB2-82CC-F713478F0BD0}</td><td>CURSORS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT111</td><td>{3A0B59A5-25C3-4B10-9BF8-DFA493A41111}</td><td>FONTS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT112</td><td>{0F1DBF06-75D6-4F53-BFE0-B975DEABB71E}</td><td>IMAGES1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT113</td><td>{6031E6C0-CBC3-4BFA-AE89-80A8ECBC1B28}</td><td>SONGS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT114</td><td>{3304FAE6-8941-4EF8-95DF-A02818DCEBBE}</td><td>TEXTURES1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT115</td><td>{929DA059-DCCB-47B9-AC95-35C827BAEEDF}</td><td>DLL</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT116</td><td>{81B1D2F7-461E-48E9-AEF0-1BF0BEAC41CB}</td><td>SKINNED</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT117</td><td>{8A4A1DAD-01FC-4EEE-84ED-32C8D406C705}</td><td>PLOOBSPROJECTTEMPLATE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT118</td><td>{3A2429F6-0535-4B23-B50E-0A559B9A82EB}</td><td>_D_DEMOS2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT119</td><td>{ABC3AB9D-88FD-4743-B727-E16FA1876AD3}</td><td>HELPERS3</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT12</td><td>{3555CAF2-F42F-41B4-ABA7-2346CA36751D}</td><td>SKINS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT120</td><td>{4BFA05B8-8756-4B56-BC38-A7E08A854EAF}</td><td>ANIMATION</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT121</td><td>{967FA745-FC80-4402-B41D-F2BFB7C8C7FA}</td><td>BILBOARD</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT122</td><td>{DA2F63F9-D635-47D6-B946-E17303BA7F1A}</td><td>PARTICLE3D</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT123</td><td>{9A53F25D-87A2-4696-90F2-DBD1760A23B7}</td><td>POSTEFFECT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT124</td><td>{B0832A9F-2906-40A7-8D1A-439CE0C30F8F}</td><td>PROPERTIES4</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT125</td><td>{9573C5E9-9C21-4FE5-8F5E-AE096249DF34}</td><td>TEMPLATESCREENS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT126</td><td>{BAE3F2D9-17AA-4944-B056-71B7FBD56E78}</td><td>HELPER1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT127</td><td>{6B960D23-A936-41DC-BFB1-9DFEEFD86B45}</td><td>PLOOBSPROJECTTEMPLATECONTENT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT128</td><td>{8E0C5E8A-7DD4-4ECF-BD85-8FD44431A0B6}</td><td>MODEL4</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT129</td><td>{6AA5145D-DD44-42A0-A746-0EC2D2718639}</td><td>TERRAIN</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT13</td><td>{BC422F44-E191-4F81-BE77-0702668F5761}</td><td>DEFAULT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT130</td><td>{5F523310-56A4-4113-84A3-FCFD2CA07C2F}</td><td>TEXTURES4</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT131</td><td>{C9F81255-696C-4F4C-9ED1-17DC2E6E6A9E}</td><td>RVO1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT132</td><td>{0B375086-5D96-48E9-AE9D-214CFDBD8316}</td><td>PROPERTIES3</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT133</td><td>{39598EB4-8B3A-4A18-B61D-BD365E4285BF}</td><td>GRAPHICSAPP1LIBRARYCONTENT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT134</td><td>{3DCC341E-6B69-45B0-A3B8-7F4CD1EAE4C2}</td><td>MODEL3</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT135</td><td>{AFA193CF-D137-4751-AABC-FFFF298E939B}</td><td>TERRAIN1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT136</td><td>{57B3E594-AE3D-4338-9888-D66AE6DCE6B2}</td><td>TEXTURES3</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT137</td><td>{12CD3943-3054-4DBB-A0FF-0620A62DBAA6}</td><td>PHONE7</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT14</td><td>{B0692739-C530-4BDC-ADAB-BB74F2E47D4F}</td><td>CURSORS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT15</td><td>{72653DF2-DC0F-4615-B47E-F882C6DB3652}</td><td>FONTS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT16</td><td>{2E3DEDB8-999C-4D55-AD86-36719534C4E1}</td><td>IMAGES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT17</td><td>{F786EB72-3072-4459-95A5-F449D4923F75}</td><td>SONGS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT18</td><td>{1D6A5E6F-A5C1-4452-B286-ABA9FE1B4AC6}</td><td>DEMOSSOURCECODE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT19</td><td>{2095AB82-DE7B-4FB6-A86A-BA019F7B8E16}</td><td>ADVANCEDDEMOS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT2</td><td>{7A023A59-6091-4D7F-B94C-6971C2FFB8AC}</td><td>VISUAL_STUDIO_2010</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT20</td><td>{38BF76F7-7E75-4C53-8F3F-81CD7399DB59}</td><td>ADVANCEDDEMOS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT21</td><td>{216D060F-F43A-4901-B1C4-9ED8982ABBA6}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT22</td><td>{DF1C62A5-ACD0-4C50-A48F-50CC81B82400}</td><td>COMMONS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT23</td><td>{B8596A02-EF98-43B5-BC18-4E93A17827F0}</td><td>DEMO1____LOADER</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT24</td><td>{6048D8FD-B441-4E52-8FDA-BE8F64E6405C}</td><td>DEMO2___MATERIALS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT25</td><td>{0B77AEF7-DE39-4A0A-B0FE-2EBD44DF969A}</td><td>DEMO3___SOUND</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT26</td><td>{F89EE6DC-1861-4716-8DB3-1FC9B8FAE5C3}</td><td>DEMO4___TERRAIN</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT27</td><td>{A2C72BF3-10BE-4A69-B211-9D1A8FCD00F4}</td><td>DEMO5___PARTICLESAMPLES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT28</td><td>{8BB2E4CD-5EF4-4299-8BD5-BB007158DA76}</td><td>HELPERS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT29</td><td>{08ED5ADD-E9C9-42B2-87B9-7BB4C4A5A35E}</td><td>DEMO6___BILBOARDSSAMPLES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT3</td><td>{AE8C66A4-AE30-441C-9619-A8653A8B81FB}</td><td>TEMPLATES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT30</td><td>{2A7CD6E7-3124-45E9-AC20-ED8248A8587C}</td><td>DEMO7___ANIMATIONSAMPLE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT31</td><td>{22B8DB3B-A5FB-4C95-9800-3ABE9B95975A}</td><td>CUSTOMSKINNEDANIMATION</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT32</td><td>{860ABF20-D3F2-4858-9E69-821502CE4B75}</td><td>MODELANIM</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT33</td><td>{32D66231-F0B9-4B77-840F-E5F9C79F8CCB}</td><td>DEMO8___GUI</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT34</td><td>{47EFCB4C-85E6-4A71-9B92-36E8D7AFA289}</td><td>DEMO9____NOISE_TEXTURES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT35</td><td>{1B0D1097-8F0E-4842-816B-8B6487AFE21F}</td><td>DEMO9.1_WATER_AND_OCEAN</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT36</td><td>{2A6C755E-4DC6-4DC8-997B-2E7ED92312B6}</td><td>DEMO9.2_SHADOW</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT37</td><td>{3225B879-1FBD-4C60-8C8A-B16A0CB4D48F}</td><td>DEMO9.3_SSAO</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT38</td><td>{43D5764B-3528-4D6F-8FFE-65863721424D}</td><td>DEMO9.4_OCTREE_CULLING</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT39</td><td>{BA961029-EDC1-46E3-A86A-E8DECC0460A0}</td><td>DEMO9.5_DECAL</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT4</td><td>{FAC3D8AC-59E6-4CD6-BED0-CCBD1C93F56C}</td><td>PROJECTTEMPLATES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT40</td><td>{FD4668D2-9864-43C8-ABAB-49CAB909C6A8}</td><td>DEMO9.6_EXPLOSION</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT41</td><td>{637E5DEA-D2B1-469C-A5EF-4ADDE1B5BBCF}</td><td>DEMO9.7_STEALTH</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT42</td><td>{54621037-97FC-456F-A8A7-3C71C88BA463}</td><td>DEMO9.8_PHYSX_2.8.X</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT43</td><td>{B518AB82-2933-42BF-A4E6-C4A04F50EA9B}</td><td>CHARACTER</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT44</td><td>{7DE6EFE8-E2B6-4063-8807-9877D1BF0B0F}</td><td>CLOTH</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT45</td><td>{012648D0-C09F-45DA-9290-5651AFC7FE16}</td><td>SCREENS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT46</td><td>{ABA8E83F-8F9C-4CCA-B5FA-8E7DE124A3D2}</td><td>FLUIDS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT47</td><td>{AB15CB1B-5EF3-447A-B21D-171E45D413AB}</td><td>MATERIAL</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT48</td><td>{DCC93D09-8830-484B-95A5-48BE62131F66}</td><td>TRIGGER</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT49</td><td>{F0678C6A-CFF9-4175-AFEE-89553A043FCF}</td><td>VEHICLE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT5</td><td>{586D962E-1559-4342-8B47-8D87C66A01B0}</td><td>ITEMTEMPLATES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT50</td><td>{0B5784D2-2CB3-4523-B49F-382E52D4C022}</td><td>PROPERTIES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT51</td><td>{E53A2B47-8856-488E-AD11-97762EA9D241}</td><td>INTRODUCTIONDEMO</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT52</td><td>{BE674A16-99E1-48E1-8D2E-6629CC6275AE}</td><td>INTRODUCTIONDEMO4.0</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT53</td><td>{080EA624-F0EE-4FE2-B541-AFC368198C4A}</td><td>DLL1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT54</td><td>{53CDE421-DB31-4AA1-AA13-3D0375990756}</td><td>INTRODUCTIONDEMO4.01</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT55</td><td>{10834398-730B-43E1-A99E-38D0965AF7D1}</td><td>COMMONS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT56</td><td>{53F3FE65-0522-4FD5-BB5D-179302BB31B4}</td><td>DEMO1___BASICSCREEN</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT57</td><td>{90CCF389-EF89-46EB-9E2B-1DBDF9BAE722}</td><td>DEMO2___INPUT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT58</td><td>{3E6C619D-BD71-4244-A536-4FEE3039E3FD}</td><td>DEMO3___LIGHTS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT59</td><td>{8CFC9AFE-7493-42AE-9F92-212C0F186D23}</td><td>HELPERS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT6</td><td>{B6D7AD00-0910-4AC4-82E1-5ADA196A9029}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT60</td><td>{0DED3AF8-BF1C-4E88-96C1-65E71FEAAACD}</td><td>DEMO4___PHYSICS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT61</td><td>{3A15446B-9783-49A6-81BF-596C1931C253}</td><td>HELPER</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT62</td><td>{97FDB67C-E16E-4DA9-94DC-CE2B1AF48C56}</td><td>DEMO5___CAMERAS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT63</td><td>{647143C2-0076-43DD-88B4-9F72D4772D26}</td><td>DEMO6___TRIGGER</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT64</td><td>{A720A0FC-4869-4F49-8515-9E8515C20284}</td><td>DEMO7___SCRIPTS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT65</td><td>{E6E8BCE9-7840-4EC2-8A22-BFF982557D6A}</td><td>DEMO8___SERIALIZATION</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT66</td><td>{4F825EAE-ABD6-469A-B830-1EE07119727E}</td><td>DEMO9___AI</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT67</td><td>{0EAF1D07-C67A-4793-950C-4540F65B889B}</td><td>BEHAVIOR_TREE</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT68</td><td>{D27AAC89-3B1A-49C0-A1FA-99B2D9056F1A}</td><td>FSM</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT69</td><td>{7B3E36E1-A9EB-4818-A480-FC17E090E0E4}</td><td>TEXTURES</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT7</td><td>{BFC73A97-AFCE-49BE-975D-FC64BA2EFB77}</td><td>CONTENT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT70</td><td>{FECAE687-E258-42CD-8E7B-258B04177E82}</td><td>RVO</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT71</td><td>{75F84CD2-25C3-44E1-8063-1F2932C44EB2}</td><td>RVO2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT72</td><td>{B390F22B-81FF-45A1-9917-B6683766B2C9}</td><td>RVO3D</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT73</td><td>{20257C11-8FE5-4CA0-B549-E08193A86E7D}</td><td>STEERING</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT74</td><td>{4716BA5B-3BCB-4167-9673-CB5A141BF69E}</td><td>WAYPOINTS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT75</td><td>{50F814E5-8B0E-4038-8EC3-A6BC5D99BB9E}</td><td>DEMOS_EXTRAS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT76</td><td>{C0CFC518-DAF2-4714-8AA1-F4401D454CA4}</td><td>PROPERTIES1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT77</td><td>{358BAE7E-77F8-4BBD-920F-E128FA1EB41C}</td><td>INTRODUCTIONDEMO4.0CONTENT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT78</td><td>{BCB1ACE8-E52C-443E-AB6B-F9DCFC7291E7}</td><td>FONT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT79</td><td>{DD1A156F-47A9-471B-B10A-AB60CB9735CA}</td><td>MODEL2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT8</td><td>{9D0FF3B7-D441-4BF3-AA32-16686A07283D}</td><td>EFFECTS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT80</td><td>{5AB282FE-7BC7-4E3F-88F9-5762AB70F752}</td><td>SCRIPT</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT81</td><td>{EBD8EE71-A95E-4711-A0EE-4EBBDC6C587F}</td><td>TEXTURES2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT82</td><td>{108EE65E-1DA0-443F-9481-CA792D0837AE}</td><td>PHONESILVERLIGHTDEMO</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT83</td><td>{1F309340-4BCF-497B-9304-270ABAF6E8F0}</td><td>GRAPHICSAPP1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT84</td><td>{A0CD43AD-8CDA-48F7-970E-2C1BDF910A2A}</td><td>PROPERTIES2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT85</td><td>{8708D297-0751-439E-8604-FB63E952364C}</td><td>GRAPHICSAPP1LIBRARY</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT86</td><td>{8C5B6779-A374-4D71-AE4F-862F24885730}</td><td>_D_DEMOS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT87</td><td>{75D6F112-0FCF-4167-93E5-CFADE925F58F}</td><td>HELPERS2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT88</td><td>{F4481343-7CE4-4DFB-8407-E761B634A689}</td><td>_D_DEMOS1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT89</td><td>{63574A8B-C6C6-4DCC-8363-4ABC2ABDC972}</td><td>BEHAVIORS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT9</td><td>{2F01333F-CFF9-45D4-BDA8-97A2A9A730DA}</td><td>MODEL</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT90</td><td>{C569E635-8890-4B60-B6AA-45C2FFEAD480}</td><td>CROWED</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT91</td><td>{72E78F7F-A1C4-4705-A6C3-6431583C3237}</td><td>GRAVITATIONALDEMO</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT92</td><td>{F62BF54A-DE6B-4A48-AFE9-A3FBDAC96842}</td><td>HELPER2</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT93</td><td>{9C9DB9B7-B4F8-4BDD-831A-17EBA3136F12}</td><td>POSTEFFECT1</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT94</td><td>{569C29C5-34ED-45D3-9045-5029483E0E09}</td><td>HELP</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT95</td><td>{12C5FFC8-247A-46B6-9C14-E79FAE839454}</td><td>WINDOWS_HIDEF</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT96</td><td>{3F858975-F89A-4062-A557-B46A6796E82E}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT97</td><td>{AED32821-22B7-46DF-A231-FC7D9D01A67D}</td><td>WINDOWS_REACH</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT98</td><td>{8AD442A7-8FDB-49C9-A1BC-FBB44A832323}</td><td>_DS_MAX_SCRIPTS</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ISX_DEFAULTCOMPONENT99</td><td>{73A8E622-2899-467D-BEE1-2926A43BFF08}</td><td>_DS_MAX_.X_EXPORTER</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>ModelImporter.dll</td><td>{5B02A614-3C21-4132-8FEF-5F8C10F61214}</td><td>DLLS</td><td>2</td><td/><td>modelimporter.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>NewComponent1</td><td>{73EAFBB1-EFE1-4B65-8940-E21177B7AC0E}</td><td>INSTALLDIR</td><td>2</td><td/><td/><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>NxCharacter.dll</td><td>{F0E362E2-3E4B-43D9-B7C9-7B8B92A36366}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>nxcharacter.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>NxCharacter.dll1</td><td>{B71757B9-D426-4DCB-892C-AD16AD4F0CA4}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>nxcharacter.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Osiris.Terrain.Content.Pipeline.dll</td><td>{800AC98C-8DEE-427E-B732-CBA798674B18}</td><td>PHONE7</td><td>2</td><td/><td>osiris.terrain.content.pipel</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXCooking.dll</td><td>{3E6CED94-E301-43E7-ACCF-FD48D75F1999}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>physxcooking.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXCooking.dll1</td><td>{11CEB600-8495-485F-BE53-DD0C27301056}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>physxcooking.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXCore.dll</td><td>{E4814B67-A2AB-42E0-8B49-3BBF9880316B}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>physxcore.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXCore.dll1</td><td>{F58EF920-F561-4863-B8DD-5A21EBBD00D3}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>physxcore.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXDevice.dll</td><td>{913A2E1A-F91C-4803-82BE-D60E452C7165}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>physxdevice.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXDevice.dll1</td><td>{F1D3EA39-FECA-43A5-AB27-D22E7724BBAF}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>physxdevice.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXLoader.dll</td><td>{90A79BC6-2080-4E84-A04E-134CA3CA3581}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>physxloader.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PhysXLoader.dll1</td><td>{3762FF2E-AD5A-40DC-AEB2-68D0110C96EA}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>physxloader.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEngineDebug.dll</td><td>{4EBA50A7-A906-4AEE-986F-6B23DB8C1AC0}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>ploobsenginedebug.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEngineDebug.dll1</td><td>{3DFA3423-B3BC-4D3A-AE66-0E53D7C5458B}</td><td>DLL</td><td>2</td><td/><td>ploobsenginedebug.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEngineDebug.dll2</td><td>{9515F7A3-3579-42DE-9949-4B4A3B12F1D9}</td><td>DLL1</td><td>2</td><td/><td>ploobsenginedebug.dll2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEngineDebug.dll3</td><td>{AFBBD6D8-05FF-43B8-BF45-2BA85DE823D4}</td><td>DLLS</td><td>2</td><td/><td>ploobsenginedebug.dll3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEngineDebug.dll4</td><td>{3A22872B-1EEA-48BA-9BD2-DE0158C86DAD}</td><td>WINDOWS_HIDEF</td><td>2</td><td/><td>ploobsenginedebug.dll4</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEngineDebug.dll5</td><td>{AC02868B-F9DB-4B87-BDC7-2DA4A4836129}</td><td>WINDOWS_REACH</td><td>2</td><td/><td>ploobsenginedebug.dll5</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePhone7.dll</td><td>{BB0A343F-41A3-4A5F-BC62-B181B690810C}</td><td>PHONE7</td><td>2</td><td/><td>ploobsenginephone7.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePhone7.dll1</td><td>{6201A6FF-DAA2-4695-A8C5-EFA8DC6BE937}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td>ploobsenginephone7.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePipelineDebug.dll</td><td>{60D41F33-3D25-4ABB-A2F6-5E9CFBE30F8E}</td><td>DLL</td><td>2</td><td/><td>ploobsenginepipelinedebug.dl</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePipelineDebug.dll1</td><td>{07339AF6-D877-476B-9308-1B394F7515E2}</td><td>DLL1</td><td>2</td><td/><td>ploobsenginepipelinedebug.dl1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePipelineDebug.dll2</td><td>{C7647F98-AB6F-4E83-B88C-2B3CD4CB3D71}</td><td>WINDOWS_HIDEF</td><td>2</td><td/><td>ploobsenginepipelinedebug.dl2</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePipelineDebug.dll3</td><td>{E9224F74-86CD-4C0A-BB80-944D70180926}</td><td>WINDOWS_REACH</td><td>2</td><td/><td>ploobsenginepipelinedebug.dl3</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsEnginePipelineDebug.dll4</td><td>{4870EF87-91CA-4CB6-9E28-0DFAEA4918C1}</td><td>DLLS</td><td>2</td><td/><td>ploobsenginepipelinedebug.dl4</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsScripts.dll</td><td>{0CFE17FF-8D89-4D2E-B63E-70EE9A4C6ABE}</td><td>DLL1</td><td>2</td><td/><td>ploobsscripts.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>PloobsSerializator.dll</td><td>{237DF9FF-6745-4095-828F-F00D79783C8C}</td><td>DLL1</td><td>2</td><td/><td>ploobsserializator.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>Ploobs_Engine_Help.chm</td><td>{F7EDF42F-2425-4787-8C5A-F3B618E45C3A}</td><td>HELP</td><td>2</td><td/><td>ploobs_engine_help.chm</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>RVO2DWrapper.dll</td><td>{FBC5D236-19A4-4CA3-8020-CE51F4B0DA50}</td><td>INTRODUCTIONDEMO4.01</td><td>2</td><td/><td>rvo2dwrapper.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>RVO3D.dll</td><td>{203D4C82-AE35-46E0-9EB5-E81FA9C11AC7}</td><td>INTRODUCTIONDEMO4.01</td><td>2</td><td/><td>rvo3d.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SharedOsiris.dll</td><td>{0713C848-CD23-4787-8A72-B3A804E996EE}</td><td>PHONE7</td><td>2</td><td/><td>sharedosiris.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SkinnedModel.dll</td><td>{8AE43D2D-7B0C-4416-AD39-5EB864175631}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>skinnedmodel.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SkinnedModel.dll1</td><td>{9812A0C6-E52E-44B3-B8E2-76994395DD65}</td><td>SKINNED</td><td>2</td><td/><td>skinnedmodel.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>SkinnedModelPipeline.dll</td><td>{5687960E-7D2B-45E5-A13F-41652BE50DF9}</td><td>SKINNED</td><td>2</td><td/><td>skinnedmodelpipeline.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>StillDesign.PhysX.Net.dll</td><td>{28B47753-62A1-43DC-B8A6-EA9DF9EFB2F2}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>stilldesign.physx.net.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>StillDesign.PhysX.Net.dll1</td><td>{A6D494C1-2705-4F28-B2CF-EF83E363F3B8}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>stilldesign.physx.net.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>XNAnimation.dll</td><td>{2C62BDC7-6A01-48A9-92FC-A535B886B65A}</td><td>PHONE7</td><td>2</td><td/><td>xnanimation.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>XNAnimation.dll1</td><td>{AA0BCB91-7296-4959-8759-17257320F5DE}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td>xnanimation.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>XNAnimationPipeline.dll</td><td>{2F583AC9-FBEF-40DD-9B0C-BF0A009B7E32}</td><td>PHONE7</td><td>2</td><td/><td>xnanimationpipeline.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>XNAnimationPipeline.dll1</td><td>{417028D7-9D3C-4D07-9921-457FF2F0E7F4}</td><td>WINDOWS_PHONE_7</td><td>2</td><td/><td>xnanimationpipeline.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>cudart32_30_9.dll</td><td>{CBF2F7AA-E830-420A-A108-5BE00CDD7643}</td><td>ADVDEMOSBIN</td><td>2</td><td/><td>cudart32_30_9.dll</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
		<row><td>cudart32_30_9.dll1</td><td>{85528326-713A-4EA9-BE2E-9A99844CCB27}</td><td>ADVANCEDDEMOS2</td><td>2</td><td/><td>cudart32_30_9.dll1</td><td>17</td><td/><td/><td/><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td><td>/LogFile=</td></row>
	</table>

	<table name="Condition">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="i2">Level</col>
		<col def="S255">Condition</col>
	</table>

	<table name="Control">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control</col>
		<col def="s20">Type</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="S72">Property</col>
		<col def="L0">Text</col>
		<col def="S50">Control_Next</col>
		<col def="L50">Help</col>
		<col def="I4">ISWindowStyle</col>
		<col def="I4">ISControlId</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="S72">Binary_</col>
		<row><td>AdminChangeFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>AdminChangeFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ComboText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Combo</td><td>DirectoryCombo</td><td>21</td><td>64</td><td>277</td><td>80</td><td>458755</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_4##</td><td>Up</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>ComboText</td><td>Text</td><td>21</td><td>50</td><td>99</td><td>14</td><td>3</td><td/><td>##IDS__IsAdminInstallBrowse_LookIn##</td><td>Combo</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallBrowse_BrowseDestination##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallBrowse_ChangeDestination##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>List</td><td>DirectoryList</td><td>21</td><td>90</td><td>332</td><td>97</td><td>7</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_8##</td><td>TailText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>NewFolder</td><td>PushButton</td><td>335</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>List</td><td>##IDS__IsAdminInstallBrowse_CreateFolder##</td><td>0</td><td/><td/><td>NewBinary2</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Tail</td><td>PathEdit</td><td>21</td><td>207</td><td>332</td><td>17</td><td>3</td><td>TARGETDIR</td><td>##IDS__IsAdminInstallBrowse_11##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>TailText</td><td>Text</td><td>21</td><td>193</td><td>99</td><td>13</td><td>3</td><td/><td>##IDS__IsAdminInstallBrowse_FolderName##</td><td>Tail</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminChangeFolder</td><td>Up</td><td>PushButton</td><td>310</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>NewFolder</td><td>##IDS__IsAdminInstallBrowse_UpOneLevel##</td><td>0</td><td/><td/><td>NewBinary3</td></row>
		<row><td>AdminNetworkLocation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>InstallNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>AdminNetworkLocation</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Browse</td><td>PushButton</td><td>286</td><td>124</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_Change##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>SetupPathEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallPoint_SpecifyNetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>40</td><td>131075</td><td/><td>##IDS__IsAdminInstallPoint_EnterNetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsAdminInstallPoint_NetworkLocationFormatted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_Install##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>LBBrowse</td><td>Text</td><td>21</td><td>90</td><td>100</td><td>10</td><td>3</td><td/><td>##IDS__IsAdminInstallPoint_NetworkLocation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminNetworkLocation</td><td>SetupPathEdit</td><td>PathEdit</td><td>21</td><td>102</td><td>330</td><td>17</td><td>3</td><td>TARGETDIR</td><td/><td>Browse</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>AdminWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsAdminInstallPointWelcome_Wizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>AdminWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsAdminInstallPointWelcome_ServerImage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Icon</td><td>Icon</td><td>15</td><td>15</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary4</td></row>
		<row><td>CancelSetup</td><td>No</td><td>PushButton</td><td>135</td><td>57</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCancelDlg_No##</td><td>Yes</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Text</td><td>Text</td><td>48</td><td>15</td><td>194</td><td>30</td><td>131075</td><td/><td>##IDS__IsCancelDlg_ConfirmCancel##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CancelSetup</td><td>Yes</td><td>PushButton</td><td>62</td><td>57</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCancelDlg_Yes##</td><td>No</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>CustomSetup</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Tree</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>203</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Change##</td><td>Help</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Details</td><td>PushButton</td><td>93</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Space##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgDesc</td><td>Text</td><td>17</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsCustomSelectionDlg_SelectFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgText</td><td>Text</td><td>9</td><td>51</td><td>360</td><td>10</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_ClickFeatureIcon##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>DlgTitle</td><td>Text</td><td>9</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsCustomSelectionDlg_CustomSetup##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>FeatureGroup</td><td>GroupBox</td><td>235</td><td>67</td><td>131</td><td>120</td><td>1</td><td/><td>##IDS__IsCustomSelectionDlg_FeatureDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Help</td><td>PushButton</td><td>22</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_Help##</td><td>Details</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>InstallLabel</td><td>Text</td><td>8</td><td>190</td><td>360</td><td>10</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_InstallTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>ItemDescription</td><td>Text</td><td>241</td><td>80</td><td>120</td><td>50</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_MultilineDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Location</td><td>Text</td><td>8</td><td>203</td><td>291</td><td>20</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_FeaturePath##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Size</td><td>Text</td><td>241</td><td>133</td><td>120</td><td>50</td><td>3</td><td/><td>##IDS__IsCustomSelectionDlg_FeatureSize##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetup</td><td>Tree</td><td>SelectionTree</td><td>8</td><td>70</td><td>220</td><td>118</td><td>7</td><td>_BrowseProperty</td><td/><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>CustomSetupTips</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS_SetupTips_CustomSetupDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS_SetupTips_CustomSetup##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>DontInstall</td><td>Icon</td><td>21</td><td>155</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary14</td></row>
		<row><td>CustomSetupTips</td><td>DontInstallText</td><td>Text</td><td>60</td><td>155</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_WillNotBeInstalled##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>FirstInstallText</td><td>Text</td><td>60</td><td>180</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_Advertise##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>Install</td><td>Icon</td><td>21</td><td>105</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary15</td></row>
		<row><td>CustomSetupTips</td><td>InstallFirstUse</td><td>Icon</td><td>21</td><td>180</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary16</td></row>
		<row><td>CustomSetupTips</td><td>InstallPartial</td><td>Icon</td><td>21</td><td>130</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary17</td></row>
		<row><td>CustomSetupTips</td><td>InstallStateMenu</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary18</td></row>
		<row><td>CustomSetupTips</td><td>InstallStateText</td><td>Text</td><td>21</td><td>91</td><td>300</td><td>10</td><td>3</td><td/><td>##IDS_SetupTips_InstallState##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>CustomSetupTips</td><td>InstallText</td><td>Text</td><td>60</td><td>105</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_AllInstalledLocal##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>MenuText</td><td>Text</td><td>50</td><td>52</td><td>300</td><td>36</td><td>3</td><td/><td>##IDS_SetupTips_IconInstallState##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>NetworkInstall</td><td>Icon</td><td>21</td><td>205</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary13</td></row>
		<row><td>CustomSetupTips</td><td>NetworkInstallText</td><td>Text</td><td>60</td><td>205</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_Network##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>OK</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_SetupTips_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomSetupTips</td><td>PartialText</td><td>Text</td><td>60</td><td>130</td><td>300</td><td>20</td><td>3</td><td/><td>##IDS_SetupTips_SubFeaturesInstalledLocal##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>CustomerInformation</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>NameLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>CompanyEdit</td><td>Edit</td><td>21</td><td>100</td><td>237</td><td>17</td><td>3</td><td>COMPANYNAME</td><td>##IDS__IsRegisterUserDlg_Tahoma80##</td><td>SerialLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>CompanyLabel</td><td>Text</td><td>21</td><td>89</td><td>75</td><td>10</td><td>3</td><td/><td>##IDS__IsRegisterUserDlg_Organization##</td><td>CompanyEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsRegisterUserDlg_PleaseEnterInfo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Text</td><td>21</td><td>161</td><td>300</td><td>14</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_InstallFor##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsRegisterUserDlg_CustomerInformation##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>NameEdit</td><td>Edit</td><td>21</td><td>63</td><td>237</td><td>17</td><td>3</td><td>USERNAME</td><td>##IDS__IsRegisterUserDlg_Tahoma50##</td><td>CompanyLabel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>NameLabel</td><td>Text</td><td>21</td><td>52</td><td>75</td><td>10</td><td>3</td><td/><td>##IDS__IsRegisterUserDlg_UserName##</td><td>NameEdit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>63</td><td>170</td><td>300</td><td>50</td><td>2</td><td>ApplicationUsers</td><td>##IDS__IsRegisterUserDlg_16##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>SerialLabel</td><td>Text</td><td>21</td><td>127</td><td>109</td><td>10</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_SerialNumber##</td><td>SerialNumber</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>CustomerInformation</td><td>SerialNumber</td><td>MaskedEdit</td><td>21</td><td>138</td><td>237</td><td>17</td><td>2</td><td>ISX_SERIALNUM</td><td/><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>DatabaseFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>65</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CHANGE##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DatabaseFolder</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary12</td></row>
		<row><td>DatabaseFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DatabaseFolder_ChangeFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DatabaseFolder_DatabaseFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>LocLabel</td><td>Text</td><td>57</td><td>52</td><td>290</td><td>10</td><td>131075</td><td/><td>##IDS_DatabaseFolder_InstallDatabaseTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Location</td><td>Text</td><td>57</td><td>65</td><td>240</td><td>40</td><td>3</td><td>_BrowseProperty</td><td>##IDS__DatabaseFolder_DatabaseDir##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DatabaseFolder</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>DestinationFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ChangeFolder</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>PushButton</td><td>301</td><td>65</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__DestinationFolder_Change##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DestFolder</td><td>Icon</td><td>21</td><td>52</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary12</td></row>
		<row><td>DestinationFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DestinationFolder_ChangeFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__DestinationFolder_DestinationFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>LocLabel</td><td>Text</td><td>57</td><td>52</td><td>290</td><td>10</td><td>131075</td><td/><td>##IDS__DestinationFolder_InstallTo##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Location</td><td>Text</td><td>57</td><td>65</td><td>240</td><td>40</td><td>3</td><td>_BrowseProperty</td><td>##IDS_INSTALLDIR##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DestinationFolder</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>DiskSpaceRequirements</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgDesc</td><td>Text</td><td>17</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFeatureDetailsDlg_SpaceRequired##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgText</td><td>Text</td><td>10</td><td>185</td><td>358</td><td>41</td><td>3</td><td/><td>##IDS__IsFeatureDetailsDlg_VolumesTooSmall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>DlgTitle</td><td>Text</td><td>9</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFeatureDetailsDlg_DiskSpaceRequirements##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>List</td><td>VolumeCostList</td><td>8</td><td>55</td><td>358</td><td>125</td><td>393223</td><td/><td>##IDS__IsFeatureDetailsDlg_Numbers##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>DiskSpaceRequirements</td><td>OK</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFeatureDetailsDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>FilesInUse</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUseMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>348</td><td>33</td><td>3</td><td/><td>##IDS__IsFilesInUse_ApplicationsUsingFiles##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUse##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Exit</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Exit##</td><td>List</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Ignore</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Ignore##</td><td>Exit</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>List</td><td>ListBox</td><td>21</td><td>87</td><td>331</td><td>135</td><td>7</td><td>FileInUseProcess</td><td/><td>Retry</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>FilesInUse</td><td>Retry</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFilesInUse_Retry##</td><td>Ignore</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>InstallChangeFolder</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ComboText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Combo</td><td>DirectoryCombo</td><td>21</td><td>64</td><td>277</td><td>80</td><td>4128779</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_4##</td><td>Up</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>ComboText</td><td>Text</td><td>21</td><td>50</td><td>99</td><td>14</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_LookIn##</td><td>Combo</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsBrowseFolderDlg_BrowseDestFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsBrowseFolderDlg_ChangeCurrentFolder##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>List</td><td>DirectoryList</td><td>21</td><td>90</td><td>332</td><td>97</td><td>15</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_8##</td><td>TailText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>NewFolder</td><td>PushButton</td><td>335</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>List</td><td>##IDS__IsBrowseFolderDlg_CreateFolder##</td><td>0</td><td/><td/><td>NewBinary2</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Tail</td><td>PathEdit</td><td>21</td><td>207</td><td>332</td><td>17</td><td>15</td><td>_BrowseProperty</td><td>##IDS__IsBrowseFolderDlg_11##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>TailText</td><td>Text</td><td>21</td><td>193</td><td>99</td><td>13</td><td>3</td><td/><td>##IDS__IsBrowseFolderDlg_FolderName##</td><td>Tail</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallChangeFolder</td><td>Up</td><td>PushButton</td><td>310</td><td>66</td><td>19</td><td>19</td><td>3670019</td><td/><td/><td>NewFolder</td><td>##IDS__IsBrowseFolderDlg_UpOneLevel##</td><td>0</td><td/><td/><td>NewBinary3</td></row>
		<row><td>InstallWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Copyright</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Text</td><td>135</td><td>144</td><td>228</td><td>73</td><td>65539</td><td/><td>##IDS__IsWelcomeDlg_WarningCopyright##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>InstallWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsWelcomeDlg_WelcomeProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>InstallWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsWelcomeDlg_InstallProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Agree</td><td>RadioButtonGroup</td><td>8</td><td>190</td><td>291</td><td>40</td><td>3</td><td>AgreeToLicense</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>LicenseAgreement</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>ISPrintButton</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsLicenseDlg_ReadLicenseAgreement##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsLicenseDlg_LicenseAgreement##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>ISPrintButton</td><td>PushButton</td><td>301</td><td>188</td><td>65</td><td>17</td><td>3</td><td/><td>##IDS_PRINT_BUTTON##</td><td>Agree</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>LicenseAgreement</td><td>Memo</td><td>ScrollableText</td><td>8</td><td>55</td><td>358</td><td>130</td><td>7</td><td/><td/><td/><td/><td>0</td><td/><td>C:\PloobsPackages\Extra\License.rtf</td><td/></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>MaintenanceType</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsMaintenanceDlg_MaitenanceOptions##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsMaintenanceDlg_ProgramMaintenance##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Ico1</td><td>Icon</td><td>35</td><td>75</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary6</td></row>
		<row><td>MaintenanceType</td><td>Ico2</td><td>Icon</td><td>35</td><td>135</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary7</td></row>
		<row><td>MaintenanceType</td><td>Ico3</td><td>Icon</td><td>35</td><td>195</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary8</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>21</td><td>55</td><td>290</td><td>170</td><td>3</td><td>_IsMaintenance</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text1</td><td>Text</td><td>80</td><td>72</td><td>260</td><td>35</td><td>3</td><td/><td>##IDS__IsMaintenanceDlg_ChangeFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text2</td><td>Text</td><td>80</td><td>135</td><td>260</td><td>35</td><td>3</td><td/><td>##IDS__IsMaintenanceDlg_RepairMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceType</td><td>Text3</td><td>Text</td><td>80</td><td>192</td><td>260</td><td>35</td><td>131075</td><td/><td>##IDS__IsMaintenanceDlg_RemoveProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>MaintenanceWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsMaintenanceWelcome_WizardWelcome##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MaintenanceWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>50</td><td>196611</td><td/><td>##IDS__IsMaintenanceWelcome_MaintenanceOptionsDescription##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>MsiRMFilesInUse</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Restart</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUseMessage##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>348</td><td>14</td><td>3</td><td/><td>##IDS__IsMsiRMFilesInUse_ApplicationsUsingFiles##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsFilesInUse_FilesInUse##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>List</td><td>ListBox</td><td>21</td><td>66</td><td>331</td><td>130</td><td>3</td><td>FileInUseProcess</td><td/><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_OK##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>MsiRMFilesInUse</td><td>Restart</td><td>RadioButtonGroup</td><td>19</td><td>187</td><td>343</td><td>40</td><td>3</td><td>RestartManagerOption</td><td/><td>List</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>OutOfSpace</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsDiskSpaceDlg_DiskSpace##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>43</td><td>3</td><td/><td>##IDS__IsDiskSpaceDlg_HighlightedVolumes##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsDiskSpaceDlg_OutOfDiskSpace##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>List</td><td>VolumeCostList</td><td>21</td><td>95</td><td>332</td><td>120</td><td>393223</td><td/><td>##IDS__IsDiskSpaceDlg_Numbers##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsDiskSpaceDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsPatchDlg_Update##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsPatchDlg_WelcomePatchWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>PatchWelcome</td><td>TextLine2</td><td>Text</td><td>135</td><td>54</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsPatchDlg_PatchClickUpdate##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>3</td><td/><td/><td>DlgTitle</td><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>ReadmeInformation</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS__IsReadmeDlg_Cancel##</td><td>Readme</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>232</td><td>16</td><td>65539</td><td/><td>##IDS__IsReadmeDlg_PleaseReadInfo##</td><td>Back</td><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>3</td><td/><td/><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadmeInformation</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>193</td><td>13</td><td>65539</td><td/><td>##IDS__IsReadmeDlg_ReadMeInfo##</td><td>DlgDesc</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1048579</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadmeInformation</td><td>Readme</td><td>ScrollableText</td><td>10</td><td>55</td><td>353</td><td>166</td><td>3</td><td/><td/><td>Banner</td><td/><td>0</td><td/><td>&lt;ISProductFolder&gt;\Redist\0409\Readme.rtf</td><td/></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>GroupBox1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary20</td></row>
		<row><td>ReadyToInstall</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>CompanyNameText</td><td>Text</td><td>38</td><td>198</td><td>211</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Company##</td><td>SerialNumberText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>CurrentSettingsText</td><td>Text</td><td>19</td><td>80</td><td>81</td><td>10</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_CurrentSettings##</td><td>InstallNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyReadyDlg_WizardReady##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgText1</td><td>Text</td><td>21</td><td>54</td><td>330</td><td>24</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_BackOrCancel##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Text</td><td>21</td><td>99</td><td>330</td><td>20</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_InstallFor##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ModifyReady##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle2</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ReadyRepair##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>DlgTitle3</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsVerifyReadyDlg_ReadyInstall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>GroupBox1</td><td>Text</td><td>19</td><td>92</td><td>330</td><td>133</td><td>65541</td><td/><td/><td>SetupTypeText1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>8388611</td><td/><td>##IDS__IsVerifyReadyDlg_Install##</td><td>InstallPerMachine</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>PushButton</td><td>63</td><td>123</td><td>248</td><td>17</td><td>8388610</td><td/><td>##IDS__IsRegisterUserDlg_Anyone##</td><td>InstallPerUser</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>PushButton</td><td>63</td><td>143</td><td>248</td><td>17</td><td>2</td><td/><td>##IDS__IsRegisterUserDlg_OnlyMe##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Text</td><td>38</td><td>211</td><td>306</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Serial##</td><td>CurrentSettingsText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText1</td><td>Text</td><td>23</td><td>97</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_SetupType##</td><td>SetupTypeText2</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText2</td><td>Text</td><td>37</td><td>114</td><td>306</td><td>14</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_SelectedSetupType##</td><td>TargetFolderText1</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText1</td><td>Text</td><td>24</td><td>136</td><td>306</td><td>11</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_DestFolder##</td><td>TargetFolderText2</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText2</td><td>Text</td><td>37</td><td>151</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_Installdir##</td><td>UserInformationText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>UserInformationText</td><td>Text</td><td>23</td><td>171</td><td>306</td><td>13</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_UserInfo##</td><td>UserNameText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToInstall</td><td>UserNameText</td><td>Text</td><td>38</td><td>184</td><td>306</td><td>9</td><td>3</td><td/><td>##IDS__IsVerifyReadyDlg_UserName##</td><td>CompanyNameText</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>RemoveNow</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary19</td></row>
		<row><td>ReadyToRemove</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ChoseRemoveProgram##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText</td><td>Text</td><td>21</td><td>51</td><td>326</td><td>24</td><td>131075</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ClickRemove##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText1</td><td>Text</td><td>21</td><td>79</td><td>330</td><td>23</td><td>3</td><td/><td>##IDS__IsVerifyRemoveAllDlg_ClickBack##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgText2</td><td>Text</td><td>21</td><td>102</td><td>330</td><td>24</td><td>3</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsVerifyRemoveAllDlg_RemoveProgram##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>8388611</td><td/><td>##IDS__IsVerifyRemoveAllDlg_Remove##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Finish</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>172</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsFatalError_Finish##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsFatalError_NotModified##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsFatalError_ClickFinish##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsFatalError_KeepOrRestore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsFatalError_RestoreOrContinueLater##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>172</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsFatalError_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteError</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>25</td><td>196611</td><td/><td>##IDS__IsFatalError_WizardInterrupted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>OK</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckBoxUpdates</td><td>CheckBox</td><td>135</td><td>164</td><td>10</td><td>9</td><td>2</td><td>ISCHECKFORPRODUCTUPDATES</td><td>CheckBox1</td><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckForUpdatesText</td><td>Text</td><td>152</td><td>162</td><td>190</td><td>30</td><td>65538</td><td/><td>##IDS__IsExitDialog_Update_YesCheckForUpdates##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchProgram</td><td>CheckBox</td><td>151</td><td>114</td><td>10</td><td>9</td><td>2</td><td>LAUNCHPROGRAM</td><td/><td>CheckLaunchReadme</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchReadme</td><td>CheckBox</td><td>151</td><td>148</td><td>10</td><td>9</td><td>2</td><td>LAUNCHREADME</td><td/><td>CheckBoxUpdates</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>182</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckLaunchProgram</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchProgramText</td><td>Text</td><td>164</td><td>112</td><td>98</td><td>15</td><td>65538</td><td/><td>##IDS__IsExitDialog_LaunchProgram##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchReadmeText</td><td>Text</td><td>164</td><td>148</td><td>120</td><td>13</td><td>65538</td><td/><td>##IDS__IsExitDialog_ShowReadMe##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsExitDialog_Finish##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>182</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsExitDialog_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_InstallSuccess##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine3</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_UninstallSuccess##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine1</td><td>Text</td><td>135</td><td>30</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_Update_SetupFinished##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine2</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>45</td><td>196610</td><td/><td>##IDS__IsExitDialog_Update_PossibleUpdates##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine3</td><td>Text</td><td>135</td><td>120</td><td>228</td><td>45</td><td>65538</td><td/><td>##IDS__IsExitDialog_Update_InternetConnection##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>A</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Abort##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>C</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>ErrorIcon</td><td>Icon</td><td>15</td><td>15</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary4</td></row>
		<row><td>SetupError</td><td>ErrorText</td><td>Text</td><td>50</td><td>15</td><td>200</td><td>50</td><td>131075</td><td/><td>##IDS__IsErrorDlg_ErrorText##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>I</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Ignore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>N</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_NO##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>O</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_OK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>R</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Retry##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupError</td><td>Y</td><td>PushButton</td><td>192</td><td>80</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsErrorDlg_Yes##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>ActionData</td><td>Text</td><td>135</td><td>125</td><td>228</td><td>12</td><td>65539</td><td/><td>##IDS__IsInitDlg_1##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>ActionText</td><td>Text</td><td>135</td><td>109</td><td>220</td><td>36</td><td>65539</td><td/><td>##IDS__IsInitDlg_2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupInitialization</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_NEXT##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsInitDlg_WelcomeWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInitialization</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>30</td><td>196611</td><td/><td>##IDS__IsInitDlg_PreparingWizard##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Finish</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_CANCEL##</td><td>Image</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>CheckShowMsiLog</td><td>CheckBox</td><td>151</td><td>172</td><td>10</td><td>9</td><td>2</td><td>ISSHOWMSILOG</td><td/><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS__IsUserExit_Finish##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsUserExit_NotModified##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsUserExit_ClickFinish##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td>CheckShowMsiLog</td><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Text</td><td>135</td><td>80</td><td>228</td><td>50</td><td>65539</td><td/><td>##IDS__IsUserExit_KeepOrRestore##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Text</td><td>135</td><td>135</td><td>228</td><td>25</td><td>65539</td><td/><td>##IDS__IsUserExit_RestoreOrContinue##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>ShowMsiLogText</td><td>Text</td><td>164</td><td>172</td><td>198</td><td>10</td><td>65538</td><td/><td>##IDS__IsSetupComplete_ShowMsiLog##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>65539</td><td/><td>##IDS__IsUserExit_WizardCompleted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupInterrupted</td><td>TextLine2</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>25</td><td>196611</td><td/><td>##IDS__IsUserExit_WizardInterrupted##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>ProgressBar</td><td>59</td><td>113</td><td>275</td><td>12</td><td>65537</td><td/><td>##IDS__IsProgressDlg_ProgressDone##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>ActionText</td><td>Text</td><td>59</td><td>100</td><td>275</td><td>12</td><td>3</td><td/><td>##IDS__IsProgressDlg_2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary21</td></row>
		<row><td>SetupProgress</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsProgressDlg_UninstallingFeatures2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgDesc2</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65538</td><td/><td>##IDS__IsProgressDlg_UninstallingFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgText</td><td>Text</td><td>59</td><td>51</td><td>275</td><td>30</td><td>196610</td><td/><td>##IDS__IsProgressDlg_WaitUninstall2##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgText2</td><td>Text</td><td>59</td><td>51</td><td>275</td><td>30</td><td>196610</td><td/><td>##IDS__IsProgressDlg_WaitUninstall##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>196610</td><td/><td>##IDS__IsProgressDlg_InstallingProductName##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>DlgTitle2</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>196610</td><td/><td>##IDS__IsProgressDlg_Uninstalling##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>LbSec</td><td>Text</td><td>192</td><td>139</td><td>32</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_SecHidden##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>LbStatus</td><td>Text</td><td>59</td><td>85</td><td>70</td><td>12</td><td>3</td><td/><td>##IDS__IsProgressDlg_Status##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>SetupIcon</td><td>Icon</td><td>21</td><td>51</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary9</td></row>
		<row><td>SetupProgress</td><td>ShowTime</td><td>Text</td><td>170</td><td>139</td><td>17</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_Hidden##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupProgress</td><td>TextTime</td><td>Text</td><td>59</td><td>139</td><td>110</td><td>12</td><td>2</td><td/><td>##IDS__IsProgressDlg_HiddenTimeRemaining##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>DlgLine</td><td>Line</td><td>0</td><td>234</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>Image</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>234</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SetupResume</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Text</td><td>135</td><td>55</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_WizardResume##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Text</td><td>135</td><td>46</td><td>228</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_ResumeSuspended##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupResume</td><td>TextLine1</td><td>Text</td><td>135</td><td>8</td><td>225</td><td>45</td><td>196611</td><td/><td>##IDS__IsResumeDlg_Resuming##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Banner</td><td>Bitmap</td><td>0</td><td>0</td><td>374</td><td>44</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary1</td></row>
		<row><td>SetupType</td><td>BannerLine</td><td>Line</td><td>0</td><td>44</td><td>374</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>RadioGroup</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CompText</td><td>Text</td><td>80</td><td>80</td><td>246</td><td>30</td><td>3</td><td/><td>##IDS__IsSetupTypeMinDlg_AllFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CompleteIco</td><td>Icon</td><td>34</td><td>80</td><td>24</td><td>24</td><td>5242881</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary10</td></row>
		<row><td>SetupType</td><td>CustText</td><td>Text</td><td>80</td><td>171</td><td>246</td><td>30</td><td>2</td><td/><td>##IDS__IsSetupTypeMinDlg_ChooseFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>CustomIco</td><td>Icon</td><td>34</td><td>171</td><td>24</td><td>24</td><td>5242880</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary11</td></row>
		<row><td>SetupType</td><td>DlgDesc</td><td>Text</td><td>21</td><td>23</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsSetupTypeMinDlg_ChooseSetupType##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>DlgText</td><td>Text</td><td>22</td><td>49</td><td>326</td><td>10</td><td>3</td><td/><td>##IDS__IsSetupTypeMinDlg_SelectSetupType##</td><td/><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SetupType</td><td>DlgTitle</td><td>Text</td><td>13</td><td>6</td><td>292</td><td>25</td><td>65539</td><td/><td>##IDS__IsSetupTypeMinDlg_SetupType##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>MinIco</td><td>Icon</td><td>34</td><td>125</td><td>24</td><td>24</td><td>5242880</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary11</td></row>
		<row><td>SetupType</td><td>MinText</td><td>Text</td><td>80</td><td>125</td><td>246</td><td>30</td><td>2</td><td/><td>##IDS__IsSetupTypeMinDlg_MinimumFeatures##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SetupType</td><td>RadioGroup</td><td>RadioButtonGroup</td><td>20</td><td>59</td><td>264</td><td>139</td><td>1048579</td><td>_IsSetupTypeMin</td><td/><td>Back</td><td/><td>0</td><td>0</td><td/><td/></row>
		<row><td>SplashBitmap</td><td>Back</td><td>PushButton</td><td>164</td><td>243</td><td>66</td><td>17</td><td>1</td><td/><td>##IDS_BACK##</td><td>Next</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Branding1</td><td>Text</td><td>4</td><td>229</td><td>50</td><td>13</td><td>3</td><td/><td>##IDS_INSTALLSHIELD_FORMATTED##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Branding2</td><td>Text</td><td>3</td><td>228</td><td>50</td><td>13</td><td>65537</td><td/><td>##IDS_INSTALLSHIELD##</td><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Cancel</td><td>PushButton</td><td>301</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_CANCEL##</td><td>Back</td><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>DlgLine</td><td>Line</td><td>48</td><td>234</td><td>326</td><td>0</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td/></row>
		<row><td>SplashBitmap</td><td>Image</td><td>Bitmap</td><td>13</td><td>12</td><td>349</td><td>211</td><td>1</td><td/><td/><td/><td/><td>0</td><td/><td/><td>NewBinary5</td></row>
		<row><td>SplashBitmap</td><td>Next</td><td>PushButton</td><td>230</td><td>243</td><td>66</td><td>17</td><td>3</td><td/><td>##IDS_NEXT##</td><td>Cancel</td><td/><td>0</td><td/><td/><td/></row>
	</table>

	<table name="ControlCondition">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Action</col>
		<col key="yes" def="s255">Condition</col>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomSetup</td><td>Details</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomSetup</td><td>InstallLabel</td><td>Hide</td><td>Installed</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>NOT Privileged</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>ProductState &gt; 0</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>Version9X</td></row>
		<row><td>CustomerInformation</td><td>DlgRadioGroupText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>NOT Privileged</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>ProductState &gt; 0</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>Version9X</td></row>
		<row><td>CustomerInformation</td><td>RadioGroup</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>CustomerInformation</td><td>SerialLabel</td><td>Show</td><td>SERIALNUMSHOW</td></row>
		<row><td>CustomerInformation</td><td>SerialNumber</td><td>Show</td><td>SERIALNUMSHOW</td></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Hide</td><td>SHOWCOPYRIGHT="No"</td></row>
		<row><td>InstallWelcome</td><td>Copyright</td><td>Show</td><td>SHOWCOPYRIGHT="Yes"</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>Disable</td><td>AgreeToLicense &lt;&gt; "Yes"</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>Enable</td><td>AgreeToLicense = "Yes"</td></row>
		<row><td>ReadyToInstall</td><td>CompanyNameText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>CurrentSettingsText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgText2</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle</td><td>Show</td><td>ProgressType0="Modify"</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle2</td><td>Show</td><td>ProgressType0="Repair"</td></row>
		<row><td>ReadyToInstall</td><td>DlgTitle3</td><td>Show</td><td>ProgressType0="install"</td></row>
		<row><td>ReadyToInstall</td><td>GroupBox1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>Disable</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>Enable</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>Hide</td><td>VersionNT &lt; "601" OR NOT ISSupportPerUser OR Installed</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>Show</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Hide</td><td>NOT SERIALNUMSHOW</td></row>
		<row><td>ReadyToInstall</td><td>SerialNumberText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>SetupTypeText2</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText1</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>TargetFolderText2</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>UserInformationText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>ReadyToInstall</td><td>UserNameText</td><td>Hide</td><td>VersionNT &gt;= "601" AND ISSupportPerUser AND NOT Installed</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Default</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>Default</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText1</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>FinishText2</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText1</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>RestContText2</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupCompleteError</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckBoxUpdates</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckForUpdatesText</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchProgram</td><td>Show</td><td>SHOWLAUNCHPROGRAM="-1" And PROGRAMFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckLaunchReadme</td><td>Show</td><td>SHOWLAUNCHREADME="-1"  And READMEFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchProgramText</td><td>Show</td><td>SHOWLAUNCHPROGRAM="-1" And PROGRAMFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>LaunchReadmeText</td><td>Show</td><td>SHOWLAUNCHREADME="-1"  And READMEFILETOLAUNCHATEND &lt;&gt; "" And NOT Installed And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation And NOT ISENABLEDWUSFINISHDIALOG</td></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine2</td><td>Show</td><td>ProgressType2="installed" And ((ACTION&lt;&gt;"INSTALL") OR (NOT ISENABLEDWUSFINISHDIALOG) OR (ISENABLEDWUSFINISHDIALOG And Installed))</td></row>
		<row><td>SetupCompleteSuccess</td><td>TextLine3</td><td>Show</td><td>ProgressType2="uninstalled" And ((ACTION&lt;&gt;"INSTALL") OR (NOT ISENABLEDWUSFINISHDIALOG) OR (ISENABLEDWUSFINISHDIALOG And Installed))</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine1</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine2</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupCompleteSuccess</td><td>UpdateTextLine3</td><td>Show</td><td>ISENABLEDWUSFINISHDIALOG And NOT Installed And ACTION="INSTALL"</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Default</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>Disable</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>Enable</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>CheckShowMsiLog</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>Default</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText1</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Hide</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>FinishText2</td><td>Show</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText1</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Hide</td><td>NOT UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>RestContText2</td><td>Show</td><td>UpdateStarted</td></row>
		<row><td>SetupInterrupted</td><td>ShowMsiLogText</td><td>Show</td><td>MsiLogFileLocation</td></row>
		<row><td>SetupProgress</td><td>DlgDesc</td><td>Show</td><td>ProgressType2="installed"</td></row>
		<row><td>SetupProgress</td><td>DlgDesc2</td><td>Show</td><td>ProgressType2="uninstalled"</td></row>
		<row><td>SetupProgress</td><td>DlgText</td><td>Show</td><td>ProgressType3="installs"</td></row>
		<row><td>SetupProgress</td><td>DlgText2</td><td>Show</td><td>ProgressType3="uninstalls"</td></row>
		<row><td>SetupProgress</td><td>DlgTitle</td><td>Show</td><td>ProgressType1="Installing"</td></row>
		<row><td>SetupProgress</td><td>DlgTitle2</td><td>Show</td><td>ProgressType1="Uninstalling"</td></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Hide</td><td>RESUME</td></row>
		<row><td>SetupResume</td><td>PreselectedText</td><td>Show</td><td>NOT RESUME</td></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Hide</td><td>NOT RESUME</td></row>
		<row><td>SetupResume</td><td>ResumeText</td><td>Show</td><td>RESUME</td></row>
	</table>

	<table name="ControlEvent">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Event</col>
		<col key="yes" def="s255">Argument</col>
		<col key="yes" def="S255">Condition</col>
		<col def="I2">Ordering</col>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>AdminChangeFolder</td><td>Cancel</td><td>Reset</td><td>0</td><td>1</td><td>1</td></row>
		<row><td>AdminChangeFolder</td><td>NewFolder</td><td>DirectoryListNew</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>AdminChangeFolder</td><td>OK</td><td>SetTargetPath</td><td>TARGETDIR</td><td>1</td><td>1</td></row>
		<row><td>AdminChangeFolder</td><td>Up</td><td>DirectoryListUp</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Back</td><td>NewDialog</td><td>AdminWelcome</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Browse</td><td>SpawnDialog</td><td>AdminChangeFolder</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>3</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>2</td></row>
		<row><td>AdminNetworkLocation</td><td>InstallNow</td><td>SetTargetPath</td><td>TARGETDIR</td><td>1</td><td>1</td></row>
		<row><td>AdminWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>AdminWelcome</td><td>Next</td><td>NewDialog</td><td>AdminNetworkLocation</td><td>1</td><td>0</td></row>
		<row><td>CancelSetup</td><td>No</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>CancelSetup</td><td>Yes</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>CancelSetup</td><td>Yes</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>CustomSetup</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>NOT Installed</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>Installed</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>ChangeFolder</td><td>SelectionBrowse</td><td>InstallChangeFolder</td><td>1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Details</td><td>SelectionBrowse</td><td>DiskSpaceRequirements</td><td>1</td><td>1</td></row>
		<row><td>CustomSetup</td><td>Help</td><td>SpawnDialog</td><td>CustomSetupTips</td><td>1</td><td>1</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>CustomSetup</td><td>Next</td><td>[_IsSetupTypeMin]</td><td>Custom</td><td>1</td><td>0</td></row>
		<row><td>CustomSetupTips</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Back</td><td>NewDialog</td><td>LicenseAgreement</td><td>NOT Installed</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>EndDialog</td><td>Exit</td><td>(SERIALNUMVALRETRYLIMIT) And (SERIALNUMVALRETRYLIMIT&lt;0) And (SERIALNUMVALRETURN&lt;&gt;SERIALNUMVALSUCCESSRETVAL)</td><td>2</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>(Not SERIALNUMVALRETURN) OR (SERIALNUMVALRETURN=SERIALNUMVALSUCCESSRETVAL)</td><td>3</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>[ALLUSERS]</td><td>1</td><td>ApplicationUsers = "AllUsers" And Privileged</td><td>1</td></row>
		<row><td>CustomerInformation</td><td>Next</td><td>[ALLUSERS]</td><td>{}</td><td>ApplicationUsers = "OnlyCurrentUser" And Privileged</td><td>2</td></row>
		<row><td>DatabaseFolder</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>SpawnDialog</td><td>InstallChangeFolder</td><td>1</td><td>1</td></row>
		<row><td>DatabaseFolder</td><td>ChangeFolder</td><td>[_BrowseProperty]</td><td>DATABASEDIR</td><td>1</td><td>2</td></row>
		<row><td>DatabaseFolder</td><td>Next</td><td>NewDialog</td><td>SetupType</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>Back</td><td>NewDialog</td><td>LicenseAgreement</td><td>NOT Installed</td><td>0</td></row>
		<row><td>DestinationFolder</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>SpawnDialog</td><td>InstallChangeFolder</td><td>1</td><td>1</td></row>
		<row><td>DestinationFolder</td><td>ChangeFolder</td><td>[_BrowseProperty]</td><td>INSTALLDIR</td><td>1</td><td>2</td></row>
		<row><td>DestinationFolder</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>1</td><td>0</td></row>
		<row><td>DiskSpaceRequirements</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Exit</td><td>EndDialog</td><td>Exit</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Ignore</td><td>EndDialog</td><td>Ignore</td><td>1</td><td>0</td></row>
		<row><td>FilesInUse</td><td>Retry</td><td>EndDialog</td><td>Retry</td><td>1</td><td>0</td></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>InstallChangeFolder</td><td>Cancel</td><td>Reset</td><td>0</td><td>1</td><td>1</td></row>
		<row><td>InstallChangeFolder</td><td>NewFolder</td><td>DirectoryListNew</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>3</td></row>
		<row><td>InstallChangeFolder</td><td>OK</td><td>SetTargetPath</td><td>[_BrowseProperty]</td><td>1</td><td>2</td></row>
		<row><td>InstallChangeFolder</td><td>Up</td><td>DirectoryListUp</td><td>0</td><td>1</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Back</td><td>NewDialog</td><td>SplashBitmap</td><td>Display_IsBitmapDlg</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>InstallWelcome</td><td>Next</td><td>NewDialog</td><td>LicenseAgreement</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Back</td><td>NewDialog</td><td>InstallWelcome</td><td>NOT Installed</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>ISPrintButton</td><td>DoAction</td><td>ISPrint</td><td>1</td><td>0</td></row>
		<row><td>LicenseAgreement</td><td>Next</td><td>NewDialog</td><td>DestinationFolder</td><td>AgreeToLicense = "Yes"</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Back</td><td>NewDialog</td><td>MaintenanceWelcome</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>CustomSetup</td><td>_IsMaintenance = "Change"</td><td>12</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>_IsMaintenance = "Reinstall"</td><td>13</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>NewDialog</td><td>ReadyToRemove</td><td>_IsMaintenance = "Remove"</td><td>11</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>Reinstall</td><td>ALL</td><td>_IsMaintenance = "Reinstall"</td><td>10</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>ReinstallMode</td><td>[ReinstallModeText]</td><td>_IsMaintenance = "Reinstall"</td><td>9</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType0]</td><td>Modify</td><td>_IsMaintenance = "Change"</td><td>2</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType0]</td><td>Repair</td><td>_IsMaintenance = "Reinstall"</td><td>1</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType1]</td><td>Modifying</td><td>_IsMaintenance = "Change"</td><td>3</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType1]</td><td>Repairing</td><td>_IsMaintenance = "Reinstall"</td><td>4</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType2]</td><td>modified</td><td>_IsMaintenance = "Change"</td><td>6</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType2]</td><td>repairs</td><td>_IsMaintenance = "Reinstall"</td><td>5</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType3]</td><td>modifies</td><td>_IsMaintenance = "Change"</td><td>7</td></row>
		<row><td>MaintenanceType</td><td>Next</td><td>[ProgressType3]</td><td>repairs</td><td>_IsMaintenance = "Reinstall"</td><td>8</td></row>
		<row><td>MaintenanceWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>MaintenanceWelcome</td><td>Next</td><td>NewDialog</td><td>MaintenanceType</td><td>1</td><td>0</td></row>
		<row><td>MsiRMFilesInUse</td><td>Cancel</td><td>EndDialog</td><td>Exit</td><td>1</td><td>1</td></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>EndDialog</td><td>Return</td><td>1</td><td>1</td></row>
		<row><td>MsiRMFilesInUse</td><td>OK</td><td>RMShutdownAndRestart</td><td>0</td><td>RestartManagerOption="CloseRestart"</td><td>2</td></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>NewDialog</td><td>AdminNetworkLocation</td><td>ACTION = "ADMIN"</td><td>0</td></row>
		<row><td>OutOfSpace</td><td>Resume</td><td>NewDialog</td><td>DestinationFolder</td><td>ACTION &lt;&gt; "ADMIN"</td><td>0</td></row>
		<row><td>PatchWelcome</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>EndDialog</td><td>Return</td><td>1</td><td>3</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>Reinstall</td><td>ALL</td><td>PATCH And REINSTALL=""</td><td>1</td></row>
		<row><td>PatchWelcome</td><td>Next</td><td>ReinstallMode</td><td>omus</td><td>PATCH And REINSTALLMODE=""</td><td>2</td></row>
		<row><td>ReadmeInformation</td><td>Back</td><td>NewDialog</td><td>LicenseAgreement</td><td>1</td><td>1</td></row>
		<row><td>ReadmeInformation</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>1</td></row>
		<row><td>ReadmeInformation</td><td>Next</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>CustomSetup</td><td>Installed OR _IsSetupTypeMin = "Custom"</td><td>2</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>DestinationFolder</td><td>NOT Installed</td><td>1</td></row>
		<row><td>ReadyToInstall</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>Installed AND _IsMaintenance = "Reinstall"</td><td>3</td></row>
		<row><td>ReadyToInstall</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallNow</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ALLUSERS]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[MSIINSTALLPERUSER]</td><td>{}</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerMachine</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ALLUSERS]</td><td>2</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[MSIINSTALLPERUSER]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType1]</td><td>Installing</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType2]</td><td>installed</td><td>1</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>InstallPerUser</td><td>[ProgressType3]</td><td>installs</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>Back</td><td>NewDialog</td><td>MaintenanceType</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>2</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>2</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>Remove</td><td>ALL</td><td>1</td><td>1</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType1]</td><td>Uninstalling</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType2]</td><td>uninstalled</td><td>1</td><td>0</td></row>
		<row><td>ReadyToRemove</td><td>RemoveNow</td><td>[ProgressType3]</td><td>uninstalls</td><td>1</td><td>0</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteError</td><td>Back</td><td>[Suspend]</td><td>{}</td><td>1</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>EndDialog</td><td>Return</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteError</td><td>Cancel</td><td>[Suspend]</td><td>1</td><td>1</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1")</td><td>3</td></row>
		<row><td>SetupCompleteError</td><td>Finish</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1") And NOT ISENABLEDWUSFINISHDIALOG</td><td>6</td></row>
		<row><td>SetupCompleteSuccess</td><td>OK</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupError</td><td>A</td><td>EndDialog</td><td>ErrorAbort</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>C</td><td>EndDialog</td><td>ErrorCancel</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>I</td><td>EndDialog</td><td>ErrorIgnore</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>N</td><td>EndDialog</td><td>ErrorNo</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>O</td><td>EndDialog</td><td>ErrorOk</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>R</td><td>EndDialog</td><td>ErrorRetry</td><td>1</td><td>0</td></row>
		<row><td>SetupError</td><td>Y</td><td>EndDialog</td><td>ErrorYes</td><td>1</td><td>0</td></row>
		<row><td>SetupInitialization</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupInterrupted</td><td>Back</td><td>[Suspend]</td><td>{}</td><td>1</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupInterrupted</td><td>Cancel</td><td>[Suspend]</td><td>1</td><td>1</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>DoAction</td><td>CleanUp</td><td>ISSCRIPTRUNNING="1"</td><td>1</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>DoAction</td><td>ShowMsiLog</td><td>MsiLogFileLocation And (ISSHOWMSILOG="1")</td><td>3</td></row>
		<row><td>SetupInterrupted</td><td>Finish</td><td>EndDialog</td><td>Exit</td><td>1</td><td>2</td></row>
		<row><td>SetupProgress</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Next</td><td>EndDialog</td><td>Return</td><td>OutOfNoRbDiskSpace &lt;&gt; 1</td><td>0</td></row>
		<row><td>SetupResume</td><td>Next</td><td>NewDialog</td><td>OutOfSpace</td><td>OutOfNoRbDiskSpace = 1</td><td>0</td></row>
		<row><td>SetupType</td><td>Back</td><td>NewDialog</td><td>CustomerInformation</td><td>1</td><td>1</td></row>
		<row><td>SetupType</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>NewDialog</td><td>CustomSetup</td><td>_IsSetupTypeMin = "Custom"</td><td>2</td></row>
		<row><td>SetupType</td><td>Next</td><td>NewDialog</td><td>ReadyToInstall</td><td>_IsSetupTypeMin &lt;&gt; "Custom"</td><td>1</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>100</td><td>_IsSetupTypeMin="Minimal"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>200</td><td>_IsSetupTypeMin="Typical"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>SetInstallLevel</td><td>300</td><td>_IsSetupTypeMin="Custom"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[ISRUNSETUPTYPEADDLOCALEVENT]</td><td>1</td><td>1</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameCustom]</td><td>_IsSetupTypeMin = "Custom"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameMinimal]</td><td>_IsSetupTypeMin = "Minimal"</td><td>0</td></row>
		<row><td>SetupType</td><td>Next</td><td>[SelectedSetupType]</td><td>[DisplayNameTypical]</td><td>_IsSetupTypeMin = "Typical"</td><td>0</td></row>
		<row><td>SplashBitmap</td><td>Cancel</td><td>SpawnDialog</td><td>CancelSetup</td><td>1</td><td>0</td></row>
		<row><td>SplashBitmap</td><td>Next</td><td>NewDialog</td><td>InstallWelcome</td><td>1</td><td>0</td></row>
	</table>

	<table name="CreateFolder">
		<col key="yes" def="s72">Directory_</col>
		<col key="yes" def="s72">Component_</col>
		<row><td>ADVANCEDDEMOS1</td><td>ISX_DEFAULTCOMPONENT20</td></row>
		<row><td>DEMO9___AI</td><td>ISX_DEFAULTCOMPONENT66</td></row>
		<row><td>DEMOSSOURCECODE</td><td>ISX_DEFAULTCOMPONENT18</td></row>
		<row><td>FONT</td><td>ISX_DEFAULTCOMPONENT78</td></row>
		<row><td>HELP</td><td>ISX_DEFAULTCOMPONENT94</td></row>
		<row><td>INSTALLDIR</td><td>ISX_DEFAULTCOMPONENT</td></row>
		<row><td>INSTALLDIR</td><td>NewComponent1</td></row>
		<row><td>INTRODUCTIONDEMO4.0</td><td>ISX_DEFAULTCOMPONENT52</td></row>
		<row><td>PersonalFolder</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>REACHDEMOS1</td><td>ISX_DEFAULTCOMPONENT101</td></row>
		<row><td>SKINNED</td><td>ISX_DEFAULTCOMPONENT116</td></row>
		<row><td>SKINS</td><td>ISX_DEFAULTCOMPONENT12</td></row>
		<row><td>SKINS1</td><td>ISX_DEFAULTCOMPONENT108</td></row>
		<row><td>TEMPLATES</td><td>ISX_DEFAULTCOMPONENT3</td></row>
		<row><td>VISUAL_STUDIO_2010</td><td>ISX_DEFAULTCOMPONENT2</td></row>
	</table>

	<table name="CustomAction">
		<col key="yes" def="s72">Action</col>
		<col def="i2">Type</col>
		<col def="S64">Source</col>
		<col def="S0">Target</col>
		<col def="I4">ExtendedType</col>
		<col def="S255">ISComments</col>
		<row><td>ISPreventDowngrade</td><td>19</td><td/><td>[IS_PREVENT_DOWNGRADE_EXIT]</td><td/><td>Exits install when a newer version of this product is found</td></row>
		<row><td>ISPrint</td><td>1</td><td>SetAllUsers.dll</td><td>PrintScrollableText</td><td/><td>Prints the contents of a ScrollableText control on a dialog.</td></row>
		<row><td>ISRunSetupTypeAddLocalEvent</td><td>1</td><td>ISExpHlp.dll</td><td>RunSetupTypeAddLocalEvent</td><td/><td>Run the AddLocal events associated with the Next button on the Setup Type dialog.</td></row>
		<row><td>ISSelfRegisterCosting</td><td>1</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterCosting</td><td/><td/></row>
		<row><td>ISSelfRegisterFiles</td><td>3073</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterFiles</td><td/><td/></row>
		<row><td>ISSelfRegisterFinalize</td><td>1</td><td>ISSELFREG.DLL</td><td>ISSelfRegisterFinalize</td><td/><td/></row>
		<row><td>ISSetAllUsers</td><td>257</td><td>SetAllUsers.dll</td><td>SetAllUsers</td><td/><td/></row>
		<row><td>ISUnSelfRegisterFiles</td><td>3073</td><td>ISSELFREG.DLL</td><td>ISUnSelfRegisterFiles</td><td/><td/></row>
		<row><td>SetARPINSTALLLOCATION</td><td>51</td><td>ARPINSTALLLOCATION</td><td>[INSTALLDIR]</td><td/><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>51</td><td>ALLUSERSPROFILE</td><td>[%SystemRoot]\Profiles\All Users</td><td/><td/></row>
		<row><td>ShowMsiLog</td><td>226</td><td>SystemFolder</td><td>[SystemFolder]notepad.exe "[MsiLogFileLocation]"</td><td/><td>Shows Property-driven MSI Log</td></row>
		<row><td>setAllUsersProfile2K</td><td>51</td><td>ALLUSERSPROFILE</td><td>[%ALLUSERSPROFILE]</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>51</td><td>USERPROFILE</td><td>[%USERPROFILE]</td><td/><td/></row>
	</table>

	<table name="Dialog">
		<col key="yes" def="s72">Dialog</col>
		<col def="i2">HCentering</col>
		<col def="i2">VCentering</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="I4">Attributes</col>
		<col def="L128">Title</col>
		<col def="s50">Control_First</col>
		<col def="S50">Control_Default</col>
		<col def="S50">Control_Cancel</col>
		<col def="S255">ISComments</col>
		<col def="S72">TextStyle_</col>
		<col def="I4">ISWindowStyle</col>
		<col def="I4">ISResourceId</col>
		<row><td>AdminChangeFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tail</td><td>OK</td><td>Cancel</td><td>Install Point Browse</td><td/><td>0</td><td/></row>
		<row><td>AdminNetworkLocation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>InstallNow</td><td>InstallNow</td><td>Cancel</td><td>Network Location</td><td/><td>0</td><td/></row>
		<row><td>AdminWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Administration Welcome</td><td/><td>0</td><td/></row>
		<row><td>CancelSetup</td><td>50</td><td>50</td><td>260</td><td>85</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>No</td><td>No</td><td>No</td><td>Cancel</td><td/><td>0</td><td/></row>
		<row><td>CustomSetup</td><td>50</td><td>50</td><td>374</td><td>266</td><td>35</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tree</td><td>Next</td><td>Cancel</td><td>Custom Selection</td><td/><td>0</td><td/></row>
		<row><td>CustomSetupTips</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Custom Setup Tips</td><td/><td>0</td><td/></row>
		<row><td>CustomerInformation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>NameEdit</td><td>Next</td><td>Cancel</td><td>Identification</td><td/><td>0</td><td/></row>
		<row><td>DatabaseFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Database Folder</td><td/><td>0</td><td/></row>
		<row><td>DestinationFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Destination Folder</td><td/><td>0</td><td/></row>
		<row><td>DiskSpaceRequirements</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Feature Details</td><td/><td>0</td><td/></row>
		<row><td>FilesInUse</td><td>50</td><td>50</td><td>374</td><td>266</td><td>19</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Retry</td><td>Retry</td><td>Exit</td><td>Files in Use</td><td/><td>0</td><td/></row>
		<row><td>InstallChangeFolder</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Tail</td><td>OK</td><td>Cancel</td><td>Browse</td><td/><td>0</td><td/></row>
		<row><td>InstallWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Welcome Panel</td><td/><td>0</td><td/></row>
		<row><td>LicenseAgreement</td><td>50</td><td>50</td><td>374</td><td>266</td><td>2</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Agree</td><td>Next</td><td>Cancel</td><td>License Agreement</td><td/><td>0</td><td/></row>
		<row><td>MaintenanceType</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RadioGroup</td><td>Next</td><td>Cancel</td><td>Change, Reinstall, Remove</td><td/><td>0</td><td/></row>
		<row><td>MaintenanceWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Maintenance Welcome</td><td/><td>0</td><td/></row>
		<row><td>MsiRMFilesInUse</td><td>50</td><td>50</td><td>374</td><td>266</td><td>19</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>Cancel</td><td>RestartManager Files in Use</td><td/><td>0</td><td/></row>
		<row><td>OutOfSpace</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Resume</td><td>Resume</td><td>Resume</td><td>Out Of Disk Space</td><td/><td>0</td><td/></row>
		<row><td>PatchWelcome</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS__IsPatchDlg_PatchWizard##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Patch Panel</td><td/><td>0</td><td/></row>
		<row><td>ReadmeInformation</td><td>50</td><td>50</td><td>374</td><td>266</td><td>7</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Readme Information</td><td/><td>0</td><td>0</td></row>
		<row><td>ReadyToInstall</td><td>50</td><td>50</td><td>374</td><td>266</td><td>35</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>InstallNow</td><td>InstallNow</td><td>Cancel</td><td>Ready to Install</td><td/><td>0</td><td/></row>
		<row><td>ReadyToRemove</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RemoveNow</td><td>RemoveNow</td><td>Cancel</td><td>Verify Remove</td><td/><td>0</td><td/></row>
		<row><td>SetupCompleteError</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Finish</td><td>Finish</td><td>Finish</td><td>Fatal Error</td><td/><td>0</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>OK</td><td>OK</td><td>OK</td><td>Exit</td><td/><td>0</td><td/></row>
		<row><td>SetupError</td><td>50</td><td>50</td><td>270</td><td>110</td><td>65543</td><td>##IDS__IsErrorDlg_InstallerInfo##</td><td>ErrorText</td><td>O</td><td>C</td><td>Error</td><td/><td>0</td><td/></row>
		<row><td>SetupInitialization</td><td>50</td><td>50</td><td>374</td><td>266</td><td>5</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Cancel</td><td>Cancel</td><td>Cancel</td><td>Setup Initialization</td><td/><td>0</td><td/></row>
		<row><td>SetupInterrupted</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Finish</td><td>Finish</td><td>Finish</td><td>User Exit</td><td/><td>0</td><td/></row>
		<row><td>SetupProgress</td><td>50</td><td>50</td><td>374</td><td>266</td><td>5</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Cancel</td><td>Cancel</td><td>Cancel</td><td>Progress</td><td/><td>0</td><td/></row>
		<row><td>SetupResume</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Resume</td><td/><td>0</td><td/></row>
		<row><td>SetupType</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>RadioGroup</td><td>Next</td><td>Cancel</td><td>Setup Type</td><td/><td>0</td><td/></row>
		<row><td>SplashBitmap</td><td>50</td><td>50</td><td>374</td><td>266</td><td>3</td><td>##IDS_PRODUCTNAME_INSTALLSHIELD##</td><td>Next</td><td>Next</td><td>Cancel</td><td>Welcome Bitmap</td><td/><td>0</td><td/></row>
	</table>

	<table name="Directory">
		<col key="yes" def="s72">Directory</col>
		<col def="S72">Directory_Parent</col>
		<col def="l255">DefaultDir</col>
		<col def="S255">ISDescription</col>
		<col def="I4">ISAttributes</col>
		<col def="S255">ISFolderName</col>
		<row><td>ADVANCEDDEMOS</td><td>DEMOSSOURCECODE</td><td>ADVANC~1|AdvancedDemos</td><td/><td>0</td><td/></row>
		<row><td>ADVANCEDDEMOS1</td><td>ADVANCEDDEMOS</td><td>ADVANC~1|AdvancedDemos</td><td/><td>0</td><td/></row>
		<row><td>ADVANCEDDEMOS2</td><td>ADVANCEDDEMOS1</td><td>ADVANC~1|AdvancedDemos</td><td/><td>0</td><td/></row>
		<row><td>ADVANCEDDEMOSCONTENT</td><td>ADVANCEDDEMOS1</td><td>ADVANC~1|AdvancedDemosContent</td><td/><td>0</td><td/></row>
		<row><td>ADVDEMOSBIN</td><td>INSTALLDIR</td><td>ADVDEM~1|AdvDemosBin</td><td/><td>0</td><td/></row>
		<row><td>ALLUSERSPROFILE</td><td>TARGETDIR</td><td>.:ALLUSE~1|All Users</td><td/><td>0</td><td/></row>
		<row><td>ANIMATION</td><td>PLOOBSPROJECTTEMPLATE</td><td>ANIMAT~1|Animation</td><td/><td>0</td><td/></row>
		<row><td>AdminToolsFolder</td><td>TARGETDIR</td><td>.:Admint~1|AdminTools</td><td/><td>0</td><td/></row>
		<row><td>AppDataFolder</td><td>TARGETDIR</td><td>.:APPLIC~1|Application Data</td><td/><td>0</td><td/></row>
		<row><td>BEHAVIORS</td><td>_D_DEMOS1</td><td>BEHAVI~1|Behaviors</td><td/><td>0</td><td/></row>
		<row><td>BEHAVIOR_TREE</td><td>DEMO9___AI</td><td>BEHAVI~1|Behavior Tree</td><td/><td>0</td><td/></row>
		<row><td>BILBOARD</td><td>PLOOBSPROJECTTEMPLATE</td><td>Bilboard</td><td/><td>0</td><td/></row>
		<row><td>CHARACTER</td><td>DEMO9.8_PHYSX_2.8.X</td><td>CHARAC~1|Character</td><td/><td>0</td><td/></row>
		<row><td>CLOTH</td><td>DEMO9.8_PHYSX_2.8.X</td><td>Cloth</td><td/><td>0</td><td/></row>
		<row><td>COMMONS</td><td>ADVANCEDDEMOS2</td><td>Commons</td><td/><td>0</td><td/></row>
		<row><td>COMMONS1</td><td>INTRODUCTIONDEMO4.01</td><td>Commons</td><td/><td>0</td><td/></row>
		<row><td>CONTENT</td><td>ADVDEMOSBIN</td><td>Content</td><td/><td>0</td><td/></row>
		<row><td>CROWED</td><td>_D_DEMOS1</td><td>Crowed</td><td/><td>0</td><td/></row>
		<row><td>CURSORS</td><td>DEFAULT</td><td>Cursors</td><td/><td>0</td><td/></row>
		<row><td>CURSORS1</td><td>DEFAULT1</td><td>Cursors</td><td/><td>0</td><td/></row>
		<row><td>CUSTOMSKINNEDANIMATION</td><td>DEMO7___ANIMATIONSAMPLE</td><td>CUSTOM~1|CustomSkinnedAnimation</td><td/><td>0</td><td/></row>
		<row><td>CommonAppDataFolder</td><td>TARGETDIR</td><td>.:Common~1|CommonAppData</td><td/><td>0</td><td/></row>
		<row><td>CommonFilesFolder</td><td>TARGETDIR</td><td>.:Common</td><td/><td>0</td><td/></row>
		<row><td>DATABASEDIR</td><td>ISYourDataBaseDir</td><td>.</td><td/><td>0</td><td/></row>
		<row><td>DEFAULT</td><td>SKINS</td><td>Default</td><td/><td>0</td><td/></row>
		<row><td>DEFAULT1</td><td>SKINS1</td><td>Default</td><td/><td>0</td><td/></row>
		<row><td>DEMO1___BASICSCREEN</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO1-~1|Demo1 - BasicScreen</td><td/><td>0</td><td/></row>
		<row><td>DEMO1____LOADER</td><td>ADVANCEDDEMOS2</td><td>DEMO1-~1|Demo1 -  Loader</td><td/><td>0</td><td/></row>
		<row><td>DEMO2___INPUT</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO2-~1|Demo2 - Input</td><td/><td>0</td><td/></row>
		<row><td>DEMO2___MATERIALS</td><td>ADVANCEDDEMOS2</td><td>DEMO2-~1|Demo2 - Materials</td><td/><td>0</td><td/></row>
		<row><td>DEMO3___LIGHTS</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO3-~1|Demo3 - Lights</td><td/><td>0</td><td/></row>
		<row><td>DEMO3___SOUND</td><td>ADVANCEDDEMOS2</td><td>DEMO3-~1|Demo3 - Sound</td><td/><td>0</td><td/></row>
		<row><td>DEMO4___PHYSICS</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO4-~1|Demo4 - Physics</td><td/><td>0</td><td/></row>
		<row><td>DEMO4___TERRAIN</td><td>ADVANCEDDEMOS2</td><td>DEMO4-~1|Demo4 - Terrain</td><td/><td>0</td><td/></row>
		<row><td>DEMO5___CAMERAS</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO5-~1|Demo5 - Cameras</td><td/><td>0</td><td/></row>
		<row><td>DEMO5___PARTICLESAMPLES</td><td>ADVANCEDDEMOS2</td><td>DEMO5-~1|Demo5 - ParticleSamples</td><td/><td>0</td><td/></row>
		<row><td>DEMO6___BILBOARDSSAMPLES</td><td>ADVANCEDDEMOS2</td><td>DEMO6-~1|Demo6 - BilboardsSamples</td><td/><td>0</td><td/></row>
		<row><td>DEMO6___TRIGGER</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO6-~1|Demo6 - Trigger</td><td/><td>0</td><td/></row>
		<row><td>DEMO7___ANIMATIONSAMPLE</td><td>ADVANCEDDEMOS2</td><td>DEMO7-~1|Demo7 - AnimationSample</td><td/><td>0</td><td/></row>
		<row><td>DEMO7___SCRIPTS</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO7-~1|Demo7 - Scripts</td><td/><td>0</td><td/></row>
		<row><td>DEMO8___GUI</td><td>ADVANCEDDEMOS2</td><td>DEMO8-~1|Demo8 - GUI</td><td/><td>0</td><td/></row>
		<row><td>DEMO8___SERIALIZATION</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO8-~1|Demo8 - Serialization</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.1_WATER_AND_OCEAN</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.1 Water and Ocean</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.2_SHADOW</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.2 Shadow</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.3_SSAO</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.3 SSAO</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.4_OCTREE_CULLING</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.4 Octree Culling</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.5_DECAL</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.5 Decal</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.6_EXPLOSION</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.6 Explosion</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.7_STEALTH</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.7 Stealth</td><td/><td>0</td><td/></row>
		<row><td>DEMO9.8_PHYSX_2.8.X</td><td>ADVANCEDDEMOS2</td><td>DEMO9_~1|Demo9.8 Physx 2.8.x</td><td/><td>0</td><td/></row>
		<row><td>DEMO9___AI</td><td>INTRODUCTIONDEMO4.01</td><td>DEMO9-~1|Demo9 - AI</td><td/><td>0</td><td/></row>
		<row><td>DEMO9____NOISE_TEXTURES</td><td>ADVANCEDDEMOS2</td><td>DEMO9-~1|Demo9 -  Noise Textures</td><td/><td>0</td><td/></row>
		<row><td>DEMOSSOURCECODE</td><td>INSTALLDIR</td><td>DEMOSS~1|DemosSourceCode</td><td/><td>0</td><td/></row>
		<row><td>DEMOS_EXTRAS</td><td>INTRODUCTIONDEMO4.01</td><td>DEMOSE~1|Demos Extras</td><td/><td>0</td><td/></row>
		<row><td>DLL</td><td>ADVANCEDDEMOS1</td><td>Dll</td><td/><td>0</td><td/></row>
		<row><td>DLL1</td><td>INTRODUCTIONDEMO4.0</td><td>Dll</td><td/><td>0</td><td/></row>
		<row><td>DLLS</td><td>REACHDEMOS1</td><td>Dlls</td><td/><td>0</td><td/></row>
		<row><td>DUDE</td><td>MODEL</td><td>Dude</td><td/><td>0</td><td/></row>
		<row><td>DUDE1</td><td>MODEL1</td><td>Dude</td><td/><td>0</td><td/></row>
		<row><td>DesktopFolder</td><td>TARGETDIR</td><td>.:Desktop</td><td/><td>3</td><td/></row>
		<row><td>EFFECTS</td><td>CONTENT</td><td>Effects</td><td/><td>0</td><td/></row>
		<row><td>EFFECTS1</td><td>ADVANCEDDEMOSCONTENT</td><td>Effects</td><td/><td>0</td><td/></row>
		<row><td>FLUIDS</td><td>DEMO9.8_PHYSX_2.8.X</td><td>Fluids</td><td/><td>0</td><td/></row>
		<row><td>FONT</td><td>INTRODUCTIONDEMO4.0CONTENT</td><td>Font</td><td/><td>0</td><td/></row>
		<row><td>FONTS</td><td>DEFAULT</td><td>Fonts</td><td/><td>0</td><td/></row>
		<row><td>FONTS1</td><td>DEFAULT1</td><td>Fonts</td><td/><td>0</td><td/></row>
		<row><td>FSM</td><td>DEMO9___AI</td><td>FSM</td><td/><td>0</td><td/></row>
		<row><td>FavoritesFolder</td><td>TARGETDIR</td><td>.:FAVORI~1|Favorites</td><td/><td>0</td><td/></row>
		<row><td>FontsFolder</td><td>TARGETDIR</td><td>.:Fonts</td><td/><td>0</td><td/></row>
		<row><td>GRAPHICSAPP1</td><td>PHONESILVERLIGHTDEMO</td><td>GRAPHI~1|GraphicsApp1</td><td/><td>0</td><td/></row>
		<row><td>GRAPHICSAPP1LIBRARY</td><td>PHONESILVERLIGHTDEMO</td><td>GRAPHI~1|GraphicsApp1Library</td><td/><td>0</td><td/></row>
		<row><td>GRAPHICSAPP1LIBRARYCONTENT</td><td>PHONESILVERLIGHTDEMO</td><td>GRAPHI~1|GraphicsApp1LibraryContent</td><td/><td>0</td><td/></row>
		<row><td>GRAVITATIONALDEMO</td><td>_D_DEMOS1</td><td>GRAVIT~1|GravitationalDemo</td><td/><td>0</td><td/></row>
		<row><td>GlobalAssemblyCache</td><td>TARGETDIR</td><td>.:Global~1|GlobalAssemblyCache</td><td/><td>0</td><td/></row>
		<row><td>HELP</td><td>INSTALLDIR</td><td>Help</td><td/><td>0</td><td/></row>
		<row><td>HELPER</td><td>DEMO4___PHYSICS</td><td>Helper</td><td/><td>0</td><td/></row>
		<row><td>HELPER1</td><td>TEMPLATESCREENS</td><td>helper</td><td/><td>0</td><td/></row>
		<row><td>HELPER2</td><td>_D_DEMOS1</td><td>helper</td><td/><td>0</td><td/></row>
		<row><td>HELPERS</td><td>DEMO5___PARTICLESAMPLES</td><td>Helpers</td><td/><td>0</td><td/></row>
		<row><td>HELPERS1</td><td>DEMO3___LIGHTS</td><td>Helpers</td><td/><td>0</td><td/></row>
		<row><td>HELPERS2</td><td>_D_DEMOS</td><td>Helpers</td><td/><td>0</td><td/></row>
		<row><td>HELPERS3</td><td>_D_DEMOS2</td><td>Helpers</td><td/><td>0</td><td/></row>
		<row><td>IMAGES</td><td>DEFAULT</td><td>Images</td><td/><td>0</td><td/></row>
		<row><td>IMAGES1</td><td>DEFAULT1</td><td>Images</td><td/><td>0</td><td/></row>
		<row><td>INSTALLDIR</td><td>PLOOBSENGINE</td><td>.</td><td/><td>0</td><td/></row>
		<row><td>INTRODUCTIONDEMO</td><td>DEMOSSOURCECODE</td><td>INTROD~1|IntroductionDemo</td><td/><td>0</td><td/></row>
		<row><td>INTRODUCTIONDEMO4.0</td><td>INTRODUCTIONDEMO</td><td>INTROD~1|IntroductionDemo4.0</td><td/><td>0</td><td/></row>
		<row><td>INTRODUCTIONDEMO4.01</td><td>INTRODUCTIONDEMO4.0</td><td>INTROD~1|IntroductionDemo4.0</td><td/><td>0</td><td/></row>
		<row><td>INTRODUCTIONDEMO4.0CONTENT</td><td>INTRODUCTIONDEMO4.0</td><td>INTROD~1|IntroductionDemo4.0Content</td><td/><td>0</td><td/></row>
		<row><td>ISCommonFilesFolder</td><td>CommonFilesFolder</td><td>Instal~1|InstallShield</td><td/><td>0</td><td/></row>
		<row><td>ISMyCompanyDir</td><td>ProgramFilesFolder</td><td>MYCOMP~1|My Company Name</td><td/><td>0</td><td/></row>
		<row><td>ISMyProductDir</td><td>ISMyCompanyDir</td><td>MYPROD~1|My Product Name</td><td/><td>0</td><td/></row>
		<row><td>ISYourDataBaseDir</td><td>INSTALLDIR</td><td>Database</td><td/><td>0</td><td/></row>
		<row><td>ITEMTEMPLATES</td><td>TEMPLATES</td><td>ITEMTE~1|ItemTemplates</td><td/><td>0</td><td/></row>
		<row><td>LocalAppDataFolder</td><td>TARGETDIR</td><td>.:LocalA~1|LocalAppData</td><td/><td>0</td><td/></row>
		<row><td>MATERIAL</td><td>DEMO9.8_PHYSX_2.8.X</td><td>Material</td><td/><td>0</td><td/></row>
		<row><td>MODEL</td><td>CONTENT</td><td>Model</td><td/><td>0</td><td/></row>
		<row><td>MODEL1</td><td>ADVANCEDDEMOSCONTENT</td><td>Model</td><td/><td>0</td><td/></row>
		<row><td>MODEL2</td><td>INTRODUCTIONDEMO4.0CONTENT</td><td>Model</td><td/><td>0</td><td/></row>
		<row><td>MODEL3</td><td>GRAPHICSAPP1LIBRARYCONTENT</td><td>Model</td><td/><td>0</td><td/></row>
		<row><td>MODEL4</td><td>PLOOBSPROJECTTEMPLATECONTENT</td><td>Model</td><td/><td>0</td><td/></row>
		<row><td>MODELANIM</td><td>DEMO7___ANIMATIONSAMPLE</td><td>MODELA~1|ModelAnim</td><td/><td>0</td><td/></row>
		<row><td>MODELINFOS</td><td>CONTENT</td><td>MODELI~1|ModelInfos</td><td/><td>0</td><td/></row>
		<row><td>MODELINFOS1</td><td>ADVANCEDDEMOSCONTENT</td><td>MODELI~1|ModelInfos</td><td/><td>0</td><td/></row>
		<row><td>MY_PRODUCT_NAME</td><td>PLOOBS</td><td>MYPROD~1|My Product Name</td><td/><td>0</td><td/></row>
		<row><td>MyPicturesFolder</td><td>TARGETDIR</td><td>.:MyPict~1|MyPictures</td><td/><td>0</td><td/></row>
		<row><td>NetHoodFolder</td><td>TARGETDIR</td><td>.:NetHood</td><td/><td>0</td><td/></row>
		<row><td>PARTICLE3D</td><td>PLOOBSPROJECTTEMPLATE</td><td>PARTIC~1|Particle3D</td><td/><td>0</td><td/></row>
		<row><td>PHONE7</td><td>PHONESILVERLIGHTDEMO</td><td>Phone7</td><td/><td>0</td><td/></row>
		<row><td>PHONESILVERLIGHTDEMO</td><td>DEMOSSOURCECODE</td><td>PHONES~1|PhoneSilverLightDemo</td><td/><td>0</td><td/></row>
		<row><td>PLOOBS</td><td>ProgramFilesFolder</td><td>Ploobs</td><td/><td>0</td><td/></row>
		<row><td>PLOOBSENGINE</td><td>PLOOBS</td><td>PLOOBS~1|PloobsEngine</td><td/><td>0</td><td/></row>
		<row><td>PLOOBSPROJECTTEMPLATE</td><td>REACHDEMOS1</td><td>PLOOBS~1|PloobsProjectTemplate</td><td/><td>0</td><td/></row>
		<row><td>PLOOBSPROJECTTEMPLATECONTENT</td><td>REACHDEMOS1</td><td>PLOOBS~1|PloobsProjectTemplateContent</td><td/><td>0</td><td/></row>
		<row><td>POSTEFFECT</td><td>PLOOBSPROJECTTEMPLATE</td><td>POSTEF~1|PostEffect</td><td/><td>0</td><td/></row>
		<row><td>POSTEFFECT1</td><td>_D_DEMOS1</td><td>POSTEF~1|PostEffect</td><td/><td>0</td><td/></row>
		<row><td>PROJECTTEMPLATES</td><td>TEMPLATES</td><td>PROJEC~1|ProjectTemplates</td><td/><td>0</td><td/></row>
		<row><td>PROPERTIES</td><td>ADVANCEDDEMOS2</td><td>PROPER~1|Properties</td><td/><td>0</td><td/></row>
		<row><td>PROPERTIES1</td><td>INTRODUCTIONDEMO4.01</td><td>PROPER~1|Properties</td><td/><td>0</td><td/></row>
		<row><td>PROPERTIES2</td><td>GRAPHICSAPP1</td><td>PROPER~1|Properties</td><td/><td>0</td><td/></row>
		<row><td>PROPERTIES3</td><td>GRAPHICSAPP1LIBRARY</td><td>PROPER~1|Properties</td><td/><td>0</td><td/></row>
		<row><td>PROPERTIES4</td><td>PLOOBSPROJECTTEMPLATE</td><td>PROPER~1|Properties</td><td/><td>0</td><td/></row>
		<row><td>PersonalFolder</td><td>TARGETDIR</td><td>.:Personal</td><td/><td>0</td><td/></row>
		<row><td>PrimaryVolumePath</td><td>TARGETDIR</td><td>.:Primar~1|PrimaryVolumePath</td><td/><td>0</td><td/></row>
		<row><td>PrintHoodFolder</td><td>TARGETDIR</td><td>.:PRINTH~1|PrintHood</td><td/><td>0</td><td/></row>
		<row><td>ProgramFilesFolder</td><td>TARGETDIR</td><td>.:PROGRA~1|program files</td><td/><td>0</td><td/></row>
		<row><td>ProgramMenuFolder</td><td>TARGETDIR</td><td>.:Programs</td><td/><td>3</td><td/></row>
		<row><td>REACHDEMOS</td><td>DEMOSSOURCECODE</td><td>REACHD~1|ReachDemos</td><td/><td>0</td><td/></row>
		<row><td>REACHDEMOS1</td><td>REACHDEMOS</td><td>REACHD~1|ReachDemos</td><td/><td>0</td><td/></row>
		<row><td>RVO</td><td>DEMO9___AI</td><td>RVO</td><td/><td>0</td><td/></row>
		<row><td>RVO1</td><td>_D_DEMOS1</td><td>RVO</td><td/><td>0</td><td/></row>
		<row><td>RVO2</td><td>DEMO9___AI</td><td>RVO2</td><td/><td>0</td><td/></row>
		<row><td>RVO3D</td><td>DEMO9___AI</td><td>RVO3D</td><td/><td>0</td><td/></row>
		<row><td>RecentFolder</td><td>TARGETDIR</td><td>.:Recent</td><td/><td>0</td><td/></row>
		<row><td>SCREENS</td><td>CLOTH</td><td>Screens</td><td/><td>0</td><td/></row>
		<row><td>SCRIPT</td><td>INTRODUCTIONDEMO4.0CONTENT</td><td>Script</td><td/><td>0</td><td/></row>
		<row><td>SKINNED</td><td>DLL</td><td>Skinned</td><td/><td>0</td><td/></row>
		<row><td>SKINS</td><td>CONTENT</td><td>Skins</td><td/><td>0</td><td/></row>
		<row><td>SKINS1</td><td>ADVANCEDDEMOSCONTENT</td><td>Skins</td><td/><td>0</td><td/></row>
		<row><td>SONGS</td><td>CONTENT</td><td>Songs</td><td/><td>0</td><td/></row>
		<row><td>SONGS1</td><td>ADVANCEDDEMOSCONTENT</td><td>Songs</td><td/><td>0</td><td/></row>
		<row><td>STEERING</td><td>DEMO9___AI</td><td>Steering</td><td/><td>0</td><td/></row>
		<row><td>SendToFolder</td><td>TARGETDIR</td><td>.:SendTo</td><td/><td>3</td><td/></row>
		<row><td>StartMenuFolder</td><td>TARGETDIR</td><td>.:STARTM~1|Start Menu</td><td/><td>3</td><td/></row>
		<row><td>StartupFolder</td><td>TARGETDIR</td><td>.:StartUp</td><td/><td>3</td><td/></row>
		<row><td>System16Folder</td><td>TARGETDIR</td><td>.:System</td><td/><td>0</td><td/></row>
		<row><td>SystemFolder</td><td>TARGETDIR</td><td>.:System32</td><td/><td>0</td><td/></row>
		<row><td>TARGETDIR</td><td/><td>SourceDir</td><td/><td>0</td><td/></row>
		<row><td>TEMPLATES</td><td>VISUAL_STUDIO_2010</td><td>TEMPLA~1|Templates</td><td/><td>0</td><td/></row>
		<row><td>TEMPLATESCREENS</td><td>PLOOBSPROJECTTEMPLATE</td><td>TEMPLA~1|TemplateScreens</td><td/><td>0</td><td/></row>
		<row><td>TERRAIN</td><td>PLOOBSPROJECTTEMPLATECONTENT</td><td>Terrain</td><td/><td>0</td><td/></row>
		<row><td>TERRAIN1</td><td>GRAPHICSAPP1LIBRARYCONTENT</td><td>Terrain</td><td/><td>0</td><td/></row>
		<row><td>TEXTURES</td><td>CONTENT</td><td>Textures</td><td/><td>0</td><td/></row>
		<row><td>TEXTURES1</td><td>ADVANCEDDEMOSCONTENT</td><td>Textures</td><td/><td>0</td><td/></row>
		<row><td>TEXTURES2</td><td>INTRODUCTIONDEMO4.0CONTENT</td><td>Textures</td><td/><td>0</td><td/></row>
		<row><td>TEXTURES3</td><td>GRAPHICSAPP1LIBRARYCONTENT</td><td>Textures</td><td/><td>0</td><td/></row>
		<row><td>TEXTURES4</td><td>PLOOBSPROJECTTEMPLATECONTENT</td><td>Textures</td><td/><td>0</td><td/></row>
		<row><td>TRIGGER</td><td>DEMO9.8_PHYSX_2.8.X</td><td>Trigger</td><td/><td>0</td><td/></row>
		<row><td>TempFolder</td><td>TARGETDIR</td><td>.:Temp</td><td/><td>0</td><td/></row>
		<row><td>TemplateFolder</td><td>TARGETDIR</td><td>.:ShellNew</td><td/><td>0</td><td/></row>
		<row><td>USERPROFILE</td><td>TARGETDIR</td><td>.:USERPR~1|UserProfile</td><td/><td>0</td><td/></row>
		<row><td>VEHICLE</td><td>DEMO9.8_PHYSX_2.8.X</td><td>Vehicle</td><td/><td>0</td><td/></row>
		<row><td>VISUAL_STUDIO_2010</td><td>PersonalFolder</td><td>VISUAL~1|Visual Studio 2010</td><td/><td>0</td><td/></row>
		<row><td>WAYPOINTS</td><td>DEMO9___AI</td><td>WAYPOI~1|Waypoints</td><td/><td>0</td><td/></row>
		<row><td>WINDOWS_HIDEF</td><td>INSTALLDIR</td><td>WINDOW~1|Windows HiDef</td><td/><td>0</td><td/></row>
		<row><td>WINDOWS_PHONE_7</td><td>INSTALLDIR</td><td>WINDOW~1|Windows Phone 7</td><td/><td>0</td><td/></row>
		<row><td>WINDOWS_REACH</td><td>INSTALLDIR</td><td>WINDOW~1|Windows Reach</td><td/><td>0</td><td/></row>
		<row><td>WindowsFolder</td><td>TARGETDIR</td><td>.:Windows</td><td/><td>0</td><td/></row>
		<row><td>WindowsVolume</td><td>TARGETDIR</td><td>.:WinRoot</td><td/><td>0</td><td/></row>
		<row><td>_DS_MAX_.X_EXPORTER</td><td>INSTALLDIR</td><td>3DSMAX~1|3DS Max .X Exporter</td><td/><td>0</td><td/></row>
		<row><td>_DS_MAX_SCRIPTS</td><td>INSTALLDIR</td><td>3DSMAX~1|3DS Max Scripts</td><td/><td>0</td><td/></row>
		<row><td>_D_DEMOS</td><td>GRAPHICSAPP1LIBRARY</td><td>2DDEMO~1|2D Demos</td><td/><td>0</td><td/></row>
		<row><td>_D_DEMOS1</td><td>GRAPHICSAPP1LIBRARY</td><td>3DDEMO~1|3D Demos</td><td/><td>0</td><td/></row>
		<row><td>_D_DEMOS2</td><td>PLOOBSPROJECTTEMPLATE</td><td>2DDEMO~1|2D Demos</td><td/><td>0</td><td/></row>
		<row><td>ploobs</td><td>ProgramMenuFolder</td><td>Ploobs</td><td/><td>1</td><td/></row>
		<row><td>ploobs_1_ploobsengine</td><td>ploobs</td><td>PLOOBS~1|PloobsEngine</td><td/><td>1</td><td/></row>
	</table>

	<table name="DrLocator">
		<col key="yes" def="s72">Signature_</col>
		<col key="yes" def="S72">Parent</col>
		<col key="yes" def="S255">Path</col>
		<col def="I2">Depth</col>
	</table>

	<table name="DuplicateFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="s72">File_</col>
		<col def="L255">DestName</col>
		<col def="S72">DestFolder</col>
	</table>

	<table name="Environment">
		<col key="yes" def="s72">Environment</col>
		<col def="l255">Name</col>
		<col def="L255">Value</col>
		<col def="s72">Component_</col>
	</table>

	<table name="Error">
		<col key="yes" def="i2">Error</col>
		<col def="L255">Message</col>
		<row><td>0</td><td>##IDS_ERROR_0##</td></row>
		<row><td>1</td><td>##IDS_ERROR_1##</td></row>
		<row><td>10</td><td>##IDS_ERROR_8##</td></row>
		<row><td>11</td><td>##IDS_ERROR_9##</td></row>
		<row><td>1101</td><td>##IDS_ERROR_22##</td></row>
		<row><td>12</td><td>##IDS_ERROR_10##</td></row>
		<row><td>13</td><td>##IDS_ERROR_11##</td></row>
		<row><td>1301</td><td>##IDS_ERROR_23##</td></row>
		<row><td>1302</td><td>##IDS_ERROR_24##</td></row>
		<row><td>1303</td><td>##IDS_ERROR_25##</td></row>
		<row><td>1304</td><td>##IDS_ERROR_26##</td></row>
		<row><td>1305</td><td>##IDS_ERROR_27##</td></row>
		<row><td>1306</td><td>##IDS_ERROR_28##</td></row>
		<row><td>1307</td><td>##IDS_ERROR_29##</td></row>
		<row><td>1308</td><td>##IDS_ERROR_30##</td></row>
		<row><td>1309</td><td>##IDS_ERROR_31##</td></row>
		<row><td>1310</td><td>##IDS_ERROR_32##</td></row>
		<row><td>1311</td><td>##IDS_ERROR_33##</td></row>
		<row><td>1312</td><td>##IDS_ERROR_34##</td></row>
		<row><td>1313</td><td>##IDS_ERROR_35##</td></row>
		<row><td>1314</td><td>##IDS_ERROR_36##</td></row>
		<row><td>1315</td><td>##IDS_ERROR_37##</td></row>
		<row><td>1316</td><td>##IDS_ERROR_38##</td></row>
		<row><td>1317</td><td>##IDS_ERROR_39##</td></row>
		<row><td>1318</td><td>##IDS_ERROR_40##</td></row>
		<row><td>1319</td><td>##IDS_ERROR_41##</td></row>
		<row><td>1320</td><td>##IDS_ERROR_42##</td></row>
		<row><td>1321</td><td>##IDS_ERROR_43##</td></row>
		<row><td>1322</td><td>##IDS_ERROR_44##</td></row>
		<row><td>1323</td><td>##IDS_ERROR_45##</td></row>
		<row><td>1324</td><td>##IDS_ERROR_46##</td></row>
		<row><td>1325</td><td>##IDS_ERROR_47##</td></row>
		<row><td>1326</td><td>##IDS_ERROR_48##</td></row>
		<row><td>1327</td><td>##IDS_ERROR_49##</td></row>
		<row><td>1328</td><td>##IDS_ERROR_122##</td></row>
		<row><td>1329</td><td>##IDS_ERROR_1329##</td></row>
		<row><td>1330</td><td>##IDS_ERROR_1330##</td></row>
		<row><td>1331</td><td>##IDS_ERROR_1331##</td></row>
		<row><td>1332</td><td>##IDS_ERROR_1332##</td></row>
		<row><td>1333</td><td>##IDS_ERROR_1333##</td></row>
		<row><td>1334</td><td>##IDS_ERROR_1334##</td></row>
		<row><td>1335</td><td>##IDS_ERROR_1335##</td></row>
		<row><td>1336</td><td>##IDS_ERROR_1336##</td></row>
		<row><td>14</td><td>##IDS_ERROR_12##</td></row>
		<row><td>1401</td><td>##IDS_ERROR_50##</td></row>
		<row><td>1402</td><td>##IDS_ERROR_51##</td></row>
		<row><td>1403</td><td>##IDS_ERROR_52##</td></row>
		<row><td>1404</td><td>##IDS_ERROR_53##</td></row>
		<row><td>1405</td><td>##IDS_ERROR_54##</td></row>
		<row><td>1406</td><td>##IDS_ERROR_55##</td></row>
		<row><td>1407</td><td>##IDS_ERROR_56##</td></row>
		<row><td>1408</td><td>##IDS_ERROR_57##</td></row>
		<row><td>1409</td><td>##IDS_ERROR_58##</td></row>
		<row><td>1410</td><td>##IDS_ERROR_59##</td></row>
		<row><td>15</td><td>##IDS_ERROR_13##</td></row>
		<row><td>1500</td><td>##IDS_ERROR_60##</td></row>
		<row><td>1501</td><td>##IDS_ERROR_61##</td></row>
		<row><td>1502</td><td>##IDS_ERROR_62##</td></row>
		<row><td>1503</td><td>##IDS_ERROR_63##</td></row>
		<row><td>16</td><td>##IDS_ERROR_14##</td></row>
		<row><td>1601</td><td>##IDS_ERROR_64##</td></row>
		<row><td>1602</td><td>##IDS_ERROR_65##</td></row>
		<row><td>1603</td><td>##IDS_ERROR_66##</td></row>
		<row><td>1604</td><td>##IDS_ERROR_67##</td></row>
		<row><td>1605</td><td>##IDS_ERROR_68##</td></row>
		<row><td>1606</td><td>##IDS_ERROR_69##</td></row>
		<row><td>1607</td><td>##IDS_ERROR_70##</td></row>
		<row><td>1608</td><td>##IDS_ERROR_71##</td></row>
		<row><td>1609</td><td>##IDS_ERROR_1609##</td></row>
		<row><td>1651</td><td>##IDS_ERROR_1651##</td></row>
		<row><td>17</td><td>##IDS_ERROR_15##</td></row>
		<row><td>1701</td><td>##IDS_ERROR_72##</td></row>
		<row><td>1702</td><td>##IDS_ERROR_73##</td></row>
		<row><td>1703</td><td>##IDS_ERROR_74##</td></row>
		<row><td>1704</td><td>##IDS_ERROR_75##</td></row>
		<row><td>1705</td><td>##IDS_ERROR_76##</td></row>
		<row><td>1706</td><td>##IDS_ERROR_77##</td></row>
		<row><td>1707</td><td>##IDS_ERROR_78##</td></row>
		<row><td>1708</td><td>##IDS_ERROR_79##</td></row>
		<row><td>1709</td><td>##IDS_ERROR_80##</td></row>
		<row><td>1710</td><td>##IDS_ERROR_81##</td></row>
		<row><td>1711</td><td>##IDS_ERROR_82##</td></row>
		<row><td>1712</td><td>##IDS_ERROR_83##</td></row>
		<row><td>1713</td><td>##IDS_ERROR_123##</td></row>
		<row><td>1714</td><td>##IDS_ERROR_124##</td></row>
		<row><td>1715</td><td>##IDS_ERROR_1715##</td></row>
		<row><td>1716</td><td>##IDS_ERROR_1716##</td></row>
		<row><td>1717</td><td>##IDS_ERROR_1717##</td></row>
		<row><td>1718</td><td>##IDS_ERROR_1718##</td></row>
		<row><td>1719</td><td>##IDS_ERROR_1719##</td></row>
		<row><td>1720</td><td>##IDS_ERROR_1720##</td></row>
		<row><td>1721</td><td>##IDS_ERROR_1721##</td></row>
		<row><td>1722</td><td>##IDS_ERROR_1722##</td></row>
		<row><td>1723</td><td>##IDS_ERROR_1723##</td></row>
		<row><td>1724</td><td>##IDS_ERROR_1724##</td></row>
		<row><td>1725</td><td>##IDS_ERROR_1725##</td></row>
		<row><td>1726</td><td>##IDS_ERROR_1726##</td></row>
		<row><td>1727</td><td>##IDS_ERROR_1727##</td></row>
		<row><td>1728</td><td>##IDS_ERROR_1728##</td></row>
		<row><td>1729</td><td>##IDS_ERROR_1729##</td></row>
		<row><td>1730</td><td>##IDS_ERROR_1730##</td></row>
		<row><td>1731</td><td>##IDS_ERROR_1731##</td></row>
		<row><td>1732</td><td>##IDS_ERROR_1732##</td></row>
		<row><td>18</td><td>##IDS_ERROR_16##</td></row>
		<row><td>1801</td><td>##IDS_ERROR_84##</td></row>
		<row><td>1802</td><td>##IDS_ERROR_85##</td></row>
		<row><td>1803</td><td>##IDS_ERROR_86##</td></row>
		<row><td>1804</td><td>##IDS_ERROR_87##</td></row>
		<row><td>1805</td><td>##IDS_ERROR_88##</td></row>
		<row><td>1806</td><td>##IDS_ERROR_89##</td></row>
		<row><td>1807</td><td>##IDS_ERROR_90##</td></row>
		<row><td>19</td><td>##IDS_ERROR_17##</td></row>
		<row><td>1901</td><td>##IDS_ERROR_91##</td></row>
		<row><td>1902</td><td>##IDS_ERROR_92##</td></row>
		<row><td>1903</td><td>##IDS_ERROR_93##</td></row>
		<row><td>1904</td><td>##IDS_ERROR_94##</td></row>
		<row><td>1905</td><td>##IDS_ERROR_95##</td></row>
		<row><td>1906</td><td>##IDS_ERROR_96##</td></row>
		<row><td>1907</td><td>##IDS_ERROR_97##</td></row>
		<row><td>1908</td><td>##IDS_ERROR_98##</td></row>
		<row><td>1909</td><td>##IDS_ERROR_99##</td></row>
		<row><td>1910</td><td>##IDS_ERROR_100##</td></row>
		<row><td>1911</td><td>##IDS_ERROR_101##</td></row>
		<row><td>1912</td><td>##IDS_ERROR_102##</td></row>
		<row><td>1913</td><td>##IDS_ERROR_103##</td></row>
		<row><td>1914</td><td>##IDS_ERROR_104##</td></row>
		<row><td>1915</td><td>##IDS_ERROR_105##</td></row>
		<row><td>1916</td><td>##IDS_ERROR_106##</td></row>
		<row><td>1917</td><td>##IDS_ERROR_107##</td></row>
		<row><td>1918</td><td>##IDS_ERROR_108##</td></row>
		<row><td>1919</td><td>##IDS_ERROR_109##</td></row>
		<row><td>1920</td><td>##IDS_ERROR_110##</td></row>
		<row><td>1921</td><td>##IDS_ERROR_111##</td></row>
		<row><td>1922</td><td>##IDS_ERROR_112##</td></row>
		<row><td>1923</td><td>##IDS_ERROR_113##</td></row>
		<row><td>1924</td><td>##IDS_ERROR_114##</td></row>
		<row><td>1925</td><td>##IDS_ERROR_115##</td></row>
		<row><td>1926</td><td>##IDS_ERROR_116##</td></row>
		<row><td>1927</td><td>##IDS_ERROR_117##</td></row>
		<row><td>1928</td><td>##IDS_ERROR_118##</td></row>
		<row><td>1929</td><td>##IDS_ERROR_119##</td></row>
		<row><td>1930</td><td>##IDS_ERROR_125##</td></row>
		<row><td>1931</td><td>##IDS_ERROR_126##</td></row>
		<row><td>1932</td><td>##IDS_ERROR_127##</td></row>
		<row><td>1933</td><td>##IDS_ERROR_128##</td></row>
		<row><td>1934</td><td>##IDS_ERROR_129##</td></row>
		<row><td>1935</td><td>##IDS_ERROR_1935##</td></row>
		<row><td>1936</td><td>##IDS_ERROR_1936##</td></row>
		<row><td>1937</td><td>##IDS_ERROR_1937##</td></row>
		<row><td>1938</td><td>##IDS_ERROR_1938##</td></row>
		<row><td>2</td><td>##IDS_ERROR_2##</td></row>
		<row><td>20</td><td>##IDS_ERROR_18##</td></row>
		<row><td>21</td><td>##IDS_ERROR_19##</td></row>
		<row><td>2101</td><td>##IDS_ERROR_2101##</td></row>
		<row><td>2102</td><td>##IDS_ERROR_2102##</td></row>
		<row><td>2103</td><td>##IDS_ERROR_2103##</td></row>
		<row><td>2104</td><td>##IDS_ERROR_2104##</td></row>
		<row><td>2105</td><td>##IDS_ERROR_2105##</td></row>
		<row><td>2106</td><td>##IDS_ERROR_2106##</td></row>
		<row><td>2107</td><td>##IDS_ERROR_2107##</td></row>
		<row><td>2108</td><td>##IDS_ERROR_2108##</td></row>
		<row><td>2109</td><td>##IDS_ERROR_2109##</td></row>
		<row><td>2110</td><td>##IDS_ERROR_2110##</td></row>
		<row><td>2111</td><td>##IDS_ERROR_2111##</td></row>
		<row><td>2112</td><td>##IDS_ERROR_2112##</td></row>
		<row><td>2113</td><td>##IDS_ERROR_2113##</td></row>
		<row><td>22</td><td>##IDS_ERROR_120##</td></row>
		<row><td>2200</td><td>##IDS_ERROR_2200##</td></row>
		<row><td>2201</td><td>##IDS_ERROR_2201##</td></row>
		<row><td>2202</td><td>##IDS_ERROR_2202##</td></row>
		<row><td>2203</td><td>##IDS_ERROR_2203##</td></row>
		<row><td>2204</td><td>##IDS_ERROR_2204##</td></row>
		<row><td>2205</td><td>##IDS_ERROR_2205##</td></row>
		<row><td>2206</td><td>##IDS_ERROR_2206##</td></row>
		<row><td>2207</td><td>##IDS_ERROR_2207##</td></row>
		<row><td>2208</td><td>##IDS_ERROR_2208##</td></row>
		<row><td>2209</td><td>##IDS_ERROR_2209##</td></row>
		<row><td>2210</td><td>##IDS_ERROR_2210##</td></row>
		<row><td>2211</td><td>##IDS_ERROR_2211##</td></row>
		<row><td>2212</td><td>##IDS_ERROR_2212##</td></row>
		<row><td>2213</td><td>##IDS_ERROR_2213##</td></row>
		<row><td>2214</td><td>##IDS_ERROR_2214##</td></row>
		<row><td>2215</td><td>##IDS_ERROR_2215##</td></row>
		<row><td>2216</td><td>##IDS_ERROR_2216##</td></row>
		<row><td>2217</td><td>##IDS_ERROR_2217##</td></row>
		<row><td>2218</td><td>##IDS_ERROR_2218##</td></row>
		<row><td>2219</td><td>##IDS_ERROR_2219##</td></row>
		<row><td>2220</td><td>##IDS_ERROR_2220##</td></row>
		<row><td>2221</td><td>##IDS_ERROR_2221##</td></row>
		<row><td>2222</td><td>##IDS_ERROR_2222##</td></row>
		<row><td>2223</td><td>##IDS_ERROR_2223##</td></row>
		<row><td>2224</td><td>##IDS_ERROR_2224##</td></row>
		<row><td>2225</td><td>##IDS_ERROR_2225##</td></row>
		<row><td>2226</td><td>##IDS_ERROR_2226##</td></row>
		<row><td>2227</td><td>##IDS_ERROR_2227##</td></row>
		<row><td>2228</td><td>##IDS_ERROR_2228##</td></row>
		<row><td>2229</td><td>##IDS_ERROR_2229##</td></row>
		<row><td>2230</td><td>##IDS_ERROR_2230##</td></row>
		<row><td>2231</td><td>##IDS_ERROR_2231##</td></row>
		<row><td>2232</td><td>##IDS_ERROR_2232##</td></row>
		<row><td>2233</td><td>##IDS_ERROR_2233##</td></row>
		<row><td>2234</td><td>##IDS_ERROR_2234##</td></row>
		<row><td>2235</td><td>##IDS_ERROR_2235##</td></row>
		<row><td>2236</td><td>##IDS_ERROR_2236##</td></row>
		<row><td>2237</td><td>##IDS_ERROR_2237##</td></row>
		<row><td>2238</td><td>##IDS_ERROR_2238##</td></row>
		<row><td>2239</td><td>##IDS_ERROR_2239##</td></row>
		<row><td>2240</td><td>##IDS_ERROR_2240##</td></row>
		<row><td>2241</td><td>##IDS_ERROR_2241##</td></row>
		<row><td>2242</td><td>##IDS_ERROR_2242##</td></row>
		<row><td>2243</td><td>##IDS_ERROR_2243##</td></row>
		<row><td>2244</td><td>##IDS_ERROR_2244##</td></row>
		<row><td>2245</td><td>##IDS_ERROR_2245##</td></row>
		<row><td>2246</td><td>##IDS_ERROR_2246##</td></row>
		<row><td>2247</td><td>##IDS_ERROR_2247##</td></row>
		<row><td>2248</td><td>##IDS_ERROR_2248##</td></row>
		<row><td>2249</td><td>##IDS_ERROR_2249##</td></row>
		<row><td>2250</td><td>##IDS_ERROR_2250##</td></row>
		<row><td>2251</td><td>##IDS_ERROR_2251##</td></row>
		<row><td>2252</td><td>##IDS_ERROR_2252##</td></row>
		<row><td>2253</td><td>##IDS_ERROR_2253##</td></row>
		<row><td>2254</td><td>##IDS_ERROR_2254##</td></row>
		<row><td>2255</td><td>##IDS_ERROR_2255##</td></row>
		<row><td>2256</td><td>##IDS_ERROR_2256##</td></row>
		<row><td>2257</td><td>##IDS_ERROR_2257##</td></row>
		<row><td>2258</td><td>##IDS_ERROR_2258##</td></row>
		<row><td>2259</td><td>##IDS_ERROR_2259##</td></row>
		<row><td>2260</td><td>##IDS_ERROR_2260##</td></row>
		<row><td>2261</td><td>##IDS_ERROR_2261##</td></row>
		<row><td>2262</td><td>##IDS_ERROR_2262##</td></row>
		<row><td>2263</td><td>##IDS_ERROR_2263##</td></row>
		<row><td>2264</td><td>##IDS_ERROR_2264##</td></row>
		<row><td>2265</td><td>##IDS_ERROR_2265##</td></row>
		<row><td>2266</td><td>##IDS_ERROR_2266##</td></row>
		<row><td>2267</td><td>##IDS_ERROR_2267##</td></row>
		<row><td>2268</td><td>##IDS_ERROR_2268##</td></row>
		<row><td>2269</td><td>##IDS_ERROR_2269##</td></row>
		<row><td>2270</td><td>##IDS_ERROR_2270##</td></row>
		<row><td>2271</td><td>##IDS_ERROR_2271##</td></row>
		<row><td>2272</td><td>##IDS_ERROR_2272##</td></row>
		<row><td>2273</td><td>##IDS_ERROR_2273##</td></row>
		<row><td>2274</td><td>##IDS_ERROR_2274##</td></row>
		<row><td>2275</td><td>##IDS_ERROR_2275##</td></row>
		<row><td>2276</td><td>##IDS_ERROR_2276##</td></row>
		<row><td>2277</td><td>##IDS_ERROR_2277##</td></row>
		<row><td>2278</td><td>##IDS_ERROR_2278##</td></row>
		<row><td>2279</td><td>##IDS_ERROR_2279##</td></row>
		<row><td>2280</td><td>##IDS_ERROR_2280##</td></row>
		<row><td>2281</td><td>##IDS_ERROR_2281##</td></row>
		<row><td>2282</td><td>##IDS_ERROR_2282##</td></row>
		<row><td>23</td><td>##IDS_ERROR_121##</td></row>
		<row><td>2302</td><td>##IDS_ERROR_2302##</td></row>
		<row><td>2303</td><td>##IDS_ERROR_2303##</td></row>
		<row><td>2304</td><td>##IDS_ERROR_2304##</td></row>
		<row><td>2305</td><td>##IDS_ERROR_2305##</td></row>
		<row><td>2306</td><td>##IDS_ERROR_2306##</td></row>
		<row><td>2307</td><td>##IDS_ERROR_2307##</td></row>
		<row><td>2308</td><td>##IDS_ERROR_2308##</td></row>
		<row><td>2309</td><td>##IDS_ERROR_2309##</td></row>
		<row><td>2310</td><td>##IDS_ERROR_2310##</td></row>
		<row><td>2315</td><td>##IDS_ERROR_2315##</td></row>
		<row><td>2318</td><td>##IDS_ERROR_2318##</td></row>
		<row><td>2319</td><td>##IDS_ERROR_2319##</td></row>
		<row><td>2320</td><td>##IDS_ERROR_2320##</td></row>
		<row><td>2321</td><td>##IDS_ERROR_2321##</td></row>
		<row><td>2322</td><td>##IDS_ERROR_2322##</td></row>
		<row><td>2323</td><td>##IDS_ERROR_2323##</td></row>
		<row><td>2324</td><td>##IDS_ERROR_2324##</td></row>
		<row><td>2325</td><td>##IDS_ERROR_2325##</td></row>
		<row><td>2326</td><td>##IDS_ERROR_2326##</td></row>
		<row><td>2327</td><td>##IDS_ERROR_2327##</td></row>
		<row><td>2328</td><td>##IDS_ERROR_2328##</td></row>
		<row><td>2329</td><td>##IDS_ERROR_2329##</td></row>
		<row><td>2330</td><td>##IDS_ERROR_2330##</td></row>
		<row><td>2331</td><td>##IDS_ERROR_2331##</td></row>
		<row><td>2332</td><td>##IDS_ERROR_2332##</td></row>
		<row><td>2333</td><td>##IDS_ERROR_2333##</td></row>
		<row><td>2334</td><td>##IDS_ERROR_2334##</td></row>
		<row><td>2335</td><td>##IDS_ERROR_2335##</td></row>
		<row><td>2336</td><td>##IDS_ERROR_2336##</td></row>
		<row><td>2337</td><td>##IDS_ERROR_2337##</td></row>
		<row><td>2338</td><td>##IDS_ERROR_2338##</td></row>
		<row><td>2339</td><td>##IDS_ERROR_2339##</td></row>
		<row><td>2340</td><td>##IDS_ERROR_2340##</td></row>
		<row><td>2341</td><td>##IDS_ERROR_2341##</td></row>
		<row><td>2342</td><td>##IDS_ERROR_2342##</td></row>
		<row><td>2343</td><td>##IDS_ERROR_2343##</td></row>
		<row><td>2344</td><td>##IDS_ERROR_2344##</td></row>
		<row><td>2345</td><td>##IDS_ERROR_2345##</td></row>
		<row><td>2347</td><td>##IDS_ERROR_2347##</td></row>
		<row><td>2348</td><td>##IDS_ERROR_2348##</td></row>
		<row><td>2349</td><td>##IDS_ERROR_2349##</td></row>
		<row><td>2350</td><td>##IDS_ERROR_2350##</td></row>
		<row><td>2351</td><td>##IDS_ERROR_2351##</td></row>
		<row><td>2352</td><td>##IDS_ERROR_2352##</td></row>
		<row><td>2353</td><td>##IDS_ERROR_2353##</td></row>
		<row><td>2354</td><td>##IDS_ERROR_2354##</td></row>
		<row><td>2355</td><td>##IDS_ERROR_2355##</td></row>
		<row><td>2356</td><td>##IDS_ERROR_2356##</td></row>
		<row><td>2357</td><td>##IDS_ERROR_2357##</td></row>
		<row><td>2358</td><td>##IDS_ERROR_2358##</td></row>
		<row><td>2359</td><td>##IDS_ERROR_2359##</td></row>
		<row><td>2360</td><td>##IDS_ERROR_2360##</td></row>
		<row><td>2361</td><td>##IDS_ERROR_2361##</td></row>
		<row><td>2362</td><td>##IDS_ERROR_2362##</td></row>
		<row><td>2363</td><td>##IDS_ERROR_2363##</td></row>
		<row><td>2364</td><td>##IDS_ERROR_2364##</td></row>
		<row><td>2365</td><td>##IDS_ERROR_2365##</td></row>
		<row><td>2366</td><td>##IDS_ERROR_2366##</td></row>
		<row><td>2367</td><td>##IDS_ERROR_2367##</td></row>
		<row><td>2368</td><td>##IDS_ERROR_2368##</td></row>
		<row><td>2370</td><td>##IDS_ERROR_2370##</td></row>
		<row><td>2371</td><td>##IDS_ERROR_2371##</td></row>
		<row><td>2372</td><td>##IDS_ERROR_2372##</td></row>
		<row><td>2373</td><td>##IDS_ERROR_2373##</td></row>
		<row><td>2374</td><td>##IDS_ERROR_2374##</td></row>
		<row><td>2375</td><td>##IDS_ERROR_2375##</td></row>
		<row><td>2376</td><td>##IDS_ERROR_2376##</td></row>
		<row><td>2379</td><td>##IDS_ERROR_2379##</td></row>
		<row><td>2380</td><td>##IDS_ERROR_2380##</td></row>
		<row><td>2381</td><td>##IDS_ERROR_2381##</td></row>
		<row><td>2382</td><td>##IDS_ERROR_2382##</td></row>
		<row><td>2401</td><td>##IDS_ERROR_2401##</td></row>
		<row><td>2402</td><td>##IDS_ERROR_2402##</td></row>
		<row><td>2501</td><td>##IDS_ERROR_2501##</td></row>
		<row><td>2502</td><td>##IDS_ERROR_2502##</td></row>
		<row><td>2503</td><td>##IDS_ERROR_2503##</td></row>
		<row><td>2601</td><td>##IDS_ERROR_2601##</td></row>
		<row><td>2602</td><td>##IDS_ERROR_2602##</td></row>
		<row><td>2603</td><td>##IDS_ERROR_2603##</td></row>
		<row><td>2604</td><td>##IDS_ERROR_2604##</td></row>
		<row><td>2605</td><td>##IDS_ERROR_2605##</td></row>
		<row><td>2606</td><td>##IDS_ERROR_2606##</td></row>
		<row><td>2607</td><td>##IDS_ERROR_2607##</td></row>
		<row><td>2608</td><td>##IDS_ERROR_2608##</td></row>
		<row><td>2609</td><td>##IDS_ERROR_2609##</td></row>
		<row><td>2611</td><td>##IDS_ERROR_2611##</td></row>
		<row><td>2612</td><td>##IDS_ERROR_2612##</td></row>
		<row><td>2613</td><td>##IDS_ERROR_2613##</td></row>
		<row><td>2614</td><td>##IDS_ERROR_2614##</td></row>
		<row><td>2615</td><td>##IDS_ERROR_2615##</td></row>
		<row><td>2616</td><td>##IDS_ERROR_2616##</td></row>
		<row><td>2617</td><td>##IDS_ERROR_2617##</td></row>
		<row><td>2618</td><td>##IDS_ERROR_2618##</td></row>
		<row><td>2619</td><td>##IDS_ERROR_2619##</td></row>
		<row><td>2620</td><td>##IDS_ERROR_2620##</td></row>
		<row><td>2621</td><td>##IDS_ERROR_2621##</td></row>
		<row><td>2701</td><td>##IDS_ERROR_2701##</td></row>
		<row><td>2702</td><td>##IDS_ERROR_2702##</td></row>
		<row><td>2703</td><td>##IDS_ERROR_2703##</td></row>
		<row><td>2704</td><td>##IDS_ERROR_2704##</td></row>
		<row><td>2705</td><td>##IDS_ERROR_2705##</td></row>
		<row><td>2706</td><td>##IDS_ERROR_2706##</td></row>
		<row><td>2707</td><td>##IDS_ERROR_2707##</td></row>
		<row><td>2708</td><td>##IDS_ERROR_2708##</td></row>
		<row><td>2709</td><td>##IDS_ERROR_2709##</td></row>
		<row><td>2710</td><td>##IDS_ERROR_2710##</td></row>
		<row><td>2711</td><td>##IDS_ERROR_2711##</td></row>
		<row><td>2712</td><td>##IDS_ERROR_2712##</td></row>
		<row><td>2713</td><td>##IDS_ERROR_2713##</td></row>
		<row><td>2714</td><td>##IDS_ERROR_2714##</td></row>
		<row><td>2715</td><td>##IDS_ERROR_2715##</td></row>
		<row><td>2716</td><td>##IDS_ERROR_2716##</td></row>
		<row><td>2717</td><td>##IDS_ERROR_2717##</td></row>
		<row><td>2718</td><td>##IDS_ERROR_2718##</td></row>
		<row><td>2719</td><td>##IDS_ERROR_2719##</td></row>
		<row><td>2720</td><td>##IDS_ERROR_2720##</td></row>
		<row><td>2721</td><td>##IDS_ERROR_2721##</td></row>
		<row><td>2722</td><td>##IDS_ERROR_2722##</td></row>
		<row><td>2723</td><td>##IDS_ERROR_2723##</td></row>
		<row><td>2724</td><td>##IDS_ERROR_2724##</td></row>
		<row><td>2725</td><td>##IDS_ERROR_2725##</td></row>
		<row><td>2726</td><td>##IDS_ERROR_2726##</td></row>
		<row><td>2727</td><td>##IDS_ERROR_2727##</td></row>
		<row><td>2728</td><td>##IDS_ERROR_2728##</td></row>
		<row><td>2729</td><td>##IDS_ERROR_2729##</td></row>
		<row><td>2730</td><td>##IDS_ERROR_2730##</td></row>
		<row><td>2731</td><td>##IDS_ERROR_2731##</td></row>
		<row><td>2732</td><td>##IDS_ERROR_2732##</td></row>
		<row><td>2733</td><td>##IDS_ERROR_2733##</td></row>
		<row><td>2734</td><td>##IDS_ERROR_2734##</td></row>
		<row><td>2735</td><td>##IDS_ERROR_2735##</td></row>
		<row><td>2736</td><td>##IDS_ERROR_2736##</td></row>
		<row><td>2737</td><td>##IDS_ERROR_2737##</td></row>
		<row><td>2738</td><td>##IDS_ERROR_2738##</td></row>
		<row><td>2739</td><td>##IDS_ERROR_2739##</td></row>
		<row><td>2740</td><td>##IDS_ERROR_2740##</td></row>
		<row><td>2741</td><td>##IDS_ERROR_2741##</td></row>
		<row><td>2742</td><td>##IDS_ERROR_2742##</td></row>
		<row><td>2743</td><td>##IDS_ERROR_2743##</td></row>
		<row><td>2744</td><td>##IDS_ERROR_2744##</td></row>
		<row><td>2745</td><td>##IDS_ERROR_2745##</td></row>
		<row><td>2746</td><td>##IDS_ERROR_2746##</td></row>
		<row><td>2747</td><td>##IDS_ERROR_2747##</td></row>
		<row><td>2748</td><td>##IDS_ERROR_2748##</td></row>
		<row><td>2749</td><td>##IDS_ERROR_2749##</td></row>
		<row><td>2750</td><td>##IDS_ERROR_2750##</td></row>
		<row><td>27500</td><td>##IDS_ERROR_130##</td></row>
		<row><td>27501</td><td>##IDS_ERROR_131##</td></row>
		<row><td>27502</td><td>##IDS_ERROR_27502##</td></row>
		<row><td>27503</td><td>##IDS_ERROR_27503##</td></row>
		<row><td>27504</td><td>##IDS_ERROR_27504##</td></row>
		<row><td>27505</td><td>##IDS_ERROR_27505##</td></row>
		<row><td>27506</td><td>##IDS_ERROR_27506##</td></row>
		<row><td>27507</td><td>##IDS_ERROR_27507##</td></row>
		<row><td>27508</td><td>##IDS_ERROR_27508##</td></row>
		<row><td>27509</td><td>##IDS_ERROR_27509##</td></row>
		<row><td>2751</td><td>##IDS_ERROR_2751##</td></row>
		<row><td>27510</td><td>##IDS_ERROR_27510##</td></row>
		<row><td>27511</td><td>##IDS_ERROR_27511##</td></row>
		<row><td>27512</td><td>##IDS_ERROR_27512##</td></row>
		<row><td>27513</td><td>##IDS_ERROR_27513##</td></row>
		<row><td>27514</td><td>##IDS_ERROR_27514##</td></row>
		<row><td>27515</td><td>##IDS_ERROR_27515##</td></row>
		<row><td>27516</td><td>##IDS_ERROR_27516##</td></row>
		<row><td>27517</td><td>##IDS_ERROR_27517##</td></row>
		<row><td>27518</td><td>##IDS_ERROR_27518##</td></row>
		<row><td>27519</td><td>##IDS_ERROR_27519##</td></row>
		<row><td>2752</td><td>##IDS_ERROR_2752##</td></row>
		<row><td>27520</td><td>##IDS_ERROR_27520##</td></row>
		<row><td>27521</td><td>##IDS_ERROR_27521##</td></row>
		<row><td>27522</td><td>##IDS_ERROR_27522##</td></row>
		<row><td>27523</td><td>##IDS_ERROR_27523##</td></row>
		<row><td>27524</td><td>##IDS_ERROR_27524##</td></row>
		<row><td>27525</td><td>##IDS_ERROR_27525##</td></row>
		<row><td>27526</td><td>##IDS_ERROR_27526##</td></row>
		<row><td>27527</td><td>##IDS_ERROR_27527##</td></row>
		<row><td>27528</td><td>##IDS_ERROR_27528##</td></row>
		<row><td>27529</td><td>##IDS_ERROR_27529##</td></row>
		<row><td>2753</td><td>##IDS_ERROR_2753##</td></row>
		<row><td>27530</td><td>##IDS_ERROR_27530##</td></row>
		<row><td>27531</td><td>##IDS_ERROR_27531##</td></row>
		<row><td>27532</td><td>##IDS_ERROR_27532##</td></row>
		<row><td>27533</td><td>##IDS_ERROR_27533##</td></row>
		<row><td>27534</td><td>##IDS_ERROR_27534##</td></row>
		<row><td>27535</td><td>##IDS_ERROR_27535##</td></row>
		<row><td>27536</td><td>##IDS_ERROR_27536##</td></row>
		<row><td>27537</td><td>##IDS_ERROR_27537##</td></row>
		<row><td>27538</td><td>##IDS_ERROR_27538##</td></row>
		<row><td>27539</td><td>##IDS_ERROR_27539##</td></row>
		<row><td>2754</td><td>##IDS_ERROR_2754##</td></row>
		<row><td>27540</td><td>##IDS_ERROR_27540##</td></row>
		<row><td>27541</td><td>##IDS_ERROR_27541##</td></row>
		<row><td>27542</td><td>##IDS_ERROR_27542##</td></row>
		<row><td>27543</td><td>##IDS_ERROR_27543##</td></row>
		<row><td>27544</td><td>##IDS_ERROR_27544##</td></row>
		<row><td>27545</td><td>##IDS_ERROR_27545##</td></row>
		<row><td>27546</td><td>##IDS_ERROR_27546##</td></row>
		<row><td>27547</td><td>##IDS_ERROR_27547##</td></row>
		<row><td>27548</td><td>##IDS_ERROR_27548##</td></row>
		<row><td>27549</td><td>##IDS_ERROR_27549##</td></row>
		<row><td>2755</td><td>##IDS_ERROR_2755##</td></row>
		<row><td>27550</td><td>##IDS_ERROR_27550##</td></row>
		<row><td>27551</td><td>##IDS_ERROR_27551##</td></row>
		<row><td>27552</td><td>##IDS_ERROR_27552##</td></row>
		<row><td>27553</td><td>##IDS_ERROR_27553##</td></row>
		<row><td>27554</td><td>##IDS_ERROR_27554##</td></row>
		<row><td>27555</td><td>##IDS_ERROR_27555##</td></row>
		<row><td>2756</td><td>##IDS_ERROR_2756##</td></row>
		<row><td>2757</td><td>##IDS_ERROR_2757##</td></row>
		<row><td>2758</td><td>##IDS_ERROR_2758##</td></row>
		<row><td>2759</td><td>##IDS_ERROR_2759##</td></row>
		<row><td>2760</td><td>##IDS_ERROR_2760##</td></row>
		<row><td>2761</td><td>##IDS_ERROR_2761##</td></row>
		<row><td>2762</td><td>##IDS_ERROR_2762##</td></row>
		<row><td>2763</td><td>##IDS_ERROR_2763##</td></row>
		<row><td>2765</td><td>##IDS_ERROR_2765##</td></row>
		<row><td>2766</td><td>##IDS_ERROR_2766##</td></row>
		<row><td>2767</td><td>##IDS_ERROR_2767##</td></row>
		<row><td>2768</td><td>##IDS_ERROR_2768##</td></row>
		<row><td>2769</td><td>##IDS_ERROR_2769##</td></row>
		<row><td>2770</td><td>##IDS_ERROR_2770##</td></row>
		<row><td>2771</td><td>##IDS_ERROR_2771##</td></row>
		<row><td>2772</td><td>##IDS_ERROR_2772##</td></row>
		<row><td>2801</td><td>##IDS_ERROR_2801##</td></row>
		<row><td>2802</td><td>##IDS_ERROR_2802##</td></row>
		<row><td>2803</td><td>##IDS_ERROR_2803##</td></row>
		<row><td>2804</td><td>##IDS_ERROR_2804##</td></row>
		<row><td>2806</td><td>##IDS_ERROR_2806##</td></row>
		<row><td>2807</td><td>##IDS_ERROR_2807##</td></row>
		<row><td>2808</td><td>##IDS_ERROR_2808##</td></row>
		<row><td>2809</td><td>##IDS_ERROR_2809##</td></row>
		<row><td>2810</td><td>##IDS_ERROR_2810##</td></row>
		<row><td>2811</td><td>##IDS_ERROR_2811##</td></row>
		<row><td>2812</td><td>##IDS_ERROR_2812##</td></row>
		<row><td>2813</td><td>##IDS_ERROR_2813##</td></row>
		<row><td>2814</td><td>##IDS_ERROR_2814##</td></row>
		<row><td>2815</td><td>##IDS_ERROR_2815##</td></row>
		<row><td>2816</td><td>##IDS_ERROR_2816##</td></row>
		<row><td>2817</td><td>##IDS_ERROR_2817##</td></row>
		<row><td>2818</td><td>##IDS_ERROR_2818##</td></row>
		<row><td>2819</td><td>##IDS_ERROR_2819##</td></row>
		<row><td>2820</td><td>##IDS_ERROR_2820##</td></row>
		<row><td>2821</td><td>##IDS_ERROR_2821##</td></row>
		<row><td>2822</td><td>##IDS_ERROR_2822##</td></row>
		<row><td>2823</td><td>##IDS_ERROR_2823##</td></row>
		<row><td>2824</td><td>##IDS_ERROR_2824##</td></row>
		<row><td>2825</td><td>##IDS_ERROR_2825##</td></row>
		<row><td>2826</td><td>##IDS_ERROR_2826##</td></row>
		<row><td>2827</td><td>##IDS_ERROR_2827##</td></row>
		<row><td>2828</td><td>##IDS_ERROR_2828##</td></row>
		<row><td>2829</td><td>##IDS_ERROR_2829##</td></row>
		<row><td>2830</td><td>##IDS_ERROR_2830##</td></row>
		<row><td>2831</td><td>##IDS_ERROR_2831##</td></row>
		<row><td>2832</td><td>##IDS_ERROR_2832##</td></row>
		<row><td>2833</td><td>##IDS_ERROR_2833##</td></row>
		<row><td>2834</td><td>##IDS_ERROR_2834##</td></row>
		<row><td>2835</td><td>##IDS_ERROR_2835##</td></row>
		<row><td>2836</td><td>##IDS_ERROR_2836##</td></row>
		<row><td>2837</td><td>##IDS_ERROR_2837##</td></row>
		<row><td>2838</td><td>##IDS_ERROR_2838##</td></row>
		<row><td>2839</td><td>##IDS_ERROR_2839##</td></row>
		<row><td>2840</td><td>##IDS_ERROR_2840##</td></row>
		<row><td>2841</td><td>##IDS_ERROR_2841##</td></row>
		<row><td>2842</td><td>##IDS_ERROR_2842##</td></row>
		<row><td>2843</td><td>##IDS_ERROR_2843##</td></row>
		<row><td>2844</td><td>##IDS_ERROR_2844##</td></row>
		<row><td>2845</td><td>##IDS_ERROR_2845##</td></row>
		<row><td>2846</td><td>##IDS_ERROR_2846##</td></row>
		<row><td>2847</td><td>##IDS_ERROR_2847##</td></row>
		<row><td>2848</td><td>##IDS_ERROR_2848##</td></row>
		<row><td>2849</td><td>##IDS_ERROR_2849##</td></row>
		<row><td>2850</td><td>##IDS_ERROR_2850##</td></row>
		<row><td>2851</td><td>##IDS_ERROR_2851##</td></row>
		<row><td>2852</td><td>##IDS_ERROR_2852##</td></row>
		<row><td>2853</td><td>##IDS_ERROR_2853##</td></row>
		<row><td>2854</td><td>##IDS_ERROR_2854##</td></row>
		<row><td>2855</td><td>##IDS_ERROR_2855##</td></row>
		<row><td>2856</td><td>##IDS_ERROR_2856##</td></row>
		<row><td>2857</td><td>##IDS_ERROR_2857##</td></row>
		<row><td>2858</td><td>##IDS_ERROR_2858##</td></row>
		<row><td>2859</td><td>##IDS_ERROR_2859##</td></row>
		<row><td>2860</td><td>##IDS_ERROR_2860##</td></row>
		<row><td>2861</td><td>##IDS_ERROR_2861##</td></row>
		<row><td>2862</td><td>##IDS_ERROR_2862##</td></row>
		<row><td>2863</td><td>##IDS_ERROR_2863##</td></row>
		<row><td>2864</td><td>##IDS_ERROR_2864##</td></row>
		<row><td>2865</td><td>##IDS_ERROR_2865##</td></row>
		<row><td>2866</td><td>##IDS_ERROR_2866##</td></row>
		<row><td>2867</td><td>##IDS_ERROR_2867##</td></row>
		<row><td>2868</td><td>##IDS_ERROR_2868##</td></row>
		<row><td>2869</td><td>##IDS_ERROR_2869##</td></row>
		<row><td>2870</td><td>##IDS_ERROR_2870##</td></row>
		<row><td>2871</td><td>##IDS_ERROR_2871##</td></row>
		<row><td>2872</td><td>##IDS_ERROR_2872##</td></row>
		<row><td>2873</td><td>##IDS_ERROR_2873##</td></row>
		<row><td>2874</td><td>##IDS_ERROR_2874##</td></row>
		<row><td>2875</td><td>##IDS_ERROR_2875##</td></row>
		<row><td>2876</td><td>##IDS_ERROR_2876##</td></row>
		<row><td>2877</td><td>##IDS_ERROR_2877##</td></row>
		<row><td>2878</td><td>##IDS_ERROR_2878##</td></row>
		<row><td>2879</td><td>##IDS_ERROR_2879##</td></row>
		<row><td>2880</td><td>##IDS_ERROR_2880##</td></row>
		<row><td>2881</td><td>##IDS_ERROR_2881##</td></row>
		<row><td>2882</td><td>##IDS_ERROR_2882##</td></row>
		<row><td>2883</td><td>##IDS_ERROR_2883##</td></row>
		<row><td>2884</td><td>##IDS_ERROR_2884##</td></row>
		<row><td>2885</td><td>##IDS_ERROR_2885##</td></row>
		<row><td>2886</td><td>##IDS_ERROR_2886##</td></row>
		<row><td>2887</td><td>##IDS_ERROR_2887##</td></row>
		<row><td>2888</td><td>##IDS_ERROR_2888##</td></row>
		<row><td>2889</td><td>##IDS_ERROR_2889##</td></row>
		<row><td>2890</td><td>##IDS_ERROR_2890##</td></row>
		<row><td>2891</td><td>##IDS_ERROR_2891##</td></row>
		<row><td>2892</td><td>##IDS_ERROR_2892##</td></row>
		<row><td>2893</td><td>##IDS_ERROR_2893##</td></row>
		<row><td>2894</td><td>##IDS_ERROR_2894##</td></row>
		<row><td>2895</td><td>##IDS_ERROR_2895##</td></row>
		<row><td>2896</td><td>##IDS_ERROR_2896##</td></row>
		<row><td>2897</td><td>##IDS_ERROR_2897##</td></row>
		<row><td>2898</td><td>##IDS_ERROR_2898##</td></row>
		<row><td>2899</td><td>##IDS_ERROR_2899##</td></row>
		<row><td>2901</td><td>##IDS_ERROR_2901##</td></row>
		<row><td>2902</td><td>##IDS_ERROR_2902##</td></row>
		<row><td>2903</td><td>##IDS_ERROR_2903##</td></row>
		<row><td>2904</td><td>##IDS_ERROR_2904##</td></row>
		<row><td>2905</td><td>##IDS_ERROR_2905##</td></row>
		<row><td>2906</td><td>##IDS_ERROR_2906##</td></row>
		<row><td>2907</td><td>##IDS_ERROR_2907##</td></row>
		<row><td>2908</td><td>##IDS_ERROR_2908##</td></row>
		<row><td>2909</td><td>##IDS_ERROR_2909##</td></row>
		<row><td>2910</td><td>##IDS_ERROR_2910##</td></row>
		<row><td>2911</td><td>##IDS_ERROR_2911##</td></row>
		<row><td>2912</td><td>##IDS_ERROR_2912##</td></row>
		<row><td>2919</td><td>##IDS_ERROR_2919##</td></row>
		<row><td>2920</td><td>##IDS_ERROR_2920##</td></row>
		<row><td>2924</td><td>##IDS_ERROR_2924##</td></row>
		<row><td>2927</td><td>##IDS_ERROR_2927##</td></row>
		<row><td>2928</td><td>##IDS_ERROR_2928##</td></row>
		<row><td>2929</td><td>##IDS_ERROR_2929##</td></row>
		<row><td>2932</td><td>##IDS_ERROR_2932##</td></row>
		<row><td>2933</td><td>##IDS_ERROR_2933##</td></row>
		<row><td>2934</td><td>##IDS_ERROR_2934##</td></row>
		<row><td>2935</td><td>##IDS_ERROR_2935##</td></row>
		<row><td>2936</td><td>##IDS_ERROR_2936##</td></row>
		<row><td>2937</td><td>##IDS_ERROR_2937##</td></row>
		<row><td>2938</td><td>##IDS_ERROR_2938##</td></row>
		<row><td>2939</td><td>##IDS_ERROR_2939##</td></row>
		<row><td>2940</td><td>##IDS_ERROR_2940##</td></row>
		<row><td>2941</td><td>##IDS_ERROR_2941##</td></row>
		<row><td>2942</td><td>##IDS_ERROR_2942##</td></row>
		<row><td>2943</td><td>##IDS_ERROR_2943##</td></row>
		<row><td>2944</td><td>##IDS_ERROR_2944##</td></row>
		<row><td>2945</td><td>##IDS_ERROR_2945##</td></row>
		<row><td>3001</td><td>##IDS_ERROR_3001##</td></row>
		<row><td>3002</td><td>##IDS_ERROR_3002##</td></row>
		<row><td>32</td><td>##IDS_ERROR_20##</td></row>
		<row><td>33</td><td>##IDS_ERROR_21##</td></row>
		<row><td>4</td><td>##IDS_ERROR_3##</td></row>
		<row><td>5</td><td>##IDS_ERROR_4##</td></row>
		<row><td>7</td><td>##IDS_ERROR_5##</td></row>
		<row><td>8</td><td>##IDS_ERROR_6##</td></row>
		<row><td>9</td><td>##IDS_ERROR_7##</td></row>
	</table>

	<table name="EventMapping">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">Event</col>
		<col def="s50">Attribute</col>
		<row><td>CustomSetup</td><td>ItemDescription</td><td>SelectionDescription</td><td>Text</td></row>
		<row><td>CustomSetup</td><td>Location</td><td>SelectionPath</td><td>Text</td></row>
		<row><td>CustomSetup</td><td>Size</td><td>SelectionSize</td><td>Text</td></row>
		<row><td>SetupInitialization</td><td>ActionData</td><td>ActionData</td><td>Text</td></row>
		<row><td>SetupInitialization</td><td>ActionText</td><td>ActionText</td><td>Text</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>AdminInstallFinalize</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>InstallFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>MoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>RemoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>RemoveRegistryValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>SetProgress</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>UnmoveFiles</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>WriteIniValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionProgress95</td><td>WriteRegistryValues</td><td>Progress</td></row>
		<row><td>SetupProgress</td><td>ActionText</td><td>ActionText</td><td>Text</td></row>
	</table>

	<table name="Extension">
		<col key="yes" def="s255">Extension</col>
		<col key="yes" def="s72">Component_</col>
		<col def="S255">ProgId_</col>
		<col def="S64">MIME_</col>
		<col def="s38">Feature_</col>
	</table>

	<table name="Feature">
		<col key="yes" def="s38">Feature</col>
		<col def="S38">Feature_Parent</col>
		<col def="L64">Title</col>
		<col def="L255">Description</col>
		<col def="I2">Display</col>
		<col def="i2">Level</col>
		<col def="S72">Directory_</col>
		<col def="i2">Attributes</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISFeatureCabName</col>
		<col def="S255">ISProFeatureName</col>
		<row><td>AlwaysInstall</td><td/><td>##DN_AlwaysInstall##</td><td>Enter the description for this feature here.</td><td>0</td><td>1</td><td>INSTALLDIR</td><td>16</td><td/><td>Enter comments regarding this feature here.</td><td/><td/></row>
	</table>

	<table name="FeatureComponents">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">Component_</col>
		<row><td>AlwaysInstall</td><td>AdvancedDemos.exe</td></row>
		<row><td>AlwaysInstall</td><td>Assimp.Interop.dll</td></row>
		<row><td>AlwaysInstall</td><td>Assimp.dll</td></row>
		<row><td>AlwaysInstall</td><td>BEPUphysics.dll</td></row>
		<row><td>AlwaysInstall</td><td>BEPUphysics.dll1</td></row>
		<row><td>AlwaysInstall</td><td>DPSFPhone.dll</td></row>
		<row><td>AlwaysInstall</td><td>DPSFPhone.dll1</td></row>
		<row><td>AlwaysInstall</td><td>FarseerPhysicsXNA.dll</td></row>
		<row><td>AlwaysInstall</td><td>FarseerPhysicsXNA.dll1</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT1</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT10</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT100</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT101</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT102</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT103</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT104</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT105</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT106</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT107</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT108</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT109</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT11</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT110</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT111</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT112</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT113</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT114</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT115</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT116</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT117</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT118</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT119</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT12</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT120</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT121</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT122</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT123</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT124</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT125</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT126</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT127</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT128</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT129</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT13</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT130</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT131</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT132</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT133</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT134</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT135</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT136</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT137</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT14</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT15</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT16</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT17</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT18</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT19</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT2</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT20</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT21</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT22</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT23</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT24</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT25</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT26</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT27</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT28</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT29</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT3</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT30</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT31</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT32</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT33</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT34</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT35</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT36</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT37</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT38</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT39</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT4</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT40</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT41</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT42</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT43</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT44</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT45</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT46</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT47</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT48</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT49</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT5</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT50</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT51</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT52</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT53</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT54</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT55</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT56</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT57</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT58</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT59</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT6</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT60</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT61</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT62</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT63</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT64</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT65</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT66</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT67</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT68</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT69</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT7</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT70</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT71</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT72</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT73</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT74</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT75</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT76</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT77</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT78</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT79</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT8</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT80</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT81</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT82</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT83</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT84</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT85</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT86</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT87</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT88</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT89</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT9</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT90</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT91</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT92</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT93</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT94</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT95</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT96</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT97</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT98</td></row>
		<row><td>AlwaysInstall</td><td>ISX_DEFAULTCOMPONENT99</td></row>
		<row><td>AlwaysInstall</td><td>ModelImporter.dll</td></row>
		<row><td>AlwaysInstall</td><td>NewComponent1</td></row>
		<row><td>AlwaysInstall</td><td>NxCharacter.dll</td></row>
		<row><td>AlwaysInstall</td><td>NxCharacter.dll1</td></row>
		<row><td>AlwaysInstall</td><td>Osiris.Terrain.Content.Pipeline.dll</td></row>
		<row><td>AlwaysInstall</td><td>PhysXCooking.dll</td></row>
		<row><td>AlwaysInstall</td><td>PhysXCooking.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PhysXCore.dll</td></row>
		<row><td>AlwaysInstall</td><td>PhysXCore.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PhysXDevice.dll</td></row>
		<row><td>AlwaysInstall</td><td>PhysXDevice.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PhysXLoader.dll</td></row>
		<row><td>AlwaysInstall</td><td>PhysXLoader.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEngineDebug.dll</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEngineDebug.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEngineDebug.dll2</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEngineDebug.dll3</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEngineDebug.dll4</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEngineDebug.dll5</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePhone7.dll</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePhone7.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePipelineDebug.dll</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePipelineDebug.dll1</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePipelineDebug.dll2</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePipelineDebug.dll3</td></row>
		<row><td>AlwaysInstall</td><td>PloobsEnginePipelineDebug.dll4</td></row>
		<row><td>AlwaysInstall</td><td>PloobsScripts.dll</td></row>
		<row><td>AlwaysInstall</td><td>PloobsSerializator.dll</td></row>
		<row><td>AlwaysInstall</td><td>Ploobs_Engine_Help.chm</td></row>
		<row><td>AlwaysInstall</td><td>RVO2DWrapper.dll</td></row>
		<row><td>AlwaysInstall</td><td>RVO3D.dll</td></row>
		<row><td>AlwaysInstall</td><td>SharedOsiris.dll</td></row>
		<row><td>AlwaysInstall</td><td>SkinnedModel.dll</td></row>
		<row><td>AlwaysInstall</td><td>SkinnedModel.dll1</td></row>
		<row><td>AlwaysInstall</td><td>SkinnedModelPipeline.dll</td></row>
		<row><td>AlwaysInstall</td><td>StillDesign.PhysX.Net.dll</td></row>
		<row><td>AlwaysInstall</td><td>StillDesign.PhysX.Net.dll1</td></row>
		<row><td>AlwaysInstall</td><td>XNAnimation.dll</td></row>
		<row><td>AlwaysInstall</td><td>XNAnimation.dll1</td></row>
		<row><td>AlwaysInstall</td><td>XNAnimationPipeline.dll</td></row>
		<row><td>AlwaysInstall</td><td>XNAnimationPipeline.dll1</td></row>
		<row><td>AlwaysInstall</td><td>cudart32_30_9.dll</td></row>
		<row><td>AlwaysInstall</td><td>cudart32_30_9.dll1</td></row>
	</table>

	<table name="File">
		<col key="yes" def="s72">File</col>
		<col def="s72">Component_</col>
		<col def="s255">FileName</col>
		<col def="i4">FileSize</col>
		<col def="S72">Version</col>
		<col def="S20">Language</col>
		<col def="I2">Attributes</col>
		<col def="i2">Sequence</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I4">ISAttributes</col>
		<col def="S72">ISComponentSubFolder_</col>
		<row><td>_dparticles.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>3DPART~1.JPG|3dparticles.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\3dparticles.jpg</td><td>1</td><td/></row>
		<row><td>actionscriptscreen.cs</td><td>ISX_DEFAULTCOMPONENT64</td><td>ACTION~1.CS|ActionScriptScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo7 - Scripts\ActionScriptScreen.cs</td><td>1</td><td/></row>
		<row><td>advanceddemos.csproj</td><td>ISX_DEFAULTCOMPONENT21</td><td>ADVANC~1.CSP|AdvancedDemos.csproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos.csproj</td><td>1</td><td/></row>
		<row><td>advanceddemos.csproj.release</td><td>ISX_DEFAULTCOMPONENT21</td><td>ADVANC~1.CAC|AdvancedDemos.csproj.Release.cachefile</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos.csproj.Release.cachefile</td><td>1</td><td/></row>
		<row><td>advanceddemos.csproj.user</td><td>ISX_DEFAULTCOMPONENT21</td><td>ADVANC~1.USE|AdvancedDemos.csproj.user</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos.csproj.user</td><td>1</td><td/></row>
		<row><td>advanceddemos.exe</td><td>AdvancedDemos.exe</td><td>ADVANC~1.EXE|AdvancedDemos.exe</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\AdvancedDemos.exe</td><td>1</td><td/></row>
		<row><td>advanceddemos.exe.config</td><td>ISX_DEFAULTCOMPONENT6</td><td>ADVANC~1.CON|AdvancedDemos.exe.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\AdvancedDemos.exe.config</td><td>1</td><td/></row>
		<row><td>advanceddemos.sln</td><td>ISX_DEFAULTCOMPONENT19</td><td>ADVANC~1.SLN|AdvancedDemos.sln</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos.sln</td><td>1</td><td/></row>
		<row><td>advanceddemos.suo</td><td>ISX_DEFAULTCOMPONENT19</td><td>ADVANC~1.SUO|AdvancedDemos.suo</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos.suo</td><td>1</td><td/></row>
		<row><td>advanceddemoscontent.content</td><td>ISX_DEFAULTCOMPONENT103</td><td>ADVANC~1.CON|AdvancedDemosContent.contentproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\AdvancedDemosContent.contentproj</td><td>1</td><td/></row>
		<row><td>alarm.wav</td><td>ISX_DEFAULTCOMPONENT113</td><td>alarm.wav</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Songs\alarm.wav</td><td>1</td><td/></row>
		<row><td>alarm.xnb</td><td>ISX_DEFAULTCOMPONENT17</td><td>alarm.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Songs\alarm.xnb</td><td>1</td><td/></row>
		<row><td>alphagradative.fx</td><td>ISX_DEFAULTCOMPONENT104</td><td>ALPHAG~1.FX|alphagradative.fx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Effects\alphagradative.fx</td><td>1</td><td/></row>
		<row><td>alphagradative.xnb</td><td>ISX_DEFAULTCOMPONENT8</td><td>ALPHAG~1.XNB|alphagradative.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Effects\alphagradative.xnb</td><td>1</td><td/></row>
		<row><td>ambientenvironmenpscreen.cs</td><td>ISX_DEFAULTCOMPONENT24</td><td>AMBIEN~1.CS|AmbientEnvironmenpScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo2 - Materials\AmbientEnvironmenpScreen.cs</td><td>1</td><td/></row>
		<row><td>animatedbilboardscreen.cs</td><td>ISX_DEFAULTCOMPONENT29</td><td>ANIMAT~1.CS|AnimatedBilboardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo6 - BilboardsSamples\AnimatedBilboardScreen.cs</td><td>1</td><td/></row>
		<row><td>animationplate.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>ANIMAT~1.JPG|AnimationPlate.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\AnimationPlate.jpg</td><td>1</td><td/></row>
		<row><td>animationscreen.cs</td><td>ISX_DEFAULTCOMPONENT120</td><td>ANIMAT~2.CS|AnimationScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Animation\AnimationScreen.cs</td><td>1</td><td/></row>
		<row><td>animationscreen1.cs</td><td>ISX_DEFAULTCOMPONENT88</td><td>ANIMAT~1.CS|AnimationScreen1.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\AnimationScreen1.cs</td><td>1</td><td/></row>
		<row><td>animscreen.cs</td><td>ISX_DEFAULTCOMPONENT32</td><td>ANIMSC~1.CS|AnimScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\ModelAnim\AnimScreen.cs</td><td>1</td><td/></row>
		<row><td>app.config</td><td>ISX_DEFAULTCOMPONENT21</td><td>APP~1.CON|app.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\app.config</td><td>1</td><td/></row>
		<row><td>app.config1</td><td>ISX_DEFAULTCOMPONENT54</td><td>APP~1.CON|app.config</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\app.config</td><td>1</td><td/></row>
		<row><td>app.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>APP~1.XAM|App.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\App.xaml</td><td>1</td><td/></row>
		<row><td>app.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>APPXAM~1.CS|App.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\App.xaml.cs</td><td>1</td><td/></row>
		<row><td>applicationicon.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>APPLIC~1.JPG|ApplicationIcon.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\ApplicationIcon.jpg</td><td>1</td><td/></row>
		<row><td>appmanifest.xml</td><td>ISX_DEFAULTCOMPONENT84</td><td>APPMAN~1.XML|AppManifest.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Properties\AppManifest.xml</td><td>1</td><td/></row>
		<row><td>assemblyinfo.cs</td><td>ISX_DEFAULTCOMPONENT50</td><td>ASSEMB~1.CS|AssemblyInfo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Properties\AssemblyInfo.cs</td><td>1</td><td/></row>
		<row><td>assemblyinfo.cs1</td><td>ISX_DEFAULTCOMPONENT76</td><td>ASSEMB~1.CS|AssemblyInfo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Properties\AssemblyInfo.cs</td><td>1</td><td/></row>
		<row><td>assemblyinfo.cs2</td><td>ISX_DEFAULTCOMPONENT84</td><td>ASSEMB~1.CS|AssemblyInfo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Properties\AssemblyInfo.cs</td><td>1</td><td/></row>
		<row><td>assemblyinfo.cs3</td><td>ISX_DEFAULTCOMPONENT132</td><td>ASSEMB~1.CS|AssemblyInfo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\Properties\AssemblyInfo.cs</td><td>1</td><td/></row>
		<row><td>assemblyinfo.cs4</td><td>ISX_DEFAULTCOMPONENT124</td><td>ASSEMB~1.CS|AssemblyInfo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Properties\AssemblyInfo.cs</td><td>1</td><td/></row>
		<row><td>assimp.dll</td><td>Assimp.dll</td><td>Assimp.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\Dlls\Assimp.dll</td><td>1</td><td/></row>
		<row><td>assimp.interop.dll</td><td>Assimp.Interop.dll</td><td>ASSIMP~1.DLL|Assimp.Interop.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\Dlls\Assimp.Interop.dll</td><td>1</td><td/></row>
		<row><td>background.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>BACKGR~1.JPG|Background.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Background.jpg</td><td>1</td><td/></row>
		<row><td>ball.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>ball.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\ball.X</td><td>1</td><td/></row>
		<row><td>ball.x1</td><td>ISX_DEFAULTCOMPONENT79</td><td>ball.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Model\ball.X</td><td>1</td><td/></row>
		<row><td>ball.x2</td><td>ISX_DEFAULTCOMPONENT134</td><td>ball.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\ball.X</td><td>1</td><td/></row>
		<row><td>ball.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>ball.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\ball.xnb</td><td>1</td><td/></row>
		<row><td>ballthrowbepu.cs</td><td>ISX_DEFAULTCOMPONENT85</td><td>BALLTH~1.CS|BallThrowBepu.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\BallThrowBepu.cs</td><td>1</td><td/></row>
		<row><td>ballthrowphysx28.cs</td><td>ISX_DEFAULTCOMPONENT42</td><td>BALLTH~1.CS|BallThrowPhysx28.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\BallThrowPhysx28.cs</td><td>1</td><td/></row>
		<row><td>base.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>base.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\base.jpg</td><td>1</td><td/></row>
		<row><td>baseobject.cs</td><td>ISX_DEFAULTCOMPONENT67</td><td>BASEOB~1.CS|BaseObject.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Behavior Tree\BaseObject.cs</td><td>1</td><td/></row>
		<row><td>baseobject.cs1</td><td>ISX_DEFAULTCOMPONENT89</td><td>BASEOB~1.CS|BaseObject.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\Behaviors\BaseObject.cs</td><td>1</td><td/></row>
		<row><td>basic2d.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.XAM|Basic2D.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2D.xaml</td><td>1</td><td/></row>
		<row><td>basic2d.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.CS|Basic2D.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2D.xaml.cs</td><td>1</td><td/></row>
		<row><td>basic2dcamera.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.XAM|Basic2DCamera.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DCamera.xaml</td><td>1</td><td/></row>
		<row><td>basic2dcamera.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.CS|Basic2DCamera.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DCamera.xaml.cs</td><td>1</td><td/></row>
		<row><td>basic2dcamerascreen.cs</td><td>ISX_DEFAULTCOMPONENT86</td><td>BASIC2~1.CS|Basic2DCameraScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Basic2DCameraScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dcamerascreen.cs1</td><td>ISX_DEFAULTCOMPONENT118</td><td>BASIC2~1.CS|Basic2DCameraScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Basic2DCameraScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dparticle.cs</td><td>ISX_DEFAULTCOMPONENT118</td><td>BASIC2~1.CS|Basic2DParticle.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Basic2DParticle.cs</td><td>1</td><td/></row>
		<row><td>basic2dparticles.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.XAM|Basic2DParticles.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DParticles.xaml</td><td>1</td><td/></row>
		<row><td>basic2dparticles.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.CS|Basic2DParticles.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DParticles.xaml.cs</td><td>1</td><td/></row>
		<row><td>basic2dparticlescreen.cs</td><td>ISX_DEFAULTCOMPONENT86</td><td>BASIC2~1.CS|Basic2DParticleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Basic2DParticleScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dphysic.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.XAM|Basic2DPhysic.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DPhysic.xaml</td><td>1</td><td/></row>
		<row><td>basic2dphysic.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.CS|Basic2DPhysic.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DPhysic.xaml.cs</td><td>1</td><td/></row>
		<row><td>basic2dphysicscreen.cs</td><td>ISX_DEFAULTCOMPONENT86</td><td>BASIC2~1.CS|Basic2DPhysicScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Basic2DPhysicScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dphysicscreen.cs1</td><td>ISX_DEFAULTCOMPONENT118</td><td>BASIC2~1.CS|Basic2DPhysicScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Basic2DPhysicScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dpositioning.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.XAM|Basic2DPositioning.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DPositioning.xaml</td><td>1</td><td/></row>
		<row><td>basic2dpositioning.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.CS|Basic2DPositioning.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DPositioning.xaml.cs</td><td>1</td><td/></row>
		<row><td>basic2dpositioningscreen.cs</td><td>ISX_DEFAULTCOMPONENT86</td><td>BASIC2~1.CS|Basic2DPositioningScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Basic2DPositioningScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dpositioningscreen.cs1</td><td>ISX_DEFAULTCOMPONENT118</td><td>BASIC2~1.CS|Basic2DPositioningScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Basic2DPositioningScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dscreen.cs</td><td>ISX_DEFAULTCOMPONENT86</td><td>BASIC2~1.CS|Basic2DScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Basic2DScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dsprite.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.XAM|Basic2DSprite.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DSprite.xaml</td><td>1</td><td/></row>
		<row><td>basic2dsprite.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BASIC2~1.CS|Basic2DSprite.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Basic2DSprite.xaml.cs</td><td>1</td><td/></row>
		<row><td>basic2dspritescreen.cs</td><td>ISX_DEFAULTCOMPONENT86</td><td>BASIC2~1.CS|Basic2DSpriteScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Basic2DSpriteScreen.cs</td><td>1</td><td/></row>
		<row><td>basic2dspritescreen.cs1</td><td>ISX_DEFAULTCOMPONENT118</td><td>BASIC2~1.CS|Basic2DSpriteScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Basic2DSpriteScreen.cs</td><td>1</td><td/></row>
		<row><td>basiccloth28screen.cs</td><td>ISX_DEFAULTCOMPONENT45</td><td>BASICC~1.CS|BasicCloth28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\Screens\BasicCloth28Screen.cs</td><td>1</td><td/></row>
		<row><td>basicscreendeferreddemo.cs</td><td>ISX_DEFAULTCOMPONENT56</td><td>BASICS~1.CS|BasicScreenDeferredDemo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo1 - BasicScreen\BasicScreenDeferredDemo.cs</td><td>1</td><td/></row>
		<row><td>basicscreenforwarddemo.cs</td><td>ISX_DEFAULTCOMPONENT56</td><td>BASICS~2.CS|BasicScreenForwardDemo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo1 - BasicScreen\BasicScreenForwardDemo.cs</td><td>1</td><td/></row>
		<row><td>behaviorpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>BEHAVI~1.XAM|BehaviorPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\BehaviorPage.xaml</td><td>1</td><td/></row>
		<row><td>behaviorpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>BEHAVI~1.CS|BehaviorPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\BehaviorPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>behaviors.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>BEHAVI~1.JPG|Behaviors.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Behaviors.jpg</td><td>1</td><td/></row>
		<row><td>behaviortreescreen.cs</td><td>ISX_DEFAULTCOMPONENT67</td><td>BEHAVI~1.CS|BehaviorTreeScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Behavior Tree\BehaviorTreeScreen.cs</td><td>1</td><td/></row>
		<row><td>behaviortreescreen.cs1</td><td>ISX_DEFAULTCOMPONENT89</td><td>BEHAVI~1.CS|BehaviorTreeScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\Behaviors\BehaviorTreeScreen.cs</td><td>1</td><td/></row>
		<row><td>bepuphysics.dll</td><td>BEPUphysics.dll</td><td>BEPUPH~1.DLL|BEPUphysics.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\BEPUphysics.dll</td><td>1</td><td/></row>
		<row><td>bepuphysics.dll1</td><td>BEPUphysics.dll1</td><td>BEPUPH~1.DLL|BEPUphysics.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\BEPUphysics.dll</td><td>1</td><td/></row>
		<row><td>bepuphysics.xml</td><td>ISX_DEFAULTCOMPONENT137</td><td>BEPUPH~1.XML|BEPUphysics.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\BEPUphysics.XML</td><td>1</td><td/></row>
		<row><td>bepuphysics.xml1</td><td>ISX_DEFAULTCOMPONENT96</td><td>BEPUPH~1.XML|BEPUphysics.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\BEPUphysics.XML</td><td>1</td><td/></row>
		<row><td>bilboard3d.cs</td><td>ISX_DEFAULTCOMPONENT121</td><td>BILBOA~1.CS|Bilboard3D.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Bilboard\Bilboard3D.cs</td><td>1</td><td/></row>
		<row><td>block.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>block.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\block.X</td><td>1</td><td/></row>
		<row><td>block.x1</td><td>ISX_DEFAULTCOMPONENT79</td><td>block.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Model\block.X</td><td>1</td><td/></row>
		<row><td>block.x2</td><td>ISX_DEFAULTCOMPONENT128</td><td>block.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Model\block.X</td><td>1</td><td/></row>
		<row><td>block.x3</td><td>ISX_DEFAULTCOMPONENT134</td><td>block.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\block.X</td><td>1</td><td/></row>
		<row><td>block.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>block.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\block.xnb</td><td>1</td><td/></row>
		<row><td>border.cs</td><td>ISX_DEFAULTCOMPONENT87</td><td>Border.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Helpers\Border.cs</td><td>1</td><td/></row>
		<row><td>border.cs1</td><td>ISX_DEFAULTCOMPONENT119</td><td>Border.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Helpers\Border.cs</td><td>1</td><td/></row>
		<row><td>box_occlusion.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>BOX_OC~1.JPG|box_occlusion.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\box_occlusion.jpg</td><td>1</td><td/></row>
		<row><td>boxcloth28screen.cs</td><td>ISX_DEFAULTCOMPONENT45</td><td>BOXCLO~1.CS|BoxCloth28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\Screens\BoxCloth28Screen.cs</td><td>1</td><td/></row>
		<row><td>brown_metal.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>BROWN-~1.JPG|brown-metal.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\brown-metal.jpg</td><td>1</td><td/></row>
		<row><td>bumpspeculardemo.cs</td><td>ISX_DEFAULTCOMPONENT24</td><td>BUMPSP~1.CS|BumpSpecularDemo.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo2 - Materials\BumpSpecularDemo.cs</td><td>1</td><td/></row>
		<row><td>busy.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>Busy.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Busy.cur</td><td>1</td><td/></row>
		<row><td>busy.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>Busy.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Busy.xnb</td><td>1</td><td/></row>
		<row><td>button.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>Button.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Button.png</td><td>1</td><td/></row>
		<row><td>button.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>Button.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Button.xnb</td><td>1</td><td/></row>
		<row><td>bye.wav</td><td>ISX_DEFAULTCOMPONENT113</td><td>bye.wav</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Songs\bye.wav</td><td>1</td><td/></row>
		<row><td>bye.xnb</td><td>ISX_DEFAULTCOMPONENT17</td><td>bye.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Songs\bye.xnb</td><td>1</td><td/></row>
		<row><td>camera2d.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>camera2d.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\camera2d.jpg</td><td>1</td><td/></row>
		<row><td>camerapathscreen.cs</td><td>ISX_DEFAULTCOMPONENT62</td><td>CAMERA~1.CS|CameraPathScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo5 - Cameras\CameraPathScreen.cs</td><td>1</td><td/></row>
		<row><td>camerascreens.cs</td><td>ISX_DEFAULTCOMPONENT62</td><td>CAMERA~2.CS|CameraScreens.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo5 - Cameras\CameraScreens.cs</td><td>1</td><td/></row>
		<row><td>cena.x</td><td>ISX_DEFAULTCOMPONENT134</td><td>cena.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\cena.X</td><td>1</td><td/></row>
		<row><td>cena.x.log</td><td>ISX_DEFAULTCOMPONENT134</td><td>CENAX~1.LOG|cena.X.log</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\cena.X.log</td><td>1</td><td/></row>
		<row><td>cenario.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>cenario.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\cenario.X</td><td>1</td><td/></row>
		<row><td>cenario.x1</td><td>ISX_DEFAULTCOMPONENT79</td><td>cenario.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Model\cenario.X</td><td>1</td><td/></row>
		<row><td>cenario.x2</td><td>ISX_DEFAULTCOMPONENT134</td><td>cenario.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\cenario.X</td><td>1</td><td/></row>
		<row><td>cenario.x3</td><td>ISX_DEFAULTCOMPONENT128</td><td>cenario.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Model\cenario.X</td><td>1</td><td/></row>
		<row><td>cenario.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>cenario.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\cenario.xnb</td><td>1</td><td/></row>
		<row><td>cerca_curva_diffuse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>CERCA_~1.JPG|cerca_curva_diffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\cerca_curva_diffuse.jpg</td><td>1</td><td/></row>
		<row><td>cerca_diffuse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>CERCA_~1.JPG|CERCA_DIFFUSE.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\CERCA_DIFFUSE.jpg</td><td>1</td><td/></row>
		<row><td>cerca_ponte_diffuse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>CERCA_~1.JPG|cerca_ponte_diffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\cerca_ponte_diffuse.jpg</td><td>1</td><td/></row>
		<row><td>changingmessagesscreen.cs</td><td>ISX_DEFAULTCOMPONENT75</td><td>CHANGI~1.CS|ChangingMessagesScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos Extras\ChangingMessagesScreen.cs</td><td>1</td><td/></row>
		<row><td>chao.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>chao.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\chao.jpg</td><td>1</td><td/></row>
		<row><td>chao.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>chao.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\chao.xnb</td><td>1</td><td/></row>
		<row><td>chao_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>chao_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\chao_0.xnb</td><td>1</td><td/></row>
		<row><td>chaobump.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>chaobump.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\chaobump.jpg</td><td>1</td><td/></row>
		<row><td>chaobump.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>chaobump.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\chaobump.xnb</td><td>1</td><td/></row>
		<row><td>charactercontrollerinput.cs</td><td>ISX_DEFAULTCOMPONENT30</td><td>CHARAC~1.CS|CharacterControllerInput.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\CharacterControllerInput.cs</td><td>1</td><td/></row>
		<row><td>charactercontrollerinput.cs1</td><td>ISX_DEFAULTCOMPONENT61</td><td>CHARAC~1.CS|CharacterControllerInput.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo4 - Physics\Helper\CharacterControllerInput.cs</td><td>1</td><td/></row>
		<row><td>charactercontrollerinput.cs2</td><td>ISX_DEFAULTCOMPONENT89</td><td>CHARAC~1.CS|CharacterControllerInput.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\Behaviors\CharacterControllerInput.cs</td><td>1</td><td/></row>
		<row><td>checkbox.checked.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>CHECKB~1.PNG|CheckBox.Checked.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\CheckBox.Checked.png</td><td>1</td><td/></row>
		<row><td>checkbox.checked.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>CHECKB~1.XNB|CheckBox.Checked.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\CheckBox.Checked.xnb</td><td>1</td><td/></row>
		<row><td>checkbox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>CheckBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\CheckBox.png</td><td>1</td><td/></row>
		<row><td>checkbox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>CheckBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\CheckBox.xnb</td><td>1</td><td/></row>
		<row><td>cilos.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>cilos.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\cilos.X</td><td>1</td><td/></row>
		<row><td>cilos.x1</td><td>ISX_DEFAULTCOMPONENT134</td><td>cilos.x</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\cilos.x</td><td>1</td><td/></row>
		<row><td>cilos.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>cilos.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\cilos.xnb</td><td>1</td><td/></row>
		<row><td>clothmaterial.cs</td><td>ISX_DEFAULTCOMPONENT44</td><td>CLOTHM~1.CS|ClothMaterial.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\ClothMaterial.cs</td><td>1</td><td/></row>
		<row><td>clothmodel.cs</td><td>ISX_DEFAULTCOMPONENT44</td><td>CLOTHM~1.CS|ClothModel.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\ClothModel.cs</td><td>1</td><td/></row>
		<row><td>cloudlight.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>CLOUDL~1.TGA|CloudLight.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\CloudLight.tga</td><td>1</td><td/></row>
		<row><td>cloudlight.tga1</td><td>ISX_DEFAULTCOMPONENT136</td><td>CLOUDL~1.TGA|CloudLight.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\CloudLight.tga</td><td>1</td><td/></row>
		<row><td>cloudlight.tga2</td><td>ISX_DEFAULTCOMPONENT130</td><td>CLOUDL~1.TGA|CloudLight.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\CloudLight.tga</td><td>1</td><td/></row>
		<row><td>cloudlight.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>CLOUDL~1.XNB|CloudLight.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\CloudLight.xnb</td><td>1</td><td/></row>
		<row><td>coin.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>coin.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\coin.png</td><td>1</td><td/></row>
		<row><td>coin.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>coin.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\coin.xnb</td><td>1</td><td/></row>
		<row><td>colladaformatscreen.cs</td><td>ISX_DEFAULTCOMPONENT125</td><td>COLLAD~1.CS|ColladaFormatScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\ColladaFormatScreen.cs</td><td>1</td><td/></row>
		<row><td>collisiontypesbepuscreen.cs</td><td>ISX_DEFAULTCOMPONENT60</td><td>COLLIS~1.CS|CollisionTypesBepuScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo4 - Physics\CollisionTypesBepuScreen.cs</td><td>1</td><td/></row>
		<row><td>color_map.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>COLOR_~1.DDS|color_map.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\color_map.dds</td><td>1</td><td/></row>
		<row><td>color_map.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>COLOR_~1.XNB|color_map.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\color_map.xnb</td><td>1</td><td/></row>
		<row><td>combobox.button.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>COMBOB~1.PNG|ComboBox.Button.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ComboBox.Button.png</td><td>1</td><td/></row>
		<row><td>combobox.button.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>COMBOB~1.XNB|ComboBox.Button.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ComboBox.Button.xnb</td><td>1</td><td/></row>
		<row><td>combobox.listbox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>COMBOB~1.PNG|ComboBox.ListBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ComboBox.ListBox.png</td><td>1</td><td/></row>
		<row><td>combobox.listbox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>COMBOB~1.XNB|ComboBox.ListBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ComboBox.ListBox.xnb</td><td>1</td><td/></row>
		<row><td>combobox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ComboBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ComboBox.png</td><td>1</td><td/></row>
		<row><td>combobox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ComboBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ComboBox.xnb</td><td>1</td><td/></row>
		<row><td>constraintscreen.cs</td><td>ISX_DEFAULTCOMPONENT75</td><td>CONSTR~1.CS|ConstraintScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos Extras\ConstraintScreen.cs</td><td>1</td><td/></row>
		<row><td>contextmenu.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>CONTEX~1.PNG|ContextMenu.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ContextMenu.png</td><td>1</td><td/></row>
		<row><td>contextmenu.selection.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>CONTEX~1.PNG|ContextMenu.Selection.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ContextMenu.Selection.png</td><td>1</td><td/></row>
		<row><td>contextmenu.selection.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>CONTEX~1.XNB|ContextMenu.Selection.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ContextMenu.Selection.xnb</td><td>1</td><td/></row>
		<row><td>contextmenu.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>CONTEX~1.XNB|ContextMenu.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ContextMenu.xnb</td><td>1</td><td/></row>
		<row><td>control.outline.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>CONTRO~1.PNG|Control.Outline.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Control.Outline.png</td><td>1</td><td/></row>
		<row><td>control.outline.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>CONTRO~1.XNB|Control.Outline.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Control.Outline.xnb</td><td>1</td><td/></row>
		<row><td>control.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>Control.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Control.png</td><td>1</td><td/></row>
		<row><td>control.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>Control.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Control.xnb</td><td>1</td><td/></row>
		<row><td>cpubillboardscreen.cs</td><td>ISX_DEFAULTCOMPONENT121</td><td>CPUBIL~1.CS|CPUBillboardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Bilboard\CPUBillboardScreen.cs</td><td>1</td><td/></row>
		<row><td>cpubillboardscreen.cs1</td><td>ISX_DEFAULTCOMPONENT29</td><td>CPUBIL~1.CS|CPUBillboardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo6 - BilboardsSamples\CPUBillboardScreen.cs</td><td>1</td><td/></row>
		<row><td>cross.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>Cross.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Cross.cur</td><td>1</td><td/></row>
		<row><td>cross.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>Cross.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Cross.xnb</td><td>1</td><td/></row>
		<row><td>crowd.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>Crowd.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Crowd.jpg</td><td>1</td><td/></row>
		<row><td>crowdpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>CROWDP~1.XAM|CrowdPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\CrowdPage.xaml</td><td>1</td><td/></row>
		<row><td>crowdpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>CROWDP~1.CS|CrowdPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\CrowdPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>crowedscreen.cs</td><td>ISX_DEFAULTCOMPONENT90</td><td>CROWED~1.CS|CrowedScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\Crowed\CrowedScreen.cs</td><td>1</td><td/></row>
		<row><td>cube.dae</td><td>ISX_DEFAULTCOMPONENT127</td><td>cube.dae</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\cube.dae</td><td>1</td><td/></row>
		<row><td>cubemap.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>cubeMap.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\cubeMap.dds</td><td>1</td><td/></row>
		<row><td>cubemap.dds1</td><td>ISX_DEFAULTCOMPONENT136</td><td>cubeMap.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\cubeMap.dds</td><td>1</td><td/></row>
		<row><td>cubemap.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>cubeMap.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\cubeMap.xnb</td><td>1</td><td/></row>
		<row><td>cubo.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>cubo.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\cubo.X</td><td>1</td><td/></row>
		<row><td>cubo.x1</td><td>ISX_DEFAULTCOMPONENT79</td><td>cubo.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Model\cubo.X</td><td>1</td><td/></row>
		<row><td>cubo.x2</td><td>ISX_DEFAULTCOMPONENT134</td><td>cubo.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\cubo.X</td><td>1</td><td/></row>
		<row><td>cubo.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>cubo.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\cubo.xnb</td><td>1</td><td/></row>
		<row><td>cudart32_30_9.dll</td><td>cudart32_30_9.dll</td><td>CUDART~1.DLL|cudart32_30_9.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\cudart32_30_9.dll</td><td>1</td><td/></row>
		<row><td>cudart32_30_9.dll1</td><td>cudart32_30_9.dll1</td><td>CUDART~1.DLL|cudart32_30_9.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\cudart32_30_9.dll</td><td>1</td><td/></row>
		<row><td>cylindricbillboardcomponent.</td><td>ISX_DEFAULTCOMPONENT121</td><td>CYLIND~1.CS|CylindricBillboardComponent.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Bilboard\CylindricBillboardComponent.cs</td><td>1</td><td/></row>
		<row><td>debugdrawscreen.cs</td><td>ISX_DEFAULTCOMPONENT75</td><td>DEBUGD~1.CS|DebugDrawScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos Extras\DebugDrawScreen.cs</td><td>1</td><td/></row>
		<row><td>decal.cs</td><td>ISX_DEFAULTCOMPONENT39</td><td>Decal.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.5 Decal\Decal.cs</td><td>1</td><td/></row>
		<row><td>decal.fx</td><td>ISX_DEFAULTCOMPONENT104</td><td>decal.fx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Effects\decal.fx</td><td>1</td><td/></row>
		<row><td>decal.xnb</td><td>ISX_DEFAULTCOMPONENT8</td><td>decal.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Effects\decal.xnb</td><td>1</td><td/></row>
		<row><td>decalcomponent.cs</td><td>ISX_DEFAULTCOMPONENT39</td><td>DECALC~1.CS|DecalComponent.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.5 Decal\DecalComponent.cs</td><td>1</td><td/></row>
		<row><td>decalscreen.cs</td><td>ISX_DEFAULTCOMPONENT39</td><td>DECALS~1.CS|DecalScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.5 Decal\DecalScreen.cs</td><td>1</td><td/></row>
		<row><td>default.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>Default.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Default.cur</td><td>1</td><td/></row>
		<row><td>default.spritefont</td><td>ISX_DEFAULTCOMPONENT111</td><td>DEFAUL~1.SPR|Default.spritefont</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Fonts\Default.spritefont</td><td>1</td><td/></row>
		<row><td>default.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>Default.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Default.xnb</td><td>1</td><td/></row>
		<row><td>default.xnb1</td><td>ISX_DEFAULTCOMPONENT15</td><td>Default.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Fonts\Default.xnb</td><td>1</td><td/></row>
		<row><td>deferredanimatedscreen.cs</td><td>ISX_DEFAULTCOMPONENT30</td><td>DEFERR~1.CS|DeferredAnimatedScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\DeferredAnimatedScreen.cs</td><td>1</td><td/></row>
		<row><td>deferreddirectionaldshadowsc</td><td>ISX_DEFAULTCOMPONENT36</td><td>DEFERR~1.CS|DeferredDirectionaldShadowScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.2 Shadow\DeferredDirectionaldShadowScreen.cs</td><td>1</td><td/></row>
		<row><td>deferredemitterfluids28scree</td><td>ISX_DEFAULTCOMPONENT46</td><td>DEFERR~1.CS|DeferredEmitterFluids28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Fluids\DeferredEmitterFluids28Screen.cs</td><td>1</td><td/></row>
		<row><td>deferredloadscreen.cs</td><td>ISX_DEFAULTCOMPONENT23</td><td>DEFERR~1.CS|DeferredLoadScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo1 -  Loader\DeferredLoadScreen.cs</td><td>1</td><td/></row>
		<row><td>demos.cs</td><td>ISX_DEFAULTCOMPONENT21</td><td>Demos.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demos.cs</td><td>1</td><td/></row>
		<row><td>demos.cs1</td><td>ISX_DEFAULTCOMPONENT54</td><td>Demos.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos.cs</td><td>1</td><td/></row>
		<row><td>demoshomescreen.cs</td><td>ISX_DEFAULTCOMPONENT21</td><td>DEMOSH~1.CS|DemosHomeScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\DemosHomeScreen.cs</td><td>1</td><td/></row>
		<row><td>demoshomescreen.cs1</td><td>ISX_DEFAULTCOMPONENT54</td><td>DEMOSH~1.CS|DemosHomeScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\DemosHomeScreen.cs</td><td>1</td><td/></row>
		<row><td>demoshomescreen.cs2</td><td>ISX_DEFAULTCOMPONENT117</td><td>DEMOSH~1.CS|DemosHomeScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\DemosHomeScreen.cs</td><td>1</td><td/></row>
		<row><td>dguiscreen.cs</td><td>ISX_DEFAULTCOMPONENT33</td><td>DGUISC~1.CS|DGUIScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo8 - GUI\DGUIScreen.cs</td><td>1</td><td/></row>
		<row><td>diagonalleft.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>DIAGON~1.CUR|DiagonalLeft.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\DiagonalLeft.cur</td><td>1</td><td/></row>
		<row><td>diagonalleft.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>DIAGON~1.XNB|DiagonalLeft.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\DiagonalLeft.xnb</td><td>1</td><td/></row>
		<row><td>diagonalright.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>DIAGON~1.CUR|DiagonalRight.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\DiagonalRight.cur</td><td>1</td><td/></row>
		<row><td>diagonalright.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>DIAGON~1.XNB|DiagonalRight.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\DiagonalRight.xnb</td><td>1</td><td/></row>
		<row><td>dpsfphone.dll</td><td>DPSFPhone.dll</td><td>DPSFPH~1.DLL|DPSFPhone.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\DPSFPhone.dll</td><td>1</td><td/></row>
		<row><td>dpsfphone.dll1</td><td>DPSFPhone.dll1</td><td>DPSFPH~1.DLL|DPSFPhone.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\DPSFPhone.dll</td><td>1</td><td/></row>
		<row><td>dpsfphone.xml</td><td>ISX_DEFAULTCOMPONENT137</td><td>DPSFPH~1.XML|DPSFPhone.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\DPSFPhone.xml</td><td>1</td><td/></row>
		<row><td>dpsfphone.xml1</td><td>ISX_DEFAULTCOMPONENT96</td><td>DPSFPH~1.XML|DPSFPhone.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\DPSFPhone.xml</td><td>1</td><td/></row>
		<row><td>duck.dae</td><td>ISX_DEFAULTCOMPONENT127</td><td>duck.dae</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\duck.dae</td><td>1</td><td/></row>
		<row><td>duckcm.tga</td><td>ISX_DEFAULTCOMPONENT127</td><td>duckCM.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\duckCM.tga</td><td>1</td><td/></row>
		<row><td>dude.fbx</td><td>ISX_DEFAULTCOMPONENT106</td><td>dude.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\Dude\dude.fbx</td><td>1</td><td/></row>
		<row><td>dude.fbx1</td><td>ISX_DEFAULTCOMPONENT105</td><td>Dude.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\Dude.fbx</td><td>1</td><td/></row>
		<row><td>dude.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>dude.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\dude.xnb</td><td>1</td><td/></row>
		<row><td>dude.xnb1</td><td>ISX_DEFAULTCOMPONENT9</td><td>Dude.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude.xnb</td><td>1</td><td/></row>
		<row><td>dudesheet.png</td><td>ISX_DEFAULTCOMPONENT136</td><td>DUDESH~1.PNG|DudeSheet.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\DudeSheet.png</td><td>1</td><td/></row>
		<row><td>dudesheet.png1</td><td>ISX_DEFAULTCOMPONENT130</td><td>DUDESH~1.PNG|DudeSheet.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\DudeSheet.png</td><td>1</td><td/></row>
		<row><td>duto_occlusion_occlusion.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>DUTO_O~1.JPG|duto_occlusion_occlusion.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\duto_occlusion_occlusion.jpg</td><td>1</td><td/></row>
		<row><td>dynamicenvmapscreen.cs</td><td>ISX_DEFAULTCOMPONENT125</td><td>DYNAMI~1.CS|DynamicEnvMapScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\DynamicEnvMapScreen.cs</td><td>1</td><td/></row>
		<row><td>dynamicobjectsaver.cs</td><td>ISX_DEFAULTCOMPONENT65</td><td>DYNAMI~1.CS|DynamicObjectSaver.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo8 - Serialization\DynamicObjectSaver.cs</td><td>1</td><td/></row>
		<row><td>dynenvmaterial.cs</td><td>ISX_DEFAULTCOMPONENT126</td><td>DYNENV~1.CS|DynEnvMaterial.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\helper\DynEnvMaterial.cs</td><td>1</td><td/></row>
		<row><td>dynenvmaterial.cs1</td><td>ISX_DEFAULTCOMPONENT92</td><td>DYNENV~1.CS|DynEnvMaterial.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\helper\DynEnvMaterial.cs</td><td>1</td><td/></row>
		<row><td>effect2.fx</td><td>ISX_DEFAULTCOMPONENT104</td><td>effect2.fx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Effects\effect2.fx</td><td>1</td><td/></row>
		<row><td>effect2.xnb</td><td>ISX_DEFAULTCOMPONENT8</td><td>effect2.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Effects\effect2.xnb</td><td>1</td><td/></row>
		<row><td>emtransparencyshader.fx</td><td>ISX_DEFAULTCOMPONENT104</td><td>EMTRAN~1.FX|EMTransparencySHADER.fx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Effects\EMTransparencySHADER.fx</td><td>1</td><td/></row>
		<row><td>emtransparencyshader.xnb</td><td>ISX_DEFAULTCOMPONENT8</td><td>EMTRAN~1.XNB|EMTransparencySHADER.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Effects\EMTransparencySHADER.xnb</td><td>1</td><td/></row>
		<row><td>enemybeast.fbx</td><td>ISX_DEFAULTCOMPONENT134</td><td>ENEMYB~1.FBX|EnemyBeast.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\EnemyBeast.fbx</td><td>1</td><td/></row>
		<row><td>enemybeastdiffuse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>ENEMYB~1.JPG|EnemyBeastDiffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\EnemyBeastDiffuse.jpg</td><td>1</td><td/></row>
		<row><td>engine_diff_tex.tga</td><td>ISX_DEFAULTCOMPONENT105</td><td>ENGINE~1.TGA|engine_diff_tex.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\engine_diff_tex.tga</td><td>1</td><td/></row>
		<row><td>engine_diff_tex.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>ENGINE~1.XNB|engine_diff_tex.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\engine_diff_tex.xnb</td><td>1</td><td/></row>
		<row><td>engine_diff_tex_0.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>ENGINE~1.XNB|engine_diff_tex_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\engine_diff_tex_0.xnb</td><td>1</td><td/></row>
		<row><td>engine_logo.png</td><td>ISX_DEFAULTCOMPONENT83</td><td>ENGINE~1.PNG|engine_logo.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\engine_logo.png</td><td>1</td><td/></row>
		<row><td>enginesetup.cs</td><td>ISX_DEFAULTCOMPONENT117</td><td>ENGINE~1.CS|EngineSetup.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\EngineSetup.cs</td><td>1</td><td/></row>
		<row><td>environmentpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>ENVIRO~1.XAM|EnvironmentPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\EnvironmentPage.xaml</td><td>1</td><td/></row>
		<row><td>environmentpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>ENVIRO~1.CS|EnvironmentPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\EnvironmentPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>environmentplate.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>ENVIRO~1.JPG|environmentplate.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\environmentplate.jpg</td><td>1</td><td/></row>
		<row><td>envmapscreen.cs</td><td>ISX_DEFAULTCOMPONENT24</td><td>ENVMAP~1.CS|EnvMapScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo2 - Materials\EnvMapScreen.cs</td><td>1</td><td/></row>
		<row><td>explosiondebrisps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionDebrisPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionDebrisPS.cs</td><td>1</td><td/></row>
		<row><td>explosionfiresmokeps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionFireSmokePS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionFireSmokePS.cs</td><td>1</td><td/></row>
		<row><td>explosionflash.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionFlash.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionFlash.cs</td><td>1</td><td/></row>
		<row><td>explosionflyingsparksps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionFlyingSparksPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionFlyingSparksPS.cs</td><td>1</td><td/></row>
		<row><td>explosionparticles.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>EXPLOS~1.PNG|ExplosionParticles.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\ExplosionParticles.png</td><td>1</td><td/></row>
		<row><td>explosionparticles.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>EXPLOS~1.XNB|ExplosionParticles.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\ExplosionParticles.xnb</td><td>1</td><td/></row>
		<row><td>explosionps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionPS.cs</td><td>1</td><td/></row>
		<row><td>explosionroundsparksps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionRoundSparksPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionRoundSparksPS.cs</td><td>1</td><td/></row>
		<row><td>explosionscreen.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionScreen.cs</td><td>1</td><td/></row>
		<row><td>explosionshockwaveps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionShockwavePS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionShockwavePS.cs</td><td>1</td><td/></row>
		<row><td>explosionsmoketrailsps.cs</td><td>ISX_DEFAULTCOMPONENT40</td><td>EXPLOS~1.CS|ExplosionSmokeTrailsPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.6 Explosion\ExplosionSmokeTrailsPS.cs</td><td>1</td><td/></row>
		<row><td>fabric.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>fabric.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\fabric.jpg</td><td>1</td><td/></row>
		<row><td>fabric.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>fabric.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\fabric.xnb</td><td>1</td><td/></row>
		<row><td>farseerphysicsxna.dll</td><td>FarseerPhysicsXNA.dll</td><td>FARSEE~1.DLL|FarseerPhysicsXNA.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\FarseerPhysicsXNA.dll</td><td>1</td><td/></row>
		<row><td>farseerphysicsxna.dll1</td><td>FarseerPhysicsXNA.dll1</td><td>FARSEE~1.DLL|FarseerPhysicsXNA.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\FarseerPhysicsXNA.dll</td><td>1</td><td/></row>
		<row><td>farseerphysicsxna.xml</td><td>ISX_DEFAULTCOMPONENT137</td><td>FARSEE~1.XML|FarseerPhysicsXNA.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\FarseerPhysicsXNA.XML</td><td>1</td><td/></row>
		<row><td>farseerphysicsxna.xml1</td><td>ISX_DEFAULTCOMPONENT96</td><td>FARSEE~1.XML|FarseerPhysicsXNA.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\FarseerPhysicsXNA.XML</td><td>1</td><td/></row>
		<row><td>fguiscreen.cs</td><td>ISX_DEFAULTCOMPONENT33</td><td>FGUISC~1.CS|FGUIScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo8 - GUI\FGUIScreen.cs</td><td>1</td><td/></row>
		<row><td>fire.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>Fire.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\Fire.png</td><td>1</td><td/></row>
		<row><td>fire.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>Fire.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\Fire.xnb</td><td>1</td><td/></row>
		<row><td>firstscreen.cs</td><td>ISX_DEFAULTCOMPONENT85</td><td>FIRSTS~1.CS|FirstScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\FirstScreen.cs</td><td>1</td><td/></row>
		<row><td>flagcloth28screen.cs</td><td>ISX_DEFAULTCOMPONENT45</td><td>FLAGCL~1.CS|FlagCloth28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\Screens\FlagCloth28Screen.cs</td><td>1</td><td/></row>
		<row><td>fluidmaterial.cs</td><td>ISX_DEFAULTCOMPONENT46</td><td>FLUIDM~1.CS|FluidMaterial.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Fluids\FluidMaterial.cs</td><td>1</td><td/></row>
		<row><td>fluids28screen.cs</td><td>ISX_DEFAULTCOMPONENT46</td><td>FLUIDS~1.CS|Fluids28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Fluids\Fluids28Screen.cs</td><td>1</td><td/></row>
		<row><td>folha.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>folha.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\folha.jpg</td><td>1</td><td/></row>
		<row><td>folha.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>folha.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\folha.xnb</td><td>1</td><td/></row>
		<row><td>folha_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>folha_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\folha_0.xnb</td><td>1</td><td/></row>
		<row><td>followerobjectsoundscreen.cs</td><td>ISX_DEFAULTCOMPONENT25</td><td>FOLLOW~1.CS|FollowerObjectSoundScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo3 - Sound\FollowerObjectSoundScreen.cs</td><td>1</td><td/></row>
		<row><td>forwardemtransparentshader.c</td><td>ISX_DEFAULTCOMPONENT41</td><td>FORWAR~1.CS|ForwardEMTransparentShader.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.7 Stealth\ForwardEMTransparentShader.cs</td><td>1</td><td/></row>
		<row><td>forwardskinnedshader.cs</td><td>ISX_DEFAULTCOMPONENT31</td><td>FORWAR~1.CS|ForwardSkinnedShader.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\CustomSkinnedAnimation\ForwardSkinnedShader.cs</td><td>1</td><td/></row>
		<row><td>fsmscreen.cs</td><td>ISX_DEFAULTCOMPONENT68</td><td>FSMSCR~1.CS|FSMScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\FSM\FSMScreen.cs</td><td>1</td><td/></row>
		<row><td>fullanimatedmodel.cs</td><td>ISX_DEFAULTCOMPONENT32</td><td>FULLAN~1.CS|FullAnimatedModel.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\ModelAnim\FullAnimatedModel.cs</td><td>1</td><td/></row>
		<row><td>game.ico</td><td>ISX_DEFAULTCOMPONENT21</td><td>Game.ico</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Game.ico</td><td>1</td><td/></row>
		<row><td>game.ico1</td><td>ISX_DEFAULTCOMPONENT54</td><td>Game.ico</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Game.ico</td><td>1</td><td/></row>
		<row><td>game.ico2</td><td>ISX_DEFAULTCOMPONENT117</td><td>Game.ico</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Game.ico</td><td>1</td><td/></row>
		<row><td>gamepage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>GAMEPA~1.XAM|GamePage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GamePage.xaml</td><td>1</td><td/></row>
		<row><td>gamepage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>GAMEPA~1.CS|GamePage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GamePage.xaml.cs</td><td>1</td><td/></row>
		<row><td>gamethumbnail.png</td><td>ISX_DEFAULTCOMPONENT21</td><td>GAMETH~1.PNG|GameThumbnail.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\GameThumbnail.png</td><td>1</td><td/></row>
		<row><td>gamethumbnail.png1</td><td>ISX_DEFAULTCOMPONENT54</td><td>GAMETH~1.PNG|GameThumbnail.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\GameThumbnail.png</td><td>1</td><td/></row>
		<row><td>gamethumbnail.png2</td><td>ISX_DEFAULTCOMPONENT117</td><td>GAMETH~1.PNG|GameThumbnail.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\GameThumbnail.png</td><td>1</td><td/></row>
		<row><td>geoclipmap.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>GEOCLI~1.JPG|GeoClipMap.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GeoClipMap.jpg</td><td>1</td><td/></row>
		<row><td>geoclipterrainpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>GEOCLI~1.XAM|GeoClipTerrainPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GeoClipTerrainPage.xaml</td><td>1</td><td/></row>
		<row><td>geoclipterrainpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>GEOCLI~1.CS|GeoClipTerrainPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GeoClipTerrainPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>globals.cs</td><td>ISX_DEFAULTCOMPONENT73</td><td>Globals.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Steering\Globals.cs</td><td>1</td><td/></row>
		<row><td>goo.png</td><td>ISX_DEFAULTCOMPONENT136</td><td>goo.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\goo.png</td><td>1</td><td/></row>
		<row><td>goo.png1</td><td>ISX_DEFAULTCOMPONENT130</td><td>goo.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\goo.png</td><td>1</td><td/></row>
		<row><td>goo.png2</td><td>ISX_DEFAULTCOMPONENT114</td><td>goo.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\goo.png</td><td>1</td><td/></row>
		<row><td>goo.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>goo.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\goo.xnb</td><td>1</td><td/></row>
		<row><td>grama1.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>grama1.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\grama1.png</td><td>1</td><td/></row>
		<row><td>grama1.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>grama1.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\grama1.xnb</td><td>1</td><td/></row>
		<row><td>grama2.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>grama2.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\grama2.png</td><td>1</td><td/></row>
		<row><td>grama2.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>grama2.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\grama2.xnb</td><td>1</td><td/></row>
		<row><td>graphicsapp1.csproj</td><td>ISX_DEFAULTCOMPONENT83</td><td>GRAPHI~1.CSP|GraphicsApp1.csproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GraphicsApp1.csproj</td><td>1</td><td/></row>
		<row><td>graphicsapp1.csproj.user</td><td>ISX_DEFAULTCOMPONENT83</td><td>GRAPHI~1.USE|GraphicsApp1.csproj.user</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GraphicsApp1.csproj.user</td><td>1</td><td/></row>
		<row><td>graphicsapp1.sln</td><td>ISX_DEFAULTCOMPONENT82</td><td>GRAPHI~1.SLN|GraphicsApp1.sln</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1.sln</td><td>1</td><td/></row>
		<row><td>graphicsapp1.suo</td><td>ISX_DEFAULTCOMPONENT82</td><td>GRAPHI~1.SUO|GraphicsApp1.suo</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1.suo</td><td>1</td><td/></row>
		<row><td>graphicsapp1library.csproj</td><td>ISX_DEFAULTCOMPONENT85</td><td>GRAPHI~2.CSP|GraphicsApp1Library.csproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\GraphicsApp1Library.csproj</td><td>1</td><td/></row>
		<row><td>graphicsapp1library.csproj.d</td><td>ISX_DEFAULTCOMPONENT85</td><td>GRAPHI~1.CAC|GraphicsApp1Library.csproj.Debug.cachefile</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\GraphicsApp1Library.csproj.Debug.cachefile</td><td>1</td><td/></row>
		<row><td>graphicsapp1library.csproj.r</td><td>ISX_DEFAULTCOMPONENT85</td><td>GRAPHI~2.CAC|GraphicsApp1Library.csproj.Release.cachefile</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\GraphicsApp1Library.csproj.Release.cachefile</td><td>1</td><td/></row>
		<row><td>graphicsapp1librarycontent.c</td><td>ISX_DEFAULTCOMPONENT133</td><td>GRAPHI~1.CON|GraphicsApp1LibraryContent.contentproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\GraphicsApp1LibraryContent.contentproj</td><td>1</td><td/></row>
		<row><td>grasscube.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>GRASSC~1.DDS|grassCUBE.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\grassCUBE.dds</td><td>1</td><td/></row>
		<row><td>grasscube.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>GRASSC~1.XNB|grassCUBE.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\grassCUBE.xnb</td><td>1</td><td/></row>
		<row><td>gravitation.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>GRAVIT~1.JPG|Gravitation.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Gravitation.jpg</td><td>1</td><td/></row>
		<row><td>gravitationalbepuscreen.cs</td><td>ISX_DEFAULTCOMPONENT60</td><td>GRAVIT~1.CS|GravitationalBepuScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo4 - Physics\GravitationalBepuScreen.cs</td><td>1</td><td/></row>
		<row><td>gravitationalbepuscreen.cs1</td><td>ISX_DEFAULTCOMPONENT91</td><td>GRAVIT~1.CS|GravitationalBepuScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\GravitationalDemo\GravitationalBepuScreen.cs</td><td>1</td><td/></row>
		<row><td>gravitationpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>GRAVIT~1.XAM|GravitationPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GravitationPage.xaml</td><td>1</td><td/></row>
		<row><td>gravitationpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>GRAVIT~2.CS|GravitationPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\GravitationPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>groupbox.flat.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>GROUPB~1.PNG|GroupBox.Flat.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\GroupBox.Flat.png</td><td>1</td><td/></row>
		<row><td>groupbox.flat.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>GROUPB~1.XNB|GroupBox.Flat.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\GroupBox.Flat.xnb</td><td>1</td><td/></row>
		<row><td>groupbox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>GroupBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\GroupBox.png</td><td>1</td><td/></row>
		<row><td>groupbox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>GroupBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\GroupBox.xnb</td><td>1</td><td/></row>
		<row><td>grouppanel.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>GROUPP~1.PNG|GroupPanel.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\GroupPanel.png</td><td>1</td><td/></row>
		<row><td>grouppanel.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>GROUPP~1.XNB|GroupPanel.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\GroupPanel.xnb</td><td>1</td><td/></row>
		<row><td>head.tga</td><td>ISX_DEFAULTCOMPONENT106</td><td>head.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\Dude\head.tga</td><td>1</td><td/></row>
		<row><td>head.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>head.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\head.xnb</td><td>1</td><td/></row>
		<row><td>head_0.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>head_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\head_0.xnb</td><td>1</td><td/></row>
		<row><td>height_map.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>HEIGHT~1.DDS|height_map.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\height_map.dds</td><td>1</td><td/></row>
		<row><td>height_map.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>HEIGHT~1.XNB|height_map.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\height_map.xnb</td><td>1</td><td/></row>
		<row><td>heightmap.raw</td><td>ISX_DEFAULTCOMPONENT129</td><td>HEIGHT~1.RAW|HeightMap.raw</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Terrain\HeightMap.raw</td><td>1</td><td/></row>
		<row><td>heightmap.raw1</td><td>ISX_DEFAULTCOMPONENT135</td><td>HEIGHT~1.RAW|HeightMap.raw</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Terrain\HeightMap.raw</td><td>1</td><td/></row>
		<row><td>hmap.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>hmap.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\hmap.jpg</td><td>1</td><td/></row>
		<row><td>horizontal.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>HORIZO~1.CUR|Horizontal.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Horizontal.cur</td><td>1</td><td/></row>
		<row><td>horizontal.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>HORIZO~1.XNB|Horizontal.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Horizontal.xnb</td><td>1</td><td/></row>
		<row><td>iapathfindingscreen.cs</td><td>ISX_DEFAULTCOMPONENT74</td><td>IAPATH~1.CS|IaPathFindingScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Waypoints\IaPathFindingScreen.cs</td><td>1</td><td/></row>
		<row><td>iawaypointsscreen.cs</td><td>ISX_DEFAULTCOMPONENT74</td><td>IAWAYP~1.CS|IaWaypointsScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Waypoints\IaWaypointsScreen.cs</td><td>1</td><td/></row>
		<row><td>icon.error.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ICONER~1.PNG|Icon.Error.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Icon.Error.png</td><td>1</td><td/></row>
		<row><td>icon.error.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ICONER~1.XNB|Icon.Error.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Icon.Error.xnb</td><td>1</td><td/></row>
		<row><td>icon.information.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ICONIN~1.PNG|Icon.Information.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Icon.Information.png</td><td>1</td><td/></row>
		<row><td>icon.information.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ICONIN~1.XNB|Icon.Information.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Icon.Information.xnb</td><td>1</td><td/></row>
		<row><td>icon.question.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ICONQU~1.PNG|Icon.Question.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Icon.Question.png</td><td>1</td><td/></row>
		<row><td>icon.question.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ICONQU~1.XNB|Icon.Question.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Icon.Question.xnb</td><td>1</td><td/></row>
		<row><td>icon.warning.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ICONWA~1.PNG|Icon.Warning.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Icon.Warning.png</td><td>1</td><td/></row>
		<row><td>icon.warning.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ICONWA~1.XNB|Icon.Warning.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Icon.Warning.xnb</td><td>1</td><td/></row>
		<row><td>ilha.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>ilha.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\ilha.X</td><td>1</td><td/></row>
		<row><td>ilha.xml</td><td>ISX_DEFAULTCOMPONENT11</td><td>ilha.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\ModelInfos\ilha.xml</td><td>1</td><td/></row>
		<row><td>ilha.xml1</td><td>ISX_DEFAULTCOMPONENT107</td><td>ilha.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\ModelInfos\ilha.xml</td><td>1</td><td/></row>
		<row><td>ilha.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>ilha.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\ilha.xnb</td><td>1</td><td/></row>
		<row><td>instancedbilboardscreen.cs</td><td>ISX_DEFAULTCOMPONENT29</td><td>INSTAN~1.CS|InstancedBilboardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo6 - BilboardsSamples\InstancedBilboardScreen.cs</td><td>1</td><td/></row>
		<row><td>interscripttalking.cs</td><td>ISX_DEFAULTCOMPONENT64</td><td>INTERS~1.CS|InterScriptTalking.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo7 - Scripts\InterScriptTalking.cs</td><td>1</td><td/></row>
		<row><td>introductiondemo4.0.csproj</td><td>ISX_DEFAULTCOMPONENT54</td><td>INTROD~1.CSP|IntroductionDemo4.0.csproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\IntroductionDemo4.0.csproj</td><td>1</td><td/></row>
		<row><td>introductiondemo4.0.csproj.d</td><td>ISX_DEFAULTCOMPONENT54</td><td>INTROD~1.CAC|IntroductionDemo4.0.csproj.Debug.cachefile</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\IntroductionDemo4.0.csproj.Debug.cachefile</td><td>1</td><td/></row>
		<row><td>introductiondemo4.0.csproj.u</td><td>ISX_DEFAULTCOMPONENT54</td><td>INTROD~1.USE|IntroductionDemo4.0.csproj.user</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\IntroductionDemo4.0.csproj.user</td><td>1</td><td/></row>
		<row><td>introductiondemo4.0.sln</td><td>ISX_DEFAULTCOMPONENT51</td><td>INTROD~1.SLN|IntroductionDemo4.0.sln</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0.sln</td><td>1</td><td/></row>
		<row><td>introductiondemo4.0.suo</td><td>ISX_DEFAULTCOMPONENT51</td><td>INTROD~1.SUO|IntroductionDemo4.0.suo</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0.suo</td><td>1</td><td/></row>
		<row><td>introductiondemo4.0content.c</td><td>ISX_DEFAULTCOMPONENT77</td><td>INTROD~1.CON|IntroductionDemo4.0Content.contentproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\IntroductionDemo4.0Content.contentproj</td><td>1</td><td/></row>
		<row><td>iscenebuilder.cs</td><td>ISX_DEFAULTCOMPONENT64</td><td>ISCENE~1.CS|ISceneBuilder.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo7 - Scripts\ISceneBuilder.cs</td><td>1</td><td/></row>
		<row><td>iscripthelper.cs</td><td>ISX_DEFAULTCOMPONENT64</td><td>ISCRIP~1.CS|IScriptHelper.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo7 - Scripts\IScriptHelper.cs</td><td>1</td><td/></row>
		<row><td>islandmap.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>ISLAND~1.PNG|islandmap.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\islandmap.png</td><td>1</td><td/></row>
		<row><td>islandmap.png1</td><td>ISX_DEFAULTCOMPONENT81</td><td>ISLAND~1.PNG|islandmap.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Textures\islandmap.png</td><td>1</td><td/></row>
		<row><td>islandmap.png2</td><td>ISX_DEFAULTCOMPONENT136</td><td>ISLAND~1.PNG|islandmap.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\islandmap.png</td><td>1</td><td/></row>
		<row><td>islandmap.png3</td><td>ISX_DEFAULTCOMPONENT130</td><td>ISLAND~1.PNG|islandmap.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\islandmap.png</td><td>1</td><td/></row>
		<row><td>islandmap.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>ISLAND~1.XNB|islandmap.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\islandmap.xnb</td><td>1</td><td/></row>
		<row><td>islandmap_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>ISLAND~1.XNB|islandmap_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\islandmap_0.xnb</td><td>1</td><td/></row>
		<row><td>jacket.tga</td><td>ISX_DEFAULTCOMPONENT106</td><td>jacket.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\Dude\jacket.tga</td><td>1</td><td/></row>
		<row><td>jacket.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>jacket.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\jacket.xnb</td><td>1</td><td/></row>
		<row><td>jacket_0.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>jacket_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\jacket_0.xnb</td><td>1</td><td/></row>
		<row><td>jeep1.jpg</td><td>ISX_DEFAULTCOMPONENT127</td><td>jeep1.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\jeep1.jpg</td><td>1</td><td/></row>
		<row><td>jointupdateable.cs</td><td>ISX_DEFAULTCOMPONENT87</td><td>JOINTU~1.CS|JointUpdateable.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Helpers\JointUpdateable.cs</td><td>1</td><td/></row>
		<row><td>jointupdateable.cs1</td><td>ISX_DEFAULTCOMPONENT119</td><td>JOINTU~1.CS|JointUpdateable.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Helpers\JointUpdateable.cs</td><td>1</td><td/></row>
		<row><td>keyboardinputscreen.cs</td><td>ISX_DEFAULTCOMPONENT57</td><td>KEYBOA~1.CS|KeyboardInputScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo2 - Input\KeyboardInputScreen.cs</td><td>1</td><td/></row>
		<row><td>kwxport_1.5.0_win32.msi</td><td>ISX_DEFAULTCOMPONENT99</td><td>KWXPOR~1.MSI|kwxport-1.5.0-win32.msi</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Extra\kwxport-1.5.0-win32.msi</td><td>1</td><td/></row>
		<row><td>lampada_occlusion_occlusion.</td><td>ISX_DEFAULTCOMPONENT136</td><td>LAMPAD~1.JPG|lampada_occlusion_occlusion.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\lampada_occlusion_occlusion.jpg</td><td>1</td><td/></row>
		<row><td>lcyl_l_occlusion.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>LCYL_L~1.JPG|lcyl_l_occlusion.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\lcyl_l_occlusion.jpg</td><td>1</td><td/></row>
		<row><td>leonscene.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>LEONSC~1.X|leonscene.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\leonscene.X</td><td>1</td><td/></row>
		<row><td>leonscene.xml</td><td>ISX_DEFAULTCOMPONENT11</td><td>LEONSC~1.XML|leonScene.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\ModelInfos\leonScene.xml</td><td>1</td><td/></row>
		<row><td>leonscene.xml1</td><td>ISX_DEFAULTCOMPONENT107</td><td>LEONSC~1.XML|leonScene.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\ModelInfos\leonScene.xml</td><td>1</td><td/></row>
		<row><td>leonscene.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>LEONSC~1.XNB|leonscene.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\leonscene.xnb</td><td>1</td><td/></row>
		<row><td>lightinterpolationscreen.cs</td><td>ISX_DEFAULTCOMPONENT58</td><td>LIGHTI~1.CS|LightInterpolationScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo3 - Lights\LightInterpolationScreen.cs</td><td>1</td><td/></row>
		<row><td>lightthrowbepu.cs</td><td>ISX_DEFAULTCOMPONENT22</td><td>LIGHTT~1.CS|LightThrowBepu.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Commons\LightThrowBepu.cs</td><td>1</td><td/></row>
		<row><td>lightthrowbepu.cs1</td><td>ISX_DEFAULTCOMPONENT55</td><td>LIGHTT~1.CS|LightThrowBepu.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Commons\LightThrowBepu.cs</td><td>1</td><td/></row>
		<row><td>lion.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>lion.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\lion.tga</td><td>1</td><td/></row>
		<row><td>lion.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>lion.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\lion.xnb</td><td>1</td><td/></row>
		<row><td>lion_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>lion_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\lion_0.xnb</td><td>1</td><td/></row>
		<row><td>lion_ddn.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>lion_ddn.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\lion_ddn.tga</td><td>1</td><td/></row>
		<row><td>lion_ddn.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>lion_ddn.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\lion_ddn.xnb</td><td>1</td><td/></row>
		<row><td>listbox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ListBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ListBox.png</td><td>1</td><td/></row>
		<row><td>listbox.selection.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>LISTBO~1.PNG|ListBox.Selection.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ListBox.Selection.png</td><td>1</td><td/></row>
		<row><td>listbox.selection.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>LISTBO~1.XNB|ListBox.Selection.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ListBox.Selection.xnb</td><td>1</td><td/></row>
		<row><td>listbox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ListBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ListBox.xnb</td><td>1</td><td/></row>
		<row><td>loading_screen.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>LOADIN~1.JPG|loading_screen.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\loading_screen.jpg</td><td>1</td><td/></row>
		<row><td>loading_screen.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>LOADIN~1.XNB|loading_screen.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\loading_screen.xnb</td><td>1</td><td/></row>
		<row><td>loadingscreen.cs</td><td>ISX_DEFAULTCOMPONENT21</td><td>LOADIN~1.CS|LoadingScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\LoadingScreen.cs</td><td>1</td><td/></row>
		<row><td>logo_texture.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>LOGO_T~1.JPG|logo_texture.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\logo_texture.jpg</td><td>1</td><td/></row>
		<row><td>logo_texture.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>LOGO_T~1.XNB|logo_texture.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\logo_texture.xnb</td><td>1</td><td/></row>
		<row><td>mainmenu.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>MainMenu.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\MainMenu.png</td><td>1</td><td/></row>
		<row><td>mainmenu.selection.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>MAINME~1.PNG|MainMenu.Selection.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\MainMenu.Selection.png</td><td>1</td><td/></row>
		<row><td>mainmenu.selection.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>MAINME~1.XNB|MainMenu.Selection.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\MainMenu.Selection.xnb</td><td>1</td><td/></row>
		<row><td>mainmenu.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>MainMenu.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\MainMenu.xnb</td><td>1</td><td/></row>
		<row><td>mainpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>MAINPA~1.XAM|MainPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\MainPage.xaml</td><td>1</td><td/></row>
		<row><td>mainpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>MAINPA~1.CS|MainPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\MainPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>material.jpg</td><td>ISX_DEFAULTCOMPONENT127</td><td>Material.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Material.jpg</td><td>1</td><td/></row>
		<row><td>meiofio.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>meiofio.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\meiofio.jpg</td><td>1</td><td/></row>
		<row><td>meiofio.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>meiofio.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\meiofio.xnb</td><td>1</td><td/></row>
		<row><td>merge.bat</td><td>ISX_DEFAULTCOMPONENT137</td><td>merge.bat</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\merge.bat</td><td>1</td><td/></row>
		<row><td>mergepipe.bat</td><td>ISX_DEFAULTCOMPONENT137</td><td>MERGEP~1.BAT|mergepipe.bat</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\mergepipe.bat</td><td>1</td><td/></row>
		<row><td>milkshakeformatscreen.cs</td><td>ISX_DEFAULTCOMPONENT125</td><td>MILKSH~1.CS|MilkShakeFormatScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\MilkShakeFormatScreen.cs</td><td>1</td><td/></row>
		<row><td>mobilephysicscreen.cs</td><td>ISX_DEFAULTCOMPONENT75</td><td>MOBILE~1.CS|MobilePhysicScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos Extras\MobilePhysicScreen.cs</td><td>1</td><td/></row>
		<row><td>model.ms3d</td><td>ISX_DEFAULTCOMPONENT127</td><td>MODEL~1.MS3|model.ms3d</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\model.ms3d</td><td>1</td><td/></row>
		<row><td>modelimporter.dll</td><td>ModelImporter.dll</td><td>MODELI~1.DLL|ModelImporter.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\Dlls\ModelImporter.dll</td><td>1</td><td/></row>
		<row><td>modelmanipulationpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>MODELM~1.XAM|ModelManipulationPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\ModelManipulationPage.xaml</td><td>1</td><td/></row>
		<row><td>modelmanipulationpage.xaml.c</td><td>ISX_DEFAULTCOMPONENT83</td><td>MODELM~1.CS|ModelManipulationPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\ModelManipulationPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>modelmanipulationplate.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>MODELM~1.JPG|modelmanipulationplate.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\modelmanipulationplate.jpg</td><td>1</td><td/></row>
		<row><td>motionblurposteffect.cs</td><td>ISX_DEFAULTCOMPONENT123</td><td>MOTION~1.CS|MotionBlurPostEffect.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\PostEffect\MotionBlurPostEffect.cs</td><td>1</td><td/></row>
		<row><td>motionforwardscreen.cs</td><td>ISX_DEFAULTCOMPONENT123</td><td>MOTION~1.CS|MotionForwardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\PostEffect\MotionForwardScreen.cs</td><td>1</td><td/></row>
		<row><td>move.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>Move.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Move.cur</td><td>1</td><td/></row>
		<row><td>move.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>Move.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Move.xnb</td><td>1</td><td/></row>
		<row><td>moveablepointlight.cs</td><td>ISX_DEFAULTCOMPONENT22</td><td>MOVEAB~1.CS|MoveablePointLight.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Commons\MoveablePointLight.cs</td><td>1</td><td/></row>
		<row><td>moveablepointlight.cs1</td><td>ISX_DEFAULTCOMPONENT55</td><td>MOVEAB~1.CS|MoveablePointLight.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Commons\MoveablePointLight.cs</td><td>1</td><td/></row>
		<row><td>movementscreen.cs</td><td>ISX_DEFAULTCOMPONENT60</td><td>MOVEME~1.CS|MovementScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo4 - Physics\MovementScreen.cs</td><td>1</td><td/></row>
		<row><td>moveratachment.cs</td><td>ISX_DEFAULTCOMPONENT74</td><td>MOVERA~1.CS|moverAtachment.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Waypoints\moverAtachment.cs</td><td>1</td><td/></row>
		<row><td>multscreen.cs</td><td>ISX_DEFAULTCOMPONENT75</td><td>MULTSC~1.CS|MultScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos Extras\MultScreen.cs</td><td>1</td><td/></row>
		<row><td>noisescreen.cs</td><td>ISX_DEFAULTCOMPONENT34</td><td>NOISES~1.CS|NoiseScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9 -  Noise Textures\NoiseScreen.cs</td><td>1</td><td/></row>
		<row><td>normal_map.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>NORMAL~1.DDS|normal_map.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\normal_map.dds</td><td>1</td><td/></row>
		<row><td>normal_map.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>NORMAL~1.XNB|normal_map.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\normal_map.xnb</td><td>1</td><td/></row>
		<row><td>normalbilboardscreen.cs</td><td>ISX_DEFAULTCOMPONENT29</td><td>NORMAL~1.CS|NormalBilboardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo6 - BilboardsSamples\NormalBilboardScreen.cs</td><td>1</td><td/></row>
		<row><td>nxcharacter.dll</td><td>NxCharacter.dll</td><td>NXCHAR~1.DLL|NxCharacter.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\NxCharacter.dll</td><td>1</td><td/></row>
		<row><td>nxcharacter.dll1</td><td>NxCharacter.dll1</td><td>NXCHAR~1.DLL|NxCharacter.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\NxCharacter.dll</td><td>1</td><td/></row>
		<row><td>objectmock.cs</td><td>ISX_DEFAULTCOMPONENT65</td><td>OBJECT~2.CS|ObjectMock.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo8 - Serialization\ObjectMock.cs</td><td>1</td><td/></row>
		<row><td>objectthrowsmokeparticle.cs</td><td>ISX_DEFAULTCOMPONENT28</td><td>OBJECT~1.CS|ObjectThrowSmokeParticle.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo5 - ParticleSamples\Helpers\ObjectThrowSmokeParticle.cs</td><td>1</td><td/></row>
		<row><td>oceanscreen.cs</td><td>ISX_DEFAULTCOMPONENT35</td><td>OCEANS~1.CS|OceanScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.1 Water and Ocean\OceanScreen.cs</td><td>1</td><td/></row>
		<row><td>octreescreen.cs</td><td>ISX_DEFAULTCOMPONENT38</td><td>OCTREE~1.CS|OctreeScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.4 Octree Culling\OctreeScreen.cs</td><td>1</td><td/></row>
		<row><td>opening.wmv</td><td>ISX_DEFAULTCOMPONENT7</td><td>opening.wmv</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\opening.wmv</td><td>1</td><td/></row>
		<row><td>opening.wmv1</td><td>ISX_DEFAULTCOMPONENT103</td><td>opening.wmv</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\opening.wmv</td><td>1</td><td/></row>
		<row><td>opening.wmv2</td><td>ISX_DEFAULTCOMPONENT77</td><td>opening.wmv</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\opening.wmv</td><td>1</td><td/></row>
		<row><td>opening.xnb</td><td>ISX_DEFAULTCOMPONENT7</td><td>opening.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\opening.xnb</td><td>1</td><td/></row>
		<row><td>osiris.terrain.content.pipel</td><td>Osiris.Terrain.Content.Pipeline.dll</td><td>OSIRIS~1.DLL|Osiris.Terrain.Content.Pipeline.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\Osiris.Terrain.Content.Pipeline.dll</td><td>1</td><td/></row>
		<row><td>pants.tga</td><td>ISX_DEFAULTCOMPONENT106</td><td>pants.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\Dude\pants.tga</td><td>1</td><td/></row>
		<row><td>pants.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>pants.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\pants.xnb</td><td>1</td><td/></row>
		<row><td>pants_0.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>pants_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\pants_0.xnb</td><td>1</td><td/></row>
		<row><td>paralaxscreen.cs</td><td>ISX_DEFAULTCOMPONENT24</td><td>PARALA~1.CS|ParalaxScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo2 - Materials\ParalaxScreen.cs</td><td>1</td><td/></row>
		<row><td>particle.bmp</td><td>ISX_DEFAULTCOMPONENT136</td><td>Particle.bmp</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\Particle.bmp</td><td>1</td><td/></row>
		<row><td>particle.bmp1</td><td>ISX_DEFAULTCOMPONENT130</td><td>Particle.bmp</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\Particle.bmp</td><td>1</td><td/></row>
		<row><td>particle.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>particle.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\particle.jpg</td><td>1</td><td/></row>
		<row><td>particles2d.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>PARTIC~1.JPG|particles2d.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\particles2d.jpg</td><td>1</td><td/></row>
		<row><td>particles3d.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>PARTIC~1.XAM|Particles3D.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Particles3D.xaml</td><td>1</td><td/></row>
		<row><td>particles3d.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>PARTIC~2.CS|Particles3D.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Particles3D.xaml.cs</td><td>1</td><td/></row>
		<row><td>particlescreen.cs</td><td>ISX_DEFAULTCOMPONENT27</td><td>PARTIC~1.CS|ParticleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo5 - ParticleSamples\ParticleScreen.cs</td><td>1</td><td/></row>
		<row><td>particlescreen.cs1</td><td>ISX_DEFAULTCOMPONENT88</td><td>PARTIC~1.CS|ParticleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\ParticleScreen.cs</td><td>1</td><td/></row>
		<row><td>particlescreen.cs2</td><td>ISX_DEFAULTCOMPONENT122</td><td>PARTIC~1.CS|ParticleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Particle3D\ParticleScreen.cs</td><td>1</td><td/></row>
		<row><td>pedestrian.cs</td><td>ISX_DEFAULTCOMPONENT73</td><td>PEDEST~1.CS|Pedestrian.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Steering\Pedestrian.cs</td><td>1</td><td/></row>
		<row><td>pedestrianplugin.cs</td><td>ISX_DEFAULTCOMPONENT73</td><td>PEDEST~1.CS|PedestrianPlugIn.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Steering\PedestrianPlugIn.cs</td><td>1</td><td/></row>
		<row><td>perlin.fx</td><td>ISX_DEFAULTCOMPONENT104</td><td>perlin.fx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Effects\perlin.fx</td><td>1</td><td/></row>
		<row><td>perlin.xnb</td><td>ISX_DEFAULTCOMPONENT8</td><td>perlin.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Effects\perlin.xnb</td><td>1</td><td/></row>
		<row><td>perlinnoisescreen.cs</td><td>ISX_DEFAULTCOMPONENT34</td><td>PERLIN~1.CS|PerlinNoiseScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9 -  Noise Textures\PerlinNoiseScreen.cs</td><td>1</td><td/></row>
		<row><td>physics2d.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>PHYSIC~1.JPG|physics2d.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\physics2d.jpg</td><td>1</td><td/></row>
		<row><td>physicsselection.ms</td><td>ISX_DEFAULTCOMPONENT98</td><td>PHYSIC~1.MS|physicsselection.ms</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Extra\ExporterScripts\physicsselection.ms</td><td>1</td><td/></row>
		<row><td>physx28materialscreen.cs</td><td>ISX_DEFAULTCOMPONENT47</td><td>PHYSX2~1.CS|Physx28MaterialScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Material\Physx28MaterialScreen.cs</td><td>1</td><td/></row>
		<row><td>physx28screen.cs</td><td>ISX_DEFAULTCOMPONENT42</td><td>PHYSX2~1.CS|Physx28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Physx28Screen.cs</td><td>1</td><td/></row>
		<row><td>physx28triggerscreen.cs</td><td>ISX_DEFAULTCOMPONENT48</td><td>PHYSX2~1.CS|Physx28TriggerScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Trigger\Physx28TriggerScreen.cs</td><td>1</td><td/></row>
		<row><td>physx28vehiclescreen.cs</td><td>ISX_DEFAULTCOMPONENT49</td><td>PHYSX2~1.CS|Physx28VehicleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Vehicle\Physx28VehicleScreen.cs</td><td>1</td><td/></row>
		<row><td>physxcharacter28screen.cs</td><td>ISX_DEFAULTCOMPONENT43</td><td>PHYSXC~1.CS|PhysxCharacter28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Character\PhysxCharacter28Screen.cs</td><td>1</td><td/></row>
		<row><td>physxcooking.dll</td><td>PhysXCooking.dll</td><td>PHYSXC~1.DLL|PhysXCooking.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\PhysXCooking.dll</td><td>1</td><td/></row>
		<row><td>physxcooking.dll1</td><td>PhysXCooking.dll1</td><td>PHYSXC~1.DLL|PhysXCooking.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\PhysXCooking.dll</td><td>1</td><td/></row>
		<row><td>physxcore.dll</td><td>PhysXCore.dll</td><td>PHYSXC~1.DLL|PhysXCore.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\PhysXCore.dll</td><td>1</td><td/></row>
		<row><td>physxcore.dll1</td><td>PhysXCore.dll1</td><td>PHYSXC~1.DLL|PhysXCore.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\PhysXCore.dll</td><td>1</td><td/></row>
		<row><td>physxdevice.dll</td><td>PhysXDevice.dll</td><td>PHYSXD~1.DLL|PhysXDevice.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\PhysXDevice.dll</td><td>1</td><td/></row>
		<row><td>physxdevice.dll1</td><td>PhysXDevice.dll1</td><td>PHYSXD~1.DLL|PhysXDevice.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\PhysXDevice.dll</td><td>1</td><td/></row>
		<row><td>physxloader.dll</td><td>PhysXLoader.dll</td><td>PHYSXL~1.DLL|PhysXLoader.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\PhysXLoader.dll</td><td>1</td><td/></row>
		<row><td>physxloader.dll1</td><td>PhysXLoader.dll1</td><td>PHYSXL~1.DLL|PhysXLoader.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\PhysXLoader.dll</td><td>1</td><td/></row>
		<row><td>pianosong.mp3</td><td>ISX_DEFAULTCOMPONENT113</td><td>PIANOS~1.MP3|pianosong.mp3</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Songs\pianosong.mp3</td><td>1</td><td/></row>
		<row><td>pianosong.xnb</td><td>ISX_DEFAULTCOMPONENT17</td><td>PIANOS~1.XNB|pianosong.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Songs\pianosong.xnb</td><td>1</td><td/></row>
		<row><td>pickingscreen.cs</td><td>ISX_DEFAULTCOMPONENT75</td><td>PICKIN~1.CS|PickingScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demos Extras\PickingScreen.cs</td><td>1</td><td/></row>
		<row><td>pillar_texture.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>PILLAR~1.JPG|pillar_texture.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\pillar_texture.jpg</td><td>1</td><td/></row>
		<row><td>pipes.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>pipes.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\pipes.jpg</td><td>1</td><td/></row>
		<row><td>piso_core_difusse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>PISO_C~1.JPG|piso_core_difusse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\piso_core_difusse.jpg</td><td>1</td><td/></row>
		<row><td>piso_diffuse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>PISO_D~1.JPG|piso_diffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\piso_diffuse.jpg</td><td>1</td><td/></row>
		<row><td>playermarine.fbx</td><td>ISX_DEFAULTCOMPONENT105</td><td>PLAYER~1.FBX|PlayerMarine.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\PlayerMarine.fbx</td><td>1</td><td/></row>
		<row><td>playermarine.fbx1</td><td>ISX_DEFAULTCOMPONENT134</td><td>PLAYER~1.FBX|PlayerMarine.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\PlayerMarine.fbx</td><td>1</td><td/></row>
		<row><td>playermarine.fbx2</td><td>ISX_DEFAULTCOMPONENT128</td><td>PLAYER~1.FBX|PlayerMarine.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Model\PlayerMarine.fbx</td><td>1</td><td/></row>
		<row><td>playermarine.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>PLAYER~1.XNB|PlayerMarine.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\PlayerMarine.xnb</td><td>1</td><td/></row>
		<row><td>playermarinediffuse.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>PLAYER~1.JPG|PlayerMarineDiffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\PlayerMarineDiffuse.jpg</td><td>1</td><td/></row>
		<row><td>playermarinediffuse.jpg1</td><td>ISX_DEFAULTCOMPONENT136</td><td>PLAYER~1.JPG|PlayerMarineDiffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\PlayerMarineDiffuse.jpg</td><td>1</td><td/></row>
		<row><td>playermarinediffuse.jpg2</td><td>ISX_DEFAULTCOMPONENT130</td><td>PLAYER~1.JPG|PlayerMarineDiffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\PlayerMarineDiffuse.jpg</td><td>1</td><td/></row>
		<row><td>playermarinediffuse.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>PLAYER~1.XNB|PlayerMarineDiffuse.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\PlayerMarineDiffuse.xnb</td><td>1</td><td/></row>
		<row><td>ploobs_engine_help.chm</td><td>Ploobs_Engine_Help.chm</td><td>PLOOBS~1.CHM|Ploobs Engine Help.chm</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Help\Ploobs Engine Help.chm</td><td>1</td><td/></row>
		<row><td>ploobsengine2d_project_templ</td><td>ISX_DEFAULTCOMPONENT4</td><td>PLOOBS~1.ZIP|PloobsEngine2D Project Template.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine2D Project Template.zip</td><td>1</td><td/></row>
		<row><td>ploobsengine_2d_template.zip</td><td>ISX_DEFAULTCOMPONENT5</td><td>PLOOBS~1.ZIP|PloobsEngine 2D Template.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine 2D Template.zip</td><td>1</td><td/></row>
		<row><td>ploobsengine_deferred_screen</td><td>ISX_DEFAULTCOMPONENT5</td><td>PLOOBS~1.ZIP|PloobsEngine Deferred Screen Template.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine Deferred Screen Template.zip</td><td>1</td><td/></row>
		<row><td>ploobsengine_forward_screen_</td><td>ISX_DEFAULTCOMPONENT5</td><td>PLOOBS~1.ZIP|PloobsEngine Forward Screen Template.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine Forward Screen Template.zip</td><td>1</td><td/></row>
		<row><td>ploobsengine_project_reach_t</td><td>ISX_DEFAULTCOMPONENT4</td><td>PLOOBS~1.ZIP|PloobsEngine Project Reach Template.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine Project Reach Template.zip</td><td>1</td><td/></row>
		<row><td>ploobsengine_project_templat</td><td>ISX_DEFAULTCOMPONENT4</td><td>PLOOBS~1.ZIP|PloobsEngine Project Template.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine Project Template.zip</td><td>1</td><td/></row>
		<row><td>ploobsengine_wp7_silverlight</td><td>ISX_DEFAULTCOMPONENT4</td><td>PLOOBS~1.ZIP|PloobsEngine WP7 SilverLight XNA.zip</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Templates\PloobsEngine WP7 SilverLight XNA.zip</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.dll</td><td>PloobsEngineDebug.dll</td><td>PLOOBS~1.DLL|PloobsEngineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\PloobsEngineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.dll1</td><td>PloobsEngineDebug.dll1</td><td>PLOOBS~1.DLL|PloobsEngineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\Dll\PloobsEngineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.dll2</td><td>PloobsEngineDebug.dll2</td><td>PLOOBS~1.DLL|PloobsEngineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\Dll\PloobsEngineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.dll3</td><td>PloobsEngineDebug.dll3</td><td>PLOOBS~1.DLL|PloobsEngineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\Dlls\PloobsEngineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.dll4</td><td>PloobsEngineDebug.dll4</td><td>PLOOBS~1.DLL|PloobsEngineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineHiDef\PloobsEngineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.dll5</td><td>PloobsEngineDebug.dll5</td><td>PLOOBS~1.DLL|PloobsEngineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineReach\PloobsEngineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.xml1</td><td>ISX_DEFAULTCOMPONENT115</td><td>PLOOBS~1.XML|PloobsEngineDebug.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\Dll\PloobsEngineDebug.xml</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.xml2</td><td>ISX_DEFAULTCOMPONENT53</td><td>PLOOBS~1.XML|PloobsEngineDebug.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\Dll\PloobsEngineDebug.xml</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.xml3</td><td>ISX_DEFAULTCOMPONENT102</td><td>PLOOBS~1.XML|PloobsEngineDebug.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\Dlls\PloobsEngineDebug.xml</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.xml4</td><td>ISX_DEFAULTCOMPONENT95</td><td>PLOOBS~1.XML|PloobsEngineDebug.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineHiDef\PloobsEngineDebug.xml</td><td>1</td><td/></row>
		<row><td>ploobsenginedebug.xml5</td><td>ISX_DEFAULTCOMPONENT97</td><td>PLOOBS~1.XML|PloobsEngineDebug.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineReach\PloobsEngineDebug.xml</td><td>1</td><td/></row>
		<row><td>ploobsenginephone7.dll</td><td>PloobsEnginePhone7.dll</td><td>PLOOBS~3.DLL|PloobsEnginePhone7.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\PloobsEnginePhone7.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginephone7.dll1</td><td>PloobsEnginePhone7.dll1</td><td>PLOOBS~1.DLL|PloobsEnginePhone7.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\PloobsEnginePhone7.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginephone7.pdb</td><td>ISX_DEFAULTCOMPONENT137</td><td>PLOOBS~1.PDB|PloobsEnginePhone7.pdb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\PloobsEnginePhone7.pdb</td><td>1</td><td/></row>
		<row><td>ploobsenginephone7.xml</td><td>ISX_DEFAULTCOMPONENT137</td><td>PLOOBS~2.XML|PloobsEnginePhone7.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\PloobsEnginePhone7.XML</td><td>1</td><td/></row>
		<row><td>ploobsenginephone7.xml1</td><td>ISX_DEFAULTCOMPONENT96</td><td>PLOOBS~1.XML|PloobsEnginePhone7.XML</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\PloobsEnginePhone7.XML</td><td>1</td><td/></row>
		<row><td>ploobsenginepipelinedebug.dl</td><td>PloobsEnginePipelineDebug.dll</td><td>PLOOBS~1.DLL|PloobsEnginePipelineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\Dll\PloobsEnginePipelineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginepipelinedebug.dl1</td><td>PloobsEnginePipelineDebug.dll1</td><td>PLOOBS~2.DLL|PloobsEnginePipelineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\Dll\PloobsEnginePipelineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginepipelinedebug.dl2</td><td>PloobsEnginePipelineDebug.dll2</td><td>PLOOBS~1.DLL|PloobsEnginePipelineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineHiDef\PloobsEnginePipelineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginepipelinedebug.dl3</td><td>PloobsEnginePipelineDebug.dll3</td><td>PLOOBS~1.DLL|PloobsEnginePipelineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineReach\PloobsEnginePipelineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsenginepipelinedebug.dl4</td><td>PloobsEnginePipelineDebug.dll4</td><td>PLOOBS~2.DLL|PloobsEnginePipelineDebug.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\Dlls\PloobsEnginePipelineDebug.dll</td><td>1</td><td/></row>
		<row><td>ploobsfeatures.csproj</td><td>ISX_DEFAULTCOMPONENT83</td><td>PLOOBS~1.CSP|PloobsFeatures.csproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\PloobsFeatures.csproj</td><td>1</td><td/></row>
		<row><td>ploobsfeatures.csproj.user</td><td>ISX_DEFAULTCOMPONENT83</td><td>PLOOBS~1.USE|PloobsFeatures.csproj.user</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\PloobsFeatures.csproj.user</td><td>1</td><td/></row>
		<row><td>ploobslevelexporter.ms</td><td>ISX_DEFAULTCOMPONENT98</td><td>PLOOBS~1.MS|PloobsLevelExporter.ms</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\Extra\ExporterScripts\PloobsLevelExporter.ms</td><td>1</td><td/></row>
		<row><td>ploobsprojecttemplate.csproj</td><td>ISX_DEFAULTCOMPONENT117</td><td>PLOOBS~1.CSP|PloobsProjectTemplate.csproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\PloobsProjectTemplate.csproj</td><td>1</td><td/></row>
		<row><td>ploobsprojecttemplatecontent</td><td>ISX_DEFAULTCOMPONENT127</td><td>PLOOBS~1.CON|PloobsProjectTemplateContent.contentproj</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\PloobsProjectTemplateContent.contentproj</td><td>1</td><td/></row>
		<row><td>ploobsscripts.dll</td><td>PloobsScripts.dll</td><td>PLOOBS~4.DLL|PloobsScripts.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\Dll\PloobsScripts.dll</td><td>1</td><td/></row>
		<row><td>ploobsserializator.dll</td><td>PloobsSerializator.dll</td><td>PLOOBS~4.DLL|PloobsSerializator.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\Dll\PloobsSerializator.dll</td><td>1</td><td/></row>
		<row><td>pointlightcircularupdater.cs</td><td>ISX_DEFAULTCOMPONENT59</td><td>POINTL~1.CS|PointLightCircularUpdater.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo3 - Lights\Helpers\PointLightCircularUpdater.cs</td><td>1</td><td/></row>
		<row><td>pointlightscreen.cs</td><td>ISX_DEFAULTCOMPONENT58</td><td>POINTL~2.CS|PointLightScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo3 - Lights\PointLightScreen.cs</td><td>1</td><td/></row>
		<row><td>ponte_suporte.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>PONTE_~1.JPG|ponte_suporte.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\ponte_suporte.jpg</td><td>1</td><td/></row>
		<row><td>positioning2d.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>POSITI~1.JPG|positioning2d.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\positioning2d.jpg</td><td>1</td><td/></row>
		<row><td>posteffect.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>POSTEF~1.JPG|PostEffect.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\PostEffect.jpg</td><td>1</td><td/></row>
		<row><td>posteffectscreen.cs</td><td>ISX_DEFAULTCOMPONENT93</td><td>POSTEF~1.CS|PostEffectScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\PostEffect\PostEffectScreen.cs</td><td>1</td><td/></row>
		<row><td>posteffectspage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>POSTEF~1.XAM|PostEffectsPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\PostEffectsPage.xaml</td><td>1</td><td/></row>
		<row><td>posteffectspage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>POSTEF~1.CS|PostEffectsPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\PostEffectsPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>pressurecloth28screen.cs</td><td>ISX_DEFAULTCOMPONENT45</td><td>PRESSU~1.CS|PressureCloth28Screen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\Screens\PressureCloth28Screen.cs</td><td>1</td><td/></row>
		<row><td>proceduralanimatedbilboardsc</td><td>ISX_DEFAULTCOMPONENT29</td><td>PROCED~1.CS|ProceduralAnimatedBilboardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo6 - BilboardsSamples\ProceduralAnimatedBilboardScreen.cs</td><td>1</td><td/></row>
		<row><td>proceduraltexturescreen.cs</td><td>ISX_DEFAULTCOMPONENT34</td><td>PROCED~1.CS|ProceduralTextureScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9 -  Noise Textures\ProceduralTextureScreen.cs</td><td>1</td><td/></row>
		<row><td>program.cs</td><td>ISX_DEFAULTCOMPONENT21</td><td>Program.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Program.cs</td><td>1</td><td/></row>
		<row><td>program.cs1</td><td>ISX_DEFAULTCOMPONENT54</td><td>Program.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Program.cs</td><td>1</td><td/></row>
		<row><td>program.cs2</td><td>ISX_DEFAULTCOMPONENT117</td><td>Program.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Program.cs</td><td>1</td><td/></row>
		<row><td>progressbar.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>PROGRE~1.PNG|ProgressBar.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ProgressBar.png</td><td>1</td><td/></row>
		<row><td>progressbar.scale.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>PROGRE~1.PNG|ProgressBar.Scale.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ProgressBar.Scale.png</td><td>1</td><td/></row>
		<row><td>progressbar.scale.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>PROGRE~1.XNB|ProgressBar.Scale.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ProgressBar.Scale.xnb</td><td>1</td><td/></row>
		<row><td>progressbar.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>PROGRE~1.XNB|ProgressBar.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ProgressBar.xnb</td><td>1</td><td/></row>
		<row><td>radiobutton.checked.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>RADIOB~1.PNG|RadioButton.Checked.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\RadioButton.Checked.png</td><td>1</td><td/></row>
		<row><td>radiobutton.checked.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>RADIOB~1.XNB|RadioButton.Checked.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\RadioButton.Checked.xnb</td><td>1</td><td/></row>
		<row><td>radiobutton.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>RADIOB~1.PNG|RadioButton.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\RadioButton.png</td><td>1</td><td/></row>
		<row><td>radiobutton.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>RADIOB~1.XNB|RadioButton.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\RadioButton.xnb</td><td>1</td><td/></row>
		<row><td>reachdemos.sln</td><td>ISX_DEFAULTCOMPONENT100</td><td>REACHD~1.SLN|ReachDemos.sln</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos.sln</td><td>1</td><td/></row>
		<row><td>reachdemos.suo</td><td>ISX_DEFAULTCOMPONENT100</td><td>REACHD~1.SUO|ReachDemos.suo</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos.suo</td><td>1</td><td/></row>
		<row><td>repeatuntil.cs</td><td>ISX_DEFAULTCOMPONENT89</td><td>REPEAT~1.CS|RepeatUntil.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\Behaviors\RepeatUntil.cs</td><td>1</td><td/></row>
		<row><td>rock.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>rock.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\rock.dds</td><td>1</td><td/></row>
		<row><td>rock.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>rock.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\rock.xnb</td><td>1</td><td/></row>
		<row><td>rotatingcamera.cs</td><td>ISX_DEFAULTCOMPONENT85</td><td>ROTATI~1.CS|RotatingCamera.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\RotatingCamera.cs</td><td>1</td><td/></row>
		<row><td>rvo2dscreen.cs</td><td>ISX_DEFAULTCOMPONENT71</td><td>RVO2DS~1.CS|RVO2DScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\RVO2\RVO2DScreen.cs</td><td>1</td><td/></row>
		<row><td>rvo2dwrapper.dll</td><td>RVO2DWrapper.dll</td><td>RVO2DW~1.DLL|RVO2DWrapper.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\RVO2DWrapper.dll</td><td>1</td><td/></row>
		<row><td>rvo3d.dll</td><td>RVO3D.dll</td><td>RVO3D.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\RVO3D.dll</td><td>1</td><td/></row>
		<row><td>rvo3dscreen.cs</td><td>ISX_DEFAULTCOMPONENT72</td><td>RVO3DS~1.CS|RVO3DScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\RVO3D\RVO3DScreen.cs</td><td>1</td><td/></row>
		<row><td>rvoobject.cs</td><td>ISX_DEFAULTCOMPONENT70</td><td>RVOOBJ~1.CS|RVOObject.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\RVO\RVOObject.cs</td><td>1</td><td/></row>
		<row><td>rvoobject.cs1</td><td>ISX_DEFAULTCOMPONENT131</td><td>RVOOBJ~1.CS|RVOObject.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\RVO\RVOObject.cs</td><td>1</td><td/></row>
		<row><td>rvoscreen.cs</td><td>ISX_DEFAULTCOMPONENT70</td><td>RVOSCR~1.CS|RVOScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\RVO\RVOScreen.cs</td><td>1</td><td/></row>
		<row><td>rvoscreen.cs1</td><td>ISX_DEFAULTCOMPONENT131</td><td>RVOSCR~1.CS|RVOScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\RVO\RVOScreen.cs</td><td>1</td><td/></row>
		<row><td>sand.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>sand.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\sand.dds</td><td>1</td><td/></row>
		<row><td>sand.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>sand.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sand.xnb</td><td>1</td><td/></row>
		<row><td>script.txt</td><td>ISX_DEFAULTCOMPONENT80</td><td>script.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Script\script.txt</td><td>1</td><td/></row>
		<row><td>scriptscreen.cs</td><td>ISX_DEFAULTCOMPONENT64</td><td>SCRIPT~1.CS|ScriptScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo7 - Scripts\ScriptScreen.cs</td><td>1</td><td/></row>
		<row><td>scrollbar.buttonhorz.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SCROLL~1.PNG|ScrollBar.ButtonHorz.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ScrollBar.ButtonHorz.png</td><td>1</td><td/></row>
		<row><td>scrollbar.buttonhorz.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SCROLL~1.XNB|ScrollBar.ButtonHorz.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ScrollBar.ButtonHorz.xnb</td><td>1</td><td/></row>
		<row><td>scrollbar.buttonvert.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SCROLL~1.PNG|ScrollBar.ButtonVert.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ScrollBar.ButtonVert.png</td><td>1</td><td/></row>
		<row><td>scrollbar.buttonvert.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SCROLL~1.XNB|ScrollBar.ButtonVert.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ScrollBar.ButtonVert.xnb</td><td>1</td><td/></row>
		<row><td>scrollbar.glyphhorz.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SCROLL~1.PNG|ScrollBar.GlyphHorz.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ScrollBar.GlyphHorz.png</td><td>1</td><td/></row>
		<row><td>scrollbar.glyphhorz.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SCROLL~1.XNB|ScrollBar.GlyphHorz.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ScrollBar.GlyphHorz.xnb</td><td>1</td><td/></row>
		<row><td>scrollbar.glyphvert.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SCROLL~1.PNG|ScrollBar.GlyphVert.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ScrollBar.GlyphVert.png</td><td>1</td><td/></row>
		<row><td>scrollbar.glyphvert.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SCROLL~1.XNB|ScrollBar.GlyphVert.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ScrollBar.GlyphVert.xnb</td><td>1</td><td/></row>
		<row><td>scrollbar.railhorz.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SCROLL~1.PNG|ScrollBar.RailHorz.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ScrollBar.RailHorz.png</td><td>1</td><td/></row>
		<row><td>scrollbar.railhorz.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SCROLL~1.XNB|ScrollBar.RailHorz.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ScrollBar.RailHorz.xnb</td><td>1</td><td/></row>
		<row><td>scrollbar.railvert.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SCROLL~1.PNG|ScrollBar.RailVert.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ScrollBar.RailVert.png</td><td>1</td><td/></row>
		<row><td>scrollbar.railvert.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SCROLL~1.XNB|ScrollBar.RailVert.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ScrollBar.RailVert.xnb</td><td>1</td><td/></row>
		<row><td>serializationscreen.cs</td><td>ISX_DEFAULTCOMPONENT65</td><td>SERIAL~1.CS|SerializationScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo8 - Serialization\SerializationScreen.cs</td><td>1</td><td/></row>
		<row><td>serializatorwrapper.cs</td><td>ISX_DEFAULTCOMPONENT65</td><td>SERIAL~1.CS|SerializatorWrapper.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo8 - Serialization\SerializatorWrapper.cs</td><td>1</td><td/></row>
		<row><td>shaderidscreen.cs</td><td>ISX_DEFAULTCOMPONENT24</td><td>SHADER~1.CS|ShaderIDScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo2 - Materials\ShaderIDScreen.cs</td><td>1</td><td/></row>
		<row><td>shadow.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>shadow.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\shadow.X</td><td>1</td><td/></row>
		<row><td>shadow.xml</td><td>ISX_DEFAULTCOMPONENT11</td><td>shadow.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\ModelInfos\shadow.xml</td><td>1</td><td/></row>
		<row><td>shadow.xml1</td><td>ISX_DEFAULTCOMPONENT107</td><td>shadow.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\ModelInfos\shadow.xml</td><td>1</td><td/></row>
		<row><td>shadow.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>shadow.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\shadow.xnb</td><td>1</td><td/></row>
		<row><td>shared.arrowdown.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.ArrowDown.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.ArrowDown.png</td><td>1</td><td/></row>
		<row><td>shared.arrowdown.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.ArrowDown.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.ArrowDown.xnb</td><td>1</td><td/></row>
		<row><td>shared.arrowleft.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.ArrowLeft.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.ArrowLeft.png</td><td>1</td><td/></row>
		<row><td>shared.arrowleft.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.ArrowLeft.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.ArrowLeft.xnb</td><td>1</td><td/></row>
		<row><td>shared.arrowright.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.ArrowRight.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.ArrowRight.png</td><td>1</td><td/></row>
		<row><td>shared.arrowright.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.ArrowRight.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.ArrowRight.xnb</td><td>1</td><td/></row>
		<row><td>shared.arrowup.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.ArrowUp.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.ArrowUp.png</td><td>1</td><td/></row>
		<row><td>shared.arrowup.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.ArrowUp.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.ArrowUp.xnb</td><td>1</td><td/></row>
		<row><td>shared.end.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.End.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.End.png</td><td>1</td><td/></row>
		<row><td>shared.end.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.End.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.End.xnb</td><td>1</td><td/></row>
		<row><td>shared.glyph.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.Glyph.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.Glyph.png</td><td>1</td><td/></row>
		<row><td>shared.glyph.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.Glyph.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.Glyph.xnb</td><td>1</td><td/></row>
		<row><td>shared.home.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.Home.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.Home.png</td><td>1</td><td/></row>
		<row><td>shared.home.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.Home.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.Home.xnb</td><td>1</td><td/></row>
		<row><td>shared.pagedown.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.PageDown.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.PageDown.png</td><td>1</td><td/></row>
		<row><td>shared.pagedown.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.PageDown.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.PageDown.xnb</td><td>1</td><td/></row>
		<row><td>shared.pageup.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SHARED~1.PNG|Shared.PageUp.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Shared.PageUp.png</td><td>1</td><td/></row>
		<row><td>shared.pageup.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SHARED~1.XNB|Shared.PageUp.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Shared.PageUp.xnb</td><td>1</td><td/></row>
		<row><td>sharedosiris.dll</td><td>SharedOsiris.dll</td><td>SHARED~1.DLL|SharedOsiris.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\SharedOsiris.dll</td><td>1</td><td/></row>
		<row><td>sidebar.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>Sidebar.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Sidebar.png</td><td>1</td><td/></row>
		<row><td>sidebar.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>Sidebar.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Sidebar.xnb</td><td>1</td><td/></row>
		<row><td>sidebarpanel.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SIDEBA~1.PNG|SidebarPanel.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\SidebarPanel.png</td><td>1</td><td/></row>
		<row><td>sidebarpanel.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SIDEBA~1.XNB|SidebarPanel.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\SidebarPanel.xnb</td><td>1</td><td/></row>
		<row><td>skin.xml</td><td>ISX_DEFAULTCOMPONENT13</td><td>Skin.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Skin.xml</td><td>1</td><td/></row>
		<row><td>skin.xml1</td><td>ISX_DEFAULTCOMPONENT109</td><td>Skin.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Skin.xml</td><td>1</td><td/></row>
		<row><td>skinnediobject.cs</td><td>ISX_DEFAULTCOMPONENT31</td><td>SKINNE~1.CS|SkinnedIObject.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\CustomSkinnedAnimation\SkinnedIObject.cs</td><td>1</td><td/></row>
		<row><td>skinnedmodel.cs</td><td>ISX_DEFAULTCOMPONENT31</td><td>SKINNE~1.CS|SkinnedModel.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\CustomSkinnedAnimation\SkinnedModel.cs</td><td>1</td><td/></row>
		<row><td>skinnedmodel.dll</td><td>SkinnedModel.dll</td><td>SKINNE~1.DLL|SkinnedModel.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\SkinnedModel.dll</td><td>1</td><td/></row>
		<row><td>skinnedmodel.dll1</td><td>SkinnedModel.dll1</td><td>SKINNE~1.DLL|SkinnedModel.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\Dll\Skinned\SkinnedModel.dll</td><td>1</td><td/></row>
		<row><td>skinnedmodelpipeline.dll</td><td>SkinnedModelPipeline.dll</td><td>SKINNE~1.DLL|SkinnedModelPipeline.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\Dll\Skinned\SkinnedModelPipeline.dll</td><td>1</td><td/></row>
		<row><td>skyboxscreen.cs</td><td>ISX_DEFAULTCOMPONENT88</td><td>SKYBOX~1.CS|SkyBoxScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\SkyBoxScreen.cs</td><td>1</td><td/></row>
		<row><td>skycube.dds</td><td>ISX_DEFAULTCOMPONENT136</td><td>skyCube.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\skyCube.dds</td><td>1</td><td/></row>
		<row><td>smoke.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>Smoke.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\Smoke.png</td><td>1</td><td/></row>
		<row><td>smoke.png1</td><td>ISX_DEFAULTCOMPONENT136</td><td>Smoke.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\Smoke.png</td><td>1</td><td/></row>
		<row><td>smoke.png2</td><td>ISX_DEFAULTCOMPONENT130</td><td>Smoke.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\Smoke.png</td><td>1</td><td/></row>
		<row><td>smoke.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>Smoke.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\Smoke.xnb</td><td>1</td><td/></row>
		<row><td>smokeps.cs</td><td>ISX_DEFAULTCOMPONENT27</td><td>SmokePS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo5 - ParticleSamples\SmokePS.cs</td><td>1</td><td/></row>
		<row><td>snow.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>snow.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\snow.dds</td><td>1</td><td/></row>
		<row><td>snow.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>snow.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\snow.xnb</td><td>1</td><td/></row>
		<row><td>snowps.cs</td><td>ISX_DEFAULTCOMPONENT27</td><td>SnowPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo5 - ParticleSamples\SnowPS.cs</td><td>1</td><td/></row>
		<row><td>snowps.cs1</td><td>ISX_DEFAULTCOMPONENT88</td><td>SnowPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\SnowPS.cs</td><td>1</td><td/></row>
		<row><td>snowps.cs2</td><td>ISX_DEFAULTCOMPONENT122</td><td>SnowPS.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Particle3D\SnowPS.cs</td><td>1</td><td/></row>
		<row><td>soundscreen.cs</td><td>ISX_DEFAULTCOMPONENT25</td><td>SOUNDS~1.CS|SoundScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo3 - Sound\SoundScreen.cs</td><td>1</td><td/></row>
		<row><td>spinbox.button.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SPINBO~1.PNG|SpinBox.Button.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\SpinBox.Button.png</td><td>1</td><td/></row>
		<row><td>spinbox.button.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SPINBO~1.XNB|SpinBox.Button.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\SpinBox.Button.xnb</td><td>1</td><td/></row>
		<row><td>spinbox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>SpinBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\SpinBox.png</td><td>1</td><td/></row>
		<row><td>spinbox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>SpinBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\SpinBox.xnb</td><td>1</td><td/></row>
		<row><td>splashscreenimage.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>SPLASH~1.JPG|SplashScreenImage.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\SplashScreenImage.jpg</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_ddn.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPNZA_~1.TGA|spnza_bricks_a_ddn.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\spnza_bricks_a_ddn.tga</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_ddn.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPNZA_~1.XNB|spnza_bricks_a_ddn.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\spnza_bricks_a_ddn.xnb</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_diff.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPNZA_~1.TGA|spnza_bricks_a_diff.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\spnza_bricks_a_diff.tga</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_diff.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPNZA_~1.XNB|spnza_bricks_a_diff.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\spnza_bricks_a_diff.xnb</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_diff_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPNZA_~1.XNB|spnza_bricks_a_diff_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\spnza_bricks_a_diff_0.xnb</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_spec.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPNZA_~1.TGA|spnza_bricks_a_spec.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\spnza_bricks_a_spec.tga</td><td>1</td><td/></row>
		<row><td>spnza_bricks_a_spec.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPNZA_~1.XNB|spnza_bricks_a_spec.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\spnza_bricks_a_spec.xnb</td><td>1</td><td/></row>
		<row><td>sponza_ceiling_a_diff.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPONZA~1.TGA|sponza_ceiling_a_diff.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\sponza_ceiling_a_diff.tga</td><td>1</td><td/></row>
		<row><td>sponza_ceiling_a_diff.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_ceiling_a_diff.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_ceiling_a_diff.xnb</td><td>1</td><td/></row>
		<row><td>sponza_ceiling_a_diff_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_ceiling_a_diff_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_ceiling_a_diff_0.xnb</td><td>1</td><td/></row>
		<row><td>sponza_ceiling_a_spec.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPONZA~1.TGA|sponza_ceiling_a_spec.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\sponza_ceiling_a_spec.tga</td><td>1</td><td/></row>
		<row><td>sponza_ceiling_a_spec.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_ceiling_a_spec.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_ceiling_a_spec.xnb</td><td>1</td><td/></row>
		<row><td>sponza_column_b_ddn.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPONZA~1.TGA|sponza_column_b_ddn.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\sponza_column_b_ddn.tga</td><td>1</td><td/></row>
		<row><td>sponza_column_b_ddn.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_column_b_ddn.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_column_b_ddn.xnb</td><td>1</td><td/></row>
		<row><td>sponza_column_b_diff.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPONZA~1.TGA|sponza_column_b_diff.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\sponza_column_b_diff.tga</td><td>1</td><td/></row>
		<row><td>sponza_column_b_diff.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_column_b_diff.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_column_b_diff.xnb</td><td>1</td><td/></row>
		<row><td>sponza_column_b_diff_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_column_b_diff_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_column_b_diff_0.xnb</td><td>1</td><td/></row>
		<row><td>sponza_floor_a_diff.tga</td><td>ISX_DEFAULTCOMPONENT114</td><td>SPONZA~1.TGA|sponza_floor_a_diff.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\sponza_floor_a_diff.tga</td><td>1</td><td/></row>
		<row><td>sponza_floor_a_diff.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_floor_a_diff.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_floor_a_diff.xnb</td><td>1</td><td/></row>
		<row><td>sponza_floor_a_diff_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>SPONZA~1.XNB|sponza_floor_a_diff_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\sponza_floor_a_diff_0.xnb</td><td>1</td><td/></row>
		<row><td>spotlightcircularupdater.cs</td><td>ISX_DEFAULTCOMPONENT59</td><td>SPOTLI~1.CS|SpotLightCircularUpdater.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo3 - Lights\Helpers\SpotLightCircularUpdater.cs</td><td>1</td><td/></row>
		<row><td>spotlightscreen.cs</td><td>ISX_DEFAULTCOMPONENT58</td><td>SPOTLI~2.CS|SpotLightScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo3 - Lights\SpotLightScreen.cs</td><td>1</td><td/></row>
		<row><td>spriteanimation.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>SPRITE~1.JPG|spriteanimation.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\spriteanimation.jpg</td><td>1</td><td/></row>
		<row><td>spriteparticle.cs</td><td>ISX_DEFAULTCOMPONENT87</td><td>SPRITE~1.CS|SpriteParticle.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\2D Demos\Helpers\SpriteParticle.cs</td><td>1</td><td/></row>
		<row><td>spriteparticle.cs1</td><td>ISX_DEFAULTCOMPONENT119</td><td>SPRITE~1.CS|SpriteParticle.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\2D Demos\Helpers\SpriteParticle.cs</td><td>1</td><td/></row>
		<row><td>ssao.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>ssao.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\ssao.X</td><td>1</td><td/></row>
		<row><td>ssao.x.log</td><td>ISX_DEFAULTCOMPONENT105</td><td>SSAOX~1.LOG|ssao.X.log</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\ssao.X.log</td><td>1</td><td/></row>
		<row><td>ssao.xml</td><td>ISX_DEFAULTCOMPONENT11</td><td>ssao.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\ModelInfos\ssao.xml</td><td>1</td><td/></row>
		<row><td>ssao.xml1</td><td>ISX_DEFAULTCOMPONENT107</td><td>ssao.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\ModelInfos\ssao.xml</td><td>1</td><td/></row>
		<row><td>ssao.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>ssao.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\ssao.xnb</td><td>1</td><td/></row>
		<row><td>ssaoscreen.cs</td><td>ISX_DEFAULTCOMPONENT37</td><td>SSAOSC~1.CS|SSAOScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.3 SSAO\SSAOScreen.cs</td><td>1</td><td/></row>
		<row><td>statesample.cs</td><td>ISX_DEFAULTCOMPONENT68</td><td>STATES~1.CS|StateSample.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\FSM\StateSample.cs</td><td>1</td><td/></row>
		<row><td>statusbar.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>STATUS~1.PNG|StatusBar.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\StatusBar.png</td><td>1</td><td/></row>
		<row><td>statusbar.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>STATUS~1.XNB|StatusBar.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\StatusBar.xnb</td><td>1</td><td/></row>
		<row><td>stealtheffectscreen.cs</td><td>ISX_DEFAULTCOMPONENT41</td><td>STEALT~1.CS|StealthEffectScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.7 Stealth\StealthEffectScreen.cs</td><td>1</td><td/></row>
		<row><td>stealthposteffect.cs</td><td>ISX_DEFAULTCOMPONENT41</td><td>STEALT~1.CS|StealthPostEffect.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.7 Stealth\StealthPostEffect.cs</td><td>1</td><td/></row>
		<row><td>steeratachment.cs</td><td>ISX_DEFAULTCOMPONENT73</td><td>STEERA~1.CS|SteerAtachment.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Steering\SteerAtachment.cs</td><td>1</td><td/></row>
		<row><td>steerscreen.cs</td><td>ISX_DEFAULTCOMPONENT73</td><td>STEERS~1.CS|SteerScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo9 - AI\Steering\SteerScreen.cs</td><td>1</td><td/></row>
		<row><td>stilldesign.physx.net.dll</td><td>StillDesign.PhysX.Net.dll</td><td>STILLD~1.DLL|StillDesign.PhysX.Net.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\StillDesign.PhysX.Net.dll</td><td>1</td><td/></row>
		<row><td>stilldesign.physx.net.dll1</td><td>StillDesign.PhysX.Net.dll1</td><td>STILLD~1.DLL|StillDesign.PhysX.Net.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\StillDesign.PhysX.Net.dll</td><td>1</td><td/></row>
		<row><td>stressbepuscreen.cs</td><td>ISX_DEFAULTCOMPONENT60</td><td>STRESS~1.CS|StressBepuScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo4 - Physics\StressBepuScreen.cs</td><td>1</td><td/></row>
		<row><td>stresspage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>STRESS~1.XAM|StressPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\StressPage.xaml</td><td>1</td><td/></row>
		<row><td>stresspage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>STRESS~2.CS|StressPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\StressPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>stressplate.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>STRESS~1.JPG|stressplate.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\stressplate.jpg</td><td>1</td><td/></row>
		<row><td>stressscreen.cs</td><td>ISX_DEFAULTCOMPONENT88</td><td>STRESS~3.CS|StressScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\StressScreen.cs</td><td>1</td><td/></row>
		<row><td>tabcontrol.header.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TABCON~1.PNG|TabControl.Header.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\TabControl.Header.png</td><td>1</td><td/></row>
		<row><td>tabcontrol.header.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TABCON~1.XNB|TabControl.Header.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\TabControl.Header.xnb</td><td>1</td><td/></row>
		<row><td>tabcontrol.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TABCON~1.PNG|TabControl.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\TabControl.png</td><td>1</td><td/></row>
		<row><td>tabcontrol.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TABCON~1.XNB|TabControl.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\TabControl.xnb</td><td>1</td><td/></row>
		<row><td>table.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>table.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\table.X</td><td>1</td><td/></row>
		<row><td>table.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>table.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\table.xnb</td><td>1</td><td/></row>
		<row><td>talk1.txt</td><td>ISX_DEFAULTCOMPONENT80</td><td>talk1.txt</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Script\talk1.txt</td><td>1</td><td/></row>
		<row><td>tank.fbx</td><td>ISX_DEFAULTCOMPONENT105</td><td>tank.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\tank.fbx</td><td>1</td><td/></row>
		<row><td>tank.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>tank.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\tank.xnb</td><td>1</td><td/></row>
		<row><td>taskapisample.cs</td><td>ISX_DEFAULTCOMPONENT125</td><td>TASKAP~1.CS|TaskApiSample.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\TaskApiSample.cs</td><td>1</td><td/></row>
		<row><td>teapot.x</td><td>ISX_DEFAULTCOMPONENT105</td><td>teapot.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\teapot.X</td><td>1</td><td/></row>
		<row><td>teapot.x1</td><td>ISX_DEFAULTCOMPONENT134</td><td>teapot.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Model\teapot.X</td><td>1</td><td/></row>
		<row><td>teapot.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>teapot.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\teapot.xnb</td><td>1</td><td/></row>
		<row><td>techtruthscreen.cs</td><td>ISX_DEFAULTCOMPONENT88</td><td>TECHTR~1.CS|TechTruthScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\TechTruthScreen.cs</td><td>1</td><td/></row>
		<row><td>templateforwardscreen.cs</td><td>ISX_DEFAULTCOMPONENT125</td><td>TEMPLA~1.CS|TemplateForwardScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\TemplateForwardScreen.cs</td><td>1</td><td/></row>
		<row><td>terrain.cs</td><td>ISX_DEFAULTCOMPONENT117</td><td>Terrain.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\Terrain.cs</td><td>1</td><td/></row>
		<row><td>terrain.jpg</td><td>ISX_DEFAULTCOMPONENT83</td><td>Terrain.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Terrain.jpg</td><td>1</td><td/></row>
		<row><td>terrain_base_texture.jpg</td><td>ISX_DEFAULTCOMPONENT129</td><td>TERRAI~1.JPG|terrain_base_texture.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Terrain\terrain_base_texture.jpg</td><td>1</td><td/></row>
		<row><td>terrain_base_texture.jpg1</td><td>ISX_DEFAULTCOMPONENT135</td><td>TERRAI~1.JPG|terrain_base_texture.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Terrain\terrain_base_texture.jpg</td><td>1</td><td/></row>
		<row><td>terrain_detail_texture.jpg</td><td>ISX_DEFAULTCOMPONENT129</td><td>TERRAI~1.JPG|terrain_detail_texture.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Terrain\terrain_detail_texture.jpg</td><td>1</td><td/></row>
		<row><td>terrain_detail_texture.jpg1</td><td>ISX_DEFAULTCOMPONENT135</td><td>TERRAI~1.JPG|terrain_detail_texture.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Terrain\terrain_detail_texture.jpg</td><td>1</td><td/></row>
		<row><td>terraingeoclipmap.cs</td><td>ISX_DEFAULTCOMPONENT125</td><td>TERRAI~2.CS|TerrainGeoClipMap.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplate\TemplateScreens\TerrainGeoClipMap.cs</td><td>1</td><td/></row>
		<row><td>terraingeoclipmap.cs1</td><td>ISX_DEFAULTCOMPONENT88</td><td>TERRAI~3.CS|TerrainGeoClipMap.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\TerrainGeoClipMap.cs</td><td>1</td><td/></row>
		<row><td>terraingrass.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>TERRAI~1.DDS|Terraingrass.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\Terraingrass.dds</td><td>1</td><td/></row>
		<row><td>terraingrass.dds1</td><td>ISX_DEFAULTCOMPONENT136</td><td>TERRAI~1.DDS|Terraingrass.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\Terraingrass.dds</td><td>1</td><td/></row>
		<row><td>terraingrass.dds2</td><td>ISX_DEFAULTCOMPONENT130</td><td>TERRAI~1.DDS|Terraingrass.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\Terraingrass.dds</td><td>1</td><td/></row>
		<row><td>terraingrass.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>TERRAI~1.XNB|Terraingrass.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\Terraingrass.xnb</td><td>1</td><td/></row>
		<row><td>terrainpage.xaml</td><td>ISX_DEFAULTCOMPONENT83</td><td>TERRAI~1.XAM|TerrainPage.xaml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\TerrainPage.xaml</td><td>1</td><td/></row>
		<row><td>terrainpage.xaml.cs</td><td>ISX_DEFAULTCOMPONENT83</td><td>TERRAI~2.CS|TerrainPage.xaml.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\TerrainPage.xaml.cs</td><td>1</td><td/></row>
		<row><td>terrainscreen.cs</td><td>ISX_DEFAULTCOMPONENT26</td><td>TERRAI~1.CS|TerrainScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo4 - Terrain\TerrainScreen.cs</td><td>1</td><td/></row>
		<row><td>terrainscreen.cs1</td><td>ISX_DEFAULTCOMPONENT88</td><td>TERRAI~1.CS|TerrainScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1Library\3D Demos\TerrainScreen.cs</td><td>1</td><td/></row>
		<row><td>text.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>Text.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Text.cur</td><td>1</td><td/></row>
		<row><td>text.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>Text.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Text.xnb</td><td>1</td><td/></row>
		<row><td>textbox.cursor.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TEXTBO~1.PNG|TextBox.Cursor.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\TextBox.Cursor.png</td><td>1</td><td/></row>
		<row><td>textbox.cursor.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TEXTBO~1.XNB|TextBox.Cursor.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\TextBox.Cursor.xnb</td><td>1</td><td/></row>
		<row><td>textbox.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TextBox.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\TextBox.png</td><td>1</td><td/></row>
		<row><td>textbox.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TextBox.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\TextBox.xnb</td><td>1</td><td/></row>
		<row><td>throwparticlesobject.cs</td><td>ISX_DEFAULTCOMPONENT28</td><td>THROWP~1.CS|ThrowParticlesObject.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo5 - ParticleSamples\Helpers\ThrowParticlesObject.cs</td><td>1</td><td/></row>
		<row><td>tile.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>tile.JPG</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\tile.JPG</td><td>1</td><td/></row>
		<row><td>tile.jpg1</td><td>ISX_DEFAULTCOMPONENT130</td><td>tile.JPG</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\tile.JPG</td><td>1</td><td/></row>
		<row><td>titlescreen.cs</td><td>ISX_DEFAULTCOMPONENT21</td><td>TITLES~1.CS|TitleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\TitleScreen.cs</td><td>1</td><td/></row>
		<row><td>titlescreen.cs1</td><td>ISX_DEFAULTCOMPONENT54</td><td>TITLES~1.CS|TitleScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\TitleScreen.cs</td><td>1</td><td/></row>
		<row><td>toolbar.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ToolBar.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ToolBar.png</td><td>1</td><td/></row>
		<row><td>toolbar.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ToolBar.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ToolBar.xnb</td><td>1</td><td/></row>
		<row><td>toolbarbutton.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TOOLBA~1.PNG|ToolBarButton.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ToolBarButton.png</td><td>1</td><td/></row>
		<row><td>toolbarbutton.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TOOLBA~1.XNB|ToolBarButton.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ToolBarButton.xnb</td><td>1</td><td/></row>
		<row><td>toolbarpanel.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TOOLBA~1.PNG|ToolBarPanel.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ToolBarPanel.png</td><td>1</td><td/></row>
		<row><td>toolbarpanel.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TOOLBA~1.XNB|ToolBarPanel.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ToolBarPanel.xnb</td><td>1</td><td/></row>
		<row><td>tooltip.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>ToolTip.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\ToolTip.png</td><td>1</td><td/></row>
		<row><td>tooltip.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>ToolTip.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\ToolTip.xnb</td><td>1</td><td/></row>
		<row><td>trackbar.button.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TRACKB~1.PNG|TrackBar.Button.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\TrackBar.Button.png</td><td>1</td><td/></row>
		<row><td>trackbar.button.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TRACKB~1.XNB|TrackBar.Button.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\TrackBar.Button.xnb</td><td>1</td><td/></row>
		<row><td>trackbar.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>TrackBar.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\TrackBar.png</td><td>1</td><td/></row>
		<row><td>trackbar.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>TrackBar.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\TrackBar.xnb</td><td>1</td><td/></row>
		<row><td>transparentdeferredscreen.cs</td><td>ISX_DEFAULTCOMPONENT24</td><td>TRANSP~1.CS|TransparentDeferredScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo2 - Materials\TransparentDeferredScreen.cs</td><td>1</td><td/></row>
		<row><td>transparentonborder.fx</td><td>ISX_DEFAULTCOMPONENT104</td><td>TRANSP~1.FX|TransparentOnBorder.fx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Effects\TransparentOnBorder.fx</td><td>1</td><td/></row>
		<row><td>transparentonborder.xnb</td><td>ISX_DEFAULTCOMPONENT8</td><td>TRANSP~1.XNB|TransparentOnBorder.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Effects\TransparentOnBorder.xnb</td><td>1</td><td/></row>
		<row><td>trash.x</td><td>ISX_DEFAULTCOMPONENT79</td><td>trash.X</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Model\trash.X</td><td>1</td><td/></row>
		<row><td>trash_can.jpg</td><td>ISX_DEFAULTCOMPONENT81</td><td>TRASH_~1.JPG|trash_can.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Textures\trash_can.jpg</td><td>1</td><td/></row>
		<row><td>tree.dds</td><td>ISX_DEFAULTCOMPONENT114</td><td>tree.dds</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\tree.dds</td><td>1</td><td/></row>
		<row><td>tree.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>tree.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\tree.xnb</td><td>1</td><td/></row>
		<row><td>treebump.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>treebump.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\treebump.jpg</td><td>1</td><td/></row>
		<row><td>treebump.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>treebump.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\treebump.xnb</td><td>1</td><td/></row>
		<row><td>treedifuse.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>TREEDI~1.JPG|treedifuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\treedifuse.jpg</td><td>1</td><td/></row>
		<row><td>treedifuse.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>TREEDI~1.XNB|treedifuse.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\treedifuse.xnb</td><td>1</td><td/></row>
		<row><td>treedifuse_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>TREEDI~1.XNB|treedifuse_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\treedifuse_0.xnb</td><td>1</td><td/></row>
		<row><td>triggerbepuscreen.cs</td><td>ISX_DEFAULTCOMPONENT63</td><td>TRIGGE~1.CS|TriggerBepuScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo6 - Trigger\TriggerBepuScreen.cs</td><td>1</td><td/></row>
		<row><td>turret_alt_diff_tex.tga</td><td>ISX_DEFAULTCOMPONENT105</td><td>TURRET~1.TGA|turret_alt_diff_tex.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\turret_alt_diff_tex.tga</td><td>1</td><td/></row>
		<row><td>turret_alt_diff_tex.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>TURRET~1.XNB|turret_alt_diff_tex.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\turret_alt_diff_tex.xnb</td><td>1</td><td/></row>
		<row><td>turret_alt_diff_tex_0.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>TURRET~1.XNB|turret_alt_diff_tex_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\turret_alt_diff_tex_0.xnb</td><td>1</td><td/></row>
		<row><td>unitlightinterpolator.cs</td><td>ISX_DEFAULTCOMPONENT59</td><td>UNITLI~1.CS|UnitLightInterpolator.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\Demo3 - Lights\Helpers\UnitLightInterpolator.cs</td><td>1</td><td/></row>
		<row><td>untitled.bmp</td><td>ISX_DEFAULTCOMPONENT114</td><td>Untitled.BMP</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\Untitled.BMP</td><td>1</td><td/></row>
		<row><td>untitled.bmp1</td><td>ISX_DEFAULTCOMPONENT130</td><td>Untitled.BMP</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\ReachDemos\ReachDemos\PloobsProjectTemplateContent\Textures\Untitled.BMP</td><td>1</td><td/></row>
		<row><td>untitled.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>Untitled.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\Untitled.xnb</td><td>1</td><td/></row>
		<row><td>upbodyc.tga</td><td>ISX_DEFAULTCOMPONENT106</td><td>upBodyC.tga</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\Dude\upBodyC.tga</td><td>1</td><td/></row>
		<row><td>upbodyc.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>upBodyC.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\upBodyC.xnb</td><td>1</td><td/></row>
		<row><td>upbodyc_0.xnb</td><td>ISX_DEFAULTCOMPONENT10</td><td>UPBODY~1.XNB|upBodyC_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\Dude\upBodyC_0.xnb</td><td>1</td><td/></row>
		<row><td>uzi.jpg</td><td>ISX_DEFAULTCOMPONENT81</td><td>uzi.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Textures\uzi.jpg</td><td>1</td><td/></row>
		<row><td>uzi.x</td><td>ISX_DEFAULTCOMPONENT79</td><td>uzi.x</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0Content\Model\uzi.x</td><td>1</td><td/></row>
		<row><td>vehicle.cs</td><td>ISX_DEFAULTCOMPONENT49</td><td>Vehicle.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Vehicle\Vehicle.cs</td><td>1</td><td/></row>
		<row><td>vertexgrid.cs</td><td>ISX_DEFAULTCOMPONENT44</td><td>VERTEX~1.CS|VertexGrid.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.8 Physx 2.8.x\Cloth\VertexGrid.cs</td><td>1</td><td/></row>
		<row><td>vertical.cur</td><td>ISX_DEFAULTCOMPONENT110</td><td>Vertical.cur</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Cursors\Vertical.cur</td><td>1</td><td/></row>
		<row><td>vertical.xnb</td><td>ISX_DEFAULTCOMPONENT14</td><td>Vertical.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Cursors\Vertical.xnb</td><td>1</td><td/></row>
		<row><td>wall_texture_diffuse.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>WALL_T~1.JPG|wall_texture_diffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\wall_texture_diffuse.jpg</td><td>1</td><td/></row>
		<row><td>wall_texture_especular.jpg</td><td>ISX_DEFAULTCOMPONENT136</td><td>WALL_T~2.JPG|wall_texture_especular.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\wall_texture_especular.jpg</td><td>1</td><td/></row>
		<row><td>water.png</td><td>ISX_DEFAULTCOMPONENT136</td><td>water.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\water.png</td><td>1</td><td/></row>
		<row><td>watercompletescreen.cs</td><td>ISX_DEFAULTCOMPONENT35</td><td>WATERC~1.CS|WaterCompleteScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo9.1 Water and Ocean\WaterCompleteScreen.cs</td><td>1</td><td/></row>
		<row><td>waypoints.xml</td><td>ISX_DEFAULTCOMPONENT54</td><td>WAYPOI~1.XML|waypoints.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\waypoints.xml</td><td>1</td><td/></row>
		<row><td>weaponmachinegun.fbx</td><td>ISX_DEFAULTCOMPONENT105</td><td>WEAPON~1.FBX|WeaponMachineGun.fbx</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Model\WeaponMachineGun.fbx</td><td>1</td><td/></row>
		<row><td>weaponmachinegun.xnb</td><td>ISX_DEFAULTCOMPONENT9</td><td>WEAPON~1.XNB|WeaponMachineGun.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Model\WeaponMachineGun.xnb</td><td>1</td><td/></row>
		<row><td>weaponmachinegundiffuse.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>WEAPON~1.JPG|WeaponMachineGunDiffuse.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\WeaponMachineGunDiffuse.jpg</td><td>1</td><td/></row>
		<row><td>weaponmachinegundiffuse.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>WEAPON~1.XNB|WeaponMachineGunDiffuse.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\WeaponMachineGunDiffuse.xnb</td><td>1</td><td/></row>
		<row><td>white.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>white.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\white.jpg</td><td>1</td><td/></row>
		<row><td>white.jpg1</td><td>ISX_DEFAULTCOMPONENT136</td><td>white.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1LibraryContent\Textures\white.jpg</td><td>1</td><td/></row>
		<row><td>white.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>white.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\white.xnb</td><td>1</td><td/></row>
		<row><td>white_0.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>white_0.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\white_0.xnb</td><td>1</td><td/></row>
		<row><td>window.caption.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.Caption.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.Caption.png</td><td>1</td><td/></row>
		<row><td>window.caption.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.Caption.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.Caption.xnb</td><td>1</td><td/></row>
		<row><td>window.closebutton.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.CloseButton.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.CloseButton.png</td><td>1</td><td/></row>
		<row><td>window.closebutton.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.CloseButton.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.CloseButton.xnb</td><td>1</td><td/></row>
		<row><td>window.framebottom.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.FrameBottom.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.FrameBottom.png</td><td>1</td><td/></row>
		<row><td>window.framebottom.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.FrameBottom.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.FrameBottom.xnb</td><td>1</td><td/></row>
		<row><td>window.frameleft.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.FrameLeft.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.FrameLeft.png</td><td>1</td><td/></row>
		<row><td>window.frameleft.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.FrameLeft.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.FrameLeft.xnb</td><td>1</td><td/></row>
		<row><td>window.frameright.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.FrameRight.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.FrameRight.png</td><td>1</td><td/></row>
		<row><td>window.frameright.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.FrameRight.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.FrameRight.xnb</td><td>1</td><td/></row>
		<row><td>window.frametop.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.FrameTop.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.FrameTop.png</td><td>1</td><td/></row>
		<row><td>window.frametop.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.FrameTop.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.FrameTop.xnb</td><td>1</td><td/></row>
		<row><td>window.icon.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.Icon.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.Icon.png</td><td>1</td><td/></row>
		<row><td>window.icon.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.Icon.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.Icon.xnb</td><td>1</td><td/></row>
		<row><td>window.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>Window.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.png</td><td>1</td><td/></row>
		<row><td>window.shadow.png</td><td>ISX_DEFAULTCOMPONENT112</td><td>WINDOW~1.PNG|Window.Shadow.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Skins\Default\Images\Window.Shadow.png</td><td>1</td><td/></row>
		<row><td>window.shadow.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>WINDOW~1.XNB|Window.Shadow.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.Shadow.xnb</td><td>1</td><td/></row>
		<row><td>window.xnb</td><td>ISX_DEFAULTCOMPONENT16</td><td>Window.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Skins\Default\Images\Window.xnb</td><td>1</td><td/></row>
		<row><td>wizard.png</td><td>ISX_DEFAULTCOMPONENT114</td><td>wizard.png</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\wizard.png</td><td>1</td><td/></row>
		<row><td>wizard.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>wizard.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\wizard.xnb</td><td>1</td><td/></row>
		<row><td>wmappmanifest.xml</td><td>ISX_DEFAULTCOMPONENT84</td><td>WMAPPM~1.XML|WMAppManifest.xml</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\GraphicsApp1\Properties\WMAppManifest.xml</td><td>1</td><td/></row>
		<row><td>wood_dark.jpg</td><td>ISX_DEFAULTCOMPONENT114</td><td>WOOD_D~1.JPG|wood_dark.jpg</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemosContent\Textures\wood_dark.jpg</td><td>1</td><td/></row>
		<row><td>wood_dark.xnb</td><td>ISX_DEFAULTCOMPONENT69</td><td>WOOD_D~1.XNB|wood_dark.xnb</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\AdvDemosBin\Content\Textures\wood_dark.xnb</td><td>1</td><td/></row>
		<row><td>xnanimation.dll</td><td>XNAnimation.dll</td><td>XNANIM~1.DLL|XNAnimation.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\XNAnimation.dll</td><td>1</td><td/></row>
		<row><td>xnanimation.dll1</td><td>XNAnimation.dll1</td><td>XNANIM~1.DLL|XNAnimation.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\XNAnimation.dll</td><td>1</td><td/></row>
		<row><td>xnanimationpipeline.dll</td><td>XNAnimationPipeline.dll</td><td>XNANIM~2.DLL|XNAnimationPipeline.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\thiagorepo\port-ploobsengine\PhoneSilverLightDemo\Phone7\XNAnimationPipeline.dll</td><td>1</td><td/></row>
		<row><td>xnanimationpipeline.dll1</td><td>XNAnimationPipeline.dll1</td><td>XNANIM~1.DLL|XNAnimationPipeline.dll</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\PloobsEngineWindowsPhone\XNAnimationPipeline.dll</td><td>1</td><td/></row>
		<row><td>xnaskinnedscreen.cs</td><td>ISX_DEFAULTCOMPONENT31</td><td>XNASKI~1.CS|XnaSkinnedScreen.cs</td><td>0</td><td/><td/><td/><td>1</td><td>C:\PloobsPackages\SourceCodeDemos\AdvancedDemos\AdvancedDemos\AdvancedDemos\Demo7 - AnimationSample\CustomSkinnedAnimation\XnaSkinnedScreen.cs</td><td>1</td><td/></row>
	</table>

	<table name="FileSFPCatalog">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s255">SFPCatalog_</col>
	</table>

	<table name="Font">
		<col key="yes" def="s72">File_</col>
		<col def="S128">FontTitle</col>
	</table>

	<table name="ISAssistantTag">
		<col key="yes" def="s72">Tag</col>
		<col def="S255">Data</col>
		<row><td>PROJECT_ASSISTANT_DEFAULT_FEATURE</td><td>AlwaysInstall</td></row>
		<row><td>PROJECT_ASSISTANT_FEATURES</td><td>NonSelectable</td></row>
		<row><td>RegistryPageEnabled</td><td>Yes</td></row>
	</table>

	<table name="ISBillBoard">
		<col key="yes" def="s72">ISBillboard</col>
		<col def="i2">Duration</col>
		<col def="i2">Origin</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Effect</col>
		<col def="i2">Sequence</col>
		<col def="i2">Target</col>
		<col def="S72">Color</col>
		<col def="S72">Style</col>
		<col def="S72">Font</col>
		<col def="L72">Title</col>
		<col def="S72">DisplayName</col>
	</table>

	<table name="ISCEApp">
		<col key="yes" def="s50">AppKey</col>
		<col def="s50">AppName</col>
		<col def="s200">CompanyName</col>
		<col def="s50">DefDir</col>
		<col def="S255">IconPath</col>
		<col def="I4">IconIndex</col>
		<col def="S255">DeviceFile</col>
		<col def="s50">DesktopTargetDir</col>
		<col def="S255">Description</col>
		<col def="i2">DeleteMedia</col>
		<col def="I4">InstallNetCF</col>
		<col def="I4">InstallSQLServer</col>
		<col def="I4">InstallSQLClient</col>
		<col def="I4">InstallSQLDev</col>
		<col def="S255">PreXML</col>
		<col def="S255">PostXML</col>
		<col def="I2">NoUninstall</col>
		<col def="S255">SPCFile</col>
		<col def="S255">PVKFile</col>
		<col def="I4">Attributes</col>
		<col def="S255">RawDeviceFile</col>
		<col def="S72">Component_</col>
		<col def="I4">InstallNetCF2</col>
		<col def="I4">InstallSQLServer2</col>
		<col def="I4">InstallSQLClient2</col>
		<col def="I4">InstallSQLDev2</col>
		<col def="S255">SPCPwd</col>
	</table>

	<table name="ISCEDir">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">DirKey</col>
		<col def="s50">DirParent</col>
		<col def="s255">DirValue</col>
	</table>

	<table name="ISCEFile">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">FileKey</col>
		<col def="s255">Name</col>
		<col def="s50">Destination</col>
		<col def="s255">Source</col>
		<col def="i4">Processor</col>
		<col def="i4">Platform</col>
		<col def="i4">CopyOption</col>
		<col def="i4">FileOption</col>
		<col def="I4">AdvancedOptions</col>
	</table>

	<table name="ISCEFileExt">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">ExtKey</col>
		<col def="s50">FileKey</col>
		<col def="S255">Description</col>
		<col def="s50">Extension</col>
		<col def="i4">IconIndex</col>
	</table>

	<table name="ISCEInstall">
		<col key="yes" def="s255">CEInstallKey</col>
		<col def="s255">CEAppName</col>
		<col def="s255">CEDesktopDir</col>
		<col def="s255">CEIniFileKey</col>
		<col def="s0">CECabs</col>
		<col def="s0">CEIcoFile</col>
		<col def="i2">DeleteMedia</col>
		<col def="S38">Component_</col>
	</table>

	<table name="ISCEOtherAppCABs">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">FileKey</col>
		<col def="s255">BuildSourcePath</col>
	</table>

	<table name="ISCERedist">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="S50">Name</col>
		<col def="I4">Platforms</col>
	</table>

	<table name="ISCERegistry">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">RegKey</col>
		<col def="i4">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="S0">Value</col>
		<col def="i4">Processor</col>
		<col def="i4">Platform</col>
		<col def="i4">Overwrite</col>
	</table>

	<table name="ISCESetupFile">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">SetupFileKey</col>
		<col def="s255">Name</col>
		<col def="s255">Source</col>
		<col def="i4">Processor</col>
		<col def="i4">Platform</col>
	</table>

	<table name="ISCEShtCut">
		<col key="yes" def="s50">AppKey</col>
		<col key="yes" def="s50">ShtCutKey</col>
		<col def="s255">DisplayName</col>
		<col def="s255">Destination</col>
		<col def="s50">Target</col>
		<col def="i4">Platform</col>
		<col def="S255">StartScreenIcon</col>
	</table>

	<table name="ISChainPackage">
		<col key="yes" def="s72">Package</col>
		<col def="S255">SourcePath</col>
		<col def="S72">ProductCode</col>
		<col def="i2">Order</col>
		<col def="i4">Options</col>
		<col def="S255">InstallCondition</col>
		<col def="S255">RemoveCondition</col>
		<col def="S0">InstallProperties</col>
		<col def="S0">RemoveProperties</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="S72">DisplayName</col>
	</table>

	<table name="ISChainPackageData">
		<col key="yes" def="s72">Package_</col>
		<col key="yes" def="s72">File</col>
		<col def="s50">FilePath</col>
		<col def="I4">Options</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="ISClrWrap">
		<col key="yes" def="s72">Action_</col>
		<col key="yes" def="s72">Name</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISComCatalogAttribute">
		<col key="yes" def="s72">ISComCatalogObject_</col>
		<col key="yes" def="s255">ItemName</col>
		<col def="S0">ItemValue</col>
	</table>

	<table name="ISComCatalogCollection">
		<col key="yes" def="s72">ISComCatalogCollection</col>
		<col def="s72">ISComCatalogObject_</col>
		<col def="s255">CollectionName</col>
	</table>

	<table name="ISComCatalogCollectionObjects">
		<col key="yes" def="s72">ISComCatalogCollection_</col>
		<col key="yes" def="s72">ISComCatalogObject_</col>
	</table>

	<table name="ISComCatalogObject">
		<col key="yes" def="s72">ISComCatalogObject</col>
		<col def="s255">DisplayName</col>
	</table>

	<table name="ISComPlusApplication">
		<col key="yes" def="s72">ISComCatalogObject_</col>
		<col def="S255">ComputerName</col>
		<col def="s72">Component_</col>
		<col def="I2">ISAttributes</col>
		<col def="S0">DepFiles</col>
	</table>

	<table name="ISComPlusApplicationDLL">
		<col key="yes" def="s72">ISComPlusApplicationDLL</col>
		<col def="s72">ISComPlusApplication_</col>
		<col def="s72">ISComCatalogObject_</col>
		<col def="s0">CLSID</col>
		<col def="S0">ProgId</col>
		<col def="S0">DLL</col>
		<col def="S0">AlterDLL</col>
	</table>

	<table name="ISComPlusProxy">
		<col key="yes" def="s72">ISComPlusProxy</col>
		<col def="s72">ISComPlusApplication_</col>
		<col def="S72">Component_</col>
		<col def="I2">ISAttributes</col>
		<col def="S0">DepFiles</col>
	</table>

	<table name="ISComPlusProxyDepFile">
		<col key="yes" def="s72">ISComPlusApplication_</col>
		<col key="yes" def="s72">File_</col>
		<col def="S0">ISPath</col>
	</table>

	<table name="ISComPlusProxyFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">ISComPlusApplicationDLL_</col>
	</table>

	<table name="ISComPlusServerDepFile">
		<col key="yes" def="s72">ISComPlusApplication_</col>
		<col key="yes" def="s72">File_</col>
		<col def="S0">ISPath</col>
	</table>

	<table name="ISComPlusServerFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">ISComPlusApplicationDLL_</col>
	</table>

	<table name="ISComponentExtended">
		<col key="yes" def="s72">Component_</col>
		<col def="I4">OS</col>
		<col def="S0">Language</col>
		<col def="s72">FilterProperty</col>
		<col def="I4">Platforms</col>
		<col def="S0">FTPLocation</col>
		<col def="S0">HTTPLocation</col>
		<col def="S0">Miscellaneous</col>
		<row><td>AdvancedDemos.exe</td><td/><td/><td>_BB50300B_8285_4FAC_A9C1_3E32FCF445A4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Assimp.Interop.dll</td><td/><td/><td>_C4140370_832A_419B_804D_42D9CE51000A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Assimp.dll</td><td/><td/><td>_57122C43_87AE_4FEC_8CBA_20BC29C82592_FILTER</td><td/><td/><td/><td/></row>
		<row><td>BEPUphysics.dll</td><td/><td/><td>_870E02D4_ED71_4601_B26C_F11D5D047979_FILTER</td><td/><td/><td/><td/></row>
		<row><td>BEPUphysics.dll1</td><td/><td/><td>_B52C90D3_36F0_4D5B_97A8_6C693E34CC78_FILTER</td><td/><td/><td/><td/></row>
		<row><td>DPSFPhone.dll</td><td/><td/><td>_26071585_800C_472A_9273_66D1BF6216B7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>DPSFPhone.dll1</td><td/><td/><td>_A9325092_E6ED_4BEC_9D29_3717C7FFF301_FILTER</td><td/><td/><td/><td/></row>
		<row><td>FarseerPhysicsXNA.dll</td><td/><td/><td>_9F28193E_E21B_4D81_A4B9_A8C3D1CEBB07_FILTER</td><td/><td/><td/><td/></row>
		<row><td>FarseerPhysicsXNA.dll1</td><td/><td/><td>_809C4A99_E461_4BD0_8902_F815AA304908_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT</td><td/><td/><td>_CFFDEAC2_6F95_4CA7_B0D7_65E917B90B8A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT1</td><td/><td/><td>_D5C11F16_80C1_4555_8B2F_2E3F9DE51A51_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT10</td><td/><td/><td>_97B7D6A1_9B0D_4919_ACEB_2FCCEFF9C90C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT100</td><td/><td/><td>_EE146328_8E91_47C0_898F_CC88D6C9C8B3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT101</td><td/><td/><td>_762E683F_9B38_43DC_BA05_D1D492BB66E6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT102</td><td/><td/><td>_E2C94FE0_94E7_4CB0_8680_94633E56CA09_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT103</td><td/><td/><td>_C406B9F0_6774_4CFB_B6AD_C52F6434E481_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT104</td><td/><td/><td>_047A6A87_16AB_4746_8A21_2009F1B4338C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT105</td><td/><td/><td>_7EAB411C_E2EA_4ED4_A646_8CBD415397B6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT106</td><td/><td/><td>_B801BC72_F147_457C_86CD_7988D8A97568_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT107</td><td/><td/><td>_E6CB81BE_B1F8_4726_8DC9_A337D0E7B4D2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT108</td><td/><td/><td>_23B1FD3D_FFED_4ABF_A7AB_66A66050341E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT109</td><td/><td/><td>_9BB3AAED_603B_4F56_8C93_46E265A21B12_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT11</td><td/><td/><td>_C4198A3B_86EC_4E6D_A8C9_C2EB4C93170F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT110</td><td/><td/><td>_E56AEED9_30AD_4739_B0DF_D15B7FD38E79_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT111</td><td/><td/><td>_28AFA5D1_2849_4BC8_AC4A_A55497F42690_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT112</td><td/><td/><td>_CE799DF2_C8F5_4568_AE32_584457DA028D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT113</td><td/><td/><td>_C1DAA7B1_E7C5_4961_B84A_83ED274644CD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT114</td><td/><td/><td>_6B4410E2_BF1D_4465_94D5_9EB0F3DD71E2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT115</td><td/><td/><td>_A946D953_B77C_4DF4_8253_43FC51CE0178_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT116</td><td/><td/><td>_3CAE382C_5463_4427_BEE6_253808E8D677_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT117</td><td/><td/><td>_BC3728EC_4895_4C2E_B3BA_6845AC9521AF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT118</td><td/><td/><td>_1D48A4C7_EF94_45E6_9000_185D16BB515F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT119</td><td/><td/><td>_8E5157CC_663B_4C70_A97A_F561E0E197EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT12</td><td/><td/><td>_727DCB18_F910_4CC8_9ECE_B52AFE2F2D35_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT120</td><td/><td/><td>_21A48B33_0E7C_46FE_9005_25EA26C8BF1B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT121</td><td/><td/><td>_8AA5C496_F5B9_402F_A9C7_61EC40C37E4F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT122</td><td/><td/><td>_596257FA_1B98_4B6B_9438_B459973E9ACE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT123</td><td/><td/><td>_7D1574B6_1EFB_472C_BD1F_1D59E307E866_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT124</td><td/><td/><td>_759708EC_C880_41E0_ACD5_5B5ED0017A29_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT125</td><td/><td/><td>_05F63C9D_CC89_49E6_8CDF_F9E8C2AD2387_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT126</td><td/><td/><td>_90C80D45_C85F_4CCF_AA18_00ABCCE1314D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT127</td><td/><td/><td>_6A3A3E1D_60C9_4D5B_BE44_3A46408D691B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT128</td><td/><td/><td>_2E18BC73_B1DD_49CA_86B2_42CFB8EC2549_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT129</td><td/><td/><td>_17A08A8B_0FAF_4C4B_A0F4_FFDD6C54DA15_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT13</td><td/><td/><td>_9370DC99_83F9_4150_859E_BF97444933AA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT130</td><td/><td/><td>_3D33D44C_9EB2_4B6A_86FB_7512B52A27FC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT131</td><td/><td/><td>_FEB07FEC_2DAF_43BC_8E07_C59FB33E20A5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT132</td><td/><td/><td>_67D9942F_C8B0_439C_A761_2378E3E54608_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT133</td><td/><td/><td>_94A18B07_EB81_41E9_BEEA_D1BA5634B96A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT134</td><td/><td/><td>_ADE2C9CE_E81D_44F0_B92F_CAB48E94F26C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT135</td><td/><td/><td>_E0884714_1509_4E4F_A51E_2917D7D2247B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT136</td><td/><td/><td>_E6658366_A59F_42F8_827F_1DA1C4F08F19_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT137</td><td/><td/><td>_B8DEE832_A4C9_4D8A_AA57_813D24BA786E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT14</td><td/><td/><td>_7D8B9257_59CC_4440_A942_9558381ECF04_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT15</td><td/><td/><td>_48147ED1_2902_4688_AE86_647C7AD1571A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT16</td><td/><td/><td>_F0C8AD80_6FF8_4550_8E27_CA44C89029AB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT17</td><td/><td/><td>_4AF8FD80_54F1_4DD9_97CA_4C4E2634E087_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT18</td><td/><td/><td>_C558A688_61CB_41D6_805A_224C4B5D1B3E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT19</td><td/><td/><td>_AF099D80_B1AE_46E7_8CE3_06D099136F7D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT2</td><td/><td/><td>_85B2FBF2_564C_4595_841A_23842BEFCE1C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT20</td><td/><td/><td>_B4048C71_5B95_4F9B_87B4_B4CCBE9C7AF3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT21</td><td/><td/><td>_23441725_4071_48A4_A1C1_1C7B47AD9442_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT22</td><td/><td/><td>_7774F8ED_F231_4349_A4B2_30996B89593A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT23</td><td/><td/><td>_BCCA0828_ED9F_4330_A265_8BAD9B9CFE58_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT24</td><td/><td/><td>_955BAF21_229E_4E6D_9C25_BCE1EF2AB362_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT25</td><td/><td/><td>_C38BF520_7CA8_4FA7_82B8_10410BB2A563_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT26</td><td/><td/><td>_D02B4548_7079_4BAC_A533_B9F458F98CE6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT27</td><td/><td/><td>_9FDE5A73_F806_4945_B11A_7595E1ABFCE7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT28</td><td/><td/><td>_FF5DE617_FECB_4EE4_8300_DF9F91A2C285_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT29</td><td/><td/><td>_244E40D3_306E_45F8_BABE_BFCE08C73EB9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT3</td><td/><td/><td>_733EAC54_4208_411C_914F_E588A8A9C826_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT30</td><td/><td/><td>_356BE3EA_1CE6_4EBF_AABF_5AFCA5B871FD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT31</td><td/><td/><td>_99FD1854_3B0B_4856_88BD_74868596A6D8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT32</td><td/><td/><td>_99EEAA93_EAE4_4400_B7EA_340C514F8363_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT33</td><td/><td/><td>_2C5743D9_7F58_4FE8_AC45_8CB5C2D387BD_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT34</td><td/><td/><td>_B4DE79C1_84CC_439F_891C_45243EBBB247_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT35</td><td/><td/><td>_0CCC848A_6CE5_4409_B624_C64EEAC8A073_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT36</td><td/><td/><td>_1E33F36D_1200_4627_9E62_28643CE078D0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT37</td><td/><td/><td>_0EA288B2_9592_422A_8BF8_7F57D937F68C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT38</td><td/><td/><td>_15D13C59_3F9E_4F3D_ACBA_D00E784F8027_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT39</td><td/><td/><td>_0C22D2B3_AE29_4060_821F_172C5647B05F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT4</td><td/><td/><td>_86AEEB3F_B3AA_4955_B24C_C61AD44C380A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT40</td><td/><td/><td>_DC74EEBD_EEB4_4DB4_B9AB_AFF9D23A3725_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT41</td><td/><td/><td>_7FF1253F_BD20_4860_97FA_D960FB574AA6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT42</td><td/><td/><td>_87582CA8_9CBF_49C6_8008_8A27FFB5CD25_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT43</td><td/><td/><td>_60CB8538_6FC4_4727_B52D_34939C4FA99E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT44</td><td/><td/><td>_D0E9EB33_D90D_49A6_9AF3_F2557721BFD0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT45</td><td/><td/><td>_52C16B17_2F3B_4FE1_9CF0_30224F611DA8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT46</td><td/><td/><td>_79953825_BFCA_4E82_BCFE_AC3C51413AD6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT47</td><td/><td/><td>_B09818BE_07CA_4CB4_8692_5C44BA070748_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT48</td><td/><td/><td>_223CD4DD_F3F3_4C3C_B2B0_7A7388229516_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT49</td><td/><td/><td>_9F231592_3D4D_4545_997B_EE18A8AF3F47_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT5</td><td/><td/><td>_B5763974_13E9_4D65_8897_BBCB4A9B5D16_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT50</td><td/><td/><td>_2A3C00A3_02A9_4375_8805_6FBCF3709922_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT51</td><td/><td/><td>_190A3D07_2712_48D9_8F01_A4B67DF708BF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT52</td><td/><td/><td>_382079D7_6D8B_4F4E_BAD4_A9C7590AD89C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT53</td><td/><td/><td>_D169517E_A9A7_4302_BCA1_3B58E21725AC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT54</td><td/><td/><td>_1E9B8772_8B8B_4706_9DAD_26483AE38D05_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT55</td><td/><td/><td>_A99E6D53_3117_4EF0_9DDE_5425E31FDBFA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT56</td><td/><td/><td>_DB187B15_886F_42EA_9E9C_B825698FDBA5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT57</td><td/><td/><td>_618BD50E_5EA4_4CB2_8D34_77C39121D43F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT58</td><td/><td/><td>_3DC02188_E873_483F_ABA4_F15DF12B67E5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT59</td><td/><td/><td>_B8726C2D_5455_431C_B619_2810EEFF6351_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT6</td><td/><td/><td>_2FA20897_9891_40EF_B5D3_7B0690567B41_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT60</td><td/><td/><td>_3C9622A2_E271_421C_B83B_220A920B73E9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT61</td><td/><td/><td>_E7D85ADC_10D0_4A93_A78E_5ADFA9F7711F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT62</td><td/><td/><td>_A5A88978_5241_4106_983D_D0E85C83D46A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT63</td><td/><td/><td>_8C07F10D_1098_495B_BDCA_D598C6559BCB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT64</td><td/><td/><td>_9EE80126_4E42_4333_921E_0E27BC2EA551_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT65</td><td/><td/><td>_5206667E_1591_4981_8220_78E428859608_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT66</td><td/><td/><td>_6B37D52C_C02D_47A4_9607_9795397CCD35_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT67</td><td/><td/><td>_815300E7_ACDD_49E1_BF05_79CDDA89F0EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT68</td><td/><td/><td>_A604B50E_021F_4FA6_8C8B_10CC9819F3FE_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT69</td><td/><td/><td>_2D4C690E_6065_483E_844C_E673551A4F3B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT7</td><td/><td/><td>_83C37F36_C497_4CF8_ACF4_210194249B06_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT70</td><td/><td/><td>_1D394720_D826_4152_A032_8D33A56818A5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT71</td><td/><td/><td>_8D83FD42_CE53_49A5_8DB3_CF80E80B71AC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT72</td><td/><td/><td>_158C3559_3C47_473C_A483_0B3F04795798_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT73</td><td/><td/><td>_85063E7B_75BE_4829_9D57_6E20F6597CDB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT74</td><td/><td/><td>_676A53EE_4D8B_455B_A843_52AE1CA14DAF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT75</td><td/><td/><td>_179FF5FD_5475_4ED6_9131_02893BC51185_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT76</td><td/><td/><td>_478C5240_7127_4ADC_80FC_28714697724B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT77</td><td/><td/><td>_1319BE37_2758_4E4F_B6AF_217FB22DE10A_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT78</td><td/><td/><td>_55CDE10E_C49C_4F82_8297_9510921937F8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT79</td><td/><td/><td>_1855A06C_0392_419A_A8AE_D5C37CE3BC74_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT8</td><td/><td/><td>_3CFEC77F_EEA2_44F9_8060_29D0CEBB48B5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT80</td><td/><td/><td>_40DE7449_072A_4E8F_9183_54D201F88438_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT81</td><td/><td/><td>_E87A1BF8_820C_4AE7_A267_C0AFF7301908_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT82</td><td/><td/><td>_218FF818_51B5_45F3_A62F_AFE522BBC917_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT83</td><td/><td/><td>_101E236E_8DC4_4094_8F0A_C0564688ECF3_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT84</td><td/><td/><td>_AFF2CD96_3690_4C60_AC27_24ADC27E068D_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT85</td><td/><td/><td>_49A8C9F0_16F7_4F27_824F_474C5F05D931_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT86</td><td/><td/><td>_A47FEB4C_AC51_4A37_A9C6_ADC6DAC02241_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT87</td><td/><td/><td>_3EDC0278_949E_40B0_9D00_22FF6B281CAB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT88</td><td/><td/><td>_1D6D0231_3D35_4630_9E50_792556E0A5C5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT89</td><td/><td/><td>_3FF953A9_CCD4_4CD5_99F8_D93B13630F25_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT9</td><td/><td/><td>_6B796AB5_A1BD_4B89_9E61_606497B255E0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT90</td><td/><td/><td>_AD1CDF22_845E_4353_901B_56B4BA83CB20_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT91</td><td/><td/><td>_BD6C252F_AF0D_4A26_92DA_EB9C4FDBE661_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT92</td><td/><td/><td>_616729E7_EDFC_4072_B4AC_3AB227DFF811_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT93</td><td/><td/><td>_9F7714A2_9EFB_4DE9_9E51_4B1F0B976667_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT94</td><td/><td/><td>_A69EA1E5_6428_4762_A998_BF02C70716E6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT95</td><td/><td/><td>_38DA05DE_796C_4307_A2A5_A8D00613ED77_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT96</td><td/><td/><td>_20C6B217_3CB5_4C2E_B17C_523B76F326F9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT97</td><td/><td/><td>_5E84348E_5FDE_428C_9847_C85731A48AE5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT98</td><td/><td/><td>_073D71DB_6253_465C_9A93_48ED67977A96_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ISX_DEFAULTCOMPONENT99</td><td/><td/><td>_E4AEF84E_8D76_4DBC_8831_BDC1127E605F_FILTER</td><td/><td/><td/><td/></row>
		<row><td>ModelImporter.dll</td><td/><td/><td>_726FC3BA_51FE_4EF7_B0DB_EF6F5CEBBAC8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>NewComponent1</td><td/><td/><td>_1E3A9096_A28D_4640_9732_75153379F168_FILTER</td><td/><td/><td/><td/></row>
		<row><td>NxCharacter.dll</td><td/><td/><td>_63ACBC4F_9762_4ECE_8125_CED07DFEE66C_FILTER</td><td/><td/><td/><td/></row>
		<row><td>NxCharacter.dll1</td><td/><td/><td>_F72FA8D2_96B2_461F_B6E2_302D9BBE96D4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Osiris.Terrain.Content.Pipeline.dll</td><td/><td/><td>_C6221A41_C02C_4DA2_968A_1E50F012DBC6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXCooking.dll</td><td/><td/><td>_05A89B4B_1AC1_46BC_8069_D986E1A38FAC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXCooking.dll1</td><td/><td/><td>_0B4F3273_7D4E_407A_B7CD_99E2AF1372D2_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXCore.dll</td><td/><td/><td>_592042C9_ACE8_4F11_B0E7_CAAE3B1540E6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXCore.dll1</td><td/><td/><td>_7028A95F_079C_4663_9257_8D7F9B41DD63_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXDevice.dll</td><td/><td/><td>_F06A37C5_A733_4C73_AA51_23C28BADD7D9_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXDevice.dll1</td><td/><td/><td>_EB3DF6CC_2C71_4033_8CDF_A11DDF38F4A8_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXLoader.dll</td><td/><td/><td>_723883C6_529B_4B44_994C_3AFE74C52DEB_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PhysXLoader.dll1</td><td/><td/><td>_160E68EE_15ED_48D3_AE16_A94F6C268511_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEngineDebug.dll</td><td/><td/><td>_544E3A79_3052_4232_A72D_CD355760CC6E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEngineDebug.dll1</td><td/><td/><td>_3E25983D_EC91_4C15_B82E_E5CF701D7794_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEngineDebug.dll2</td><td/><td/><td>_E1CF3326_7DFB_4A62_BA36_3EA3BAA94DC0_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEngineDebug.dll3</td><td/><td/><td>_9A7FE0A3_6442_4861_BD20_1EC4DA4AEFD6_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEngineDebug.dll4</td><td/><td/><td>_8B10201C_9BDF_449D_A7DB_34328222FF1B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEngineDebug.dll5</td><td/><td/><td>_0ED43E27_81CC_412F_B52E_0D8A79758EA4_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePhone7.dll</td><td/><td/><td>_D5DF0098_8DEF_447B_A82E_151C45E6B637_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePhone7.dll1</td><td/><td/><td>_0465CB3B_FBAB_4E68_A8CB_3C78E2565906_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePipelineDebug.dll</td><td/><td/><td>_855F2B1E_9E7F_42CC_9C30_F2252FBB1470_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePipelineDebug.dll1</td><td/><td/><td>_6668B917_BBE2_44FA_AE18_F9BEA0B2B831_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePipelineDebug.dll2</td><td/><td/><td>_555ADEAE_5514_4FEE_BB7C_320E8BC032FA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePipelineDebug.dll3</td><td/><td/><td>_325DDC25_CD78_4563_89A8_6064D2BE5020_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsEnginePipelineDebug.dll4</td><td/><td/><td>_4D7DD7C3_74DC_43F9_A2DF_55631A35CA43_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsScripts.dll</td><td/><td/><td>_38E898A6_5183_40CD_AFBA_DD185FF98D1E_FILTER</td><td/><td/><td/><td/></row>
		<row><td>PloobsSerializator.dll</td><td/><td/><td>_E1E855EE_52A0_4568_87A4_48A2FF372B22_FILTER</td><td/><td/><td/><td/></row>
		<row><td>Ploobs_Engine_Help.chm</td><td/><td/><td>_98DD2D38_5A24_4AB2_B332_F22E15A54F42_FILTER</td><td/><td/><td/><td/></row>
		<row><td>RVO2DWrapper.dll</td><td/><td/><td>_680E6B5B_EB32_41CE_998D_136213D81743_FILTER</td><td/><td/><td/><td/></row>
		<row><td>RVO3D.dll</td><td/><td/><td>_CD5C0FB2_33ED_4ECD_969C_98A53FAAF290_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SharedOsiris.dll</td><td/><td/><td>_D41D8215_F0CF_4C59_8B83_AF8DE735A4EA_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SkinnedModel.dll</td><td/><td/><td>_948958F5_7A72_4D38_B3CE_78382729DCC5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SkinnedModel.dll1</td><td/><td/><td>_767768FF_62E3_49A3_A013_8D656CE91E55_FILTER</td><td/><td/><td/><td/></row>
		<row><td>SkinnedModelPipeline.dll</td><td/><td/><td>_A1182AF3_F3E5_404F_8360_4A0E0A5E4A94_FILTER</td><td/><td/><td/><td/></row>
		<row><td>StillDesign.PhysX.Net.dll</td><td/><td/><td>_C8379D87_1FD7_4B3F_AE9C_1BA00A78C9C7_FILTER</td><td/><td/><td/><td/></row>
		<row><td>StillDesign.PhysX.Net.dll1</td><td/><td/><td>_F8835431_F407_440F_9182_56713DF5D402_FILTER</td><td/><td/><td/><td/></row>
		<row><td>XNAnimation.dll</td><td/><td/><td>_304BA9F0_6FFE_4683_96F7_AB9EB5A23E8B_FILTER</td><td/><td/><td/><td/></row>
		<row><td>XNAnimation.dll1</td><td/><td/><td>_E168F6E1_E165_4FA8_BB94_D2D48E702AD5_FILTER</td><td/><td/><td/><td/></row>
		<row><td>XNAnimationPipeline.dll</td><td/><td/><td>_C1724642_29C4_4C13_8E73_5856890BD0FC_FILTER</td><td/><td/><td/><td/></row>
		<row><td>XNAnimationPipeline.dll1</td><td/><td/><td>_939090D6_E712_469E_B3A2_691C0C217C21_FILTER</td><td/><td/><td/><td/></row>
		<row><td>cudart32_30_9.dll</td><td/><td/><td>_04199171_9A0E_4701_9C8F_BA1779E9C0FF_FILTER</td><td/><td/><td/><td/></row>
		<row><td>cudart32_30_9.dll1</td><td/><td/><td>_0B004BE0_553D_4083_8AE3_EF5322935EA0_FILTER</td><td/><td/><td/><td/></row>
	</table>

	<table name="ISCustomActionReference">
		<col key="yes" def="s72">Action_</col>
		<col def="S0">Description</col>
		<col def="S255">FileType</col>
		<col def="S255">ISCAReferenceFilePath</col>
	</table>

	<table name="ISDIMDependency">
		<col key="yes" def="s72">ISDIMReference_</col>
		<col def="s255">RequiredUUID</col>
		<col def="S255">RequiredMajorVersion</col>
		<col def="S255">RequiredMinorVersion</col>
		<col def="S255">RequiredBuildVersion</col>
		<col def="S255">RequiredRevisionVersion</col>
	</table>

	<table name="ISDIMReference">
		<col key="yes" def="s72">ISDIMReference</col>
		<col def="S0">ISBuildSourcePath</col>
	</table>

	<table name="ISDIMReferenceDependencies">
		<col key="yes" def="s72">ISDIMReference_Parent</col>
		<col key="yes" def="s72">ISDIMDependency_</col>
	</table>

	<table name="ISDIMVariable">
		<col key="yes" def="s72">ISDIMVariable</col>
		<col def="s72">ISDIMReference_</col>
		<col def="s0">Name</col>
		<col def="S0">NewValue</col>
		<col def="I4">Type</col>
	</table>

	<table name="ISDLLWrapper">
		<col key="yes" def="s72">EntryPoint</col>
		<col def="I4">Type</col>
		<col def="s0">Source</col>
		<col def="s255">Target</col>
	</table>

	<table name="ISDRMFile">
		<col key="yes" def="s72">ISDRMFile</col>
		<col def="S72">File_</col>
		<col def="S72">ISDRMLicense_</col>
		<col def="s255">Shell</col>
	</table>

	<table name="ISDRMFileAttribute">
		<col key="yes" def="s72">ISDRMFile_</col>
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISDRMLicense">
		<col key="yes" def="s72">ISDRMLicense</col>
		<col def="S255">Description</col>
		<col def="S50">ProjectVersion</col>
		<col def="I4">Attributes</col>
		<col def="S255">LicenseNumber</col>
		<col def="S255">RequestCode</col>
		<col def="S255">ResponseCode</col>
	</table>

	<table name="ISDependency">
		<col key="yes" def="S50">ISDependency</col>
		<col def="I2">Exclude</col>
		<row><td>VISUAL C++ 10.0 CRT (IA64)</td><td>1</td></row>
	</table>

	<table name="ISDisk1File">
		<col key="yes" def="s72">ISDisk1File</col>
		<col def="s255">ISBuildSourcePath</col>
		<col def="I4">Disk</col>
	</table>

	<table name="ISDynamicFile">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="s255">SourceFolder</col>
		<col def="I2">IncludeFlags</col>
		<col def="S0">IncludeFiles</col>
		<col def="S0">ExcludeFiles</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISFeatureDIMReferences">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">ISDIMReference_</col>
	</table>

	<table name="ISFeatureMergeModuleExcludes">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s255">ModuleID</col>
		<col key="yes" def="i2">Language</col>
	</table>

	<table name="ISFeatureMergeModules">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s255">ISMergeModule_</col>
		<col key="yes" def="i2">Language_</col>
	</table>

	<table name="ISFeatureSetupPrerequisites">
		<col key="yes" def="s38">Feature_</col>
		<col key="yes" def="s72">ISSetupPrerequisites_</col>
	</table>

	<table name="ISFileManifests">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">Manifest_</col>
	</table>

	<table name="ISIISItem">
		<col key="yes" def="s72">ISIISItem</col>
		<col def="S72">ISIISItem_Parent</col>
		<col def="L255">DisplayName</col>
		<col def="i4">Type</col>
		<col def="S72">Component_</col>
	</table>

	<table name="ISIISProperty">
		<col key="yes" def="s72">ISIISProperty</col>
		<col key="yes" def="s72">ISIISItem_</col>
		<col def="S0">Schema</col>
		<col def="S255">FriendlyName</col>
		<col def="I4">MetaDataProp</col>
		<col def="I4">MetaDataType</col>
		<col def="I4">MetaDataUserType</col>
		<col def="I4">MetaDataAttributes</col>
		<col def="L0">MetaDataValue</col>
		<col def="I4">Order</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISInstallScriptAction">
		<col key="yes" def="s72">EntryPoint</col>
		<col def="I4">Type</col>
		<col def="s72">Source</col>
		<col def="S255">Target</col>
	</table>

	<table name="ISLanguage">
		<col key="yes" def="s50">ISLanguage</col>
		<col def="I2">Included</col>
		<row><td>1033</td><td>1</td></row>
	</table>

	<table name="ISLinkerLibrary">
		<col key="yes" def="s72">ISLinkerLibrary</col>
		<col def="s255">Library</col>
		<col def="i4">Order</col>
		<row><td>isrt.obl</td><td>isrt.obl</td><td>2</td></row>
		<row><td>iswi.obl</td><td>iswi.obl</td><td>1</td></row>
	</table>

	<table name="ISLocalControl">
		<col key="yes" def="s72">Dialog_</col>
		<col key="yes" def="s50">Control_</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="I4">Attributes</col>
		<col def="I2">X</col>
		<col def="I2">Y</col>
		<col def="I2">Width</col>
		<col def="I2">Height</col>
		<col def="S72">Binary_</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="ISLocalDialog">
		<col key="yes" def="S50">Dialog_</col>
		<col key="yes" def="S50">ISLanguage_</col>
		<col def="I4">Attributes</col>
		<col def="S72">TextStyle_</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
	</table>

	<table name="ISLocalRadioButton">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
	</table>

	<table name="ISLockPermissions">
		<col key="yes" def="s72">LockObject</col>
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="S255">Domain</col>
		<col key="yes" def="s255">User</col>
		<col def="I4">Permission</col>
		<col def="I4">Attributes</col>
	</table>

	<table name="ISLogicalDisk">
		<col key="yes" def="i2">DiskId</col>
		<col key="yes" def="s255">ISProductConfiguration_</col>
		<col key="yes" def="s255">ISRelease_</col>
		<col def="i2">LastSequence</col>
		<col def="L64">DiskPrompt</col>
		<col def="S255">Cabinet</col>
		<col def="S32">VolumeLabel</col>
		<col def="S32">Source</col>
	</table>

	<table name="ISLogicalDiskFeatures">
		<col key="yes" def="i2">ISLogicalDisk_</col>
		<col key="yes" def="s255">ISProductConfiguration_</col>
		<col key="yes" def="s255">ISRelease_</col>
		<col key="yes" def="S38">Feature_</col>
		<col def="i2">Sequence</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISMergeModule">
		<col key="yes" def="s255">ISMergeModule</col>
		<col key="yes" def="i2">Language</col>
		<col def="s255">Name</col>
		<col def="S255">Destination</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISMergeModuleCfgValues">
		<col key="yes" def="s255">ISMergeModule_</col>
		<col key="yes" def="i2">Language_</col>
		<col key="yes" def="s72">ModuleConfiguration_</col>
		<col def="L0">Value</col>
		<col def="i2">Format</col>
		<col def="L255">Type</col>
		<col def="L255">ContextData</col>
		<col def="L255">DefaultValue</col>
		<col def="I2">Attributes</col>
		<col def="L255">DisplayName</col>
		<col def="L255">Description</col>
		<col def="L255">HelpLocation</col>
		<col def="L255">HelpKeyword</col>
	</table>

	<table name="ISObject">
		<col key="yes" def="s50">ObjectName</col>
		<col def="s15">Language</col>
	</table>

	<table name="ISObjectProperty">
		<col key="yes" def="S50">ObjectName</col>
		<col key="yes" def="S50">Property</col>
		<col def="S0">Value</col>
		<col def="I2">IncludeInBuild</col>
	</table>

	<table name="ISPalmApp">
		<col key="yes" def="s72">PalmApp</col>
		<col key="yes" def="s72">Component</col>
	</table>

	<table name="ISPalmAppFile">
		<col key="yes" def="s72">PalmApp</col>
		<col key="yes" def="s72">FileKey</col>
		<col def="i4">Destination</col>
	</table>

	<table name="ISPatchConfigImage">
		<col key="yes" def="S72">PatchConfiguration_</col>
		<col key="yes" def="s72">UpgradedImage_</col>
	</table>

	<table name="ISPatchConfiguration">
		<col key="yes" def="s72">Name</col>
		<col def="i2">CanPCDiffer</col>
		<col def="i2">CanPVDiffer</col>
		<col def="i2">IncludeWholeFiles</col>
		<col def="i2">LeaveDecompressed</col>
		<col def="i2">OptimizeForSize</col>
		<col def="i2">EnablePatchCache</col>
		<col def="S0">PatchCacheDir</col>
		<col def="i4">Flags</col>
		<col def="S0">PatchGuidsToReplace</col>
		<col def="s0">TargetProductCodes</col>
		<col def="s50">PatchGuid</col>
		<col def="s0">OutputPath</col>
		<col def="i2">MinMsiVersion</col>
		<col def="I4">Attributes</col>
	</table>

	<table name="ISPatchConfigurationProperty">
		<col key="yes" def="S72">ISPatchConfiguration_</col>
		<col key="yes" def="S50">Property</col>
		<col def="S50">Value</col>
	</table>

	<table name="ISPatchExternalFile">
		<col key="yes" def="s50">Name</col>
		<col key="yes" def="s13">ISUpgradedImage_</col>
		<col def="s72">FileKey</col>
		<col def="s255">FilePath</col>
	</table>

	<table name="ISPatchWholeFile">
		<col key="yes" def="s50">UpgradedImage</col>
		<col key="yes" def="s72">FileKey</col>
		<col def="S72">Component</col>
	</table>

	<table name="ISPathVariable">
		<col key="yes" def="s32">ISPathVariable</col>
		<col def="S255">Value</col>
		<col def="S255">TestValue</col>
		<col def="i4">Type</col>
		<row><td>CommonFilesFolder</td><td/><td/><td>1</td></row>
		<row><td>ISPROJECTDIR</td><td/><td/><td>1</td></row>
		<row><td>ISProductFolder</td><td/><td/><td>1</td></row>
		<row><td>ISProjectDataFolder</td><td/><td/><td>1</td></row>
		<row><td>ISProjectFolder</td><td/><td/><td>1</td></row>
		<row><td>ProgramFilesFolder</td><td/><td/><td>1</td></row>
		<row><td>SystemFolder</td><td/><td/><td>1</td></row>
		<row><td>WindowsFolder</td><td/><td/><td>1</td></row>
	</table>

	<table name="ISProductConfiguration">
		<col key="yes" def="s72">ISProductConfiguration</col>
		<col def="S255">ProductConfigurationFlags</col>
		<col def="I4">GeneratePackageCode</col>
		<row><td>Express</td><td/><td>1</td></row>
	</table>

	<table name="ISProductConfigurationInstance">
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="i2">InstanceId</col>
		<col key="yes" def="s72">Property</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISProductConfigurationProperty">
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s72">Property</col>
		<col def="L255">Value</col>
		<row><td>Express</td><td>MSIPackageFileName</td><td>PloobsEngine</td></row>
	</table>

	<table name="ISRelease">
		<col key="yes" def="s72">ISRelease</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="s255">BuildLocation</col>
		<col def="s255">PackageName</col>
		<col def="i4">Type</col>
		<col def="s0">SupportedLanguagesUI</col>
		<col def="i4">MsiSourceType</col>
		<col def="i4">ReleaseType</col>
		<col def="s72">Platforms</col>
		<col def="S0">SupportedLanguagesData</col>
		<col def="s6">DefaultLanguage</col>
		<col def="i4">SupportedOSs</col>
		<col def="s50">DiskSize</col>
		<col def="i4">DiskSizeUnit</col>
		<col def="i4">DiskClusterSize</col>
		<col def="S0">ReleaseFlags</col>
		<col def="i4">DiskSpanning</col>
		<col def="S255">SynchMsi</col>
		<col def="s255">MediaLocation</col>
		<col def="S255">URLLocation</col>
		<col def="S255">DigitalURL</col>
		<col def="S255">DigitalPVK</col>
		<col def="S255">DigitalSPC</col>
		<col def="S255">Password</col>
		<col def="S255">VersionCopyright</col>
		<col def="i4">Attributes</col>
		<col def="S255">CDBrowser</col>
		<col def="S255">DotNetBuildConfiguration</col>
		<col def="S255">MsiCommandLine</col>
		<col def="I4">ISSetupPrerequisiteLocation</col>
		<row><td>CD_ROM</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>0</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>650</td><td>0</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>Custom</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>2</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>100</td><td>0</td><td>1024</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-10</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>8.75</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-18</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>15.83</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-5</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>1</td><td>Intel</td><td/><td>1033</td><td>0</td><td>4.38</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75933</td><td/><td/><td/><td>3</td></row>
		<row><td>DVD-9</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>Default</td><td>3</td><td>1033</td><td>0</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>7.95</td><td>1</td><td>2048</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>75805</td><td/><td/><td/><td>3</td></row>
		<row><td>Debug</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>1</td><td>1033</td><td>2</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>8464397</td><td/><td/><td/><td/></row>
		<row><td>Release</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>1</td><td>1033</td><td>2</td><td>2</td><td>Intel</td><td/><td>1033</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>8464397</td><td/><td/><td/><td/></row>
		<row><td>SingleImage</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>1</td><td>1033</td><td>0</td><td>1</td><td>Intel</td><td/><td>1033</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>108573</td><td/><td/><td/><td>3</td></row>
		<row><td>WebDeployment</td><td>Express</td><td>&lt;ISProjectDataFolder&gt;</td><td>PackageName</td><td>4</td><td>1033</td><td>2</td><td>1</td><td>Intel</td><td/><td>1033</td><td>0</td><td>0</td><td>0</td><td>0</td><td/><td>0</td><td/><td>MediaLocation</td><td/><td>http://</td><td/><td/><td/><td/><td>124941</td><td/><td/><td/><td>3</td></row>
	</table>

	<table name="ISReleaseASPublishInfo">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="S0">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="ISReleaseExtended">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="I4">WebType</col>
		<col def="S255">WebURL</col>
		<col def="I4">WebCabSize</col>
		<col def="S255">OneClickCabName</col>
		<col def="S255">OneClickHtmlName</col>
		<col def="S255">WebLocalCachePath</col>
		<col def="I4">EngineLocation</col>
		<col def="S255">Win9xMsiUrl</col>
		<col def="S255">WinNTMsiUrl</col>
		<col def="I4">ISEngineLocation</col>
		<col def="S255">ISEngineURL</col>
		<col def="I4">OneClickTargetBrowser</col>
		<col def="S255">DigitalCertificateIdNS</col>
		<col def="S255">DigitalCertificateDBaseNS</col>
		<col def="S255">DigitalCertificatePasswordNS</col>
		<col def="I4">DotNetRedistLocation</col>
		<col def="S255">DotNetRedistURL</col>
		<col def="I4">DotNetVersion</col>
		<col def="S255">DotNetBaseLanguage</col>
		<col def="S0">DotNetLangaugePacks</col>
		<col def="S255">DotNetFxCmdLine</col>
		<col def="S255">DotNetLangPackCmdLine</col>
		<col def="S50">JSharpCmdLine</col>
		<col def="I4">Attributes</col>
		<col def="I4">JSharpRedistLocation</col>
		<col def="I4">MsiEngineVersion</col>
		<col def="S255">WinMsi30Url</col>
		<col def="S255">CertPassword</col>
		<row><td>CD_ROM</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>Custom</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-10</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-18</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-5</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>1</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>DVD-9</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>Debug</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td/><td>http://www.installengine.com/cert05/isengine</td><td/><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td>16</td><td>3</td><td>4</td><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>Release</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>0</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td/><td>http://www.installengine.com/cert05/isengine</td><td/><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td>16</td><td>3</td><td>4</td><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>SingleImage</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>install</td><td>install</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>1</td><td>http://www.installengine.com/Msiengine20</td><td>http://www.installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>0</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
		<row><td>WebDeployment</td><td>Express</td><td>0</td><td>http://</td><td>0</td><td>setup</td><td>Default</td><td>[LocalAppDataFolder]Downloaded Installations</td><td>2</td><td>http://www.Installengine.com/Msiengine20</td><td>http://www.Installengine.com/Msiengine20</td><td>0</td><td>http://www.installengine.com/cert05/isengine</td><td>2</td><td/><td/><td/><td>3</td><td>http://www.installengine.com/cert05/dotnetfx</td><td>0</td><td>1033</td><td/><td/><td/><td/><td/><td>3</td><td/><td>http://www.installengine.com/Msiengine30</td><td/></row>
	</table>

	<table name="ISReleaseProperty">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s0">Value</col>
	</table>

	<table name="ISReleasePublishInfo">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col def="S255">Repository</col>
		<col def="S255">DisplayName</col>
		<col def="S255">Publisher</col>
		<col def="S255">Description</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISSQLConnection">
		<col key="yes" def="s72">ISSQLConnection</col>
		<col def="s255">Server</col>
		<col def="s255">Database</col>
		<col def="s255">UserName</col>
		<col def="s255">Password</col>
		<col def="s255">Authentication</col>
		<col def="i2">Attributes</col>
		<col def="i2">Order</col>
		<col def="S0">Comments</col>
		<col def="I4">CmdTimeout</col>
		<col def="S0">BatchSeparator</col>
		<col def="S0">ScriptVersion_Table</col>
		<col def="S0">ScriptVersion_Column</col>
	</table>

	<table name="ISSQLConnectionDBServer">
		<col key="yes" def="s72">ISSQLConnectionDBServer</col>
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col key="yes" def="s72">ISSQLDBMetaData_</col>
		<col def="i2">Order</col>
	</table>

	<table name="ISSQLConnectionScript">
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="i2">Order</col>
	</table>

	<table name="ISSQLDBMetaData">
		<col key="yes" def="s72">ISSQLDBMetaData</col>
		<col def="S0">DisplayName</col>
		<col def="S0">AdoDriverName</col>
		<col def="S0">AdoCxnDriver</col>
		<col def="S0">AdoCxnServer</col>
		<col def="S0">AdoCxnDatabase</col>
		<col def="S0">AdoCxnUserID</col>
		<col def="S0">AdoCxnPassword</col>
		<col def="S0">AdoCxnWindowsSecurity</col>
		<col def="S0">AdoCxnNetLibrary</col>
		<col def="S0">TestDatabaseCmd</col>
		<col def="S0">TestTableCmd</col>
		<col def="S0">VersionInfoCmd</col>
		<col def="S0">VersionBeginToken</col>
		<col def="S0">VersionEndToken</col>
		<col def="S0">LocalInstanceNames</col>
		<col def="S0">CreateDbCmd</col>
		<col def="S0">SwitchDbCmd</col>
		<col def="I4">ISAttributes</col>
		<col def="S0">TestTableCmd2</col>
		<col def="S0">WinAuthentUserId</col>
		<col def="S0">DsnODBCName</col>
		<col def="S0">AdoCxnPort</col>
		<col def="S0">AdoCxnAdditional</col>
		<col def="S0">QueryDatabasesCmd</col>
		<col def="S0">CreateTableCmd</col>
		<col def="S0">InsertRecordCmd</col>
		<col def="S0">SelectTableCmd</col>
		<col def="S0">ScriptVersion_Table</col>
		<col def="S0">ScriptVersion_Column</col>
		<col def="S0">ScriptVersion_ColumnType</col>
	</table>

	<table name="ISSQLRequirement">
		<col key="yes" def="s72">ISSQLRequirement</col>
		<col key="yes" def="s72">ISSQLConnection_</col>
		<col def="S15">MajorVersion</col>
		<col def="S25">ServicePackLevel</col>
		<col def="i4">Attributes</col>
		<col def="S72">ISSQLConnectionDBServer_</col>
	</table>

	<table name="ISSQLScriptError">
		<col key="yes" def="i4">ErrNumber</col>
		<col key="yes" def="S72">ISSQLScriptFile_</col>
		<col def="i2">ErrHandling</col>
		<col def="L255">Message</col>
		<col def="i2">Attributes</col>
	</table>

	<table name="ISSQLScriptFile">
		<col key="yes" def="s72">ISSQLScriptFile</col>
		<col def="s72">Component_</col>
		<col def="i2">Scheduling</col>
		<col def="L255">InstallText</col>
		<col def="L255">UninstallText</col>
		<col def="S0">ISBuildSourcePath</col>
		<col def="S0">Comments</col>
		<col def="i2">ErrorHandling</col>
		<col def="i2">Attributes</col>
		<col def="S15">Version</col>
		<col def="S255">Condition</col>
	</table>

	<table name="ISSQLScriptImport">
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="S255">Server</col>
		<col def="S255">Database</col>
		<col def="S255">UserName</col>
		<col def="S255">Password</col>
		<col def="i4">Authentication</col>
		<col def="S0">IncludeTables</col>
		<col def="S0">ExcludeTables</col>
		<col def="i4">Attributes</col>
	</table>

	<table name="ISSQLScriptReplace">
		<col key="yes" def="s72">ISSQLScriptReplace</col>
		<col key="yes" def="s72">ISSQLScriptFile_</col>
		<col def="S0">Search</col>
		<col def="S0">Replace</col>
		<col def="i4">Attributes</col>
	</table>

	<table name="ISScriptFile">
		<col key="yes" def="s255">ISScriptFile</col>
	</table>

	<table name="ISSelfReg">
		<col key="yes" def="s72">FileKey</col>
		<col def="I2">Cost</col>
		<col def="I2">Order</col>
		<col def="S50">CmdLine</col>
	</table>

	<table name="ISSetupFile">
		<col key="yes" def="s72">ISSetupFile</col>
		<col def="S255">FileName</col>
		<col def="V0">Stream</col>
		<col def="S50">Language</col>
		<col def="I2">Splash</col>
		<col def="S0">Path</col>
	</table>

	<table name="ISSetupPrerequisites">
		<col key="yes" def="s72">ISSetupPrerequisites</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I2">Order</col>
		<col def="I2">ISSetupLocation</col>
		<col def="S255">ISReleaseFlags</col>
	</table>

	<table name="ISSetupType">
		<col key="yes" def="s38">ISSetupType</col>
		<col def="L255">Description</col>
		<col def="L255">Display_Name</col>
		<col def="i2">Display</col>
		<col def="S255">Comments</col>
		<row><td>Custom</td><td>##IDS__IsSetupTypeMinDlg_ChooseFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Custom##</td><td>3</td><td/></row>
		<row><td>Minimal</td><td>##IDS__IsSetupTypeMinDlg_MinimumFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Minimal##</td><td>2</td><td/></row>
		<row><td>Typical</td><td>##IDS__IsSetupTypeMinDlg_AllFeatures##</td><td>##IDS__IsSetupTypeMinDlg_Typical##</td><td>1</td><td/></row>
	</table>

	<table name="ISSetupTypeFeatures">
		<col key="yes" def="s38">ISSetupType_</col>
		<col key="yes" def="s38">Feature_</col>
		<row><td>Custom</td><td>AlwaysInstall</td></row>
		<row><td>Minimal</td><td>AlwaysInstall</td></row>
		<row><td>Typical</td><td>AlwaysInstall</td></row>
	</table>

	<table name="ISStorages">
		<col key="yes" def="s72">Name</col>
		<col def="S0">ISBuildSourcePath</col>
	</table>

	<table name="ISString">
		<col key="yes" def="s255">ISString</col>
		<col key="yes" def="s50">ISLanguage_</col>
		<col def="S0">Value</col>
		<col def="I2">Encoded</col>
		<col def="S0">Comment</col>
		<col def="I4">TimeStamp</col>
		<row><td>COMPANY_NAME</td><td>1033</td><td>Ploobs</td><td>0</td><td/><td>-375340767</td></row>
		<row><td>DN_AlwaysInstall</td><td>1033</td><td>Always Install</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_COLOR</td><td>1033</td><td>The color settings of your system are not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_OS</td><td>1033</td><td>The operating system is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_PROCESSOR</td><td>1033</td><td>The processor is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_RAM</td><td>1033</td><td>The amount of RAM is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_SCREEN</td><td>1033</td><td>The screen resolution is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_EXPRESS_LAUNCH_CONDITION_SOFTWARE_WOW6432NODE_XNA_FRA</td><td>1033</td><td>XNA 4.0 SDK Not Found. Download and Install it from here: http://www.microsoft.com/download/en/details.aspx?id=23714</td><td>0</td><td/><td>371132566</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPACT</td><td>1033</td><td>Compact</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPACT_DESC</td><td>1033</td><td>Compact Description</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPLETE</td><td>1033</td><td>Complete</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_COMPLETE_DESC</td><td>1033</td><td>Complete</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM</td><td>1033</td><td>Custom</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM_DESC</td><td>1033</td><td>Custom Description</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_CUSTOM_DESC_PRO</td><td>1033</td><td>Custom</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_TYPICAL</td><td>1033</td><td>Typical</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDPROP_SETUPTYPE_TYPICAL_DESC</td><td>1033</td><td>Typical Description</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_1</td><td>1033</td><td>[1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_1b</td><td>1033</td><td>[1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_1c</td><td>1033</td><td>[1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_1d</td><td>1033</td><td>[1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Advertising</td><td>1033</td><td>Advertising application</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_AllocatingRegistry</td><td>1033</td><td>Allocating registry space</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_AppCommandLine</td><td>1033</td><td>Application: [1], Command line: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_AppId</td><td>1033</td><td>AppId: [1]{{, AppType: [2]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_AppIdAppTypeRSN</td><td>1033</td><td>AppId: [1]{{, AppType: [2], Users: [3], RSN: [4]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Application</td><td>1033</td><td>Application: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_BindingExes</td><td>1033</td><td>Binding executables</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ClassId</td><td>1033</td><td>Class ID: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ClsID</td><td>1033</td><td>Class ID: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ComponentIDQualifier</td><td>1033</td><td>Component ID: [1], Qualifier: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ComponentIdQualifier2</td><td>1033</td><td>Component ID: [1], Qualifier: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace</td><td>1033</td><td>Computing space requirements</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace2</td><td>1033</td><td>Computing space requirements</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ComputingSpace3</td><td>1033</td><td>Computing space requirements</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ContentTypeExtension</td><td>1033</td><td>MIME Content Type: [1], Extension: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ContentTypeExtension2</td><td>1033</td><td>MIME Content Type: [1], Extension: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_CopyingNetworkFiles</td><td>1033</td><td>Copying files to the network</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_CopyingNewFiles</td><td>1033</td><td>Copying new files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingDuplicate</td><td>1033</td><td>Creating duplicate files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingFolders</td><td>1033</td><td>Creating folders</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingIISRoots</td><td>1033</td><td>Creating IIS Virtual Roots...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_CreatingShortcuts</td><td>1033</td><td>Creating shortcuts</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_DeletingServices</td><td>1033</td><td>Deleting services</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_EnvironmentStrings</td><td>1033</td><td>Updating environment strings</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_EvaluateLaunchConditions</td><td>1033</td><td>Evaluating launch conditions</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Extension</td><td>1033</td><td>Extension: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Extension2</td><td>1033</td><td>Extension: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Feature</td><td>1033</td><td>Feature: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FeatureColon</td><td>1033</td><td>Feature: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_File</td><td>1033</td><td>File: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_File2</td><td>1033</td><td>File: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDependencies</td><td>1033</td><td>File: [1],  Dependencies: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir</td><td>1033</td><td>File: [1], Directory: [9]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir2</td><td>1033</td><td>File: [1], Directory: [9]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDir3</td><td>1033</td><td>File: [1], Directory: [9]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize</td><td>1033</td><td>File: [1], Directory: [9], Size: [6]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize2</td><td>1033</td><td>File: [1],  Directory: [9],  Size: [6]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize3</td><td>1033</td><td>File: [1],  Directory: [9],  Size: [6]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirSize4</td><td>1033</td><td>File: [1],  Directory: [2],  Size: [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileDirectorySize</td><td>1033</td><td>File: [1],  Directory: [9],  Size: [6]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileFolder</td><td>1033</td><td>File: [1], Folder: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileFolder2</td><td>1033</td><td>File: [1], Folder: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileSectionKeyValue</td><td>1033</td><td>File: [1],  Section: [2],  Key: [3], Value: [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FileSectionKeyValue2</td><td>1033</td><td>File: [1],  Section: [2],  Key: [3], Value: [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Folder</td><td>1033</td><td>Folder: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Folder1</td><td>1033</td><td>Folder: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Font</td><td>1033</td><td>Font: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Font2</td><td>1033</td><td>Font: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FoundApp</td><td>1033</td><td>Found application: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_FreeSpace</td><td>1033</td><td>Free space: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_GeneratingScript</td><td>1033</td><td>Generating script operations for action:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ISLockPermissionsCost</td><td>1033</td><td>Gathering permissions information for objects...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ISLockPermissionsInstall</td><td>1033</td><td>Applying permissions information for objects...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_InitializeODBCDirs</td><td>1033</td><td>Initializing ODBC directories</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_InstallODBC</td><td>1033</td><td>Installing ODBC components</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_InstallServices</td><td>1033</td><td>Installing new services</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_InstallingSystemCatalog</td><td>1033</td><td>Installing system catalog</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_KeyName</td><td>1033</td><td>Key: [1], Name: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_KeyNameValue</td><td>1033</td><td>Key: [1], Name: [2], Value: [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_LibId</td><td>1033</td><td>LibID: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Libid2</td><td>1033</td><td>LibID: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_MigratingFeatureStates</td><td>1033</td><td>Migrating feature states from related applications</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_MovingFiles</td><td>1033</td><td>Moving files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_NameValueAction</td><td>1033</td><td>Name: [1], Value: [2], Action [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_NameValueAction2</td><td>1033</td><td>Name: [1], Value: [2], Action [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_PatchingFiles</td><td>1033</td><td>Patching files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ProgID</td><td>1033</td><td>ProgID: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_ProgID2</td><td>1033</td><td>ProgID: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_PropertySignature</td><td>1033</td><td>Property: [1], Signature: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_PublishProductFeatures</td><td>1033</td><td>Publishing product features</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_PublishProductInfo</td><td>1033</td><td>Publishing product information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_PublishingQualifiedComponents</td><td>1033</td><td>Publishing qualified components</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegUser</td><td>1033</td><td>Registering user</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterClassServer</td><td>1033</td><td>Registering class servers</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterExtensionServers</td><td>1033</td><td>Registering extension servers</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterFonts</td><td>1033</td><td>Registering fonts</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterMimeInfo</td><td>1033</td><td>Registering MIME info</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisterTypeLibs</td><td>1033</td><td>Registering type libraries</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringComPlus</td><td>1033</td><td>Registering COM+ Applications and Components</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringModules</td><td>1033</td><td>Registering modules</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringProduct</td><td>1033</td><td>Registering product</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RegisteringProgIdentifiers</td><td>1033</td><td>Registering program identifiers</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemoveApps</td><td>1033</td><td>Removing applications</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingBackup</td><td>1033</td><td>Removing backup files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingDuplicates</td><td>1033</td><td>Removing duplicated files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingFiles</td><td>1033</td><td>Removing files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingFolders</td><td>1033</td><td>Removing folders</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingIISRoots</td><td>1033</td><td>Removing IIS Virtual Roots...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingIni</td><td>1033</td><td>Removing INI file entries</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingMoved</td><td>1033</td><td>Removing moved files</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingODBC</td><td>1033</td><td>Removing ODBC components</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingRegistry</td><td>1033</td><td>Removing system registry values</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RemovingShortcuts</td><td>1033</td><td>Removing shortcuts</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_RollingBack</td><td>1033</td><td>Rolling back action:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_SearchForRelated</td><td>1033</td><td>Searching for related applications</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_SearchInstalled</td><td>1033</td><td>Searching for installed applications</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_SearchingQualifyingProducts</td><td>1033</td><td>Searching for qualifying products</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_SearchingQualifyingProducts2</td><td>1033</td><td>Searching for qualifying products</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Service</td><td>1033</td><td>Service: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Service2</td><td>1033</td><td>Service: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Service3</td><td>1033</td><td>Service: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Service4</td><td>1033</td><td>Service: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Shortcut</td><td>1033</td><td>Shortcut: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Shortcut1</td><td>1033</td><td>Shortcut: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_StartingServices</td><td>1033</td><td>Starting services</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_StoppingServices</td><td>1033</td><td>Stopping services</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishProductFeatures</td><td>1033</td><td>Unpublishing product features</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishQualified</td><td>1033</td><td>Unpublishing Qualified Components</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnpublishingProductInfo</td><td>1033</td><td>Unpublishing product information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregTypeLibs</td><td>1033</td><td>Unregistering type libraries</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterClassServers</td><td>1033</td><td>Unregister class servers</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterExtensionServers</td><td>1033</td><td>Unregistering extension servers</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisterModules</td><td>1033</td><td>Unregistering modules</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringComPlus</td><td>1033</td><td>Unregistering COM+ Applications and Components</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringFonts</td><td>1033</td><td>Unregistering fonts</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringMimeInfo</td><td>1033</td><td>Unregistering MIME info</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UnregisteringProgramIds</td><td>1033</td><td>Unregistering program identifiers</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UpdateComponentRegistration</td><td>1033</td><td>Updating component registration</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_UpdateEnvironmentStrings</td><td>1033</td><td>Updating environment strings</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_Validating</td><td>1033</td><td>Validating install</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_WritingINI</td><td>1033</td><td>Writing INI file values</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ACTIONTEXT_WritingRegistry</td><td>1033</td><td>Writing system registry values</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_BACK</td><td>1033</td><td>&lt; &amp;Back</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_CANCEL</td><td>1033</td><td>Cancel</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_CANCEL2</td><td>1033</td><td>&amp;Cancel</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_CHANGE</td><td>1033</td><td>&amp;Change...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_COST</td><td>1033</td><td>Costing COM+ application: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_INSTALL</td><td>1033</td><td>Installing COM+ application: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_COMPLUS_PROGRESSTEXT_UNINSTALL</td><td>1033</td><td>Uninstalling COM+ application: [1]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DIALOG_TEXT2_DESCRIPTION</td><td>1033</td><td>Dialog Normal Description</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DIALOG_TEXT_DESCRIPTION_EXTERIOR</td><td>1033</td><td>{&amp;TahomaBold10}Dialog Bold Title</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DIALOG_TEXT_DESCRIPTION_INTERIOR</td><td>1033</td><td>{&amp;MSSansBold8}Dialog Bold Title</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DIFX_AMD64</td><td>1033</td><td>[ProductName] requires an X64 processor. Click OK to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DIFX_IA64</td><td>1033</td><td>[ProductName] requires an IA64 processor. Click OK to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DIFX_X86</td><td>1033</td><td>[ProductName] requires an X86 processor. Click OK to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_DatabaseFolder_InstallDatabaseTo</td><td>1033</td><td>Install [ProductName] database to:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_0</td><td>1033</td><td>{{Fatal error: }}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1</td><td>1033</td><td>Error [1]. </td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_10</td><td>1033</td><td>=== Logging started: [Date]  [Time] ===</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_100</td><td>1033</td><td>Could not remove shortcut [2]. Verify that the shortcut file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_101</td><td>1033</td><td>Could not register type library for file [2].  Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_102</td><td>1033</td><td>Could not unregister type library for file [2].  Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_103</td><td>1033</td><td>Could not update the INI file [2][3].  Verify that the file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_104</td><td>1033</td><td>Could not schedule file [2] to replace file [3] on reboot.  Verify that you have write permissions to file [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_105</td><td>1033</td><td>Error removing ODBC driver manager, ODBC error [2]: [3]. Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_106</td><td>1033</td><td>Error installing ODBC driver manager, ODBC error [2]: [3]. Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_107</td><td>1033</td><td>Error removing ODBC driver [4], ODBC error [2]: [3]. Verify that you have sufficient privileges to remove ODBC drivers.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_108</td><td>1033</td><td>Error installing ODBC driver [4], ODBC error [2]: [3]. Verify that the file [4] exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_109</td><td>1033</td><td>Error configuring ODBC data source [4], ODBC error [2]: [3]. Verify that the file [4] exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_11</td><td>1033</td><td>=== Logging stopped: [Date]  [Time] ===</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_110</td><td>1033</td><td>Service [2] ([3]) failed to start.  Verify that you have sufficient privileges to start system services.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_111</td><td>1033</td><td>Service [2] ([3]) could not be stopped.  Verify that you have sufficient privileges to stop system services.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_112</td><td>1033</td><td>Service [2] ([3]) could not be deleted.  Verify that you have sufficient privileges to remove system services.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_113</td><td>1033</td><td>Service [2] ([3]) could not be installed.  Verify that you have sufficient privileges to install system services.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_114</td><td>1033</td><td>Could not update environment variable [2].  Verify that you have sufficient privileges to modify environment variables.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_115</td><td>1033</td><td>You do not have sufficient privileges to complete this installation for all users of the machine.  Log on as an administrator and then retry this installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_116</td><td>1033</td><td>Could not set file security for file [3]. Error: [2].  Verify that you have sufficient privileges to modify the security permissions for this file.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_117</td><td>1033</td><td>Component Services (COM+ 1.0) are not installed on this computer.  This installation requires Component Services in order to complete successfully.  Component Services are available on Windows 2000.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_118</td><td>1033</td><td>Error registering COM+ application.  Contact your support personnel for more information.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_119</td><td>1033</td><td>Error unregistering COM+ application.  Contact your support personnel for more information.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_12</td><td>1033</td><td>Action start [Time]: [1].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_120</td><td>1033</td><td>Removing older versions of this application</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_121</td><td>1033</td><td>Preparing to remove older versions of this application</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_122</td><td>1033</td><td>Error applying patch to file [2].  It has probably been updated by other means, and can no longer be modified by this patch.  For more information contact your patch vendor.  {{System Error: [3]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_123</td><td>1033</td><td>[2] cannot install one of its required products. Contact your technical support group.  {{System Error: [3].}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_124</td><td>1033</td><td>The older version of [2] cannot be removed.  Contact your technical support group.  {{System Error [3].}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_125</td><td>1033</td><td>The description for service '[2]' ([3]) could not be changed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_126</td><td>1033</td><td>The Windows Installer service cannot update the system file [2] because the file is protected by Windows.  You may need to update your operating system for this program to work correctly. {{Package version: [3], OS Protected version: [4]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_127</td><td>1033</td><td>The Windows Installer service cannot update the protected Windows file [2]. {{Package version: [3], OS Protected version: [4], SFP Error: [5]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_128</td><td>1033</td><td>The Windows Installer service cannot update one or more protected Windows files. SFP Error: [2]. List of protected files: [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_129</td><td>1033</td><td>User installations are disabled via policy on the machine.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_13</td><td>1033</td><td>Action ended [Time]: [1]. Return value [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_130</td><td>1033</td><td>This setup requires Internet Information Server 4.0 or higher for configuring IIS Virtual Roots. Please make sure that you have IIS 4.0 or higher.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_131</td><td>1033</td><td>This setup requires Administrator privileges for configuring IIS Virtual Roots.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1329</td><td>1033</td><td>A file that is required cannot be installed because the cabinet file [2] is not digitally signed. This may indicate that the cabinet file is corrupt.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1330</td><td>1033</td><td>A file that is required cannot be installed because the cabinet file [2] has an invalid digital signature. This may indicate that the cabinet file is corrupt.{ Error [3] was returned by WinVerifyTrust.}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1331</td><td>1033</td><td>Failed to correctly copy [2] file: CRC error.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1332</td><td>1033</td><td>Failed to correctly patch [2] file: CRC error.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1333</td><td>1033</td><td>Failed to correctly patch [2] file: CRC error.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1334</td><td>1033</td><td>The file '[2]' cannot be installed because the file cannot be found in cabinet file '[3]'. This could indicate a network error, an error reading from the CD-ROM, or a problem with this package.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1335</td><td>1033</td><td>The cabinet file '[2]' required for this installation is corrupt and cannot be used. This could indicate a network error, an error reading from the CD-ROM, or a problem with this package.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1336</td><td>1033</td><td>There was an error creating a temporary file that is needed to complete this installation. Folder: [3]. System error code: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_14</td><td>1033</td><td>Time remaining: {[1] minutes }{[2] seconds}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_15</td><td>1033</td><td>Out of memory. Shut down other applications before retrying.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_16</td><td>1033</td><td>Installer is no longer responding.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1609</td><td>1033</td><td>An error occurred while applying security settings. [2] is not a valid user or group. This could be a problem with the package, or a problem connecting to a domain controller on the network. Check your network connection and click Retry, or Cancel to end the install. Unable to locate the user's SID, system error [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1651</td><td>1033</td><td>Admin user failed to apply patch for a per-user managed or a per-machine application which is in advertise state.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_17</td><td>1033</td><td>Installer terminated prematurely.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1715</td><td>1033</td><td>Installed [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1716</td><td>1033</td><td>Configured [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1717</td><td>1033</td><td>Removed [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1718</td><td>1033</td><td>File [2] was rejected by digital signature policy.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1719</td><td>1033</td><td>Windows Installer service could not be accessed. Contact your support personnel to verify that it is properly registered and enabled.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1720</td><td>1033</td><td>There is a problem with this Windows Installer package. A script required for this install to complete could not be run. Contact your support personnel or package vendor. Custom action [2] script error [3], [4]: [5] Line [6], Column [7], [8]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1721</td><td>1033</td><td>There is a problem with this Windows Installer package. A program required for this install to complete could not be run. Contact your support personnel or package vendor. Action: [2], location: [3], command: [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1722</td><td>1033</td><td>There is a problem with this Windows Installer package. A program run as part of the setup did not finish as expected. Contact your support personnel or package vendor. Action [2], location: [3], command: [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1723</td><td>1033</td><td>There is a problem with this Windows Installer package. A DLL required for this install to complete could not be run. Contact your support personnel or package vendor. Action [2], entry: [3], library: [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1724</td><td>1033</td><td>Removal completed successfully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1725</td><td>1033</td><td>Removal failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1726</td><td>1033</td><td>Advertisement completed successfully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1727</td><td>1033</td><td>Advertisement failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1728</td><td>1033</td><td>Configuration completed successfully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1729</td><td>1033</td><td>Configuration failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1730</td><td>1033</td><td>You must be an Administrator to remove this application. To remove this application, you can log on as an administrator, or contact your technical support group for assistance.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1731</td><td>1033</td><td>The source installation package for the product [2] is out of sync with the client package. Try the installation again using a valid copy of the installation package '[3]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1732</td><td>1033</td><td>In order to complete the installation of [2], you must restart the computer. Other users are currently logged on to this computer, and restarting may cause them to lose their work. Do you want to restart now?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_18</td><td>1033</td><td>Please wait while Windows configures [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_19</td><td>1033</td><td>Gathering required information...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1935</td><td>1033</td><td>An error occurred during the installation of assembly component [2]. HRESULT: [3]. {{assembly interface: [4], function: [5], assembly name: [6]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1936</td><td>1033</td><td>An error occurred during the installation of assembly '[6]'. The assembly is not strongly named or is not signed with the minimal key length. HRESULT: [3]. {{assembly interface: [4], function: [5], component: [2]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1937</td><td>1033</td><td>An error occurred during the installation of assembly '[6]'. The signature or catalog could not be verified or is not valid. HRESULT: [3]. {{assembly interface: [4], function: [5], component: [2]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_1938</td><td>1033</td><td>An error occurred during the installation of assembly '[6]'. One or more modules of the assembly could not be found. HRESULT: [3]. {{assembly interface: [4], function: [5], component: [2]}}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2</td><td>1033</td><td>Warning [1]. </td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_20</td><td>1033</td><td>{[ProductName] }Setup completed successfully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_21</td><td>1033</td><td>{[ProductName] }Setup failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2101</td><td>1033</td><td>Shortcuts not supported by the operating system.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2102</td><td>1033</td><td>Invalid .ini action: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2103</td><td>1033</td><td>Could not resolve path for shell folder [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2104</td><td>1033</td><td>Writing .ini file: [3]: System error: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2105</td><td>1033</td><td>Shortcut Creation [3] Failed. System error: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2106</td><td>1033</td><td>Shortcut Deletion [3] Failed. System error: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2107</td><td>1033</td><td>Error [3] registering type library [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2108</td><td>1033</td><td>Error [3] unregistering type library [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2109</td><td>1033</td><td>Section missing for .ini action.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2110</td><td>1033</td><td>Key missing for .ini action.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2111</td><td>1033</td><td>Detection of running applications failed, could not get performance data. Registered operation returned : [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2112</td><td>1033</td><td>Detection of running applications failed, could not get performance index. Registered operation returned : [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2113</td><td>1033</td><td>Detection of running applications failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_22</td><td>1033</td><td>Error reading from file: [2]. {{ System error [3].}}  Verify that the file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2200</td><td>1033</td><td>Database: [2]. Database object creation failed, mode = [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2201</td><td>1033</td><td>Database: [2]. Initialization failed, out of memory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2202</td><td>1033</td><td>Database: [2]. Data access failed, out of memory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2203</td><td>1033</td><td>Database: [2]. Cannot open database file. System error [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2204</td><td>1033</td><td>Database: [2]. Table already exists: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2205</td><td>1033</td><td>Database: [2]. Table does not exist: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2206</td><td>1033</td><td>Database: [2]. Table could not be dropped: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2207</td><td>1033</td><td>Database: [2]. Intent violation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2208</td><td>1033</td><td>Database: [2]. Insufficient parameters for Execute.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2209</td><td>1033</td><td>Database: [2]. Cursor in invalid state.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2210</td><td>1033</td><td>Database: [2]. Invalid update data type in column [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2211</td><td>1033</td><td>Database: [2]. Could not create database table [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2212</td><td>1033</td><td>Database: [2]. Database not in writable state.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2213</td><td>1033</td><td>Database: [2]. Error saving database tables.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2214</td><td>1033</td><td>Database: [2]. Error writing export file: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2215</td><td>1033</td><td>Database: [2]. Cannot open import file: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2216</td><td>1033</td><td>Database: [2]. Import file format error: [3], Line [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2217</td><td>1033</td><td>Database: [2]. Wrong state to CreateOutputDatabase [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2218</td><td>1033</td><td>Database: [2]. Table name not supplied.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2219</td><td>1033</td><td>Database: [2]. Invalid Installer database format.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2220</td><td>1033</td><td>Database: [2]. Invalid row/field data.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2221</td><td>1033</td><td>Database: [2]. Code page conflict in import file: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2222</td><td>1033</td><td>Database: [2]. Transform or merge code page [3] differs from database code page [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2223</td><td>1033</td><td>Database: [2]. Databases are the same. No transform generated.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2224</td><td>1033</td><td>Database: [2]. GenerateTransform: Database corrupt. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2225</td><td>1033</td><td>Database: [2]. Transform: Cannot transform a temporary table. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2226</td><td>1033</td><td>Database: [2]. Transform failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2227</td><td>1033</td><td>Database: [2]. Invalid identifier '[3]' in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2228</td><td>1033</td><td>Database: [2]. Unknown table '[3]' in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2229</td><td>1033</td><td>Database: [2]. Could not load table '[3]' in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2230</td><td>1033</td><td>Database: [2]. Repeated table '[3]' in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2231</td><td>1033</td><td>Database: [2]. Missing ')' in SQL query: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2232</td><td>1033</td><td>Database: [2]. Unexpected token '[3]' in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2233</td><td>1033</td><td>Database: [2]. No columns in SELECT clause in SQL query: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2234</td><td>1033</td><td>Database: [2]. No columns in ORDER BY clause in SQL query: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2235</td><td>1033</td><td>Database: [2]. Column '[3]' not present or ambiguous in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2236</td><td>1033</td><td>Database: [2]. Invalid operator '[3]' in SQL query: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2237</td><td>1033</td><td>Database: [2]. Invalid or missing query string: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2238</td><td>1033</td><td>Database: [2]. Missing FROM clause in SQL query: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2239</td><td>1033</td><td>Database: [2]. Insufficient values in INSERT SQL statement.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2240</td><td>1033</td><td>Database: [2]. Missing update columns in UPDATE SQL statement.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2241</td><td>1033</td><td>Database: [2]. Missing insert columns in INSERT SQL statement.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2242</td><td>1033</td><td>Database: [2]. Column '[3]' repeated.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2243</td><td>1033</td><td>Database: [2]. No primary columns defined for table creation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2244</td><td>1033</td><td>Database: [2]. Invalid type specifier '[3]' in SQL query [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2245</td><td>1033</td><td>IStorage::Stat failed with error [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2246</td><td>1033</td><td>Database: [2]. Invalid Installer transform format.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2247</td><td>1033</td><td>Database: [2] Transform stream read/write failure.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2248</td><td>1033</td><td>Database: [2] GenerateTransform/Merge: Column type in base table does not match reference table. Table: [3] Col #: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2249</td><td>1033</td><td>Database: [2] GenerateTransform: More columns in base table than in reference table. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2250</td><td>1033</td><td>Database: [2] Transform: Cannot add existing row. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2251</td><td>1033</td><td>Database: [2] Transform: Cannot delete row that does not exist. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2252</td><td>1033</td><td>Database: [2] Transform: Cannot add existing table. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2253</td><td>1033</td><td>Database: [2] Transform: Cannot delete table that does not exist. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2254</td><td>1033</td><td>Database: [2] Transform: Cannot update row that does not exist. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2255</td><td>1033</td><td>Database: [2] Transform: Column with this name already exists. Table: [3] Col: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2256</td><td>1033</td><td>Database: [2] GenerateTransform/Merge: Number of primary keys in base table does not match reference table. Table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2257</td><td>1033</td><td>Database: [2]. Intent to modify read only table: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2258</td><td>1033</td><td>Database: [2]. Type mismatch in parameter: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2259</td><td>1033</td><td>Database: [2] Table(s) Update failed</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2260</td><td>1033</td><td>Storage CopyTo failed. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2261</td><td>1033</td><td>Could not remove stream [2]. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2262</td><td>1033</td><td>Stream does not exist: [2]. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2263</td><td>1033</td><td>Could not open stream [2]. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2264</td><td>1033</td><td>Could not remove stream [2]. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2265</td><td>1033</td><td>Could not commit storage. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2266</td><td>1033</td><td>Could not rollback storage. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2267</td><td>1033</td><td>Could not delete storage [2]. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2268</td><td>1033</td><td>Database: [2]. Merge: There were merge conflicts reported in [3] tables.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2269</td><td>1033</td><td>Database: [2]. Merge: The column count differed in the '[3]' table of the two databases.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2270</td><td>1033</td><td>Database: [2]. GenerateTransform/Merge: Column name in base table does not match reference table. Table: [3] Col #: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2271</td><td>1033</td><td>SummaryInformation write for transform failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2272</td><td>1033</td><td>Database: [2]. MergeDatabase will not write any changes because the database is open read-only.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2273</td><td>1033</td><td>Database: [2]. MergeDatabase: A reference to the base database was passed as the reference database.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2274</td><td>1033</td><td>Database: [2]. MergeDatabase: Unable to write errors to Error table. Could be due to a non-nullable column in a predefined Error table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2275</td><td>1033</td><td>Database: [2]. Specified Modify [3] operation invalid for table joins.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2276</td><td>1033</td><td>Database: [2]. Code page [3] not supported by the system.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2277</td><td>1033</td><td>Database: [2]. Failed to save table [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2278</td><td>1033</td><td>Database: [2]. Exceeded number of expressions limit of 32 in WHERE clause of SQL query: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2279</td><td>1033</td><td>Database: [2] Transform: Too many columns in base table [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2280</td><td>1033</td><td>Database: [2]. Could not create column [3] for table [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2281</td><td>1033</td><td>Could not rename stream [2]. System error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2282</td><td>1033</td><td>Stream name invalid [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_23</td><td>1033</td><td>Cannot create the file [3].  A directory with this name already exists.  Cancel the installation and try installing to a different location.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2302</td><td>1033</td><td>Patch notify: [2] bytes patched to far.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2303</td><td>1033</td><td>Error getting volume info. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2304</td><td>1033</td><td>Error getting disk free space. GetLastError: [2]. Volume: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2305</td><td>1033</td><td>Error waiting for patch thread. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2306</td><td>1033</td><td>Could not create thread for patch application. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2307</td><td>1033</td><td>Source file key name is null.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2308</td><td>1033</td><td>Destination file name is null.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2309</td><td>1033</td><td>Attempting to patch file [2] when patch already in progress.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2310</td><td>1033</td><td>Attempting to continue patch when no patch is in progress.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2315</td><td>1033</td><td>Missing path separator: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2318</td><td>1033</td><td>File does not exist: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2319</td><td>1033</td><td>Error setting file attribute: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2320</td><td>1033</td><td>File not writable: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2321</td><td>1033</td><td>Error creating file: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2322</td><td>1033</td><td>User canceled.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2323</td><td>1033</td><td>Invalid file attribute.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2324</td><td>1033</td><td>Could not open file: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2325</td><td>1033</td><td>Could not get file time for file: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2326</td><td>1033</td><td>Error in FileToDosDateTime.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2327</td><td>1033</td><td>Could not remove directory: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2328</td><td>1033</td><td>Error getting file version info for file: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2329</td><td>1033</td><td>Error deleting file: [3]. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2330</td><td>1033</td><td>Error getting file attributes: [3]. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2331</td><td>1033</td><td>Error loading library [2] or finding entry point [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2332</td><td>1033</td><td>Error getting file attributes. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2333</td><td>1033</td><td>Error setting file attributes. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2334</td><td>1033</td><td>Error converting file time to local time for file: [3]. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2335</td><td>1033</td><td>Path: [2] is not a parent of [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2336</td><td>1033</td><td>Error creating temp file on path: [3]. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2337</td><td>1033</td><td>Could not close file: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2338</td><td>1033</td><td>Could not update resource for file: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2339</td><td>1033</td><td>Could not set file time for file: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2340</td><td>1033</td><td>Could not update resource for file: [3], Missing resource.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2341</td><td>1033</td><td>Could not update resource for file: [3], Resource too large.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2342</td><td>1033</td><td>Could not update resource for file: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2343</td><td>1033</td><td>Specified path is empty.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2344</td><td>1033</td><td>Could not find required file IMAGEHLP.DLL to validate file:[2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2345</td><td>1033</td><td>[2]: File does not contain a valid checksum value.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2347</td><td>1033</td><td>User ignore.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2348</td><td>1033</td><td>Error attempting to read from cabinet stream.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2349</td><td>1033</td><td>Copy resumed with different info.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2350</td><td>1033</td><td>FDI server error</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2351</td><td>1033</td><td>File key '[2]' not found in cabinet '[3]'. The installation cannot continue.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2352</td><td>1033</td><td>Could not initialize cabinet file server. The required file 'CABINET.DLL' may be missing.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2353</td><td>1033</td><td>Not a cabinet.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2354</td><td>1033</td><td>Cannot handle cabinet.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2355</td><td>1033</td><td>Corrupt cabinet.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2356</td><td>1033</td><td>Could not locate cabinet in stream: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2357</td><td>1033</td><td>Cannot set attributes.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2358</td><td>1033</td><td>Error determining whether file is in-use: [3]. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2359</td><td>1033</td><td>Unable to create the target file - file may be in use.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2360</td><td>1033</td><td>Progress tick.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2361</td><td>1033</td><td>Need next cabinet.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2362</td><td>1033</td><td>Folder not found: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2363</td><td>1033</td><td>Could not enumerate subfolders for folder: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2364</td><td>1033</td><td>Bad enumeration constant in CreateCopier call.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2365</td><td>1033</td><td>Could not BindImage exe file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2366</td><td>1033</td><td>User failure.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2367</td><td>1033</td><td>User abort.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2368</td><td>1033</td><td>Failed to get network resource information. Error [2], network path [3]. Extended error: network provider [5], error code [4], error description [6].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2370</td><td>1033</td><td>Invalid CRC checksum value for [2] file.{ Its header says [3] for checksum, its computed value is [4].}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2371</td><td>1033</td><td>Could not apply patch to file [2]. GetLastError: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2372</td><td>1033</td><td>Patch file [2] is corrupt or of an invalid format. Attempting to patch file [3]. GetLastError: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2373</td><td>1033</td><td>File [2] is not a valid patch file.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2374</td><td>1033</td><td>File [2] is not a valid destination file for patch file [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2375</td><td>1033</td><td>Unknown patching error: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2376</td><td>1033</td><td>Cabinet not found.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2379</td><td>1033</td><td>Error opening file for read: [3] GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2380</td><td>1033</td><td>Error opening file for write: [3]. GetLastError: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2381</td><td>1033</td><td>Directory does not exist: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2382</td><td>1033</td><td>Drive not ready: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_24</td><td>1033</td><td>Please insert the disk: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2401</td><td>1033</td><td>64-bit registry operation attempted on 32-bit operating system for key [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2402</td><td>1033</td><td>Out of memory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_25</td><td>1033</td><td>The installer has insufficient privileges to access this directory: [2].  The installation cannot continue.  Log on as an administrator or contact your system administrator.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2501</td><td>1033</td><td>Could not create rollback script enumerator.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2502</td><td>1033</td><td>Called InstallFinalize when no install in progress.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2503</td><td>1033</td><td>Called RunScript when not marked in progress.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_26</td><td>1033</td><td>Error writing to file [2].  Verify that you have access to that directory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2601</td><td>1033</td><td>Invalid value for property [2]: '[3]'</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2602</td><td>1033</td><td>The [2] table entry '[3]' has no associated entry in the Media table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2603</td><td>1033</td><td>Duplicate table name [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2604</td><td>1033</td><td>[2] Property undefined.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2605</td><td>1033</td><td>Could not find server [2] in [3] or [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2606</td><td>1033</td><td>Value of property [2] is not a valid full path: '[3]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2607</td><td>1033</td><td>Media table not found or empty (required for installation of files).</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2608</td><td>1033</td><td>Could not create security descriptor for object. Error: '[2]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2609</td><td>1033</td><td>Attempt to migrate product settings before initialization.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2611</td><td>1033</td><td>The file [2] is marked as compressed, but the associated media entry does not specify a cabinet.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2612</td><td>1033</td><td>Stream not found in '[2]' column. Primary key: '[3]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2613</td><td>1033</td><td>RemoveExistingProducts action sequenced incorrectly.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2614</td><td>1033</td><td>Could not access IStorage object from installation package.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2615</td><td>1033</td><td>Skipped unregistration of Module [2] due to source resolution failure.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2616</td><td>1033</td><td>Companion file [2] parent missing.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2617</td><td>1033</td><td>Shared component [2] not found in Component table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2618</td><td>1033</td><td>Isolated application component [2] not found in Component table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2619</td><td>1033</td><td>Isolated components [2], [3] not part of same feature.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2620</td><td>1033</td><td>Key file of isolated application component [2] not in File table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2621</td><td>1033</td><td>Resource DLL or Resource ID information for shortcut [2] set incorrectly.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27</td><td>1033</td><td>Error reading from file [2].  Verify that the file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2701</td><td>1033</td><td>The depth of a feature exceeds the acceptable tree depth of [2] levels.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2702</td><td>1033</td><td>A Feature table record ([2]) references a non-existent parent in the Attributes field.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2703</td><td>1033</td><td>Property name for root source path not defined: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2704</td><td>1033</td><td>Root directory property undefined: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2705</td><td>1033</td><td>Invalid table: [2]; Could not be linked as tree.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2706</td><td>1033</td><td>Source paths not created. No path exists for entry [2] in Directory table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2707</td><td>1033</td><td>Target paths not created. No path exists for entry [2] in Directory table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2708</td><td>1033</td><td>No entries found in the file table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2709</td><td>1033</td><td>The specified Component name ('[2]') not found in Component table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2710</td><td>1033</td><td>The requested 'Select' state is illegal for this Component.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2711</td><td>1033</td><td>The specified Feature name ('[2]') not found in Feature table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2712</td><td>1033</td><td>Invalid return from modeless dialog: [3], in action [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2713</td><td>1033</td><td>Null value in a non-nullable column ('[2]' in '[3]' column of the '[4]' table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2714</td><td>1033</td><td>Invalid value for default folder name: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2715</td><td>1033</td><td>The specified File key ('[2]') not found in the File table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2716</td><td>1033</td><td>Could not create a random subcomponent name for component '[2]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2717</td><td>1033</td><td>Bad action condition or error calling custom action '[2]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2718</td><td>1033</td><td>Missing package name for product code '[2]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2719</td><td>1033</td><td>Neither UNC nor drive letter path found in source '[2]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2720</td><td>1033</td><td>Error opening source list key. Error: '[2]'</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2721</td><td>1033</td><td>Custom action [2] not found in Binary table stream.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2722</td><td>1033</td><td>Custom action [2] not found in File table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2723</td><td>1033</td><td>Custom action [2] specifies unsupported type.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2724</td><td>1033</td><td>The volume label '[2]' on the media you're running from does not match the label '[3]' given in the Media table. This is allowed only if you have only 1 entry in your Media table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2725</td><td>1033</td><td>Invalid database tables</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2726</td><td>1033</td><td>Action not found: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2727</td><td>1033</td><td>The directory entry '[2]' does not exist in the Directory table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2728</td><td>1033</td><td>Table definition error: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2729</td><td>1033</td><td>Install engine not initialized.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2730</td><td>1033</td><td>Bad value in database. Table: '[2]'; Primary key: '[3]'; Column: '[4]'</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2731</td><td>1033</td><td>Selection Manager not initialized.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2732</td><td>1033</td><td>Directory Manager not initialized.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2733</td><td>1033</td><td>Bad foreign key ('[2]') in '[3]' column of the '[4]' table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2734</td><td>1033</td><td>Invalid reinstall mode character.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2735</td><td>1033</td><td>Custom action '[2]' has caused an unhandled exception and has been stopped. This may be the result of an internal error in the custom action, such as an access violation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2736</td><td>1033</td><td>Generation of custom action temp file failed: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2737</td><td>1033</td><td>Could not access custom action [2], entry [3], library [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2738</td><td>1033</td><td>Could not access VBScript run time for custom action [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2739</td><td>1033</td><td>Could not access JavaScript run time for custom action [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2740</td><td>1033</td><td>Custom action [2] script error [3], [4]: [5] Line [6], Column [7], [8].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2741</td><td>1033</td><td>Configuration information for product [2] is corrupt. Invalid info: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2742</td><td>1033</td><td>Marshaling to Server failed: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2743</td><td>1033</td><td>Could not execute custom action [2], location: [3], command: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2744</td><td>1033</td><td>EXE failed called by custom action [2], location: [3], command: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2745</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected language [4], found language [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2746</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected product [4], found product [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2747</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected product version &lt; [4], found product version [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2748</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected product version &lt;= [4], found product version [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2749</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected product version == [4], found product version [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2750</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected product version &gt;= [4], found product version [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27502</td><td>1033</td><td>Could not connect to [2] '[3]'. [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27503</td><td>1033</td><td>Error retrieving version string from [2] '[3]'. [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27504</td><td>1033</td><td>SQL version requirements not met: [3]. This installation requires [2] [4] or later.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27505</td><td>1033</td><td>Could not open SQL script file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27506</td><td>1033</td><td>Error executing SQL script [2]. Line [3]. [4]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27507</td><td>1033</td><td>Connection or browsing for database servers requires that MDAC be installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27508</td><td>1033</td><td>Error installing COM+ application [2]. [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27509</td><td>1033</td><td>Error uninstalling COM+ application [2]. [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2751</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected product version &gt; [4], found product version [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27510</td><td>1033</td><td>Error installing COM+ application [2].  Could not load Microsoft(R) .NET class libraries. Registering .NET serviced components requires that Microsoft(R) .NET Framework be installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27511</td><td>1033</td><td>Could not execute SQL script file [2]. Connection not open: [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27512</td><td>1033</td><td>Error beginning transactions for [2] '[3]'. Database [4]. [5]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27513</td><td>1033</td><td>Error committing transactions for [2] '[3]'. Database [4]. [5]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27514</td><td>1033</td><td>This installation requires a Microsoft SQL Server. The specified server '[3]' is a Microsoft SQL Server Desktop Engine or SQL Server Express.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27515</td><td>1033</td><td>Error retrieving schema version from [2] '[3]'. Database: '[4]'. [5]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27516</td><td>1033</td><td>Error writing schema version to [2] '[3]'. Database: '[4]'. [5]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27517</td><td>1033</td><td>This installation requires Administrator privileges for installing COM+ applications. Log on as an administrator and then retry this installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27518</td><td>1033</td><td>The COM+ application "[2]" is configured to run as an NT service; this requires COM+ 1.5 or later on the system. Since your system has COM+ 1.0, this application will not be installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27519</td><td>1033</td><td>Error updating XML file [2]. [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2752</td><td>1033</td><td>Could not open transform [2] stored as child storage of package [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27520</td><td>1033</td><td>Error opening XML file [2]. [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27521</td><td>1033</td><td>This setup requires MSXML 3.0 or higher for configuring XML files. Please make sure that you have version 3.0 or higher.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27522</td><td>1033</td><td>Error creating XML file [2]. [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27523</td><td>1033</td><td>Error loading servers.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27524</td><td>1033</td><td>Error loading NetApi32.DLL. The ISNetApi.dll needs to have NetApi32.DLL properly loaded and requires an NT based operating system.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27525</td><td>1033</td><td>Server not found. Verify that the specified server exists. The server name can not be empty.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27526</td><td>1033</td><td>Unspecified error from ISNetApi.dll.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27527</td><td>1033</td><td>The buffer is too small.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27528</td><td>1033</td><td>Access denied. Check administrative rights.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27529</td><td>1033</td><td>Invalid computer.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2753</td><td>1033</td><td>The File '[2]' is not marked for installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27530</td><td>1033</td><td>Unknown error returned from NetAPI. System error: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27531</td><td>1033</td><td>Unhandled exception.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27532</td><td>1033</td><td>Invalid user name for this server or domain.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27533</td><td>1033</td><td>The case-sensitive passwords do not match.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27534</td><td>1033</td><td>The list is empty.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27535</td><td>1033</td><td>Access violation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27536</td><td>1033</td><td>Error getting group.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27537</td><td>1033</td><td>Error adding user to group. Verify that the group exists for this domain or server.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27538</td><td>1033</td><td>Error creating user.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27539</td><td>1033</td><td>ERROR_NETAPI_ERROR_NOT_PRIMARY returned from NetAPI.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2754</td><td>1033</td><td>The File '[2]' is not a valid patch file.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27540</td><td>1033</td><td>The specified user already exists.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27541</td><td>1033</td><td>The specified group already exists.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27542</td><td>1033</td><td>Invalid password. Verify that the password is in accordance with your network password policy.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27543</td><td>1033</td><td>Invalid name.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27544</td><td>1033</td><td>Invalid group.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27545</td><td>1033</td><td>The user name can not be empty and must be in the format DOMAIN\Username.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27546</td><td>1033</td><td>Error loading or creating INI file in the user TEMP directory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27547</td><td>1033</td><td>ISNetAPI.dll is not loaded or there was an error loading the dll. This dll needs to be loaded for this operation. Verify that the dll is in the SUPPORTDIR directory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27548</td><td>1033</td><td>Error deleting INI file containing new user information from the user's TEMP directory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27549</td><td>1033</td><td>Error getting the primary domain controller (PDC).</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2755</td><td>1033</td><td>Server returned unexpected error [2] attempting to install package [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27550</td><td>1033</td><td>Every field must have a value in order to create a user.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27551</td><td>1033</td><td>ODBC driver for [2] not found. This is required to connect to [2] database servers.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27552</td><td>1033</td><td>Error creating database [4]. Server: [2] [3]. [5]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27553</td><td>1033</td><td>Error connecting to database [4]. Server: [2] [3]. [5]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27554</td><td>1033</td><td>Error attempting to open connection [2]. No valid database metadata associated with this connection.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_27555</td><td>1033</td><td>Error attempting to apply permissions to object '[2]'. System error: [3] ([4])</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2756</td><td>1033</td><td>The property '[2]' was used as a directory property in one or more tables, but no value was ever assigned.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2757</td><td>1033</td><td>Could not create summary info for transform [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2758</td><td>1033</td><td>Transform [2] does not contain an MSI version.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2759</td><td>1033</td><td>Transform [2] version [3] incompatible with engine; Min: [4], Max: [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2760</td><td>1033</td><td>Transform [2] invalid for package [3]. Expected upgrade code [4], found [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2761</td><td>1033</td><td>Cannot begin transaction. Global mutex not properly initialized.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2762</td><td>1033</td><td>Cannot write script record. Transaction not started.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2763</td><td>1033</td><td>Cannot run script. Transaction not started.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2765</td><td>1033</td><td>Assembly name missing from AssemblyName table : Component: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2766</td><td>1033</td><td>The file [2] is an invalid MSI storage file.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2767</td><td>1033</td><td>No more data{ while enumerating [2]}.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2768</td><td>1033</td><td>Transform in patch package is invalid.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2769</td><td>1033</td><td>Custom Action [2] did not close [3] MSIHANDLEs.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2770</td><td>1033</td><td>Cached folder [2] not defined in internal cache folder table.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2771</td><td>1033</td><td>Upgrade of feature [2] has a missing component.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2772</td><td>1033</td><td>New upgrade feature [2] must be a leaf feature.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_28</td><td>1033</td><td>Another application has exclusive access to the file [2].  Please shut down all other applications, then click Retry.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2801</td><td>1033</td><td>Unknown Message -- Type [2]. No action is taken.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2802</td><td>1033</td><td>No publisher is found for the event [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2803</td><td>1033</td><td>Dialog View did not find a record for the dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2804</td><td>1033</td><td>On activation of the control [3] on dialog [2] CMsiDialog failed to evaluate the condition [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2806</td><td>1033</td><td>The dialog [2] failed to evaluate the condition [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2807</td><td>1033</td><td>The action [2] is not recognized.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2808</td><td>1033</td><td>Default button is ill-defined on dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2809</td><td>1033</td><td>On the dialog [2] the next control pointers do not form a cycle. There is a pointer from [3] to [4], but there is no further pointer.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2810</td><td>1033</td><td>On the dialog [2] the next control pointers do not form a cycle. There is a pointer from both [3] and [5] to [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2811</td><td>1033</td><td>On dialog [2] control [3] has to take focus, but it is unable to do so.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2812</td><td>1033</td><td>The event [2] is not recognized.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2813</td><td>1033</td><td>The EndDialog event was called with the argument [2], but the dialog has a parent.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2814</td><td>1033</td><td>On the dialog [2] the control [3] names a nonexistent control [4] as the next control.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2815</td><td>1033</td><td>ControlCondition table has a row without condition for the dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2816</td><td>1033</td><td>The EventMapping table refers to an invalid control [4] on dialog [2] for the event [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2817</td><td>1033</td><td>The event [2] failed to set the attribute for the control [4] on dialog [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2818</td><td>1033</td><td>In the ControlEvent table EndDialog has an unrecognized argument [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2819</td><td>1033</td><td>Control [3] on dialog [2] needs a property linked to it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2820</td><td>1033</td><td>Attempted to initialize an already initialized handler.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2821</td><td>1033</td><td>Attempted to initialize an already initialized dialog: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2822</td><td>1033</td><td>No other method can be called on dialog [2] until all the controls are added.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2823</td><td>1033</td><td>Attempted to initialize an already initialized control: [3] on dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2824</td><td>1033</td><td>The dialog attribute [3] needs a record of at least [2] field(s).</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2825</td><td>1033</td><td>The control attribute [3] needs a record of at least [2] field(s).</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2826</td><td>1033</td><td>Control [3] on dialog [2] extends beyond the boundaries of the dialog [4] by [5] pixels.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2827</td><td>1033</td><td>The button [4] on the radio button group [3] on dialog [2] extends beyond the boundaries of the group [5] by [6] pixels.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2828</td><td>1033</td><td>Tried to remove control [3] from dialog [2], but the control is not part of the dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2829</td><td>1033</td><td>Attempt to use an uninitialized dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2830</td><td>1033</td><td>Attempt to use an uninitialized control on dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2831</td><td>1033</td><td>The control [3] on dialog [2] does not support [5] the attribute [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2832</td><td>1033</td><td>The dialog [2] does not support the attribute [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2833</td><td>1033</td><td>Control [4] on dialog [3] ignored the message [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2834</td><td>1033</td><td>The next pointers on the dialog [2] do not form a single loop.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2835</td><td>1033</td><td>The control [2] was not found on dialog [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2836</td><td>1033</td><td>The control [3] on the dialog [2] cannot take focus.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2837</td><td>1033</td><td>The control [3] on dialog [2] wants the winproc to return [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2838</td><td>1033</td><td>The item [2] in the selection table has itself as a parent.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2839</td><td>1033</td><td>Setting the property [2] failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2840</td><td>1033</td><td>Error dialog name mismatch.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2841</td><td>1033</td><td>No OK button was found on the error dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2842</td><td>1033</td><td>No text field was found on the error dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2843</td><td>1033</td><td>The ErrorString attribute is not supported for standard dialogs.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2844</td><td>1033</td><td>Cannot execute an error dialog if the Errorstring is not set.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2845</td><td>1033</td><td>The total width of the buttons exceeds the size of the error dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2846</td><td>1033</td><td>SetFocus did not find the required control on the error dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2847</td><td>1033</td><td>The control [3] on dialog [2] has both the icon and the bitmap style set.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2848</td><td>1033</td><td>Tried to set control [3] as the default button on dialog [2], but the control does not exist.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2849</td><td>1033</td><td>The control [3] on dialog [2] is of a type, that cannot be integer valued.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2850</td><td>1033</td><td>Unrecognized volume type.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2851</td><td>1033</td><td>The data for the icon [2] is not valid.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2852</td><td>1033</td><td>At least one control has to be added to dialog [2] before it is used.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2853</td><td>1033</td><td>Dialog [2] is a modeless dialog. The execute method should not be called on it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2854</td><td>1033</td><td>On the dialog [2] the control [3] is designated as first active control, but there is no such control.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2855</td><td>1033</td><td>The radio button group [3] on dialog [2] has fewer than 2 buttons.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2856</td><td>1033</td><td>Creating a second copy of the dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2857</td><td>1033</td><td>The directory [2] is mentioned in the selection table but not found.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2858</td><td>1033</td><td>The data for the bitmap [2] is not valid.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2859</td><td>1033</td><td>Test error message.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2860</td><td>1033</td><td>Cancel button is ill-defined on dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2861</td><td>1033</td><td>The next pointers for the radio buttons on dialog [2] control [3] do not form a cycle.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2862</td><td>1033</td><td>The attributes for the control [3] on dialog [2] do not define a valid icon size. Setting the size to 16.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2863</td><td>1033</td><td>The control [3] on dialog [2] needs the icon [4] in size [5]x[5], but that size is not available. Loading the first available size.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2864</td><td>1033</td><td>The control [3] on dialog [2] received a browse event, but there is no configurable directory for the present selection. Likely cause: browse button is not authored correctly.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2865</td><td>1033</td><td>Control [3] on billboard [2] extends beyond the boundaries of the billboard [4] by [5] pixels.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2866</td><td>1033</td><td>The dialog [2] is not allowed to return the argument [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2867</td><td>1033</td><td>The error dialog property is not set.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2868</td><td>1033</td><td>The error dialog [2] does not have the error style bit set.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2869</td><td>1033</td><td>The dialog [2] has the error style bit set, but is not an error dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2870</td><td>1033</td><td>The help string [4] for control [3] on dialog [2] does not contain the separator character.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2871</td><td>1033</td><td>The [2] table is out of date: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2872</td><td>1033</td><td>The argument of the CheckPath control event on dialog [2] is invalid.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2873</td><td>1033</td><td>On the dialog [2] the control [3] has an invalid string length limit: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2874</td><td>1033</td><td>Changing the text font to [2] failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2875</td><td>1033</td><td>Changing the text color to [2] failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2876</td><td>1033</td><td>The control [3] on dialog [2] had to truncate the string: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2877</td><td>1033</td><td>The binary data [2] was not found</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2878</td><td>1033</td><td>On the dialog [2] the control [3] has a possible value: [4]. This is an invalid or duplicate value.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2879</td><td>1033</td><td>The control [3] on dialog [2] cannot parse the mask string: [4].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2880</td><td>1033</td><td>Do not perform the remaining control events.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2881</td><td>1033</td><td>CMsiHandler initialization failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2882</td><td>1033</td><td>Dialog window class registration failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2883</td><td>1033</td><td>CreateNewDialog failed for the dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2884</td><td>1033</td><td>Failed to create a window for the dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2885</td><td>1033</td><td>Failed to create the control [3] on the dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2886</td><td>1033</td><td>Creating the [2] table failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2887</td><td>1033</td><td>Creating a cursor to the [2] table failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2888</td><td>1033</td><td>Executing the [2] view failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2889</td><td>1033</td><td>Creating the window for the control [3] on dialog [2] failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2890</td><td>1033</td><td>The handler failed in creating an initialized dialog.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2891</td><td>1033</td><td>Failed to destroy window for dialog [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2892</td><td>1033</td><td>[2] is an integer only control, [3] is not a valid integer value.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2893</td><td>1033</td><td>The control [3] on dialog [2] can accept property values that are at most [5] characters long. The value [4] exceeds this limit, and has been truncated.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2894</td><td>1033</td><td>Loading RICHED20.DLL failed. GetLastError() returned: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2895</td><td>1033</td><td>Freeing RICHED20.DLL failed. GetLastError() returned: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2896</td><td>1033</td><td>Executing action [2] failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2897</td><td>1033</td><td>Failed to create any [2] font on this system.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2898</td><td>1033</td><td>For [2] textstyle, the system created a '[3]' font, in [4] character set.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2899</td><td>1033</td><td>Failed to create [2] textstyle. GetLastError() returned: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_29</td><td>1033</td><td>There is not enough disk space to install the file [2].  Free some disk space and click Retry, or click Cancel to exit.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2901</td><td>1033</td><td>Invalid parameter to operation [2]: Parameter [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2902</td><td>1033</td><td>Operation [2] called out of sequence.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2903</td><td>1033</td><td>The file [2] is missing.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2904</td><td>1033</td><td>Could not BindImage file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2905</td><td>1033</td><td>Could not read record from script file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2906</td><td>1033</td><td>Missing header in script file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2907</td><td>1033</td><td>Could not create secure security descriptor. Error: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2908</td><td>1033</td><td>Could not register component [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2909</td><td>1033</td><td>Could not unregister component [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2910</td><td>1033</td><td>Could not determine user's security ID.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2911</td><td>1033</td><td>Could not remove the folder [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2912</td><td>1033</td><td>Could not schedule file [2] for removal on restart.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2919</td><td>1033</td><td>No cabinet specified for compressed file: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2920</td><td>1033</td><td>Source directory not specified for file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2924</td><td>1033</td><td>Script [2] version unsupported. Script version: [3], minimum version: [4], maximum version: [5].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2927</td><td>1033</td><td>ShellFolder id [2] is invalid.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2928</td><td>1033</td><td>Exceeded maximum number of sources. Skipping source '[2]'.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2929</td><td>1033</td><td>Could not determine publishing root. Error: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2932</td><td>1033</td><td>Could not create file [2] from script data. Error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2933</td><td>1033</td><td>Could not initialize rollback script [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2934</td><td>1033</td><td>Could not secure transform [2]. Error [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2935</td><td>1033</td><td>Could not unsecure transform [2]. Error [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2936</td><td>1033</td><td>Could not find transform [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2937</td><td>1033</td><td>Windows Installer cannot install a system file protection catalog. Catalog: [2], Error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2938</td><td>1033</td><td>Windows Installer cannot retrieve a system file protection catalog from the cache. Catalog: [2], Error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2939</td><td>1033</td><td>Windows Installer cannot delete a system file protection catalog from the cache. Catalog: [2], Error: [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2940</td><td>1033</td><td>Directory Manager not supplied for source resolution.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2941</td><td>1033</td><td>Unable to compute the CRC for file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2942</td><td>1033</td><td>BindImage action has not been executed on [2] file.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2943</td><td>1033</td><td>This version of Windows does not support deploying 64-bit packages. The script [2] is for a 64-bit package.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2944</td><td>1033</td><td>GetProductAssignmentType failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_2945</td><td>1033</td><td>Installation of ComPlus App [2] failed with error [3].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_3</td><td>1033</td><td>Info [1]. </td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_30</td><td>1033</td><td>Source file not found: [2].  Verify that the file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_3001</td><td>1033</td><td>The patches in this list contain incorrect sequencing information: [2][3][4][5][6][7][8][9][10][11][12][13][14][15][16].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_3002</td><td>1033</td><td>Patch [2] contains invalid sequencing information. </td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_31</td><td>1033</td><td>Error reading from file: [3]. {{ System error [2].}}  Verify that the file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_32</td><td>1033</td><td>Error writing to file: [3]. {{ System error [2].}}  Verify that you have access to that directory.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_33</td><td>1033</td><td>Source file not found{{(cabinet)}}: [2].  Verify that the file exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_34</td><td>1033</td><td>Cannot create the directory [2].  A file with this name already exists.  Please rename or remove the file and click Retry, or click Cancel to exit.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_35</td><td>1033</td><td>The volume [2] is currently unavailable.  Please select another.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_36</td><td>1033</td><td>The specified path [2] is unavailable.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_37</td><td>1033</td><td>Unable to write to the specified folder [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_38</td><td>1033</td><td>A network error occurred while attempting to read from the file [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_39</td><td>1033</td><td>An error occurred while attempting to create the directory [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_4</td><td>1033</td><td>Internal Error [1]. [2]{, [3]}{, [4]}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_40</td><td>1033</td><td>A network error occurred while attempting to create the directory [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_41</td><td>1033</td><td>A network error occurred while attempting to open the source file cabinet [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_42</td><td>1033</td><td>The specified path is too long [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_43</td><td>1033</td><td>The Installer has insufficient privileges to modify the file [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_44</td><td>1033</td><td>A portion of the path [2] exceeds the length allowed by the system.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_45</td><td>1033</td><td>The path [2] contains words that are not valid in folders.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_46</td><td>1033</td><td>The path [2] contains an invalid character.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_47</td><td>1033</td><td>[2] is not a valid short file name.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_48</td><td>1033</td><td>Error getting file security: [3] GetLastError: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_49</td><td>1033</td><td>Invalid Drive: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_5</td><td>1033</td><td>{{Disk full: }}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_50</td><td>1033</td><td>Could not create key [2]. {{ System error [3].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_51</td><td>1033</td><td>Could not open key: [2]. {{ System error [3].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_52</td><td>1033</td><td>Could not delete value [2] from key [3]. {{ System error [4].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_53</td><td>1033</td><td>Could not delete key [2]. {{ System error [3].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_54</td><td>1033</td><td>Could not read value [2] from key [3]. {{ System error [4].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_55</td><td>1033</td><td>Could not write value [2] to key [3]. {{ System error [4].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_56</td><td>1033</td><td>Could not get value names for key [2]. {{ System error [3].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_57</td><td>1033</td><td>Could not get sub key names for key [2]. {{ System error [3].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_58</td><td>1033</td><td>Could not read security information for key [2]. {{ System error [3].}}  Verify that you have sufficient access to that key, or contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_59</td><td>1033</td><td>Could not increase the available registry space. [2] KB of free registry space is required for the installation of this application.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_6</td><td>1033</td><td>Action [Time]: [1]. [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_60</td><td>1033</td><td>Another installation is in progress. You must complete that installation before continuing this one.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_61</td><td>1033</td><td>Error accessing secured data. Please make sure the Windows Installer is configured properly and try the installation again.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_62</td><td>1033</td><td>User [2] has previously initiated an installation for product [3].  That user will need to run that installation again before using that product.  Your current installation will now continue.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_63</td><td>1033</td><td>User [2] has previously initiated an installation for product [3].  That user will need to run that installation again before using that product.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_64</td><td>1033</td><td>Out of disk space -- Volume: '[2]'; required space: [3] KB; available space: [4] KB.  Free some disk space and retry.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_65</td><td>1033</td><td>Are you sure you want to cancel?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_66</td><td>1033</td><td>The file [2][3] is being held in use{ by the following process: Name: [4], ID: [5], Window Title: [6]}.  Close that application and retry.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_67</td><td>1033</td><td>The product [2] is already installed, preventing the installation of this product.  The two products are incompatible.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_68</td><td>1033</td><td>Out of disk space -- Volume: [2]; required space: [3] KB; available space: [4] KB.  If rollback is disabled, enough space is available. Click Cancel to quit, Retry to check available disk space again, or Ignore to continue without rollback.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_69</td><td>1033</td><td>Could not access network location [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_7</td><td>1033</td><td>[ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_70</td><td>1033</td><td>The following applications should be closed before continuing the installation:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_71</td><td>1033</td><td>Could not find any previously installed compliant products on the machine for installing this product.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_72</td><td>1033</td><td>The key [2] is not valid.  Verify that you entered the correct key.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_73</td><td>1033</td><td>The installer must restart your system before configuration of [2] can continue.  Click Yes to restart now or No if you plan to restart later.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_74</td><td>1033</td><td>You must restart your system for the configuration changes made to [2] to take effect. Click Yes to restart now or No if you plan to restart later.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_75</td><td>1033</td><td>An installation for [2] is currently suspended.  You must undo the changes made by that installation to continue.  Do you want to undo those changes?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_76</td><td>1033</td><td>A previous installation for this product is in progress.  You must undo the changes made by that installation to continue.  Do you want to undo those changes?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_77</td><td>1033</td><td>No valid source could be found for product [2].  The Windows Installer cannot continue.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_78</td><td>1033</td><td>Installation operation completed successfully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_79</td><td>1033</td><td>Installation operation failed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_8</td><td>1033</td><td>{[2]}{, [3]}{, [4]}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_80</td><td>1033</td><td>Product: [2] -- [3]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_81</td><td>1033</td><td>You may either restore your computer to its previous state or continue the installation later. Would you like to restore?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_82</td><td>1033</td><td>An error occurred while writing installation information to disk.  Check to make sure enough disk space is available, and click Retry, or Cancel to end the installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_83</td><td>1033</td><td>One or more of the files required to restore your computer to its previous state could not be found.  Restoration will not be possible.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_84</td><td>1033</td><td>The path [2] is not valid.  Please specify a valid path.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_85</td><td>1033</td><td>Out of memory. Shut down other applications before retrying.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_86</td><td>1033</td><td>There is no disk in drive [2]. Please insert one and click Retry, or click Cancel to go back to the previously selected volume.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_87</td><td>1033</td><td>There is no disk in drive [2]. Please insert one and click Retry, or click Cancel to return to the browse dialog and select a different volume.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_88</td><td>1033</td><td>The folder [2] does not exist.  Please enter a path to an existing folder.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_89</td><td>1033</td><td>You have insufficient privileges to read this folder.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_9</td><td>1033</td><td>Message type: [1], Argument: [2]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_90</td><td>1033</td><td>A valid destination folder for the installation could not be determined.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_91</td><td>1033</td><td>Error attempting to read from the source installation database: [2].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_92</td><td>1033</td><td>Scheduling reboot operation: Renaming file [2] to [3]. Must reboot to complete operation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_93</td><td>1033</td><td>Scheduling reboot operation: Deleting file [2]. Must reboot to complete operation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_94</td><td>1033</td><td>Module [2] failed to register.  HRESULT [3].  Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_95</td><td>1033</td><td>Module [2] failed to unregister.  HRESULT [3].  Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_96</td><td>1033</td><td>Failed to cache package [2]. Error: [3]. Contact your support personnel.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_97</td><td>1033</td><td>Could not register font [2].  Verify that you have sufficient permissions to install fonts, and that the system supports this font.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_98</td><td>1033</td><td>Could not unregister font [2]. Verify that you have sufficient permissions to remove fonts.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ERROR_99</td><td>1033</td><td>Could not create shortcut [2]. Verify that the destination folder exists and that you can access it.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_INSTALLDIR</td><td>1033</td><td>[INSTALLDIR]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_INSTALLSHIELD</td><td>1033</td><td>InstallShield</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_INSTALLSHIELD_FORMATTED</td><td>1033</td><td>{&amp;MSSWhiteSerif8}InstallShield</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ISSCRIPT_VERSION_MISSING</td><td>1033</td><td>The InstallScript engine is missing from this machine.  If available, please run ISScript.msi, or contact your support personnel for further assistance.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_ISSCRIPT_VERSION_OLD</td><td>1033</td><td>The InstallScript engine on this machine is older than the version required to run this setup.  If available, please install the latest version of ISScript.msi, or contact your support personnel for further assistance.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_NEXT</td><td>1033</td><td>&amp;Next &gt;</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_OK</td><td>1033</td><td>OK</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_BROWSE</td><td>1033</td><td>Open [ProductName]'s original [SETUPEXENAME]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_INVALID</td><td>1033</td><td>This executable file does not appear to be the original executable file for [ProductName]. Without using the original [SETUPEXENAME] to install additional dependencies, [ProductName] may not work correctly. Would you like to find the original [SETUPEXENAME]?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PREREQUISITE_SETUP_SEARCH</td><td>1033</td><td>This installation may require additional dependencies. Without its dependencies, [ProductName] may not work correctly. Would you like to find the original [SETUPEXENAME]?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PREVENT_DOWNGRADE_EXIT</td><td>1033</td><td>A newer version of this application is already installed on this computer. If you wish to install this version, please uninstall the newer version first. Click OK to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PRINT_BUTTON</td><td>1033</td><td>&amp;Print</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PRODUCTNAME_INSTALLSHIELD</td><td>1033</td><td>[ProductName] - InstallShield Wizard</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEAPPPOOL</td><td>1033</td><td>Creating application pool %s</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEAPPPOOLS</td><td>1033</td><td>Creating application Pools...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEVROOT</td><td>1033</td><td>Creating IIS virtual directory %s</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEVROOTS</td><td>1033</td><td>Creating IIS virtual directories...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSION</td><td>1033</td><td>Creating web service extension</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS</td><td>1033</td><td>Creating web service extensions...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSITE</td><td>1033</td><td>Creating IIS website %s</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_CREATEWEBSITES</td><td>1033</td><td>Creating IIS websites...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_EXTRACT</td><td>1033</td><td>Extracting information for IIS virtual directories...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_EXTRACTDONE</td><td>1033</td><td>Extracted information for IIS virtual directories...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEAPPPOOL</td><td>1033</td><td>Removing application pool</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEAPPPOOLS</td><td>1033</td><td>Removing application pools...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVESITE</td><td>1033</td><td>Removing web site at port %d</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEVROOT</td><td>1033</td><td>Removing IIS virtual directory %s</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEVROOTS</td><td>1033</td><td>Removing IIS virtual directories...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION</td><td>1033</td><td>Removing web service extension</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS</td><td>1033</td><td>Removing web service extensions...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_REMOVEWEBSITES</td><td>1033</td><td>Removing IIS websites...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKAPPPOOLS</td><td>1033</td><td>Rolling back application pools...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKVROOTS</td><td>1033</td><td>Rolling back virtual directory and web site changes...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS</td><td>1033</td><td>Rolling back web service extensions...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_TEXTFILECHANGS_REPLACE</td><td>1033</td><td>Replacing %s with %s in %s...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_XML_COSTING</td><td>1033</td><td>Costing XML files...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_XML_CREATE_FILE</td><td>1033</td><td>Creating XML file %s...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_XML_FILES</td><td>1033</td><td>Performing XML file changes...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_XML_REMOVE_FILE</td><td>1033</td><td>Removing XML file %s...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_XML_ROLLBACK_FILES</td><td>1033</td><td>Rolling back XML file changes...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_PROGMSG_XML_UPDATE_FILE</td><td>1033</td><td>Updating XML file %s...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SETUPEXE_EXPIRE_MSG</td><td>1033</td><td>This setup works until %s. The setup will now exit.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME1</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-710972011</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME10</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-375332544</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME11</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-375344800</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME12</td><td>1033</td><td>LAUNCH~1.EXE|Launch CheckVersion.exe</td><td>0</td><td/><td>-375340704</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME13</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-375340704</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME14</td><td>1033</td><td>LAUNCH~1.EXE|Launch setup.exe</td><td>0</td><td/><td>-375332512</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME15</td><td>1033</td><td>LAUNCH~1.EXE|Launch setup.exe</td><td>0</td><td/><td>-375330464</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME16</td><td>1033</td><td>LAUNCH~1.EXE|Launch SendEmail.exe</td><td>0</td><td/><td>-375330464</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME2</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-710969963</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME3</td><td>1033</td><td>LAUNCH~1.EXE|Launch IntroductionDemo4.0.exe</td><td>0</td><td/><td>-710967915</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME4</td><td>1033</td><td>LAUNCH~1.EXE|Launch IntroductionDemo4.0.exe</td><td>0</td><td/><td>-710967915</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME5</td><td>1033</td><td>LAUNCH~1.EXE|Launch PloobsProjectTemplate.exe</td><td>0</td><td/><td>-710961771</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME6</td><td>1033</td><td>LAUNCH~1.EXE|Launch PloobsProjectTemplate.exe</td><td>0</td><td/><td>-710961771</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME7</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-710939083</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME8</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-710985706</td></row>
		<row><td>IDS_SHORTCUT_DISPLAY_NAME9</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>1629530135</td></row>
		<row><td>IDS_SQLBROWSE_INTRO</td><td>1033</td><td>From the list of servers below, select the database server you would like to target.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLBROWSE_INTRO_DB</td><td>1033</td><td>From the list of catalog names below, select the database catalog you would like to target.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLBROWSE_INTRO_TEMPLATE</td><td>1033</td><td>[IS_SQLBROWSE_INTRO]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_BROWSE</td><td>1033</td><td>B&amp;rowse...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_BROWSE_DB</td><td>1033</td><td>Br&amp;owse...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_CATALOG</td><td>1033</td><td>&amp;Name of database catalog:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_CONNECT</td><td>1033</td><td>Connect using:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_DESC</td><td>1033</td><td>Select database server and authentication method</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_ID</td><td>1033</td><td>&amp;Login ID:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_INTRO</td><td>1033</td><td>Select the database server to install to from the list below or click Browse to see a list of all database servers. You can also specify the way to authenticate your login using your current credentials or a SQL Login ID and Password.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_PSWD</td><td>1033</td><td>&amp;Password:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_SERVER</td><td>1033</td><td>&amp;Database Server:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_SERVER2</td><td>1033</td><td>&amp;Database server that you are installing to:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_SQL</td><td>1033</td><td>S&amp;erver authentication using the Login ID and password below</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_TITLE</td><td>1033</td><td>{&amp;MSSansBold8}Database Server</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLLOGIN_WIN</td><td>1033</td><td>&amp;Windows authentication credentials of current user</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLSCRIPT_INSTALLING</td><td>1033</td><td>Executing SQL Install Script...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SQLSCRIPT_UNINSTALLING</td><td>1033</td><td>Executing SQL Uninstall Script...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_STANDARD_USE_SETUPEXE</td><td>1033</td><td>This installation cannot be run by directly launching the MSI package. You must run setup.exe.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_Advertise</td><td>1033</td><td>Will be installed on first use. (Available only if the feature supports this option.)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_AllInstalledLocal</td><td>1033</td><td>Will be completely installed to the local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_CustomSetup</td><td>1033</td><td>{&amp;MSSansBold8}Custom Setup Tips</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_CustomSetupDescription</td><td>1033</td><td>Custom Setup allows you to selectively install program features.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_IconInstallState</td><td>1033</td><td>The icon next to the feature name indicates the install state of the feature. Click the icon to drop down the install state menu for each feature.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_InstallState</td><td>1033</td><td>This install state means the feature...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_Network</td><td>1033</td><td>Will be installed to run from the network. (Available only if the feature supports this option.)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_OK</td><td>1033</td><td>OK</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_SubFeaturesInstalledLocal</td><td>1033</td><td>Will have some subfeatures installed to the local hard drive. (Available only if the feature has subfeatures.)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_SetupTips_WillNotBeInstalled</td><td>1033</td><td>Will not be installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_Available</td><td>1033</td><td>Available</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_Bytes</td><td>1033</td><td>bytes</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_CompilingFeaturesCost</td><td>1033</td><td>Compiling cost for this feature...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_Differences</td><td>1033</td><td>Differences</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_DiskSize</td><td>1033</td><td>Disk Size</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureCompletelyRemoved</td><td>1033</td><td>This feature will be completely removed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureContinueNetwork</td><td>1033</td><td>This feature will continue to be run from the network</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureFreeSpace</td><td>1033</td><td>This feature frees up [1] on your hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledCD</td><td>1033</td><td>This feature, and all subfeatures, will be installed to run from the CD.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledCD2</td><td>1033</td><td>This feature will be installed to run from CD.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledLocal</td><td>1033</td><td>This feature, and all subfeatures, will be installed on local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledLocal2</td><td>1033</td><td>This feature will be installed on local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledNetwork</td><td>1033</td><td>This feature, and all subfeatures, will be installed to run from the network.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledNetwork2</td><td>1033</td><td>This feature will be installed to run from network.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledRequired</td><td>1033</td><td>Will be installed when required.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledWhenRequired</td><td>1033</td><td>This feature will be set to be installed when required.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureInstalledWhenRequired2</td><td>1033</td><td>This feature will be installed when required.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureLocal</td><td>1033</td><td>This feature will be installed on the local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureLocal2</td><td>1033</td><td>This feature will be installed on your local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureNetwork</td><td>1033</td><td>This feature will be installed to run from the network.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureNetwork2</td><td>1033</td><td>This feature will be available to run from the network.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureNotAvailable</td><td>1033</td><td>This feature will not be available.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureOnCD</td><td>1033</td><td>This feature will be installed to run from CD.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureOnCD2</td><td>1033</td><td>This feature will be available to run from CD.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureRemainLocal</td><td>1033</td><td>This feature will remain on your local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureRemoveNetwork</td><td>1033</td><td>This feature will be removed from your local hard drive, but will be still available to run from the network.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureRemovedCD</td><td>1033</td><td>This feature will be removed from your local hard drive but will still be available to run from CD.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureRemovedUnlessRequired</td><td>1033</td><td>This feature will be removed from your local hard drive but will be set to be installed when required.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureRequiredSpace</td><td>1033</td><td>This feature requires [1] on your hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureRunFromCD</td><td>1033</td><td>This feature will continue to be run from the CD</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree</td><td>1033</td><td>This feature frees up [1] on your hard drive. It has [2] of [3] subfeatures selected. The subfeatures free up [4] on your hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree2</td><td>1033</td><td>This feature frees up [1] on your hard drive. It has [2] of [3] subfeatures selected. The subfeatures require [4] on your hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree3</td><td>1033</td><td>This feature requires [1] on your hard drive. It has [2] of [3] subfeatures selected. The subfeatures free up [4] on your hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureSpaceFree4</td><td>1033</td><td>This feature requires [1] on your hard drive. It has [2] of [3] subfeatures selected. The subfeatures require [4] on your hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureUnavailable</td><td>1033</td><td>This feature will become unavailable.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureUninstallNoNetwork</td><td>1033</td><td>This feature will be uninstalled completely, and you won't be able to run it from the network.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureWasCD</td><td>1033</td><td>This feature was run from the CD but will be set to be installed when required.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureWasCDLocal</td><td>1033</td><td>This feature was run from the CD but will be installed on the local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureWasOnNetworkInstalled</td><td>1033</td><td>This feature was run from the network but will be installed when required.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureWasOnNetworkLocal</td><td>1033</td><td>This feature was run from the network but will be installed on the local hard drive.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_FeatureWillBeUninstalled</td><td>1033</td><td>This feature will be uninstalled completely, and you won't be able to run it from CD.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_Folder</td><td>1033</td><td>Fldr|New Folder</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_GB</td><td>1033</td><td>GB</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_KB</td><td>1033</td><td>KB</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_MB</td><td>1033</td><td>MB</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_Required</td><td>1033</td><td>Required</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_TimeRemaining</td><td>1033</td><td>Time remaining: {[1] min }{[2] sec}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS_UITEXT_Volume</td><td>1033</td><td>Volume</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__AgreeToLicense_0</td><td>1033</td><td>I &amp;do not accept the terms in the license agreement</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__AgreeToLicense_1</td><td>1033</td><td>I &amp;accept the terms in the license agreement</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DatabaseFolder_ChangeFolder</td><td>1033</td><td>Click Next to install to this folder, or click Change to install to a different folder.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DatabaseFolder_DatabaseDir</td><td>1033</td><td>[DATABASEDIR]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DatabaseFolder_DatabaseFolder</td><td>1033</td><td>{&amp;MSSansBold8}Database Folder</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DestinationFolder_Change</td><td>1033</td><td>&amp;Change...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DestinationFolder_ChangeFolder</td><td>1033</td><td>Click Next to install to this folder, or click Change to install to a different folder.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DestinationFolder_DestinationFolder</td><td>1033</td><td>{&amp;MSSansBold8}Destination Folder</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DestinationFolder_InstallTo</td><td>1033</td><td>Install [ProductName] to:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DisplayName_Custom</td><td>1033</td><td>Custom</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DisplayName_Minimal</td><td>1033</td><td>Minimal</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__DisplayName_Typical</td><td>1033</td><td>Typical</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_11</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_4</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_8</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_BrowseDestination</td><td>1033</td><td>Browse to the destination folder.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_ChangeDestination</td><td>1033</td><td>{&amp;MSSansBold8}Change Current Destination Folder</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_CreateFolder</td><td>1033</td><td>Create new folder|</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_FolderName</td><td>1033</td><td>&amp;Folder name:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_LookIn</td><td>1033</td><td>&amp;Look in:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallBrowse_UpOneLevel</td><td>1033</td><td>Up one level|</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPointWelcome_ServerImage</td><td>1033</td><td>The InstallShield(R) Wizard will create a server image of [ProductName] at a specified network location. To continue, click Next.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPointWelcome_Wizard</td><td>1033</td><td>{&amp;TahomaBold10}Welcome to the InstallShield Wizard for [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPoint_Change</td><td>1033</td><td>&amp;Change...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPoint_EnterNetworkLocation</td><td>1033</td><td>Enter the network location or click Change to browse to a location.  Click Install to create a server image of [ProductName] at the specified network location or click Cancel to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPoint_Install</td><td>1033</td><td>&amp;Install</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPoint_NetworkLocation</td><td>1033</td><td>&amp;Network location:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPoint_NetworkLocationFormatted</td><td>1033</td><td>{&amp;MSSansBold8}Network Location</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsAdminInstallPoint_SpecifyNetworkLocation</td><td>1033</td><td>Specify a network location for the server image of the product.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseButton</td><td>1033</td><td>&amp;Browse...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_11</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_4</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_8</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_BrowseDestFolder</td><td>1033</td><td>Browse to the destination folder.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_ChangeCurrentFolder</td><td>1033</td><td>{&amp;MSSansBold8}Change Current Destination Folder</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_CreateFolder</td><td>1033</td><td>Create New Folder|</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_FolderName</td><td>1033</td><td>&amp;Folder name:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_LookIn</td><td>1033</td><td>&amp;Look in:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_OK</td><td>1033</td><td>OK</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseFolderDlg_UpOneLevel</td><td>1033</td><td>Up One Level|</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseForAccount</td><td>1033</td><td>Browse for a User Account</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseGroup</td><td>1033</td><td>Select a Group</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsBrowseUsernameTitle</td><td>1033</td><td>Select a User Name</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCancelDlg_ConfirmCancel</td><td>1033</td><td>Are you sure you want to cancel [ProductName] installation?</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCancelDlg_No</td><td>1033</td><td>&amp;No</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCancelDlg_Yes</td><td>1033</td><td>&amp;Yes</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsConfirmPassword</td><td>1033</td><td>Con&amp;firm password:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCreateNewUserTitle</td><td>1033</td><td>New User Information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCreateUserBrowse</td><td>1033</td><td>N&amp;ew User Information...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Change</td><td>1033</td><td>&amp;Change...</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_ClickFeatureIcon</td><td>1033</td><td>Click on an icon in the list below to change how a feature is installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_CustomSetup</td><td>1033</td><td>{&amp;MSSansBold8}Custom Setup</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeatureDescription</td><td>1033</td><td>Feature Description</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeaturePath</td><td>1033</td><td>&lt;selected feature path&gt;</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_FeatureSize</td><td>1033</td><td>Feature size</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Help</td><td>1033</td><td>&amp;Help</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_InstallTo</td><td>1033</td><td>Install to:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_MultilineDescription</td><td>1033</td><td>Multiline description of the currently selected item</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_SelectFeatures</td><td>1033</td><td>Select the program features you want installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsCustomSelectionDlg_Space</td><td>1033</td><td>&amp;Space</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsDiskSpaceDlg_DiskSpace</td><td>1033</td><td>Disk space required for the installation exceeds available disk space.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsDiskSpaceDlg_HighlightedVolumes</td><td>1033</td><td>The highlighted volumes do not have enough disk space available for the currently selected features. You can remove files from the highlighted volumes, choose to install fewer features onto local drives, or select different destination drives.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsDiskSpaceDlg_Numbers</td><td>1033</td><td>{120}{70}{70}{70}{70}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsDiskSpaceDlg_OK</td><td>1033</td><td>OK</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsDiskSpaceDlg_OutOfDiskSpace</td><td>1033</td><td>{&amp;MSSansBold8}Out of Disk Space</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsDomainOrServer</td><td>1033</td><td>&amp;Domain or server:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_Abort</td><td>1033</td><td>&amp;Abort</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_ErrorText</td><td>1033</td><td>&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;&lt;error text goes here&gt;</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_Ignore</td><td>1033</td><td>&amp;Ignore</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_InstallerInfo</td><td>1033</td><td>[ProductName] Installer Information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_NO</td><td>1033</td><td>&amp;No</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_OK</td><td>1033</td><td>&amp;OK</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_Retry</td><td>1033</td><td>&amp;Retry</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsErrorDlg_Yes</td><td>1033</td><td>&amp;Yes</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_Finish</td><td>1033</td><td>&amp;Finish</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_InstallSuccess</td><td>1033</td><td>The InstallShield Wizard has successfully installed [ProductName]. Click Finish to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_LaunchProgram</td><td>1033</td><td>Launch the program</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_ShowReadMe</td><td>1033</td><td>Show the readme file</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_UninstallSuccess</td><td>1033</td><td>The InstallShield Wizard has successfully uninstalled [ProductName]. Click Finish to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_Update_InternetConnection</td><td>1033</td><td>Your Internet connection can be used to make sure that you have the latest updates.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_Update_PossibleUpdates</td><td>1033</td><td>Some program files might have been updated since you purchased your copy of [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_Update_SetupFinished</td><td>1033</td><td>Setup has finished installing [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_Update_YesCheckForUpdates</td><td>1033</td><td>&amp;Yes, check for program updates (Recommended) after the setup completes.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsExitDialog_WizardCompleted</td><td>1033</td><td>{&amp;TahomaBold10}InstallShield Wizard Completed</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_ClickFinish</td><td>1033</td><td>Click Finish to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_Finish</td><td>1033</td><td>&amp;Finish</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_KeepOrRestore</td><td>1033</td><td>You can either keep any existing installed elements on your system to continue this installation at a later time or you can restore your system to its original state prior to the installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_NotModified</td><td>1033</td><td>Your system has not been modified. To complete installation at another time, please run setup again.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_RestoreOrContinueLater</td><td>1033</td><td>Click Restore or Continue Later to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_WizardCompleted</td><td>1033</td><td>{&amp;TahomaBold10}InstallShield Wizard Completed</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFatalError_WizardInterrupted</td><td>1033</td><td>The wizard was interrupted before [ProductName] could be completely installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_DiskSpaceRequirements</td><td>1033</td><td>{&amp;MSSansBold8}Disk Space Requirements</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_Numbers</td><td>1033</td><td>{120}{70}{70}{70}{70}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_OK</td><td>1033</td><td>OK</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_SpaceRequired</td><td>1033</td><td>The disk space required for the installation of the selected features.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFeatureDetailsDlg_VolumesTooSmall</td><td>1033</td><td>The highlighted volumes do not have enough disk space available for the currently selected features. You can remove files from the highlighted volumes, choose to install fewer features onto local drives, or select different destination drives.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFilesInUse_ApplicationsUsingFiles</td><td>1033</td><td>The following applications are using files that need to be updated by this setup. Close these applications and click Retry to continue.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFilesInUse_Exit</td><td>1033</td><td>&amp;Exit</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFilesInUse_FilesInUse</td><td>1033</td><td>{&amp;MSSansBold8}Files in Use</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFilesInUse_FilesInUseMessage</td><td>1033</td><td>Some files that need to be updated are currently in use.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFilesInUse_Ignore</td><td>1033</td><td>&amp;Ignore</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsFilesInUse_Retry</td><td>1033</td><td>&amp;Retry</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsGroup</td><td>1033</td><td>&amp;Group:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsGroupLabel</td><td>1033</td><td>Gr&amp;oup:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsInitDlg_1</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsInitDlg_2</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsInitDlg_PreparingWizard</td><td>1033</td><td>[ProductName] Setup is preparing the InstallShield Wizard which will guide you through the program setup process.  Please wait.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsInitDlg_WelcomeWizard</td><td>1033</td><td>{&amp;TahomaBold10}Welcome to the InstallShield Wizard for [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsLicenseDlg_LicenseAgreement</td><td>1033</td><td>{&amp;MSSansBold8}License Agreement</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsLicenseDlg_ReadLicenseAgreement</td><td>1033</td><td>Please read the following license agreement carefully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsLogonInfoDescription</td><td>1033</td><td>Specify the user name and password of the user account that will logon to use this application. The user account must be in the form DOMAIN\Username.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsLogonInfoTitle</td><td>1033</td><td>{&amp;MSSansBold8}Logon Information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsLogonInfoTitleDescription</td><td>1033</td><td>Specify a user name and password</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsLogonNewUserDescription</td><td>1033</td><td>Select the button below to specify information about a new user that will be created during the installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_ChangeFeatures</td><td>1033</td><td>Change which program features are installed. This option displays the Custom Selection dialog in which you can change the way features are installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_MaitenanceOptions</td><td>1033</td><td>Modify, repair, or remove the program.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_Modify</td><td>1033</td><td>{&amp;MSSansBold8}&amp;Modify</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_ProgramMaintenance</td><td>1033</td><td>{&amp;MSSansBold8}Program Maintenance</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_Remove</td><td>1033</td><td>{&amp;MSSansBold8}&amp;Remove</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_RemoveProductName</td><td>1033</td><td>Remove [ProductName] from your computer.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_Repair</td><td>1033</td><td>{&amp;MSSansBold8}Re&amp;pair</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceDlg_RepairMessage</td><td>1033</td><td>Repair installation errors in the program. This option fixes missing or corrupt files, shortcuts, and registry entries.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceWelcome_MaintenanceOptionsDescription</td><td>1033</td><td>The InstallShield(R) Wizard will allow you to modify, repair, or remove [ProductName]. To continue, click Next.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMaintenanceWelcome_WizardWelcome</td><td>1033</td><td>{&amp;TahomaBold10}Welcome to the InstallShield Wizard for [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_ApplicationsUsingFiles</td><td>1033</td><td>The following applications are using files that need to be updated by this setup.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_CloseRestart</td><td>1033</td><td>Automatically close and attempt to restart applications.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsMsiRMFilesInUse_RebootAfter</td><td>1033</td><td>Do not close applications. (A reboot will be required.)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsPatchDlg_PatchClickUpdate</td><td>1033</td><td>The InstallShield(R) Wizard will install the Patch for [ProductName] on your computer.  To continue, click Update.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsPatchDlg_PatchWizard</td><td>1033</td><td>[ProductName] Patch - InstallShield Wizard</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsPatchDlg_Update</td><td>1033</td><td>&amp;Update &gt;</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsPatchDlg_WelcomePatchWizard</td><td>1033</td><td>{&amp;TahomaBold10}Welcome to the Patch for [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_2</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_Hidden</td><td>1033</td><td>(Hidden for now)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_HiddenTimeRemaining</td><td>1033</td><td>)Hidden for now)Estimated time remaining:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_InstallingProductName</td><td>1033</td><td>{&amp;MSSansBold8}Installing [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_ProgressDone</td><td>1033</td><td>Progress done</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_SecHidden</td><td>1033</td><td>(Hidden for now)Sec.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_Status</td><td>1033</td><td>Status:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_Uninstalling</td><td>1033</td><td>{&amp;MSSansBold8}Uninstalling [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_UninstallingFeatures</td><td>1033</td><td>The program features you selected are being uninstalled.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_UninstallingFeatures2</td><td>1033</td><td>The program features you selected are being installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_WaitUninstall</td><td>1033</td><td>Please wait while the InstallShield Wizard uninstalls [ProductName]. This may take several minutes.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsProgressDlg_WaitUninstall2</td><td>1033</td><td>Please wait while the InstallShield Wizard installs [ProductName]. This may take several minutes.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsReadmeDlg_Cancel</td><td>1033</td><td>&amp;Cancel</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsReadmeDlg_PleaseReadInfo</td><td>1033</td><td>Please read the following readme information carefully.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsReadmeDlg_ReadMeInfo</td><td>1033</td><td>{&amp;MSSansBold8}Readme Information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_16</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_Anyone</td><td>1033</td><td>&amp;Anyone who uses this computer (all users)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_CustomerInformation</td><td>1033</td><td>{&amp;MSSansBold8}Customer Information</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_InstallFor</td><td>1033</td><td>Install this application for:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_OnlyMe</td><td>1033</td><td>Only for &amp;me ([USERNAME])</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_Organization</td><td>1033</td><td>&amp;Organization:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_PleaseEnterInfo</td><td>1033</td><td>Please enter your information.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_SerialNumber</td><td>1033</td><td>&amp;Serial Number:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_Tahoma50</td><td>1033</td><td>{\Tahoma8}{50}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_Tahoma80</td><td>1033</td><td>{\Tahoma8}{80}</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsRegisterUserDlg_UserName</td><td>1033</td><td>&amp;User Name:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsResumeDlg_ResumeSuspended</td><td>1033</td><td>The InstallShield(R) Wizard will complete the suspended installation of [ProductName] on your computer. To continue, click Next.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsResumeDlg_Resuming</td><td>1033</td><td>{&amp;TahomaBold10}Resuming the InstallShield Wizard for [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsResumeDlg_WizardResume</td><td>1033</td><td>The InstallShield(R) Wizard will complete the installation of [ProductName] on your computer. To continue, click Next.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSelectDomainOrServer</td><td>1033</td><td>Select a Domain or Server</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSelectDomainUserInstructions</td><td>1033</td><td>Use the browse buttons to select a domain\server and a user name.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupComplete_ShowMsiLog</td><td>1033</td><td>Show the Windows Installer log</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_13</td><td>1033</td><td/><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_AllFeatures</td><td>1033</td><td>All program features will be installed. (Requires the most disk space.)</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_ChooseFeatures</td><td>1033</td><td>Choose which program features you want installed and where they will be installed. Recommended for advanced users.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_ChooseSetupType</td><td>1033</td><td>Choose the setup type that best suits your needs.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Complete</td><td>1033</td><td>{&amp;MSSansBold8}&amp;Complete</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Custom</td><td>1033</td><td>{&amp;MSSansBold8}Cu&amp;stom</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Minimal</td><td>1033</td><td>{&amp;MSSansBold8}&amp;Minimal</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_MinimumFeatures</td><td>1033</td><td>Minimum required features will be installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_SelectSetupType</td><td>1033</td><td>Please select a setup type.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_SetupType</td><td>1033</td><td>{&amp;MSSansBold8}Setup Type</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsSetupTypeMinDlg_Typical</td><td>1033</td><td>{&amp;MSSansBold8}&amp;Typical</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_ClickFinish</td><td>1033</td><td>Click Finish to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_Finish</td><td>1033</td><td>&amp;Finish</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_KeepOrRestore</td><td>1033</td><td>You can either keep any existing installed elements on your system to continue this installation at a later time or you can restore your system to its original state prior to the installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_NotModified</td><td>1033</td><td>Your system has not been modified. To install this program at a later time, please run the installation again.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_RestoreOrContinue</td><td>1033</td><td>Click Restore or Continue Later to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_WizardCompleted</td><td>1033</td><td>{&amp;TahomaBold10}InstallShield Wizard Completed</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserExit_WizardInterrupted</td><td>1033</td><td>The wizard was interrupted before [ProductName] could be completely installed.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsUserNameLabel</td><td>1033</td><td>&amp;User name:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_BackOrCancel</td><td>1033</td><td>If you want to review or change any of your installation settings, click Back. Click Cancel to exit the wizard.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ClickInstall</td><td>1033</td><td>Click Install to begin the installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Company</td><td>1033</td><td>Company: [COMPANYNAME]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_CurrentSettings</td><td>1033</td><td>Current Settings:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_DestFolder</td><td>1033</td><td>Destination Folder:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Install</td><td>1033</td><td>&amp;Install</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Installdir</td><td>1033</td><td>[INSTALLDIR]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ModifyReady</td><td>1033</td><td>{&amp;MSSansBold8}Ready to Modify the Program</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ReadyInstall</td><td>1033</td><td>{&amp;MSSansBold8}Ready to Install the Program</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_ReadyRepair</td><td>1033</td><td>{&amp;MSSansBold8}Ready to Repair the Program</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_SelectedSetupType</td><td>1033</td><td>[SelectedSetupType]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_Serial</td><td>1033</td><td>Serial: [ISX_SERIALNUM]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_SetupType</td><td>1033</td><td>Setup Type:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_UserInfo</td><td>1033</td><td>User Information:</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_UserName</td><td>1033</td><td>Name: [USERNAME]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyReadyDlg_WizardReady</td><td>1033</td><td>The wizard is ready to begin installation.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ChoseRemoveProgram</td><td>1033</td><td>You have chosen to remove the program from your system.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ClickBack</td><td>1033</td><td>If you want to review or change any settings, click Back.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_ClickRemove</td><td>1033</td><td>Click Remove to remove [ProductName] from your computer. After removal, this program will no longer be available for use.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_Remove</td><td>1033</td><td>&amp;Remove</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsVerifyRemoveAllDlg_RemoveProgram</td><td>1033</td><td>{&amp;MSSansBold8}Remove the Program</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsWelcomeDlg_InstallProductName</td><td>1033</td><td>The InstallShield(R) Wizard will install [ProductName] on your computer. To continue, click Next.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsWelcomeDlg_WarningCopyright</td><td>1033</td><td>WARNING: This program is protected by copyright law and international treaties.</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__IsWelcomeDlg_WelcomeProductName</td><td>1033</td><td>{&amp;TahomaBold10}Welcome to the InstallShield Wizard for [ProductName]</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__TargetReq_DESC_COLOR</td><td>1033</td><td>The color settings of your system are not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__TargetReq_DESC_OS</td><td>1033</td><td>The operating system is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__TargetReq_DESC_PROCESSOR</td><td>1033</td><td>The processor is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__TargetReq_DESC_RAM</td><td>1033</td><td>The amount of RAM is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>IDS__TargetReq_DESC_RESOLUTION</td><td>1033</td><td>The screen resolution is not adequate for running [ProductName].</td><td>0</td><td/><td>-1650505517</td></row>
		<row><td>ID_STRING1</td><td>1033</td><td/><td>0</td><td/><td>-1650503469</td></row>
		<row><td>ID_STRING10</td><td>1033</td><td>PLOOBS~1|PloobsHelp</td><td>0</td><td/><td>-710961515</td></row>
		<row><td>ID_STRING11</td><td>1033</td><td>PLOOBS~1|PloobsDemos Code</td><td>0</td><td/><td>-710951275</td></row>
		<row><td>ID_STRING12</td><td>1033</td><td>PLOOBS~1|PloobsHelp</td><td>0</td><td/><td>-710949227</td></row>
		<row><td>ID_STRING13</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-710996490</td></row>
		<row><td>ID_STRING14</td><td>1033</td><td>PloobsEngine Installer</td><td>0</td><td/><td>-710946569</td></row>
		<row><td>ID_STRING15</td><td>1033</td><td>PloobsEngine Game Engine</td><td>0</td><td/><td>-710999785</td></row>
		<row><td>ID_STRING16</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>1906353056</td></row>
		<row><td>ID_STRING2</td><td>1033</td><td>http://www.Ploobs.com</td><td>0</td><td/><td>-375340767</td></row>
		<row><td>ID_STRING3</td><td>1033</td><td>Ploobs</td><td>0</td><td/><td>-375340767</td></row>
		<row><td>ID_STRING4</td><td>1033</td><td>http://www.ploobs.com.br/forum</td><td>0</td><td/><td>-375340767</td></row>
		<row><td>ID_STRING5</td><td>1033</td><td>contato@ploobs.com.br</td><td>0</td><td/><td>-1650489037</td></row>
		<row><td>ID_STRING6</td><td>1033</td><td>55 11 8888 8888</td><td>0</td><td/><td>-1650523821</td></row>
		<row><td>ID_STRING7</td><td>1033</td><td>http://www.Ploobs.com.br</td><td>0</td><td/><td>-1910530989</td></row>
		<row><td>ID_STRING8</td><td>1033</td><td>LAUNCH~1.EXE|Launch AdvancedDemos.exe</td><td>0</td><td/><td>-710988203</td></row>
		<row><td>ID_STRING9</td><td>1033</td><td>PLOOBS~1|PloobsDemos Code</td><td>0</td><td/><td>-710986091</td></row>
		<row><td>IIDS_UITEXT_FeatureUninstalled</td><td>1033</td><td>This feature will remain uninstalled.</td><td>0</td><td/><td>-1650505517</td></row>
	</table>

	<table name="ISTargetImage">
		<col key="yes" def="s13">UpgradedImage_</col>
		<col key="yes" def="s13">Name</col>
		<col def="s0">MsiPath</col>
		<col def="i2">Order</col>
		<col def="I4">Flags</col>
		<col def="i2">IgnoreMissingFiles</col>
	</table>

	<table name="ISUpgradeMsiItem">
		<col key="yes" def="s72">UpgradeItem</col>
		<col def="s0">ObjectSetupPath</col>
		<col def="S255">ISReleaseFlags</col>
		<col def="i2">ISAttributes</col>
	</table>

	<table name="ISUpgradedImage">
		<col key="yes" def="s13">Name</col>
		<col def="s0">MsiPath</col>
		<col def="s8">Family</col>
	</table>

	<table name="ISVirtualDirectory">
		<col key="yes" def="s72">Directory_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualPackage">
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualRegistry">
		<col key="yes" def="s72">Registry_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualRelease">
		<col key="yes" def="s72">ISRelease_</col>
		<col key="yes" def="s72">ISProductConfiguration_</col>
		<col key="yes" def="s255">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISVirtualShortcut">
		<col key="yes" def="s72">Shortcut_</col>
		<col key="yes" def="s72">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="ISXmlElement">
		<col key="yes" def="s72">ISXmlElement</col>
		<col def="s72">ISXmlFile_</col>
		<col def="S72">ISXmlElement_Parent</col>
		<col def="L0">XPath</col>
		<col def="L0">Content</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISXmlElementAttrib">
		<col key="yes" def="s72">ISXmlElementAttrib</col>
		<col key="yes" def="s72">ISXmlElement_</col>
		<col def="L255">Name</col>
		<col def="L0">Value</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="ISXmlFile">
		<col key="yes" def="s72">ISXmlFile</col>
		<col def="l255">FileName</col>
		<col def="s72">Component_</col>
		<col def="s72">Directory</col>
		<col def="I4">ISAttributes</col>
		<col def="S0">SelectionNamespaces</col>
		<col def="S255">Encoding</col>
	</table>

	<table name="ISXmlLocator">
		<col key="yes" def="s72">Signature_</col>
		<col key="yes" def="S72">Parent</col>
		<col def="S255">Element</col>
		<col def="S255">Attribute</col>
		<col def="I2">ISAttributes</col>
	</table>

	<table name="Icon">
		<col key="yes" def="s72">Name</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
		<col def="I2">ISIconIndex</col>
		<row><td>ARPPRODUCTICON.exe</td><td/><td>C:\PloobsPackages\Extra\Logo Ploobs.ico</td><td>0</td></row>
		<row><td>AdvancedDemos.exe1_9DB5D6AEFA124E3DA8CF530CF2CC721A.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\Demos\AdvancedDemos\AdvancedDemos\AdvancedDemos\obj\x86\Debug\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe1_B40AEFBA2B34475AB951A1FB015F9B75.exe</td><td/><td>C:\PloobsPackages\AdvDemosBin\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe1_C27B04AF4D7B4D5183530DD025DE344C.exe</td><td/><td>C:\Dlls\Windows HiDef\PloobsEngineDebug.dll</td><td>0</td></row>
		<row><td>AdvancedDemos.exe1_D62F826B0F4E49CA896624A3986EF72C.exe</td><td/><td>C:\PloobsPackages\AdvDemosBin\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe1_E23014CDA5E04106B55219C7FDBC614A.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\AdvDemosBinaries\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe2_6154E393B7704CAC88BBA7F0DBF34416.exe</td><td/><td>C:\PloobsPackages\PloobsEngine\program files\Ploobs\PloobsEngine\AdvDemosBin\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe_5CDF6576053D4A4C906E0F5EB48406D2.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\Demos\AdvancedDemos\AdvancedDemos\AdvancedDemos\bin\x86\Debug\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe_6A40C0350952450FB1B9EA113D1E4B98.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\AdvDemosBinaries\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe_6CC9FEA39ABF4BD090C0B67E5AC76FD4.exe</td><td/><td>C:\PloobsPackages\AdvDemosBin\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>AdvancedDemos.exe_C22A40BD76874218813E1B1FA2004D79.exe</td><td/><td>C:\Dlls\Windows HiDef\PloobsEngineDebug.dll</td><td>0</td></row>
		<row><td>AdvancedDemos.exe_D769F19A128A4529992174320BA39633.exe</td><td/><td>C:\PloobsPackages\AdvDemosBin\AdvancedDemos.exe</td><td>0</td></row>
		<row><td>CheckVersion.exe_4A52161B8EB6408DA644DF1ADFC081AC.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>IntroductionDemo4._6C95F7DA50704362AFAAA600902C84E6.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\Demos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\IntroductionDemo4.0\obj\x86\Debug\IntroductionDemo4.0.exe</td><td>0</td></row>
		<row><td>IntroductionDemo4._B7DB531E6F6546A7A9C24D02D5F2A00F.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\Demos\IntroductionDemo\IntroductionDemo4.0\IntroductionDemo4.0\IntroductionDemo4.0\bin\x86\Debug\IntroductionDemo4.0.exe</td><td>0</td></row>
		<row><td>PloobsProjectTempl_02BF06FCC83A4251943032B8B2EDEA9C.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\Demos\ReachDemos\ReachDemos\PloobsProjectTemplate\bin\x86\Debug\PloobsProjectTemplate.exe</td><td>0</td></row>
		<row><td>PloobsProjectTempl_15A79966EACB4594ACABC2FF8AB54870.exe</td><td/><td>C:\Users\tpastor\Desktop\Dlls\Demos\ReachDemos\ReachDemos\PloobsProjectTemplate\obj\x86\Debug\PloobsProjectTemplate.exe</td><td>0</td></row>
		<row><td>SendEmail.exe_7C6F2E8305BB416198F59344B76FD534.exe</td><td/><td>&lt;ISProductFolder&gt;\redist\Language Independent\OS Independent\GenericExe.ico</td><td>0</td></row>
		<row><td>UNINST_Uninstall_P_5ABF4EFCD1D2450DB61B10E01DE95B13.exe</td><td/><td>C:\Program Files (x86)\InstallShield\2011LE\Redist\Language Independent\OS Independent\uninstall.ico</td><td>0</td></row>
		<row><td>setup.exe1_68845380BD4341D9BD2D98577C7F148D.exe</td><td/><td>C:\PloobsPackages\PloobsUpdater\setup.exe</td><td>0</td></row>
		<row><td>setup.exe_4FFCC3A6D4704D89856AFF1DC48DC6EC.exe</td><td/><td>C:\PloobsPackages\PloobsEngine\setup.exe</td><td>0</td></row>
	</table>

	<table name="IniFile">
		<col key="yes" def="s72">IniFile</col>
		<col def="l255">FileName</col>
		<col def="S72">DirProperty</col>
		<col def="l255">Section</col>
		<col def="l128">Key</col>
		<col def="s255">Value</col>
		<col def="i2">Action</col>
		<col def="s72">Component_</col>
	</table>

	<table name="IniLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="s255">FileName</col>
		<col def="s96">Section</col>
		<col def="s128">Key</col>
		<col def="I2">Field</col>
		<col def="I2">Type</col>
	</table>

	<table name="InstallExecuteSequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AllocateRegistrySpace</td><td>NOT Installed</td><td>1550</td><td>AllocateRegistrySpace</td><td/></row>
		<row><td>AppSearch</td><td/><td>400</td><td>AppSearch</td><td/></row>
		<row><td>BindImage</td><td/><td>4300</td><td>BindImage</td><td/></row>
		<row><td>CCPSearch</td><td>CCP_TEST</td><td>500</td><td>CCPSearch</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>CreateFolders</td><td/><td>3700</td><td>CreateFolders</td><td/></row>
		<row><td>CreateShortcuts</td><td/><td>4500</td><td>CreateShortcuts</td><td/></row>
		<row><td>DeleteServices</td><td>VersionNT</td><td>2000</td><td>DeleteServices</td><td/></row>
		<row><td>DuplicateFiles</td><td/><td>4210</td><td>DuplicateFiles</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>FindRelatedProducts</td><td>NOT ISSETUPDRIVEN</td><td>420</td><td>FindRelatedProducts</td><td/></row>
		<row><td>ISPreventDowngrade</td><td>ISFOUNDNEWERPRODUCTVERSION</td><td>450</td><td>ISPreventDowngrade</td><td/></row>
		<row><td>ISRunSetupTypeAddLocalEvent</td><td>Not Installed And Not ISRUNSETUPTYPEADDLOCALEVENT</td><td>1050</td><td>ISRunSetupTypeAddLocalEvent</td><td/></row>
		<row><td>ISSelfRegisterCosting</td><td/><td>2201</td><td/><td/></row>
		<row><td>ISSelfRegisterFiles</td><td/><td>5601</td><td/><td/></row>
		<row><td>ISSelfRegisterFinalize</td><td/><td>6601</td><td/><td/></row>
		<row><td>ISSetAllUsers</td><td>Not Installed</td><td>10</td><td/><td/></row>
		<row><td>ISUnSelfRegisterFiles</td><td/><td>2202</td><td/><td/></row>
		<row><td>InstallFiles</td><td/><td>4000</td><td>InstallFiles</td><td/></row>
		<row><td>InstallFinalize</td><td/><td>6600</td><td>InstallFinalize</td><td/></row>
		<row><td>InstallInitialize</td><td/><td>1501</td><td>InstallInitialize</td><td/></row>
		<row><td>InstallODBC</td><td/><td>5400</td><td>InstallODBC</td><td/></row>
		<row><td>InstallServices</td><td>VersionNT</td><td>5800</td><td>InstallServices</td><td/></row>
		<row><td>InstallValidate</td><td/><td>1400</td><td>InstallValidate</td><td/></row>
		<row><td>IsolateComponents</td><td/><td>950</td><td>IsolateComponents</td><td/></row>
		<row><td>LaunchConditions</td><td>Not Installed</td><td>410</td><td>LaunchConditions</td><td/></row>
		<row><td>MigrateFeatureStates</td><td/><td>1010</td><td>MigrateFeatureStates</td><td/></row>
		<row><td>MoveFiles</td><td/><td>3800</td><td>MoveFiles</td><td/></row>
		<row><td>MsiConfigureServices</td><td>VersionMsi &gt;= "5.00"</td><td>5850</td><td>MSI5 MsiConfigureServices</td><td/></row>
		<row><td>MsiPublishAssemblies</td><td/><td>6250</td><td>MsiPublishAssemblies</td><td/></row>
		<row><td>MsiUnpublishAssemblies</td><td/><td>1750</td><td>MsiUnpublishAssemblies</td><td/></row>
		<row><td>PatchFiles</td><td/><td>4090</td><td>PatchFiles</td><td/></row>
		<row><td>ProcessComponents</td><td/><td>1600</td><td>ProcessComponents</td><td/></row>
		<row><td>PublishComponents</td><td/><td>6200</td><td>PublishComponents</td><td/></row>
		<row><td>PublishFeatures</td><td/><td>6300</td><td>PublishFeatures</td><td/></row>
		<row><td>PublishProduct</td><td/><td>6400</td><td>PublishProduct</td><td/></row>
		<row><td>RMCCPSearch</td><td>Not CCP_SUCCESS And CCP_TEST</td><td>600</td><td>RMCCPSearch</td><td/></row>
		<row><td>RegisterClassInfo</td><td/><td>4600</td><td>RegisterClassInfo</td><td/></row>
		<row><td>RegisterComPlus</td><td/><td>5700</td><td>RegisterComPlus</td><td/></row>
		<row><td>RegisterExtensionInfo</td><td/><td>4700</td><td>RegisterExtensionInfo</td><td/></row>
		<row><td>RegisterFonts</td><td/><td>5300</td><td>RegisterFonts</td><td/></row>
		<row><td>RegisterMIMEInfo</td><td/><td>4900</td><td>RegisterMIMEInfo</td><td/></row>
		<row><td>RegisterProduct</td><td/><td>6100</td><td>RegisterProduct</td><td/></row>
		<row><td>RegisterProgIdInfo</td><td/><td>4800</td><td>RegisterProgIdInfo</td><td/></row>
		<row><td>RegisterTypeLibraries</td><td/><td>5500</td><td>RegisterTypeLibraries</td><td/></row>
		<row><td>RegisterUser</td><td/><td>6000</td><td>RegisterUser</td><td/></row>
		<row><td>RemoveDuplicateFiles</td><td/><td>3400</td><td>RemoveDuplicateFiles</td><td/></row>
		<row><td>RemoveEnvironmentStrings</td><td/><td>3300</td><td>RemoveEnvironmentStrings</td><td/></row>
		<row><td>RemoveExistingProducts</td><td/><td>6601</td><td>RemoveExistingProducts</td><td/></row>
		<row><td>RemoveFiles</td><td/><td>3500</td><td>RemoveFiles</td><td/></row>
		<row><td>RemoveFolders</td><td/><td>3600</td><td>RemoveFolders</td><td/></row>
		<row><td>RemoveIniValues</td><td/><td>3100</td><td>RemoveIniValues</td><td/></row>
		<row><td>RemoveODBC</td><td/><td>2400</td><td>RemoveODBC</td><td/></row>
		<row><td>RemoveRegistryValues</td><td/><td>2600</td><td>RemoveRegistryValues</td><td/></row>
		<row><td>RemoveShortcuts</td><td/><td>3200</td><td>RemoveShortcuts</td><td/></row>
		<row><td>ResolveSource</td><td>Not Installed</td><td>850</td><td>ResolveSource</td><td/></row>
		<row><td>ScheduleReboot</td><td>ISSCHEDULEREBOOT</td><td>6410</td><td>ScheduleReboot</td><td/></row>
		<row><td>SelfRegModules</td><td/><td>5600</td><td>SelfRegModules</td><td/></row>
		<row><td>SelfUnregModules</td><td/><td>2200</td><td>SelfUnregModules</td><td/></row>
		<row><td>SetARPINSTALLLOCATION</td><td/><td>1100</td><td>SetARPINSTALLLOCATION</td><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>VersionNT = 400</td><td>970</td><td/><td/></row>
		<row><td>SetODBCFolders</td><td/><td>1200</td><td>SetODBCFolders</td><td/></row>
		<row><td>StartServices</td><td>VersionNT</td><td>5900</td><td>StartServices</td><td/></row>
		<row><td>StopServices</td><td>VersionNT</td><td>1900</td><td>StopServices</td><td/></row>
		<row><td>UnpublishComponents</td><td/><td>1700</td><td>UnpublishComponents</td><td/></row>
		<row><td>UnpublishFeatures</td><td/><td>1800</td><td>UnpublishFeatures</td><td/></row>
		<row><td>UnregisterClassInfo</td><td/><td>2700</td><td>UnregisterClassInfo</td><td/></row>
		<row><td>UnregisterComPlus</td><td/><td>2100</td><td>UnregisterComPlus</td><td/></row>
		<row><td>UnregisterExtensionInfo</td><td/><td>2800</td><td>UnregisterExtensionInfo</td><td/></row>
		<row><td>UnregisterFonts</td><td/><td>2500</td><td>UnregisterFonts</td><td/></row>
		<row><td>UnregisterMIMEInfo</td><td/><td>3000</td><td>UnregisterMIMEInfo</td><td/></row>
		<row><td>UnregisterProgIdInfo</td><td/><td>2900</td><td>UnregisterProgIdInfo</td><td/></row>
		<row><td>UnregisterTypeLibraries</td><td/><td>2300</td><td>UnregisterTypeLibraries</td><td/></row>
		<row><td>ValidateProductID</td><td/><td>700</td><td>ValidateProductID</td><td/></row>
		<row><td>WriteEnvironmentStrings</td><td/><td>5200</td><td>WriteEnvironmentStrings</td><td/></row>
		<row><td>WriteIniValues</td><td/><td>5100</td><td>WriteIniValues</td><td/></row>
		<row><td>WriteRegistryValues</td><td/><td>5000</td><td>WriteRegistryValues</td><td/></row>
		<row><td>setAllUsersProfile2K</td><td>VersionNT &gt;= 500</td><td>980</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>VersionNT</td><td>960</td><td/><td/></row>
	</table>

	<table name="InstallShield">
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
		<row><td>ActiveLanguage</td><td>1033</td></row>
		<row><td>Comments</td><td/></row>
		<row><td>CurrentMedia</td><td dt:dt="bin.base64" md5="2799399ceb498ff35e6de50cfb0b9ff6">
UgBlAGwAZQBhAHMAZQABAEUAeABwAHIAZQBzAHMA
			</td></row>
		<row><td>DefaultProductConfiguration</td><td>Express</td></row>
		<row><td>ISCompilerOption_CompileBeforeBuild</td><td>1</td></row>
		<row><td>ISCompilerOption_Debug</td><td>0</td></row>
		<row><td>ISCompilerOption_IncludePath</td><td/></row>
		<row><td>ISCompilerOption_LibraryPath</td><td/></row>
		<row><td>ISCompilerOption_MaxErrors</td><td>50</td></row>
		<row><td>ISCompilerOption_MaxWarnings</td><td>50</td></row>
		<row><td>ISCompilerOption_OutputPath</td><td>&lt;ISProjectDataFolder&gt;\Script Files</td></row>
		<row><td>ISCompilerOption_PreProcessor</td><td>_ISSCRIPT_NEW_STYLE_DLG_DEFS</td></row>
		<row><td>ISCompilerOption_WarningLevel</td><td>3</td></row>
		<row><td>ISCompilerOption_WarningsAsErrors</td><td>1</td></row>
		<row><td>ISTheme</td><td>InstallShield Blue.theme</td></row>
		<row><td>ISUSLock</td><td>{0A3D0299-D328-4706-98AF-6C828295A331}</td></row>
		<row><td>ISUSSignature</td><td>{BD8DA779-2FAF-482F-B7B9-FAC977CD40C9}</td></row>
		<row><td>ISVisitedViews</td><td>viewAssistant,viewProject,viewUI,viewISToday,viewObjects,viewDependencies,viewAppFiles,viewFeatureFiles,viewRelease,viewDesignPatches,viewInstallScriptStd,viewSystemSearch,viewCustomActions,viewSupportFiles,viewTextMessages,viewBillboards,viewAppV,viewSetupDesign,viewShortcuts,viewFileExtensions,viewSetupTypes,viewUpgradePaths,viewUpdateService,viewCE,viewXMLConfig,viewTextFiles,viewRealSetupDesign,viewEnvironmentVariables,viewIniFiles,viewRegistry</td></row>
		<row><td>Limited</td><td>1</td></row>
		<row><td>LockPermissionMode</td><td>1</td></row>
		<row><td>MsiExecCmdLineOptions</td><td/></row>
		<row><td>MsiLogFile</td><td/></row>
		<row><td>OnUpgrade</td><td>0</td></row>
		<row><td>Owner</td><td/></row>
		<row><td>PatchFamily</td><td>MyPatchFamily1</td></row>
		<row><td>PatchSequence</td><td>1.0.0</td></row>
		<row><td>SaveAsSchema</td><td/></row>
		<row><td>SccEnabled</td><td>0</td></row>
		<row><td>SccPath</td><td/></row>
		<row><td>SchemaVersion</td><td>771</td></row>
		<row><td>Type</td><td>MSIE</td></row>
	</table>

	<table name="InstallUISequence">
		<col key="yes" def="s72">Action</col>
		<col def="S255">Condition</col>
		<col def="I2">Sequence</col>
		<col def="S255">ISComments</col>
		<col def="I4">ISAttributes</col>
		<row><td>AppSearch</td><td/><td>400</td><td>AppSearch</td><td/></row>
		<row><td>CCPSearch</td><td>CCP_TEST</td><td>500</td><td>CCPSearch</td><td/></row>
		<row><td>CostFinalize</td><td/><td>1000</td><td>CostFinalize</td><td/></row>
		<row><td>CostInitialize</td><td/><td>800</td><td>CostInitialize</td><td/></row>
		<row><td>ExecuteAction</td><td/><td>1300</td><td>ExecuteAction</td><td/></row>
		<row><td>FileCost</td><td/><td>900</td><td>FileCost</td><td/></row>
		<row><td>FindRelatedProducts</td><td/><td>430</td><td>FindRelatedProducts</td><td/></row>
		<row><td>ISPreventDowngrade</td><td>ISFOUNDNEWERPRODUCTVERSION</td><td>450</td><td>ISPreventDowngrade</td><td/></row>
		<row><td>ISSetAllUsers</td><td>Not Installed</td><td>10</td><td/><td/></row>
		<row><td>InstallWelcome</td><td>Not Installed</td><td>1210</td><td>InstallWelcome</td><td/></row>
		<row><td>IsolateComponents</td><td/><td>950</td><td>IsolateComponents</td><td/></row>
		<row><td>LaunchConditions</td><td>Not Installed</td><td>410</td><td>LaunchConditions</td><td/></row>
		<row><td>MaintenanceWelcome</td><td>Installed And Not RESUME And Not Preselected And Not PATCH</td><td>1230</td><td>MaintenanceWelcome</td><td/></row>
		<row><td>MigrateFeatureStates</td><td/><td>1200</td><td>MigrateFeatureStates</td><td/></row>
		<row><td>PatchWelcome</td><td>Installed And PATCH And Not IS_MAJOR_UPGRADE</td><td>1205</td><td>Patch Panel</td><td/></row>
		<row><td>RMCCPSearch</td><td>Not CCP_SUCCESS And CCP_TEST</td><td>600</td><td>RMCCPSearch</td><td/></row>
		<row><td>ResolveSource</td><td>Not Installed</td><td>990</td><td>ResolveSource</td><td/></row>
		<row><td>SetAllUsersProfileNT</td><td>VersionNT = 400</td><td>970</td><td/><td/></row>
		<row><td>SetupCompleteError</td><td/><td>-3</td><td>SetupCompleteError</td><td/></row>
		<row><td>SetupCompleteSuccess</td><td/><td>-1</td><td>SetupCompleteSuccess</td><td/></row>
		<row><td>SetupInitialization</td><td/><td>420</td><td>SetupInitialization</td><td/></row>
		<row><td>SetupInterrupted</td><td/><td>-2</td><td>SetupInterrupted</td><td/></row>
		<row><td>SetupProgress</td><td/><td>1240</td><td>SetupProgress</td><td/></row>
		<row><td>SetupResume</td><td>Installed And (RESUME Or Preselected) And Not PATCH</td><td>1220</td><td>SetupResume</td><td/></row>
		<row><td>ValidateProductID</td><td/><td>700</td><td>ValidateProductID</td><td/></row>
		<row><td>setAllUsersProfile2K</td><td>VersionNT &gt;= 500</td><td>980</td><td/><td/></row>
		<row><td>setUserProfileNT</td><td>VersionNT</td><td>960</td><td/><td/></row>
	</table>

	<table name="IsolatedComponent">
		<col key="yes" def="s72">Component_Shared</col>
		<col key="yes" def="s72">Component_Application</col>
	</table>

	<table name="LaunchCondition">
		<col key="yes" def="s255">Condition</col>
		<col def="l255">Description</col>
		<row><td>(Not Version9X) And (Not VersionNT=400) And (Not VersionNT=500) And (Not VersionNT=502) And (Not (VersionNT=600 And Not (MsiNTProductType=1))) And (Not (VersionNT=601 And Not (MsiNTProductType=1)))</td><td>##IDPROP_EXPRESS_LAUNCH_CONDITION_OS##</td></row>
	</table>

	<table name="ListBox">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
	</table>

	<table name="ListView">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="L64">Text</col>
		<col def="S72">Binary_</col>
	</table>

	<table name="LockPermissions">
		<col key="yes" def="s72">LockObject</col>
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="S255">Domain</col>
		<col key="yes" def="s255">User</col>
		<col def="I4">Permission</col>
	</table>

	<table name="MIME">
		<col key="yes" def="s64">ContentType</col>
		<col def="s255">Extension_</col>
		<col def="S38">CLSID</col>
	</table>

	<table name="Media">
		<col key="yes" def="i2">DiskId</col>
		<col def="i2">LastSequence</col>
		<col def="L64">DiskPrompt</col>
		<col def="S255">Cabinet</col>
		<col def="S32">VolumeLabel</col>
		<col def="S32">Source</col>
	</table>

	<table name="MoveFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="L255">SourceName</col>
		<col def="L255">DestName</col>
		<col def="S72">SourceFolder</col>
		<col def="s72">DestFolder</col>
		<col def="i2">Options</col>
	</table>

	<table name="MsiAssembly">
		<col key="yes" def="s72">Component_</col>
		<col def="s38">Feature_</col>
		<col def="S72">File_Manifest</col>
		<col def="S72">File_Application</col>
		<col def="I2">Attributes</col>
	</table>

	<table name="MsiAssemblyName">
		<col key="yes" def="s72">Component_</col>
		<col key="yes" def="s255">Name</col>
		<col def="s255">Value</col>
	</table>

	<table name="MsiDigitalCertificate">
		<col key="yes" def="s72">DigitalCertificate</col>
		<col def="v0">CertData</col>
	</table>

	<table name="MsiDigitalSignature">
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="s72">SignObject</col>
		<col def="s72">DigitalCertificate_</col>
		<col def="V0">Hash</col>
	</table>

	<table name="MsiDriverPackages">
		<col key="yes" def="s72">Component</col>
		<col def="i4">Flags</col>
		<col def="I4">Sequence</col>
		<col def="S0">ReferenceComponents</col>
	</table>

	<table name="MsiEmbeddedChainer">
		<col key="yes" def="s72">MsiEmbeddedChainer</col>
		<col def="S255">Condition</col>
		<col def="S255">CommandLine</col>
		<col def="s72">Source</col>
		<col def="I4">Type</col>
	</table>

	<table name="MsiEmbeddedUI">
		<col key="yes" def="s72">MsiEmbeddedUI</col>
		<col def="s255">FileName</col>
		<col def="i2">Attributes</col>
		<col def="I4">MessageFilter</col>
		<col def="V0">Data</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="MsiFileHash">
		<col key="yes" def="s72">File_</col>
		<col def="i2">Options</col>
		<col def="i4">HashPart1</col>
		<col def="i4">HashPart2</col>
		<col def="i4">HashPart3</col>
		<col def="i4">HashPart4</col>
	</table>

	<table name="MsiLockPermissionsEx">
		<col key="yes" def="s72">MsiLockPermissionsEx</col>
		<col def="s72">LockObject</col>
		<col def="s32">Table</col>
		<col def="s0">SDDLText</col>
		<col def="S255">Condition</col>
	</table>

	<table name="MsiPackageCertificate">
		<col key="yes" def="s72">PackageCertificate</col>
		<col def="s72">DigitalCertificate_</col>
	</table>

	<table name="MsiPatchCertificate">
		<col key="yes" def="s72">PatchCertificate</col>
		<col def="s72">DigitalCertificate_</col>
	</table>

	<table name="MsiPatchMetadata">
		<col key="yes" def="s72">PatchConfiguration_</col>
		<col key="yes" def="S72">Company</col>
		<col key="yes" def="s72">Property</col>
		<col def="S0">Value</col>
	</table>

	<table name="MsiPatchOldAssemblyFile">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="S72">Assembly_</col>
	</table>

	<table name="MsiPatchOldAssemblyName">
		<col key="yes" def="s72">Assembly</col>
		<col key="yes" def="s255">Name</col>
		<col def="S255">Value</col>
	</table>

	<table name="MsiPatchSequence">
		<col key="yes" def="s72">PatchConfiguration_</col>
		<col key="yes" def="s0">PatchFamily</col>
		<col key="yes" def="S0">Target</col>
		<col def="s0">Sequence</col>
		<col def="i2">Supersede</col>
	</table>

	<table name="MsiServiceConfig">
		<col key="yes" def="s72">MsiServiceConfig</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="i4">ConfigType</col>
		<col def="S0">Argument</col>
		<col def="s72">Component_</col>
	</table>

	<table name="MsiServiceConfigFailureActions">
		<col key="yes" def="s72">MsiServiceConfigFailureActions</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="I4">ResetPeriod</col>
		<col def="L255">RebootMessage</col>
		<col def="L255">Command</col>
		<col def="S0">Actions</col>
		<col def="S0">DelayActions</col>
		<col def="s72">Component_</col>
	</table>

	<table name="MsiShortcutProperty">
		<col key="yes" def="s72">MsiShortcutProperty</col>
		<col def="s72">Shortcut_</col>
		<col def="s0">PropertyKey</col>
		<col def="s0">PropVariantValue</col>
	</table>

	<table name="ODBCAttribute">
		<col key="yes" def="s72">Driver_</col>
		<col key="yes" def="s40">Attribute</col>
		<col def="S255">Value</col>
	</table>

	<table name="ODBCDataSource">
		<col key="yes" def="s72">DataSource</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s255">DriverDescription</col>
		<col def="i2">Registration</col>
	</table>

	<table name="ODBCDriver">
		<col key="yes" def="s72">Driver</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s72">File_</col>
		<col def="S72">File_Setup</col>
	</table>

	<table name="ODBCSourceAttribute">
		<col key="yes" def="s72">DataSource_</col>
		<col key="yes" def="s32">Attribute</col>
		<col def="S255">Value</col>
	</table>

	<table name="ODBCTranslator">
		<col key="yes" def="s72">Translator</col>
		<col def="s72">Component_</col>
		<col def="s255">Description</col>
		<col def="s72">File_</col>
		<col def="S72">File_Setup</col>
	</table>

	<table name="Patch">
		<col key="yes" def="s72">File_</col>
		<col key="yes" def="i2">Sequence</col>
		<col def="i4">PatchSize</col>
		<col def="i2">Attributes</col>
		<col def="V0">Header</col>
		<col def="S38">StreamRef_</col>
		<col def="S255">ISBuildSourcePath</col>
	</table>

	<table name="PatchPackage">
		<col key="yes" def="s38">PatchId</col>
		<col def="i2">Media_</col>
	</table>

	<table name="ProgId">
		<col key="yes" def="s255">ProgId</col>
		<col def="S255">ProgId_Parent</col>
		<col def="S38">Class_</col>
		<col def="L255">Description</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="I4">ISAttributes</col>
	</table>

	<table name="Property">
		<col key="yes" def="s72">Property</col>
		<col def="L0">Value</col>
		<col def="S255">ISComments</col>
		<row><td>ALLUSERS</td><td>1</td><td/></row>
		<row><td>ARPCOMMENTS</td><td>##ID_STRING15##</td><td/></row>
		<row><td>ARPCONTACT</td><td>##ID_STRING5##</td><td/></row>
		<row><td>ARPHELPLINK</td><td>##ID_STRING4##</td><td/></row>
		<row><td>ARPHELPTELEPHONE</td><td>##ID_STRING6##</td><td/></row>
		<row><td>ARPINSTALLLOCATION</td><td/><td/></row>
		<row><td>ARPPRODUCTICON</td><td>ARPPRODUCTICON.exe</td><td/></row>
		<row><td>ARPSIZE</td><td/><td/></row>
		<row><td>ARPURLINFOABOUT</td><td>##ID_STRING2##</td><td/></row>
		<row><td>ARPURLUPDATEINFO</td><td>##ID_STRING7##</td><td/></row>
		<row><td>AgreeToLicense</td><td>No</td><td/></row>
		<row><td>ApplicationUsers</td><td>AllUsers</td><td/></row>
		<row><td>DWUSINTERVAL</td><td>30</td><td/></row>
		<row><td>DWUSLINK</td><td>CE8BC73FCEBBC76FCEACF0D8F94B978F3E8C908F795CA778CEABB7387E5CC728D9FBA7884EAC</td><td/></row>
		<row><td>DefaultUIFont</td><td>ExpressDefault</td><td/></row>
		<row><td>DialogCaption</td><td>InstallShield for Windows Installer</td><td/></row>
		<row><td>DiskPrompt</td><td>[1]</td><td/></row>
		<row><td>DiskSerial</td><td>1234-5678</td><td/></row>
		<row><td>DisplayNameCustom</td><td>##IDS__DisplayName_Custom##</td><td/></row>
		<row><td>DisplayNameMinimal</td><td>##IDS__DisplayName_Minimal##</td><td/></row>
		<row><td>DisplayNameTypical</td><td>##IDS__DisplayName_Typical##</td><td/></row>
		<row><td>Display_IsBitmapDlg</td><td>1</td><td/></row>
		<row><td>ErrorDialog</td><td>SetupError</td><td/></row>
		<row><td>INSTALLLEVEL</td><td>200</td><td/></row>
		<row><td>ISCHECKFORPRODUCTUPDATES</td><td>1</td><td/></row>
		<row><td>ISENABLEDWUSFINISHDIALOG</td><td/><td/></row>
		<row><td>ISSHOWMSILOG</td><td/><td/></row>
		<row><td>ISVROOT_PORT_NO</td><td>0</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_COST</td><td>##IDS_COMPLUS_PROGRESSTEXT_COST##</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_INSTALL</td><td>##IDS_COMPLUS_PROGRESSTEXT_INSTALL##</td><td/></row>
		<row><td>IS_COMPLUS_PROGRESSTEXT_UNINSTALL</td><td>##IDS_COMPLUS_PROGRESSTEXT_UNINSTALL##</td><td/></row>
		<row><td>IS_PREVENT_DOWNGRADE_EXIT</td><td>##IDS_PREVENT_DOWNGRADE_EXIT##</td><td/></row>
		<row><td>IS_PROGMSG_TEXTFILECHANGS_REPLACE</td><td>##IDS_PROGMSG_TEXTFILECHANGS_REPLACE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_COSTING</td><td>##IDS_PROGMSG_XML_COSTING##</td><td/></row>
		<row><td>IS_PROGMSG_XML_CREATE_FILE</td><td>##IDS_PROGMSG_XML_CREATE_FILE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_FILES</td><td>##IDS_PROGMSG_XML_FILES##</td><td/></row>
		<row><td>IS_PROGMSG_XML_REMOVE_FILE</td><td>##IDS_PROGMSG_XML_REMOVE_FILE##</td><td/></row>
		<row><td>IS_PROGMSG_XML_ROLLBACK_FILES</td><td>##IDS_PROGMSG_XML_ROLLBACK_FILES##</td><td/></row>
		<row><td>IS_PROGMSG_XML_UPDATE_FILE</td><td>##IDS_PROGMSG_XML_UPDATE_FILE##</td><td/></row>
		<row><td>IS_SQLSERVER_AUTHENTICATION</td><td>0</td><td/></row>
		<row><td>IS_SQLSERVER_DATABASE</td><td/><td/></row>
		<row><td>IS_SQLSERVER_PASSWORD</td><td/><td/></row>
		<row><td>IS_SQLSERVER_SERVER</td><td/><td/></row>
		<row><td>IS_SQLSERVER_USERNAME</td><td>sa</td><td/></row>
		<row><td>InstallChoice</td><td>AR</td><td/></row>
		<row><td>LAUNCHPROGRAM</td><td>1</td><td/></row>
		<row><td>LAUNCHREADME</td><td>1</td><td/></row>
		<row><td>Manufacturer</td><td>##COMPANY_NAME##</td><td/></row>
		<row><td>PIDKEY</td><td/><td/></row>
		<row><td>PIDTemplate</td><td>12345&lt;###-%%%%%%%&gt;@@@@@</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEAPPPOOL</td><td>##IDS_PROGMSG_IIS_CREATEAPPPOOL##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEAPPPOOLS</td><td>##IDS_PROGMSG_IIS_CREATEAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEVROOT</td><td>##IDS_PROGMSG_IIS_CREATEVROOT##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEVROOTS</td><td>##IDS_PROGMSG_IIS_CREATEVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSERVICEEXTENSION</td><td>##IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSION##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_CREATEWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSITE</td><td>##IDS_PROGMSG_IIS_CREATEWEBSITE##</td><td/></row>
		<row><td>PROGMSG_IIS_CREATEWEBSITES</td><td>##IDS_PROGMSG_IIS_CREATEWEBSITES##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACT</td><td>##IDS_PROGMSG_IIS_EXTRACT##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTDONE</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTDONEz</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_EXTRACTzDONE</td><td>##IDS_PROGMSG_IIS_EXTRACTDONE##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEAPPPOOL</td><td>##IDS_PROGMSG_IIS_REMOVEAPPPOOL##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEAPPPOOLS</td><td>##IDS_PROGMSG_IIS_REMOVEAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVESITE</td><td>##IDS_PROGMSG_IIS_REMOVESITE##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEVROOT</td><td>##IDS_PROGMSG_IIS_REMOVEVROOT##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEVROOTS</td><td>##IDS_PROGMSG_IIS_REMOVEVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSION##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>PROGMSG_IIS_REMOVEWEBSITES</td><td>##IDS_PROGMSG_IIS_REMOVEWEBSITES##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKAPPPOOLS</td><td>##IDS_PROGMSG_IIS_ROLLBACKAPPPOOLS##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKVROOTS</td><td>##IDS_PROGMSG_IIS_ROLLBACKVROOTS##</td><td/></row>
		<row><td>PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS</td><td>##IDS_PROGMSG_IIS_ROLLBACKWEBSERVICEEXTENSIONS##</td><td/></row>
		<row><td>ProductCode</td><td>{2AF6A123-A362-4E2B-B23A-D1E952C15A9C}</td><td/></row>
		<row><td>ProductName</td><td>PloobsEngine</td><td/></row>
		<row><td>ProductVersion</td><td>0.4.0</td><td/></row>
		<row><td>ProgressType0</td><td>install</td><td/></row>
		<row><td>ProgressType1</td><td>Installing</td><td/></row>
		<row><td>ProgressType2</td><td>installed</td><td/></row>
		<row><td>ProgressType3</td><td>installs</td><td/></row>
		<row><td>RebootYesNo</td><td>Yes</td><td/></row>
		<row><td>ReinstallFileVersion</td><td>o</td><td/></row>
		<row><td>ReinstallModeText</td><td>omus</td><td/></row>
		<row><td>ReinstallRepair</td><td>r</td><td/></row>
		<row><td>RestartManagerOption</td><td>CloseRestart</td><td/></row>
		<row><td>SERIALNUMBER</td><td/><td/></row>
		<row><td>SERIALNUMVALSUCCESSRETVAL</td><td>1</td><td/></row>
		<row><td>SecureCustomProperties</td><td>ISFOUNDNEWERPRODUCTVERSION;USERNAME;COMPANYNAME;ISX_SERIALNUM;SUPPORTDIR;SOFTWARE_WOW6432NODE_XNA_FRAMEWORK_V4.0</td><td/></row>
		<row><td>SelectedSetupType</td><td>##IDS__DisplayName_Typical##</td><td/></row>
		<row><td>SetupType</td><td>Typical</td><td/></row>
		<row><td>UpgradeCode</td><td>{A3C64CAF-37B3-4A46-BA1F-6F367E5D87A0}</td><td/></row>
		<row><td>_IsMaintenance</td><td>Change</td><td/></row>
		<row><td>_IsSetupTypeMin</td><td>Typical</td><td/></row>
	</table>

	<table name="PublishComponent">
		<col key="yes" def="s38">ComponentId</col>
		<col key="yes" def="s255">Qualifier</col>
		<col key="yes" def="s72">Component_</col>
		<col def="L0">AppData</col>
		<col def="s38">Feature_</col>
	</table>

	<table name="RadioButton">
		<col key="yes" def="s72">Property</col>
		<col key="yes" def="i2">Order</col>
		<col def="s64">Value</col>
		<col def="i2">X</col>
		<col def="i2">Y</col>
		<col def="i2">Width</col>
		<col def="i2">Height</col>
		<col def="L64">Text</col>
		<col def="L50">Help</col>
		<col def="I4">ISControlId</col>
		<row><td>AgreeToLicense</td><td>1</td><td>No</td><td>0</td><td>15</td><td>291</td><td>15</td><td>##IDS__AgreeToLicense_0##</td><td/><td/></row>
		<row><td>AgreeToLicense</td><td>2</td><td>Yes</td><td>0</td><td>0</td><td>291</td><td>15</td><td>##IDS__AgreeToLicense_1##</td><td/><td/></row>
		<row><td>ApplicationUsers</td><td>1</td><td>AllUsers</td><td>1</td><td>7</td><td>290</td><td>14</td><td>##IDS__IsRegisterUserDlg_Anyone##</td><td/><td/></row>
		<row><td>ApplicationUsers</td><td>2</td><td>OnlyCurrentUser</td><td>1</td><td>23</td><td>290</td><td>14</td><td>##IDS__IsRegisterUserDlg_OnlyMe##</td><td/><td/></row>
		<row><td>RestartManagerOption</td><td>1</td><td>CloseRestart</td><td>6</td><td>9</td><td>331</td><td>14</td><td>##IDS__IsMsiRMFilesInUse_CloseRestart##</td><td/><td/></row>
		<row><td>RestartManagerOption</td><td>2</td><td>Reboot</td><td>6</td><td>21</td><td>331</td><td>14</td><td>##IDS__IsMsiRMFilesInUse_RebootAfter##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>1</td><td>Change</td><td>0</td><td>0</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Modify##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>2</td><td>Reinstall</td><td>0</td><td>60</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Repair##</td><td/><td/></row>
		<row><td>_IsMaintenance</td><td>3</td><td>Remove</td><td>0</td><td>120</td><td>290</td><td>14</td><td>##IDS__IsMaintenanceDlg_Remove##</td><td/><td/></row>
		<row><td>_IsSetupTypeMin</td><td>1</td><td>Typical</td><td>1</td><td>6</td><td>264</td><td>14</td><td>##IDS__IsSetupTypeMinDlg_Typical##</td><td/><td/></row>
	</table>

	<table name="RegLocator">
		<col key="yes" def="s72">Signature_</col>
		<col def="i2">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="I2">Type</col>
		<row><td>NewSignature1</td><td>2</td><td>SOFTWARE\Wow6432Node\Microsoft\XNA\Framework\v4.0\Installed</td><td>1</td><td>2</td></row>
	</table>

	<table name="Registry">
		<col key="yes" def="s72">Registry</col>
		<col def="i2">Root</col>
		<col def="s255">Key</col>
		<col def="S255">Name</col>
		<col def="S0">Value</col>
		<col def="s72">Component_</col>
		<col def="I4">ISAttributes</col>
		<row><td>Registry1</td><td>2</td><td>Software\Wow6432Node\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx</td><td/><td/><td>ISX_DEFAULTCOMPONENT</td><td>1</td></row>
		<row><td>Registry2</td><td>2</td><td>Software\Ploobs\PloobsEngine</td><td>Location</td><td>[INSTALLDIR]</td><td>ISX_DEFAULTCOMPONENT</td><td>0</td></row>
		<row><td>Registry3</td><td>2</td><td>Software\Ploobs\PloobsEngine</td><td>Version</td><td>0.4.2</td><td>ISX_DEFAULTCOMPONENT</td><td>0</td></row>
	</table>

	<table name="RemoveFile">
		<col key="yes" def="s72">FileKey</col>
		<col def="s72">Component_</col>
		<col def="L255">FileName</col>
		<col def="s72">DirProperty</col>
		<col def="i2">InstallMode</col>
		<row><td>AdvancedDemos.exe</td><td>AdvancedDemos.exe</td><td/><td>ploobs_1_ploobsengine</td><td>2</td></row>
		<row><td>NewShortcut1</td><td>ISX_DEFAULTCOMPONENT</td><td/><td>ploobs_1_ploobsengine</td><td>2</td></row>
		<row><td>NewShortcut2</td><td>ISX_DEFAULTCOMPONENT</td><td/><td>ploobs_1_ploobsengine</td><td>2</td></row>
		<row><td>UNINST_Uninstall_PloobsEngine</td><td>NewComponent1</td><td/><td>ploobs_1_ploobsengine</td><td>2</td></row>
	</table>

	<table name="RemoveIniFile">
		<col key="yes" def="s72">RemoveIniFile</col>
		<col def="l255">FileName</col>
		<col def="S72">DirProperty</col>
		<col def="l96">Section</col>
		<col def="l128">Key</col>
		<col def="L255">Value</col>
		<col def="i2">Action</col>
		<col def="s72">Component_</col>
	</table>

	<table name="RemoveRegistry">
		<col key="yes" def="s72">RemoveRegistry</col>
		<col def="i2">Root</col>
		<col def="l255">Key</col>
		<col def="L255">Name</col>
		<col def="s72">Component_</col>
	</table>

	<table name="ReserveCost">
		<col key="yes" def="s72">ReserveKey</col>
		<col def="s72">Component_</col>
		<col def="S72">ReserveFolder</col>
		<col def="i4">ReserveLocal</col>
		<col def="i4">ReserveSource</col>
	</table>

	<table name="SFPCatalog">
		<col key="yes" def="s255">SFPCatalog</col>
		<col def="V0">Catalog</col>
		<col def="S0">Dependency</col>
	</table>

	<table name="SelfReg">
		<col key="yes" def="s72">File_</col>
		<col def="I2">Cost</col>
	</table>

	<table name="ServiceControl">
		<col key="yes" def="s72">ServiceControl</col>
		<col def="s255">Name</col>
		<col def="i2">Event</col>
		<col def="S255">Arguments</col>
		<col def="I2">Wait</col>
		<col def="s72">Component_</col>
	</table>

	<table name="ServiceInstall">
		<col key="yes" def="s72">ServiceInstall</col>
		<col def="s255">Name</col>
		<col def="L255">DisplayName</col>
		<col def="i4">ServiceType</col>
		<col def="i4">StartType</col>
		<col def="i4">ErrorControl</col>
		<col def="S255">LoadOrderGroup</col>
		<col def="S255">Dependencies</col>
		<col def="S255">StartName</col>
		<col def="S255">Password</col>
		<col def="S255">Arguments</col>
		<col def="s72">Component_</col>
		<col def="L255">Description</col>
	</table>

	<table name="Shortcut">
		<col key="yes" def="s72">Shortcut</col>
		<col def="s72">Directory_</col>
		<col def="l128">Name</col>
		<col def="s72">Component_</col>
		<col def="s255">Target</col>
		<col def="S255">Arguments</col>
		<col def="L255">Description</col>
		<col def="I2">Hotkey</col>
		<col def="S72">Icon_</col>
		<col def="I2">IconIndex</col>
		<col def="I2">ShowCmd</col>
		<col def="S72">WkDir</col>
		<col def="S255">DisplayResourceDLL</col>
		<col def="I2">DisplayResourceId</col>
		<col def="S255">DescriptionResourceDLL</col>
		<col def="I2">DescriptionResourceId</col>
		<col def="S255">ISComments</col>
		<col def="S255">ISShortcutName</col>
		<col def="I4">ISAttributes</col>
		<row><td>AdvancedDemos.exe</td><td>ploobs_1_ploobsengine</td><td>##IDS_SHORTCUT_DISPLAY_NAME10##</td><td>AdvancedDemos.exe</td><td>AlwaysInstall</td><td/><td/><td/><td>AdvancedDemos.exe_D769F19A128A4529992174320BA39633.exe</td><td>1</td><td>1</td><td>ADVDEMOSBIN</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>NewShortcut1</td><td>ploobs_1_ploobsengine</td><td>##ID_STRING9##</td><td>ISX_DEFAULTCOMPONENT</td><td>[DEMOSSOURCECODE]</td><td/><td/><td/><td/><td/><td>1</td><td/><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>NewShortcut11</td><td>DesktopFolder</td><td>##ID_STRING9##</td><td>ISX_DEFAULTCOMPONENT</td><td>[DEMOSSOURCECODE]</td><td/><td/><td/><td/><td/><td>1</td><td/><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>NewShortcut2</td><td>ploobs_1_ploobsengine</td><td>##ID_STRING10##</td><td>ISX_DEFAULTCOMPONENT</td><td>[HELP]</td><td/><td/><td/><td/><td/><td>1</td><td/><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>NewShortcut21</td><td>DesktopFolder</td><td>##ID_STRING10##</td><td>ISX_DEFAULTCOMPONENT</td><td>[HELP]</td><td/><td/><td/><td/><td/><td>1</td><td/><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>UNINST_Uninstall_PloobsEngine</td><td>ploobs_1_ploobsengine</td><td>UNINST|Uninstall PloobsEngine</td><td>NewComponent1</td><td>[SystemFolder]msiexec.exe</td><td>/x {2AF6A123-A362-4E2B-B23A-D1E952C15A9C}</td><td/><td/><td>UNINST_Uninstall_P_5ABF4EFCD1D2450DB61B10E01DE95B13.exe</td><td>0</td><td>1</td><td/><td/><td/><td/><td/><td/><td/><td/></row>
	</table>

	<table name="Signature">
		<col key="yes" def="s72">Signature</col>
		<col def="s255">FileName</col>
		<col def="S20">MinVersion</col>
		<col def="S20">MaxVersion</col>
		<col def="I4">MinSize</col>
		<col def="I4">MaxSize</col>
		<col def="I4">MinDate</col>
		<col def="I4">MaxDate</col>
		<col def="S255">Languages</col>
	</table>

	<table name="TextStyle">
		<col key="yes" def="s72">TextStyle</col>
		<col def="s32">FaceName</col>
		<col def="i2">Size</col>
		<col def="I4">Color</col>
		<col def="I2">StyleBits</col>
		<row><td>Arial8</td><td>Arial</td><td>8</td><td/><td/></row>
		<row><td>Arial9</td><td>Arial</td><td>9</td><td/><td/></row>
		<row><td>ArialBlue10</td><td>Arial</td><td>10</td><td>16711680</td><td/></row>
		<row><td>ArialBlueStrike10</td><td>Arial</td><td>10</td><td>16711680</td><td>8</td></row>
		<row><td>CourierNew8</td><td>Courier New</td><td>8</td><td/><td/></row>
		<row><td>CourierNew9</td><td>Courier New</td><td>9</td><td/><td/></row>
		<row><td>ExpressDefault</td><td>Tahoma</td><td>8</td><td/><td/></row>
		<row><td>MSGothic9</td><td>MS Gothic</td><td>9</td><td/><td/></row>
		<row><td>MSSGreySerif8</td><td>MS Sans Serif</td><td>8</td><td>8421504</td><td/></row>
		<row><td>MSSWhiteSerif8</td><td>Tahoma</td><td>8</td><td>16777215</td><td/></row>
		<row><td>MSSansBold8</td><td>Tahoma</td><td>8</td><td/><td>1</td></row>
		<row><td>MSSansSerif8</td><td>MS Sans Serif</td><td>8</td><td/><td/></row>
		<row><td>MSSansSerif9</td><td>MS Sans Serif</td><td>9</td><td/><td/></row>
		<row><td>Tahoma10</td><td>Tahoma</td><td>10</td><td/><td/></row>
		<row><td>Tahoma8</td><td>Tahoma</td><td>8</td><td/><td/></row>
		<row><td>Tahoma9</td><td>Tahoma</td><td>9</td><td/><td/></row>
		<row><td>TahomaBold10</td><td>Tahoma</td><td>10</td><td/><td>1</td></row>
		<row><td>TahomaBold8</td><td>Tahoma</td><td>8</td><td/><td>1</td></row>
		<row><td>Times8</td><td>Times New Roman</td><td>8</td><td/><td/></row>
		<row><td>Times9</td><td>Times New Roman</td><td>9</td><td/><td/></row>
		<row><td>TimesItalic12</td><td>Times New Roman</td><td>12</td><td/><td>2</td></row>
		<row><td>TimesItalicBlue10</td><td>Times New Roman</td><td>10</td><td>16711680</td><td>2</td></row>
		<row><td>TimesRed16</td><td>Times New Roman</td><td>16</td><td>255</td><td/></row>
		<row><td>VerdanaBold14</td><td>Verdana</td><td>13</td><td/><td>1</td></row>
	</table>

	<table name="TypeLib">
		<col key="yes" def="s38">LibID</col>
		<col key="yes" def="i2">Language</col>
		<col key="yes" def="s72">Component_</col>
		<col def="I4">Version</col>
		<col def="L128">Description</col>
		<col def="S72">Directory_</col>
		<col def="s38">Feature_</col>
		<col def="I4">Cost</col>
	</table>

	<table name="UIText">
		<col key="yes" def="s72">Key</col>
		<col def="L255">Text</col>
		<row><td>AbsentPath</td><td/></row>
		<row><td>GB</td><td>##IDS_UITEXT_GB##</td></row>
		<row><td>KB</td><td>##IDS_UITEXT_KB##</td></row>
		<row><td>MB</td><td>##IDS_UITEXT_MB##</td></row>
		<row><td>MenuAbsent</td><td>##IDS_UITEXT_FeatureNotAvailable##</td></row>
		<row><td>MenuAdvertise</td><td>##IDS_UITEXT_FeatureInstalledWhenRequired2##</td></row>
		<row><td>MenuAllCD</td><td>##IDS_UITEXT_FeatureInstalledCD##</td></row>
		<row><td>MenuAllLocal</td><td>##IDS_UITEXT_FeatureInstalledLocal##</td></row>
		<row><td>MenuAllNetwork</td><td>##IDS_UITEXT_FeatureInstalledNetwork##</td></row>
		<row><td>MenuCD</td><td>##IDS_UITEXT_FeatureInstalledCD2##</td></row>
		<row><td>MenuLocal</td><td>##IDS_UITEXT_FeatureInstalledLocal2##</td></row>
		<row><td>MenuNetwork</td><td>##IDS_UITEXT_FeatureInstalledNetwork2##</td></row>
		<row><td>NewFolder</td><td>##IDS_UITEXT_Folder##</td></row>
		<row><td>SelAbsentAbsent</td><td>##IDS_UITEXT_GB##</td></row>
		<row><td>SelAbsentAdvertise</td><td>##IDS_UITEXT_FeatureInstalledWhenRequired##</td></row>
		<row><td>SelAbsentCD</td><td>##IDS_UITEXT_FeatureOnCD##</td></row>
		<row><td>SelAbsentLocal</td><td>##IDS_UITEXT_FeatureLocal##</td></row>
		<row><td>SelAbsentNetwork</td><td>##IDS_UITEXT_FeatureNetwork##</td></row>
		<row><td>SelAdvertiseAbsent</td><td>##IDS_UITEXT_FeatureUnavailable##</td></row>
		<row><td>SelAdvertiseAdvertise</td><td>##IDS_UITEXT_FeatureInstalledRequired##</td></row>
		<row><td>SelAdvertiseCD</td><td>##IDS_UITEXT_FeatureOnCD2##</td></row>
		<row><td>SelAdvertiseLocal</td><td>##IDS_UITEXT_FeatureLocal2##</td></row>
		<row><td>SelAdvertiseNetwork</td><td>##IDS_UITEXT_FeatureNetwork2##</td></row>
		<row><td>SelCDAbsent</td><td>##IDS_UITEXT_FeatureWillBeUninstalled##</td></row>
		<row><td>SelCDAdvertise</td><td>##IDS_UITEXT_FeatureWasCD##</td></row>
		<row><td>SelCDCD</td><td>##IDS_UITEXT_FeatureRunFromCD##</td></row>
		<row><td>SelCDLocal</td><td>##IDS_UITEXT_FeatureWasCDLocal##</td></row>
		<row><td>SelChildCostNeg</td><td>##IDS_UITEXT_FeatureFreeSpace##</td></row>
		<row><td>SelChildCostPos</td><td>##IDS_UITEXT_FeatureRequiredSpace##</td></row>
		<row><td>SelCostPending</td><td>##IDS_UITEXT_CompilingFeaturesCost##</td></row>
		<row><td>SelLocalAbsent</td><td>##IDS_UITEXT_FeatureCompletelyRemoved##</td></row>
		<row><td>SelLocalAdvertise</td><td>##IDS_UITEXT_FeatureRemovedUnlessRequired##</td></row>
		<row><td>SelLocalCD</td><td>##IDS_UITEXT_FeatureRemovedCD##</td></row>
		<row><td>SelLocalLocal</td><td>##IDS_UITEXT_FeatureRemainLocal##</td></row>
		<row><td>SelLocalNetwork</td><td>##IDS_UITEXT_FeatureRemoveNetwork##</td></row>
		<row><td>SelNetworkAbsent</td><td>##IDS_UITEXT_FeatureUninstallNoNetwork##</td></row>
		<row><td>SelNetworkAdvertise</td><td>##IDS_UITEXT_FeatureWasOnNetworkInstalled##</td></row>
		<row><td>SelNetworkLocal</td><td>##IDS_UITEXT_FeatureWasOnNetworkLocal##</td></row>
		<row><td>SelNetworkNetwork</td><td>##IDS_UITEXT_FeatureContinueNetwork##</td></row>
		<row><td>SelParentCostNegNeg</td><td>##IDS_UITEXT_FeatureSpaceFree##</td></row>
		<row><td>SelParentCostNegPos</td><td>##IDS_UITEXT_FeatureSpaceFree2##</td></row>
		<row><td>SelParentCostPosNeg</td><td>##IDS_UITEXT_FeatureSpaceFree3##</td></row>
		<row><td>SelParentCostPosPos</td><td>##IDS_UITEXT_FeatureSpaceFree4##</td></row>
		<row><td>TimeRemaining</td><td>##IDS_UITEXT_TimeRemaining##</td></row>
		<row><td>VolumeCostAvailable</td><td>##IDS_UITEXT_Available##</td></row>
		<row><td>VolumeCostDifference</td><td>##IDS_UITEXT_Differences##</td></row>
		<row><td>VolumeCostRequired</td><td>##IDS_UITEXT_Required##</td></row>
		<row><td>VolumeCostSize</td><td>##IDS_UITEXT_DiskSize##</td></row>
		<row><td>VolumeCostVolume</td><td>##IDS_UITEXT_Volume##</td></row>
		<row><td>bytes</td><td>##IDS_UITEXT_Bytes##</td></row>
	</table>

	<table name="Upgrade">
		<col key="yes" def="s38">UpgradeCode</col>
		<col key="yes" def="S20">VersionMin</col>
		<col key="yes" def="S20">VersionMax</col>
		<col key="yes" def="S255">Language</col>
		<col key="yes" def="i4">Attributes</col>
		<col def="S255">Remove</col>
		<col def="s72">ActionProperty</col>
		<col def="S72">ISDisplayName</col>
		<row><td>{00000000-0000-0000-0000-000000000000}</td><td>***ALL_VERSIONS***</td><td></td><td></td><td>2</td><td/><td>ISFOUNDNEWERPRODUCTVERSION</td><td>ISPreventDowngrade</td></row>
	</table>

	<table name="Verb">
		<col key="yes" def="s255">Extension_</col>
		<col key="yes" def="s32">Verb</col>
		<col def="I2">Sequence</col>
		<col def="L255">Command</col>
		<col def="L255">Argument</col>
	</table>

	<table name="_Validation">
		<col key="yes" def="s32">Table</col>
		<col key="yes" def="s32">Column</col>
		<col def="s4">Nullable</col>
		<col def="I4">MinValue</col>
		<col def="I4">MaxValue</col>
		<col def="S255">KeyTable</col>
		<col def="I2">KeyColumn</col>
		<col def="S32">Category</col>
		<col def="S255">Set</col>
		<col def="S255">Description</col>
		<row><td>ActionText</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to be described.</td></row>
		<row><td>ActionText</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description displayed in progress dialog and log when action is executing.</td></row>
		<row><td>ActionText</td><td>Template</td><td>Y</td><td/><td/><td/><td/><td>Template</td><td/><td>Optional localized format template used to format action data records for display during action execution.</td></row>
		<row><td>AdminExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdminExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdminExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdminExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdminExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdminUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdminUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdminUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdminUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdminUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdvtExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdvtExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdvtExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AdvtUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>AdvtUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>AdvtUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>AdvtUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>AdvtUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>AppId</td><td>ActivateAtStorage</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td/></row>
		<row><td>AppId</td><td>AppId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td/></row>
		<row><td>AppId</td><td>DllSurrogate</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppId</td><td>LocalService</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppId</td><td>RemoteServerName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>AppId</td><td>RunAsInteractiveUser</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td/></row>
		<row><td>AppId</td><td>ServiceParameters</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>AppSearch</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The property associated with a Signature</td></row>
		<row><td>AppSearch</td><td>Signature_</td><td>N</td><td/><td/><td>ISXmlLocator;Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>BBControl</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>BBControl</td><td>BBControl</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the control. This name must be unique within a billboard, but can repeat on different billboard.</td></row>
		<row><td>BBControl</td><td>Billboard_</td><td>N</td><td/><td/><td>Billboard</td><td>1</td><td>Identifier</td><td/><td>External key to the Billboard table, name of the billboard.</td></row>
		<row><td>BBControl</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>BBControl</td><td>Type</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The type of the control.</td></row>
		<row><td>BBControl</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>BBControl</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>Billboard</td><td>Action</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of an action. The billboard is displayed during the progress messages received from this action.</td></row>
		<row><td>Billboard</td><td>Billboard</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the billboard.</td></row>
		<row><td>Billboard</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>An external key to the Feature Table. The billboard is shown only if this feature is being installed.</td></row>
		<row><td>Billboard</td><td>Ordering</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>A positive integer. If there is more than one billboard corresponding to an action they will be shown in the order defined by this column.</td></row>
		<row><td>Binary</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>Binary</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>Binary</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique key identifying the binary data.</td></row>
		<row><td>BindImage</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>The index into the File table. This must be an executable file.</td></row>
		<row><td>BindImage</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>Paths</td><td/><td>A list of ;  delimited paths that represent the paths to be searched for the import DLLS. The list is usually a list of properties each enclosed within square brackets [] .</td></row>
		<row><td>CCPSearch</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>CheckBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to the item.</td></row>
		<row><td>CheckBox</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with the item.</td></row>
		<row><td>Class</td><td>AppId_</td><td>Y</td><td/><td/><td>AppId</td><td>1</td><td>Guid</td><td/><td>Optional AppID containing DCOM information for associated application (string GUID).</td></row>
		<row><td>Class</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>optional argument for LocalServers.</td></row>
		<row><td>Class</td><td>Attributes</td><td>Y</td><td/><td>32767</td><td/><td/><td/><td/><td>Class registration attributes.</td></row>
		<row><td>Class</td><td>CLSID</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The CLSID of an OLE factory.</td></row>
		<row><td>Class</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>Class</td><td>Context</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The numeric server context for this server. CLSCTX_xxxx</td></row>
		<row><td>Class</td><td>DefInprocHandler</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td>1;2;3</td><td>Optional default inproc handler.  Only optionally provided if Context=CLSCTX_LOCAL_SERVER.  Typically "ole32.dll" or "mapi32.dll"</td></row>
		<row><td>Class</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description for the Class.</td></row>
		<row><td>Class</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the CLSID factory to be operational.</td></row>
		<row><td>Class</td><td>FileTypeMask</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Optional string containing information for the HKCRthis CLSID) key. If multiple patterns exist, they must be delimited by a semicolon, and numeric subkeys will be generated: 0,1,2...</td></row>
		<row><td>Class</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index.</td></row>
		<row><td>Class</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Optional foreign key into the Icon Table, specifying the icon file associated with this CLSID. Will be written under the DefaultIcon key.</td></row>
		<row><td>Class</td><td>ProgId_Default</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>Optional ProgId associated with this CLSID.</td></row>
		<row><td>ComboBox</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list.	The integers do not have to be consecutive.</td></row>
		<row><td>ComboBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same combobox.</td></row>
		<row><td>ComboBox</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ComboBox</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>CompLocator</td><td>ComponentId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID unique to this component, version, and language.</td></row>
		<row><td>CompLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>CompLocator</td><td>Type</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td>A boolean value that determines if the registry value is a filename or a directory location.</td></row>
		<row><td>Complus</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the ComPlus component.</td></row>
		<row><td>Complus</td><td>ExpType</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>ComPlus component attributes.</td></row>
		<row><td>Component</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Remote execution option, one of irsEnum</td></row>
		<row><td>Component</td><td>Component</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>Component</td><td>ComponentId</td><td>Y</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID unique to this component, version, and language.</td></row>
		<row><td>Component</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A conditional statement that will disable this component if the specified condition evaluates to the 'True' state. If a component is disabled, it will not be installed, regardless of the 'Action' state associated with the component.</td></row>
		<row><td>Component</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Required key of a Directory table record. This is actually a property name whose value contains the actual path, set either by the AppSearch action or with the default setting obtained from the Directory table.</td></row>
		<row><td>Component</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a component.</td></row>
		<row><td>Component</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsCommit</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsInstall</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsRollback</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISDotNetInstallerArgsUninstall</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Arguments passed to the key file of the component if if implements the .NET Installer class</td></row>
		<row><td>Component</td><td>ISRegFileToMergeAtBuild</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path and File name of a .REG file to merge into the component at build time.</td></row>
		<row><td>Component</td><td>ISScanAtBuildFile</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File used by the Dot Net scanner to populate dependant assemblies' File_Application field.</td></row>
		<row><td>Component</td><td>KeyPath</td><td>Y</td><td/><td/><td>File;ODBCDataSource;Registry</td><td>1</td><td>Identifier</td><td/><td>Either the primary key into the File table, Registry table, or ODBCDataSource table. This extract path is stored when the component is installed, and is used to detect the presence of the component and to return the path to it.</td></row>
		<row><td>Condition</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Expression evaluated to determine if Level in the Feature table is to change.</td></row>
		<row><td>Condition</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Reference to a Feature entry in Feature table.</td></row>
		<row><td>Condition</td><td>Level</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>New selection Level to set in Feature table if Condition evaluates to TRUE.</td></row>
		<row><td>Control</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>Control</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>External key to the Binary table.</td></row>
		<row><td>Control</td><td>Control</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the control. This name must be unique within a dialog, but can repeat on different dialogs.</td></row>
		<row><td>Control</td><td>Control_Next</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>The name of an other control on the same dialog. This link defines the tab order of the controls. The links have to form one or more cycles!</td></row>
		<row><td>Control</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>External key to the Dialog table, name of the dialog.</td></row>
		<row><td>Control</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>Help</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The help strings used with the button. The text is optional.</td></row>
		<row><td>Control</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to .rtf file for scrollable text control</td></row>
		<row><td>Control</td><td>ISControlId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A number used to represent the control ID of the Control, Used in Dialog export</td></row>
		<row><td>Control</td><td>ISWindowStyle</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies non-MSI window styles to be applied to this control.</td></row>
		<row><td>Control</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of a defined property to be linked to this control.</td></row>
		<row><td>Control</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>Control</td><td>Type</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The type of the control.</td></row>
		<row><td>Control</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>Control</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ControlCondition</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>Default;Disable;Enable;Hide;Show</td><td>The desired action to be taken on the specified control.</td></row>
		<row><td>ControlCondition</td><td>Condition</td><td>N</td><td/><td/><td/><td/><td>Condition</td><td/><td>A standard conditional statement that specifies under which conditions the action should be triggered.</td></row>
		<row><td>ControlCondition</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control.</td></row>
		<row><td>ControlCondition</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the dialog.</td></row>
		<row><td>ControlEvent</td><td>Argument</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A value to be used as a modifier when triggering a particular event.</td></row>
		<row><td>ControlEvent</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A standard conditional statement that specifies under which conditions an event should be triggered.</td></row>
		<row><td>ControlEvent</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control</td></row>
		<row><td>ControlEvent</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the dialog.</td></row>
		<row><td>ControlEvent</td><td>Event</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>An identifier that specifies the type of the event that should take place when the user interacts with control specified by the first two entries.</td></row>
		<row><td>ControlEvent</td><td>Ordering</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>An integer used to order several events tied to the same control. Can be left blank.</td></row>
		<row><td>CreateFolder</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table.</td></row>
		<row><td>CreateFolder</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Primary key, could be foreign key into the Directory table.</td></row>
		<row><td>CustomAction</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, name of action, normally appears in sequence table unless private use.</td></row>
		<row><td>CustomAction</td><td>ExtendedType</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The numeric custom action type info flags.</td></row>
		<row><td>CustomAction</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments for this custom action.</td></row>
		<row><td>CustomAction</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>CustomSource</td><td/><td>The table reference of the source of the code.</td></row>
		<row><td>CustomAction</td><td>Target</td><td>Y</td><td/><td/><td>ISDLLWrapper;ISInstallScriptAction</td><td>1</td><td>Formatted</td><td/><td>Excecution parameter, depends on the type of custom action</td></row>
		<row><td>CustomAction</td><td>Type</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>The numeric custom action type, consisting of source location, code type, entry, option flags.</td></row>
		<row><td>Dialog</td><td>Attributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this dialog.</td></row>
		<row><td>Dialog</td><td>Control_Cancel</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the cancel control. Hitting escape or clicking on the close icon on the dialog is equivalent to pushing this button.</td></row>
		<row><td>Dialog</td><td>Control_Default</td><td>Y</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the default control. Hitting return is equivalent to pushing this button.</td></row>
		<row><td>Dialog</td><td>Control_First</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Defines the control that has the focus when the dialog is created.</td></row>
		<row><td>Dialog</td><td>Dialog</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the dialog.</td></row>
		<row><td>Dialog</td><td>HCentering</td><td>N</td><td>0</td><td>100</td><td/><td/><td/><td/><td>Horizontal position of the dialog on a 0-100 scale. 0 means left end, 100 means right end of the screen, 50 center.</td></row>
		<row><td>Dialog</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the dialog.</td></row>
		<row><td>Dialog</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments for this dialog.</td></row>
		<row><td>Dialog</td><td>ISResourceId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A Number the Specifies the Dialog ID to be used in Dialog Export</td></row>
		<row><td>Dialog</td><td>ISWindowStyle</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies non-MSI window styles to be applied to this control. This is only used in Script Based Setups.</td></row>
		<row><td>Dialog</td><td>TextStyle_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign Key into TextStyle table, only used in Script Based Projects.</td></row>
		<row><td>Dialog</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A text string specifying the title to be displayed in the title bar of the dialog's window.</td></row>
		<row><td>Dialog</td><td>VCentering</td><td>N</td><td>0</td><td>100</td><td/><td/><td/><td/><td>Vertical position of the dialog on a 0-100 scale. 0 means top end, 100 means bottom end of the screen, 50 center.</td></row>
		<row><td>Dialog</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the dialog.</td></row>
		<row><td>Directory</td><td>DefaultDir</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The default sub-path under parent's path.</td></row>
		<row><td>Directory</td><td>Directory</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for directory entry, primary key. If a property by this name is defined, it contains the full path to the directory.</td></row>
		<row><td>Directory</td><td>Directory_Parent</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Reference to the entry in this table specifying the default parent directory. A record parented to itself or with a Null parent represents a root of the install tree.</td></row>
		<row><td>Directory</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3;4;5;6;7</td><td>This is used to store Installshield custom properties of a directory.  Currently the only one is Shortcut.</td></row>
		<row><td>Directory</td><td>ISDescription</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of folder</td></row>
		<row><td>Directory</td><td>ISFolderName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is used in Pro projects because the pro identifier used in the tree wasn't necessarily unique.</td></row>
		<row><td>DrLocator</td><td>Depth</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The depth below the path to which the Signature_ is recursively searched. If absent, the depth is assumed to be 0.</td></row>
		<row><td>DrLocator</td><td>Parent</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The parent file signature. It is also a foreign key in the Signature table. If null and the Path column does not expand to a full path, then all the fixed drives of the user system are searched using the Path.</td></row>
		<row><td>DrLocator</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>AnyPath</td><td/><td>The path on the user system. This is a either a subpath below the value of the Parent or a full path. The path may contain properties enclosed within [ ] that will be expanded.</td></row>
		<row><td>DrLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>DuplicateFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the duplicate file.</td></row>
		<row><td>DuplicateFile</td><td>DestFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full pathname to a destination folder.</td></row>
		<row><td>DuplicateFile</td><td>DestName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Filename to be given to the duplicate file.</td></row>
		<row><td>DuplicateFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular file entry</td></row>
		<row><td>DuplicateFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing the source file to be duplicated.</td></row>
		<row><td>Environment</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the environmental value.</td></row>
		<row><td>Environment</td><td>Environment</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for the environmental variable setting</td></row>
		<row><td>Environment</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the environmental value.</td></row>
		<row><td>Environment</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to set in the environmental settings.</td></row>
		<row><td>Error</td><td>Error</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer error number, obtained from header file IError(...) macros.</td></row>
		<row><td>Error</td><td>Message</td><td>Y</td><td/><td/><td/><td/><td>Template</td><td/><td>Error formatting template, obtained from user ed. or localizers.</td></row>
		<row><td>EventMapping</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The name of the control attribute, that is set when this event is received.</td></row>
		<row><td>EventMapping</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>A foreign key to the Control table, name of the control.</td></row>
		<row><td>EventMapping</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the Dialog table, name of the Dialog.</td></row>
		<row><td>EventMapping</td><td>Event</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>An identifier that specifies the type of the event that the control subscribes to.</td></row>
		<row><td>Extension</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>Extension</td><td>Extension</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The extension associated with the table row.</td></row>
		<row><td>Extension</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the CLSID factory to be operational.</td></row>
		<row><td>Extension</td><td>MIME_</td><td>Y</td><td/><td/><td>MIME</td><td>1</td><td>Text</td><td/><td>Optional Context identifier, typically "type/format" associated with the extension</td></row>
		<row><td>Extension</td><td>ProgId_</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>Optional ProgId associated with this extension.</td></row>
		<row><td>Feature</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2;4;5;6;8;9;10;16;17;18;20;21;22;24;25;26;32;33;34;36;37;38;48;49;50;52;53;54</td><td>Feature attributes</td></row>
		<row><td>Feature</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Longer descriptive text describing a visible feature item.</td></row>
		<row><td>Feature</td><td>Directory_</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>UpperCase</td><td/><td>The name of the Directory that can be configured by the UI. A non-null value will enable the browse button.</td></row>
		<row><td>Feature</td><td>Display</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Numeric sort order, used to force a specific display ordering.</td></row>
		<row><td>Feature</td><td>Feature</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular feature record.</td></row>
		<row><td>Feature</td><td>Feature_Parent</td><td>Y</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Optional key of a parent record in the same table. If the parent is not selected, then the record will not be installed. Null indicates a root item.</td></row>
		<row><td>Feature</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Comments</td></row>
		<row><td>Feature</td><td>ISFeatureCabName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Name of CAB used when compressing CABs by Feature. Used to override build generated name for CAB file.</td></row>
		<row><td>Feature</td><td>ISProFeatureName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the feature used by pro projects.  This doesn't have to be unique.</td></row>
		<row><td>Feature</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Release Flags that specify whether this  feature will be built in a particular release.</td></row>
		<row><td>Feature</td><td>Level</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The install level at which record will be initially selected. An install level of 0 will disable an item and prevent its display.</td></row>
		<row><td>Feature</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Short text identifying a visible feature item.</td></row>
		<row><td>FeatureComponents</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>FeatureComponents</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>File</td><td>Attributes</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer containing bit flags representing file attributes (with the decimal value of each bit position in parentheses)</td></row>
		<row><td>File</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file.</td></row>
		<row><td>File</td><td>File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token, must match identifier in cabinet.  For uncompressed files, this field is ignored.</td></row>
		<row><td>File</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>File name used for installation.  This may contain a "short name|long name" pair.  It may be just a long name, hence it cannot be of the Filename data type.</td></row>
		<row><td>File</td><td>FileSize</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>File</td><td>ISAttributes</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>This field contains the following attributes: UseSystemSettings(0x1)</td></row>
		<row><td>File</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>File</td><td>ISComponentSubFolder_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key referencing component subfolder containing this file.  Only for Pro.</td></row>
		<row><td>File</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>List of decimal language Ids, comma-separated if more than one.</td></row>
		<row><td>File</td><td>Sequence</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Sequence with respect to the media images; order must track cabinet order.</td></row>
		<row><td>File</td><td>Version</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Version</td><td/><td>Version string for versioned files;  Blank for unversioned files.</td></row>
		<row><td>FileSFPCatalog</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>File associated with the catalog</td></row>
		<row><td>FileSFPCatalog</td><td>SFPCatalog_</td><td>N</td><td/><td/><td>SFPCatalog</td><td>1</td><td>Text</td><td/><td>Catalog associated with the file</td></row>
		<row><td>Font</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, foreign key into File table referencing font file.</td></row>
		<row><td>Font</td><td>FontTitle</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Font name.</td></row>
		<row><td>ISAssistantTag</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISAssistantTag</td><td>Tag</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Color</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Duration</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Effect</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Font</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>ISBillboard</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Origin</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Sequence</td><td>N</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Style</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Target</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Title</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISBillBoard</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>AppName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>CompanyName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>DefDir</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>DeleteMedia</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>DesktopTargetDir</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>DeviceFile</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>IconIndex</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>IconPath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallNetCF</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallNetCF2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallSQLClient</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallSQLClient2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallSQLDev</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallSQLDev2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallSQLServer</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>InstallSQLServer2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>NoUninstall</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>PVKFile</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>PostXML</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>PreXML</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>RawDeviceFile</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>SPCFile</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEApp</td><td>SPCPwd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEDir</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEDir</td><td>DirKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEDir</td><td>DirParent</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEDir</td><td>DirValue</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>AdvancedOptions</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>CopyOption</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>Destination</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>FileOption</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>Platform</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>Processor</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFile</td><td>Source</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFileExt</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFileExt</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFileExt</td><td>ExtKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFileExt</td><td>Extension</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFileExt</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEFileExt</td><td>IconIndex</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>CEAppName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>CECabs</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>CEDesktopDir</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>CEIcoFile</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>CEIniFileKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>CEInstallKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>Component_</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEInstall</td><td>DeleteMedia</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEOtherAppCABs</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEOtherAppCABs</td><td>BuildSourcePath</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEOtherAppCABs</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERedist</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERedist</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERedist</td><td>Platforms</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Overwrite</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Platform</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Processor</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>RegKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Root</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCERegistry</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCESetupFile</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCESetupFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCESetupFile</td><td>Platform</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCESetupFile</td><td>Processor</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCESetupFile</td><td>SetupFileKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCESetupFile</td><td>Source</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>AppKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>Destination</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>DisplayName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>Platform</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>ShtCutKey</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>StartScreenIcon</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISCEShtCut</td><td>Target</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Display name for the chained package. Used only in the IDE.</td></row>
		<row><td>ISChainPackage</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>InstallCondition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>InstallProperties</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Options</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Order</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>Package</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>ProductCode</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackage</td><td>RemoveCondition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>RemoveProperties</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackage</td><td>SourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackageData</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>ISChainPackageData</td><td>File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>ISChainPackageData</td><td>FilePath</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>ISChainPackageData</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>ISChainPackageData</td><td>Options</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISChainPackageData</td><td>Package_</td><td>N</td><td/><td/><td>ISChainPackage</td><td>1</td><td>Identifier</td><td/><td/></row>
		<row><td>ISClrWrap</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into CustomAction table</td></row>
		<row><td>ISClrWrap</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Property associated with this Action</td></row>
		<row><td>ISClrWrap</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value associated with this Property</td></row>
		<row><td>ISComCatalogAttribute</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogAttribute</td><td>ItemName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The named attribute for a catalog object.</td></row>
		<row><td>ISComCatalogAttribute</td><td>ItemValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A value associated with the attribute defined in the ItemName column.</td></row>
		<row><td>ISComCatalogCollection</td><td>CollectionName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>A catalog collection name.</td></row>
		<row><td>ISComCatalogCollection</td><td>ISComCatalogCollection</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComCatalogCollection table.</td></row>
		<row><td>ISComCatalogCollection</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogCollectionObjects</td><td>ISComCatalogCollection_</td><td>N</td><td/><td/><td>ISComCatalogCollection</td><td>1</td><td>Identifier</td><td/><td>A unique key for the ISComCatalogCollection table.</td></row>
		<row><td>ISComCatalogCollectionObjects</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComCatalogObject</td><td>DisplayName</td><td>N</td><td/><td/><td/><td/><td/><td/><td>The display name of a catalog object.</td></row>
		<row><td>ISComCatalogObject</td><td>ISComCatalogObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplication</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table that a COM+ application belongs to.</td></row>
		<row><td>ISComPlusApplication</td><td>ComputerName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Computer name that a COM+ application belongs to.</td></row>
		<row><td>ISComPlusApplication</td><td>DepFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List of the dependent files.</td></row>
		<row><td>ISComPlusApplication</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>InstallShield custom attributes associated with a COM+ application.</td></row>
		<row><td>ISComPlusApplication</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>AlterDLL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Alternate filename of the COM+ application component. Will be used for a .NET serviced component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>CLSID</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>CLSID of the COM+ application component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>DLL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Filename of the COM+ application component.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComCatalogObject_</td><td>N</td><td/><td/><td>ISComCatalogObject</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComCatalogObject table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComPlusApplicationDLL</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusApplicationDLL</td><td>ProgId</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>ProgId of the COM+ application component.</td></row>
		<row><td>ISComPlusProxy</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table that a COM+ application proxy belongs to.</td></row>
		<row><td>ISComPlusProxy</td><td>DepFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List of the dependent files.</td></row>
		<row><td>ISComPlusProxy</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>InstallShield custom attributes associated with a COM+ application proxy.</td></row>
		<row><td>ISComPlusProxy</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table that a COM+ application proxy belongs to.</td></row>
		<row><td>ISComPlusProxy</td><td>ISComPlusProxy</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key for the ISComPlusProxy table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusProxyDepFile</td><td>ISPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the dependent file.</td></row>
		<row><td>ISComPlusProxyFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusProxyFile</td><td>ISComPlusApplicationDLL_</td><td>N</td><td/><td/><td>ISComPlusApplicationDLL</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>ISComPlusApplication_</td><td>N</td><td/><td/><td>ISComPlusApplication</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplication table.</td></row>
		<row><td>ISComPlusServerDepFile</td><td>ISPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the dependent file.</td></row>
		<row><td>ISComPlusServerFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table.</td></row>
		<row><td>ISComPlusServerFile</td><td>ISComPlusApplicationDLL_</td><td>N</td><td/><td/><td>ISComPlusApplicationDLL</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISComPlusApplicationDLL table.</td></row>
		<row><td>ISComponentExtended</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>ISComponentExtended</td><td>FTPLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>FTP Location</td></row>
		<row><td>ISComponentExtended</td><td>FilterProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property to set if you want to filter a component</td></row>
		<row><td>ISComponentExtended</td><td>HTTPLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HTTP Location</td></row>
		<row><td>ISComponentExtended</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Language</td></row>
		<row><td>ISComponentExtended</td><td>Miscellaneous</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Miscellaneous</td></row>
		<row><td>ISComponentExtended</td><td>OS</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>bitwise addition of OSs</td></row>
		<row><td>ISComponentExtended</td><td>Platforms</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>bitwise addition of Platforms.</td></row>
		<row><td>ISCustomActionReference</td><td>Action_</td><td>N</td><td/><td/><td>CustomAction</td><td>1</td><td>Identifier</td><td/><td>Foreign key into theICustomAction table.</td></row>
		<row><td>ISCustomActionReference</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Contents of the file speciifed in ISCAReferenceFilePath. This column is only used by MSI.</td></row>
		<row><td>ISCustomActionReference</td><td>FileType</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>file type of the file specified  ISCAReferenceFilePath. This column is only used by MSI.</td></row>
		<row><td>ISCustomActionReference</td><td>ISCAReferenceFilePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.  This column only exists in ISM.</td></row>
		<row><td>ISDIMDependency</td><td>ISDIMReference_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISDIMDependency table</td></row>
		<row><td>ISDIMDependency</td><td>RequiredBuildVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the build version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredMajorVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the major version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredMinorVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the minor version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredRevisionVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>the revision version identifying the required DIM</td></row>
		<row><td>ISDIMDependency</td><td>RequiredUUID</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>the UUID identifying the required DIM</td></row>
		<row><td>ISDIMReference</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISDIMReference</td><td>ISDIMReference</td><td>N</td><td/><td/><td>ISDIMDependency</td><td>1</td><td>Identifier</td><td/><td>This is the primary key to the ISDIMReference table</td></row>
		<row><td>ISDIMReferenceDependencies</td><td>ISDIMDependency_</td><td>N</td><td/><td/><td>ISDIMDependency</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMDependency table.</td></row>
		<row><td>ISDIMReferenceDependencies</td><td>ISDIMReference_Parent</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISDIMVariable</td><td>ISDIMReference_</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISDIMVariable</td><td>ISDIMVariable</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISDIMVariable table</td></row>
		<row><td>ISDIMVariable</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of a variable defined in the .dim file</td></row>
		<row><td>ISDIMVariable</td><td>NewValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>New value that you want to override with</td></row>
		<row><td>ISDIMVariable</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type of the variable. 0: Build Variable, 1: Runtime Variable</td></row>
		<row><td>ISDLLWrapper</td><td>EntryPoint</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the target column in the CustomAction table</td></row>
		<row><td>ISDLLWrapper</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This is column points to the source file for the DLLWrapper Custom Action</td></row>
		<row><td>ISDLLWrapper</td><td>Target</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The function signature</td></row>
		<row><td>ISDLLWrapper</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type</td></row>
		<row><td>ISDRMFile</td><td>File_</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File table.  A null value will cause a build warning.</td></row>
		<row><td>ISDRMFile</td><td>ISDRMFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for this item.</td></row>
		<row><td>ISDRMFile</td><td>ISDRMLicense_</td><td>Y</td><td/><td/><td>ISDRMLicense</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing License that packages this file.</td></row>
		<row><td>ISDRMFile</td><td>Shell</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text indicating the activation shell used at runtime.</td></row>
		<row><td>ISDRMFileAttribute</td><td>ISDRMFile_</td><td>N</td><td/><td/><td>ISDRMFile</td><td>1</td><td>Identifier</td><td/><td>Primary foreign key into ISDRMFile table.</td></row>
		<row><td>ISDRMFileAttribute</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the attribute</td></row>
		<row><td>ISDRMFileAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The value of the attribute</td></row>
		<row><td>ISDRMLicense</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Bitwise field used to specify binary attributes of this license.</td></row>
		<row><td>ISDRMLicense</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>An internal description of this license.</td></row>
		<row><td>ISDRMLicense</td><td>ISDRMLicense</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique key identifying the license record.</td></row>
		<row><td>ISDRMLicense</td><td>LicenseNumber</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The license number.</td></row>
		<row><td>ISDRMLicense</td><td>ProjectVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The version of the project that this license is tied to.</td></row>
		<row><td>ISDRMLicense</td><td>RequestCode</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The request code.</td></row>
		<row><td>ISDRMLicense</td><td>ResponseCode</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The response code.</td></row>
		<row><td>ISDependency</td><td>Exclude</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISDependency</td><td>ISDependency</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISDisk1File</td><td>Disk</td><td>Y</td><td/><td/><td/><td/><td/><td>-1;0;1</td><td>Used to differentiate between disk1(1), last disk(-1), and other(0).</td></row>
		<row><td>ISDisk1File</td><td>ISBuildSourcePath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of file to be copied to Disk1 folder</td></row>
		<row><td>ISDisk1File</td><td>ISDisk1File</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for ISDisk1File table</td></row>
		<row><td>ISDynamicFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file.</td></row>
		<row><td>ISDynamicFile</td><td>ExcludeFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Wildcards for excluded files.</td></row>
		<row><td>ISDynamicFile</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15</td><td>This is used to store Installshield custom properties of a dynamic filet.  Currently the only one is SelfRegister.</td></row>
		<row><td>ISDynamicFile</td><td>IncludeFiles</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Wildcards for included files.</td></row>
		<row><td>ISDynamicFile</td><td>IncludeFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Include flags.</td></row>
		<row><td>ISDynamicFile</td><td>SourceFolder</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISFeatureDIMReferences</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureDIMReferences</td><td>ISDIMReference_</td><td>N</td><td/><td/><td>ISDIMReference</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISDIMReference table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>Feature_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>Language</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModuleExcludes</td><td>ModuleID</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>ISMergeModule_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>1</td><td>Text</td><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureMergeModules</td><td>Language_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>2</td><td/><td/><td>Foreign key into ISMergeModule table.</td></row>
		<row><td>ISFeatureSetupPrerequisites</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISFeatureSetupPrerequisites</td><td>ISSetupPrerequisites_</td><td>N</td><td/><td/><td>ISSetupPrerequisites</td><td>1</td><td/><td/><td/></row>
		<row><td>ISFileManifests</td><td>File_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into File table.</td></row>
		<row><td>ISFileManifests</td><td>Manifest_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into File table.</td></row>
		<row><td>ISIISItem</td><td>Component_</td><td>Y</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key to Component table.</td></row>
		<row><td>ISIISItem</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localizable Item Name.</td></row>
		<row><td>ISIISItem</td><td>ISIISItem</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for each item.</td></row>
		<row><td>ISIISItem</td><td>ISIISItem_Parent</td><td>Y</td><td/><td/><td>ISIISItem</td><td>1</td><td>Identifier</td><td/><td>This record's parent record.</td></row>
		<row><td>ISIISItem</td><td>Type</td><td>N</td><td/><td/><td/><td/><td/><td/><td>IIS resource type.</td></row>
		<row><td>ISIISProperty</td><td>FriendlyName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS property name.</td></row>
		<row><td>ISIISProperty</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Flags.</td></row>
		<row><td>ISIISProperty</td><td>ISIISItem_</td><td>N</td><td/><td/><td>ISIISItem</td><td>1</td><td>Identifier</td><td/><td>Primary key for table, foreign key into ISIISItem.</td></row>
		<row><td>ISIISProperty</td><td>ISIISProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key for table.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property attributes.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataProp</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property ID.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property data type.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataUserType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>IIS property user data type.</td></row>
		<row><td>ISIISProperty</td><td>MetaDataValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS property value.</td></row>
		<row><td>ISIISProperty</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Order sequencing.</td></row>
		<row><td>ISIISProperty</td><td>Schema</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>IIS7 schema information.</td></row>
		<row><td>ISInstallScriptAction</td><td>EntryPoint</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the target column in the CustomAction table</td></row>
		<row><td>ISInstallScriptAction</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This is column points to the source file for the DLLWrapper Custom Action</td></row>
		<row><td>ISInstallScriptAction</td><td>Target</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The function signature</td></row>
		<row><td>ISInstallScriptAction</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Type</td></row>
		<row><td>ISLanguage</td><td>ISLanguage</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the language ID.</td></row>
		<row><td>ISLanguage</td><td>Included</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1</td><td>Specify whether this language should be included.</td></row>
		<row><td>ISLinkerLibrary</td><td>ISLinkerLibrary</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Unique identifier for the link library.</td></row>
		<row><td>ISLinkerLibrary</td><td>Library</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path of the object library (.obl file).</td></row>
		<row><td>ISLinkerLibrary</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Order of the Library</td></row>
		<row><td>ISLocalControl</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this control.</td></row>
		<row><td>ISLocalControl</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>External key to the Binary table.</td></row>
		<row><td>ISLocalControl</td><td>Control_</td><td>N</td><td/><td/><td>Control</td><td>2</td><td>Identifier</td><td/><td>Name of the control. This name must be unique within a dialog, but can repeat on different dialogs.</td></row>
		<row><td>ISLocalControl</td><td>Dialog_</td><td>N</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>External key to the Dialog table, name of the dialog.</td></row>
		<row><td>ISLocalControl</td><td>Height</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Height of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to .rtf file for scrollable text control</td></row>
		<row><td>ISLocalControl</td><td>ISLanguage_</td><td>N</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalControl</td><td>Width</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Width of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>X</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Horizontal coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalControl</td><td>Y</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Vertical coordinate of the upper left corner of the bounding rectangle of the control.</td></row>
		<row><td>ISLocalDialog</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A 32-bit word that specifies the attribute flags to be applied to this dialog.</td></row>
		<row><td>ISLocalDialog</td><td>Dialog_</td><td>Y</td><td/><td/><td>Dialog</td><td>1</td><td>Identifier</td><td/><td>Name of the dialog.</td></row>
		<row><td>ISLocalDialog</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Height of the bounding rectangle of the dialog.</td></row>
		<row><td>ISLocalDialog</td><td>ISLanguage_</td><td>Y</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalDialog</td><td>TextStyle_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign Key into TextStyle table, only used in Script Based Projects.</td></row>
		<row><td>ISLocalDialog</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Width of the bounding rectangle of the dialog.</td></row>
		<row><td>ISLocalRadioButton</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The height of the button.</td></row>
		<row><td>ISLocalRadioButton</td><td>ISLanguage_</td><td>N</td><td/><td/><td>ISLanguage</td><td>1</td><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISLocalRadioButton</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td>RadioButton</td><td>2</td><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ISLocalRadioButton</td><td>Property</td><td>N</td><td/><td/><td>RadioButton</td><td>1</td><td>Identifier</td><td/><td>A named property to be tied to this radio button. All the buttons tied to the same property become part of the same group.</td></row>
		<row><td>ISLocalRadioButton</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The width of the button.</td></row>
		<row><td>ISLocalRadioButton</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The horizontal coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>ISLocalRadioButton</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The vertical coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>ISLockPermissions</td><td>Attributes</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permissions attributes mask, 1==Deny access; 2==No inherit</td></row>
		<row><td>ISLockPermissions</td><td>Domain</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Domain name for user whose permissions are being set.</td></row>
		<row><td>ISLockPermissions</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into CreateFolder, Registry, or File table</td></row>
		<row><td>ISLockPermissions</td><td>Permission</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permission Access mask.</td></row>
		<row><td>ISLockPermissions</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>CreateFolder;File;Registry</td><td>Reference to another table name</td></row>
		<row><td>ISLockPermissions</td><td>User</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>User for permissions to be set. This can be a property, hardcoded named, or SID string</td></row>
		<row><td>ISLogicalDisk</td><td>Cabinet</td><td>Y</td><td/><td/><td/><td/><td>Cabinet</td><td/><td>If some or all of the files stored on the media are compressed in a cabinet, the name of that cabinet.</td></row>
		<row><td>ISLogicalDisk</td><td>DiskId</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Primary key, integer to determine sort order for table.</td></row>
		<row><td>ISLogicalDisk</td><td>DiskPrompt</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk name: the visible text actually printed on the disk.  This will be used to prompt the user when this disk needs to be inserted.</td></row>
		<row><td>ISLogicalDisk</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISLogicalDisk</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISLogicalDisk</td><td>LastSequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the last file for this media.</td></row>
		<row><td>ISLogicalDisk</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>Property</td><td/><td>The property defining the location of the cabinet file.</td></row>
		<row><td>ISLogicalDisk</td><td>VolumeLabel</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The label attributed to the volume.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>Feature_</td><td>Y</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table,</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties, like Compressed, etc.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISLogicalDisk_</td><td>N</td><td>1</td><td>32767</td><td>ISLogicalDisk</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the ISLogicalDisk table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISLogicalDiskFeatures</td><td>Sequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the file for this media.</td></row>
		<row><td>ISMergeModule</td><td>Destination</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Destination.</td></row>
		<row><td>ISMergeModule</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a merge module.</td></row>
		<row><td>ISMergeModule</td><td>ISMergeModule</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The GUID identifying the merge module.</td></row>
		<row><td>ISMergeModule</td><td>Language</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Default decimal language of module.</td></row>
		<row><td>ISMergeModule</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the merge module.</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Attributes (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ContextData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>ContextData  (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>DefaultValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>DefaultValue  (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>DisplayName (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Format</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Format (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>HelpKeyword</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HelpKeyword (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>HelpLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>HelpLocation (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ISMergeModule_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>1</td><td>Text</td><td/><td>The module signature, a foreign key into the ISMergeModule table</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Language_</td><td>N</td><td/><td/><td>ISMergeModule</td><td>2</td><td/><td/><td>Default decimal language of module.</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>ModuleConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Identifier, foreign key into ModuleConfiguration table (ModuleConfiguration.Name)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Type (from configurable merge module)</td></row>
		<row><td>ISMergeModuleCfgValues</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for this item.</td></row>
		<row><td>ISObject</td><td>Language</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObject</td><td>ObjectName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>IncludeInBuild</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Boolean, 0 for false non 0 for true</td></row>
		<row><td>ISObjectProperty</td><td>ObjectName</td><td>Y</td><td/><td/><td>ISObject</td><td>1</td><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISObjectProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>ISPalmApp</td><td>Component</td><td>N</td><td/><td/><td>Component</td><td>1</td><td/><td/><td/></row>
		<row><td>ISPalmApp</td><td>PalmApp</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISPalmAppFile</td><td>Destination</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISPalmAppFile</td><td>FileKey</td><td>N</td><td/><td/><td>File</td><td>1</td><td/><td/><td/></row>
		<row><td>ISPalmAppFile</td><td>PalmApp</td><td>N</td><td/><td/><td>ISPalmApp</td><td>1</td><td/><td/><td/></row>
		<row><td>ISPatchConfigImage</td><td>PatchConfiguration_</td><td>Y</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISPatchConfigurationTable</td></row>
		<row><td>ISPatchConfigImage</td><td>UpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISUpgradedImageTable</td></row>
		<row><td>ISPatchConfiguration</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>PatchConfiguration attributes</td></row>
		<row><td>ISPatchConfiguration</td><td>CanPCDiffer</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether Product Codes may differ</td></row>
		<row><td>ISPatchConfiguration</td><td>CanPVDiffer</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether the Major Product Version may differ</td></row>
		<row><td>ISPatchConfiguration</td><td>EnablePatchCache</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to Enable Patch cacheing</td></row>
		<row><td>ISPatchConfiguration</td><td>Flags</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Patching API Flags</td></row>
		<row><td>ISPatchConfiguration</td><td>IncludeWholeFiles</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to build a binary level patch</td></row>
		<row><td>ISPatchConfiguration</td><td>LeaveDecompressed</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to leave intermediate files devcompressed when finished</td></row>
		<row><td>ISPatchConfiguration</td><td>MinMsiVersion</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Minimum Required MSI Version</td></row>
		<row><td>ISPatchConfiguration</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the Patch Configuration</td></row>
		<row><td>ISPatchConfiguration</td><td>OptimizeForSize</td><td>N</td><td/><td/><td/><td/><td/><td/><td>This is determine whether to Optimize for large files</td></row>
		<row><td>ISPatchConfiguration</td><td>OutputPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Build Location</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchCacheDir</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Directory to recieve the Patch Cache information</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchGuid</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Unique Patch Identifier</td></row>
		<row><td>ISPatchConfiguration</td><td>PatchGuidsToReplace</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>List Of Patch Guids to unregister</td></row>
		<row><td>ISPatchConfiguration</td><td>TargetProductCodes</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>List Of target Product Codes</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>ISPatchConfiguration_</td><td>Y</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Name of the Patch Configuration</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the Patch Configuration Property value</td></row>
		<row><td>ISPatchConfigurationProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value of the Patch Configuration Property</td></row>
		<row><td>ISPatchExternalFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Filekey</td></row>
		<row><td>ISPatchExternalFile</td><td>FilePath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Filepath</td></row>
		<row><td>ISPatchExternalFile</td><td>ISUpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to the isupgraded image table</td></row>
		<row><td>ISPatchExternalFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Uniqu name to identify this record.</td></row>
		<row><td>ISPatchWholeFile</td><td>Component</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Component containing file key</td></row>
		<row><td>ISPatchWholeFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Key of file to be included as whole</td></row>
		<row><td>ISPatchWholeFile</td><td>UpgradedImage</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>Foreign key to ISUpgradedImage Table</td></row>
		<row><td>ISPathVariable</td><td>ISPathVariable</td><td>N</td><td/><td/><td/><td/><td/><td/><td>The name of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>TestValue</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The test value of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>Type</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;4;8</td><td>The type of the path variable.</td></row>
		<row><td>ISPathVariable</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The value of the path variable.</td></row>
		<row><td>ISProductConfiguration</td><td>GeneratePackageCode</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td>0;1</td><td>Indicates whether or not to generate a package code.</td></row>
		<row><td>ISProductConfiguration</td><td>ISProductConfiguration</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the product configuration.</td></row>
		<row><td>ISProductConfiguration</td><td>ProductConfigurationFlags</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Product configuration (release) flags.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>InstanceId</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Identifies the instance number of this instance. This value is stored in the Property InstanceId.</td></row>
		<row><td>ISProductConfigurationInstance</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Product Congiuration property name</td></row>
		<row><td>ISProductConfigurationInstance</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>ISProductConfigurationProperty</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISProductConfigurationProperty</td><td>Property</td><td>N</td><td/><td/><td>Property</td><td>1</td><td>Text</td><td/><td>Product Congiuration property name</td></row>
		<row><td>ISProductConfigurationProperty</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property. Never null or empty.</td></row>
		<row><td>ISRelease</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding boolean values for various release attributes.</td></row>
		<row><td>ISRelease</td><td>BuildLocation</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Build location.</td></row>
		<row><td>ISRelease</td><td>CDBrowser</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Demoshield browser location.</td></row>
		<row><td>ISRelease</td><td>DefaultLanguage</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Default language for setup.</td></row>
		<row><td>ISRelease</td><td>DigitalPVK</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing private key (.pvk) file.</td></row>
		<row><td>ISRelease</td><td>DigitalSPC</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing Software Publisher Certificate (.spc) file.</td></row>
		<row><td>ISRelease</td><td>DigitalURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital signing URL.</td></row>
		<row><td>ISRelease</td><td>DiskClusterSize</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Disk cluster size.</td></row>
		<row><td>ISRelease</td><td>DiskSize</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk size.</td></row>
		<row><td>ISRelease</td><td>DiskSizeUnit</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2</td><td>Disk size units (KB or MB).</td></row>
		<row><td>ISRelease</td><td>DiskSpanning</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;2</td><td>Disk spanning (automatic, enforce size, etc.).</td></row>
		<row><td>ISRelease</td><td>DotNetBuildConfiguration</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Build Configuration for .NET solutions.</td></row>
		<row><td>ISRelease</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISRelease</td><td>ISRelease</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISRelease</td><td>ISSetupPrerequisiteLocation</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2;3</td><td>Location the Setup Prerequisites will be placed in</td></row>
		<row><td>ISRelease</td><td>MediaLocation</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Media location on disk.</td></row>
		<row><td>ISRelease</td><td>MsiCommandLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command line passed to the msi package from setup.exe</td></row>
		<row><td>ISRelease</td><td>MsiSourceType</td><td>N</td><td>-1</td><td>4</td><td/><td/><td/><td/><td>MSI media source type.</td></row>
		<row><td>ISRelease</td><td>PackageName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Package name.</td></row>
		<row><td>ISRelease</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Password.</td></row>
		<row><td>ISRelease</td><td>Platforms</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Platforms supported (Intel, Alpha, etc.).</td></row>
		<row><td>ISRelease</td><td>ReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Release flags.</td></row>
		<row><td>ISRelease</td><td>ReleaseType</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;4</td><td>Release type (single, uncompressed, etc.).</td></row>
		<row><td>ISRelease</td><td>SupportedLanguagesData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Languages supported (for component filtering).</td></row>
		<row><td>ISRelease</td><td>SupportedLanguagesUI</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>UI languages supported.</td></row>
		<row><td>ISRelease</td><td>SupportedOSs</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Indicate which operating systmes are supported.</td></row>
		<row><td>ISRelease</td><td>SynchMsi</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>MSI file to synchronize file keys and other data with (patch-like functionality).</td></row>
		<row><td>ISRelease</td><td>Type</td><td>N</td><td>0</td><td>6</td><td/><td/><td/><td/><td>Release type (CDROM, Network, etc.).</td></row>
		<row><td>ISRelease</td><td>URLLocation</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Media location via URL.</td></row>
		<row><td>ISRelease</td><td>VersionCopyright</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Version stamp information.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISRelease table.</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>Property</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>AS Repository property name</td></row>
		<row><td>ISReleaseASPublishInfo</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>AS Repository property value</td></row>
		<row><td>ISReleaseExtended</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding boolean values for various release attributes.</td></row>
		<row><td>ISReleaseExtended</td><td>CertPassword</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Digital certificate password</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificateDBaseNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to cerificate database for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificateIdNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to cerificate ID for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DigitalCertificatePasswordNS</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Password for Netscape digital  signature</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetBaseLanguage</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Base Languge of .NET Redist</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetFxCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to DotNetFx.exe</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetLangPackCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to LangPack.exe</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetLangaugePacks</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>.NET Redist language packs to include</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetRedistLocation</td><td>Y</td><td>0</td><td>3</td><td/><td/><td/><td/><td>Location of .NET framework Redist (Web, SetupExe, Source, None)</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetRedistURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to .NET framework Redist</td></row>
		<row><td>ISReleaseExtended</td><td>DotNetVersion</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Version of .NET framework Redist (1.0, 1.1)</td></row>
		<row><td>ISReleaseExtended</td><td>EngineLocation</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Location of msi engine (Web, SetupExe...)</td></row>
		<row><td>ISReleaseExtended</td><td>ISEngineLocation</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Location of ISScript  engine (Web, SetupExe...)</td></row>
		<row><td>ISReleaseExtended</td><td>ISEngineURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to InstallShield scripting engine</td></row>
		<row><td>ISReleaseExtended</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleaseExtended</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISReleaseExtended</td><td>JSharpCmdLine</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Command Line to pass to vjredist.exe</td></row>
		<row><td>ISReleaseExtended</td><td>JSharpRedistLocation</td><td>Y</td><td>0</td><td>3</td><td/><td/><td/><td/><td>Location of J# framework Redist (Web, SetupExe, Source, None)</td></row>
		<row><td>ISReleaseExtended</td><td>MsiEngineVersion</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding selected MSI engine versions included in this release</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickCabName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File name of generated cabfile</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickHtmlName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>File name of generated html page</td></row>
		<row><td>ISReleaseExtended</td><td>OneClickTargetBrowser</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Target browser (IE, Netscape, both...)</td></row>
		<row><td>ISReleaseExtended</td><td>WebCabSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of the cabfile</td></row>
		<row><td>ISReleaseExtended</td><td>WebLocalCachePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Directory to cache downloaded package</td></row>
		<row><td>ISReleaseExtended</td><td>WebType</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>Type of web install (One Executable, Downloader...)</td></row>
		<row><td>ISReleaseExtended</td><td>WebURL</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to .msi package</td></row>
		<row><td>ISReleaseExtended</td><td>Win9xMsiUrl</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to Ansi MSI engine</td></row>
		<row><td>ISReleaseExtended</td><td>WinMsi30Url</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to MSI 3.0 engine</td></row>
		<row><td>ISReleaseExtended</td><td>WinNTMsiUrl</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>URL to Unicode MSI engine</td></row>
		<row><td>ISReleaseProperty</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISProductConfiguration table.</td></row>
		<row><td>ISReleaseProperty</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISRelease table.</td></row>
		<row><td>ISReleaseProperty</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property name</td></row>
		<row><td>ISReleaseProperty</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISReleasePublishInfo</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item description</td></row>
		<row><td>ISReleasePublishInfo</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item display name</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Bitfield holding various attributes</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td>ISProductConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key into the ISProductConfiguration table.</td></row>
		<row><td>ISReleasePublishInfo</td><td>ISRelease_</td><td>N</td><td/><td/><td>ISRelease</td><td>1</td><td>Text</td><td/><td>The name of the release.</td></row>
		<row><td>ISReleasePublishInfo</td><td>Publisher</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository item publisher</td></row>
		<row><td>ISReleasePublishInfo</td><td>Repository</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Repository which to  publish the built merge module</td></row>
		<row><td>ISSQLConnection</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Authentication</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>BatchSeparator</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>CmdTimeout</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Database</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ISSQLConnection</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLConnection record.</td></row>
		<row><td>ISSQLConnection</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Password</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ScriptVersion_Column</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>ScriptVersion_Table</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>Server</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnection</td><td>UserName</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLConnectionDBServer</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLConnectionDBServer record.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>ISSQLDBMetaData_</td><td>N</td><td/><td/><td>ISSQLDBMetaData</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLDBMetaData table.</td></row>
		<row><td>ISSQLConnectionDBServer</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLConnectionScript</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLConnectionScript</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLConnectionScript</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnAdditional</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnDatabase</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnDriver</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnNetLibrary</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnPassword</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnPort</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnServer</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnUserID</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoCxnWindowsSecurity</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>AdoDriverName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>CreateDbCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>CreateTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>DsnODBCName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ISSQLDBMetaData</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLDBMetaData record.</td></row>
		<row><td>ISSQLDBMetaData</td><td>InsertRecordCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>LocalInstanceNames</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>QueryDatabasesCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_Column</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_ColumnType</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>ScriptVersion_Table</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>SelectTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>SwitchDbCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestDatabaseCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestTableCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>TestTableCmd2</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionBeginToken</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionEndToken</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>VersionInfoCmd</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLDBMetaData</td><td>WinAuthentUserId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>ISSQLConnectionDBServer_</td><td>Y</td><td/><td/><td>ISSQLConnectionDBServer</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnectionDBServer table.</td></row>
		<row><td>ISSQLRequirement</td><td>ISSQLConnection_</td><td>N</td><td/><td/><td>ISSQLConnection</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLConnection table.</td></row>
		<row><td>ISSQLRequirement</td><td>ISSQLRequirement</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLRequirement record.</td></row>
		<row><td>ISSQLRequirement</td><td>MajorVersion</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLRequirement</td><td>ServicePackLevel</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ErrHandling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ErrNumber</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptError</td><td>ISSQLScriptFile_</td><td>Y</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table</td></row>
		<row><td>ISSQLScriptError</td><td>Message</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Custom end-user message. Reserved for future use.</td></row>
		<row><td>ISSQLScriptFile</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Comments</td></row>
		<row><td>ISSQLScriptFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the SQL script.</td></row>
		<row><td>ISSQLScriptFile</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>A conditional statement that will disable this script if the specified condition evaluates to the 'False' state. If a script is disabled, it will not be installed regardless of the 'Action' state associated with the component.</td></row>
		<row><td>ISSQLScriptFile</td><td>ErrorHandling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path, the category is of Text instead of Path because of potential use of path variables.</td></row>
		<row><td>ISSQLScriptFile</td><td>ISSQLScriptFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISSQLScriptFile table</td></row>
		<row><td>ISSQLScriptFile</td><td>InstallText</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Feedback end-user text at install</td></row>
		<row><td>ISSQLScriptFile</td><td>Scheduling</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptFile</td><td>UninstallText</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Feedback end-user text at Uninstall</td></row>
		<row><td>ISSQLScriptFile</td><td>Version</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Schema Version (####.#####.####)</td></row>
		<row><td>ISSQLScriptImport</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Authentication</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Database</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>ExcludeTables</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLScriptImport</td><td>IncludeTables</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>Server</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptImport</td><td>UserName</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>Attributes</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>ISSQLScriptFile_</td><td>N</td><td/><td/><td>ISSQLScriptFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSQLScriptFile table.</td></row>
		<row><td>ISSQLScriptReplace</td><td>ISSQLScriptReplace</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular ISSQLScriptReplace record.</td></row>
		<row><td>ISSQLScriptReplace</td><td>Replace</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSQLScriptReplace</td><td>Search</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISScriptFile</td><td>ISScriptFile</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the full path of the script file. The path portion may be expressed in path variable form.</td></row>
		<row><td>ISSelfReg</td><td>CmdLine</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSelfReg</td><td>Cost</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSelfReg</td><td>FileKey</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key to the file table</td></row>
		<row><td>ISSelfReg</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupFile</td><td>FileName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is the file name to use when streaming the file to the support files location</td></row>
		<row><td>ISSetupFile</td><td>ISSetupFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>This is the primary key to the ISSetupFile table</td></row>
		<row><td>ISSetupFile</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Four digit language identifier.  0 for Language Neutral</td></row>
		<row><td>ISSetupFile</td><td>Path</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Link to the source file on the build machine</td></row>
		<row><td>ISSetupFile</td><td>Splash</td><td>Y</td><td/><td/><td/><td/><td>Short</td><td/><td>Boolean value indication whether his setup file entry belongs in the Splasc Screen section</td></row>
		<row><td>ISSetupFile</td><td>Stream</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The bits to stream to the support location</td></row>
		<row><td>ISSetupPrerequisites</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupPrerequisites</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Release Flags that specify whether this prereq  will be included in a particular release.</td></row>
		<row><td>ISSetupPrerequisites</td><td>ISSetupLocation</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2</td><td/></row>
		<row><td>ISSetupPrerequisites</td><td>ISSetupPrerequisites</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupPrerequisites</td><td>Order</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISSetupType</td><td>Comments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>ISSetupType</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Longer descriptive text describing a visible feature item.</td></row>
		<row><td>ISSetupType</td><td>Display</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Numeric sort order, used to force a specific display ordering.</td></row>
		<row><td>ISSetupType</td><td>Display_Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>A string used to set the initial text contained within a control (if appropriate).</td></row>
		<row><td>ISSetupType</td><td>ISSetupType</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular feature record.</td></row>
		<row><td>ISSetupTypeFeatures</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>ISSetupTypeFeatures</td><td>ISSetupType_</td><td>N</td><td/><td/><td>ISSetupType</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISSetupType table.</td></row>
		<row><td>ISStorages</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Path to the file to stream into sub-storage</td></row>
		<row><td>ISStorages</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Name of the sub-storage key</td></row>
		<row><td>ISString</td><td>Comment</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Comment</td></row>
		<row><td>ISString</td><td>Encoded</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Encoding for multi-byte strings.</td></row>
		<row><td>ISString</td><td>ISLanguage_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is a foreign key to the ISLanguage table.</td></row>
		<row><td>ISString</td><td>ISString</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>String id.</td></row>
		<row><td>ISString</td><td>TimeStamp</td><td>Y</td><td/><td/><td/><td/><td>Time/Date</td><td/><td>Time Stamp. MSI's Time/Date column type is just an int, with bits packed in a certain order.</td></row>
		<row><td>ISString</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>real string value.</td></row>
		<row><td>ISTargetImage</td><td>Flags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>relative order of the target image</td></row>
		<row><td>ISTargetImage</td><td>IgnoreMissingFiles</td><td>N</td><td/><td/><td/><td/><td/><td/><td>If true, ignore missing source files when creating patch</td></row>
		<row><td>ISTargetImage</td><td>MsiPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to the target image</td></row>
		<row><td>ISTargetImage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the TargetImage</td></row>
		<row><td>ISTargetImage</td><td>Order</td><td>N</td><td/><td/><td/><td/><td/><td/><td>relative order of the target image</td></row>
		<row><td>ISTargetImage</td><td>UpgradedImage_</td><td>N</td><td/><td/><td>ISUpgradedImage</td><td>1</td><td>Text</td><td/><td>foreign key to the upgraded Image table</td></row>
		<row><td>ISUpgradeMsiItem</td><td>ISAttributes</td><td>N</td><td/><td/><td/><td/><td/><td>0;1</td><td/></row>
		<row><td>ISUpgradeMsiItem</td><td>ISReleaseFlags</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>ISUpgradeMsiItem</td><td>ObjectSetupPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The path to the setup you want to upgrade.</td></row>
		<row><td>ISUpgradeMsiItem</td><td>UpgradeItem</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the Upgrade Item.</td></row>
		<row><td>ISUpgradedImage</td><td>Family</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the image family</td></row>
		<row><td>ISUpgradedImage</td><td>MsiPath</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Path to the upgraded image</td></row>
		<row><td>ISUpgradedImage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the UpgradedImage</td></row>
		<row><td>ISVirtualDirectory</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Directory table.</td></row>
		<row><td>ISVirtualDirectory</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualDirectory</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File  table.</td></row>
		<row><td>ISVirtualFile</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualFile</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualPackage</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualPackage</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualRegistry</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualRegistry</td><td>Registry_</td><td>N</td><td/><td/><td>Registry</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Registry table.</td></row>
		<row><td>ISVirtualRegistry</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualRelease</td><td>ISProductConfiguration_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISProductConfiguration table.</td></row>
		<row><td>ISVirtualRelease</td><td>ISRelease_</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key into ISRelease table.</td></row>
		<row><td>ISVirtualRelease</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property name</td></row>
		<row><td>ISVirtualRelease</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISVirtualShortcut</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Property name</td></row>
		<row><td>ISVirtualShortcut</td><td>Shortcut_</td><td>N</td><td/><td/><td>Shortcut</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Shortcut table.</td></row>
		<row><td>ISVirtualShortcut</td><td>Value</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Property value</td></row>
		<row><td>ISXmlElement</td><td>Content</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Element contents</td></row>
		<row><td>ISXmlElement</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML element attributes</td></row>
		<row><td>ISXmlElement</td><td>ISXmlElement</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized, internal token for Xml element</td></row>
		<row><td>ISXmlElement</td><td>ISXmlElement_Parent</td><td>Y</td><td/><td/><td>ISXmlElement</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISXMLElement table.</td></row>
		<row><td>ISXmlElement</td><td>ISXmlFile_</td><td>N</td><td/><td/><td>ISXmlFile</td><td>1</td><td>Identifier</td><td/><td>Foreign key into XmlFile table.</td></row>
		<row><td>ISXmlElement</td><td>XPath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>XPath fragment including any operators</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML elementattib attributes</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISXmlElementAttrib</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized, internal token for Xml element attribute</td></row>
		<row><td>ISXmlElementAttrib</td><td>ISXmlElement_</td><td>N</td><td/><td/><td>ISXmlElement</td><td>1</td><td>Identifier</td><td/><td>Foreign key into ISXMLElement table.</td></row>
		<row><td>ISXmlElementAttrib</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized attribute name</td></row>
		<row><td>ISXmlElementAttrib</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized attribute value</td></row>
		<row><td>ISXmlFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>ISXmlFile</td><td>Directory</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Directory table.</td></row>
		<row><td>ISXmlFile</td><td>Encoding</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>XML File Encoding</td></row>
		<row><td>ISXmlFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized XML file name</td></row>
		<row><td>ISXmlFile</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td>Number</td><td/><td>Internal XML file attributes</td></row>
		<row><td>ISXmlFile</td><td>ISXmlFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized,internal token for Xml file</td></row>
		<row><td>ISXmlFile</td><td>SelectionNamespaces</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Selection namespaces</td></row>
		<row><td>ISXmlLocator</td><td>Attribute</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>The name of an attribute within the XML element.</td></row>
		<row><td>ISXmlLocator</td><td>Element</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>XPath query that will locate an element in an XML file.</td></row>
		<row><td>ISXmlLocator</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td>0;1;2</td><td/></row>
		<row><td>ISXmlLocator</td><td>Parent</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The parent file signature. It is also a foreign key in the Signature table.</td></row>
		<row><td>ISXmlLocator</td><td>Signature_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The Signature_ represents a unique file signature and is also the foreign key in the Signature,  RegLocator, IniLocator, ISXmlLocator, CompLocator and the DrLocator tables.</td></row>
		<row><td>Icon</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The binary icon data in PE (.DLL or .EXE) or icon (.ICO) format.</td></row>
		<row><td>Icon</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to the ICO or EXE file.</td></row>
		<row><td>Icon</td><td>ISIconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index to be extracted.</td></row>
		<row><td>Icon</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key. Name of the icon file.</td></row>
		<row><td>IniFile</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>0;1;3</td><td>The type of modification to be made, one of iifEnum</td></row>
		<row><td>IniFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the .INI value.</td></row>
		<row><td>IniFile</td><td>DirProperty</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the .INI file is.</td></row>
		<row><td>IniFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name in which to write the information</td></row>
		<row><td>IniFile</td><td>IniFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>IniFile</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file key below Section.</td></row>
		<row><td>IniFile</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file Section.</td></row>
		<row><td>IniFile</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to be written.</td></row>
		<row><td>IniLocator</td><td>Field</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The field in the .INI line. If Field is null or 0 the entire line is read.</td></row>
		<row><td>IniLocator</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name.</td></row>
		<row><td>IniLocator</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Key value (followed by an equals sign in INI file).</td></row>
		<row><td>IniLocator</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Section name within in file (within square brackets in INI file).</td></row>
		<row><td>IniLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table.</td></row>
		<row><td>IniLocator</td><td>Type</td><td>Y</td><td>0</td><td>2</td><td/><td/><td/><td/><td>An integer value that determines if the .INI value read is a filename or a directory location or to be used as is w/o interpretation.</td></row>
		<row><td>InstallExecuteSequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>InstallExecuteSequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>InstallExecuteSequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>InstallExecuteSequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>InstallExecuteSequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>InstallShield</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property, uppercase if settable by launcher or loader.</td></row>
		<row><td>InstallShield</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>InstallUISequence</td><td>Action</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of action to invoke, either in the engine or the handler DLL.</td></row>
		<row><td>InstallUISequence</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td>Optional expression which skips the action if evaluates to expFalse.If the expression syntax is invalid, the engine will terminate, returning iesBadActionData.</td></row>
		<row><td>InstallUISequence</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store MM Custom Action Types</td></row>
		<row><td>InstallUISequence</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Sequence.</td></row>
		<row><td>InstallUISequence</td><td>Sequence</td><td>Y</td><td>-4</td><td>32767</td><td/><td/><td/><td/><td>Number that determines the sort order in which the actions are to be executed.  Leave blank to suppress action.</td></row>
		<row><td>IsolatedComponent</td><td>Component_Application</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Key to Component table item for application</td></row>
		<row><td>IsolatedComponent</td><td>Component_Shared</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Key to Component table item to be isolated</td></row>
		<row><td>LaunchCondition</td><td>Condition</td><td>N</td><td/><td/><td/><td/><td>Condition</td><td/><td>Expression which must evaluate to TRUE in order for install to commence.</td></row>
		<row><td>LaunchCondition</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Localizable text to display when condition fails and install must abort.</td></row>
		<row><td>ListBox</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ListBox</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same listbox.</td></row>
		<row><td>ListBox</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ListBox</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>ListView</td><td>Binary_</td><td>Y</td><td/><td/><td>Binary</td><td>1</td><td>Identifier</td><td/><td>The name of the icon to be displayed with the icon. The binary information is looked up from the Binary Table.</td></row>
		<row><td>ListView</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>ListView</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this item. All the items tied to the same property become part of the same listview.</td></row>
		<row><td>ListView</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible text to be assigned to the item. Optional. If this entry or the entire column is missing, the text is the same as the value.</td></row>
		<row><td>ListView</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The value string associated with this item. Selecting the line will set the associated property to this value.</td></row>
		<row><td>LockPermissions</td><td>Domain</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Domain name for user whose permissions are being set. (usually a property)</td></row>
		<row><td>LockPermissions</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Registry or File table</td></row>
		<row><td>LockPermissions</td><td>Permission</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Permission Access mask.  Full Control = 268435456 (GENERIC_ALL = 0x10000000)</td></row>
		<row><td>LockPermissions</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>Directory;File;Registry</td><td>Reference to another table name</td></row>
		<row><td>LockPermissions</td><td>User</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>User for permissions to be set.  (usually a property)</td></row>
		<row><td>MIME</td><td>CLSID</td><td>Y</td><td/><td/><td>Class</td><td>1</td><td>Guid</td><td/><td>Optional associated CLSID.</td></row>
		<row><td>MIME</td><td>ContentType</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Primary key. Context identifier, typically "type/format".</td></row>
		<row><td>MIME</td><td>Extension_</td><td>N</td><td/><td/><td>Extension</td><td>1</td><td>Text</td><td/><td>Optional associated extension (without dot)</td></row>
		<row><td>Media</td><td>Cabinet</td><td>Y</td><td/><td/><td/><td/><td>Cabinet</td><td/><td>If some or all of the files stored on the media are compressed in a cabinet, the name of that cabinet.</td></row>
		<row><td>Media</td><td>DiskId</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>Primary key, integer to determine sort order for table.</td></row>
		<row><td>Media</td><td>DiskPrompt</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Disk name: the visible text actually printed on the disk.  This will be used to prompt the user when this disk needs to be inserted.</td></row>
		<row><td>Media</td><td>LastSequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>File sequence number for the last file for this media.</td></row>
		<row><td>Media</td><td>Source</td><td>Y</td><td/><td/><td/><td/><td>Property</td><td/><td>The property defining the location of the cabinet file.</td></row>
		<row><td>Media</td><td>VolumeLabel</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The label attributed to the volume.</td></row>
		<row><td>MoveFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>If this component is not "selected" for installation or removal, no action will be taken on the associated MoveFile entry</td></row>
		<row><td>MoveFile</td><td>DestFolder</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the destination directory</td></row>
		<row><td>MoveFile</td><td>DestName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name to be given to the original file after it is moved or copied.  If blank, the destination file will be given the same name as the source file</td></row>
		<row><td>MoveFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key that uniquely identifies a particular MoveFile record</td></row>
		<row><td>MoveFile</td><td>Options</td><td>N</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Integer value specifying the MoveFile operating mode, one of imfoEnum</td></row>
		<row><td>MoveFile</td><td>SourceFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the source directory</td></row>
		<row><td>MoveFile</td><td>SourceName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the source file(s) to be moved or copied.  Can contain the '*' or '?' wildcards.</td></row>
		<row><td>MsiAssembly</td><td>Attributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Assembly attributes</td></row>
		<row><td>MsiAssembly</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>MsiAssembly</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Feature table.</td></row>
		<row><td>MsiAssembly</td><td>File_Application</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into File table, denoting the application context for private assemblies. Null for global assemblies.</td></row>
		<row><td>MsiAssembly</td><td>File_Manifest</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the manifest file for the assembly.</td></row>
		<row><td>MsiAssemblyName</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into Component table.</td></row>
		<row><td>MsiAssemblyName</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name part of the name-value pairs for the assembly name.</td></row>
		<row><td>MsiAssemblyName</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The value part of the name-value pairs for the assembly name.</td></row>
		<row><td>MsiDigitalCertificate</td><td>CertData</td><td>N</td><td/><td/><td/><td/><td>Binary</td><td/><td>A certificate context blob for a signer certificate</td></row>
		<row><td>MsiDigitalCertificate</td><td>DigitalCertificate</td><td>N</td><td/><td/><td>MsiPackageCertificate</td><td>2</td><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiDigitalSignature</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td>MsiDigitalCertificate</td><td>1</td><td>Identifier</td><td/><td>Foreign key to MsiDigitalCertificate table identifying the signer certificate</td></row>
		<row><td>MsiDigitalSignature</td><td>Hash</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>The encoded hash blob from the digital signature</td></row>
		<row><td>MsiDigitalSignature</td><td>SignObject</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Foreign key to Media table</td></row>
		<row><td>MsiDigitalSignature</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Reference to another table name (only Media table is supported)</td></row>
		<row><td>MsiDriverPackages</td><td>Component</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Primary key used to identify a particular component record.</td></row>
		<row><td>MsiDriverPackages</td><td>Flags</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Driver package flags</td></row>
		<row><td>MsiDriverPackages</td><td>ReferenceComponents</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiDriverPackages</td><td>Sequence</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>Installation sequence number</td></row>
		<row><td>MsiEmbeddedChainer</td><td>CommandLine</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Condition</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>MsiEmbeddedChainer</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Source</td><td>N</td><td/><td/><td/><td/><td>CustomSource</td><td/><td/></row>
		<row><td>MsiEmbeddedChainer</td><td>Type</td><td>Y</td><td/><td/><td/><td/><td>Integer</td><td>2;18;50</td><td/></row>
		<row><td>MsiEmbeddedUI</td><td>Attributes</td><td>N</td><td>0</td><td>3</td><td/><td/><td>Integer</td><td/><td>Information about the data in the Data column.</td></row>
		<row><td>MsiEmbeddedUI</td><td>Data</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>This column contains binary information.</td></row>
		<row><td>MsiEmbeddedUI</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Filename</td><td/><td>The name of the file that receives the binary information in the Data column.</td></row>
		<row><td>MsiEmbeddedUI</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>MsiEmbeddedUI</td><td>MessageFilter</td><td>Y</td><td>0</td><td>234913791</td><td/><td/><td>Integer</td><td/><td>Specifies the types of messages that are sent to the user interface DLL. This column is only relevant for rows with the msidbEmbeddedUI attribute.</td></row>
		<row><td>MsiEmbeddedUI</td><td>MsiEmbeddedUI</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The primary key for the table.</td></row>
		<row><td>MsiFileHash</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, foreign key into File table referencing file with this hash</td></row>
		<row><td>MsiFileHash</td><td>HashPart1</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart2</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart3</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>HashPart4</td><td>N</td><td/><td/><td/><td/><td/><td/><td>Size of file in bytes (long integer).</td></row>
		<row><td>MsiFileHash</td><td>Options</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Various options and attributes for this hash.</td></row>
		<row><td>MsiLockPermissionsEx</td><td>Condition</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Expression which must evaluate to TRUE in order for this set of permissions to be applied</td></row>
		<row><td>MsiLockPermissionsEx</td><td>LockObject</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into Registry, File, CreateFolder, or ServiceInstall table</td></row>
		<row><td>MsiLockPermissionsEx</td><td>MsiLockPermissionsEx</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiLockPermissionsEx</td><td>SDDLText</td><td>N</td><td/><td/><td/><td/><td>FormattedSDDLText</td><td/><td>String to indicate permissions to be applied to the LockObject</td></row>
		<row><td>MsiLockPermissionsEx</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td>CreateFolder;File;Registry;ServiceInstall</td><td>Reference to another table name</td></row>
		<row><td>MsiPackageCertificate</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A foreign key to the digital certificate table</td></row>
		<row><td>MsiPackageCertificate</td><td>PackageCertificate</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiPatchCertificate</td><td>DigitalCertificate_</td><td>N</td><td/><td/><td>MsiDigitalCertificate</td><td>1</td><td>Identifier</td><td/><td>A foreign key to the digital certificate table</td></row>
		<row><td>MsiPatchCertificate</td><td>PatchCertificate</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique identifier for the row</td></row>
		<row><td>MsiPatchMetadata</td><td>Company</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Optional company name</td></row>
		<row><td>MsiPatchMetadata</td><td>PatchConfiguration_</td><td>N</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the ISPatchConfiguration table</td></row>
		<row><td>MsiPatchMetadata</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the metadata</td></row>
		<row><td>MsiPatchMetadata</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value of the metadata</td></row>
		<row><td>MsiPatchOldAssemblyFile</td><td>Assembly_</td><td>Y</td><td/><td/><td>MsiPatchOldAssemblyName</td><td>1</td><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyFile</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Assembly</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Name</td><td>N</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchOldAssemblyName</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td/><td/><td/></row>
		<row><td>MsiPatchSequence</td><td>PatchConfiguration_</td><td>N</td><td/><td/><td>ISPatchConfiguration</td><td>1</td><td>Text</td><td/><td>Foreign key to the patch configuration table</td></row>
		<row><td>MsiPatchSequence</td><td>PatchFamily</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the family to which this patch belongs</td></row>
		<row><td>MsiPatchSequence</td><td>Sequence</td><td>N</td><td/><td/><td/><td/><td>Version</td><td/><td>The version of this patch in this family</td></row>
		<row><td>MsiPatchSequence</td><td>Supersede</td><td>N</td><td/><td/><td/><td/><td>Integer</td><td/><td>Supersede</td></row>
		<row><td>MsiPatchSequence</td><td>Target</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Target product codes for this patch family</td></row>
		<row><td>MsiServiceConfig</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Argument(s) for service configuration. Value depends on the content of the ConfigType field</td></row>
		<row><td>MsiServiceConfig</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the configuration of the service</td></row>
		<row><td>MsiServiceConfig</td><td>ConfigType</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Service Configuration Option</td></row>
		<row><td>MsiServiceConfig</td><td>Event</td><td>N</td><td>0</td><td>7</td><td/><td/><td/><td/><td>Bit field:   0x1 = Install, 0x2 = Uninstall, 0x4 = Reinstall</td></row>
		<row><td>MsiServiceConfig</td><td>MsiServiceConfig</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>MsiServiceConfig</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Actions</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A list of integer actions separated by [~] delimiters: 0 = SC_ACTION_NONE, 1 = SC_ACTION_RESTART, 2 = SC_ACTION_REBOOT, 3 = SC_ACTION_RUN_COMMAND. Terminate with [~][~]</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Command</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Command line of the process to CreateProcess function to execute</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the configuration of the service</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>DelayActions</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A list of delays (time in milli-seconds), separated by [~] delmiters, to wait before taking the corresponding Action. Terminate with [~][~]</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Event</td><td>N</td><td>0</td><td>7</td><td/><td/><td/><td/><td>Bit field:   0x1 = Install, 0x2 = Uninstall, 0x4 = Reinstall</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>MsiServiceConfigFailureActions</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>RebootMessage</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Message to be broadcast to server users before rebooting</td></row>
		<row><td>MsiServiceConfigFailureActions</td><td>ResetPeriod</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Time in seconds after which to reset the failure count to zero. Leave blank if it should never be reset</td></row>
		<row><td>MsiShortcutProperty</td><td>MsiShortcutProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token</td></row>
		<row><td>MsiShortcutProperty</td><td>PropVariantValue</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>String representation of the value in the property</td></row>
		<row><td>MsiShortcutProperty</td><td>PropertyKey</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Canonical string representation of the Property Key being set</td></row>
		<row><td>MsiShortcutProperty</td><td>Shortcut_</td><td>N</td><td/><td/><td>Shortcut</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Shortcut table</td></row>
		<row><td>ODBCAttribute</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of ODBC driver attribute</td></row>
		<row><td>ODBCAttribute</td><td>Driver_</td><td>N</td><td/><td/><td>ODBCDriver</td><td>1</td><td>Identifier</td><td/><td>Reference to ODBC driver in ODBCDriver table</td></row>
		<row><td>ODBCAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for ODBC driver attribute</td></row>
		<row><td>ODBCDataSource</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCDataSource</td><td>DataSource</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for data source</td></row>
		<row><td>ODBCDataSource</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for data source</td></row>
		<row><td>ODBCDataSource</td><td>DriverDescription</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Reference to driver description, may be existing driver</td></row>
		<row><td>ODBCDataSource</td><td>Registration</td><td>N</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Registration option: 0=machine, 1=user, others t.b.d.</td></row>
		<row><td>ODBCDriver</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCDriver</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for driver, non-localized</td></row>
		<row><td>ODBCDriver</td><td>Driver</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for driver</td></row>
		<row><td>ODBCDriver</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Reference to key driver file</td></row>
		<row><td>ODBCDriver</td><td>File_Setup</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Optional reference to key driver setup DLL</td></row>
		<row><td>ODBCSourceAttribute</td><td>Attribute</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of ODBC data source attribute</td></row>
		<row><td>ODBCSourceAttribute</td><td>DataSource_</td><td>N</td><td/><td/><td>ODBCDataSource</td><td>1</td><td>Identifier</td><td/><td>Reference to ODBC data source in ODBCDataSource table</td></row>
		<row><td>ODBCSourceAttribute</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Value for ODBC data source attribute</td></row>
		<row><td>ODBCTranslator</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reference to associated component</td></row>
		<row><td>ODBCTranslator</td><td>Description</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>Text used as registered name for translator</td></row>
		<row><td>ODBCTranslator</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Reference to key translator file</td></row>
		<row><td>ODBCTranslator</td><td>File_Setup</td><td>Y</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Optional reference to key translator setup DLL</td></row>
		<row><td>ODBCTranslator</td><td>Translator</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized.internal token for translator</td></row>
		<row><td>Patch</td><td>Attributes</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Integer containing bit flags representing patch attributes</td></row>
		<row><td>Patch</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Primary key, non-localized token, foreign key to File table, must match identifier in cabinet.</td></row>
		<row><td>Patch</td><td>Header</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>Binary stream. The patch header, used for patch validation.</td></row>
		<row><td>Patch</td><td>ISBuildSourcePath</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Full path to patch header.</td></row>
		<row><td>Patch</td><td>PatchSize</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Size of patch in bytes (long integer).</td></row>
		<row><td>Patch</td><td>Sequence</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Primary key, sequence with respect to the media images; order must track cabinet order.</td></row>
		<row><td>Patch</td><td>StreamRef_</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>External key into the MsiPatchHeaders table specifying the row that contains the patch header stream.</td></row>
		<row><td>PatchPackage</td><td>Media_</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Foreign key to DiskId column of Media table. Indicates the disk containing the patch package.</td></row>
		<row><td>PatchPackage</td><td>PatchId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A unique string GUID representing this patch.</td></row>
		<row><td>ProgId</td><td>Class_</td><td>Y</td><td/><td/><td>Class</td><td>1</td><td>Guid</td><td/><td>The CLSID of an OLE factory corresponding to the ProgId.</td></row>
		<row><td>ProgId</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Localized description for the Program identifier.</td></row>
		<row><td>ProgId</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a component, like ExtractIcon, etc.</td></row>
		<row><td>ProgId</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>Optional icon index.</td></row>
		<row><td>ProgId</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Optional foreign key into the Icon Table, specifying the icon file associated with this ProgId. Will be written under the DefaultIcon key.</td></row>
		<row><td>ProgId</td><td>ProgId</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The Program Identifier. Primary key.</td></row>
		<row><td>ProgId</td><td>ProgId_Parent</td><td>Y</td><td/><td/><td>ProgId</td><td>1</td><td>Text</td><td/><td>The Parent Program Identifier. If specified, the ProgId column becomes a version independent prog id.</td></row>
		<row><td>Property</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>User Comments.</td></row>
		<row><td>Property</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property, uppercase if settable by launcher or loader.</td></row>
		<row><td>Property</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>String value for property.</td></row>
		<row><td>PublishComponent</td><td>AppData</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>This is localisable Application specific data that can be associated with a Qualified Component.</td></row>
		<row><td>PublishComponent</td><td>ComponentId</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>A string GUID that represents the component id that will be requested by the alien product.</td></row>
		<row><td>PublishComponent</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table.</td></row>
		<row><td>PublishComponent</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Feature table.</td></row>
		<row><td>PublishComponent</td><td>Qualifier</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>This is defined only when the ComponentId column is an Qualified Component Id. This is the Qualifier for ProvideComponentIndirect.</td></row>
		<row><td>RadioButton</td><td>Height</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The height of the button.</td></row>
		<row><td>RadioButton</td><td>Help</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The help strings used with the button. The text is optional.</td></row>
		<row><td>RadioButton</td><td>ISControlId</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>A number used to represent the control ID of the Control, Used in Dialog export</td></row>
		<row><td>RadioButton</td><td>Order</td><td>N</td><td>1</td><td>32767</td><td/><td/><td/><td/><td>A positive integer used to determine the ordering of the items within one list..The integers do not have to be consecutive.</td></row>
		<row><td>RadioButton</td><td>Property</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A named property to be tied to this radio button. All the buttons tied to the same property become part of the same group.</td></row>
		<row><td>RadioButton</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The visible title to be assigned to the radio button.</td></row>
		<row><td>RadioButton</td><td>Value</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value string associated with this button. Selecting the button will set the associated property to this value.</td></row>
		<row><td>RadioButton</td><td>Width</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The width of the button.</td></row>
		<row><td>RadioButton</td><td>X</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The horizontal coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>RadioButton</td><td>Y</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The vertical coordinate of the upper left corner of the bounding rectangle of the radio button.</td></row>
		<row><td>RegLocator</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>RegLocator</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>RegLocator</td><td>Root</td><td>N</td><td>0</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum.</td></row>
		<row><td>RegLocator</td><td>Signature_</td><td>N</td><td/><td/><td>Signature</td><td>1</td><td>Identifier</td><td/><td>The table key. The Signature_ represents a unique file signature and is also the foreign key in the Signature table. If the type is 0, the registry values refers a directory, and _Signature is not a foreign key.</td></row>
		<row><td>RegLocator</td><td>Type</td><td>Y</td><td>0</td><td>18</td><td/><td/><td/><td/><td>An integer value that determines if the registry value is a filename or a directory location or to be used as is w/o interpretation.</td></row>
		<row><td>Registry</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the installing of the registry value.</td></row>
		<row><td>Registry</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a registry item.  Currently the only one is Automatic.</td></row>
		<row><td>Registry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>Registry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>Registry</td><td>Registry</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>Registry</td><td>Root</td><td>N</td><td>-1</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum.</td></row>
		<row><td>Registry</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The registry value.</td></row>
		<row><td>RemoveFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key referencing Component that controls the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>DirProperty</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full pathname to the folder of the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>FileKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key used to identify a particular file entry</td></row>
		<row><td>RemoveFile</td><td>FileName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Name of the file to be removed.</td></row>
		<row><td>RemoveFile</td><td>InstallMode</td><td>N</td><td/><td/><td/><td/><td/><td>1;2;3</td><td>Installation option, one of iimEnum.</td></row>
		<row><td>RemoveIniFile</td><td>Action</td><td>N</td><td/><td/><td/><td/><td/><td>2;4</td><td>The type of modification to be made, one of iifEnum.</td></row>
		<row><td>RemoveIniFile</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the deletion of the .INI value.</td></row>
		<row><td>RemoveIniFile</td><td>DirProperty</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the .INI file is.</td></row>
		<row><td>RemoveIniFile</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The .INI file name in which to delete the information</td></row>
		<row><td>RemoveIniFile</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file key below Section.</td></row>
		<row><td>RemoveIniFile</td><td>RemoveIniFile</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>RemoveIniFile</td><td>Section</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The .INI file Section.</td></row>
		<row><td>RemoveIniFile</td><td>Value</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The value to be deleted. The value is required when Action is iifIniRemoveTag</td></row>
		<row><td>RemoveRegistry</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table referencing component that controls the deletion of the registry value.</td></row>
		<row><td>RemoveRegistry</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>RegPath</td><td/><td>The key for the registry value.</td></row>
		<row><td>RemoveRegistry</td><td>Name</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The registry value name.</td></row>
		<row><td>RemoveRegistry</td><td>RemoveRegistry</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>RemoveRegistry</td><td>Root</td><td>N</td><td>-1</td><td>3</td><td/><td/><td/><td/><td>The predefined root key for the registry value, one of rrkEnum</td></row>
		<row><td>ReserveCost</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Reserve a specified amount of space if this component is to be installed.</td></row>
		<row><td>ReserveCost</td><td>ReserveFolder</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of a property whose value is assumed to resolve to the full path to the destination directory</td></row>
		<row><td>ReserveCost</td><td>ReserveKey</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key that uniquely identifies a particular ReserveCost record</td></row>
		<row><td>ReserveCost</td><td>ReserveLocal</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Disk space to reserve if linked component is installed locally.</td></row>
		<row><td>ReserveCost</td><td>ReserveSource</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>Disk space to reserve if linked component is installed to run from the source location.</td></row>
		<row><td>SFPCatalog</td><td>Catalog</td><td>Y</td><td/><td/><td/><td/><td>Binary</td><td/><td>SFP Catalog</td></row>
		<row><td>SFPCatalog</td><td>Dependency</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Parent catalog - only used by SFP</td></row>
		<row><td>SFPCatalog</td><td>SFPCatalog</td><td>N</td><td/><td/><td/><td/><td>Filename</td><td/><td>File name for the catalog.</td></row>
		<row><td>SelfReg</td><td>Cost</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The cost of registering the module.</td></row>
		<row><td>SelfReg</td><td>File_</td><td>N</td><td/><td/><td>File</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the module that needs to be registered.</td></row>
		<row><td>ServiceControl</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Arguments for the service.  Separate by [~].</td></row>
		<row><td>ServiceControl</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the startup of the service</td></row>
		<row><td>ServiceControl</td><td>Event</td><td>N</td><td>0</td><td>187</td><td/><td/><td/><td/><td>Bit field:  Install:  0x1 = Start, 0x2 = Stop, 0x8 = Delete, Uninstall: 0x10 = Start, 0x20 = Stop, 0x80 = Delete</td></row>
		<row><td>ServiceControl</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Name of a service. /, \, comma and space are invalid</td></row>
		<row><td>ServiceControl</td><td>ServiceControl</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>ServiceControl</td><td>Wait</td><td>Y</td><td>0</td><td>1</td><td/><td/><td/><td/><td>Boolean for whether to wait for the service to fully start</td></row>
		<row><td>ServiceInstall</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Arguments to include in every start of the service, passed to WinMain</td></row>
		<row><td>ServiceInstall</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table that controls the startup of the service</td></row>
		<row><td>ServiceInstall</td><td>Dependencies</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Other services this depends on to start.  Separate by [~], and end with [~][~]</td></row>
		<row><td>ServiceInstall</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of service.</td></row>
		<row><td>ServiceInstall</td><td>DisplayName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>External Name of the Service</td></row>
		<row><td>ServiceInstall</td><td>ErrorControl</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Severity of error if service fails to start</td></row>
		<row><td>ServiceInstall</td><td>LoadOrderGroup</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>LoadOrderGroup</td></row>
		<row><td>ServiceInstall</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Internal Name of the Service</td></row>
		<row><td>ServiceInstall</td><td>Password</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>password to run service with.  (with StartName)</td></row>
		<row><td>ServiceInstall</td><td>ServiceInstall</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>ServiceInstall</td><td>ServiceType</td><td>N</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Type of the service</td></row>
		<row><td>ServiceInstall</td><td>StartName</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>User or object name to run service as</td></row>
		<row><td>ServiceInstall</td><td>StartType</td><td>N</td><td>0</td><td>4</td><td/><td/><td/><td/><td>Type of the service</td></row>
		<row><td>Shortcut</td><td>Arguments</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The command-line arguments for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Component table denoting the component whose selection gates the the shortcut creation/deletion.</td></row>
		<row><td>Shortcut</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The description for the shortcut.</td></row>
		<row><td>Shortcut</td><td>DescriptionResourceDLL</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This field contains a Formatted string value for the full path to the language neutral file that contains the MUI manifest.</td></row>
		<row><td>Shortcut</td><td>DescriptionResourceId</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The description name index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Directory_</td><td>N</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the Directory table denoting the directory where the shortcut file is created.</td></row>
		<row><td>Shortcut</td><td>DisplayResourceDLL</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>This field contains a Formatted string value for the full path to the language neutral file that contains the MUI manifest.</td></row>
		<row><td>Shortcut</td><td>DisplayResourceId</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The display name index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Hotkey</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The hotkey for the shortcut. It has the virtual-key code for the key in the low-order byte, and the modifier flags in the high-order byte.</td></row>
		<row><td>Shortcut</td><td>ISAttributes</td><td>Y</td><td/><td/><td/><td/><td/><td/><td>This is used to store Installshield custom properties of a shortcut.  Mainly used in pro project types.</td></row>
		<row><td>Shortcut</td><td>ISComments</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Author’s comments on this Shortcut.</td></row>
		<row><td>Shortcut</td><td>ISShortcutName</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>A non-unique name for the shortcut.  Mainly used by pro pro project types.</td></row>
		<row><td>Shortcut</td><td>IconIndex</td><td>Y</td><td>-32767</td><td>32767</td><td/><td/><td/><td/><td>The icon index for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Icon_</td><td>Y</td><td/><td/><td>Icon</td><td>1</td><td>Identifier</td><td/><td>Foreign key into the File table denoting the external icon file for the shortcut.</td></row>
		<row><td>Shortcut</td><td>Name</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the shortcut to be created.</td></row>
		<row><td>Shortcut</td><td>Shortcut</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Primary key, non-localized token.</td></row>
		<row><td>Shortcut</td><td>ShowCmd</td><td>Y</td><td/><td/><td/><td/><td/><td>1;3;7</td><td>The show command for the application window.The following values may be used.</td></row>
		<row><td>Shortcut</td><td>Target</td><td>N</td><td/><td/><td/><td/><td>Shortcut</td><td/><td>The shortcut target. This is usually a property that is expanded to a file or a folder that the shortcut points to.</td></row>
		<row><td>Shortcut</td><td>WkDir</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of property defining location of working directory.</td></row>
		<row><td>Signature</td><td>FileName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The name of the file. This may contain a "short name|long name" pair.</td></row>
		<row><td>Signature</td><td>Languages</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>The languages supported by the file.</td></row>
		<row><td>Signature</td><td>MaxDate</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The maximum creation date of the file.</td></row>
		<row><td>Signature</td><td>MaxSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The maximum size of the file.</td></row>
		<row><td>Signature</td><td>MaxVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The maximum version of the file.</td></row>
		<row><td>Signature</td><td>MinDate</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The minimum creation date of the file.</td></row>
		<row><td>Signature</td><td>MinSize</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The minimum size of the file.</td></row>
		<row><td>Signature</td><td>MinVersion</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The minimum version of the file.</td></row>
		<row><td>Signature</td><td>Signature</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>The table key. The Signature represents a unique file signature.</td></row>
		<row><td>TextStyle</td><td>Color</td><td>Y</td><td>0</td><td>16777215</td><td/><td/><td/><td/><td>A long integer indicating the color of the string in the RGB format (Red, Green, Blue each 0-255, RGB = R + 256*G + 256^2*B).</td></row>
		<row><td>TextStyle</td><td>FaceName</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>A string indicating the name of the font used. Required. The string must be at most 31 characters long.</td></row>
		<row><td>TextStyle</td><td>Size</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The size of the font used. This size is given in our units (1/12 of the system font height). Assuming that the system font is set to 12 point size, this is equivalent to the point size.</td></row>
		<row><td>TextStyle</td><td>StyleBits</td><td>Y</td><td>0</td><td>15</td><td/><td/><td/><td/><td>A combination of style bits.</td></row>
		<row><td>TextStyle</td><td>TextStyle</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of the style. The primary key of this table. This name is embedded in the texts to indicate a style change.</td></row>
		<row><td>TypeLib</td><td>Component_</td><td>N</td><td/><td/><td>Component</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Component Table, specifying the component for which to return a path when called through LocateComponent.</td></row>
		<row><td>TypeLib</td><td>Cost</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The cost associated with the registration of the typelib. This column is currently optional.</td></row>
		<row><td>TypeLib</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td/></row>
		<row><td>TypeLib</td><td>Directory_</td><td>Y</td><td/><td/><td>Directory</td><td>1</td><td>Identifier</td><td/><td>Optional. The foreign key into the Directory table denoting the path to the help file for the type library.</td></row>
		<row><td>TypeLib</td><td>Feature_</td><td>N</td><td/><td/><td>Feature</td><td>1</td><td>Identifier</td><td/><td>Required foreign key into the Feature Table, specifying the feature to validate or install in order for the type library to be operational.</td></row>
		<row><td>TypeLib</td><td>Language</td><td>N</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>The language of the library.</td></row>
		<row><td>TypeLib</td><td>LibID</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The GUID that represents the library.</td></row>
		<row><td>TypeLib</td><td>Version</td><td>Y</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The version of the library. The major version is in the upper 8 bits of the short integer. The minor version is in the lower 8 bits.</td></row>
		<row><td>UIText</td><td>Key</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>A unique key that identifies the particular string.</td></row>
		<row><td>UIText</td><td>Text</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The localized version of the string.</td></row>
		<row><td>Upgrade</td><td>ActionProperty</td><td>N</td><td/><td/><td/><td/><td>UpperCase</td><td/><td>The property to set when a product in this set is found.</td></row>
		<row><td>Upgrade</td><td>Attributes</td><td>N</td><td>0</td><td>2147483647</td><td/><td/><td/><td/><td>The attributes of this product set.</td></row>
		<row><td>Upgrade</td><td>ISDisplayName</td><td>Y</td><td/><td/><td>ISUpgradeMsiItem</td><td>1</td><td/><td/><td/></row>
		<row><td>Upgrade</td><td>Language</td><td>Y</td><td/><td/><td/><td/><td>Language</td><td/><td>A comma-separated list of languages for either products in this set or products not in this set.</td></row>
		<row><td>Upgrade</td><td>Remove</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The list of features to remove when uninstalling a product from this set.  The default is "ALL".</td></row>
		<row><td>Upgrade</td><td>UpgradeCode</td><td>N</td><td/><td/><td/><td/><td>Guid</td><td/><td>The UpgradeCode GUID belonging to the products in this set.</td></row>
		<row><td>Upgrade</td><td>VersionMax</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The maximum ProductVersion of the products in this set.  The set may or may not include products with this particular version.</td></row>
		<row><td>Upgrade</td><td>VersionMin</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>The minimum ProductVersion of the products in this set.  The set may or may not include products with this particular version.</td></row>
		<row><td>Verb</td><td>Argument</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>Optional value for the command arguments.</td></row>
		<row><td>Verb</td><td>Command</td><td>Y</td><td/><td/><td/><td/><td>Formatted</td><td/><td>The command text.</td></row>
		<row><td>Verb</td><td>Extension_</td><td>N</td><td/><td/><td>Extension</td><td>1</td><td>Text</td><td/><td>The extension associated with the table row.</td></row>
		<row><td>Verb</td><td>Sequence</td><td>Y</td><td>0</td><td>32767</td><td/><td/><td/><td/><td>Order within the verbs for a particular extension. Also used simply to specify the default verb.</td></row>
		<row><td>Verb</td><td>Verb</td><td>N</td><td/><td/><td/><td/><td>Text</td><td/><td>The verb for the command.</td></row>
		<row><td>_Validation</td><td>Category</td><td>Y</td><td/><td/><td/><td/><td/><td>"Text";"Formatted";"Template";"Condition";"Guid";"Path";"Version";"Language";"Identifier";"Binary";"UpperCase";"LowerCase";"Filename";"Paths";"AnyPath";"WildCardFilename";"RegPath";"KeyFormatted";"CustomSource";"Property";"Cabinet";"Shortcut";"URL";"DefaultDir"</td><td>String category</td></row>
		<row><td>_Validation</td><td>Column</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of column</td></row>
		<row><td>_Validation</td><td>Description</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Description of column</td></row>
		<row><td>_Validation</td><td>KeyColumn</td><td>Y</td><td>1</td><td>32</td><td/><td/><td/><td/><td>Column to which foreign key connects</td></row>
		<row><td>_Validation</td><td>KeyTable</td><td>Y</td><td/><td/><td/><td/><td>Identifier</td><td/><td>For foreign key, Name of table to which data must link</td></row>
		<row><td>_Validation</td><td>MaxValue</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Maximum value allowed</td></row>
		<row><td>_Validation</td><td>MinValue</td><td>Y</td><td>-2147483647</td><td>2147483647</td><td/><td/><td/><td/><td>Minimum value allowed</td></row>
		<row><td>_Validation</td><td>Nullable</td><td>N</td><td/><td/><td/><td/><td/><td>Y;N;@</td><td>Whether the column is nullable</td></row>
		<row><td>_Validation</td><td>Set</td><td>Y</td><td/><td/><td/><td/><td>Text</td><td/><td>Set of values that are permitted</td></row>
		<row><td>_Validation</td><td>Table</td><td>N</td><td/><td/><td/><td/><td>Identifier</td><td/><td>Name of table</td></row>
	</table>
</msi>
