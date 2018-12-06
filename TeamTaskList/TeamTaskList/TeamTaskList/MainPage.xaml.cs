﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static bool TaskIsEdited { get; set; }
        public static bool TaskIsAdded { get; set; }
        public static bool Sorted { get; set; }
        private int lastTaskIdSent = -1;

        public MainPage()
        {
            taskModels = taskSampleDataInstance.GetTaskModels();
            //BindingContext = taskModels;
            InitializeComponent();
            
            RefreshAllTasks();
        }

        public void ChangeHeader(string newText)
        {
            header.Text = newText;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (TaskIsAdded)
            {
                RefreshAddedTask();
                TaskIsAdded = false;
            }
            if (TaskIsEdited)
            {
                RefreshEditedTask();
                TaskIsEdited = false;
            }
            if (Sorted)
            {
                RefreshAllTasks();
                Sorted = false;
            }
        }

        private void RefreshAllTasks()
        {
            gridTasks.Children.Clear();
            PopulateGrid();
        }

        private void RefreshAddedTask()
        {
            ToAddRow();
            AddTaskToGrid(taskModels.Count - 1);
        }

        private void RefreshEditedTask()
        {
            //taskId is the same value as the classId of a taskNoteControl.
            foreach(var task in taskModels)
            {
                if (lastTaskIdSent == task.Id)
                {
                    foreach (TaskNoteControl taskNote in gridTasks.Children)
                    {
                        if (lastTaskIdSent == int.Parse(taskNote.ClassId))
                        {
                            taskNote.TaskTitle = task.Title;
                            taskNote.TaskDescription = task.Description;
                            taskNote.TaskPriority = task.Priority.ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void PopulateGrid()
        {
            ColumnRowDefinition();

            for (int i = 0; i < taskModels.Count; i++)
            {
                AddTaskToGrid(i);
            }
        }

        private void ColumnRowDefinition()
        {
            int numberOfrows;
            int numberOfTasks = taskModels.Count;

            if (numberOfTasks % 2 != 0)
                numberOfrows = (numberOfTasks / maxColumns) + 1;
            else
                numberOfrows = numberOfTasks / maxColumns;

            gridTasks.RowDefinitions.Clear();
            gridTasks.ColumnDefinitions.Clear();

            for (int i = 0; i < numberOfrows; i++)
            {
                gridTasks.RowDefinitions.Add(new RowDefinition()
                {
                    Height = GridLength.Auto
                });
            }
            for (int i = 0; i < maxColumns; i++)
            {
                gridTasks.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = GridLength.Star
                });
            }
        }

        private void ToAddRow()
        {
            int numberOfrows;
            int numberOfTasks = taskModels.Count;

            if (numberOfTasks % 2 != 0)
                numberOfrows = (numberOfTasks / maxColumns) + 1;
            else
                numberOfrows = numberOfTasks / maxColumns;

            if(numberOfrows > gridTasks.RowDefinitions.Count)
            {
                gridTasks.RowDefinitions.Add(new RowDefinition()
                {
                    Height = GridLength.Auto
                });
            }
        }

        private void AddTaskToGrid(int taskListIndex)
        {
            //AT THE MOMENT
            //This works because the task is added to the end of the list. Therefore it's index is easy to find.
            //If in the future sorting is done, this might change. 

            var taskControl = new TaskNoteControl()
            {
                TaskTitle = taskModels[taskListIndex].Title,
                TaskDescription = taskModels[taskListIndex].Description,
                TaskPriority = taskModels[taskListIndex].Priority.ToString(),
                Margin = 10,
            };
            taskControl.ClassId = taskModels[taskListIndex].Id.ToString();
            gridTasks.Children.Add(taskControl, taskListIndex % maxColumns, taskListIndex / maxColumns); //Add(object, columnIndex (0 is first column), rowIndex (0 is first row)
            taskControl.ClickedTask += OnTaskClicked;
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            RefreshAllTasks();
        }

        private async void OnAddTask(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskNewPage());
        }

        private async void OnTaskClicked(object sender, EventArgs e)
        {
            TaskNoteControl se = (TaskNoteControl)sender;
            //TODO; make a check to see if it is a number
            int taskId = int.Parse(se.ClassId);
            lastTaskIdSent = taskId;
            await Navigation.PushAsync(new TaskDetailPage(taskId));
        }

        private async void OnSort(object sender, EventArgs e)
        {
            //For more information: https://www.youtube.com/watch?v=dOU0Qei3Qlk 
            //And https://github.com/HoussemDellai/Xamarin-Forms-Popup-Demo
            await Navigation.PushModalAsync(new NavigationPage(new SortPupUp()));
        }
    }
}
