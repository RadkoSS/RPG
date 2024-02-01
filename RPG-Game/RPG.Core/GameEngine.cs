namespace RPG.Core;

using Contracts;
using GameModels;
using Data.Contracts;
using GameModels.Enums;

using static Common.GameConstants.MonsterConstants;
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

        BaseGameModel? character;

        int characterChoice = LoadCharacterSelect();

        character = await SetupGame(characterChoice);

        while (command != MenuOptions.Exit)
        {
            switch (command)
            {
                case MenuOptions.MainMenu:
                    LoadMenu();
                    break;

                case MenuOptions.CharacterSelect:

                    characterChoice = LoadCharacterSelect();
                    character = await SetupGame(characterChoice);

                    if (character == null)
                    {
                        continue;
                    }
                    break;

                case MenuOptions.InGame:

                    if (character == null)
                    {
                        Console.WriteLine("Please, choose a character first!");
                        continue;
                    }

                    BeginRpgGame(character);
                    break;

                default:
                    Console.WriteLine("Invalid command!");
                    break;
            }

            command = ReadCommand();
        }

        Console.WriteLine();
        Console.WriteLine("Exiting to desktop...");
    }

    private async Task<BaseGameModel?> SetupGame(int characterChoice)
    {
        BaseGameModel? character = GetChosenCharacter(characterChoice);

        if (character != null)
        {
            AskForStatsBuff(character);
            InitField(character.Symbol);

            await this.statsLogger.LogCharacterChoice(character, characterChoice);

            BeginRpgGame(character);
        }
        else
        {
            Console.WriteLine("Please, choose a valid character!");
        }

        return character;
    }

    private BaseGameModel? GetChosenCharacter(int characterId)
    {
        switch (characterId)
        {
            case WarriorMenuNumber:
                return new Warrior();
            case ArcherMenuNumber:
                return new Archer();
            case MageMenuNumber:
                return new Mage();
            default:
                Console.WriteLine("Invalid character selected!");
                return null;
        }
    }

    private void BeginRpgGame(BaseGameModel character)
    {
        InitField(character.Symbol);

        HashSet<Monster> monsters = new HashSet<Monster>();

        int characterRow = 0;
        int characterColumn = 0;

        while (character.IsAlive)
        {
            Monster monster = new Monster();

            SetRandomCoordinatesForMonster(monster, character.Symbol);

            monsters.Add(monster);

            this.field[monster.Row, monster.Column] = DefaultMonsterSymbol;

            PrintCharacterInfo(character);

            PrintField();

            int action = GetActionChoice();

            switch (action)
            {
                case 1:
                    // ToDo: Implement attack logic...
                    break;

                case 2:
                    char direction = char.ToLower(Console.ReadKey().KeyChar);
                    Console.WriteLine();

                    bool movePerformed = MoveCharacter(direction, ref characterRow, ref characterColumn, character.Symbol);

                    if (!movePerformed)
                    {
                        Console.WriteLine("Invalid move!");
                        continue;
                    }

                    break;
            }

            PrintCharacterInfo(character);

            PrintField();
        }

        Console.WriteLine("Game over!");
    }

    private void PrintCharacterInfo(BaseGameModel character)
        => Console.WriteLine($"Health: {character.Health}   Mana: {character.Mana}");

    private bool IsValidMove(int row, int column)
        => row >= 0 && row < this.field.GetLength(0) && column >= 0 
           && column < this.field.GetLength(1) 
           && this.field[row, column] != DefaultMonsterSymbol;

    private bool MoveCharacter(char direction, ref int characterRow, ref int characterColumn, char characterSymbol)
    {
        switch (direction)
        {
            case 'w':

                if (!IsValidMove(characterRow - 1, characterColumn))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterRow--;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 's':

                if (!IsValidMove(characterRow + 1, characterColumn))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterRow++;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 'd':

                if (!IsValidMove(characterRow, characterColumn + 1))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterColumn++;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 'a':

                if (!IsValidMove(characterRow, characterColumn - 1))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterColumn--;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 'e':

                if (!IsValidMove(characterRow - 1, characterColumn + 1))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterRow--;
                characterColumn++;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 'x':

                if (!IsValidMove(characterRow + 1, characterColumn + 1))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterRow++;
                characterColumn++;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 'q':
                if (!IsValidMove(characterRow - 1, characterColumn - 1))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterRow--;
                characterColumn--;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            case 'z':

                if (!IsValidMove(characterRow + 1, characterColumn - 1))
                {
                    return false;
                }

                this.field[characterRow, characterColumn] = DefaultFieldSymbol;
                characterRow++;
                characterColumn--;
                this.field[characterRow, characterColumn] = characterSymbol;
                return true;

            default:
                return false;
        }
    }

    private void SetRandomCoordinatesForMonster(Monster monster, char playerSymbol)
    {
        Random random = new Random();

        int randomRow;
        int randomColumn;

        do
        {
            randomRow = random.Next(0, field.GetLength(0));
            randomColumn = random.Next(0, field.GetLength(1));
        } while (!AreValidMonsterCoordinates(randomRow, randomColumn, playerSymbol));

        monster.Row = randomRow;
        monster.Column = randomColumn;
    }

    private bool AreValidMonsterCoordinates(int row, int column, char playerSymbol)
    {
        char position = this.field[row, column];

        return position != DefaultMonsterSymbol && position != playerSymbol;
    }

    private int GetActionChoice()
    {
        Console.WriteLine("Choose action");
        Console.WriteLine("1) Attack");
        Console.WriteLine("2) Move");

        int choice;
        do
        {
            bool result = int.TryParse(Console.ReadLine(), out choice);

            if (!result)
            {
                Console.WriteLine("Invalid choice!");
            }

        } while (choice < 1 || choice > 2);

        return choice;
    }

    private void AskForStatsBuff(BaseGameModel character)
    {
        Console.WriteLine($"Would you like to buff up your stats before starting? (Limit: {MaxBuffPoints} points total)");
        Console.WriteLine("Response (Y\\N):");
        char response = char.ToLower(Console.ReadKey().KeyChar);

        if (response == 'y')
        {
            int pointsAdded = 0;

            while (pointsAdded < MaxBuffPoints)
            {
                Console.WriteLine();
                Console.WriteLine("Which one of your character's abilities do you want to buff?");

                Console.WriteLine("1) Add to Strength");
                Console.WriteLine("2) Add to Agility");
                Console.WriteLine("3) Add to Intelligence");

                int choice = int.Parse(Console.ReadLine()!);

                if (choice < 1 || choice > 3)
                {
                    Console.WriteLine("Invalid choice of ability!");
                    continue;
                }

                Console.Write("Enter amount of points to add to the chosen ability: ");

                int amountToAdd = int.Parse(Console.ReadLine()!);

                int difference = MaxBuffPoints - pointsAdded;

                if (amountToAdd <= 0 || amountToAdd > difference)
                {
                    Console.WriteLine($"{amountToAdd} is an invalid number. The maximum you can add is {difference}.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        character.Strength += amountToAdd;
                        break;
                    case 2:
                        character.Agility += amountToAdd;
                        break;
                    case 3:
                        character.Intelligence += amountToAdd;
                        break;
                }

                pointsAdded += amountToAdd;
                difference = MaxBuffPoints - pointsAdded;

                Console.WriteLine($"Remaining Points: {difference}");
            }

            character.Setup();

            Console.WriteLine("Character buffed successfully!");
        }
        else
        {
            Console.WriteLine($"{Environment.NewLine}Character stays unbuffed!");
        }
    }

    private MenuOptions ReadCommand()
    {
        Console.WriteLine();
        Console.WriteLine($"Available options: {Environment.NewLine}{(int)MenuOptions.MainMenu}) MainMenu{Environment.NewLine}{(int)MenuOptions.CharacterSelect}) Character selection{Environment.NewLine}{(int)MenuOptions.InGame}) Start a new game{Environment.NewLine}{(int)MenuOptions.Exit} Exit to desktop.");

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
        Console.WriteLine($"Welcome!{Environment.NewLine}Press any key to play.");

        Console.ReadKey(true);

        Console.WriteLine();
    }

    private int LoadCharacterSelect()
    {
        Console.WriteLine("Choose character type:");
        Console.WriteLine("Options:");

        Console.Write($"1) Warrior{Environment.NewLine}2) Archer{Environment.NewLine}3) Mage{Environment.NewLine}Your pick: ");

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