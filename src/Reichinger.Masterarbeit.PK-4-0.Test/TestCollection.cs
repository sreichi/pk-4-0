using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    [CollectionDefinition("Test collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}