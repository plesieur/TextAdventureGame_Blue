using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Enemies
    {
        List<Enemy> enemyList = new List<Enemy>();
        public Enemies()
        {
            enemyList.Add(new Enemy()
            {
                Name = "Street Thug",
                HP = 15,
                Dmg = 10
            });
            enemyList.Add(new Enemy()
            {
                Name = "Security Guard",
                HP = 20,
                Dmg = 15
            });
            enemyList.Add(new Enemy()
            {
                Name = "Enemy",
                HP = 30,
                Dmg = 20
            });
            enemyList.Add(new Enemy()
            {
                Name = "Drone",
                HP = 10,
                Dmg = 5
            });
            enemyList.Add(new Enemy()
            {
                Name = "Goon",
                HP = 18,
                Dmg = 13
            });
            enemyList.Add(new Enemy()
            {
                Name = "Robot",
                HP = 25,
                Dmg = 20
            });
        }
        public Enemy getEnemy()
        {
            Random rnd = new Random();
            return enemyList[rnd.Next(enemyList.Count)];
        }
    }
    public class Enemy {
        private string name;
        private int hp;
        private int dmg;
        public Enemy ()
        {
            name = "";
            hp = 0;
            dmg = 0;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int MaxDmg
        {
            get { return dmg; }
        }
        public int Dmg
        {
            get {
                Random rnd = new Random();
                return rnd.Next(dmg);
            }
            set { dmg = value; }
        }
    }
}
