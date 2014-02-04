Simple Mvvm Toolit VS 2013 Deploy ReadMe

This project creates a VSIX installer package which can be uploaded to the
Visual Studio Extensions Gallery: http://visualstudiogallery.msdn.microsoft.com.

It includes VS item and project templates, required NuGet packages, and a
template wizard for the Simple Mvvm Ria Services multi-project template.

Also included are two pkgdefs for installing CSharp and Xml snippets. However,
this restricts the VSIX to Visual Studio Professional edition or higher. The
extension is then defined as a "tool" in the extensions gallery.

In order to support the Visual Studio Express editions, it will be necessary
to generate a VSIX installer that does not include the snippet pkgdefs. Users 
of express editions will then need to install the snippets manually.
