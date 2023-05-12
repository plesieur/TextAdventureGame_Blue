using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Items
    {
        HealItems heals;
        ArmorItems armors;
        Weapons weapons;
        public Items()
        {
            heals = new HealItems();
            armors = new ArmorItems();
            weapons = new Weapons();
        }
        public HealItem getHeal()
        {
            return heals.get();
        }
        public Armor getArmor()
        {
            return armors.get();
        }
        public Weapon getWeapon()
        {
            return weapons.get();
        }

    }
    public class Item {
        HealItems heals;
        ArmorItems armors;
        Weapons weapons;
        private string name;
        HealItem healItem;
        Armor armor;
        Weapon weapon;
        public enum ItemType
        {
            Heal,
            Armor,
            Weapon
        }
        ItemType type;
        public Item()
        {
            heals = new HealItems();
            armors = new ArmorItems();
            weapons = new Weapons();
            Random rnd = new Random();
            List<string> itemNames = new List<string>();
            int rndint = rnd.Next(3);
            if (rndint == 0)
            {
                type = ItemType.Heal;
                healItem = heals.get();
                name = healItem.Name;

            } else if (rndint == 1)
            {
                type = ItemType.Armor;
                armor = armors.get();
                name = armor.Name;
            } else
            {
                type = ItemType.Weapon;
                weapon = weapons.get();
                name = weapon.Name;
            }
        }
        public ItemType Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Armor ArmorItem
        {
            get { return armor; }
            set { armor = value; }
        }
        public Weapon WeaponItem
        {
            get { return weapon; }
            set { weapon = value; }
        }
        public HealItem Heal
        {
            get { return healItem; }
            set { healItem = value; }
        }
    }


    public class HealItems
    {
        List<HealItem> healList = new List<HealItem>();
        public HealItems()
        {
            healList.Add(new HealItem()
            {
                Name = "Needle",
                HPUp = 15
            });
            healList.Add(new HealItem()
            {
                Name = "Pill Bottle",
                HPUp = 25
            });
            healList.Add(new HealItem()
            {
                Name = "Healing Drone",
                HPUp = 30
            });
            healList.Add(new HealItem()
            {
                Name = "Bandages",
                HPUp = 50
            });
            healList.Add(new HealItem()
            {
                Name = "Medkit",
                HPUp = 100
            });
        }
        public HealItem get()
        {
            Random rnd = new Random();
            return healList[rnd.Next(healList.Count)];
        }
        public List<HealItem> getNames()
        {
            return healList;
        }
    }
    public class HealItem
    {
        private string name;
        private int hpUp;
        public HealItem()
        {
            name = "";
            hpUp = 0;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int HPUp {
            get { return hpUp; }
            set { hpUp = value; }
        }
    }

    public class ArmorItems
    {
        List<Armor> armorList = new List<Armor>();
        public ArmorItems()
        {
            armorList.Add(new Armor()
            {
                Name = "Trucker Hat",
                Defence = 3
            });
            armorList.Add(new Armor()
            {
                Name = "Hoodie",
                Defence = 5
            });
            armorList.Add(new Armor()
            {
                Name = "Bicycle Helmet",
                Defence = 10
            });
            armorList.Add(new Armor()
            {
                Name = "Knee Pads",
                Defence = 15
            });
            armorList.Add(new Armor()
            {
                Name = "Elbow Pads",
                Defence = 17
            });
            armorList.Add(new Armor()
            {
                Name = "Armored Helmet",
                Defence = 25
            });
            armorList.Add(new Armor()
            {
                Name = "Armored Pants",
                Defence = 30
            });
            armorList.Add(new Armor()
            {
                Name = "Armored Vest",
                Defence = 35
            });
            armorList.Add(new Armor()
            {
                Name = "Shield",
                Defence = 45
            });


        }
        public Armor get()
        {
            Random rnd = new Random();
            return armorList[rnd.Next(armorList.Count)];
        }
        public List<Armor> getNames()
        {
            return armorList;
        }
    }
    public class Armor
    {
        private string name;
        private int defence;
        public Armor()
        {
            name = "";
            defence = 0;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Defence
        {
            get { return defence; }
            set { defence = value; }
        }
    }

    public class Weapons
    {
        List<Weapon> weaponList = new List<Weapon>();
        public Weapons()
        {
            weaponList.Add(new Weapon()
            {
                Name = "Knife",
                Dmg = 12
            });
            weaponList.Add(new Weapon()
            {
                Name = "Brass Knuckles",
                Dmg = 16
            });
            weaponList.Add(new Weapon()
            {
                Name = "Metal Bat",
                Dmg = 20
            });
            weaponList.Add(new Weapon()
            {
                Name = "Sword",
                Dmg = 25
            });
            weaponList.Add(new Weapon()
            {
                Name = "Handgun",
                Dmg = 30
            });
            weaponList.Add(new Weapon()
            {
                Name = "BFG 9000",
                Dmg = 40
            });
        }
        public Weapon get()
        {
            Random rnd = new Random();
            return weaponList[rnd.Next(weaponList.Count)];
        }
        public List<Weapon> getNames()
        {
            return weaponList;
        }
    }
    public class Weapon
    {
        private string name;
        private int dmg;
        public Weapon()
        {
            name = "";
            dmg = 0;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Dmg
        {
            get { Random rnd = new Random(); return rnd.Next(dmg)+1; }
            set { dmg = value; }
        }
    }
}
