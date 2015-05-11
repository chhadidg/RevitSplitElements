using EditElements.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditElements
{
    public partial class ColumnForm : Form
    {
        public ColumnForm()
        {
            InitializeComponent(); 
            
            if (GlobalVariables.RememberColumns)
            {
                checkBox_RememberColumns.Checked = true;
            }
            else
            {
                checkBox_RememberColumns.Checked = false;
            }
        }

        private void button_Vertical_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalVariables.MessageVerticalColumn = 1;
        }

        private void button_Slanted_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalVariables.MessageVerticalColumn = 2;
        }

        private void checkBox_RememberColumns_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_RememberColumns.Checked)
            {
                GlobalVariables.RememberColumns = true;
            }
            else
            {
                GlobalVariables.RememberColumns = false;
            }
        }
    }
}
