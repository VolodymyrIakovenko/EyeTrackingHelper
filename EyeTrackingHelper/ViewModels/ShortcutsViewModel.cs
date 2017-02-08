namespace EyeTrackingHelper.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using EnvDTE;
    using EyeTrackingHelper.Annotations;
    using Microsoft.VisualStudio.PlatformUI;
    using Microsoft.VisualStudio.Shell;
    using Command = EyeTrackingHelper.Command;
    using Shortcut = EyeTrackingHelper.Models.Shortcut;

    public class ShortcutsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Shortcut> _shortcuts;

        public ShortcutsViewModel()
        {
            Shortcuts = new ObservableCollection<Shortcut>
            {
                new Shortcut("{ }", new Command(output => InsertTextToCodePane(), _ => true)),
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Shortcut> Shortcuts
        {
            get { return _shortcuts; }
            set
            {
                _shortcuts = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InsertTextToCodePane()
        {
            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            if (dte == null)
            {
                return;
            }

            var activeDoc = dte.ActiveDocument.Object() as TextDocument;
            if (activeDoc == null)
            {
                return;
            }

            var currentLine = activeDoc.Selection.CurrentLine;
            var currentColumn = activeDoc.Selection.CurrentColumn;

            activeDoc.Selection.Insert("{");
            activeDoc.Selection.NewLine();
            activeDoc.Selection.NewLine();
            activeDoc.Selection.MoveTo(activeDoc.Selection.CurrentLine, currentColumn);
            activeDoc.Selection.Insert("}");
            activeDoc.Selection.MoveTo(currentLine + 1, currentColumn);
            activeDoc.Selection.Indent();
        }


    }
}
