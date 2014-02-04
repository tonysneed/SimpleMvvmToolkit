Simple Mvvm Extensions ReadMe

Instructions for updating the Simple Mvvm Visual Studio extension (VSIX).

1. Open Project Templates\Templates-ReadMe.txt and follow the instructions for updating the project templates.

2. Copy zip files from Output\Zip folder to corresponding foleer in:
   - Deploy\SimpleMvvmToolkit.VS2013.Deploy\ProjectTemplates
   - Create folders for appropriate project categories and sub-categories
   
3. Copy NuGet packages needed into the Packages folder:
   - Deploy\SimpleMvvmToolkit.VS2013.Deploy\ProjectTemplates
   
4. Open the VSIX VS solution: SimpleMvvmToolkit.VS2013.Deploy.sln
   - Show all files, then inlude added packages
     > Right-click and select Include in Project
     > Set Build Action to Content, set Include in VSIX property to True
   - Include new project template folders in the project
     > Set Build Action to Content, set Include in VSIX property to True
   - Include new item templates in the project
     > Set Build Action to Content, set Include in VSIX property to True
 
5. Open the Vsix Maniftest file to insert additional items in the VSIX installer
   - Update the Version field
   - On the Assets tab, select New
   - Select the Type (Item or Project Template)
   - For Source select File on File System
   - For Path select the item or project zip file
