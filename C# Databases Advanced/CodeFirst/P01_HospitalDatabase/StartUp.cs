namespace P01_HospitalDatabase
{
    using Data;
    using Data.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new HospitalContext())
            {
                var d = new Doctor()
                {
                    Name = "UNKNOWN",
                    Specialty = "UNKNOWN"
                };

                foreach (var visitation in db.Visitations)
                {
                    visitation.Doctor = d;
                }

                db.SaveChanges();
            }
        }
    }
}
