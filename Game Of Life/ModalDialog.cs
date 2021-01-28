using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class ModalDialog : Form
    {

       public decimal randomSeed;

        public ModalDialog()
        {
            InitializeComponent();
        }

        private void buttonRandomize_Click(object sender, EventArgs e)
        {
                Random rand = new Random();
                SeedNumericUpDown.Value = rand.Next(1000);
        }

        public decimal Seed
        {
            get
            {
                return SeedNumericUpDown.Value;
            }
            set
            {
                SeedNumericUpDown.Value = value;
            }
        }
    }
}
