﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamTaskList.Models;

namespace TeamTaskList.Data
{
    public class TaskSampleData
    {
        private static TaskSampleData instance = new TaskSampleData();
        private List<TaskModel> taskList;
        private int numberOfTasks = 30;

        private TaskSampleData()
        {
            taskList = new List<TaskModel>();
            PopulateList();
        }

        private void PopulateList()
        {
            Random rand = new Random();
            for (int i = 0; i < numberOfTasks; i++)
            {
                TaskModel task = new TaskModel()
                {
                    Id = i,
                    Title = "Task " + i,
                    Description = "This is some sort of task. This describes what you have to do.",
                    Priority = rand.Next(1, 5)                    
                };
                taskList.Add(task);
            }
        }

        public static TaskSampleData GetInstance()
        {
            return instance;
        }

        public List<TaskModel> GetTaskModels()
        {
            return taskList;
        }
        public void UpdateTask(TaskModel update)
        {
            foreach(var task in taskList)
            {
                if(update.Id == task.Id)
                {
                    task.Title = update.Title;
                    task.Description = update.Description;
                    task.Priority = update.Priority;
                }
            }
        }
    }
}
