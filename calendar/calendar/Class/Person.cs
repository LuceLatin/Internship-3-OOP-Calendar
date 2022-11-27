using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Osoba sadržava sljedeće podatke:
Ime
Prezime
    Postavlja se isključivo pri instanciranju objekta i nije moguće mijenjati kasnije na nikoji način
Email
    Postavlja se isključivo pri instanciranju objekta i nije moguće mijenjati kasnije na nikoji način
Prisutnost
Implementirati kao dictionary u kojem id eventa predstavlja key a value predstavlja boolean vrijednost
Moguće je direktno mijenjati samo unutar klase, a prema vani exposeat metode 
*/
namespace calendar.Class
{
    public class Person
    {
        public string FirstName { get; set; } 
        public string LastName { get; } 
        public string Email { get; }
        public Dictionary<Guid, bool> Attendance { get; set; } = new Dictionary<Guid, bool>(); //dictionary u kojem id eventa predstavlja key a value predstavlja boolean vrijednost
        

        public Person(string firstName, string lastName, string email) 
        {

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            
        }

        public void IsAttendance(Guid id, bool isAttending)
        {
            if(!Attendance.ContainsKey(id))
            {
                Attendance.Add(id, isAttending);
            }
            else
                Attendance[id]=isAttending;
        }

        //dodat key u dict

        public void AddKeyOfEvent(List<Event> events)
        {
            foreach (var e in events)
            {
                this.Attendance.Add(e.Id, false); //po defaultu kao
            }
        }


        public void printPerson()
        {
            Console.WriteLine("printPerson");
            Console.WriteLine(this.Attendance);
        }

        //za provjeru je li mi prominilo attendance u false
        public void cwPerson()
        {
            //Console.WriteLine($"First name : {this.FirstName}, Last name {this.LastName}.");
            foreach (var item in Attendance)
            {
                Console.WriteLine(item.Value);
            }
           
            
        }
    }
}
