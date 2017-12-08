using System;
using Microsoft.VisualStudio.Shell;

namespace SolutionCleaner
{
    internal sealed class CleanUpProjectCommand : CleanUpCommandBase
    {
        public static readonly Guid CommandSet = new Guid("076c6292-35fb-4ce4-b264-a4d2eba87990");

        private CleanUpProjectCommand(Package package) : base(package, CommandSet)
        {
        }

        public static CleanUpProjectCommand Instance
        {
            get;
            private set;
        }

        public static void Initialize(Package package)
        {
            Instance = new CleanUpProjectCommand(package);
        }

        protected override void MenuItemCallback(object sender, EventArgs e)
        {
            CleanupProjects(this.Dte.SelectedItems.Item(1).Project);
        }
    }
}
