using System;
using System.Linq;

namespace P08_PetClinics.Models
{
    public class Clinic : INameable
    {
        private readonly Pet[] _pets;
        private readonly int _midddleRoom;

        public Clinic(string name, int roomsCount)
        {
            ValiDateRoomsCount(roomsCount);
            Name = name;
            _pets = new Pet[roomsCount];
            _midddleRoom = roomsCount / 2;
        }

        public string Name { get; }

        public bool HasEmptyRooms => _pets.Any(r => r == null);

        private void ValiDateRoomsCount(int roomsCount)
        {
            if (roomsCount % 2 == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }
        }

        public bool Add(Pet pet)
        {
            var currentEmptyRoom = _midddleRoom;

            for (int index = 0; index < _pets.Length; index++)
            {
                if (index % 2 == 0)
                {
                    currentEmptyRoom += index;
                }
                else
                {
                    currentEmptyRoom -= index;
                }

                if (_pets[currentEmptyRoom] == null)
                {
                    _pets[currentEmptyRoom] = pet;
                    return true;
                }
            }

            return false;
        }

        public bool Release()
        {
            for (int index = _midddleRoom; index < _pets.Length; index++)
            {
                if (_pets[index] != null)
                {
                    _pets[index] = null;
                    return true;
                }
            }

            for (int index = 0; index < _midddleRoom; index++)
            {
                if (_pets[index] != null)
                {
                    _pets[index] = null;
                    return true;
                }
            }

            return false;
        }

        public void Print()
        {
            for (int index = 1; index <= _pets.Length; index++)
            {
                Print(index);
            }
        }

        public void Print(int roomNumber)
        {
            var result = _pets[roomNumber - 1]?.ToString() ?? "Room empty";
            Console.WriteLine(result);
        }
    }
}