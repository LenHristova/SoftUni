namespace P03_OldestFamilyMember
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Family
    {
        private List<Person> _members;

        public List<Person> Members
        {
            get { return _members; }
            set { _members = value; }
        }

        public Family()
        {
            _members = new List<Person>();
        }

        public void AddMember(Person member)
        {
            _members.Add(member);
        }

        public Person GetOldestMember()
        {
            if (Members.Count > 0)
            {
                var oldestAge = _members.Select(p => p.Age).Max();
                return _members.FirstOrDefault(p => p.Age == oldestAge);
            }
            else
            {
                throw new Exception("No family members!");
            }
        }
    }
}