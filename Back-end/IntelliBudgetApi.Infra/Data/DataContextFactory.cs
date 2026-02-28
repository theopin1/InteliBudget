using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IntelliBudgetApi.Infra.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseMySql(
            "Server=localhost;Port=3307;Database=intellibudget;User=root;Password=123456;AllowPublicKeyRetrieval=true;SslMode=None;",
            new MySqlServerVersion(new Version(8, 4, 4))
        );

        return new DataContext(optionsBuilder.Options);
    }
}