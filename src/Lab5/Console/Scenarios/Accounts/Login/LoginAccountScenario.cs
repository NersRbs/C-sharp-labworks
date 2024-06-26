﻿using Contracts;
using Contracts.Accounts;
using Spectre.Console;

namespace Console.Scenarios.Accounts.Login;

public class LoginAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    public LoginAccountScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "User";

    public void Run()
    {
        string id = AnsiConsole.Ask<string>("Enter your id:\n");
        string pinCode = AnsiConsole.Ask<string>("Enter your pin code:\n");

        if (long.TryParse(id, out long parsedId) is false)
        {
            AnsiConsole.MarkupLine("[red]Invalid id[/]");
            System.Console.ReadLine();
            return;
        }

        Result result = _accountService.Login(parsedId, pinCode);
        string message = result switch
        {
            Result.Success => "[green]Login successful[/]",
            Result.Failed => "[red]Login failed[/]",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.MarkupLine(message);
        System.Console.ReadLine();
    }
}