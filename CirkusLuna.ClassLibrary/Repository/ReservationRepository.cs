using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private List<Reservation> _reservations = new List<Reservation>();

        //Sender repositories ind som parametre, så der kan hentes Show og Customer data
        public ReservationRepository(IShowRepository showRepository, ICustomerRepository customerRepository)
        {
            //Efter oprettelse af JSONRepo kan denne constructor i princippet være tom da data nu håndteres af ReservationJSONRepository.
            //Dette bliver ikke loadet mere.
            //Hent shows
            Show show1 = showRepository.GetById(1);
            //Hent customers
            Customer customer1 = customerRepository.GetById(1);
            //Opret reservationer
            Reservation reservation1 = new Reservation(1, new DateTime(2026, 7, 12, 3, 0, 0), TicketType.Standard, 5, 5, customer1, show1);
            _reservations.Add(reservation1);
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation GetById(int id)
        {
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].ReservationId == id)
                {
                    return _reservations[i];
                }
            }
            return null;
        }

        public void Add(Reservation reservation)
        {
            _reservations.Add(reservation);
        }

        public void Update(Reservation reservation)
        {
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].ReservationId == reservation.ReservationId)
                {
                    _reservations[i].ReservationTime = reservation.ReservationTime;
                    _reservations[i].TicketType = reservation.TicketType;
                    _reservations[i].TotalSeats = reservation.TotalSeats;
                    _reservations[i].SeatNumber = reservation.SeatNumber;
                    _reservations[i].Customer = reservation.Customer;
                    _reservations[i].Show = reservation.Show;
                    break; //no point continuing the loop
                }
            }
        }

        public void Delete(int id)
        {
            _reservations.Remove(GetById(id));
        }

        public List<Reservation> GetByCustomer(int id)
        {
            List<Reservation> result = new List<Reservation>();
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Customer.Id == id) //check customers id
                {
                    result.Add(_reservations[i]);
                }
            }
            return result;
        }

        public List<Reservation> GetByShow(int id)
        {
            List<Reservation> result = new List<Reservation>();
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Show.Id == id) //check show id
                {
                    result.Add(_reservations[i]);
                }
            }
            return result;
        }
    }
}