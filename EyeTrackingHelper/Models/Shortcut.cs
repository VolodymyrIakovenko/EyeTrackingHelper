namespace EyeTrackingHelper.Models
{
    using Enums;
    using EnvDTE;
    using Microsoft.VisualStudio.Shell;
    using System;
    using System.Windows.Input;
    using Command = EyeTrackingHelper.Command;

    public class Shortcut
    {
        public Shortcut(string text, ShortcutsType type)
        {
            Text = text;
            Type = type;
            ExecutionCommand = new Command(output => InsertTextToCodePane(), _ => true);
        }

        public string Text { get; set; }

        public ShortcutsType Type { get; set; }

        public ICommand ExecutionCommand { get; set; }

        private void InsertTextToCodePane()
        {
            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            if (dte == null || dte.ActiveDocument == null)
            {
                return;
            }

            var activeDoc = dte.ActiveDocument.Object() as TextDocument;
            if (activeDoc == null)
            {
                return;
            }

            switch (Type)
            {
                case ShortcutsType.CurlyBrackets:
                    OutputCurlyBrackets(activeDoc);
                    break;

                case ShortcutsType.Parentheses:
                    OutputParentheses(activeDoc);
                    break;

                case ShortcutsType.RowDefinitions:
                    OutputRowDefinitions(activeDoc, 3);
                    break;

                case ShortcutsType.ColumnDefinitions:
                    OutputColumnDefinitions(activeDoc, 3);
                    break;

                case ShortcutsType.For:
                    ExecuteSnippet("for", activeDoc);
                    break;

                case ShortcutsType.Foreach:
                    ExecuteSnippet("foreach", activeDoc);
                    break;

                case ShortcutsType.If:
                    ExecuteSnippet("if", activeDoc);
                    break;

                case ShortcutsType.Else:
                    ExecuteSnippet("else", activeDoc);
                    break;

                case ShortcutsType.AutoProperty:
                    ExecuteSnippet("prop", activeDoc);
                    break;

                case ShortcutsType.ConsoleOutput:
                    ExecuteSnippet("cw", activeDoc);
                    break;

                case ShortcutsType.TryCatch:
                    ExecuteSnippet("try", activeDoc);
                    break;

                case ShortcutsType.Undo:
                    ExecuteUndo(activeDoc);
                    break;

                default:
                    throw new NotImplementedException();
            }

            dte.ActiveDocument.Activate();
        }

        private void OutputCurlyBrackets(TextDocument activeDoc)
        {
            activeDoc.Selection.NewLine();

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

        private void OutputParentheses(TextDocument activeDoc)
        {
            activeDoc.Selection.Insert("(");
            activeDoc.Selection.Insert(")");
            activeDoc.Selection.MoveTo(activeDoc.Selection.CurrentLine, activeDoc.Selection.CurrentColumn - 1);
        }

        private void OutputRowDefinitions(TextDocument activeDoc, int rowAmount)
        {
            activeDoc.Selection.Insert("<Grid.RowDefinitions>");
            activeDoc.Selection.NewLine();

            for (var i = 0; i < rowAmount; i++)
            {
                activeDoc.Selection.Insert("<RowDefinition Height=\"*\"/>");
                if (i != rowAmount - 1)
                {
                    activeDoc.Selection.NewLine();
                }
            }

            activeDoc.Selection.NewLine();
            activeDoc.Selection.Unindent();
            activeDoc.Selection.Insert("</Grid.RowDefinitions>");
        }

        private void OutputColumnDefinitions(TextDocument activeDoc, int columnAmount)
        {
            activeDoc.Selection.Insert("<Grid.ColumnDefinitions>");
            activeDoc.Selection.NewLine();

            for (var i = 0; i < columnAmount; i++)
            {
                activeDoc.Selection.Insert("<ColumnDefinition Width=\"*\"/>");
                if (i != columnAmount - 1)
                {
                    activeDoc.Selection.NewLine();
                }
            }

            activeDoc.Selection.NewLine();
            activeDoc.Selection.Unindent();
            activeDoc.Selection.Insert("</Grid.ColumnDefinitions>");
        }

        private void ExecuteUndo(TextDocument activeDoc)
        {
            try
            {
                activeDoc.DTE.ActiveDocument.Activate();
                activeDoc.DTE.ExecuteCommand("Edit.Undo");
            }
            catch (Exception)
            {
                // Nothing to undo, ignore.
            }
            
        }

        private void ExecuteSnippet(string snippet, TextDocument activeDoc)
        {
            activeDoc.Selection.Insert(snippet);

            activeDoc.DTE.ActiveDocument.Activate();
            activeDoc.DTE.ExecuteCommand("Edit.InsertTab");
        }
    }
}
