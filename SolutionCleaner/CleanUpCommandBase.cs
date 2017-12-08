using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace SolutionCleaner
{
    internal abstract class CleanUpCommandBase
    {
        protected const int CommandId = 0x0100;

        protected IServiceProvider ServiceProvider;
        protected DTE2 Dte;

        protected CleanUpCommandBase(Package package, Guid commandSet)
        {
            this.ServiceProvider = package ?? throw new ArgumentNullException(nameof(package));
            this.Dte = this.ServiceProvider.GetService(typeof(DTE)) as DTE2;

            var commandService =
                    this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                var menuCommandID = new CommandID(commandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        protected abstract void MenuItemCallback(object sender, EventArgs e);

        protected void CleanupProjects(params Project [] projects)
        {
            StringBuilder cleaninigResults = new StringBuilder("Clean Up Results:");
            cleaninigResults.Append(Environment.NewLine);

            foreach (var project in projects)
            {
                cleaninigResults.Append(CleanupProject(project));
            }

            VsShellUtilities.ShowMessageBox(
                    this.ServiceProvider,
                    cleaninigResults.ToString(),
                    null,
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private string CleanupProject(Project project)
        {
            StringBuilder cleaninigResults = new StringBuilder(Environment.NewLine);
            cleaninigResults.Append($"Project: {project.Name}: ");

            try
            {
                var path = project.Properties.Item("FullPath").Value as string;

                var binPath = Path.Combine(path, "bin");
                var objPath = Path.Combine(path, "obj");

                if (Directory.Exists(binPath)) {
                    Directory.Delete(binPath, true);
                    cleaninigResults.Append("['bin' > removed], ");
                }
                else
                {
                    cleaninigResults.Append("['bin' > skipped], ");
                }

                if (Directory.Exists(objPath)) {
                    Directory.Delete(objPath, true);
                    cleaninigResults.Append("['obj' > removed] ");
                }
                else
                {
                    cleaninigResults.Append("['obj' > skipped] ");
                }
            }
            catch (Exception exception)
            {
                cleaninigResults.Append($"Error while cleaning: {exception.Message}");
            }

            return cleaninigResults.ToString();
        }
    }
}
