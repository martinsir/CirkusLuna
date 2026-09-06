using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Service;

namespace CirkusLuna.Console.Controller
{
    public class ConsoleController
    {
        // Private fields til alle services
        private IShowService _showService;

        private IReservationService _reservationService;
        private ICustomerService _customerService;
        private IArtistService _artistService;
        private INewsPostService _newsPostService;
        private IEmployeeService _employeeService;

        //Controlleren modtager alle services som parametre, så den ikke selv behøver at oprette dem
        public ConsoleController(
            IShowService showService,
            IReservationService reservationService,
            ICustomerService customerService,
            IArtistService artistService,
            INewsPostService newsPostService,
            IEmployeeService employeeService)
        {
            _showService = showService;
            _reservationService = reservationService;
            _customerService = customerService;
            _artistService = artistService;
            _newsPostService = newsPostService;
            _employeeService = employeeService;
        }

        // -------------------- Employee --------------------

        //Håndterer login logik - returnerer employee hvis fundet, ellers null
        public Employee Login(string password)
        {
            return _employeeService.Login(password);
        }

        //Opretter ny employee og returnerer ny employee (det oprettede objekt)
        public Employee CreateEmployee(string firstName, string lastName, string email, string role)
        {
            return _employeeService.AddEmployee(firstName, lastName, email, role);
        }

        //Returnerer alle employees
        public List<Employee> GetAllEmployees()
        {
            return _employeeService.GetAll();
        }

        // -------------------- Show --------------------

        //Returnerer alle shows
        public List<Show> GetAllShows()
        {
            return _showService.GetAll();
        }

        //Finder show på ID
        public Show GetShowById(int id)
        {
            return _showService.GetById(id);
        }

        //Søger efter shows i en bestemt by
        public List<Show> GetShowsByCity(string cityName)
        {
            return _showService.GetByCity(cityName);
        }

        //Returnerer alle byer sorteret alfabetisk via bubble sort
        public List<City> GetSortedCities()
        {
            return _showService.GetSortedCities();
        }

        //Opretter nyt show og returnerer det oprettede objekt
        public Show CreateShow(string showName, DateOnly date, int seats, int vipSeats, string cityName)
        {
            return _showService.AddShow(showName, date, seats, vipSeats, cityName);
        }

        //Opdaterer showets navn
        public void UpdateShowName(int id, string showName)
        {
            _showService.UpdateShowName(id, showName);
        }

        //Opdaterer showets dato
        public void UpdateShowDate(int id, DateOnly date)
        {
            _showService.UpdateShowDate(id, date);
        }

        //Opdaterer antal standard pladser
        public void UpdateShowSeats(int id, int seats)
        {
            _showService.UpdateShowSeats(id, seats);
        }

        //Opdaterer antal VIP pladser
        public void UpdateShowVipSeats(int id, int vipSeats)
        {
            _showService.UpdateShowVipSeats(id, vipSeats);
        }

        //Opdaterer showets by
        public void UpdateShowCity(int id, string cityName)
        {
            _showService.UpdateShowCity(id, cityName);
        }

        //Sletter show på ID
        public void DeleteShow(int id)
        {
            _showService.DeleteShow(id);
        }

        // -------------------- Reservation --------------------

        //Returnerer alle reservationer
        public List<Reservation> GetAllReservations()
        {
            return _reservationService.GetAll();
        }

        //Returnerer alle reservationer for et bestemt show - bruges til at beregne ledige pladser
        public List<Reservation> GetReservationsByShow(int showId)
        {
            return _reservationService.GetByShow(showId);
        }

        //Koordinerer oprettelse af kunde og reservation på tværs af to services
        //Returnerer true hvis reservation lykkedes, false hvis ikke
        public bool CreateReservation(Show chosenShow, string firstName, string lastName,
            string email, string phoneNumber, TicketType ticketType, int ticketAmount)
        {
            //Trin 1 - Opret kunde via customerService
            Customer newCustomer = _customerService.AddCustomer(firstName, lastName, email, phoneNumber);

            //Trin 2 - Beregn næste sædenummer baseret på eksisterende reservationer
            int nextSeatNumber = 1;
            foreach (Reservation r in _reservationService.GetByShow(chosenShow.Id))
            {
                nextSeatNumber += r.TotalSeats;
            }

            //Trin 3 - Opret reservation
            int reservationId = _reservationService.GetAll().Count + 1;
            Reservation newReservation = new Reservation(
                reservationId,
                new DateTime(chosenShow.Date.Year, chosenShow.Date.Month, chosenShow.Date.Day),
                ticketType,
                ticketAmount,
                nextSeatNumber,
                newCustomer,
                chosenShow
            );

            //Trin 4 - Send til reservationService som validerer og gemmer
            return _reservationService.CreateReservation(newReservation);
        }

        //Opdaterer antal billetter på reservation - returnerer true ved success
        public bool UpdateReservationTickets(int reservationId, int newTicketAmount)
        {
            return _reservationService.UpdateReservationTickets(reservationId, newTicketAmount);
        }

        //Opdaterer kundeoplysninger på reservation
        public void UpdateReservationCustomerInfo(int reservationId, string email, string phoneNumber)
        {
            _reservationService.UpdateCustomerInfo(reservationId, email, phoneNumber);
        }

        //Sletter reservation på ID
        public void DeleteReservation(int id)
        {
            _reservationService.DeleteReservation(id);
        }

        // -------------------- Customer --------------------

        //Returnerer alle kunder
        public List<Customer> GetAllCustomers()
        {
            return _customerService.GetAll();
        }

        //Opdaterer kundens navn
        public void UpdateCustomerName(int id, string firstName, string lastName)
        {
            _customerService.UpdateName(id, firstName, lastName);
        }

        //Opdaterer kundens email
        public void UpdateCustomerEmail(int id, string email)
        {
            _customerService.UpdateEmail(id, email);
        }

        //Opdaterer kundens telefonnummer
        public void UpdateCustomerPhoneNumber(int id, string phoneNumber)
        {
            _customerService.UpdatePhoneNumber(id, phoneNumber);
        }

        // -------------------- Artist --------------------

        //Returnerer alle artister
        public List<Artist> GetAllArtists()
        {
            return _artistService.GetAll();
        }

        //Finder artist på ID
        public Artist GetArtistById(int id)
        {
            return _artistService.GetById(id);
        }

        //Opretter ny artist og returnerer det oprettede objekt
        public Artist CreateArtist(string firstName, string lastName, string email, string act)
        {
            return _artistService.AddArtist(firstName, lastName, email, act);
        }

        //Opdaterer artistens navn
        public void UpdateArtistName(int id, string firstName, string lastName)
        {
            _artistService.UpdateName(id, firstName, lastName);
        }

        //Opdaterer artistens email
        public void UpdateArtistEmail(int id, string email)
        {
            _artistService.UpdateEmail(id, email);
        }

        //Opdaterer artistens act
        public void UpdateArtistAct(int id, string act)
        {
            _artistService.UpdateAct(id, act);
        }

        //Sletter artist på ID
        public void DeleteArtist(int id)
        {
            _artistService.DeleteArtist(id);
        }

        // -------------------- NewsPost --------------------

        //Returnerer alle newsposts
        public List<NewsPost> GetAllNewsPosts()
        {
            return _newsPostService.GetAll();
        }

        // Opretter ny nnewspost
        public NewsPost CreateNewsPost(string title, string content)
        {
            return _newsPostService.AddNewsPost(title, content);
        }

        // Sletter newsposts på ID
        public void DeleteNewsPost(int id)
        {
            _newsPostService.DeleteNewsPost(id);
        }
    }
}