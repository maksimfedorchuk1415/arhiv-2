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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            DGridZayavka.ItemsSource = vidachaEntities1.GetContext().zayavka.ToList();
        }

        private void DGridZayavka_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            zayavka elemForAccept = DGridZayavka.SelectedItems.Cast<zayavka>().FirstOrDefault();
            if(MessageBox.Show($"Вы точно хотите выдать эту заявку?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            
            elemForAccept.status = "Готова к выдаче";
            try
            {
                vidachaEntities1.GetContext().SaveChanges();
                MessageBox.Show("Заявка готова к выдачи");
                DGridZayavka.ItemsSource = vidachaEntities1.GetContext().zayavka.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnOut_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }
    }
}
