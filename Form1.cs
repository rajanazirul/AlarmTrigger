using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace AlarmTrigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("for /L %N in (1,1,5) do @plink -ssh -pw raspberry pi@192.168.4.1 gpio -g write 8 1");
            //cmd.StandardInput.WriteLine("start chrome");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 openForm = new Form1();
            //openForm.Show();
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine($" Username:{textBox1.Text}: NT ID:{textBox2.Text}: QRAP No:{textBox3.Text}: Containment Action:{richTextBox1.Text}");
                w.WriteLine("-------------------------------");

                //Log(textBox1.Text, w);
                //Log(textBox2.Text, w);
                //Log(textBox3.Text, w);
                //Log(richTextBox1.Text, w);
            }
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("for /L %N in (1,1,5) do @plink -ssh -pw raspberry pi@192.168.4.1 gpio -g write 8 0");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            Application.Exit();
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

    }
}
