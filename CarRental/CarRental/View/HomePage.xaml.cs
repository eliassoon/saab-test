using CarRental.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        private Frame Frame;
        private BookingsViewModel BookingsVM;

        public HomePage()
        {
            InitializeComponent();
        }

        public HomePage(Frame frame, BookingsViewModel bookingsVM)
        {
            InitializeComponent();
            Frame = frame;
            BookingsVM = bookingsVM;
        }

        private void rentBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new RentalPage(Frame, BookingsVM));
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ReturnPage(Frame, BookingsVM));
        }
    }
}
