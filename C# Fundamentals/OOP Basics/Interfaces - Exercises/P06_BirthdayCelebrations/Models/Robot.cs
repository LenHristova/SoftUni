 namespace P06_BirthdayCelebrations.Models
 {
     public class Robot : IIdentifiable
     {
         public string Id { get; set; }
         public string Model { get; set; }

         public Robot(string id, string model)
         {
             Id = id;
             Model = model;
         }
     }
 }