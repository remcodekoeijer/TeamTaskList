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

        public TaskDetailPage(int taskId)
        {
            InitializeComponent();

            oldTask = taskSampleDataInstance.GetTaskModels()[taskId];
            taskIdLabel.Text = "Task ID: " + oldTask.Id.ToString();
            taskTitleEntry.Text = oldTask.Title;
            taskDescriptionEntry.Text = oldTask.Description;
            taskPriorityEntry.Text = oldTask.Priority.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnEditTask(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(taskTitleEntry.Text) ||
                string.IsNullOrWhiteSpace(taskDescriptionEntry.Text) ||
                string.IsNullOrWhiteSpace(taskPriorityEntry.Text))
            {
                await DisplayAlert("Missing Information", "You must fill out all fields.", "OK");
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
    }
}