using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using CirkusLuna.ClassLibrary.Service;
using CirkusLuna.Console.Controller;

//Repositories
IArtistRepository artistRepository = new ArtistJSONRepository();
IShowRepository showRepository = new ShowJSONRepository();
IEmployeeRepository employeeRepository = new EmployeeRepository();
ICustomerRepository customerRepository = new CustomerJSONRepository();
IReservationRepository reservationRepository = new ReservationJSONRepository();
INewsPostRepository newsPostRepository = new NewsPostJSONRepository();

//Services
IReservationService reservationService = new ReservationService(reservationRepository, showRepository);
ICustomerService customerService = new CustomerService(customerRepository);
IArtistService artistService = new ArtistService(artistRepository);
IShowService showService = new ShowService(showRepository);
INewsPostService newsPostService = new NewsPostService(newsPostRepository);
IEmployeeService employeeService = new EmployeeService(employeeRepository);

//Controller - modtager alle services
ConsoleController controller = new ConsoleController(
    showService, reservationService, customerService,
    artistService, newsPostService, employeeService);

//Dictionary der mapper billettype til pris
Dictionary<TicketType, double> ticketPrices = new Dictionary<TicketType, double>
{
    { TicketType.Standard, 149.95},
    { TicketType.VIP, 299.95}
};

