using BS.ReceiptAnalyzer.Local.Core;
using System.Windows.Forms;

namespace BS.ReceiptAnalyzer.Local.Views
{
    public partial class MainView : Form
    {
        private readonly IAnalysisTaskManager _taskManager;
        private Guid? _taskId;
        private bool _canExecuteNextStep;

        public MainView(IAnalysisTaskManager taskManager)
        {
            InitializeComponent();
            ExecuteNextStepButton.Enabled = false;
            _taskManager = taskManager;
        }

        private async Task ExecuteNextStep()
        {
            HandleStartProcessingStarted();
            var request = new TaskManagerContract.ExecuteNextStep.Request 
            {
                TaskId = _taskId!.Value
            };
            var nextStepResult = await _taskManager.ExecuteNextStep(request);
            
            HandleNextStepFinished(nextStepResult);
        }


        #region Control Event Handlers

        private async void StartTaskButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Plik obrazu (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var request = new TaskManagerContract.CreateTask.Request 
                { 
                    FilePath = dialog.FileName
                };
                var startTask = await _taskManager.CreateTask(request);
                HandleTaskStarted(startTask);
                await ExecuteNextStep();
            }
        }

        #endregion


        #region Analysis Task Results Handlers

        private void HandleTaskStarted(TaskManagerContract.CreateTask.Result result)
        {
            if (!result.Success)
            {
                if (MessageBox.Show(result.Error, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    return;
                }
            }
            _taskId = result.TaskId;

            TaskIdTextBox.Text = _taskId.ToString();
            StartTaskGroupBox.Enabled = false;
            SourceImagePixtureBox.ImageLocation = _taskManager.GetSourceImagePath(_taskId.Value);
            SourceImagePixtureBox.Load();
        }

        private void HandleNextStepFinished(TaskManagerContract.ExecuteNextStep.Result result)
        {
            HandleStepProcessingFinished();
            ProcessingProgressBar.Value = result.CompletionPercentage;
            //TODO: obsługa wyniku
        }

        private void HandleStartProcessingStarted()
        {

        }

        private void HandleStepProcessingFinished()
        {

        }

        #endregion


    }
}
