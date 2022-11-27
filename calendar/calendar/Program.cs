using calendar.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;

namespace calendar
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var emails = new List<string>() { "luce.latin@gmail.com", "ivo.ivic@gmail.com", "ana.anic@gmail.com" };
            var events = new List<Event>();
            var persons = new List<Person>();


            var firstEvent = new Event()
            {
                Name = "Multimedia",
                Location = "B320",
                BeginningDate = new DateTime(2022, 1, 15),
                EndingDate = new DateTime(2022, 1, 16),
            };
            firstEvent.SetEmailList(new List<string> { "luce.latin@gmail.com", "zoe@gmail.com", "stipe@gmail.com" });
            events.Add(firstEvent);


            var secondEvent = new Event()

            {
                Name = "Design",
                Location = "B321",
                BeginningDate = new DateTime(2021, 3, 20),
                EndingDate = new DateTime(2021, 6, 12)
            };
            secondEvent.SetEmailList(new List<string> { "zoe@gmail.com", "marija@gmail.com", "luce.latin@gmail.com" });
            events.Add(secondEvent);



            var thirdEvent = new Event()
            {
                Name = "Programming",
                Location = "B322",
                BeginningDate = new DateTime(2022, 11, 20),
                EndingDate = new DateTime(2022, 12, 22)
            };


            thirdEvent.SetEmailList(new List<string> { "ivan@gmail.com", "luce.latin@gmail.com", "marija@gmail.com" });
            events.Add(thirdEvent);

            var fourthEvent = new Event()
            {
                Name = "Marketing",
                Location = "B323",
                BeginningDate = new DateTime(2022, 11, 28),
                EndingDate = new DateTime(2022, 12, 30)
            };
            fourthEvent.SetEmailList(new List<string> { "leona@gmail.com", "luce.latin@gmail.com", "marija@gmail.com" });

            var person1 = new Person("luce", "latin", "luce.latin@gmail.com");
            var person2 = new Person("marija", "maric", "marija@gmail.com");
            var person3 = new Person("ivan", "ivic", "ivan@gmail.com");
            var person4 = new Person("mate", "matic", "mate@gmail.com");
            var person5 = new Person("stipe", "stjepic", "stipe@gmail.com");
            var person6 = new Person("luka", "damjanic", "luka@gmail.com");
            var person7 = new Person("mia", "mijic", "mia@gmail.com");
            var person8 = new Person("leona", "modric", "leona@gmail.com");
            var person9 = new Person("zoe", "zoic", "zoe@gmail.com");
            var person10 = new Person("ivana", "ivanic", "ivana@gmail.com");
            
            persons.Add(person1);
            persons.Add(person2);
            persons.Add(person3);
            persons.Add(person4);
            persons.Add(person5);
            persons.Add(person6);
            persons.Add(person7);
            persons.Add(person8);
            persons.Add(person9);
            persons.Add(person10);



            events.Add(fourthEvent);
            foreach(var e in events)
            {
                foreach (var p in persons)
                {
                    if (e.EmailList.Contains(p.Email))
                    {
                        p.Attendance.Add(e.Id, true);

                    }
                }
                
            }
            MainMenu(events, persons);

        }




        public static void MainMenu(List<Event> events, List<Person> person)
        {
            Console.Clear();
            Console.WriteLine("CALENDAR");
            Console.WriteLine("Choose one of the options by entering one of the offered numbers:");
            Console.WriteLine("1 - Active events");
            Console.WriteLine("2 - Upcoming events");
            Console.WriteLine("3 - Events that have ended");
            Console.WriteLine("4 - Create an event");
            Console.WriteLine("0 - Exit from the program");
            var choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("Active events");
                    ActiveEvents(events, person);
                    Console.WriteLine("press enter");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Upcoming events");
                    UpcomingEvents(events, person);
                    Console.WriteLine("press enter");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Events that have ended");
                    EndedEvents(events, person);
                    Console.WriteLine("press enter");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Create an event");
                    CreateEvent(events, person);
                    foreach (var item in events)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.WriteLine("press enter");
                    Console.ReadLine();
                    break;
                default:
                    break;

            }
            MainMenu(events, person);


        }

        static void EndedEvents(List<Event> events, List<Person> person)
        {
            //var listOfParticipants= new List<Person>();
            //var didNotCome = new List<Person>();

            foreach (var item in events)
            {
                var newListOfPeople = new List<Person>(); //spremam u listu sve koji su bili na eventu
                foreach (var p in person)
                {
                    if(p.Attendance!=null)
                    {
                        if (p.Attendance.ContainsKey(item.Id) && p.Attendance[item.Id] == true) // PROVjERI jel triba ono da pise true
                        {
                            newListOfPeople.Add(p);
                        }
                    }
                    
                }

                var didNotCome = new List<Person>(); //spremam u listu sve koji nisu bili na eventu
                foreach (var p in person)
                {
                    if (p.Attendance.ContainsKey(item.Id) && p.Attendance[item.Id] == false)
                    {
                        didNotCome.Add(p);
                    }
                }

                if (item.EndingDate < DateTime.Now)
                {
                    var listOfParticipants = newListOfPeople;
                    //var didNotCome = DidNotAttendedTheEvent(person, item);
                    Console.WriteLine($"name {item.Name}, location {item.Location}, ended before {(DateTime.Now - item.EndingDate).Days} days, duration {(item.EndingDate - item.BeginningDate).TotalHours} hours.");
                    Console.WriteLine("List of participants:");
                    
                    var strBuilder1 = new StringBuilder(); 
                    for (var i = 0; i < listOfParticipants.Count; i++)
                    {
                        if (listOfParticipants.Count == (i + 1))
                        {
                            strBuilder1.AppendLine(listOfParticipants[i].Email);
                        }
                        else
                        {
                            strBuilder1.AppendLine(listOfParticipants[i].Email + ", ");
                        }
                    }
                    Console.WriteLine(strBuilder1);
                    Console.WriteLine("People who did not come:");
                    //spremljeno je u listu 
                    // Svi ispisi popisa osoba se rade kroz jedan string na način da se svaki mail odvoji
                    //zarezom i razmakom od drugog.
                    var strBuilder2 = new StringBuilder();
                    for (var i = 0; i < didNotCome.Count; i++)
                    {
                        if (didNotCome.Count == (i + 1))
                        {
                            strBuilder2.AppendLine(didNotCome[i].Email);
                        }
                        else
                        {
                            strBuilder2.AppendLine(didNotCome[i].Email + ", ");
                        }
                    }
                    Console.WriteLine(strBuilder2);
                    Console.WriteLine();
                    

                }
            }
            
        }


        public List<Person> AttendedTheEvent(List<Person> person, Event oneEvent)
        {
            Console.WriteLine("Attended");
            var newListOfPeople = new List<Person>(); //spremam u listu sve koji su bili na eventu
            foreach (var item in person)
            {
                if (item.Attendance.ContainsKey(oneEvent.Id) && item.Attendance[oneEvent.Id] == true) // PROVjERI jel triba ono da pise true
                {
                    newListOfPeople.Add(item);
                }
            }
            return newListOfPeople;
        }

        public List<Person> DidNotAttendedTheEvent(List<Person> person, Event oneEvent)
        {
            Console.WriteLine("Did not attended");
            var didNotCome = new List<Person>(); //spremam u listu sve koji su bili na eventu
            foreach (var item in person)
            {
                if (item.Attendance.ContainsKey(oneEvent.Id) && item.Attendance[oneEvent.Id] == false)
                {
                    didNotCome.Add(item);
                }
            }
            return didNotCome;
        }


        public static void CreateEvent(List<Event> events, List<Person> person) //q
        {

            Console.WriteLine("Enter the name of the event: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the location of the event: ");
            var location = Console.ReadLine();
            Console.WriteLine("Enter the start date of the event YYYY-MM-DD: ");
            var startDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter the end date of the event YYYY-MM-DD: ");
            var endDate = Convert.ToDateTime(Console.ReadLine());
            if (startDate > endDate)
            {
                Console.WriteLine("The start date of the event must be before the end date of the event.");
            }
            else if (startDate < DateTime.Now)
            {
                Console.WriteLine("The event must start in the future.");
            }
            else
            {
                var email = "";
                var newEmails = new List<string>();
                while (true)
                {
                    Console.WriteLine("Enter the email of the person you want to add to the previously entered event. To break the program, type q.");
                    email = Console.ReadLine();
                    if (email == "q")
                    {
                        break;
                    }
                    var temp = ReturnName(email, events, person);
                    if (temp != "empty")
                    {
                        if (!IsPersonAvailable(email, temp, events, startDate, endDate))
                        {
                            Console.WriteLine("Person is not availbale.");
                            continue;
                        }
                    }

                    else
                    {
                        newEmails.Add(email);

                    }
                }
                //newEvent.SetEmailList(newEmails);
                var newEvent = new Event()
                {
                    Name = name,
                    Location = location,
                    BeginningDate = startDate,
                    EndingDate = endDate,
                };
                foreach (var item in person)
                {
                    if (item.Email == email)
                        item.Attendance.Add(newEvent.Id, true);
                }
                events.Add(newEvent);
                Console.WriteLine("Event successfully created!");

            }

        }

        static string ReturnName(string email, List<Event> events, List<Person> persons)
        {
            foreach (var person in persons)
            {
                if (person.Email == email)
                    return person.FirstName;

            }
            return "empty";
        }
        public static bool IsPersonAvailable(string email, string person, List<Event> events, DateTime start, DateTime end)
        {
            foreach (var item in events)
            {
                if (item.EmailList.Contains(person))
                {
                    if (item.BeginningDate > start && item.EndingDate < end)
                        return false;

                }
            }
            return true;
        }





        public static void ActiveEvents(List<Event> events, List<Person> person)
        {
            Console.WriteLine("Currently active events: ");
            foreach (var item in events)
            {
                var newListOfPeople = new List<Person>(); //spremam u listu sve koji su bili na eventu
                foreach (var p in person)
                {
                    if (p.Attendance != null)
                    {
                        if (p.Attendance.ContainsKey(item.Id) && p.Attendance[item.Id] == true) 
                        {
                            newListOfPeople.Add(p);
                        }
                    }
                }


                    if (item.BeginningDate <= DateTime.Now && item.EndingDate >= DateTime.Now)
                    {
                        var listOfParticipants = newListOfPeople;
                        Console.WriteLine($"Id: {item.Id}, name {item.Name}, location {item.Location}, Ends in {(item.EndingDate - DateTime.Now).Days} days");
                        Console.WriteLine("List of participants:");



                        var strBuilder1 = new StringBuilder();
                        for (var i = 0; i < listOfParticipants.Count; i++)
                        {
                            if (listOfParticipants.Count == (i+1))
                            {
                                strBuilder1.AppendLine(listOfParticipants[i].Email);
                            }
                            else
                            {
                                strBuilder1.AppendLine(listOfParticipants[i].Email + ", ");
                            }
                        }
                    Console.WriteLine(strBuilder1);
                }
                
            }
            ActiveEventsSubMenu(events, person);

        }

        public static void ActiveEventsSubMenu(List<Event> events, List<Person> person)
        {
            Console.WriteLine("1 - Record the absence"); 
            Console.WriteLine("0 - Return to the main menu");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    MainMenu(events, person);
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("Record the absence");
                    RecordAbsence(events, person);
                    break;
                default:
                    MainMenu(events, person);
                    break;
            }


        }

        //Korisnik unosi id eventa i emailove osoba kojima želi zabilježiti da nisu bili prisutni

        public static void RecordAbsence(List<Event> events, List<Person> person)
        {
            foreach (var item in events)
            {
                Console.WriteLine($"Id: {item.Id} for {item.Name} emailovi: ");
                foreach (var e in item.EmailList)
                {
                    Console.WriteLine($" {e} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Enter Id of Event");
            var idOfEvent = Console.ReadLine();
            Console.WriteLine("Enter email which you want to delete: format aaaa.bbb@gmail.com  ccc.ddd@gmail.com");
            var emails = Console.ReadLine();
            //splitanje po razmaku
            var splitEmails = emails.Split(" ");
            if(splitEmails.Length == 0)
            {
                Console.WriteLine("There are no participants");
            }
            if(Confirmation())
            {
                foreach (var e in splitEmails)
                {
                    events.Find(a => a.Id.ToString() == idOfEvent).EmailList.Remove(e);
                    person.Find(b => b.Email == e).IsAttendance(new Guid(idOfEvent), false);
                    //person.Find(b => b.Email == e).Attendance[new Guid(idOfEvent)] = false; //update attendance
                }

             }
                
            
            
        }

        public static void UpcomingEvents(List<Event> events, List<Person> person)

        {
            foreach (var item in events)
            {
                if (item.BeginningDate > DateTime.Now)
                {
                    var newListOfPeople = new List<Person>(); //spremam u listu sve koji su bili na eventu
                    foreach (var p in person)
                    {
                        if (p.Attendance != null)
                        {
                            if (p.Attendance.ContainsKey(item.Id) && p.Attendance[item.Id] == true) // PROVjERI jel triba ono da pise true
                            {
                                newListOfPeople.Add(p);
                            }
                        }

                    }
                    Console.WriteLine($"Id: {item.Id}, name {item.Name}, location {item.Location}, starts in {(item.BeginningDate - DateTime.Now).Days} days, duration {(item.EndingDate - item.BeginningDate).TotalHours} hours");
                    Console.WriteLine("List of participants:");
                    var listOfParticipants = newListOfPeople;
                    var strBuilder1 = new StringBuilder();
                    for (var i = 0; i < listOfParticipants.Count; i++)
                    {
                        if (listOfParticipants.Count == (i + 1))
                        {
                            strBuilder1.AppendLine(listOfParticipants[i].Email);
                        }
                        else
                        {
                            strBuilder1.AppendLine(listOfParticipants[i].Email + ", ");
                        }
                    }
                    Console.WriteLine(strBuilder1);
                   
                }
            }
            UpcomingEventSubMenu(events, person);

        }

        public static void UpcomingEventSubMenu(List<Event> events, List<Person> person)
        {
            Console.WriteLine("1 - Delete the event");
            Console.WriteLine("2 - Remove people from the event");
            Console.WriteLine("0 - Return to the main menu"); 
            var choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 0:
                    MainMenu(events, person);
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("Delete the event");
                    DeleteEvent(events, person);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Remove people from the event");
                    RemovePeopleFromEvent(events, person);
                    break;
                default:
                    MainMenu(events, person);
                    break;


            }
        }

        //Korisnik unosi id eventa kojeg želi izbrisati te uz to briše korisnicima podatke o prisutnosti na tom eventu
        //(moguće je izbrisati samo nadolazeći event)
        public static void DeleteEvent(List<Event> events, List<Person> person)
        {
            foreach(var item in events)
            {
                Console.WriteLine($"Id: {item.Id} for {item.Name} emailovi: ");
                foreach (var e in item.EmailList)
                {
                    Console.WriteLine($" {e} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Enter the id of the event you want to delete:");
            var id = Console.ReadLine();
            var checkId = events.Find(a => a.Id == new Guid(id)); //vraca null ako ne nadje tj to znaci da nema eventa

                if (checkId == null)
                    Console.WriteLine("Event does not exist.");
                //moguće je izbrisati samo nadolazeći even
                //nadji datum eventa preko ida
                if ((events.Find(a => a.Id.ToString() == id).BeginningDate) > DateTime.Now)
                {
                    if (Confirmation())
                    {
                        foreach (var item in person)
                        {
                            item.Attendance.Remove(new Guid(id));
                        }
                        events.Remove(checkId); //ako ga je nasa izbrisi ga
                    }

            }
      
        }

        public static void RemovePeopleFromEvent(List<Event> events, List<Person> person)
        {
            foreach (var item in events)
            {
                Console.WriteLine($"Id: {item.Id} for {item.Name} emailovi: ");
                foreach (var e in item.EmailList)
                {
                    Console.WriteLine($" {e} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Enter the id of the event:");
            var idOfEvent = Console.ReadLine();
            var checkId = events.Find(a => a.Id == new Guid(idOfEvent));

            Console.WriteLine("Enter email which you want to delete: format aaaa.bbb@gmail.com  ccc.ddd@gmail.com");
            var emails = Console.ReadLine();
            //splitanje po razmaku
            var splitEmails = emails.Split(" ");
            if (splitEmails.Length == 0)
            {
                Console.WriteLine("There are no participants");
            }
            if (Confirmation())
            {
                foreach (var e in splitEmails) //email osobe
                {
                    if ((events.Find(a => a.Id == (new Guid(idOfEvent))).EmailList.Contains(e))==false)
                    {
                        Console.WriteLine("That person is not in this event");
                    }
                    else
                    {
                        events.Find(a => a.Id == (new Guid(idOfEvent))).EmailList.Remove(e);
                        person.Find(b => b.Email == e).Attendance.Remove((new Guid(idOfEvent))); //update attendance

                    }
                }

            }

        }


        public static bool Confirmation()
        {
            Console.WriteLine("Are you sure you want to do this? Enter y/n");
            char choice = Console.ReadLine().ToLower()[0];
            if (choice == 'y')
            {
                Console.WriteLine("The command will be executed.");
                return true;
            }
            else
            {
                Console.WriteLine("The command will not be executed.");
                return false;
            }
        }



    }


}



