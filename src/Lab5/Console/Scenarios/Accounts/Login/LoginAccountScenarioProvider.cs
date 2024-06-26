﻿using System.Diagnostics.CodeAnalysis;
using Contracts.Accounts;
using Contracts.Admins;

namespace Console.Scenarios.Accounts.Login;

public class LoginAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ICurrentAdminService _currentAdminService;

    public LoginAccountScenarioProvider(
        IAccountService accountService,
        ICurrentAccountService currentAccountService,
        ICurrentAdminService currentAdminService)
    {
        _accountService = accountService;
        _currentAccountService = currentAccountService;
        _currentAdminService = currentAdminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is not null ||
            _currentAdminService.AuthorizationStatus is AuthorizationStatus.Success)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAccountScenario(_accountService);
        return true;
    }
}