namespace EyeTrackingHelper
{
    using System.Runtime.InteropServices;
    using EyeTrackingHelper.ViewModels;
    using Microsoft.VisualStudio.Shell;
    using Tobii.EyeX;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("1a1701fd-3179-41a4-98e3-529e973009e4")]
    public sealed class ShortcutsWindow : ToolWindowPane
    {
        private Host _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortcutsWindow"/> class.
        /// </summary>
        public ShortcutsWindow() : base(null)
        {
            _host = new Host();
            _host.InitializeWpfAgent();

            this.Caption = "ShortcutsWindow";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            var shortcutsWindowsControl = new ShortcutsWindowControl();
            shortcutsWindowsControl.DataContext = new ShortcutsViewModel();
            this.Content = shortcutsWindowsControl;
        }

        protected override void Dispose(bool disposing)
        {
            _host.Dispose();

            base.Dispose(disposing);
        }
    }
}
