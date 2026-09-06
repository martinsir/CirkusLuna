namespace CirkusLuna.ClassLibrary.Model
{
    public class Reservation
    {
        //Reservation properties

        public int ReservationId { get; set; }
        public DateTime ReservationTime { get; set; }
        public TicketType TicketType { get; set; }
        public int TotalSeats { get; set; }
        public int SeatNumber { get; set; }

        // reservation you can directly access reservation.Customer.FullName or reservation.Show.Date
        //Signaling datatype Customer and Show that it can be null with a ? operator
        public Customer? Customer { get; set; }

        public Show? Show { get; set; }

        //Constructor
        public Reservation(int reservationId, DateTime reservationTime, TicketType ticketType, int totalSeats, int seatNumber, Customer customer, Show show)
        {
            ReservationId = reservationId;
            ReservationTime = reservationTime;
            TicketType = ticketType;
            TotalSeats = totalSeats;
            SeatNumber = seatNumber;
            Customer = customer;
            Show = show;
        }
    }
}