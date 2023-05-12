using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading;
using System.Xml.Linq;
using TextAdventureGameInputParser;
using TextAdventureGameInputParser.WordClass;
using static System.Net.Mime.MediaTypeNames;
using static TestConsole.Item;

namespace TestConsole
{
    internal class Program
    {
        const bool DEBUG = true;
        const int MAX_INVENTORY = 5;
        const bool CHANGE = false;
        public static Player player1 = new Player(0);
        private static void Main()
        {
            var parser = CreateParser();
            Environment scene = new Environment();
            Sentence results;
            Enemies enemies = new Enemies();
            //Enemy enemy = new Enemy();
            //engage(ref enemy);
            Console.WriteLine("What is your name? ");
            player1.Name = Console.ReadLine();
            Console.WriteLine("Type 'help' or '?' for a list of commands\n");
            scene.CurrentRoom().lookCmd();
            do
            {
                Console.Write("\nParse what?> ");
                var input = Console.ReadLine() ?? "";
                Console.WriteLine();
                if (string.IsNullOrWhiteSpace(input))
                    return;
                results = parser.Parse(input);
                executeCommand(results, parser, scene, player1);
            } while (true);
        }

        private static Parser CreateParser()
        {
            var parser = new Parser();

            parser.AddVerbs("GO", "OPEN", "CLOSE", "GIVE", "SHOW", "LOOK", "INVENTORY", "GET", "TAKE", "DROP", "USE", "EXAMINE", "HELP", "QUIT", "ATTACK");
            parser.AddImportantFillers("TO", "ON", "IN");
            parser.AddUnimportantFillers("THE", "A", "AN", "AT");
            parser.AddNouns(
                "NORTH",
                "EAST",
                "WEST",
                "SOUTH",
                "UP",
                "DOWN",
                "KNIFE",
                "BRASS KNUCKLES",
                "METAL BAT",
                "HANDGUN",
                "LASTER SWORD",
                "BFG 9000",
                "BANDAGES",
                "NEEDLE",
                "PILL BOTTLE",
                "HEALING DRONE",
                "MEDKIT",
                "HOODIE",
                "TRUCKER HAT",
                "BICYCLE HELMET",
                "KNEE PADS",
                "ELBOW PADS",
                "ARMORED HELMET",
                "ARMORED VEST",
                "ARMORED PANTS",
                "ENERGY SHEILD"
            );
            parser.Aliases.Add("GO NORTH", "N", "NORTH", "WALK NORTH", "MOVE NORTH", "RUN NORTH");
            parser.Aliases.Add("GO SOUTH", "S", "SOUTH", "WALK SOUTH", "MOVE SOUTH", "RUN SOUTH");
            parser.Aliases.Add("GO WEST", "W", "WEST", "WALK WEST", "MOVE WEST", "RUN WEST");
            parser.Aliases.Add("GO EAST", "E", "EAST", "WALK EAST", "MOVE EAST", "RUN EAST");
            parser.Aliases.Add("GO UP", "U", "UP", "UPSTAIRS", "ASCEND", "RUN UPSTAIRS", "CLIMB UP", "RISE", "UPPER", "WALK UPSTAIRS");
            parser.Aliases.Add("GO DOWN", "D", "DOWN", "DOWNSTAIRS", "DESCEND", "RUN DOWNSTAIRS", "CLIMB DOWN", "LOWER", "WALK DOWNSTAIRS");

            parser.Aliases.Add("ATTACK", "A", "ENGAGE");
            parser.Aliases.Add("INVENTORY", "I", "INV");
            parser.Aliases.Add("LOOK" , "OBSERVE", "OBSERVE SURROUNDINGS", "WHAT DO I SEE", "LOOK AROUND", "VIEW", "SCAN", "EXPLORE", "PEEK");

            parser.Aliases.Add("HELP", "H", "?", "ASSISTANCE", "TUTORIAL", "ASSIST", "CLUE", "HINT","ANSWER");
            parser.Aliases.Add("QUIT", "Q", "EXIT");

            parser.Aliases.Add("ATTACK", "A", "HIT", "FIGHT", "ENGAGE");

            return parser;
        }



