using MahApps.Metro.Controls;
using System;

namespace StudentManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:MetroWindow
    {
        public MainWindow()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            InitializeComponent(); 

        }    

    }
}
