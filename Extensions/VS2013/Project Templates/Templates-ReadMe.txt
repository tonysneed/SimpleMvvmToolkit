Simple Mvvm Templates ReadMe

Instructions for updating the Simple Mvvm project templates.
- Repeat these steps for each template
- After creating folder extracts, zip contents into Zip folder

1. Open the corresponding sample project for the template type
   - Samples\VS2013\Main
   
2. Open the folder containing the project template files
   - Project Templates\Output\Extract

3. Open packages.config and update to latest NuGet package.
   - Compare with the sample project to make sure it's correct
   
4. Open the csproj file in a text editor to update it
   - Update the SimpleMvvmToolkit references
   - Make sure to update both the Common and platform-specific references
     > Update the NuGet folder name to the latest version number
     > Also update the file version number if needed
   - Also update any other references added by the NuGet package
   
5. Open the MyTemplate.vstemplate file with a text editor
   - Update NuGet package version in WizardData element
   - Add or remove any NuGet packages as required
    
Instructions for creating a new Simple Mvvm project template.

1. Open the project in Visual Studio, then export project template
   - Paste the info and image paths from the TemplateInfo.txt file
   - Uncheck option to install template, but leave option to open folder
   
2. Open the zip file that was exported and extract the contents to a folder
   - Create a folder with the template name under Project Templates\Output\Extract
   
3. Edit the MyTemplate.vstemplate file
   - Set the Name field
   - Replace ProjectSubType element, add NumberOfParentCategoriesToRollUp
   - Add WizardExtension and WizardData elements
     > Add all required NuGet packages to WizardData element
     
Instructions for creating a multi-project template.

1. If needed, add a template wizard class lib project to the deployment solution
   - Add the NuGet.VisualStudio NuGet package
   - Add root and child wizard classes

2. Edit the RootTemplate.vstemplate file
   - Add project template links
   - Add root wizard extension element
   
3. Add child template folders
   - Edit vstemplate file
     > Add child wizard extension element
     > Add wizard data element with NuGet packages
   - Edit csproj file with text editor
     > Set AssemblyName to $safeprojectname$
     > Add ..\ to HintPath for package references
     > Edit project references to use $saferootprojectname$
   - Search for other places where $safeprojectname$ and $saferootprojectname$
     should be used (for example, xaml namespaces)
     
 