using System;
using System.Collections.Generic;
using System.Linq;

namespace SydneyHotel
{
    // UPDATED: Encapsulated reservation details inside a class
    class ReservationDetail
    {
        public string CustomerName { get; set; }
        public int Nights { get; set; }
        public string RoomService { get; set; }
        public double TotalPrice => CalculatePrice(); // UPDATED: Auto-calculate price

        // UPDATED: Moved price calculation logic inside class
        private double CalculatePrice()
        {
            double price = Nights switch
            {
                <= 3 => 100 * Nights,
                <= 10 => 80.5 * Nights,
                _ => 75.3 * Nights
            };
            return RoomService.ToLower() == "yes" ? price * 1.1 : price;
        }
    }

    class Program
    {
        // UPDATED: Generalized user input function to reduce repetition
        static string GetInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        // UPDATED: Added input validation for numeric values
        static int GetValidNumber(string message, int min, int max)
        {
            int value;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);
            return value;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(".................Welcome to Sydney Hotel...............");
            int customerCount = GetValidNumber("Enter the number of Customers: ", 1, 100); // UPDATED: Uses validated input
            Console.WriteLine("\n--------------------------------------------------------------------\n");

            List<ReservationDetail> reservations = new List<ReservationDetail>(); // UPDATED: Using List instead of array


            for (int i = 0; i < customerCount; i++)
            {
                var reservation = new ReservationDetail
                {
                    CustomerName = GetInput("Enter customer name: "),
                    Nights = GetValidNumber("Enter the number of nights (1-20): ", 1, 20),
                    RoomService = GetInput("Enter yes/no for room service: ")
                };

                reservations.Add(reservation);
                Console.WriteLine($"The total price for {reservation.CustomerName} is ${reservation.TotalPrice}");
                Console.WriteLine("\n--------------------------------------------------------------------");
            }

            // UPDATED: Using LINQ to find min and max spending customers
            var minReservation = reservations.OrderBy(r => r.TotalPrice).First();
            var maxReservation = reservations.OrderByDescending(r => r.TotalPrice).First();

            Console.WriteLine("\n\t\t\t\tSummary of reservation");
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("Name\t\tNumber of nights\t\tRoom service\t\tCharge");
            Console.WriteLine($"{minReservation.CustomerName}\t\t{minReservation.Nights}\t\t\t{minReservation.RoomService}\t\t\t${minReservation.TotalPrice}");
            Console.WriteLine($"{maxReservation.CustomerName}\t\t{maxReservation.Nights}\t\t\t{maxReservation.RoomService}\t\t\t${maxReservation.TotalPrice}");
            Console.WriteLine("\n--------------------------------------------------------------------\n");

            Console.WriteLine($"The customer spending most is {maxReservation.CustomerName} with ${maxReservation.TotalPrice}");
            Console.WriteLine($"The customer spending least is {minReservation.CustomerName} with ${minReservation.TotalPrice}");
            Console.WriteLine("Press any key to continue....");
            Console.ReadLine();
        }
    }
}
