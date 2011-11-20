using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FTPLib;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace PloobsUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\.NETFramework\\v4.0.30319\\AssemblyFoldersEx\\PloobsEngine", false);
            if (myKey != null)
            {
                String o = myKey.GetValue(null) as String;
                if (o != null)
                {
             
                    foreach (var item in Directory.EnumerateFiles(o))
                    {
                        if (item.EndsWith(".dll"))
                        {
                            //PloobsEngineDebug, Version=0.0.0.1, Culture=neutral, PublicKeyToken=0c21691816f8c6d0
                            Assembly Assembly = Assembly.LoadFile(item);
                            String name = Assembly.FullName.Split(',')[0];
                            String version = Assembly.FullName.Split(',')[1].Split('=')[1];
                            packageName = version;
                        }
                    }
                }
            }

                try
                {
                    ftp = new FTP("ftp.ploobs.com.br", "ploobs", "5ruTrus6");
                    ftp.Connect();
                    ftp.ChangeDir("/ploobs/Web/Updater");

                    foreach (String item in ftp.List())
                    {
                        String[] files = item.Split(' ');
                        String file = files[files.Count() - 1];
                        AvaliableVersions.Add(file);
                        listBox1.Items.Add(file);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (packageName != null)
                {
                    label2.Content = "CurrentVersion: " + packageName;
                }

        }

        String packageName = null;
        List<String> AvaliableVersions = new List<string>();
        FTP ftp = null;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (AvaliableVersions[AvaliableVersions.Count - 1] != packageName)
            {
                DeleteCurrentEngineVersion();

                Random rd = new Random();
                int rdnum = rd.Next();
                String sitem = AvaliableVersions[AvaliableVersions.Count - 1];
                ftp.ChangeDir("/ploobs/Web/Updater/" + sitem);
                String temppath = System.IO.Path.GetTempPath() + "PloobsEngine" + rdnum + ".msi";
                ftp.OpenDownload("PloobsEngine.msi", temppath, true);

                while (ftp.DoDownload() != 0)
                {
                    Thread.Sleep(500);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = temppath;
                process.Start();
                process.WaitForExit();

                packageName = AvaliableVersions[AvaliableVersions.Count - 1];
                if (packageName != null)
                {
                    label2.Content = "CurrentVersion: " + packageName;
                }   

            }
        }

        public void DeleteCurrentEngineVersion()
        {            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "msiexec.exe /x {2AF6A123-A362-4E2B-B23A-D1E952C15A9C}";
            process.Start();
            process.WaitForExit();
        }
        
        private void listBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Random rd = new Random();
            int rdnum = rd.Next();
            String sitem = listBox1.SelectedItem as String;
            ftp.ChangeDir("/ploobs/Web/Updater/" + sitem);
            String temppath = System.IO.Path.GetTempPath() + "Change" + rdnum + ".pdf";
            ftp.OpenDownload("Changes.pdf", temppath, true);

            while (ftp.DoDownload() != 0)
            {
            }
            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = temppath; 
            process.Start(); 
            process.WaitForExit();
        }
    }
}
