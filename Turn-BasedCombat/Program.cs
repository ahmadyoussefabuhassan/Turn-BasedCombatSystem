using Turn_BasedCombat.Core;

namespace Turn_BasedCombat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your hero's name:");
            string heroName = Console.ReadLine() ?? "Hero";
            BattleSystem battleSystem = new BattleSystem(heroName);
            battleSystem.StartBattle(); 
        }
    }
}
