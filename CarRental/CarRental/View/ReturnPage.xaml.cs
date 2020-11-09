using CarRental.Model;
using CarRental.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.View
{
    /// <summary>
    /// Interaction logic for ReturnPage.xaml
    /// </summary>
    public partial class ReturnPage : Page
    {

        Frame Frame;
        BookingsViewModel BookingsVM;

        public ReturnPage()
        {
            InitializeComponent();
        }

        public ReturnPage(Frame frame, BookingsViewModel bookingsVM)
        {
            InitializeComponent();
            Frame = frame;
            BookingsVM = bookingsVM;
            populateBookingsList();
        }

        private void populateBookingsList()
        {
            // Cannot figure out why this won't run after every update that I would like
            ActiveBookings.Items.Clear();
            foreach (var item in BookingsVM.getAllActiveBookings())
            {
                ActiveBookings.Items.Add(item);
            }
        }

        private void rentBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new RentalPage(Frame, BookingsVM));
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ReturnPage(Frame, BookingsVM));
        }

        private void confirmReturn_Click(object sender, RoutedEventArgs e)
        {
            Booking selected = BookingsVM.findBooking(ActiveBookings.SelectedItem.ToString());
            if(DatePicker_Return.SelectedDate == null || KilometersDrive_TBox.Text == "")
            {
                MessageBox.Show("Du behöver ange ett slutdatum och antal körda kilometer.");
            }
            else
            {
                double price = BookingsVM.calculatePrice(selected, DatePicker_Return.SelectedDate.Value, KilometersDrive_TBox.Text);
                FinalPrice.Content =  price + "kr";
                BookingsVM.removeBookingFromRepo(selected);
                populateBookingsList();
            }
        }

        private void activeBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReturnBtn.IsEnabled = true;

            if(e.AddedItems.Count != 0)
            {
                Booking selected = BookingsVM.findBooking(ActiveBookings.SelectedItem.ToString());
                BookingId_Txt.Content = selected.Id;
                SocialSecurity_Txt.Content = selected.SocialSecurity;
                CarType_Txt.Content = selected.CarType;
                DateOfRent_Txt.Content = selected.StartDate;
            }
        }
    }
}
