using System;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Xml.Linq;

var fire = new Battle();
//fire.Report();

//fire.fight1vs1();

fire.fightSvsS();

//static string DiceThrow(int x, int dice)
//{
//    int sum = 0;
//    Random rand = new Random();

//    for (int i = 0; i < x; i++)
//    {
//        sum += rand.Next(1, dice + 1);
//    }

//    return sum.ToString();
//}

//string d2d6 = DiceThrow(2, 6);
//Console.WriteLine(d2d6);


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

    public int hit()
    {
        return rand.Next(1, Attack+1) + Strength;
    }

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
            for (int i = 0; i < 2; i++)
            {
                var name = drunk_names[rand.Next(0, drunk_names.Count)];
                squad.Fighters.Add(new Fighter(name));
            }
        }
        foreach (var squad in Herbalists.Squads)
        {
            for (int i = 0; i < 2; i++)
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

    public Fighter random_drunk_fighter()
    {
        var rand_squad = Drunks.Squads[rand.Next(0, Drunks.Squads.Count)];
        var rand_drunk_fighter = rand_squad.Fighters[rand.Next(0, rand_squad.Fighters.Count)];
        return rand_drunk_fighter;
    }
    public Fighter random_herbalist_fighter()
    {
        var rand_squad = Herbalists.Squads[rand.Next(0, Herbalists.Squads.Count)];
        var rand_herbalist_fighter = rand_squad.Fighters[rand.Next(0, rand_squad.Fighters.Count)];
        return rand_herbalist_fighter;
    }

    public Squad random_drunk_squad()
    {
        var rand_drunk_squad = Drunks.Squads[rand.Next(0, Drunks.Squads.Count)];
        return rand_drunk_squad;
    }
    public Squad random_herbalist_squad()
    {
        var rand_herbalist_squad = Herbalists.Squads[rand.Next(0, Herbalists.Squads.Count)];
        return rand_herbalist_squad;
    }

    public void fight1vs1()
    {
        
        var herbalist = random_herbalist_fighter();
        var drunk = random_drunk_fighter();
        var drunkdamage = 0;
        var herbalistdamage = 0;
        Console.WriteLine($"{drunk.Health}");
        Console.WriteLine($"{herbalist.Health}");
        while (drunk.Health > 0 && herbalist.Health > 0)
        {
            drunkdamage = drunk.hit();
            herbalist.Health -= drunkdamage;
            herbalistdamage = herbalist.hit();
            drunk.Health -= herbalistdamage;
            Console.WriteLine($" {drunk.Name}, attacks the opponen, {herbalist.Name} gets {drunkdamage} damage and his health is {herbalist.Health}");
            Console.WriteLine($" {herbalist.Name} attacks the opponen, {drunk.Name} gets {herbalistdamage} damage and his health is {drunk.Health}");
            if (drunk.Health <= 0 && herbalist.Health <= 0)
            {
                Console.WriteLine($"Both died, no winner");
            }
            else if (drunk.Health <= 0)
            {
                Console.WriteLine($"{herbalist.Name} is the winner!");
            }
            else if (herbalist.Health <= 0)
            {
                Console.WriteLine($"{drunk.Name} is the winner!");
            }

        };
    }

    public void fightSvsS()
    {

        var herbalists = random_herbalist_squad();
        var drunks = random_drunk_squad();
        var drunkdamage = 0;
        var herbalistdamage = 0;

        do
        {
            bool drunksAlive = false;
            bool herbalistsAlive = false;

            foreach (var x in drunks.Fighters)
                if (x.Health > 0)
                {
                    
                    drunksAlive = true;
                    //Console.WriteLine($"{drunksAlive} drunk");
                    //Console.WriteLine($"{x.Name}");
                    
                }
                else
                {
                    //Console.WriteLine($"drunk died!");
                }


            foreach (var x in herbalists.Fighters)
                if (x.Health > 0)
                {
                    herbalistsAlive = true;
                    //Console.WriteLine($"{herbalistsAlive} herb");
                }
                else
                {
                    //Console.WriteLine($"herbalist died!");
                }

            //Console.WriteLine($"{drunksAlive} drunk");
            //Console.WriteLine($"{herbalistsAlive} herb");

            if (!drunksAlive && !herbalistsAlive)
            {
                Console.WriteLine($"All died!");
            }
            else if (!herbalistsAlive)
            {
                Console.WriteLine($"{drunks.Label} wins!");
            }
            else if (!drunksAlive)
            {
                Console.WriteLine($"{herbalists.Label} wins!");
            }

            if (!drunksAlive || !herbalistsAlive)
            {
                break;
            }

            for (int i = 0; i < drunks.Fighters.Count; i++)
            {
                var drunk_fighter = drunks.Fighters[i];
                int x = 0;
                int y = 0;
                while (true)
                {
                    if (drunk_fighter.Health <= 0 && x <= i)
                    {
                        
                        drunk_fighter = drunks.Fighters[x];
                        x++;
                    }
                    else
                    {
                        break;
                    }
                }
                

                var herbalist_fighter = herbalists.Fighters[i];
                while (true)
                {
                    if (herbalist_fighter.Health <= 0 && y <= i)
                    {
                        herbalist_fighter = herbalists.Fighters[y];
                        y++;

                    }
                    else
                    {
                        break;
                    }
                }

                drunkdamage = drunk_fighter.hit();
                herbalist_fighter.Health -= drunkdamage;
                herbalistdamage = herbalist_fighter.hit();
                drunk_fighter.Health -= herbalistdamage;
                Console.WriteLine($" {drunk_fighter.Name}, attacks the opponen, {herbalist_fighter.Name} gets {drunkdamage} damage and his health is {herbalist_fighter.Health}");
                Console.WriteLine($" {herbalist_fighter.Name} attacks the opponen, {drunk_fighter.Name} gets {herbalistdamage} damage and his health is {drunk_fighter.Health}");
                
            }

        } while (true);
    }

}




