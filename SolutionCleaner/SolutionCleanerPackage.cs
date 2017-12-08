using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace SolutionCleaner
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class SolutionCleanerPackage : Package
    {
        public const string PackageGuidString = "c53fd02b-f57b-424a-895e-964d95aa7a12";

        protected override void Initialize()
        {
            base.Initialize();
            CleanUpProjectCommand.Initialize(this);
            CleanUpSolutionCommand.Initialize(this);
        }
    }
}
