using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagement.View
{
    /// <summary>
    /// Interaction logic for AccountSettingsView.xaml
    /// </summary>
    public partial class AccountSettingsView : UserControl
    {
  
        public AccountSettingsView()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            InitializeComponent();

        }






        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (StudentDBEntities DB = new StudentDBEntities())
            {
                UserImage data = new UserImage();

                data.ImagePath = txtPicturePath.Text;
                data.ImageToByte = File.ReadAllBytes(txtPicturePath.Text);
                DB.UserImage.Add(data);
                DB.SaveChanges();
              



            }
            MessageBox.Show("You have save!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StudentDBEntities DB = new StudentDBEntities();
            UserImage image = new UserImage();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*";
            openFileDialog1.DefaultExt = ".jpeg";
            txtPicturePath.Text = openFileDialog1.FileName;
            ImageSource imageSource = new BitmapImage(new Uri(txtPicturePath.Text));
            image1.Source = imageSource;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (StudentDBEntities DB = new StudentDBEntities())
            {
                UserImage images = new UserImage();
                var result = (from t in DB.UserImage
                              where t.ImagePath == txtPicturePath.Text
                              select t.ImageToByte).FirstOrDefault();
                Stream StreamObj = new MemoryStream(result);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                image2.Source = BitObj;
            }
        }
    }
}
