using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using minimalTR_dal;
using minimalTR_dal.Attendee;

namespace minimalTR_api.Tests.Attendee
{
    public class AttendeeTest
    {
        [Theory(DisplayName = "Create Attendee - Age validation")]
        [Trait("Api", "Attendee")]
        [InlineData("Javier", 17)]
        public async Task When_CreateAttendeeIsCaleld_WithAgeLowerThan18_ShouldReturn_BadRequest(string name, int age)
        {
            //Arrange
            await using var application = new TestApi();
            var client = application.CreateClient();

            //Act
            var response = await client.PostAsJsonAsync("/attendee", new { Name = name, Age = age });

            //Assert
            response.Should().Be400BadRequest().And.HaveError("Age", "Attendee should be have at least 18 years old");
        }

        [Theory(DisplayName = "Create Attendee - Save success")]
        [Trait("Api", "Attendee")]
        [InlineData("Javier", 18)]
        public async Task When_CreateAttendeeIsCalled_WithValidData_ShouldReturn_Created(string name, int age)
        {
            //Arrange
            await using var application = new TestApi();
            var client = application.CreateClient();

            //Act
            var response = await client.PostAsJsonAsync("/attendee", new { Name = name, Age = age });

            //Assert
            response.Should().Be201Created();
        }

        [Theory(DisplayName = "Create Attendee - Get by Id")]
        [Trait("Api", "Attendee")]
        [InlineData("Javier", 18)]
        public async Task When_GetByIdIsCalled_WithValidData_ShouldReturn_AnAttendee(string name, int age)
        {
            //Arrange
            await using var application = new TestApi();
            var client = application.CreateClient();
            var testAttendee = new minimalTR_dal.Attendee.AttendeeInformation { Age = age, Name = name };
            AddAttendee(application, testAttendee);

            //Act
            var response = await client.GetAsync($"/attendee/{testAttendee.Id}");

            //Assert
            response.Should().Be200Ok().And.BeAs(testAttendee);
        }

        private static void AddAttendee(TestApi application, AttendeeInformation testAttendee)
        {
            using var scope = application.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MinimaltrDB>();
            db.Attendees.Add(testAttendee);
            db.SaveChanges();
        }
    }
}
