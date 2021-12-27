using PassFile.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace PassFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            check:
            if (!File.Exists(@"C:\Program Files\WinRAR\WinRAR.exe"))
            {
                DialogResult result = MessageBox.Show("This app need winrar to work, please install it first", "Requirement", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if(result == DialogResult.Abort)
                {
                    Application.Exit();
                    Environment.Exit(1);
                }
                else if (result == DialogResult.Retry)
                {
                    goto check;
                }
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                Application.Exit();
                Environment.Exit(1);
            }
        }

        private void gbPass_Enter(object sender, EventArgs e)
        {
            if (tbPass.TextLength < 4)
            {
                lWarning.Visible = true;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (File.Exists(tbFile.Text))
            {
                //Asign
                string sFile = tbFile.Text;
                string snoExt = Path.Combine(Path.GetDirectoryName(sFile), Path.GetFileNameWithoutExtension(sFile));
                string sApp = snoExt + ".exe";
                string sPassW = tbPass.Text;
                bool Pass = false;
                //check pass
                if (tbPass.TextLength >= 4)
                    Pass = true;
                lWarning.Visible = !Pass;

                string sTemp = Path.GetTempPath();
                //create icon
                string sIco = Path.Combine(sTemp, "ico.ico");
                if (!File.Exists(sIco))
                {
                    var vIco = new FileStream(sIco, FileMode.Create, FileAccess.Write);
                    Properties.Resources._1031.Save(vIco);
                }
                //create opt
                string sOpt = Path.Combine(sTemp, "opt.txt");
                if (!File.Exists(sOpt))
                    File.WriteAllText(sOpt, @"Silent=1
Overwrite=1");
                string stOpt = Path.GetFullPath(sOpt);
                //passed
                if (Pass)
                {
                    Process.Start("WinRAR.exe", string.Format(@"a -cfg- -ep1 -df -ibck -inul -iicon""{4}"" -k -m5 -ma5 -md128 -r -s -sfxdefault.sfx -hp{1} -tl -y ""{3}"" ""-z{2}"" ""{0}""", sFile, sPassW, stOpt, sApp, sIco));
                    MessageBox.Show("The file has been processed");
                    tbFile.Text = "";
                    tbPass.Text = "";
                }

            }
            else
                MessageBox.Show("The program cannot find the file specified");
        }

        private void tbFile_MouseUp(object sender, MouseEventArgs e)
        {
            string ok = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ok = openFileDialog1.FileName;
            }
            tbFile.Text = ok;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            fHelp f2 = new fHelp();
            f2.ShowDialog();
        }
    }
}
