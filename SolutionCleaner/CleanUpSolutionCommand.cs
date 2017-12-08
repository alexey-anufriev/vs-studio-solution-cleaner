using System;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace SolutionCleaner
{
    internal sealed class CleanUpSolutionCommand : CleanUpCommandBase
    {
        public static readonly Guid CommandSet = new Guid("d729e146-72da-4933-9826-9e1d873b22ca");

        private const string VsProjectKindSolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";

        private CleanUpSolutionCommand(Package package) : base(package, CommandSet)
        {
        }

        public static CleanUpSolutionCommand Instance 
        {
            get;
            private set;
        }

        public static void Initialize(Package package)
        {
            Instance = new CleanUpSolutionCommand(package);
        }

        protected override void MenuItemCallback(object sender, EventArgs e)
        {
            CleanupProjects(GetAllSolutionProjects().ToArray());
        }

        private List<Project> GetAllSolutionProjects()
        {
            Projects projects = this.Dte.Solution.Projects;
            List<Project> projectsList = new List<Project>();

            var item = projects.GetEnumerator();

            while (item.MoveNext())
            {
                var project = item.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == VsProjectKindSolutionFolder)
                {
                    projectsList.AddRange(GetSolutionFolderProjects(project));
                }
                else
                {
                    projectsList.Add(project);
                }
            }

            return projectsList;
        }

        private static IEnumerable<Project> GetSolutionFolderProjects(Project solutionFolder)
        {
            List<Project> projects = new List<Project>();

            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;

                if (subProject == null)
                {
                    continue;
                }

                if (subProject.Kind == VsProjectKindSolutionFolder)
                {
                    projects.AddRange(GetSolutionFolderProjects(subProject));
                }
                else
                {
                    projects.Add(subProject);
                }
            }

            return projects;
        }
    }
}
