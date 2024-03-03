using BS.ReceiptAnalyzer.Domain.Events;
using BS.ReceiptAnalyzer.Domain.Exceptions;
using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Domain.Model.ReceiptAnalyzed;
using FluentAssertions;

namespace BS.ReceiptAnalyzer.Domain.Tests.Aggregate
{
    public class AnalysisTaskTests
    {
        [Fact]
        public void Create_NoArgs_DomainEventOccured() 
        {
            var task = AnalysisTask.Create();

            task.Should().NotBeNull();
            task.Should().BeAssignableTo<AnalysisTask>();
            task.CreationTime.Should().NotBe(default);
            task.Status.Should().Be(AnalysisTaskStatus.Pending);
            task.Progression.Should().Be(AnalysisTaskProgression.NotStarted);
            task.DomainEvents.Single().Should().BeAssignableTo<AnalysisTaskCreated>();
        }

        [Fact]
        public void Start_IfTaskIsPending_DomainEventOccured()
        {
            var task = GetNewTask();

            task.Start("testsha");

            task.Status.Should().Be(AnalysisTaskStatus.OnProcessing);
            task.Progression.Should().Be(AnalysisTaskProgression.Scheduled);
            task.DomainEvents.Single().Should().BeAssignableTo<AnalysisTaskStarted>();
        }

        [Fact]
        public void Start_IfTaskIsAreadyOnProcessing_ThrowsInvalidStateException()
        {
            var task = GetStartedTask();

            Thread.Sleep(3);

            Assert.Throws<InvalidStateException>(() => task.Start("testsha"));
        }

        [Fact]
        public void Cancel_IfTaskInOnProcessing_DomainEventOccured()
        {
            var task = GetStartedTask();

            task.Cancel();

            task.Status.Should().Be(AnalysisTaskStatus.Canceled);
            task.DomainEvents.Single().Should().BeAssignableTo<AnalysisTaskCanceled>();
        }

        [Fact]
        public void Cancel_IfTaskIsAlreadyCanceled_InvalidStateException()
        {
            var task = GetStartedTask();
            task.Cancel();
            task.ClearEvents();

            Assert.Throws<InvalidStateException>(task.Cancel);       
        }

        [Fact]
        public void NotifyStepFinished_WhenStepSuccessed_AnalysisTaskProgressionNotifiedOccured()
        {
            var task = GetStartedTask();

            var notification = AnalysisTaskStep.Create(
                AnalysisTaskProgression.ReceiptsRecognition,
                true,
                GetCurrentTime(),
                new DateTimeOffset(2024, 3, 1, 12, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2024, 3, 1, 12, 3, 30, TimeSpan.Zero));
            task.NotifyStepFinished(notification);

            task.Status.Should().Be(AnalysisTaskStatus.OnProcessing);
            task.Progression.Should().Be(AnalysisTaskProgression.ReceiptsRecognition);
            task.DomainEvents.Single().Should().BeAssignableTo<AnalysisTaskProgressionNotified>();
        }

        [Fact]
        public void Success_ForCorrectData_AnalysisTaskFinishedOccured()
        {
            var task = GetStartedTask();

            task.Success(new List<Receipt> { new Receipt() });

            task.Status.Should().Be(AnalysisTaskStatus.Finished);
            task.Progression.Should().Be(AnalysisTaskProgression.Finished);
            task.DomainEvents.Single().Should().BeAssignableTo<AnalysisTaskFinished>();
        }

        [Fact]
        public void FailTask_WithReason_AnalysisTaskFailedOccured()
        {
            var task = GetStartedTask();

            task.Fail("Error occured");

            task.Status.Should().Be(AnalysisTaskStatus.Failed);
            task.DomainEvents.Single().Should().BeAssignableTo<AnalysisTaskFailed>();
        }

        private DateTimeOffset GetCurrentTime() => DateTimeOffset.Now;

        private AnalysisTask GetNewTask()
        {
            var task = AnalysisTask.Create();
            task.ClearEvents();

            return task;
        }

        private AnalysisTask GetStartedTask()
        {
            var task = AnalysisTask.Create();
            task.Start("testsha");
            task.ClearEvents();

            return task;
        }
    }
}
