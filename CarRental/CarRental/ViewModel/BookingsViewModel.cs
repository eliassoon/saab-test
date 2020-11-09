using CarRental.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarRental.ViewModel
{
    public class BookingsViewModel : ViewModelBase
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

        public double CalculatePrice(Booking selected, DateTime dateOfReturn, string kilometersDriven)
        {

            if (DateTime.Compare(selected.StartDate, dateOfReturn) < 0 && int.TryParse(kilometersDriven, out int kilometers))
            {
                int nbrOfDays = int.Parse((dateOfReturn - selected.StartDate).TotalDays.ToString());
                int nbrOfKilometers = kilometers - selected.StartKilometers;

                int costPerDay = 395;
                int costPerKilometer = 10;

                int standardPrice = costPerDay * nbrOfDays;
                int kilometerPrice = costPerKilometer * nbrOfKilometers;

                double finalPrice;

                switch (selected.CarType)
                {
                    case nameof(CarTypes.Bil):
                        {
                            finalPrice = standardPrice + kilometerPrice;
                            break;
                        }
                    case nameof(CarTypes.Van):
                        {
                            finalPrice = standardPrice * 1.2 + kilometerPrice;
                            break;
                        }
                    case nameof(CarTypes.Buss):
                        {
                            finalPrice = standardPrice * 1.7 + kilometerPrice * 1.5;
                            break;
                        }

                    default:
                        {
                            finalPrice = -1;
                            break;
                        }
                }

                return finalPrice;
            }
            return -1;
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
