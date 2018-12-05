using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskList.Data;
using TeamTaskList.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamTaskList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskNewPage : ContentPage
    {
        private TaskSampleData taskSampleDataInstance = TaskSampleData.GetInstance();
        public TaskNewPage ()
		{
			InitializeComponent ();
		}

        async void OnSaveTask(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(taskTitleEntry.Text) ||
                string.IsNullOrWhiteSpace(taskDescriptionEntry.Text) ||
                string.IsNullOrWhiteSpace(taskPriorityEntry.Text))
            {
                await DisplayAlert("Missing Information", "You must fill out all fields.", "OK");
            }
            else if(!int.TryParse(taskPriorityEntry.Text, out int result))
            {
                await DisplayAlert("Wrong value", "Priority needs to be a non-decimal numeric value", "OK");
            }
            else
            {
                if (await DisplayAlert("Save changes?", "Are you sure you want to save these changes?",
                    "Yes", "Cancel") == true)
                {
                    var newTask = new TaskModel()
                    {
                        Id = -1,
                        Title = taskTitleEntry.Text,
                        Description = taskDescriptionEntry.Text,
                        Priority = int.Parse(taskPriorityEntry.Text)
                    };
                    taskSampleDataInstance.AddTask(newTask);
                    MainPage.TaskIsAdded = true;
                    await Navigation.PopAsync();
                }
            }
        }

        async void OnCancel(object sender, EventArgs e)
        {
            if (await DisplayAlert("Discard new task?", "Are you sure you want to discard these changes?",
                    "Yes", "Cancel") == true)
            {
                await Navigation.PopAsync();
            }
        }
    }
}