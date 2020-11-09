using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model
{
    class BookingsRepository
    {

        public List<Booking> bookingsRepository { get; set; }

        public BookingsRepository()
        {
            bookingsRepository = getBookingsRepo();
        }

        public List<Booking> getBookingsRepo()
        {
            List<Booking> listOfBookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                if (connection == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }

                SqlCommand query = new SqlCommand("SELECT * from Bookings", connection);
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Booking b = new Booking();
                    b.Id = (int)row["BookingId"];
                    b.SocialSecurity = row["SocialSecurity"].ToString();
                    b.CarType = row["CarType"].ToString();
                    b.StartDate = (DateTime)row["Date"];
                    b.StartKilometers = (int)row["StartKilometers"];
                   
                    listOfBookings.Add(b);
                }

                return listOfBookings;
            }
        }


        public void addBooking(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                if (connection == null)
                {
                    throw new Exception("Connection string is null or is not working properly.");
                } else if (booking == null){
                    throw new Exception("The passed argument is null");
                }

                SqlCommand query = new SqlCommand("INSERT INTO Bookings (SocialSecurity, CarType, Date, StartKilometers) VALUES (@pSocialSecurity, @pCarType, @pDate, @pKilometers)", connection);
                connection.Open();

                query.CommandType = CommandType.Text;
                query.Parameters.Add("@pSocialSecurity", SqlDbType.Int).Value = booking.SocialSecurity;
                query.Parameters.Add("@pCarType", SqlDbType.VarChar).Value = booking.CarType;
                query.Parameters.Add("@pDate", SqlDbType.DateTime).Value = booking.StartDate;
                query.Parameters.Add("@pKilometers", SqlDbType.Int).Value = booking.StartKilometers;

                query.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void deleteBooking(int id)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                if(connection == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }

                SqlCommand query = new SqlCommand("DELETE FROM BOOKINGS WHERE BookingID = @pBookingId", connection);
                connection.Open();

                query.CommandType = CommandType.Text;
                query.Parameters.Add("@pBookingId", SqlDbType.Int).Value = id;
                query.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}
