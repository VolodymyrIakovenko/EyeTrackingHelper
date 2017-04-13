namespace EyeTrackingHelper.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using EyeTrackingHelper.Annotations;
    using Shortcut = EyeTrackingHelper.Models.Shortcut;
    using Enums;

    public class ShortcutsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Shortcut> _shortcuts;

        public ShortcutsViewModel()
        {
            Shortcuts = new ObservableCollection<Shortcut>
            {
                new Shortcut("{ }", ShortcutsType.CurlyBrackets),
                new Shortcut("( )", ShortcutsType.Parentheses),
                new Shortcut("For", ShortcutsType.For),
                new Shortcut("Foreach", ShortcutsType.Foreach),
                new Shortcut("If", ShortcutsType.If),
                new Shortcut("Auto Property", ShortcutsType.AutoProperty),
                new Shortcut("Console Output", ShortcutsType.ConsoleOutput),
                new Shortcut("Try Catch", ShortcutsType.TryCatch)
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
    }
}
