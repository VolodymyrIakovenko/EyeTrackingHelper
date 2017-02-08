namespace EyeTrackingHelper.Models
{
    using System.Windows.Input;

    public class Shortcut
    {
        public Shortcut(string name, ICommand executionCommand)
        {
            Name = name;
            ExecutionCommand = executionCommand;
        }

        public string Name { get; set; }

        public ICommand ExecutionCommand { get; set; }
    }
}
