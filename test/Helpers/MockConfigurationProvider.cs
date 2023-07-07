using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Language.Flow;

namespace DbConnectionFactoryTests.Helpers
{
    public static class MockConfigurationProvider
    {
        public static IReturnsResult<IConfigurationSection> ValidSection<T>(this Mock<IConfigurationSection> MockConfigurationSection, string ConnectionString)
        {
            return MockConfigurationSection
                .SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")])
                .Returns(ConnectionString);
        }

        public static IReturnsResult<IConfiguration> ValidConfiguration<T>(this Mock<IConfiguration> MockConfiguration, Mock<IConfigurationSection> MockConfigurationSection)
        {
            return MockConfiguration
                .Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                .Returns(MockConfigurationSection.Object);
        }

        public static IReturnsResult<IConfigurationSection> InValidSection<T>(this Mock<IConfigurationSection> MockConfigurationSection, string InvalidConnectionString)
        {
            return MockConfigurationSection
                .SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")])
                .Returns(InvalidConnectionString);
        }
    }
}
