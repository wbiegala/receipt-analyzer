using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Shared.Hashing;
using BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask;
using MediatR;
using Moq;
using Microsoft.Extensions.Logging;
using BS.ReceiptAnalyzer.Core.Internal.Queries;
using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Core.Commands.UploadSourceImage;
using FluentAssertions;

namespace BS.ReceiptAnalyzer.Core.Tests.Commands
{
    public class CreateAnalysisTaskTests
    {
        public CreateAnalysisTaskTests()
        {
            _repositoryMock.Reset();
            _hashServiceMock.Reset();
            _mediatorMock.Reset();
            _loggerMock.Reset();
        }

        [Fact]
        public async Task Handle_WhenNoHashImageInDb_CreatesNewAnalysisTask()
        {
            _repositoryMock.Setup(rep => rep.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _hashServiceMock.Setup(s => s.GetHash(It.IsAny<byte[]>())).Returns(fakeHash);
            _mediatorMock.Setup(med => med.Send(It.IsAny<GetAnalysisTaskByImageHashQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AnalysisTask?)null);
            _mediatorMock.Setup(med => med.Send(It.IsAny<UploadSourceImageCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new UploadSourceImageCommandResult { Success = true });


            var handler = new CreateAnalysisTaskCommandHandler(_repositoryMock.Object, _hashServiceMock.Object,
                _mediatorMock.Object, new LoggerMock<CreateAnalysisTaskCommandHandler>());
            var command = new CreateAnalysisTaskCommand(false, new MemoryStream(file), "image/jpg");
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.IsNew.Should().BeTrue();
        }


        private readonly Mock<IAnalysisTaskRepository> _repositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IHashService> _hashServiceMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<ILogger<CreateAnalysisTaskCommandHandler>> _loggerMock = new();

        private const string fakeHash = "fake_hash";
        private readonly byte[] file = new byte[] { 1, 2, 3 };
    }
}
