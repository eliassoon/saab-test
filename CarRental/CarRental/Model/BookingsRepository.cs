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

            return listOfBookings;
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


                SqlCommand query = new SqlCommand("insertBooking", connection);
                connection.Open();
                query.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("pSocialSecurity", SqlDbType.VarChar);
                SqlParameter param2 = new SqlParameter("pCarType", SqlDbType.Int);
                SqlParameter param3 = new SqlParameter("pDate", SqlDbType.DateTime);

                param1.Value = booking.SocialSecurity;
                param2.Value = booking.CarType;
                param3.Value = booking.StartDate;

                query.Parameters.Add(param1);
                query.Parameters.Add(param2);
                query.Parameters.Add(param3);

                query.ExecuteNonQuery();

            }
        }
    }
}
