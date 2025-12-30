using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_BasedCombat.Model
{
    internal class Hero : Character
    {
        public Hero(string Name, int Health, int MaxHealth, int Damage)
            => (this.Name, this.Health, this.MaxHealth, this.Damage) 
            = (Name, Health, MaxHealth, Damage);
        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} for {Damage} damage.");
            target.Defend(Damage);
        }
        public override void Defend(int damage)
        {
            var HealthBefore = damage - 2;
            if(HealthBefore < 0)
                HealthBefore = 0;
            Console.WriteLine($"{Name} used shield and reduced {2} damage. Actual damage taken: {HealthBefore}");
            base.Defend(HealthBefore);

        }
    }

}
