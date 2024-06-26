﻿using Contracts;
using Contracts.Accounts;
using Spectre.Console;

namespace Console.Scenarios.Accounts.MonetaryTransaction;

public class MonetaryTransactionScenario : IScenario
{
    private readonly IAccountService _accountService;

    public MonetaryTransactionScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Monetary Transaction";

    public void Run()
    {
        string amount = AnsiConsole.Ask<string>("Enter amount:");
        if (long.TryParse(amount, out long parsedAmount) is false)
        {
            AnsiConsole.MarkupLine("[red]Invalid amount[/]");
            System.Console.ReadLine();
            return;
        }

        Result result = _accountService.AddMonetaryTransaction(parsedAmount).Result;
        string message = result switch
        {
            Result.Success => "[green]Transaction successful[/]",
            Result.Failed => "[red]Transaction failed[/]",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };
        AnsiConsole.MarkupLine(message);
        System.Console.ReadLine();
    }
}