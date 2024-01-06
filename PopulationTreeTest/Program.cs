// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

/*for(int i = 0; i < 10; i++)
{
    Person p = new Person(nameGenerator, (Gender)(rand.Next(0, 1)));
    p.SetBirthDateRange(0, 30, rand);
    p.SetDeathDateRange(65, rand);

    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
}*/

Timeline timeline = new Timeline(20, 0);

timeline.CalculateTimeline(-10000, 3000);

var persons = timeline.GetPersonsFromYear(-9000);

Console.WriteLine($"-9000: {persons.Count} persons");

persons = timeline.GetPersonsFromYear(-5000);

Console.WriteLine($"-5000: {persons.Count} persons");

persons = timeline.GetPersonsFromYear(-1000);

Console.WriteLine($"-1000: {persons.Count} persons");

persons = timeline.GetPersonsFromYear(0);

Console.WriteLine($"0: {persons.Count} persons");

persons = timeline.GetPersonsFromYear(1000);

Console.WriteLine($"1000: {persons.Count} persons");

persons = timeline.GetPersonsFromYear(1990);

Console.WriteLine($"1990: {persons.Count} persons");

persons = timeline.GetPersonsFromYear(2748);

Console.WriteLine($"2748: {persons.Count} persons");

/*foreach (var p in persons)
{
    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
}*/
