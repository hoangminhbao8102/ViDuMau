﻿using BussinessLayer.DLUSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDLU
{
    public partial class Form1 : Form
    {
        CTSVSubsystem obj = new CTSVSubsystem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = obj.GetAllSinhVien();
        }
    }
}
