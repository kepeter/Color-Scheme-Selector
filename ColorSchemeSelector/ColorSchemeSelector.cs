using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ColorSchemeSelector
{
    [PackageRegistration ( UseManagedResourcesOnly = true )]
    [InstalledProductRegistration ( "#110", "#112", "1.0", IconResourceID = 400 )]
    [ProvideMenuResource ( "Menus.ctmenu", 1 )]
    [ProvideToolWindow ( typeof ( ColorSchemeToolWindow ), Style = Microsoft.VisualStudio.Shell.VsDockStyle.Tabbed )]
    [Guid ( Guids.Package )]
    public sealed class ColorSchemeSelector : Package
    {
        public ColorSchemeSelector ( )
        {
        }

        private void ShowToolWindow ( object sender, EventArgs e )
        {
            ToolWindowPane window = this.FindToolWindow ( typeof ( ColorSchemeToolWindow ), 0, true );
            if ( ( null == window ) || ( null == window.Frame ) )
            {
                throw new NotSupportedException ( "Can not create tool window." );
            }
            IVsWindowFrame windowFrame = ( IVsWindowFrame )window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure ( windowFrame.Show ( ) );
        }

        #region Package Members

        protected override void Initialize ( )
        {
            base.Initialize ( );

            OleMenuCommandService oOleMenuCommandService = ( OleMenuCommandService )GetService ( typeof ( IMenuCommandService ) );

            if ( oOleMenuCommandService != null )
            {
                CommandID oCommandID = new CommandID ( new Guid ( Guids.CommandSet ), ( int )CommandIDs.ColorSchemeSelector );
                MenuCommand oMenuCommand = new MenuCommand ( ShowToolWindow, oCommandID );

                oOleMenuCommandService.AddCommand ( oMenuCommand );
            }
        }

        #endregion
    }
}
