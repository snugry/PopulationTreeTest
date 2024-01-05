﻿// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

/*for(int i = 0; i < 10; i++)
{
    Person p = new Person(nameGenerator, (Gender)(rand.Next(0, 1)));
    p.SetBirthDateRange(0, 30, rand);
    p.SetDeathDateRange(65, rand);

    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
}*/

Timeline timeline = new Timeline(12, 0);

timeline.CalculateTimeline(20, 2000);

var persons = timeline.GetPersonsFromYear(1500);

foreach(var p in persons)
{
    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
}
