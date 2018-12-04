using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskList.Controls;
using TeamTaskList.Data;
using TeamTaskList.Models;
using Xamarin.Forms;

namespace TeamTaskList
{
    public partial class MainPage : ContentPage
    {
        private int maxColumns = 2;

        private TaskSampleData taskSampleDataInstance = TaskSampleData.GetInstance();
        private List<TaskModel> taskModels;
        private int numberOfTasks;

        public MainPage()
        {
            taskModels = taskSampleDataInstance.GetTaskModels();
            BindingContext = taskModels;
            InitializeComponent();

            numberOfTasks = taskModels.Count;

            int numberOfrows;
            if (numberOfTasks % 2 != 0)
                numberOfrows = (numberOfTasks / maxColumns) + 1;
            else
                numberOfrows = numberOfTasks / maxColumns;

            for (int i = 0; i < numberOfrows; i++)
            {
                gridTasks.RowDefinitions.Add(new RowDefinition()
                {
                    Height = GridLength.Star
                });
            }
            for (int i = 0; i < maxColumns; i++)
            {
                gridTasks.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = GridLength.Star
                });
            }

            PopulateGrid();
        }

        private void PopulateGrid()
        {
            for (int i = 0; i < numberOfTasks; i++)
            {
                TaskNoteControl taskControl = new TaskNoteControl()
                {
                    TaskTitle = taskModels[i].Title,
                    TaskDescription = taskModels[i].Description,
                    Margin = 10,
                };
                taskControl.ClassId = "Task-" + taskModels[i].Id;
                gridTasks.Children.Add(taskControl, i % maxColumns, i / maxColumns);
                taskControl.ClickedTask += OnClickedCustom;

                //Button btn = new Button()
                //{
                //    Text = taskModels[i].Title + "\n" + taskModels[i].Description,
                //    BackgroundColor = Color.FromHex("#AA4139"),
                //    Margin = 10,
                //    HeightRequest = 150
                //};
                //btn.ClassId = "Task-" + taskModels[i].Id;
                //gridTasks.Children.Add(btn, i % maxColumns, i / maxColumns);

                //btn.Clicked += OnClicked;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshTasks();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            RefreshTasks();
        }
        
        private void RefreshTasks()
        {
            //TODO 
            //Instead of clearing list and repopulating, find the button that was changed and update that one. If possible. (somehow save the (class)id of the button and reuse here?)
            gridTasks.Children.Clear();
            PopulateGrid();
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            Button sendBtn = (Button)sender;
            //TODO; make a check to se eif it is a number
            int taskId = int.Parse(sendBtn.ClassId.Split('-')[1]);
            await Navigation.PushAsync(new TaskDetailPage(taskId));
        }
        private async void OnClickedCustom(object s, EventArgs e)
        {
            TaskNoteControl sender = (TaskNoteControl)s;
            //TODO; make a check to se eif it is a number
            int taskId = int.Parse(sender.ClassId.Split('-')[1]);
            await Navigation.PushAsync(new TaskDetailPage(taskId));
        }
    }
}
