using CarRental.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

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
                BookingsRepository.AddBooking(Bookings[newIndex]);
            }
        }

        public List<string> GetAllActiveBookings()
        {
            List<string> active = new List<string>();


            // This is not an effective search for active bookings at all
            // Should change to a query
            foreach(var booking in BookingsRepository.bookingsRepository)
            {
                if (booking.Returned == false) 
                {
                    active.Add(booking.Id.ToString());
                }
            }

            return active;
        }

        public Booking FindBooking(string id)
        {
            Booking booking = (from temp in Bookings
                               where temp.Id.Equals(int.Parse(id))
                               select temp).First();

            return booking;
        }

    }
}
