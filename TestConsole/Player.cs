using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestConsole
{
    public class Player
    {
        private static Room _currentRoom;
        private List<Item> _inventory;
        private List<HealItem> heals = new List<HealItem> { };
        private static int _roomIndex;
        private Weapon activeWeapon;
        private Armor armor;
        private int hp;
        private int maxHp;
        private string name;
        public Player(int startRoom)
        {
            activeWeapon = new Weapon()
            {
                Name = "Fists",
                Dmg = 10
            };
            armor = new Armor()
            {
                Name = "Nothing",
                Defence = 0
            };
            maxHp = 100;
            hp = maxHp;
            name = "Player";
            _roomIndex = startRoom;
            _inventory = new List<Item>();
            _currentRoom = Environment.Scene[_roomIndex];
        }
        public static Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }
        public List<Item> Inventory { get { return _inventory; } }
        public List<HealItem> Heals { get { return heals; } set { heals = value; } }
        public static int RoomIndex { get { return _roomIndex; } set { _roomIndex = value; } }

        public Weapon ActiveWeapon
        {
            get { return activeWeapon; }
            set { activeWeapon = value; }
        }
        public Armor ActiveArmor
        {
            get { return armor; }
            set { armor = value; }
        }
        public int Ap
        {
            get
            {
                if (name == "Mighty" || name == "Godly")
                {
                    return 2147483647;
                }
                else if (name == "Weak" || name == "Hell")
                {
                    return 1;
                }
                else
                {
                    return armor.Defence;
                }
            }
        }
        public int Dp
        {
            get
            {
                if (name == "Mighty" || name == "Godly")
                {
                    return 2147483647;
                }
                else if (name == "Frail" || name == "Hell")
                {
                    return 1;
                }
                else
                {
                    return activeWeapon.Dmg;
                }
            }
        }
        public int Hp
        {
            get { return hp; } 
            set { 
                if (value > maxHp)
                {
                    hp = maxHp;
                }
                else
                {
                    hp = value;
                }
            }
        }
        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public string Name
        {
            get { return name; }
            set {
                switch (value)
                {
                    case "Immortal":
                        MaxHp = 2147483647;
                        Hp = 2147483647;
                        break;
                    case "Godly":
                        MaxHp = 2147483647;
                        Hp = 2147483647;
                        break;
                    case "Mortal":
                        MaxHp = 1;
                        Hp = 1;
                        break;
                    case "Hell":
                        MaxHp = 1;
                        Hp = 1;
                        break;
                    default:
                        break;
                }
                name = value;
            }
        }
    }
}
