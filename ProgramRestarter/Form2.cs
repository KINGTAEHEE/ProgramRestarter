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
    public partial class Form2 : Form
    {
        string processName = string.Empty;
        string processMainWindowTitle = string.Empty;
        string program = string.Empty;

        public Form2(string _process, string _program)
        {
            InitializeComponent();

            // --> 폼 관련 설정(화면 안보이게 + 트레이 아이콘)
            this.WindowState = FormWindowState.Minimized; // 폼 최소화
            this.ShowInTaskbar = false; // 작업표시줄에 안보이게
            this.Visible = false; // 폼 안보이게
            // <-- 폼 관련 설정(화면 안보이게 + 트레이 아이콘)

            // --> 선택된 프로그램과 실행할 프로그램 정보를 내부 변수로 넣는다
            string[] splitProcessInfo = new string[2];
            splitProcessInfo = _process.Split('/');
            string[] splitProcessInfo2 = new string[2];
            splitProcessInfo2 = splitProcessInfo[0].Split('(');
            processName = splitProcessInfo2[0].Trim();
            processMainWindowTitle = splitProcessInfo[1].Trim();
            program = _program;
            // <-- 선택된 프로그램과 실행할 프로그램 정보를 내부 변수로 넣는다

            notifyIcon1.Text = "ProgramRestarter: " + processName;
            notifyIcon1.BalloonTipTitle = "ProgramRestarter";
            notifyIcon1.BalloonTipText = processName + " 프로그램 감시를 시작합니다.";
            notifyIcon1.ShowBalloonTip(1 * 1000);

            timer1.Interval = 10 * 1000; // 10초마다 체크하도록 설정
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false; // 타이머 중복실행 방지용 - 처리 시작하기 앞서 막는다

            if (ProcessCheck(processName) == false) // 프로세스가 실행중이지 않음
            {
                ProcessStart(program); // 프로그램을 실행함
            }
            else // 프로세스가 실행중임
            {
                //if (ProcessCheck("윈도우 오류보고창") == true) // 실행중이지만 윈도우 오류보고창 뜬 상태
                //{
                //    ProcessStop("윈도우 오류보고창"); // 윈도우 오류보고창을 강제 종료
                //    System.Threading.Thread.Sleep(1 * 1000); // 1초 대기
                //    ProcessStart(program); // 프로그램을 실행함
                //}
                //else // 정상실행중
                //{
                //    // 아무것도 하지 않고 넘어감
                //}
            }

            timer1.Enabled = true; // 타이머 중복실행 방지용 - 처리 완료되면 다시 연다
        }

        private bool ProcessCheck(string _process)
        {
            Process[] processList = Process.GetProcessesByName(_process);

            if (processList.Length < 1) // 프로세스가 실행중이지 않음
            {
                return false;
            }
            else // 프로세스가 실행중임
            {
                return true;
            }
        }

        private void ProcessStart(string _fileName)
        {
            Process.Start(_fileName);
        }

        private void ProcessStop(string _process)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals(_process))
                {
                    process.Kill();
                }
            }
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}
