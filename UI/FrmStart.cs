﻿
using shootModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shoot.UI
{
    public partial class FrmStart : Form
    {
        private User user;
        /// <summary>
        /// 窗口宽度
        /// </summary>
        private readonly int width = int.Parse(UpdateManager.getAtt("width"));
        /// <summary>
        /// 窗口高度
        /// </summary>
        private readonly int height = int.Parse(UpdateManager.getAtt("height"));
        public FrmStart(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void FrmStart_Load(object sender, EventArgs e)
        {
            this.Width = width;//设置窗口的宽度
            this.Height = height;//设置窗口的高度

        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            FrmMain form = new FrmMain(this);
            form.Show();
            this.Hide();
        }
    }
}
