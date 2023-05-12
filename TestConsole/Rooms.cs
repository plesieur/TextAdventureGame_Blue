using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestConsole.Item;

namespace TestConsole
{
    public class Room
    {
        private string _name;
        private string _description;
        private List<Item> _items = new List<Item>();
        private int[] _direction;
        private Enemy _enemy = null;
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public List<Item> Items
        {
            get { return _items; }
            set
            {
                /*foreach (Item item in value)
                {
                    _items.Add(item);
                }*/
                _items = value;
            }
        }

        public int[] Dir {
            get { return _direction; }
            set {
                _direction = value;
            }
        }

        public void movement(string direction, Player player1)
        {
            List<string> directions = new List<string>() { "NORTH", "SOUTH", "EAST", "WEST", "UP", "DOWN" };
            int location = Player.RoomIndex;

            int dir = directions.IndexOf(direction);
            if (this.Dir[dir] != -1)
            {   
                location = this.Dir[dir];
                if (Environment.Scene[location].Name == "Room 8" && Environment.Scene[63].enemy.HP > 0) {
                    Console.WriteLine("Before entering this building you must defeat 'Mini Boss 1'");
                } else if (Environment.Scene[location].Name == "Room 14" && Environment.Scene[64].enemy.HP > 0) {
                    Console.WriteLine("Before entering this building you must defeat 'Mini Boss 2'");
                } else if (Environment.Scene[location].Name == "Room 13" && Environment.Scene[65].enemy.HP > 0) {
                    Console.WriteLine("Before entering this building you must defeat 'Mini Boss 3'");
                } else {
                    Player.RoomIndex = location;
                    Player.CurrentRoom = Environment.Scene[location];
                }
                
            } else
            {
                Console.WriteLine("WALL\n");
            }
        }

        public void take(Item item, Player player1)
        {

            if (this.Items.Contains(item))
            {
                player1.Inventory.Add(item);
                this.Items = new List<Item> { };
                Console.WriteLine("You have taken {0}", item);
            } else
            {
                Console.WriteLine("I don't see the {0}", item);
            }

        }

        public void drop(Item item, Player player1)
        {

            if (player1.Inventory.Contains(item))
            {
                player1.Inventory.Remove(item);
                this.Items.Add(item);
                Console.WriteLine("You have dropped the {0}", item);
            }
            else
            {
                Console.WriteLine("You don't have the {0}", item);
            }

        }

        public void lookCmd()
        {
            Console.WriteLine("You are in the {0}", _name);
            Console.WriteLine(_description);
            if (_items.Count > 0)
            {
                Console.WriteLine("You see");
                foreach (Item item in _items) { Console.WriteLine("  {0}", item); }
            }
        }

        public Enemy enemy {
            get { return _enemy; }
            set { _enemy = value; }
        }
    }

    public class Environment
    {
        private static List<Room> _scene = new List<Room>();
        private static Items items = new Items();
        public Environment()
        {
            _scene = CreateRooms();
        }

        public static List<Room> Scene { get { return _scene; } } 

        public Room CurrentRoom()
        {
            return _scene [Player.RoomIndex];
        }
        private static List<Room> CreateRooms()
        {
            Enemies enemies = new Enemies();
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room()
            {
                Name = "Start 1",
                Description = "Welcome to T.A.G, a Text Adventure Game where you can explore the vast city of Neo Titania and experience all that encompasses it.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 25, -1, -1, -1, -1 }
                //today is april 26
            });

