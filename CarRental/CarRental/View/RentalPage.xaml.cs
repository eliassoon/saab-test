using CarRental.Model;
using CarRental.ViewModel;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;

namespace CarRental.View
{
    /// <summary>
    /// Interaction logic for RentalPage.xaml
    /// </summary>
    public partial class RentalPage : Page
    {

        Frame Frame;
        BookingsViewModel BookingsVM;

        public RentalPage()
        {
            InitializeComponent();
        }

        public RentalPage(Frame frame, BookingsViewModel bookingsVM)
        {
            InitializeComponent();
            Frame = frame;
            BookingsVM = bookingsVM;
            LoadCarTypes();
        }

        private void LoadCarTypes()
        {
            foreach (CarType carType in Enum.GetValues(typeof(CarType)))
            {
                CarType_CBox.Items.Add(carType);
            }
        }

        private void rentBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Frame.Navigate(new RentalPage(Frame, BookingsVM));
        }

        private void returnBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Frame.Navigate(new ReturnPage(Frame, BookingsVM));
        }

        private void AddBooking_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Booking booking = new Booking();
            booking.SocialSecurity = SocialSecurity_TBox.Text;
            booking.CarType = (CarType)CarType_CBox.SelectedItem;
            booking.StartDate = Date_Picker.SelectedDate.Value;
            booking.StartKilometers = 0;

            BookingsVM.AddBookingToRepo(booking);
        }
    }
}
