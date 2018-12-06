using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TeamTaskList.Data;
using TeamTaskList.Models;

namespace TeamTaskList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailPage : ContentPage
    {
        private TaskModel oldTask;
        private TaskModel editedTask;
        private TaskSampleData taskSampleDataInstance = TaskSampleData.GetInstance();
        private int taskId;

        public TaskDetailPage(int taskId)
        {
            InitializeComponent();
            this.taskId = taskId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (taskSampleDataInstance.GetTaskModelById(taskId, out oldTask))
            {
                taskIdLabel.Text = "Task ID: " + oldTask.Id.ToString();
                taskTitleEntry.Text = oldTask.Title;
                taskDescriptionEntry.Text = oldTask.Description;
                taskPriorityEntry.Text = oldTask.Priority.ToString();
            }
            else
            {
                TaskNotFound();
            }
        }

        async void OnSaveTask(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(taskTitleEntry.Text) ||
                string.IsNullOrWhiteSpace(taskDescriptionEntry.Text) ||
                string.IsNullOrWhiteSpace(taskPriorityEntry.Text))
            {
                await DisplayAlert("Missing Information", "You must fill out all fields.", "OK");
            }
            else if (!int.TryParse(taskPriorityEntry.Text, out int result))
            {
                await DisplayAlert("Wrong value", "Priority needs to be a non-decimal numeric value", "OK");
            }
            else
            {
                if(await DisplayAlert("Save changes?", "Are you sure you want to save these changes?", 
                    "Yes", "Cancel") == true)
                {
                    editedTask = new TaskModel()
                    {
                        Id = oldTask.Id,
                        Title = taskTitleEntry.Text,
                        Description = taskDescriptionEntry.Text,
                        Priority = int.Parse(taskPriorityEntry.Text)
                    };
                    taskSampleDataInstance.UpdateTask(editedTask);
                    MainPage.TaskIsEdited = true;
                    await Navigation.PopAsync();
                }
            }
        }

        void OnRevertTask(object sender, EventArgs e)
        {
            taskTitleEntry.Text = oldTask.Title;
            taskDescriptionEntry.Text = oldTask.Description;
            taskPriorityEntry.Text = oldTask.Priority.ToString();
        }

        private async void TaskNotFound()
        {
            await DisplayAlert("Task Not Found", "The requested task is not found.", "OK");
            await Navigation.PopAsync();
        }
    }
}