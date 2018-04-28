using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using P02_PeopleDatabase;

namespace P02_PeopleDatabaseTests
{
    public class PeopleDatabaseTests
    {
        private PeopleDatabase _db;
        private FieldInfo _storingArrayFieldInfo;
        private FieldInfo _currentIndexFieldInfo;
        private const int MAX_CAPACITY = 16;

        private static PeopleDatabase CreatePeopleDatabase(params Person[] persons) => new PeopleDatabase(persons);

        private void SetUpDb(Person[] persons = null)
        {
            var currentIndex = persons?.Length ?? 0;
            persons = persons?.Concat(new Person[MAX_CAPACITY - currentIndex]).ToArray() ?? new Person[MAX_CAPACITY];

            _db = CreatePeopleDatabase();
            _storingArrayFieldInfo = GetFieldInfo(typeof(Person[]));
            _storingArrayFieldInfo.SetValue(_db, persons);

            _currentIndexFieldInfo = GetFieldInfo(typeof(int));
            _currentIndexFieldInfo.SetValue(_db, currentIndex);
        }

        private static readonly object[] ValidPeople =
        {
            new Person[]
            {
                new Person(1, "Pesho"),
                new Person(2, "Gosho"),
                new Person(3, "Shosho"),
            },
            new Person[]
            {
                new Person(1, "Pesho"),
                new Person(2, "pesho"),
                new Person(3, "peSho"),
            },
            new Person[]
            {
                new Person(long.MaxValue, "Pesho"),
            },
            new Person[]
            {
                new Person(0, "Pesho"),
            },
        };

        private static readonly Person[] InvalidPeople =
        {
            new Person(1, null),
            new Person(-2, "Gosho"),
            new Person(long.MinValue, "Pesho"),
            null
        };

        private FieldInfo GetFieldInfo(Type fieldType)
        {
            var fieldInfo = _db.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == fieldType);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field of type {fieldType} not found.");

            return fieldInfo;
        }

        [Test]
        [TestCase(new object[] { })]
        [TestCaseSource(nameof(ValidPeople))]
        public void CtorWithValidPersonsShouldWorkCorrectly(params Person[] persons)
        {
            _db = CreatePeopleDatabase(persons);

            var dataArrayFieldInfo = GetFieldInfo(typeof(Person[]));
            var dataArrayValue = dataArrayFieldInfo.GetValue(_db);

            var personsCount = persons?.Length ?? 0;
            var buffer = MAX_CAPACITY - personsCount;
            var expectedDataArrayValue = persons?.Concat(new Person[buffer]) ?? new Person[MAX_CAPACITY];
            Assert.That(dataArrayValue, Is.EquivalentTo(expectedDataArrayValue), "Stored data is not equivalent to expected data.");

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            var currentIndexValue = currentIndexFieldInfo.GetValue(_db);
            var expectedCurrentIndexValue = personsCount;
            Assert.That(currentIndexValue, Is.EqualTo(expectedCurrentIndexValue), "Current index value is wrong.");
        }

        [Test]
        [TestCaseSource(nameof(InvalidPeople))]
        public void CtorWithInvalidPersonsShoulThrowException(Person person)
        {
            void TestDelegate() => CreatePeopleDatabase(person);
            CheckForNullPerson(TestDelegate, person);
            CheckForNullUsername(TestDelegate, person?.Username);
            CheckForNegativeId(TestDelegate, person?.Id);
        }

        private static void CheckForNegativeId(TestDelegate testDelegat, long? id)
        {
            if (id < 0)
            {
                Assert.That(testDelegat, Throws.TypeOf(typeof(ArgumentOutOfRangeException)), "Must throw \"ArgumentOutOfRangeException\"");
            }
        }

        private static void CheckForNullUsername(TestDelegate testDelegat, string username)
        {
            if (username == null)
            {
                Assert.That(testDelegat, Throws.ArgumentNullException, "Must throw \"ArgumentNullException\"");
            }
        }

