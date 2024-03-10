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
using static System.Net.WebRequestMethods;

namespace Arhiv.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private zayavka _currentZayavka = new zayavka();
        private users _users = new users();
        public string fsp;
        public UserPage()
        {
            InitializeComponent();
            LabelFSP.Content += Convert.ToString(App.Current.Resources["UserInfo"]);
            DataContext = _currentZayavka;
            fsp = Convert.ToString(App.Current.Resources["UserInfo"]);
        }

        private void AddZayavka_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentZayavka.type))
            {
                MessageBox.Show("Выбирите заявку!");
                return;
            }

            _users = vidachaEntities.GetContext().users.Where(x => x.fsp == fsp).FirstOrDefault();
            _currentZayavka.users_id = _users.id;
            _currentZayavka.date = DateTime.Now;
            _currentZayavka.status = "В рассмотрении";
            if (_currentZayavka.id == 0)
                vidachaEntities.GetContext().zayavka.Add(_currentZayavka);
            try
            {
                vidachaEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно!");
                DGridZayavka.ItemsSource = vidachaEntities.GetContext().zayavka.Where(x => x.users.fsp == fsp).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                vidachaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridZayavka.ItemsSource = vidachaEntities.GetContext().zayavka.Where(x => x.users.fsp == fsp).ToList();
            }
        }

        private void BtnOut_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DGridZayavka.SelectedItems.Cast<zayavka>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elemForRemoving.Count} записей?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            vidachaEntities.GetContext().zayavka.RemoveRange(elemForRemoving);
            vidachaEntities.GetContext().SaveChanges();
            MessageBox.Show("Вы успешно удалили записи!");
            DGridZayavka.ItemsSource = vidachaEntities.GetContext().zayavka.Where(x => x.users.fsp == fsp).ToList();
        }
    }
}
