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

                for(int i = 0; i < taskPriorityPicker.Items.Count; i++)
                {
                    //Set the picker to the value of the taskId priority
                    if (int.Parse(taskPriorityPicker.Items[i]) == oldTask.Priority)
                    {
                        taskPriorityPicker.SelectedIndex = i;
                    }
                }
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
                taskPriorityPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Missing Information", "You must fill out all fields.", "OK");
            }
            else if (!int.TryParse(taskPriorityPicker.Items[taskPriorityPicker.SelectedIndex], out int result))
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
                        Priority = int.Parse(taskPriorityPicker.Items[taskPriorityPicker.SelectedIndex])
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
            for (int i = 0; i < taskPriorityPicker.Items.Count; i++)
            {
                //Set the picker to the value of the taskId priority
                if (int.Parse(taskPriorityPicker.Items[i]) == oldTask.Priority)
                {
                    taskPriorityPicker.SelectedIndex = i;
                }
            }
        }

        private async void TaskNotFound()
        {
            await DisplayAlert("Task Not Found", "The requested task is not found.", "OK");
            await Navigation.PopAsync();
        }
    }
}