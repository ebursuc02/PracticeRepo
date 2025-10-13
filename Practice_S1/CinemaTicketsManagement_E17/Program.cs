using CinemaTicketsManagement_E17;

Movie movie = new Movie("Titanic");

movie.SellTickets(5, "3D", "back", 18.0);
movie.SellTickets(10, "4D", "back", 18.0);
movie.SellTickets(5, "2D", "front", 18.0);

Console.WriteLine($"The value earned from {movie.Name} is {movie.GetTotalEarnedMoney():F2}");
