using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENTFORM1
{
    public partial class OyunForm : Form
    {
        public OyunForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; // Static bir classtan asenkron şekilde bu formu kontrol ettiğimiz için program patlıyor. Bunun için çapraz thread'ı false yapıyorum.
        }

        private void OyunForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client.Disconnect();
            Application.Exit();
        }
    }
}
