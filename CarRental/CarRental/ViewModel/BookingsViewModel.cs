﻿using CarRental.Model;
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
            Bookings.CollectionChanged += bookings_CollectionChanged;
        }

        

        private void bookings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                BookingsRepository.addBooking(Bookings[newIndex]);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                List<Booking> tempListOfRemovedItems = e.OldItems.OfType<Booking>().ToList();
                BookingsRepository.deleteBooking(tempListOfRemovedItems[0].Id);
            }
        }

        public void addBookingToRepo(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException("Error: The argument is Null");
            Bookings.Add(booking);
        }

        public void removeBookingFromRepo(Booking booking)
        {
            Bookings.Remove(booking);
        }

        public List<string> getAllActiveBookings()
        {
            return (BookingsRepository.bookingsRepository.Select(booking => booking.Id.ToString())).ToList();
        }

        public double calculatePrice(Booking selected, DateTime dateOfReturn, string kilometersDriven)
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

        public Booking findBooking(string id)
        {
            Booking booking = (from temp in Bookings
                               where temp.Id.Equals(int.Parse(id))
                               select temp).First();
            return booking;
        }

    }
}
