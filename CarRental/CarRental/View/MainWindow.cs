using CarRental.View;
using CarRental.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SaabCarRentalService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BookingsViewModel BookingsVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            BookingsVM = new BookingsViewModel();
            Frame.Navigate(new HomePage(Frame, BookingsVM));
        }
    }
}
