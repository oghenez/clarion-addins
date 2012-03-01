using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System;
using System.IO;
using log4net;
using ICSharpCode.SharpDevelop;

namespace ClarionAddins.IdeDebug
{
    public partial class OptionsPanel : AbstractOptionPanel
    {
        public OptionsPanel()
        {
            InitializeComponent();
            SetupDGV();
        }

        public override void LoadPanelContents()
        {
            cbQuietMode.Checked = MessageService.QuietMode;
            cbExtraDebugInfo.Checked = PropertyService.Get<bool>("ClarionAddins.IdeDebug.cbExtraDebugInfo", false);
            cbOutputDebugStringAppender.Checked = PropertyService.Get<bool>("ClarionAddins.IdeDebug.cbOutputDebugStringAppender", false);
            tbDebugViewPath.Text = PropertyService.Get("ClarionAddins.IdeDebug.tbDebugViewPath", "");
        }

        private void SetupDGV()
        {
            dgvXLog.RowTemplate.Height = dgvXLog.RowTemplate.Height - 2;
            dgvXLog.DataSource = GetXlogDataTable();
            dgvXLog.RowHeadersVisible = false;
            dgvXLog.AllowUserToAddRows = false;
            dgvXLog.AllowUserToResizeColumns = true;
            dgvXLog.AllowUserToResizeRows = false;
            dgvXLog.ReadOnly = true;
            dgvXLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvXLog.MultiSelect = false;
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
            dataGridViewCellStyle.BackColor = SystemColors.ButtonFace;
            dgvXLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
            dgvXLog.Columns["File"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private DataTable GetXlogDataTable()
        {
            DataTable dt = new DataTable("XLogFiles");
            dt.Columns.Add("File", typeof(String));
            //MessageBox.Show(Application.ExecutablePath);
            string[] filePaths = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.xlog");
            foreach (string file in filePaths)
            {
                DataRow row = dt.NewRow();
                row["File"] = file;
                dt.Rows.Add(row);
            }
            return dt;
            
        }

        public override bool StorePanelContents()
        {
            MessageService.QuietMode = cbQuietMode.Checked;
            PropertyService.Set("CoreProperties.QuietMode", MessageService.QuietMode);
            PropertyService.Set("ClarionAddins.IdeDebug.cbExtraDebugInfo", cbExtraDebugInfo.Checked);
            PropertyService.Set("ClarionAddins.IdeDebug.cbOutputDebugStringAppender", cbOutputDebugStringAppender.Checked);
            PropertyService.Set("ClarionAddins.IdeDebug.tbDebugViewPath", tbDebugViewPath.Text);
            return true;
        }

        private void dgvXLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                Form logViewer = new XLogViewer((string)dgvXLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                logViewer.Show(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Exe Files (*.exe)|*.exe|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.AutoUpgradeEnabled = true;
            ofd.Title = "Select DebugView EXE";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbDebugViewPath.Text = ofd.FileName;
            }
        }
    }
}