        private static void CheckForNullPerson(TestDelegate testDelegat, Person person)
        {
            if (person == null)
            {
                Assert.That(testDelegat, Throws.ArgumentNullException, "Must throw \"ArgumentNullException\"");
            }
        }
[Test]
        public void CtorWithMoreThen16PeopleShouldThrowException()
        {
            var overflowed = new Person[MAX_CAPACITY + 1];

            Assert.That(() => CreatePeopleDatabase(overflowed), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
        }

        [Test]
        [TestCaseSource(nameof(ValidPeople))]
        public void AddShouldWorkCorrectly(params Person[] persons)
        {
            SetUpDb();

            foreach (var value in persons)
            {
                _db.Add(value);
            }

            var storingArrayValue = _storingArrayFieldInfo.GetValue(_db);

            int buffer = MAX_CAPACITY - persons.Length;
            var expectedStoringArrayValue = persons.Concat(new Person[buffer]);

            Assert.That(storingArrayValue, Is.EquivalentTo(expectedStoringArrayValue), "Stored data is not equivalent to expected data.");

            var currentIndexValue = _currentIndexFieldInfo.GetValue(_db);
            var expectedCurrentIndexValue = persons.Length;

            Assert.That(currentIndexValue, Is.EqualTo(expectedCurrentIndexValue), "Current index value is wrong.");
        }

        [Test]
        [TestCaseSource(nameof(InvalidPeople))]
        public void AddShouldThrowExceptionWithInvalidPerson(Person person)
        {
            SetUpDb();

            void TestDelegate() => _db.Add(person);
            CheckForNullPerson(TestDelegate, person);
            CheckForNullUsername(TestDelegate, person?.Username);
            CheckForNegativeId(TestDelegate, person?.Id);
        }

        [Test]
        public void AddToFullDbShouldThrowException()
        {
            var fullDb = new Person[16].Select(p => new Person(1, "Pesho")).ToArray();
            SetUpDb(fullDb);

            Assert.That(() => _db.Add(new Person(1, "Pesho")), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
        }

        [Test]
        public void AddExistedPersonShouldThrowException()
        {
            SetUpDb(new Person[] { new Person(1, "Pesho") });
            Assert.That(() => _db.Add(new Person(1, "Gosho")), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
            Assert.That(() => _db.Add(new Person(2, "Pesho")), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
        }

        [Test]
        [TestCaseSource(nameof(ValidPeople))]
        public void RemoveShouldWorkCorrectly(params Person[] persons)
        {
            SetUpDb(persons);

            _db.Remove();

            var storingArrayValue = _storingArrayFieldInfo.GetValue(_db);

            var personsCountAfterRemoving = persons.Length - 1;
            var buffer = MAX_CAPACITY - personsCountAfterRemoving;
            var expectedStoringArrayValue = persons.Take(personsCountAfterRemoving).Concat(new Person[buffer]);

            Assert.That(storingArrayValue, Is.EquivalentTo(expectedStoringArrayValue), "Stored data is not equivalent to expected data.");

            var currentIndexValue = _currentIndexFieldInfo.GetValue(_db);
            var expectedCurrentIndexValue = personsCountAfterRemoving;

            Assert.That(currentIndexValue, Is.EqualTo(expectedCurrentIndexValue), "Current index value is wrong.");
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrowException()
        {
            SetUpDb();

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            currentIndexFieldInfo.SetValue(_db, 0);

            Assert.That(() => _db.Remove(), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
        }

        [Test]
        [TestCaseSource(nameof(ValidPeople))]
        public void FetchShouldReturnArray(Person[] persons)
        {
            SetUpDb();

            int buffer = MAX_CAPACITY - persons.Length;
            var dbValues = persons.Concat(new Person[buffer]).ToArray();

            _storingArrayFieldInfo.SetValue(_db, dbValues);
            _currentIndexFieldInfo.SetValue(_db, persons.Length);

            var expectedArray = persons;
            Assert.That(() => _db.Fetch(), Is.EquivalentTo(expectedArray), "Fetched data is not equivalent to expected data.");

            var actualReturnType = _db.Fetch().GetType();
            var expectedReturnType = typeof(Person[]);
            Assert.That(actualReturnType, Is.EqualTo(expectedReturnType), "Return type does not match the expected type.");
        }

        [Test]
        [TestCaseSource(nameof(ValidPeople))]
        public void FindByUsernameShoudReturnPersonWithThatName(params Person[] persons)
        {
            SetUpDb(persons);

            var person = persons.First();

            Assert.That(() => _db.FindByUsername(person.Username), Is.EqualTo(person), "Returned person not equal to expected person.");
        }

        [Test]
        public void FindByUsernameShoudThrowExseption()
        {
            var persons = new Person[]
            {
                new Person(1, "Pesho")
            };

            SetUpDb(persons);

            Assert.That(() => _db.FindByUsername(null), Throws.ArgumentNullException, "Must throw \"ArgumentNullException\"");
            Assert.That(() => _db.FindByUsername("Gosho"), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
        }

        [Test]
        [TestCaseSource(nameof(ValidPeople))]
        public void FindByIdShoudReturnPersonWithThatId(params Person[] persons)
        {
            SetUpDb(persons);

            var person = persons.First();

            Assert.That(() => _db.FindById(person.Id), Is.EqualTo(person), "Returned person not equal to expected person.");
        }

        [Test]
        public void FindByIdShoudThrowExseption()
        {
            var persons = new Person[]
            {
                new Person(1, "Pesho")
            };

            SetUpDb(persons);

            Assert.That(() => _db.FindById(-1), Throws.TypeOf(typeof(ArgumentOutOfRangeException)), "Must throw \"ArgumentOutOfRangeException\"");
            Assert.That(() => _db.FindById(2), Throws.InvalidOperationException, "Must throw \"InvalidOperationException\"");
        }
    }
}