        private static void executeCommand(Sentence results, Parser parser, Environment scene, Player player1)
        {

            if (DEBUG)
            {
                Console.WriteLine(results);   //print debug info about parsed sentence
            }
            if (!results.ParseSuccess)
            {
                Console.WriteLine("Excuse Me?");   //Did not recognize command
            }
            else if (results.Ambiguous)
            {
                Console.WriteLine("Be more specfic with {0}", results.Word4.Value.ToLower());
            } else { 
                switch (results.Word1.Value)
                {
                    case "HELP":
                        Console.WriteLine("COMMANDS\n--------\n");
                        parser.PrintVerbs();
                        break;
                    case "QUIT":
                        Console.WriteLine("See Ya\n");
                        System.Environment.Exit(0);
                        break;
                    case "GO":
                        Player.CurrentRoom.movement(results.Word4.Value, player1);
                        scene.CurrentRoom().lookCmd();
                        if (Player.CurrentRoom.enemy != null && Player.CurrentRoom.enemy.HP > 0 && Player.CurrentRoom.enemy.Name != "Adgitated Homeless Man")
                        {
                            Player.CurrentRoom.enemy = engage(Player.CurrentRoom.enemy);
                        }
                        break;
                    case "LOOK":
                        scene.CurrentRoom().lookCmd();
                        break;
                    case "INVENTORY":
                        if (player1.Inventory.Count == 0)
                        {
                            Console.WriteLine("You are not carrying anything");
                        } else
                        {
                            Console.WriteLine("You are carrying");
                            foreach(Item item in player1.Inventory)
                            {
                                Console.WriteLine("  {0}", item.Name);
                            }
                        }
                        break;
                    case "TAKE":
                        if (player1.Inventory.Count == MAX_INVENTORY)
                        {
                            Console.WriteLine("You cannot carry any more items");
                            Console.WriteLine("Drop an item first");
                        } else
                        {
                            if (Player.CurrentRoom.Items.Count != 0)
                            {
                                if (Player.CurrentRoom.Items[0].Type == ItemType.Heal)
                                {
                                    player1.Heals.Add(Player.CurrentRoom.Items[0].Heal);
                                    Console.WriteLine(Player.CurrentRoom.Items[0].Name + " added to heal items");
                                    Player.CurrentRoom.Items = new List<Item> { };
                                } else if (Player.CurrentRoom.Items[0].Type == ItemType.Armor) {
                                    Console.WriteLine("Would you like to equip " + Player.CurrentRoom.Items[0].Name + "? Your current armor is " + player1.ActiveArmor.Name);
                                    bool valid = false;
                                    string temp;
                                    while (!valid)
                                    {
                                        Console.WriteLine("y/n");
                                        temp = Console.ReadLine().ToLower();
                                        if (temp == "y")
                                        {
                                            player1.ActiveArmor = Player.CurrentRoom.Items[0].ArmorItem;
                                            Player.CurrentRoom.Items = new List<Item> { };
                                            valid = true;
                                        } else if (temp == "n") {
                                            valid = true;
                                        } else {
                                            Console.WriteLine("Please enter a valid input");
                                        }
                                    }
                                } else {
                                    Console.WriteLine("Would you like to equip " + Player.CurrentRoom.Items[0].Name + "? Your current Weapon is " + player1.ActiveWeapon.Name);
                                    bool valid = false;
                                    string temp;
                                    while (!valid)
                                    {
                                        Console.WriteLine("y/n");
                                        temp = Console.ReadLine().ToLower();
                                        if (temp == "y")
                                        {
                                            player1.ActiveWeapon = Player.CurrentRoom.Items[0].WeaponItem;
                                            Player.CurrentRoom.Items = new List<Item> { };
                                            valid = true;
                                        }
                                        else if (temp == "n")
                                        {
                                            valid = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please enter a valid input");
                                        }
                                    }
                                }
                            } else {
                                Console.WriteLine("There's nothing here");
                            }
                            //Item item = parseItem((results.Word4.Value).ToLower(),
                            //results.Word4.PrecedingAdjective == null ? null : (results.Word4.PrecedingAdjective.Value).ToLower());
                            //Player.CurrentRoom.take(item, player1);
                        }
                        break;
                    case "DROP":
                        if (player1.Inventory.Count == 0)
                        {
                            Console.WriteLine("You don't have any items to drop");
                        }
                        else
                        {
                            //Item item = parseItem((results.Word4.Value).ToLower(), 
                                //results.Word4.PrecedingAdjective == null ? null : (results.Word4.PrecedingAdjective.Value).ToLower());
                            //Player.CurrentRoom.drop(item, player1);
                        }
                        break;
                    case "ATTACK":
                        if (Player.CurrentRoom.enemy != null )
                        {
                            if (Player.CurrentRoom.enemy.HP > 0)
                            {
                                Player.CurrentRoom.enemy = engage(Player.CurrentRoom.enemy);
                            }
                            else
                            {
                                Console.WriteLine("The {0} is already defeated", Player.CurrentRoom.enemy.Name);
                            }
                        } else { 
                            Console.WriteLine("There isn't any enemy to fight");
                        }
                        break;
                    case "USE":
                        if (player1.Name == "Godly" || player1.Name == "Immortal")
                        {
                            Console.WriteLine("Use dont need this");
                        } else if (player1.Heals.Count == 0)
                        {
                            Console.WriteLine("You dont have any health items");
                        } else
                        {
                            Console.WriteLine("Use which health item?");
                            Console.WriteLine("[0] Cancel");
                            for (int i = 0; i < player1.Heals.Count; i++)
                            {
                                Console.WriteLine("[" + (i+1) + "] " + player1.Heals[i].Name);
                            }
                            bool valid = false;
                            while (!valid)
                            {
                                if (0==0)
                                {

                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Dunno");
                        break;
                }
            }

        }

        static string parseItem (string noun, string adjective) {
            string item;

            if (adjective != null)
            {
                item = adjective + " " + noun;
            }
            else
            {
                item = noun;
            }
            return item;
        }
        public static int battles = 0;
        public static Enemy engage(Enemy enemy) 
        {
            if (battles == 0)
            {
                Console.WriteLine("Welcome to your first battle, your commands are \r\nAttack \r\nEnemy stats \r\nMy stats \r\nCmd");
            } Console.WriteLine("You engage with {0}", enemy.Name);

            battles++;
            bool done = false;
            while (!done)
            {
                switch (Console.ReadLine().ToUpper())
                {
                    case "ATTACK":
                        Console.WriteLine("You attacked the " + enemy.Name + " and did " + player1.ActiveWeapon.Dmg + " damage with your " + player1.ActiveWeapon.Name);
                        enemy.HP -= player1.Dp;
                        if (enemy.HP <= 0)
                        {
                            if (enemy.Name == "Final-Boss")
                            {

                                Console.WriteLine("You killed the evil CEO and finally brought (relative) peace to the city \r\n\r\nYOU WIN!!!");
                                Thread.Sleep(10000);
                                System.Environment.Exit(0);
                            }
                            done = true;
                            Console.WriteLine("You killed the " + enemy.Name);
                        }
                        else
                        {
                            enemyAttack(enemy);
                        }
                        break;
                    case "ENEMY STATS":
                        Console.WriteLine("Name: " + enemy.Name + " \r\nHp: " + enemy.HP + " \r\nMax Dmg: " + enemy.MaxDmg);
                        Random rnd = new Random();
                        if (rnd.Next(2) == 1)
                        {
                            enemyAttack(enemy);
                        }
                        break;
                    case "MY STATS":
                        Console.WriteLine("Name: " + player1.Name + " \r\nHp: " + player1.Hp + " \r\nActive Armor: " + player1.ActiveArmor.Name + " \r\nArmor Defence: " + player1.ActiveArmor.Defence + " \r\nActive Weapon: " + player1.ActiveWeapon.Name + " \r\nMax Dmg: " + player1.ActiveWeapon.Dmg);
                        break;
                    case "USE ITEM":
                        
                        break;
                    case "CMD":
                        Console.WriteLine("\"Attack\" \r\n\"Enemy Stats\"");
                        break;
                    case "FLEE":
                        Console.WriteLine("You fled (Coward)");
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid input");
                        break;
                }
            }
            return enemy;
            void enemyAttack(Enemy enemy)
            {
                int damage = enemy.Dmg;
                if (player1.ActiveArmor != null)
                {
                    if (damage - player1.Ap < 0)
                    {
                        damage = 0;
                    }
                    else
                    {
                        damage -= player1.Ap;
                    }
                }
                player1.Hp -= damage;
                Console.WriteLine("The " + enemy.Name + " hit you for " + damage + " damage");
                if (player1.Hp <= 0)
                {
                    done = true;
                    Console.WriteLine("You died! :(");
                    System.Environment.Exit(0);
                }
            }
        }
    }
}