while (true)
{
    //Indledning
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.WriteLine("--------------------- Velkommen til Cirkus Luna ---------------------\n");
    Console.ResetColor();

    Console.WriteLine("------------------ Menu ------------------");

    Console.WriteLine("Tast 1 - Se alle fremtidige forestillinger");
    Console.WriteLine("Tast 2 - Søg efter den næste forestilling i en by");
    Console.WriteLine("Tast 3 - Se oversigt over alle kommende shows i diverse byer, sorteret alfabetisk!");
    Console.WriteLine("Tast 4 - Se de seneste nyheder!");

    Console.WriteLine("\nLog ind som medarbejder - Tast 5\n");

    //Søg efter forestillinger
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        //Kald DisplayShows();
        DisplayShows();

        //Initier billetreservation
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.Write("Bestil billetter nu! - Tast 1");
        Console.ResetColor();
        Console.WriteLine(); //Også her, ellers fortsatte BackgroundColor...

        string createReservation = Console.ReadLine();

        if (createReservation == "1")
        {
            Console.WriteLine("Indtast SHOW NUMMER på det show du ønsker at bestille billetter til!");
            int showId = ReadInt();
            Show chosenShow = controller.GetShowById(showId);

            //Tjekker at indtastet showId er valid
            if (chosenShow == null)
            {
                Console.Write("Show ikke fundet, prøv at indtast det rette nummer igen.");
            }

            //Opret kunde - kald CreateReservation(chosenShow)
            else
            {
                CreateReservation(chosenShow);
            }
        }
    }
    else if (choice == "2")
    {
        //Søg efter show i en bestemt by
        Console.WriteLine("Indtast bynavn");
        string cityInput = Console.ReadLine();
        List<Show> shows = controller.GetShowsByCity(cityInput);

        if (shows.Count == 0)
        {
            Console.WriteLine($"Ingen forestillinger fundet i {cityInput}.");
        }
        else
        {
            foreach (Show show in shows)
            {
                Console.WriteLine($"\n{show.ShowName} kommer til {show.City.Name} d. {show.Date}. \n Følgende stjerner optræder:");
                foreach (Artist artist in show.Artists)
                {
                    Console.WriteLine($"{artist.Act}, {artist.FullName}");
                }
                Console.WriteLine($"Der er {show.Seats} antal ledige pladser og {show.VipSeats} VIP pladser. Book nu mens der stadig er ledige biletter!");
            }
        }
    }
    else if (choice == "3")
    {
        DisplaySortedCities();
    }
    else if (choice == "4")
    {
        DisplayNews();
    }

    //Medarbejder "LogIn"
    else if (choice == "5")
    {
        Console.WriteLine("Angiv venligst dit medarbejder password:");
        string employeePassword = Console.ReadLine();
        //Ny medarbejder logIn
        Employee loggedIn = controller.Login(employeePassword);

        //Hvis loggedIn ikke er null (Medarbejder fundet)
        if (loggedIn != null)
        {
            //Sender medarbejder til "Profil"
            Console.WriteLine($"Velkommen til din profil {loggedIn.FullName}.");
            Console.WriteLine($"Din information:\n{loggedIn.Role}\n{loggedIn.Email}\n");

            //While loop for at holde konsol kørende
            bool employeeActive = true;
            while (employeeActive)
            {
                Console.WriteLine("\n----- Menu -----\n Vælg handling ved at angive nr. \n");
                Console.WriteLine("1 - Vis liste over forestillinger");
                Console.WriteLine("2 - Vis liste over kunder");
                Console.WriteLine("3 - Vis liste over artister");
                Console.WriteLine("4 - Vis liste over reservationer");
                Console.WriteLine("5 - Vis liste over nyheder");
                Console.WriteLine("6 - Vis liste over medarbejdere");

                Console.WriteLine("\n0 - Log ud");

                string employeeChoice = Console.ReadLine();

                if (employeeChoice == "1")
                {
                    DisplayShows();
                    Console.WriteLine("1 - Vil du oprette et nyt show?");
                    Console.WriteLine("2 - Vil du ændre et eksisterende show?");
                    Console.WriteLine("3 - Vil du slette et eksisterende show?");

                    Console.WriteLine("\n0 - Gå tilbage til Menu");

                    string employeeEditShow = Console.ReadLine();

                    if (employeeEditShow == "1")
                    {
                        CreateShow();
                    }
                    else if (employeeEditShow == "2")
                    {
                        UpdateShow();
                    }
                    else if (employeeEditShow == "3")
                    {
                        DeleteShow();
                    }
                }
                else if (employeeChoice == "2")
                {
                    DisplayCustomers();
                    Console.WriteLine("1 - Ønsker du at ændre oplysninger på en kunde?");
                    Console.WriteLine("0 - Gå tilbage til Menu");
                    string employeeEditCustomer = Console.ReadLine();
                    if (employeeEditCustomer == "1")
                    {
                        UpdateCustomer();
                    }
                    else if (employeeEditCustomer == "0")
                    {
                        //Returnerer til employeeActive
                    }
                }
                else if (employeeChoice == "3")
                {
                    DisplayArtists();
                    Console.WriteLine("1 - Ønsker du at ændre oplysninger på en Artist?");
                    Console.WriteLine("2 - Ønsker du at oprette en ny Artist?");
                    Console.WriteLine("3 - Øsker du at fyre (slette) en artist?");

                    Console.WriteLine("\n0 - Gå tilbage til Menu");
                    string employeeEditArtist = Console.ReadLine();
                    if (employeeEditArtist == "1")
                    {
                        UpdateArtist();
                    }
                    else if (employeeEditArtist == "2")
                    {
                        CreateArtist();
                    }
                    else if (employeeEditArtist == "3")
                    {
                        DeleteArtist();
                    }
                    else if (employeeEditArtist == "0")
                    {
                        //Returnerer til employeeActive
                    }
                }
                else if (employeeChoice == "4")
                {
                    DisplayReservation();
                    Console.WriteLine("1 - Vil du oprette en reservation til et bestemt show?");
                    Console.WriteLine("2 - Vil du ændre en eksisterende reservation?");
                    Console.WriteLine("3 - Vil du slette en eksisterende reservation?");

                    Console.WriteLine("\n0 - Gå tilbage til Menu");
                    string employeeEditReservation = Console.ReadLine();
                    if (employeeEditReservation == "1")
                    {
                        Console.WriteLine("Angiv ID på det show du vil oprette en reservation til: ");
                        int employeeCreateReservation = ReadInt();
                        Show chosenShow = controller.GetShowById(employeeCreateReservation);
                        CreateReservation(chosenShow);
                    }
                    else if (employeeEditReservation == "2")
                    {
                        UpdateReservation();
                    }
                    else if (employeeChoice == "3")
                    {
                        DeleteReservation();
                    }
                }
                else if (employeeChoice == "5")
                {
                    DisplayNews();
                    Console.WriteLine("1 - Vil du oprette en ny Post?: ");
                    Console.WriteLine("2 - Vil du slette en Post?: ");

                    string manageNewsPost = Console.ReadLine();
                    if (manageNewsPost == "1")
                    {
                        CreateNewsPost();
                    }
                    else if (manageNewsPost == "2")
                    {
                        DeleteNewsPost();
                    }
                }
                else if (employeeChoice == "6")
                {
                    DisplayEmployees();
                    Console.WriteLine("1 - Vil du oprette en ny medarbejder?");
                    Console.WriteLine("2 - Vil du ændre oplysninger på en eksisterende medarbejder?");
                    string employeeEditEmployee = Console.ReadLine();

                    if (employeeEditEmployee == "1")
                    {
                        CreateEmployee();
                    }
                    else if (employeeEditEmployee == "2")
                    {
                        //UpdateEmployee();
                    }
                }
                else if (employeeChoice == "0")
                {
                    employeeActive = false;
                }
            }
        }
    }

    //Funktioner
    // -------------------- Display() funktioner ----------------------------

    void DisplayShows()
    {
        foreach (Show show in controller.GetAllShows())
        {
            int bookedStandardSeats = 0;
            int bookedVipSeats = 0;
            foreach (Reservation r in controller.GetReservationsByShow(show.Id))
            {
                if (r.TicketType == TicketType.VIP)
                {
                    bookedVipSeats += r.TotalSeats;
                }
                else
                {
                    bookedStandardSeats += r.TotalSeats;
                }
            }

            int remainingStandard = show.Seats - bookedStandardSeats;
            int remainingVip = show.VipSeats - bookedVipSeats;

            //Display til konsol
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write($"SHOW NUMMER [{show.Id}]");
            Console.ResetColor();
            Console.WriteLine(); //Plads til ResetColor();

            Console.WriteLine($"{show.ShowName} i {show.City.Name} d. {show.Date}" +
                $"\nDer er {remainingStandard} standard pladser og {remainingVip} VIP pladser tilbage! Skynd dig at bestille inden de er udsolgt! " +
                $"\nKom og oplev aftenens stjerner:\n");

            foreach (Artist artist in show.Artists)
            {
                Console.WriteLine($"{artist.Act}, {artist.FullName}");
            }
            Console.WriteLine("------------------------------------");
        }
    }

    void DisplayReservation()
    {
        foreach (Reservation reservation in controller.GetAllReservations())
        {
            Console.WriteLine($"Reservation [{reservation.ReservationId}] - Kunde: {reservation.Customer.FullName}, mail: {reservation.Customer.Email}, Antal billetter: {reservation.TotalSeats}");
        }
    }

    void DisplayCustomers()
    {
        foreach (Customer customer in controller.GetAllCustomers())
        {
            Console.WriteLine($"Kunde nr: [{customer.Id}] - {customer.FullName} - {customer.Email} - {customer.PhoneNumber}");
        }
    }

    void DisplayArtists()
    {
        foreach (Artist artist in controller.GetAllArtists())
        {
            Console.WriteLine($"[{artist.Id}] {artist.Act}, {artist.FullName}");
        }
    }

    void DisplayNews()
    {
        foreach (NewsPost post in controller.GetAllNewsPosts())
        {
            Console.WriteLine($"[{post.NewsPostId}] | {post.Title} | {post.Content} - Udgivet d. {post.PublishedDateTime}");
        }
    }

    void DisplayEmployees()
    {
        foreach (Employee employee in controller.GetAllEmployees())
        {
            Console.WriteLine($"Medarbejder nr: [{employee.Id}] Navn: {employee.FullName} | Stilling: {employee.Role} | Kontakt: {employee.Email}");
        }
    }

    void DisplaySortedCities()
    {
        Console.WriteLine("\n--- Byer med forestillinger (A-Å) ---");
        foreach (City city in controller.GetSortedCities())
        {
            Console.WriteLine($"[{city.Id}]- {city.Name}");
        }
    }

    // -------------------- Create() funktioner ----------------------------

    //Funktion til at oprette reservation ud fra chosenShow
    void CreateReservation(Show chosenShow)
    {
        Console.WriteLine("Indtast navn: ");
        string firstName = Console.ReadLine();

        Console.WriteLine("Indtast efternavn: ");
        string lastName = Console.ReadLine();

        Console.WriteLine("Indtast email: ");
        string email = Console.ReadLine();

        Console.WriteLine("Indtast telefonnummer: ");
        string phoneNumber = Console.ReadLine();

        //Kalder AddCustomer() fra servicelag
        Customer newCustomer = customerService.AddCustomer(firstName, lastName, email, phoneNumber);

        //Vælg billettype
        Console.WriteLine("Vælg billettype - Standard (1) eller VIP (2)");
        string ticketChoice = Console.ReadLine();
        TicketType ticketType;

        //Simpel if statement til at afgøre billettype
        if (ticketChoice == "2")
        {
            ticketType = TicketType.VIP;
        }
        else
        {
            ticketType = TicketType.Standard;
        }

        //Vælg antal biletter
        Console.WriteLine("Hvor mange billetter ønsker du?:  ");
        int ticketAmount = ReadInt();

        //Controller koordinerer oprettelse af kunde og reservation
        bool createCustomerSuccess = controller.CreateReservation(chosenShow, firstName, lastName, email, phoneNumber, ticketType, ticketAmount);

        if (createCustomerSuccess)
        {
            //Slår prisen op i dictionary baseret på billettype
            double pricePerTicket = ticketPrices[ticketType];
            double totalPrice = pricePerTicket * ticketAmount;

            Console.WriteLine($"\nTak {newCustomer.FirstName}! Din reservation er oprettet. Her er din kvittering: ");
            Console.WriteLine($"Show: {chosenShow.ShowName} i {chosenShow.City.Name} d. {chosenShow.Date}");
            Console.WriteLine($"Billettype: {ticketType}, antal {ticketAmount}.");
            Console.WriteLine($"Pris pr. billet: {pricePerTicket} kr. — Total: {totalPrice} kr.");
        }
        else
        {
            Console.WriteLine("Reservationen kunne ikke oprettes - ingen ledige pladser eller showet er allerede afholdt.");
        }
    }

    void CreateArtist()
    {
        Console.WriteLine("Indtast navn på ny artist: ");
        string firstName = Console.ReadLine();
        Console.WriteLine("Indtast efternavn på ny artist: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Indtast email på ny artist: ");
        string email = Console.ReadLine();
        Console.WriteLine("Indtast Act på ny artist: ");
        string act = Console.ReadLine();

        Artist newArtist = controller.CreateArtist(firstName, lastName, email, act);
        Console.WriteLine($"Artist nr: [{newArtist.Id}] - {newArtist.FirstName} {newArtist.LastName}, {newArtist.Act} er nu oprettet i systemet! ");
    }

    void CreateEmployee()
    {
        Console.WriteLine("Angiv fornavn på ny medarbejder: ");
        string firstName = Console.ReadLine();
        Console.WriteLine("Angiv efternavn på ny medarbejder: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Angiv email på ny medarbejder: ");
        string email = Console.ReadLine();
        Console.WriteLine($"Angiv hvilken stilling {firstName} skal have?: ");
        string role = Console.ReadLine();

        //Kald til service og opret ny medarbejder.
        Employee newEmployee = controller.CreateEmployee(firstName, lastName, email, role);

        Console.WriteLine($"Velkommen til ny {newEmployee.Role} - {newEmployee.FullName}, {newEmployee.Email}.\nMedarbejder password: {newEmployee.Password}");
    }

    void CreateNewsPost()
    {
        Console.WriteLine("Angiv titel på post: ");
        string newsTitle = Console.ReadLine();
        Console.WriteLine("Skriv postens indhold: ");
        string newsContent = Console.ReadLine();

        NewsPost newPost = controller.CreateNewsPost(newsTitle, newsContent);

        Console.WriteLine($"Din post [{newPost.NewsPostId}] er oprettet d. {newPost.PublishedDateTime}\n" +
            $"{newsTitle} - {newsContent}");
    }

    void CreateShow()
    {
        Console.WriteLine("Indtast titel på nyt show: ");
        string showName = Console.ReadLine();
        Console.WriteLine("Angiv dato for show i YYYY-MM-DD format:");
        string showDateInput = Console.ReadLine();
        DateOnly newDate = DateOnly.Parse(showDateInput);
        Console.WriteLine("Angiv hvor mange standard pladser showet skal have: ");
        int showStandardTickets = ReadInt();
        Console.WriteLine("Angiv hvor mange VIP pladser showet skal have: ");
        int showVipTickets = ReadInt();
        Console.WriteLine($"Angiv by hvor showet skal finde sted: ");
        string cityName = Console.ReadLine();

        Show newShow = controller.CreateShow(showName, newDate, showStandardTickets, showVipTickets, cityName);

        Console.WriteLine($"{newShow.ShowName} er oprettet og afholdes d. {newShow.Date}. Der er {showStandardTickets} standard pladser og {showVipTickets} VIP pladser." +
            $"\n Forestillingen finder sted i {newShow.City.Name}");
    }

    // -------------------- Update() funktioner ----------------------------

    //UpdateCustomer
    void UpdateCustomer()
    {
        //Finder eksisterende kunde på ID
        Console.WriteLine("Skriv ID på kunde der skal ændres: ");
        int existingCustomerId = ReadInt();

        //Holder update() kørende
        bool updateCustomer = true;
        while (updateCustomer == true)
        {
            //Oversigt over mulige handlinger
            Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
            Console.WriteLine("1 - Ændre Fornavn + Efternavn");
            Console.WriteLine("2 - Ændre Email");
            Console.WriteLine("3 - Ændre telefon nr.");
            //Console.WriteLine("4 - Ændre kundestatus: Standard / VIP\n"); - Lige udkommenteret, den skal ikke være tilgængelig for customer

            Console.WriteLine("0 - Afslut ændringer");

            int updateCustomerChoice = ReadInt();

            if (updateCustomerChoice == 1)
            {
                Console.WriteLine("Indtast nyt fornavn:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Indtast nyt efternavn:");
                string lastName = Console.ReadLine();
                //Opdater ændringer
                controller.UpdateCustomerName(existingCustomerId, firstName, lastName);
            }
            else if (updateCustomerChoice == 2)
            {
                Console.WriteLine("Indtast ny email:");
                string email = Console.ReadLine();
                controller.UpdateCustomerEmail(existingCustomerId, email);
            }
            else if (updateCustomerChoice == 3)
            {
                Console.WriteLine("Indtast nyt telefon nr.:");
                string PhoneNumber = Console.ReadLine();
                controller.UpdateCustomerPhoneNumber(existingCustomerId, PhoneNumber);
            }
            else if (updateCustomerChoice == 0)
            {
                updateCustomer = false;
            }
        }
    }

    void UpdateArtist()
    {
        //Finder eksisterende Artist på ID
        Console.WriteLine("Skriv ID på artist der skal ændres: ");
        int existingArtistId = ReadInt();

        //Holder update() kørende
        bool updateArtist = true;
        while (updateArtist == true)
        {
            //Oversigt over mulige handlinger
            Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
            Console.WriteLine("1 - Ændre Fornavn + Efternavn");
            Console.WriteLine("2 - Ændre Email");
            Console.WriteLine("3 - Ændre artist Act\n");

            Console.WriteLine("0 - Afslut ændringer");

            int updateArtistChoice = ReadInt();

            if (updateArtistChoice == 1)
            {
                Console.WriteLine("Indtast nyt fornavn:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Indtast nyt efternavn:");
                string lastName = Console.ReadLine();
                //Opdater ændringer
                controller.UpdateArtistName(existingArtistId, firstName, lastName);
            }
            else if (updateArtistChoice == 2)
            {
                Console.WriteLine("Indtast ny email:");
                string email = Console.ReadLine();
                controller.UpdateArtistEmail(existingArtistId, email);
            }
            else if (updateArtistChoice == 3)
            {
                Console.WriteLine("Indtast ny Act:");
                string act = Console.ReadLine();
                controller.UpdateArtistAct(existingArtistId, act);
            }
            else if (updateArtistChoice == 0)
            {
                updateArtist = false;
            }
        }
    }

    void UpdateReservation()
    {
        //Finder eksisterende Artist på ID
        Console.WriteLine("Skriv ID på den reservation der skal ændres: ");
        int existingReservationId = ReadInt();

        //Holder update() kørende
        bool updateReservation = true;
        while (updateReservation == true)
        {
            //Oversigt over mulige handlinger
            Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
            Console.WriteLine("1 - Ændre kundeoplysninger (Email og tlf.)");
            Console.WriteLine("2 - Ændre antal billetter: ");
            //Console.WriteLine("3 - Ændre billettype (Standard/VIP): "); - ikke skrevet
            //Console.WriteLine("4 - Ændre til et andet show"); - Gemmer den her, hvis vi ønsker at implementere den

            Console.WriteLine("0 - Afslut ændringer");

            int updateReservationChoice = ReadInt();

            if (updateReservationChoice == 1)
            {
                Console.WriteLine("Indtast ny email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Indtast nyt tlf nummer: ");
                string phoneNumber = Console.ReadLine();
                //Opdater ændringer
                controller.UpdateReservationCustomerInfo(existingReservationId, email, phoneNumber);
            }
            else if (updateReservationChoice == 2)
            {
                Console.WriteLine("Indtast ønsket antal billetter: ");
                int newTicketAmount = ReadInt();
                bool success = controller.UpdateReservationTickets(existingReservationId, newTicketAmount);
                if (success)
                    Console.WriteLine($"Billetter opdateret til {newTicketAmount}.");
                else
                    Console.WriteLine("Der er ikke nok ledige billetter.");
            }
            else if (updateReservationChoice == 0)
            {
                updateReservation = false;
            }
        }
    }

    void UpdateShow()
    {
        Console.WriteLine("Angiv ID på det show der skal ændres: ");
        int existingShowId = ReadInt();

        //Holder update() kørende
        bool updateShow = true;
        while (updateShow == true)
        {
            //Oversigt over mulige handlinger
            Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
            Console.WriteLine("1 - Ændre navn på show: ");
            Console.WriteLine("2 - Ændre dato for show: ");
            Console.WriteLine("3 - Ændre antal ledige standard pladser: ");
            Console.WriteLine("4 - Ændre antal ledige VIP pladser: ");
            Console.WriteLine("5 - Ændre by for show: ");

            Console.WriteLine("\n0 - Gå tilbage til Menu. ");

            string updateShowChoice = Console.ReadLine();

            if (updateShowChoice == "1")
            {
                Console.WriteLine("Angiv nyt navn til show: ");
                string showName = Console.ReadLine();
                controller.UpdateShowName(existingShowId, showName);
                Console.WriteLine($"Du har ændret navnet til {showName}");
            }
            else if (updateShowChoice == "2")
            {
                Console.WriteLine("Angiv ny dato for show i YYYY-MM-DD format:");
                DateOnly date = DateOnly.Parse(Console.ReadLine());
                controller.UpdateShowDate(existingShowId, date);

                Console.WriteLine($"Dato ændret til {date}");
            }
            else if (updateShowChoice == "3")
            {
                Console.WriteLine("Angiv hvor mange ledige standard pladser der skal være: ");
                int updatedStandardSeat = ReadInt();
                controller.UpdateShowSeats(existingShowId, updatedStandardSeat);
                Console.WriteLine($"Du har oprettet {updatedStandardSeat} standard pladser. ");
            }
            else if (updateShowChoice == "4")
            {
                Console.WriteLine("Angiv hvor mange ledige VIP pladser der skal være: ");
                int UpdatedVipSeat = ReadInt();
                controller.UpdateShowVipSeats(existingShowId, UpdatedVipSeat);
                Console.WriteLine($"Du har oprettet {UpdatedVipSeat} VIP pladser. ");
            }
            else if (updateShowChoice == "5")
            {
                Show show = showService.GetById(existingShowId); //Reference så der kan hentes data fra show (til {show.City.Name})
                Console.WriteLine($"Hvor skal showet finde sted istedet for {show.City.Name}?: ");
                string UpdatedShowCity = Console.ReadLine();
                controller.UpdateShowCity(existingShowId, UpdatedShowCity);

                Console.WriteLine($"Showet er flyttet fra {show.City.Name} til {UpdatedShowCity}");
            }
            //Afslut updateShow
            else if (updateShowChoice == "0")
            {
                updateShow = false;
            }
        }
    }

    // -------------------- Delete() funktioner ----------------------------

    void DeleteReservation()
    {
        Console.WriteLine("Angiv ID på den reservation du ønsker at slette: ");
        int reservationId = ReadInt();
        controller.DeleteReservation(reservationId);
        Console.WriteLine("Reservationen er slettet.");
    }

    void DeleteNewsPost()
    {
        Console.WriteLine("Angiv ID på den news post der skal slettes: ");
        int newsPostId = ReadInt();
        NewsPost newsPost = newsPostService.GetById(newsPostId);
        controller.DeleteNewsPost(newsPostId);
        Console.WriteLine("Post slettet.");
    }

    void DeleteArtist()
    {
        Console.WriteLine("Angiv ID på den artist der skal slettes: ");
        int artistId = ReadInt();
        Artist artist = artistService.GetById(artistId);
        controller.DeleteArtist(artistId);
        Console.WriteLine($"Artist {artist.FullName} er fyret.");
    }

    void DeleteShow()
    {
        Console.WriteLine("Angiv ID på det show der skal slettes: ");
        //Exception - hvis brugeren indtaster et bogstav istedet for et tal

        int showId = ReadInt();

        //Find show i service
        Show show = controller.GetShowById(showId);
        Console.WriteLine($"Er du sikker på at du vil slette {show.ShowName}? Denne handling kan ikke fortrydes:\n" +
            "1 - Ja, slet valgt show.\n" +
            "2 - Nej, fortryd og gå tilbage til menu. ");
        int deleteShowChoice = ReadInt();
        if (deleteShowChoice == 1)
        {
            controller.DeleteShow(showId);
            Console.WriteLine("Show slettet.");
        }
        else if (deleteShowChoice == 2)
        {
            Console.WriteLine("Valg fortrudt. Går tilbage til menu.");
        }
    }

    //Hjælpefunktion der håndterer int input fra brugeren
    //Try/catch bruges til at fange fejl hvis brugeren indtaster noget der ikke er et tal
    int ReadInt()
    {
        while (true)
        {
            try
            {
                //Forsøger at konvertere input til int
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                //Fanges hvis input ikke kan konverteres til int - f.eks. hvis brugeren skriver "a,b,c"
                Console.WriteLine("Ugyldigt input - indtast venligst et tal.");
            }
        }
    }
} //Ende af While (true)