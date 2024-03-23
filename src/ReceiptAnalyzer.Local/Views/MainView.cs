using BS.ReceiptAnalyzer.Local.Core;
using System.Windows.Forms;

namespace BS.ReceiptAnalyzer.Local.Views
{
    public partial class MainView : Form
    {
        private readonly IAnalysisTaskManager _taskManager;
        private Guid? _taskId;

        public MainView(IAnalysisTaskManager taskManager)
        {
            InitializeComponent();
            _taskManager = taskManager;
        }

        private async void StartTaskButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Plik obrazu (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var request = new TaskManagerContract.CreateTaskRequest { FilePath = dialog.FileName };
                var startTask = await _taskManager.CreateTask(request);
                HandleTaskStarted(startTask);
            }
        }

        private void HandleTaskStarted(TaskManagerContract.CreateTaskResult result)
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
    }
}
