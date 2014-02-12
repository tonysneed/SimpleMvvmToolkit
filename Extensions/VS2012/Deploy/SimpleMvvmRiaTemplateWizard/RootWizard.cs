using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using EnvDTE;
using EnvDTE80;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Web.Silverlight;

namespace SimpleMvvmRiaTemplateWizard
{
    // Root wizard used by root project vstemplate
    public class RootWizard : IWizard
    {
        // Fields
        private DTE2 _dte2;
        private Project _selectedSolutionFolder;

        public static Dictionary<string, string> GlobalDictionary =
            new Dictionary<string,string>();

        // Add global replacement parameters
        public void RunStarted(object automationObject, 
            Dictionary<string, string> replacementsDictionary, 
            WizardRunKind runKind, object[] customParams)
        {
            // Place "$safeclientprojectname$ in the global dictionary.
            // Copy from $safeprojectname$ passed in my root vstemplate
            GlobalDictionary["$safeclientprojectname$"] = replacementsDictionary["$safeprojectname$"];

            // Init fields
            _dte2 = (DTE2)automationObject;
            Array source = null;
            try
            {
                source = (Array)_dte2.ActiveSolutionProjects;
            }
            catch (COMException)
            {
            }
            Project project = (source == null) ? null : source.OfType<Project>().FirstOrDefault<Project>();
            if ((project != null) && (project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}"))
            {
                _selectedSolutionFolder = project;
            }
        }

        // Add Silverlight app to the Web project and create test web pages
        public void RunFinished()
        {
            Project silverlightProject = GetSilverlightProject();
            Project webProject = GetWebProject();
            if (webProject != null)
            {
                _dte2.Solution.SolutionBuild.StartupProjects = webProject.UniqueName;
                Properties properties = webProject.Properties;
                ProjectItem aspxTestPage = this.GetAspxTestPage(webProject);
                if (aspxTestPage != null)
                {
                    properties.Item("WebApplication.StartPageUrl").Value = aspxTestPage.Name;
                    properties.Item("WebApplication.DebugStartAction").Value = 1;
                }
                if (silverlightProject != null)
                {
                    IVsHierarchy hierarchy;
                    IVsHierarchy hierarchy2;
                    silverlightProject.Properties.Item("SilverlightProject.LinkedServerProject").Value = webProject.FullName;
                    var sp = _dte2 as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                    IVsSolution service = null;
                    using (var provider2 = new ServiceProvider(sp))
                    {
                        service = provider2.GetService(typeof(IVsSolution)) as IVsSolution;
                    }
                    if (((service.GetProjectOfUniqueName(webProject.UniqueName, out hierarchy) == 0) && (hierarchy != null)) && (service.GetProjectOfUniqueName(silverlightProject.UniqueName, out hierarchy2) == 0))
                    {
                        ((IVsSilverlightProjectConsumer)hierarchy).LinkToSilverlightProject("ClientBin", true, false, hierarchy2 as IVsSilverlightProject);
                    }
                }
            }
        }

        private Project GetSilverlightProject()
        {
            string projectName = GlobalDictionary["$safeclientprojectname$"];
            return GetProject(projectName);
        }
        private Project GetWebProject()
        {
            string clientProjectName = GlobalDictionary["$safeclientprojectname$"];
            return GetProject(clientProjectName + ".Web");
        }

        private Project GetProject(string projectName)
        {
            Project project = null;
            if (_selectedSolutionFolder != null)
            {
                project = GetProject(from p in this._selectedSolutionFolder.ProjectItems.Cast<ProjectItem>()
                                     where p.SubProject != null
                                     select p.SubProject, projectName);
            }
            return (project ?? GetProject(this._dte2.Solution.Projects.Cast<Project>(), projectName));
        }

        private static Project GetProject(IEnumerable<Project> projects, string projectName)
        {
            foreach (Project project in projects)
            {
                if (Path.GetFileNameWithoutExtension(project.FullName).Equals(projectName))
                {
                    return project;
                }
            }
            return null;
        }

        private ProjectItem GetAspxTestPage(Project project)
        {
            string testPage = GlobalDictionary["$safeclientprojectname$"] + "TestPage.aspx";
            var items = project.ProjectItems.Cast<ProjectItem>().ToList();
            return items.FirstOrDefault(item => item.Name.Equals(testPage, StringComparison.OrdinalIgnoreCase));
        }

        // This method is called before opening any item that 
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }
    }
}
