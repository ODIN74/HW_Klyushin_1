using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Klyushin_1
{
    public partial class gameForm : System.Windows.Forms.Form
    {
        public gameForm()
        {
            InitializeComponent();
        }

        private void gameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Game.Stop();
        }
    }
}
