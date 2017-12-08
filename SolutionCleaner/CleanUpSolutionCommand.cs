using System;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace SolutionCleaner
{
    internal sealed class CleanUpSolutionCommand : CleanUpCommandBase
    {
        public static readonly Guid CommandSet = new Guid("d729e146-72da-4933-9826-9e1d873b22ca");

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
            CleanupProjects(this.Dte.Solution.Projects.Cast<Project>().ToArray());
        }
    }
}
