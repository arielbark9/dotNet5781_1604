using System;
using System.Collections.Generic;
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
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            foreach (var user in bl.GetAllUsers())
            {
                if (user.UserName == tbUser.Text && user.Password == passboxPass.Password)
                {
                    found = true;
                    if (user.Admin == true)
                    {
                        new MainAdminWindow(user, bl).Show();
                        this.Close();
                        break;
                    }
                    //else
                    //    new MainUserWindow().Show();
                }    
            }
            if(!found)
                MessageBox.Show("Invalid credentials! please try again.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void bReg_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(bl);
            registerWindow.ShowDialog();
        }
    }
}
