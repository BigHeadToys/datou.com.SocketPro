using datou.com.SocketPro.entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datou.com.SocketPro
{
    public partial class FrmShowJson : Form
    {
        public FrmShowJson(List<Student> stuList)
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = stuList;
        }
    }
}
