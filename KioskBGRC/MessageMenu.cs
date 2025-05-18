using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KioskBGRC
{
    public partial class MessageMenu : MetroFramework.Forms.MetroForm
    {
        public MessageMenu()
        {
            InitializeComponent();
        }
        
        public MessageMenu(string data)
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
