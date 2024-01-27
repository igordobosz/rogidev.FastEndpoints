using System.Reflection;
using DbUp;
using rogidev.Rotator.Persistence.Scripts;

namespace rogidev.Rotator.Persistence.Migration;

public static class MigrateDatabase
{
    public static void Migrate(string connectionString)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);
        var upgradeEngine = DeployChanges.To
            .SqlDatabase(connectionString, "dbo")
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith(typeof(IScriptsAssembly).Namespace ?? throw new InvalidOperationException()))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();

        if (!upgradeEngine.IsUpgradeRequired())
        {
            return;
        }

        var result = upgradeEngine.PerformUpgrade();
        if (!result.Successful)
        {
            throw new InvalidOperationException("Upgrade failed");
        }
    }
}