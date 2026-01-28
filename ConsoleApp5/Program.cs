using System;
using System.Drawing;
using System.Xml.Linq;

var fire = new Battle();
fire.Report();



static int DiceThrow(int x, int dice)
{
    int sum = 0;
    Random rand = new Random();

    for (int i = 0; i < x; i++)
    {
        sum += rand.Next(1, dice+1);
    } 
    return sum;
}

int d6 = DiceThrow(1, 6);
Console.WriteLine(d6);


public class Fighter
{
    private static Random rand = new Random();
    public Fighter()
    {
    }
    public Fighter(string name )
    {
        Name = name;
        Health = 50 + rand.Next(1, 30);
        Attack = 6 + rand.Next(1, 6);
        Strength = 1 + rand.Next(0, 4);
    }
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Attack { get; set; }

}

public class Squad
{
    public Squad(string label)
    {
        Label = label;
        Fighters = new List<Fighter>();
    }
    public string Label { get; set; }
    public List<Fighter> Fighters { get; set; }
}

public class Fraction 
{
    public Fraction(string name)
    {
        Name = name;
        Squads = new List<Squad>();
    }
    public string Name { get; set; }
    public List<Squad> Squads { get; set; }
}


public class Battle
{
    private static Random rand = new Random();
    public Fraction Drunks { get; }
    public Fraction Herbalists { get; }

    public Battle()
    {
        Drunks = new Fraction("Drunks");
        Herbalists = new Fraction("Herbalists");
        Drunks.Squads.Add(new Squad("Whiskey lovers"));
        Drunks.Squads.Add(new Squad("Rom lovers"));
        Herbalists.Squads.Add(new Squad("Mak lovers"));
        Herbalists.Squads.Add(new Squad("Hemp lovers"));
        List<string> drunk_names = new List<string>();
        List<string> herbalist_names = new List<string>();
        string pathD = Path.Combine(AppContext.BaseDirectory, "DrunkNam.txt");
        string pathH = Path.Combine(AppContext.BaseDirectory, "HerbalistNames.txt");

        drunk_names.AddRange(File.ReadAllLines(pathD));
        herbalist_names.AddRange(File.ReadAllLines(pathH));

        foreach (var squad in Drunks.Squads)
        {
            for (int i = 0; i < 10; i++)
            {
                var name = drunk_names[rand.Next(0, drunk_names.Count)];
                squad.Fighters.Add(new Fighter(name));
            }
        }
        foreach (var squad in Herbalists.Squads)
        {
            for (int i = 0; i < 10; i++)
            {
                var name = herbalist_names[rand.Next(0, herbalist_names.Count)];
                squad.Fighters.Add(new Fighter(name));
            }
        }


    }

    public void Report()
    {
        Console.WriteLine($"Here are two hateful enemies {Drunks.Name} and {Herbalists.Name}");
        Console.WriteLine($"{Drunks.Name} has:");
        foreach (var drunk in Drunks.Squads)
        {
            Console.WriteLine($" {drunk.Label} with {drunk.Fighters.Count} Fighters, their names are:");
            foreach (var fighter in drunk.Fighters)
            {
                Console.WriteLine($" {fighter.Name} with Health: {fighter.Health}, Attack: {fighter.Attack}, Strength: {fighter.Strength}");
            }
        }
        Console.WriteLine($"{Herbalists.Name} has:");
        foreach (var drunk in Herbalists.Squads)
        {
            Console.WriteLine($" {drunk.Label} with {drunk.Fighters.Count} Fighters, their names are:");
            foreach (var fighter in drunk.Fighters)
            {
                Console.WriteLine($" {fighter.Name} with Health: {fighter.Health}, Attack: {fighter.Attack}, Strength: {fighter.Strength}");
            }
        }
    }

}



