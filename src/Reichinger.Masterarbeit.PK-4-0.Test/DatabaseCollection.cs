using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}