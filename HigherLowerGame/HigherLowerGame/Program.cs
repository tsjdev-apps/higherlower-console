using Spectre.Console;

// game variables
var isGameRunning = true;


while (isGameRunning)
{
    // clear the console
    AnsiConsole.Clear();

    // create the header
    CreateHeader();

    // create the number to guess
    int numberToGuess = new Random().Next(1, 101);

    // get the player selection
    int guess = GetPlayerGuess();

    // set counter
    var counter = 1;

    // game logic
    while (guess != numberToGuess)
    {
        if (guess > numberToGuess)
        {
            AnsiConsole.MarkupLine("The number is [yellow]lower[/] than your guess.");
        }
        else
        {
            AnsiConsole.MarkupLine("The number is [yellow]higher[/] than your guess.");
        }

        counter++;
        guess = GetPlayerGuess();
    }

    // game end
    AnsiConsole.MarkupLine($"\nCongratulations! You matched my number in [yellow]{counter}[/] steps!\n");

    isGameRunning = AnsiConsole.Confirm("Do you want to play again?", true);
}

static void CreateHeader()
{
    // Create a grid for the header text
    Grid grid = new();
    grid.AddColumn();
    grid.AddRow(new FigletText("HigherLowerGame").Centered().Color(Color.Red));
    grid.AddRow(Align.Center(new Panel("[red]Sample by Thomas Sebastian Jensen ([link]https://www.tsjdev-apps.de[/])[/]")));

    // Write the grid to the console
    AnsiConsole.Write(grid);
    AnsiConsole.WriteLine();
}

static int GetPlayerGuess()
{
    // get the player guess
    return AnsiConsole.Prompt(
        new TextPrompt<int>("Please enter a number between [green]1[/] and [green]100[/]:")
            .PromptStyle("white")
            .ValidationErrorMessage("[red]That's not a valid number[/]")
            .Validate(number =>
            {
                return number switch
                {
                    < 1 => ValidationResult.Error("[red]Please select a higher number.[/]"),
                    > 100 => ValidationResult.Error("[red]Please select a lower number[/]"),
                    _ => ValidationResult.Success(),
                };
            }));
}