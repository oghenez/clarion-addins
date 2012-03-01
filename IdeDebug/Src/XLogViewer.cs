using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ClarionAddins.IdeDebug
{
    public partial class XLogViewer : Form
    {

        public XLogViewer()
        {
            InitializeComponent();
        }

        public XLogViewer(string p)
        {
            InitializeComponent();
            tbData.Text = DecodeXLog(p);
        }

        private string DecodeXLog(string fname)
        {
            string result = string.Empty;
            if (File.Exists(fname))
            {
                FileStream fileStream = new FileStream(fname, FileMode.Open);
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    result = Encoding.ASCII.GetString(Convert.FromBase64String((string)binaryFormatter.Deserialize(fileStream)));
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show("Failed to Read Log. Reason: " + ex.Message);
                }
                finally
                {
                    fileStream.Close();
                }
            }
            return result;
        }
    }
}
