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
    public partial class OptionsModal : Form
    {
        public OptionsModal()
        {
            InitializeComponent();
        }
        public decimal Interval
        {
            get
            {
                return timerIntervalNumericUpDown.Value;
            }
            set
            {
                timerIntervalNumericUpDown.Value = value;
            }
        }

        public decimal ParentWidth
        {
            get
            {
                return widthUniverseNumericUpDown.Value;
            }
            set
            {
                widthUniverseNumericUpDown.Value = value;
            }
        }

        public decimal ParentHeight
        {
            get
            {
                return heightUniverseNumericUpDown.Value;
            }
            set
            {
                heightUniverseNumericUpDown.Value = value;
            }
        }
    }
}
