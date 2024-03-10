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
using static System.Net.WebRequestMethods;
using Arhiv.Entity;
using Arhiv.Class;

namespace Arhiv.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        private users _currentUser = new users();
        public RegPage()
        {
            InitializeComponent();
            DataContext = _currentUser;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbLogin.Text) || string.IsNullOrWhiteSpace(TbPassword.Text) || string.IsNullOrWhiteSpace(TbFSP.Text))
            {
                MessageBox.Show("Заполните все поля!!");
                return;
            }
            _currentUser.role = 0;
            if (_currentUser.id == 0)
                vidachaEntities1.GetContext().users.Add(_currentUser);
            try
            {
                vidachaEntities1.GetContext().SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!");
                App.Current.Resources["UserInfo"] = TbFSP.Text;
                NavigationService.GetNavigationService(this).Navigate(new UserPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
