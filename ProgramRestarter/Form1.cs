using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramRestarter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; // 수정 불가능한 콤보박스로 설정

            // --> 현재 실행중인 프로세스 리스트를 받아와 콤보박스에 넣는다
            Process[] processList = Process.GetProcesses();
            int i = processList.Count();
            string[] processName = new string[i];
            int j = 0;
            foreach (Process p in processList)
            {
                processName[j] = p.ProcessName + @"(" + p.Id + @")" + @" / " + p.MainWindowTitle;
                j++;
            }
            comboBox1.DataSource = processName;
            // <--현재 실행중인 프로세스 리스트를 받아와 콤보박스에 넣는다
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form2 form2 = new Form2(comboBox1.SelectedItem.ToString(), textBox1.Text);
        }
    }
}
