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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AccountingPage.xaml
    /// </summary>
    public partial class AccountingPage : Page
    {
        private Konstantinov_1310Context _dbContext;
        public AccountingPage()
        {
            InitializeComponent();
            UpdateCountriesList();
        }

        public AccountingPage(Konstantinov_1310Context dbContext)
        {
            InitializeComponent();
            UpdateCountriesList();

            _dbContext = dbContext;
            SetDataContext(_dbContext);
        }

        private void SetDataContext(Konstantinov_1310Context dbContext)
        {
            var hotel = dbContext.Hotels;

            foreach (var item in hotel)
            {
                NameTextBox.Text = item.Name;
                CountStarsTextBox.Text = Convert.ToString(item.CountOfStars);
                CountriesComboBox.SelectedItem = item.CountCode;
            }
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Konstantinov_1310Context())
            {
                var country = context.Countries;
                int countCode = 1;

                foreach (var item in country)
                {
                    if((string)CountriesComboBox.SelectedItem == item.Name)
                    {
                        countCode = item.Code;
                    }
                }

                var hotels = new Hotel()
                {
                    Name = NameTextBox.Text,
                    CountOfStars = Convert.ToInt32(CountStarsTextBox.Text),
                    CountCode = countCode
                };

                context.Hotels.Add(hotels);
                context.SaveChanges();
            }

            NameTextBox.Text = "";
            CountStarsTextBox.Text = "";

            MessageBox.Show("Отель успешно добавлен!");
        }

        private void UpdateCountriesList()
        {
            _dbContext = new Konstantinov_1310Context();
            var countries = from country in _dbContext.Countries
                            select new 
                            {
                                Name = country.Name
                            };
            foreach (var country in countries)
            {
                CountriesComboBox.Items.Add(country.Name);
            }

        }
    }
}
