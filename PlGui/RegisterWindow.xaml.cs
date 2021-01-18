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
using System.Windows.Shapes;
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IBL bl;
        BO.User newUser = new BO.User() { Admin = false, UserName = "username", Password = "" };
        public RegisterWindow(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            gridViewUser.DataContext = newUser;
        }
        private void passwordConfirmTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordConfirmTextBox.Password == passwordTextBox.Password)
                passwordConfirmTextBox.Background = Brushes.LightGreen;
            else
                passwordConfirmTextBox.Background = Brushes.Orchid;
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            newUser.Password = passwordTextBox.Password;
            if (passwordConfirmTextBox.Password == passwordTextBox.Password)
                passwordConfirmTextBox.Background = Brushes.LightGreen;
            else
                passwordConfirmTextBox.Background = Brushes.Orchid;
        }

        private void pbRegister_Click(object sender, RoutedEventArgs e)
        {
            if (newUser.Password != "" && passwordConfirmTextBox.Password == passwordTextBox.Password)
            {
                bl.AddUser(newUser);
                this.Close();
            }
            else
                MessageBox.Show("Please enter matching passcodes!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
