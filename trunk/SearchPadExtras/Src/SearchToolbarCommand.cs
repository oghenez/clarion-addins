using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using SearchAndReplace;

namespace ClarionEdge.SearchPadExtras
{

    public class FindNext : AbstractMenuCommand
    {
        public override void Run()
        {
            if (SearchOptions.CurrentFindPattern.Length > 0)
            {
                SearchModeOptions.MyFindNext();
                return;
            }
            Find find = new Find();
            find.Run();
        }
    }

    public class FindComboBox : AbstractComboBoxCommand
    {
        ComboBox comboBox;
        public FindComboBox()
        {
        }

        void RefreshComboBox()
        {
            comboBox.Items.Clear();
            foreach (string findItem in SearchOptions.FindPatterns)
            {
                comboBox.Items.Add(findItem);
            }
            comboBox.Text = SearchOptions.FindPattern;
        }

        void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                CommitSearch();
                e.Handled = true;
            }
        }

        void CommitSearch()
        {
            if (comboBox.Text.Length > 0)
            {
                LoggingService.Debug("FindComboBox.CommitSearch()");

                SearchOptions.SearchAndReplaceBinding = SearchOptions.CurrentDocumentBinding;
                SearchOptions.FindPattern = comboBox.Text;
                SearchModeOptions.MyFindNext();
                comboBox.Focus();
            }
        }

        void SearchOptionsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Key == "FindPatterns")
            {
                RefreshComboBox();
            }
        }

        protected override void OnOwnerChanged(EventArgs e)
        {
            base.OnOwnerChanged(e);
            ToolBarComboBox toolbarItem = (ToolBarComboBox)Owner;
            comboBox = toolbarItem.ComboBox;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox.KeyPress += OnKeyPress;
            SearchOptions.Properties.PropertyChanged += new PropertyChangedEventHandler(SearchOptionsChanged);
            comboBox.LostFocus += new EventHandler(comboBox_LostFocus);
            comboBox.SelectionChangeCommitted += new EventHandler(comboBox_SelectionChangeCommitted);
            RefreshComboBox();
        }

        void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchOptions.FindPattern = comboBox.Text;
        }

        void comboBox_LostFocus(object sender, EventArgs e)
        {
            SearchOptions.FindPattern = comboBox.Text;
        }
    }
}
