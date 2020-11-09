using CarRental.Model;
using System;
using System.Collections.ObjectModel;

namespace CarRental.ViewModel
{
    public class BookingsViewModel
    {

        public ObservableCollection<Booking> Bookings { get; set; }
        private BookingsRepository BookingsRepository { get; set; }

        public BookingsViewModel()
        {
            BookingsRepository = new BookingsRepository();
            Bookings = new ObservableCollection<Booking>(BookingsRepository.bookingsRepository);
            Bookings.CollectionChanged += Bookings_CollectionChanged;
        }

        public void AddBookingToRepo(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException("Error: The argument is Null");
            Bookings.Add(booking);
        }

        private void Bookings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                BookingsRepository.addBooking(Bookings[newIndex]);
            }
        }


    }
}
