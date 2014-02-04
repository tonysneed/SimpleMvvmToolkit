using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet.VisualStudio;

namespace SimpleMvvmPortableTemplateWizard
{
    // Root wizard is used by root project vstemplate
    public class RootWizard : IWizard
    {
        // Dev env
        private DTE2 _dte2;

        // Use to communicate $saferootprojectname$ to ChildWizard
        public static Dictionary<string, string> GlobalDictionary =
            new Dictionary<string, string>();

        // Add global replacement parameters
        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            // Place "$saferootprojectname$ in the global dictionary.
            // Copy from $safeprojectname$ passed in my root vstemplate
            GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"];

            // Init fields
            _dte2 = (DTE2)automationObject;
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            // TODO: Add ReadMe file to solution folder
            //const string solutionFolderName = "Solution Items";
            //var solution = (Solution2)_dte2.Solution;
            //var solutionFolder = (SolutionFolder)solution.AddSolutionFolder(solutionFolderName).Object;
            //string dirName = Path.GetDirectoryName(solution.FileName);
            //string fileName = Path.Combine(dirName, "ReadMe.txt");
            //solutionFolder.AddFromTemplate(fileName, dirName, "ReadMe.txt");

            // Set startup project
            Project wpfProject = GetWpfProject();
            if (wpfProject != null)
            {
                _dte2.Solution.SolutionBuild.StartupProjects = wpfProject.UniqueName;
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private Project GetWpfProject()
        {
            string projectName = GlobalDictionary["$saferootprojectname$"] + ".Wpf";
            foreach (Project project in _dte2.Solution.Projects)
            {
                if (Path.GetFileNameWithoutExtension(project.FullName).Equals(projectName))
                {
                    return project;
                }
            }
            return null;
        }
    }
}
