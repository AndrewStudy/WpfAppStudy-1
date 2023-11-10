using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        private Konstantinov_1310Context _dbContext;
        private string _idHotel;

        public HotelPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AccountingPage());
        }

        private void HotelsGridUpdate()
        {
            HotelsGrid.ItemsSource = null;

            _dbContext = new Konstantinov_1310Context();
            var hotels = from hotel in _dbContext.Hotels
                         join country in _dbContext.Countries
                         on hotel.CountCode equals country.Code
                         select new
                         {
                             IdHotel = hotel.Id,
                             Name = hotel.Name,
                             CountStars = hotel.CountOfStars,
                             CountCode = country.Name
                         };

            foreach (var hotel in hotels)
            {
                HotelsGrid.Items.Add(hotel);
            }
        }

        private void BtnEditHotel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AccountingPage(_dbContext));
        }

        private void BtnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AccountingPage());
        }

        private void BtnDeleteHotel_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Konstantinov_1310Context())
            {
                if (this._idHotel != null)
                {
                    var hotel = context.Hotels.Where(d => d.Id == Convert.ToInt32(this._idHotel)).Single();
                    context.Hotels.Remove(hotel);
                    context.SaveChanges();

                    MessageBox.Show($"Успешно удален {hotel.Name}");
                    
                }
            }
            HotelsGridUpdate();
        }

        private void HotelsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = HotelsGrid.SelectedCells[0];
            var content = (item.Column.GetCellContent(item.Item) as TextBlock).Text;
            this._idHotel = content;
        }

        private void HotelsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            
            HotelsGridUpdate();
        }
    }
}
