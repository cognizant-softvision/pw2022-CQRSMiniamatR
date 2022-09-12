using FluentAssertions;

namespace minimalTR_api.Tests.Ping;

public class PingTests
{
    [Theory(DisplayName = "Test basic ping")]
    [Trait("Api", "Ping")]
    [InlineData("Test")]
    [InlineData("Welcome to the tests")]
    public async Task When_PingIsCalled_ShouldReturn_Ok(string testMessage)
    {
        //Arrange
        await using var application = new TodoApplication();
        var client = application.CreateClient();

        //Act
        var pingResponse = await client.GetAsync($"/ping?message={testMessage}");

        //Assert
        pingResponse.Should().Be200Ok();
        (await pingResponse.Content.ReadAsStringAsync()).Should().Be($"Pong! Your message was {testMessage}");
    }
}
