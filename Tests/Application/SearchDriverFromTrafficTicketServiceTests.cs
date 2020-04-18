using FluentAssertions;
using LanguageExt;
using Moq;
using NUnit.Framework;

namespace Tests.Application
{
    [TestFixture]
    public sealed class SearchDriverFromTrafficTicketServiceTests
    {
        private Mock<Examples.Application.TrafficTicketRepository> trafficTicketRepository;
        private Mock<Examples.Application.DriverRepository> driverRepository;
        private Examples.Application.SearchDriverFromTrafficTicketService service;

        [SetUp]
        public void SetUp() 
        {
            trafficTicketRepository = new Mock<Examples.Application.TrafficTicketRepository>();
            driverRepository = new Mock<Examples.Application.DriverRepository>();
            service = new Examples.Application.SearchDriverFromTrafficTicketService(
                trafficTicket: trafficTicketRepository.Object,
                driverRepository: driverRepository.Object);
        }

        [Test]
        public void GetsDriveFullNameUnSafe() 
        {
            int trafficTicketId = 1;
            var trafficTicket = new Examples.Application.TrafficTicket(
                driverPersonalIdentificationCode: "422345456P");
            trafficTicketRepository
                .Setup(x => x.UnsafeSearchBy(trafficTicketId))
                .Returns(trafficTicket);
            var driver = new Examples.Application.Driver(
                firstName: "Alvaro",
                lastName: "Gonzalez");
            driverRepository
                .Setup(x => x.UnsafeSearchBy(trafficTicket.DriverPersonalIdentificationCode))
                .Returns(driver);

            var driverFullName = service.UnsafeGetFullName(trafficTicketId);

            driverFullName.Should().Be($"{driver.FirstName} {driver.LastName}");
        }

        [Test]
        public void GetsDriveFullNameSafe()
        {
            int trafficTicketId = 1;
            var trafficTicket = new Examples.Application.TrafficTicket(
                driverPersonalIdentificationCode: "422345456P");
            trafficTicketRepository
                .Setup(x => x.SafeSearchBy(trafficTicketId))
                .Returns(trafficTicket);
            var driver = new Examples.Application.Driver(
                firstName: "Alvaro",
                lastName: "Gonzalez");
            driverRepository
                .Setup(x => x.SafeSearchy(trafficTicket.DriverPersonalIdentificationCode))
                .Returns(driver);

            var driverFullName = service.SafeGetFullName(trafficTicketId);

            driverFullName.ShouldBeSomeWithValue($"{driver.FirstName} {driver.LastName}");
        }
    }
}

public static class TestUtils 
{
    public static void ShouldBeSomeWithValue<T>(
        this Option<T> optionalType, 
        T expectedValue) 
    {
        optionalType.IsSome.Should().BeTrue();
        optionalType.IfSome(fullName => fullName.Should().Be(expectedValue));
    }
}