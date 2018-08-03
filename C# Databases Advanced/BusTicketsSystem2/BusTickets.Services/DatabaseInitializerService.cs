namespace BusTickets.Services
{
    using System;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly BusTicketsContext context;

        public DatabaseInitializerService(BusTicketsContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.EnsureDeleted();
            this.context.Database.Migrate();

            SeedCustomers();
            SeedBankAcounts();
            SeedCountries();
            SeedTowns();
            SeedCompanies();
            SeedStations();
            SeedReviews();
            SeedTrips();
            SeedTickets();
        }

        private void SeedTickets()
        {
            var tickets = new[]
            {
                new Ticket{CustomerId = 1, TripId = 1, Price = 200, Seat = "A1"},
                new Ticket{CustomerId = 5, TripId = 1, Price = 200, Seat = "A5"},
                new Ticket{CustomerId = 3, TripId = 1, Price = 200, Seat = "B8"},
                new Ticket{CustomerId = 4, TripId = 1, Price = 200, Seat = "C3"},
                new Ticket{CustomerId = 1, TripId = 2, Price = 200, Seat = "A1"},
                new Ticket{CustomerId = 5, TripId = 2, Price = 200, Seat = "A5"},
                new Ticket{CustomerId = 3, TripId = 2, Price = 200, Seat = "B8"},
                new Ticket{CustomerId = 4, TripId = 2, Price = 200, Seat = "C3"},
                new Ticket{CustomerId = 1, TripId = 5, Price = 200, Seat = "A1"},
                new Ticket{CustomerId = 2, TripId = 5, Price = 200, Seat = "A5"},
            };

            this.context.Tickets.AddRange(tickets);
            this.context.SaveChanges();
        }

        private void SeedTrips()
        {
            var trips = new[]
            {
                new Trip{DepartureTime = new DateTime(2018, 3, 5, 5, 30, 0), ArrivalTime = new DateTime(2018, 3, 7, 7, 30, 0), Status = TripStatus.Arrived, OriginStationId = 1, DestinationStationId = 4, CompanyId = 1},
                new Trip{DepartureTime = new DateTime(2018, 3, 5, 5, 30, 0), ArrivalTime = new DateTime(2018, 3, 7, 7, 30, 0), Status = TripStatus.Arrived, OriginStationId = 1, DestinationStationId = 4, CompanyId = 1},
                new Trip{DepartureTime = new DateTime(2018, 3, 8, 5, 30, 0), ArrivalTime = new DateTime(2018, 3, 10, 7, 30, 0), Status = TripStatus.Arrived, OriginStationId = 1, DestinationStationId = 3, CompanyId = 1},
                new Trip{DepartureTime = new DateTime(2018, 3, 8, 5, 30, 0), ArrivalTime = new DateTime(2018, 3, 10, 7, 30, 0), Status = TripStatus.Arrived, OriginStationId = 3, DestinationStationId = 1, CompanyId = 1},
                new Trip{DepartureTime = new DateTime(2018, 3, 8, 5, 30, 0), ArrivalTime = new DateTime(2018, 3, 10, 7, 30, 0), Status = TripStatus.Cancelled, OriginStationId = 3, DestinationStationId = 1, CompanyId = 2},
                new Trip{DepartureTime = new DateTime(2018, 5, 8, 5, 30, 0), ArrivalTime = new DateTime(2018, 5, 10, 7, 30, 0), Status = TripStatus.Delayed, OriginStationId = 1, DestinationStationId = 2, CompanyId = 3},

            };

            this.context.Trips.AddRange(trips);
            this.context.SaveChanges();
        }

        private void SeedReviews()
        {
            var reviews = new[]
            {
                new Review{CustomerId = 1, CompanyId = 1, Grade = 10, Content = "Perfect company!"}, 
                new Review{CustomerId = 5, CompanyId = 1, Grade = 9.5, Content = "Very good!"}, 
                new Review{CustomerId = 1, CompanyId = 2, Grade = 2, Content = "Poor company!"}, 
                new Review{CustomerId = 4, CompanyId = 1, Grade = 9.8, Content = "Bravo!"}, 
                new Review{CustomerId = 3, CompanyId = 1, Grade = 10, Content = "Perfect!!!"}, 
                new Review{CustomerId = 2, CompanyId = 2, Grade = 3.5, Content = "Poor work!"}, 
            };

            this.context.Reviews.AddRange(reviews);
            this.context.SaveChanges();
        }

        private void SeedStations()
        {
            var stations = new[]
            {
                new Station{Name = "MadridMain", TownId = 5},
                new Station{Name = "SofiaMain", TownId = 1},
                new Station{Name = "Playa", TownId = 4},
                new Station{Name = "Nord", TownId = 4},
                new Station{Name = "Main", TownId = 4},
            };

            this.context.Stations.AddRange(stations);
            this.context.SaveChanges();
        }

        private void SeedCompanies()
        {
            var companies = new[]
            {
                new Company{Name = "BusTrip2000", NationalityId = 1},
                new Company{Name = "PlovdivTour", NationalityId = 1},
                new Company{Name = "MadridTour", NationalityId = 3},
                new Company{Name = "XXX", NationalityId = 2},
                new Company{Name = "Fun", NationalityId = 2},
            };

            this.context.Companies.AddRange(companies);
            this.context.SaveChanges();
        }

        private void SeedTowns()
        {
            var towns = new[]
            {
                new Town{Name = "Sofia", CountryId = 1},
                new Town{Name = "New York", CountryId = 2},
                new Town{Name = "Plovdiv", CountryId = 1},
                new Town{Name = "Valencia", CountryId = 3},
                new Town{Name = "Madrid", CountryId = 3},
            };

            this.context.Towns.AddRange(towns);
            this.context.SaveChanges();
        }

        private void SeedCountries()
        {
            var countries = new[]
            {
                new Country{Name = "Bulgaria"},
                new Country{Name = "USA"},
                new Country{Name = "Spain"},
            };

            this.context.Countries.AddRange(countries);
            this.context.SaveChanges();
        }

        private void SeedBankAcounts()
        {
            var bankAccounts = new []
            {
                new BankAccount{AccountNumber = "BGR33133235", Balance = 5000, CustomerId = 1}, 
                new BankAccount{AccountNumber = "BGR33323558", Balance = 3000, CustomerId = 5}, 
                new BankAccount{AccountNumber = "BGR25883235", Balance = 300, CustomerId = 2}, 
                new BankAccount{AccountNumber = "BGR33158235", Balance = 500, CustomerId = 4}, 
                new BankAccount{AccountNumber = "BGR47533235", Balance = 1000, CustomerId = 3}, 
            };

            this.context.BankAccounts.AddRange(bankAccounts);
            this.context.SaveChanges();
        }

        private void SeedCustomers()
        {
            var customers = new []
            {
                new Customer{FirstName = "Ben", LastName = "Stone", DateOfBirth = new DateTime(1956, 5, 3), Gender = Gender.Male},
                new Customer{FirstName = "Sam", LastName = "Fun", DateOfBirth = new DateTime(1976, 5, 30), Gender = Gender.Male},
                new Customer{FirstName = "Lora", LastName = "Stone", DateOfBirth = new DateTime(1985, 3, 3), Gender = Gender.Female},
                new Customer{FirstName = "Sara", LastName = "Bart", DateOfBirth = new DateTime(2000,12, 13), Gender = Gender.Female},
                new Customer{FirstName = "Rose", LastName = "Lee", DateOfBirth = new DateTime(2000, 5, 15), Gender = Gender.Female},
            };

            this.context.Customers.AddRange(customers);
            this.context.SaveChanges();
        }
    }
}
