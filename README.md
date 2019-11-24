# dummy-net-core

A dummy implementation to catch up with .NET Core.

## It contains:

- An API written in .NET Core 3.0 , using EF Core.
- An Angular client.

## Requires:

- You 'd need to setup an Auth0 demo account to support Authentication/Authorization.
- A Google Geolocation API Key (not mandatory)

## Comments

Quite an enjoyable experience since last time ( .NET 4.5.1 version).
As for EntityFramework the biggest news is that they somehow managed to "break" the way to handle many-to-many relationships under the hood, while moving from EF to EF Core version ([Issue Discussion here](https://github.com/aspnet/EntityFrameworkCore/issues/10508)). Doesn't matter really, I find it to be a feature.
EF is still great for code-first modeling with Fluent API and Migrations and of course, LINQ to SQL is always a pleasure to use. Other than that, I still hide it behind "Repositories" and rather use it as a connection provider than a UoW.
