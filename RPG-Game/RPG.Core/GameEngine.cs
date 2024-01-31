namespace RPG.Core;

using Contracts;
using Data.Contracts;
using GameModels;
using GameModels.Enums;

using static Common.GameConstants.MatrixFieldConstants;
using static Common.GameConstants.CharacterSelectConstants;

internal class GameEngine : IGameEngine
{
    private readonly char[,] field;

    private readonly IStatsLogger statsLogger;

    public GameEngine(IStatsLogger statsLogger)
    {
        this.field = new char[FieldSize, FieldSize];
        this.statsLogger = statsLogger;
    }

    public async Task PlayGame()
    {
        LoadMenu();

        MenuOptions command = MenuOptions.CharacterSelect;

        while (command != MenuOptions.Exit)
        {
            switch (command)
            {
                case MenuOptions.MainMenu:
                    LoadMenu();
                    break;

                case MenuOptions.CharacterSelect:
                case MenuOptions.InGame:

                    int characterChoice = LoadCharacterSelect();

                    BaseGameModel character;

                    switch (characterChoice)
                    {
                        case WarriorMenuNumber:
                            character = new Warrior();
                            break;
                        case ArcherMenuNumber:
                            character = new Archer();
                            break;
                        case MageMenuNumber:
                            character = new Mage();
                            break;
                        default:
                            Console.WriteLine("Invalid character selected!");
                            continue;
                    }

                    Monster monster = new Monster();

                    InitField(character.Symbol);

                    break;
            }

            command = ReadCommand();
        }
    }

    private MenuOptions ReadCommand()
    {
        bool isValidCommand;
        MenuOptions command;
        do
        {
            isValidCommand = Enum.TryParse(Console.ReadLine()!, out command);

            if (!isValidCommand)
            {
                Console.WriteLine("Invalid command!");
            }

        } while (!isValidCommand);

        return command;
    }

    private void LoadMenu()
    {
        Console.WriteLine("Welcome!\nPress any key to play.");

        Console.ReadKey(true);
    }

    private int LoadCharacterSelect()
    {
        Console.WriteLine("Choose character type:");
        Console.WriteLine("Options:");

        Console.Write("1) Warrior\n2) Archer\n3) Mage\nYour pick: ");

        bool result = int.TryParse(Console.ReadLine(), out int choice);

        while (result == false || choice < WarriorMenuNumber || choice > MageMenuNumber)
        {
            Console.WriteLine("Invalid command!");
            result = int.TryParse(Console.ReadLine(), out choice);
        }

        return choice;
    }

    private void InitField(char playerSymbol)
    {
        for (int row = 0; row < field.GetLength(0); row++)
        {
            for (int column = 0; column < field.GetLength(1); column++)
            {
                if (row == 0 && column == 0)
                {
                    field[row, column] = playerSymbol;
                    continue;
                }

                field[row, column] = DefaultFieldSymbol;
            }
        }
    }

    private void PrintField()
    {
        for (int row = 0; row < field.GetLength(0); row++)
        {
            for (int column = 0; column < field.GetLength(1); column++)
            {
                if (column == field.GetLength(1) - 1)
                {
                    Console.Write(field[row, column]);
                    break;
                }

                Console.Write($"{field[row, column]} ");
            }

            Console.WriteLine();
        }
    }
}