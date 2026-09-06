using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

namespace CirkusLuna.ClassLibrary.Service
{
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;
        private IShowRepository _showRepository;

        public ReservationService(IReservationRepository reservationRepository, IShowRepository showRepository)
        {
            _reservationRepository = reservationRepository;
            _showRepository = showRepository;
        }

        public bool CreateReservation(Reservation reservation)
        {
            //is the show in the future
            if (reservation.Show.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                return false; //show is in the past
            }

            //count booked seats
            List<Reservation> currentReservations = _reservationRepository.GetByShow(reservation.Show.Id);
            int bookedSeats = 0;
            int bookedVipSeats = 0;

            for (int i = 0; i < currentReservations.Count; i++)
            {
                if (currentReservations[i].TicketType == TicketType.VIP)
                {
                    bookedVipSeats += currentReservations[i].TotalSeats;
                }
                else
                {
                    bookedSeats += currentReservations[i].TotalSeats;
                }
            }

            //check available seats
            if (reservation.TicketType == TicketType.VIP)
            {
                if (bookedVipSeats + reservation.TotalSeats > reservation.Show.VipSeats)
                {
                    return false; //Not enough VIP seats
                }
            }
            else
            {
                if (bookedSeats + reservation.TotalSeats > reservation.Show.Seats)
                {
                    return false; //not enough standard seats
                }
            }
            _reservationRepository.Add(reservation);
            return true;
        }

        public List<Reservation> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public List<Reservation> GetByCustomer(int id)
        {
            return _reservationRepository.GetByCustomer(id);
        }

        public List<Reservation> GetByShow(int id)
        {
            return _reservationRepository.GetByShow(id);
        }

        //Find og returner reservation på ID
        public Reservation GetById(int id)
        {
            return _reservationRepository.GetById(id);
        }

        //Opdater antal billetter - tjek tilgængelighed først
        public bool UpdateReservationTickets(int reservationId, int newTicketAmount)
        {
            Reservation reservation = _reservationRepository.GetById(reservationId);

            //Tæller bookede pladser for show, ekskluderer denne reservation
            int bookedSeats = 0;
            foreach (Reservation r in _reservationRepository.GetByShow(reservation.Show.Id))
            {
                if (r.ReservationId != reservation.ReservationId)
                {
                    bookedSeats += r.TotalSeats;
                }
            }
            //Tjek om der er nok pladser af den rigtige type
            int availableSeats = 0;
            if (reservation.TicketType == TicketType.VIP)
            {
                availableSeats = reservation.Show.VipSeats;
            }
            else
            {
                availableSeats = reservation.Show.Seats;
            }

            if (bookedSeats + newTicketAmount <= availableSeats)
            {
                reservation.TotalSeats = newTicketAmount;
                _reservationRepository.Update(reservation);
                return true;
            }
            return false;
        }

        // Opdaterer kundeoplysninger på reservation
        public void UpdateCustomerInfo(int reservationId, string email, string phoneNumber)
        {
            Reservation reservation = _reservationRepository.GetById(reservationId);
            reservation.Customer.Email = email;
            reservation.Customer.PhoneNumber = phoneNumber;
            _reservationRepository.Update(reservation);
        }

        // Sletter reservation på ID
        public void DeleteReservation(int id)
        {
            _reservationRepository.Delete(id);
        }
    }
}