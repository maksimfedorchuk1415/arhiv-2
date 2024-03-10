using Arhiv.Entity;
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
using Arhiv.Entity;
using Arhiv.Class;

namespace Arhiv.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public users _currentUser = new users();
        public string user_fio;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnAutn1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbLogin.Text) || string.IsNullOrWhiteSpace(TbPassword.Password))
            {
                MessageBox.Show("Введите все данные!");
                return;
            }   
            if (vidachaEntities.GetContext().users.Any(x => x.login == TbLogin.Text && x.password == TbPassword.Password && x.role == 1))
            {
                _currentUser = vidachaEntities.GetContext().users.Where(b => b.login == TbLogin.Text && b.password == TbPassword.Password).FirstOrDefault();
                user_fio = _currentUser.fsp;
                App.Current.Resources["UserInfo"] = user_fio;
                NavigationService.GetNavigationService(this).Navigate(new AdminPage());
            }
            else
            {
                if (vidachaEntities.GetContext().users.Any(x => x.login == TbLogin.Text && x.password ==    TbPassword.Password && x.role == 0))
                {
                    _currentUser = vidachaEntities.GetContext().users.Where(b => b.login == TbLogin.Text && b.password ==   TbPassword.Password).FirstOrDefault();
                    user_fio = _currentUser.fsp;
                    App.Current.Resources["UserInfo"] = user_fio;
                    NavigationService.GetNavigationService(this).Navigate(new UserPage());
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль!");
                }
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RegPage());
        }
    }
}
