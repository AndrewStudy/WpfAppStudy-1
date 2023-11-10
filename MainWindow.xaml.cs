using System;
using System.Collections.Generic;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HotelPage());
            Manager.MainFrame = MainFrame;
            ImportTours();
        }

        private void ImportTours()
        {
            var fileData = File.ReadAllLines(@"C:\Users\Студент\Desktop\ИС-4-9-20\Константинов\Туры.txt");
            var images = Directory.GetFiles(@"C:\Users\Студент\Desktop\ИС-4-9-20\Константинов\Туры фото");

            using (var context = new Konstantinov_1310Context())
            {

                foreach (var lines in fileData)
                {
                    var data = lines.Split('\t');
                    var name = data[0].Replace("\"", "");

                    var tempTour = new Tour
                    {
                        Name = name,
                        TicketCount = int.Parse(data[2]),
                        Price = decimal.Parse(data[3]),
                        IsActual = (data[4] == "0") ? false : true,
                        ImagePreview = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(name))).ToArray()
                    };

                    context.Tours.Add(tempTour);
                    context.SaveChanges();
                }
                
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoForward)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnGo.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
                BtnGo.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
