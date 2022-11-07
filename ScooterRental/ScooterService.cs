using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooters;

        public ScooterService(List<Scooter> inventory)
        {
            _scooters = inventory;
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIDException(id);
            }

            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException(pricePerMinute);
            }
            
            if (_scooters.Any(Scooter => Scooter.Id ==id ))
            {
                throw new DuplicateScooterException(id);
            }
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIDException(id);
            }

            var Scooter = _scooters.FirstOrDefault(Scooter => Scooter.Id == id);

           if (Scooter == null)
           {
               throw new ScooterDoesNotExists(id);
           }
           _scooters.Remove(Scooter);
        }

        public IList<Scooter> GetScooters()
        {
            if (_scooters.Count ==0 )
            {
                throw new EmptyListException();
            }
            return _scooters.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            if (string.IsNullOrEmpty(scooterId))
            {
                throw new InvalidIDException(scooterId);
            }

            if (!_scooters.Any(Scooter => Scooter.Id == scooterId))
            {
                throw new ScooterDoesNotExists(scooterId);
            }
            return _scooters.FirstOrDefault(scooter => scooter.Id == scooterId);
        }
    }
}