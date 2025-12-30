using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turn_BasedCombat.Model;

namespace Turn_BasedCombat.Core
{
    /// <summary>
    /// Manages the core combat logic, handles turn rotation using a Circular LinkedList, 
    /// and controls interactions between the Hero and Enemies.
    /// </summary>
    internal class BattleSystem
    {
        /// <summary>
        /// A linked list containing all active characters (Hero and Enemies) in the battle.
        /// </summary>
        private LinkedList<Character> _fighters;

        /// <summary>
        /// A pointer node tracking whose turn it is currently.
        /// </summary>
        private LinkedListNode<Character>? _currentFighterNode;

        /// <summary>
        /// A list to store characters that have been defeated (The Graveyard).
        /// </summary>
        private List<Character> _dieds;

        /// <summary>
        /// Random number generator for enemy spawning and game logic.
        /// </summary>
        private Random _random;

        /// <summary>
        /// Initializes a new instance of the BattleSystem.
        /// Sets up the Hero, generates random Enemies, and prepares the turn order.
        /// </summary>
        /// <param name="Name">The name of the player's Hero.</param>
        public BattleSystem(string Name)
        {
            _fighters = new LinkedList<Character>();
            _fighters.AddLast(new Hero(Name, 100, 100, 20));
            _random = new Random();
            RandoEnemy(); // Spawns enemies
            _currentFighterNode = _fighters.First!;
            _dieds = new List<Character>();
        }

        /// <summary>
        /// Starts the main game loop. 
        /// Continues until all enemies are defeated or the hero falls.
        /// Checks for win/loss conditions at the start of each cycle.
        /// </summary>
        public void StartBattle()
        {
            Console.WriteLine("Battle Start!");
            while (_fighters.Count > 0)
            {
                // Check Victory Condition
                if (!_fighters.Any(f => f is Enemy))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Print(); // Show graveyard
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n VICTORY! All enemies are defeated!");
                    Console.ResetColor();
                    break;
                }

                // Check Defeat Condition
                if (!_fighters.Any(f => f is Hero))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n DEFEAT! All heroes are defeated!");
                    Console.ResetColor();
                    break;
                }

                if (_currentFighterNode == null) return;

                // Execute the turn logic
                HandleTurn();

                // Rotate to the next fighter
                MoveNext();
            }
            Console.WriteLine("\n\t\t\t\tBattle Ended!");
        }

        /// <summary>
        /// Advances the turn to the next fighter in the Circular LinkedList.
        /// Detects if the current fighter is dead, adds them to the graveyard list, 
        /// and removes their node from the active battle list.
        /// </summary>
        private void MoveNext()
        {
            if (_currentFighterNode == null)
                return;

            var isDead = !_currentFighterNode.Value.IsAlive; // Changed logic variable name for clarity

            if (isDead)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {_currentFighterNode.Value.Name} removed from battle.");
                Console.ResetColor();

                // Store next node before removing current
                var nextnode = _currentFighterNode.Next ?? _fighters.First;

                // Archive and remove
                _dieds.Add(_currentFighterNode.Value);
                _fighters.Remove(_currentFighterNode);

                _currentFighterNode = nextnode;
            }
            else
            {
                // Circular movement: If Next is null, go back to First
                _currentFighterNode = _currentFighterNode.Next ?? _fighters.First;
            }
        }

        /// <summary>
        /// Generates a random number of enemies (between 1 and 4) with randomized health stats
        /// and adds them to the fighters list.
        /// </summary>
        private void RandoEnemy()
        {
            int enemycount = _random.Next(1, 5);
            for (int i = 0; i < enemycount; i++)
            {
                int HealthEnemy = _random.Next(23, 100);
                _fighters.AddLast(new Enemy($"Fulul{i + 1}", HealthEnemy, 1));
            }
        }

        /// <summary>
        /// Determines the action logic based on the current fighter type.
        /// <para>If Hero: Displays a list of enemies and prompts user for target selection.</para>
        /// <para>If Enemy: Automatically targets the first available Hero using basic AI.</para>
        /// </summary>
        private void HandleTurn()
        {
            if (_currentFighterNode == null)
                return;

            var Character = _currentFighterNode.Value;

            // --- Hero Logic ---
            if (Character is Hero hero)
            {
                // Filter alive enemies
                var target = _fighters.Where(f => f is Enemy && f.IsAlive).ToList();
                if (target.Count == 0)
                {
                    Console.WriteLine("All enemies are defeated");
                    return;
                }

                // Display Options
                for (int i = 0; i < target.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {target[i]}");
                }

                Console.WriteLine("Choose a target to attack:");
                var input = Console.ReadLine();

                // Process Input
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= target.Count)
                {
                    hero.Attack(target[choice - 1]);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Turn skipped.");
                }
            }
            // --- Enemy Logic ---
            else if (Character is Enemy enemy)
            {
                var target = _fighters.FirstOrDefault(f => f is Hero && f.IsAlive);
                if (target != null)
                {
                    enemy.Attack(target);
                }
                else
                {
                    Console.WriteLine("All heroes are defeated! Enemies win!");
                    return;
                }
            }
        }

        /// <summary>
        /// Prints the summary of all defeated enemies (The Graveyard) to the console.
        /// Called automatically upon victory.
        /// </summary>
        private void Print()
        {
            Console.WriteLine("\n--- The enemies who were killed ---");
            foreach (var died in _dieds)
            {
                Console.WriteLine(died);
            }
            Console.WriteLine("-----------------------------\n");
        }
    }
}