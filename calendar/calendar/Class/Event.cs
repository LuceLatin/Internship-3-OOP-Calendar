using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Event sadržava sljedeće podatke:
Id
Generira se automatski isključivo pri instanciranju objekta i nije ga moguće mijenjati kasnije na nikoji način
Naziv
Lokacija
Datum početka
Datum kraja
Emailovi osoba koje sudjeluju
Moguće je direktno mijenjati samo unutar klase, a prema se exposeaju metode koje 
*/
namespace calendar.Class
{
    public class Event
    {
        public Guid Id { get;  } //nije Guid jer ne mogu deletat onda po indeksu :(
        public string Name { get; set; } //private sve osin id
        public string Location { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public List<string> EmailList { get; private set; }


        public Event()
        {
            Id = Guid.NewGuid();
            
        }
        
        
        public void SetEmailList(List<string> emailList)
        {
            EmailList = emailList;
        }
        
        /*
        public void SetEmail(List<Person> people)  // set email (private set)
        {
            this.EmailList = people;
            var emails = this.EmailList;
            for (var i = 0; i < emails.Count; i++)
            {
                this.EmailList[i].Attendance = new Dictionary<Guid, bool>();
                this.EmailList[i].Attendance.Add(this.Id, true);
            }
        }
        */

        public void PrintEvent()
        {
            Console.WriteLine(Name);
            foreach (var item in EmailList)
            {
                Console.WriteLine(item);
            }

        }

    }

    
}
