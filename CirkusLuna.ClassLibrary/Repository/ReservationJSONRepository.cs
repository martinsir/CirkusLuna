using CirkusLuna.ClassLibrary.Model;
using System.Text.Json;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ReservationJSONRepository : IReservationRepository
    {
        // JSON persistence saves reservation data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;

        private List<Reservation> _reservations;

        public ReservationJSONRepository(string path)
        {
            _path = path;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                _reservations =
                    JsonSerializer.Deserialize<List<Reservation>>(json) ?? new List<Reservation>();
            }
            else
            {
                _reservations = new List<Reservation>();
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_reservations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
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
                    return _reservations[i];
            }
            return null;
        }

        public List<Reservation> GetByCustomer(int id)
        {
            List<Reservation> result = new List<Reservation>();
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Customer?.Id == id)
                    result.Add(_reservations[i]);
            }
            return result;
        }

        public List<Reservation> GetByShow(int id)
        {
            List<Reservation> result = new List<Reservation>();
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Show?.Id == id)
                    result.Add(_reservations[i]);
            }
            return result;
        }

        public void Add(Reservation reservation)
        {
            _reservations.Add(reservation);
            SaveToFile();
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
                    break;
                }
            }
            SaveToFile();
        }

        public void Delete(int id)
        {
            _reservations.Remove(GetById(id));
            SaveToFile();
        }
    }
}