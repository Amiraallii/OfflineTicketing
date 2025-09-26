using Moq;
using OfflineTicketing.Application.Features.Ticket.Commands.UpdateTicket;
using OfflineTicketing.Core.Enums;
using OfflineTicketing.Infrastructure.IRepositories.IRepositories;

namespace OfflineTicketing.Test.Ticket
{
    public class TestUpdateTicket
    {
        private readonly Mock<ITicketRepository> _mockRepository;   
        private readonly UpdateTicketCommandHandler _handler;
        public TestUpdateTicket()
        {
            _mockRepository = new Mock<ITicketRepository>();
            _handler = new UpdateTicketCommandHandler(_mockRepository.Object);
        }
        [Fact]
        public async Task UpdateTicket_Success()
        {
            var creatorId = Guid.NewGuid();
            var assigneeId = Guid.NewGuid();
            var existing = Core.Entities.Ticket.Create(
                title: "Old title",
                description: "Old desc",
                status: StatusTypeEnum.Open,
                priority: PriorityTypeEnum.Medium,
                createdByUserId: creatorId,
                assignedToUserId: assigneeId
            );
            var id = existing.Id;

            var cmd = new UpdateTicketCommand
            {
                Id = id,
                Title = "New title",
                Description = "New desc",
                Status = StatusTypeEnum.InProgress,
                Priority = PriorityTypeEnum.High
            };
            _mockRepository.Setup(r => r.GetTicketById(id, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(existing);

            _mockRepository.Setup(r => r.UpdateTicket(existing))
                            .Returns(Task.CompletedTask);

            await _handler.Handle(cmd, CancellationToken.None);

            Assert.Equal("New title", existing.Title);
            Assert.Equal("New desc", existing.Description);
            Assert.Equal(StatusTypeEnum.InProgress, existing.Status);
            Assert.Equal(PriorityTypeEnum.High, existing.Priority);


            _mockRepository.Verify(r => r.GetTicketById(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepository.Verify(r => r.UpdateTicket(existing), Times.Once);
            _mockRepository.VerifyNoOtherCalls();
        }
    }
}
