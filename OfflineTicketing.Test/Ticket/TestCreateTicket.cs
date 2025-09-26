using Moq;
using OfflineTicketing.Application.Features.Ticket.Commands.CreateTicket;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Test.Ticket
{
    public class TestCreateTicket
    {
        private readonly Mock<ITicketRepository> _mockRepository;
        private readonly CreateTicketCommandHandler _handler;
        public TestCreateTicket()
        {
            _mockRepository = new Mock<ITicketRepository>();

            _handler = new CreateTicketCommandHandler(_mockRepository.Object);
        }
        [Fact]
        public async Task CreateTicket_Success()
        {
            var command = new CreateTicketCommand
            {
                Title = "Test Ticket",
                Description = "This is a test ticket",
                Status = Core.Enums.StatusTypeEnum.Open,
                Priority = Core.Enums.PriorityTypeEnum.High,
                CreatedByUserId = Guid.NewGuid(),
                AssignedToUserId = Guid.NewGuid()
            };
            _mockRepository.Setup(repo => repo.CreateTicket(It.IsAny<Core.Entities.Ticket>()))
                           .ReturnsAsync(Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);
            _mockRepository.Verify(repo => repo.CreateTicket(It.IsAny<Core.Entities.Ticket>()), Times.Once);
        }

    }
}
