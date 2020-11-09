using CarRental.Model;
using CarRental.ViewModel;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
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
            loadCarTypes();
        }

        private void loadCarTypes()
        {
            foreach (var type in Enum.GetNames(typeof(CarTypes)))
            {
                CarType_CBox.Items.Add(type);
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

        private void addBooking_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(!int.TryParse(SocialSecurity_TBox.Text, out int socialSecurity) || CarType_CBox.SelectedItem == null || DatePicker_Start.SelectedDate.Value == null)
            {
                MessageBox.Show("You need to fill out all the required fields");
            }
            else
            {
                Booking booking = new Booking();
                booking.SocialSecurity = socialSecurity.ToString();
                booking.CarType = (string)CarType_CBox.SelectedItem;
                booking.StartDate = DatePicker_Start.SelectedDate.Value;
                booking.StartKilometers = 0;

                BookingsVM.addBookingToRepo(booking);
                MessageBox.Show("Din bokning har blivit godkänd");

                SocialSecurity_TBox.Text = "";
                CarType_CBox.SelectedItem = null;
                DatePicker_Start.SelectedDate = null;


            }

            
        }
    }
}
