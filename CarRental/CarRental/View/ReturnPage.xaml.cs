﻿using CarRental.Model;
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
            PopulateBookingsList();
        }

        private void PopulateBookingsList()
        {
           foreach(var item in BookingsVM.GetAllActiveBookings())
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

        }

        private void ActiveBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Booking selected = BookingsVM.FindBooking(ActiveBookings.SelectedItem.ToString());
            BookingId_Txt.Content = selected.Id;
            SocialSecurity_Txt.Content = selected.SocialSecurity;
            CarType_Txt.Content = selected.CarType;
            DateOfRent_Txt.Content = selected.StartDate;

        }
    }
}