            rooms.Add(new Room()
            {
                Name = "Room 1",
                Description = "In this room, there is a chair to the west and there is a door to the North.",
                Items = new List<Item> { },
                Dir = new int[6] { 36, -1, -1, 67, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 2",
                Description = "In this location, there is a chair with a desk near it, and there is also a door that goes North and a door that goes South.",
                Items = new List<Item> { },
                Dir = new int[6] { 63, 68, -1, 36, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 3",
                Description = "In this room, there is a single door leading westward. You feel like there is something hidden in this room.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, -1, 36, 79, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 4",
                Description = "Inside this room, there is a couch and a tv, the tv has static on it and there is a door facing west of your current location.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 36, -1, 66, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 5",
                Description = "You are in a room that seems to be a lounge. If you look north you could see an entrance to a library section of the building.",
                Items = new List<Item> { },
                Dir = new int[6] { 72, -1, -1, 56, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 6",
                Description = "North of you there is a door that has a sign on it, it reads “Do not Disturb”. It would be wise to listen to the sign.",
                Items = new List<Item> { },
                Dir = new int[6] { 7, 73, 57, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 7",
                Description = "Inside this room, you could see unused furniture that was probably donated to this shelter, East of you is a door.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 6, 58, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 8",
                Description = "This room is mostly empty apart from a desk that looks like it belongs at an airport. There is a door to the East that looks like it's made of steel.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, -1, 9, 29, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 9",
                Description = "In this room, there are three doors, one to the west, one to the east, and one to the South. You could hear the faint humming of electronics.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 11, 10, 8, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 10",
                Description = "In this room, there is a storage container that appears to be locked incredibly securely, South of you there is a door.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 40, -1, 9, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 11",
                Description = "This room looks empty apart from the tv and leather couch situated in the corner, West of you is a door and you could hear a faint conversation coming from the other side of it.",
                Items = new List<Item> { },
                Dir = new int[6] { 9, -1, -1, 39, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 12",
                Description = "This room is empty, There is a door Westward and a door North.",
                Items = new List<Item> { },
                Dir = new int[6] { 40, -1, -1, 64, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 13",
                Description = "As you entered this room you could hear the door close behind you. You feel like you're going to have a bad time. There is a door facing East and a door facing South.",
                Items = new List<Item> { },
                Dir = new int[6] { 33, 45, 44, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 14",
                Description = "This room is mostly empty, There is one door facing East and the exit is behind you.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 34, 15, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 15",
                Description = "This room has a faint smell of paint and the wallpaper is peeling. There is a door to your East and a door to your North.",
                Items = new List<Item> { },
                Dir = new int[6] { 17, -1, 16, 14, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 16",
                Description = "Inside this room, it looks like a janitor's closet. There isn’t a single window or a lightbulb in here.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, -1, -1, 15, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 17",
                Description = "This room smells like chlorine and wet paint. There is a door to the West and the East of this room. There is also a door to the North.",
                Items = new List<Item> { },
                Dir = new int[6] { 19, 15, 18, 41, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 18",
                Description = "Within this room, there is a bed and a small table next to it. There is a room to the North of you.",
                Items = new List<Item> { },
                Dir = new int[6] { 70, -1, -1, 17, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 19",
                Description = "This room looks like it used to be a person's bedroom. There is a window to the North and a poster left on the walls.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 17, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 20",
                Description = "Inside this room, there is a door to the East and a door to the South.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 21, 42, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 21",
                Description = "Within this room, there are 3 doors, One to the South, one to the West, and one to the North.",
                Items = new List<Item> { },
                Dir = new int[6] { 20, 22, -1, 43, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 22",
                Description = "In this room, there is a door to the west.",
                Items = new List<Item> { },
                Dir = new int[6] { 21, -1, -1, 71, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 23",
                Description = "This room has a door to the stairway. You could smell the chemical fumes dripping from the ceiling.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 51, -1, 52, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Room 24",
                Description = "Standing in this room gives you a feeling of anxiety, You question if you can do this and if you can save humanity. These thoughts plague you for a moment before exiting.",
                Items = new List<Item> { },
                Dir = new int[6] { 53, -1, 54, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 1",
                Description = "You have encountered an intersection before you lie two possible streets you can walk down.",
                Items = new List<Item> { },
                Dir = new int[6] { 0, 61, 26, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 2",
                Description = "There are two buildings both in front of you and South of you.",
                Items = new List<Item> { },
                Dir = new int[6] { 67, -1, 75, 25, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 3",
                Description = "You walk into a streetway and there is a marketplace to the North of you.",
                Items = new List<Item> { },
                Dir = new int[6] { 77, -1, 28, 75, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 4",
                Description = "You walk into a streetway and there is a homeless shelter North and a park South and you see a street across from you.",
                Items = new List<Item> { },
                Dir = new int[6] { 56, 80, 29, 27, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 5",
                Description = "You walk into this street and there is a building East and a street South.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 30, 8, 28, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 6",
                Description = "You walk into this street and you see an enemy South and a park West.",
                Items = new List<Item> { },
                Dir = new int[6] { 29, 59, -1, 80, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 7",
                Description = "You walk onto the street and you see another street.",
                Items = new List<Item> { },
                Dir = new int[6] { 59, -1, -1, 32, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 8",
                Description = "When you go onto this street you see a park north and a street across from you.",
                Items = new List<Item> { },
                Dir = new int[6] { 80, -1, 31, 33, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 9",
                Description = "You walk into the street and you see a skyscraper South and a street across.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 13, 32, 60, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 10",
                Description = "North of you is a building but it also seems to be locked and it is guarded by armed guards.",
                Items = new List<Item> { },
                Dir = new int[6] { 14, -1, 60, 35, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Street 11",
                Description = "If you walk east you can see a thug not too far from you.",
                Items = new List<Item> { },
                Dir = new int[6] { 61, -1, 34, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 1",
                Description = "You enter this room and encounter an enemy and you see that there are 3 other doors around.",
                Items = new List<Item> { },
                Dir = new int[6] { 4, 1, 2, 3, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 2",
                Description = "You enter this alley and you see an enemy and beyond that enemy is more of the alley.",
                Items = new List<Item> { },
                Dir = new int[6] { 38, 75, -1, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 3",
                Description = "You go deeper into the alley and you see an enemy again and you see a dead end with loot in a corner.",
                Items = new List<Item> { },
                Dir = new int[6] { 81, 37, -1, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 4",
                Description = "You enter this room and see an enemy and another door to your left.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 69, 11, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 5",
                Description = "You enter and see an enemy and see a door across from you.",
                Items = new List<Item> { },
                Dir = new int[6] { 10, 12, -1, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 6",
                Description = "You go upstairs and enter a room and see an enemy and a door to your left.",
                Items = new List<Item> { },
                Dir = new int[6] { 48, -1, 17, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 7",
                Description = "You enter the room and see an enemy and you see a door to your right.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 76, -1, 20, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 8",
                Description = "You go upstairs and enter a room and see an enemy and a door to your left.",
                Items = new List<Item> { },
                Dir = new int[6] { 49, -1, 21, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 9",
                Description = "You walk into this room and you see an enemy and a door that says stairs.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 50, -1, 13, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 10",
                Description = "You walk into this room and you see an enemy and a door that says stairs.",
                Items = new List<Item> { },
                Dir = new int[6] { 13, -1, 50, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 11",
                Description = "You walk into this room and you see an enemy and a door that says stairs.",
                Items = new List<Item> { },
                Dir = new int[6] { 52, -1, 51, -1, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Enemy 12",
                Description = "You walk into this room and you see an enemy and a door that says stairs.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 54, -1, 53, -1, -1 },
                enemy = enemies.getEnemy()
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 1",
                Description = "You have entered a stairwell, it only goes down and the walls seem to be made from old concrete. They have some indents and cracks in random places.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 41, -1, -1, -1, 49 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 2",
                Description = "At the bottom of the stairway there is a single door to your South, the door looks to be made of a heavy blast-resistant alloy. As you were stepping down the staircase you suddenly smelled a scent that smelled like a blend of laundry detergent and some chemical you can’t discern.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 43, -1, -1, 48, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 3",
                Description = "You see a winding staircase before you, you have blood on your hands and determination in your eyes. There is still a winding road that leads to the end.",
                Items = new List<Item> { },
                Dir = new int[6] { 44, -1, -1, 45, 51, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 4",
                Description = "Once you had finished climbing the steps you suddenly felt a bit heavier. Before you, there are two doors.",
                Items = new List<Item> { },
                Dir = new int[6] { 23, -1, -1, 46, -1, 50 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 5",
                Description = "Before you is a staircase, it appears to be even more winding than the previous steps. Unlike the one, before you cannot hear any speaking or music, You could feel the tension building within your mind.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 46, 23, -1, 53, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 6",
                Description = "There are two doors before you, unlike the floors you climbed previously there doesn’t seem to be any windows or any source of natural light. You could hear a faint electrical buzzing.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 24, 47, -1, -1, 52 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 7",
                Description = "Before you is a staircase. One single pathway to the battle that destiny has destined you to fight, At the top of the staircase you can see a dark oak door that has some blood stained on it.",
                Items = new List<Item> { },
                Dir = new int[6] { 47, -1, -1, 24, 55, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Stairs 8",
                Description = "Despite every nerve and cell of your body telling you to go downstairs and retreat your body didn’t listen. Before you stand the door that will determine the fate of everyone living in Neo Titania City.",
                Items = new List<Item> { },
                Dir = new int[6] { 82, -1, -1, 82, -1, 54 }
            });

            rooms.Add(new Room()
            {
                Name = "Hall 1",
                Description = "Before you there is a long hallway, There is some calm music playing in the background and you could hear some children playing a couple of rooms away.",
                Items = new List<Item> { },
                Dir = new int[6] { 57, 28, 5, 73, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Hall 2",
                Description = "A few people are relaxing on couches and talking casually. There is also a young-looking couple dancing together.",
                Items = new List<Item> { },
                Dir = new int[6] { 58, 56, 72, 6, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Hall 3",
                Description = "At the end of the hall there are quite a few boxes and some large storage units.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 57, 74, 7, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Thug 1",
                Description = "You walk onto the street and you see a thug, who is armed with a crowbar.",
                Items = new List<Item> { },
                Dir = new int[6] { 30, 31, 62, -1, -1, -1 },
                enemy = new Enemy() { Name = "Street Thug", HP = 15, Dmg = 10 }
            });

            rooms.Add(new Room()
            {
                Name = "Thug 2",
                Description = "You walk onto the street and you see a thug who has a muscular and imposing appearance, He is armed with a knife.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, -1, 33, 34, -1, -1 },
                enemy = new Enemy() { Name = "Street Thug", HP = 15, Dmg = 10 }
            });

            rooms.Add(new Room()
            {
                Name = "Thug 3",
                Description = "You walk onto the street and you see a thug wearing a backward headband, She is armed with a handgun.",
                Items = new List<Item> { },
                Dir = new int[6] { 25, 35, -1, -1, -1, -1 },
                enemy = new Enemy() { Name = "Street Thug", HP = 15, Dmg = 10 }
            });

            rooms.Add(new Room()
            {
                Name = "Alley 1",
                Description = "There is an alleyway that looks deep. You could faintly see a few security guards and it looks like they are guarding something.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 78, -1, 59, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Mini-Boss 1",
                Description = "Before you stand a hulking man, He has an incredibly muscular body and stands at about 8 Feet. He also wields a big club.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 2, -1, -1, -1, -1 },
                enemy = new Enemy() { Name = "Mini-Boss 1", HP = 100, Dmg = 50 }
            });

            rooms.Add(new Room()
            {
                Name = "Mini-Boss 2",
                Description = "Before you there is a person who looks to be normal height, she stands up and you see that she is a special forces agent due to her body armor. She holds a small SMG and you could see a knife on her waist.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, -1, 12, -1, -1, -1 },
                enemy = new Enemy() { Name = "Mini-Boss 2", HP = 100, Dmg = 50 }
            });

            rooms.Add(new Room()
            {
                Name = "Mini-Boss 3",
                Description = "Inside this room a man is sitting on a meditation cushion. On his back, you see that he has a scar and you instantly recognize this man. He is the man who arrested your father before you were born.",
                Items = new List<Item> { },
                Dir = new int[6] { 76, -1, -1, -1, -1, -1 },
                enemy = new Enemy() { Name = "Mini-Boss 3", HP = 100, Dmg = 50 }
            });
            HealItem hTemp = new HealItems().get();
            rooms.Add(new Room()
            {
                Name = "Health Item 1",
                Description = "You could see a standard issue health drone in the corner of the room, It would be wise to heal up any damages you might have accrued.",
                Items = new List<Item> { new Item() { Type = ItemType.Heal, Heal = hTemp, Name = hTemp.Name } },
                Dir = new int[6] { -1, -1, 4, -1, -1, -1 }
            });
            Weapon wTemp = new Weapons().get();
            rooms.Add(new Room()
            {
                Name = "Weapon 1",
                Description = "In this room, you see a desk right in the center of the room. You could see one of the drawers is open and there is a handgun visible.",
                Items = new List<Item> { new Item() { Type = ItemType.Weapon, WeaponItem = wTemp, Name = wTemp.Name } },
                Dir = new int[6] { -1, 26, 1, -1, -1, -1 }
            });
            
            rooms.Add(new Room()
            {
                Name = "Sword 1",
                Description = "After slamming your body weight into the door and breaking it open you could see a sword attached to the wall. The sword has lights that track along the outer part of the blade.",
                Items = new List<Item> { new Item() { Type = ItemType.Weapon, Name = "Sword", WeaponItem = new Weapon() { Name = "Sword", Dmg = 25 } } },
                Dir = new int[6] { 2, -1, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Item 1",
                Description = "As you enter this room you could see a box in the corner.",
                Items = new List<Item> { new Item() },
                Dir = new int[6] { 39, -1, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Item 2",
                Description = "This room has a strange smell that smells kinda like perfume. In the center of the room, you could see a desk with many compartments inside it.",
                Items = new List<Item> { new Item() },
                Dir = new int[6] { -1, 18, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Item 3",
                Description = "This room has many storage crates inside, It seems like it is weapon storage.",
                Items = new List<Item> { new Item() },
                Dir = new int[6] { -1, -1, 22, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Library 1",
                Description = "You see shelf after shelf full to the brim of books, such things became unnecessary with the advent of the Cloud and its storage capabilities.",
                Items = new List<Item> { },
                Dir = new int[6] { 74, 5, -1, 57, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Grub Hall 1",
                Description = "There are many tables and chairs. The scent of the food brings you back to your childhood.",
                Items = new List<Item> { },
                Dir = new int[6] { 6, -1, 56, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Rest Area 1",
                Description = "Within this room there is just a simple bed and dresser, Along with some paintings on the walls. It makes your mind feel at home and like your soul has been soothed.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 72, -1, 58, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "AHM 1",
                Description = "You walk on a street and you find a homeless man who seems agitated.",
                Items = new List<Item> { },
                Dir = new int[6] { 37, -1, 27, 26, -1, -1 },
                enemy = new Enemy() { Name = "Adgitated Homeless Man", HP = 500, Dmg = 1 }
            });

            rooms.Add(new Room()
            {
                Name = "Puzzle 1",
                Description = "Before you is a door, however, it doesn’t seem you can open it. Luckily enough there is an electrical box on the left side of the wall and maybe you can figure out how to open the door.",
                Items = new List<Item> { },
                Dir = new int[6] { 42, 65, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Marketplace 1",
                Description = "You could see what looks like hundreds of vendors selling food and many other commodities. The sheer amount of noise and conflicting smells cause you to feel slightly overwhelmed.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 27, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Black Market 1",
                Description = "You could see many vendors who are not just selling food and beverages but are also offering explicit services, you could also see some people hiring assassins.",
                Items = new List<Item> { },
                Dir = new int[6] { 62, -1, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Easter Egg 1",
                Description = "You touch a rock, and you could see the universe deconstruct and reconstruct itself right before your very eyes. You wanted to scream but your body wouldn't let you, You could feel your mind deconstructing itself all because you touched a rock.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, -1, 3, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Park 1",
                Description = "As you enter the park you could feel a sense of dread being lifted from your shoulders, Children play near the trees and the play sets, and People are walking around and having nice conversations. This is one of the few places where people can find comfort while everything else burns outside them.",
                Items = new List<Item> { },
                Dir = new int[6] { 28, 32, 30, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Loot 1",
                Description = "After defeating the guards guarding the storage unit you can finally gain access to the contents within, hopefully, this will help in the fight against Exgol.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 38, -1, -1, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Final Boss",
                Description = "You could feel your adrenaline building with every passing moment, You glare at the man who has single-handedly caused so much pain for the people of Neo Titania. He looks into your eyes with an expression of intrigue hidden by a veneer of boredom. This battle will decide everything.",
                Items = new List<Item> { },
                Dir = new int[6] { -1, 55, 55, -1, -1, -1 },
                enemy = new Enemy() { Name = "Final-Boss", HP = 200, Dmg = 0 }
            });


            return rooms;
        }

    }

}
