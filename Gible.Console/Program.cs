// See https://aka.ms/new-console-template for more information
using Gible.Console.Commands;
using Gible.Domain.Commands;
using Gible.Domain.Models;
using Gible.Domain.Queries;
using Gible.Domain.Repositories;
using Gible.Domain.Settings;
using Gible.Tech.Mongo;
using Knox.ConsoleCommanding;
using Knox.Extensions;

var running = true;

var clientHandler = new MongoClientHandler();
var recipeRepository = new Repository<Recipe>(clientHandler);
var userRepository = new Repository<User>(clientHandler);
var applicationSettings = new ApplicationSettings();
var searchTagsQueryHandler = new GetRecipesWithTagsQueryHandler(recipeRepository);

var commands = new List<IConsoleCommand>()
{
    new InitializeRecipesConsoleCommand(new InitializeRecipesCommandHandler(recipeRepository), applicationSettings),
    new UpdateRecipeTagConsoleCommand(new UpdateRecipeTagCommandHandler(recipeRepository, userRepository), recipeRepository, userRepository),
    new RegisterUserConsoleCommand(userRepository),
    new SearchByTagsConsoleCommand(searchTagsQueryHandler)
};

while (running)
{
    foreach (var command in commands)
    {
        WriteLine(command.CommandDocumentation);
    }

    Write("\n] ");
    var inputRaw = Console.ReadLine().Wrap().UnwrapOrExchange(string.Empty)!;
    var input = inputRaw.Split(" ");
    var first = input.First().Wrap().UnwrapOrExchange(string.Empty)!;

    switch (first)
    {
        case "exit":
            running = false;
            break;

        default:
            try
            {
                var success = false;
                foreach (var command in commands)
                {
                    if (first == command.CommandName)
                    {
                        var context = new ConsoleCommandContext(input);
                        await command.ExecuteAsync(context);

                        WriteSuccess($"{command.SuccessMessage(context)}");

                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    WriteError($"Could not find command matching pattern {inputRaw}");
                }
            }
            catch (Exception ex)
            {
                WriteError($"Could not execute command matching pattern \"{inputRaw}\"");
                WriteError(ex.Message);
            }
            finally
            {
                WriteLine(string.Empty);
            }
            break;
    }

    static void WriteError(string error) => WriteLine(error, ConsoleColor.Red);
    static void WriteSuccess(string success) => WriteLine(success, ConsoleColor.DarkGreen);

    static void WriteLine(string line = "", ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(line);
        Console.ResetColor();
    }

    static void Write(string line, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.Write(line);
        Console.ResetColor();
    }
}