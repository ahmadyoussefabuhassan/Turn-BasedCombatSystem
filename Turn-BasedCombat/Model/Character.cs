using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_BasedCombat.Model
{
    /// <summary>
    /// Represents the base abstract class for all combat entities (Heroes and Enemies).
    /// Contains shared properties and methods for health management and combat interaction.
    /// </summary>
    internal abstract class Character
    {
        /// <summary>
        /// Gets or sets the name of the character.
        /// initialized to null! to suppress compiler warnings, assuming it will be set via constructor.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the current health points of the character.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the maximum health points this character can have.
        /// Used for UI display and healing caps.
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the base damage output of the character.
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Gets a value indicating whether the character is currently alive (Health > 0).
        /// </summary>
        public bool IsAlive => Health > 0;

        /// <summary>
        /// Performs an attack action against a specified target.
        /// Implementation depends on the specific character type (Hero or Enemy).
        /// </summary>
        /// <param name="target">The character receiving the attack.</param>
        public abstract void Attack(Character target);

        /// <summary>
        /// Handles receiving damage from an attack.
        /// Reduces health by the damage amount and ensures health does not drop below zero.
        /// </summary>
        /// <param name="damage">The amount of damage to inflict.</param>
        public virtual void Defend(int damage)
        {
            Health -= damage;
            // Prevent health from becoming negative
            if (Health < 0)
                Health = 0;
        }

        /// <summary>
        /// Returns a string representation of the character's current status.
        /// </summary>
        /// <returns>A formatted string containing Name, current Health/MaxHealth, and Damage.</returns>
        override public string ToString()
        {
            return $"Name: {Name}, Health: {Health}/{MaxHealth}, Damage: {Damage}";
        }
    }
}