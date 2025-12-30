using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_BasedCombat.Model
{
    internal class Enemy : Character
    {
        public Enemy(string Name ,int Health , int Damage )
            => (this.Name , this.Health , this.MaxHealth,this.Damage) = (Name , Health , Health, Damage);
        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage.");
            target.Defend(Damage);
        }
    }
}
