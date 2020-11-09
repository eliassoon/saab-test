using System;
using System.Collections.Generic;
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
            bookingsRepository = GetBookingsRepo();
        }

        public List<Booking> GetBookingsRepo()
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
                    b.Returned = (bool)row["Returned"];
                   
                    listOfBookings.Add(b);
                }

                return listOfBookings;
            }
        }

        public void AddBooking(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                if (connection == null)
                {
                    throw new Exception("Connection string is null or is not working properly.");
                } else if (booking == null){
                    throw new Exception("The passed argument is null");
                }

                SqlCommand query = new SqlCommand("INSERT INTO Bookings (SocialSecurity, CarType, Date) VALUES (@pSocialSecurity, @pCarType, @pDate)", connection);
                connection.Open();

                query.CommandType = CommandType.Text;
                query.Parameters.Add("@pSocialSecurity", SqlDbType.VarChar).Value = booking.SocialSecurity;
                query.Parameters.Add("@pCarType", SqlDbType.VarChar).Value = booking.CarType;
                query.Parameters.Add("@pDate", SqlDbType.DateTime).Value = booking.StartDate;

                query.ExecuteNonQuery();


            }
        }
    }
}
