using System;
using System.Collections.Generic;
using System.Text;
using TeamTaskList.Models;

namespace TeamTaskList.Data
{
    public class TaskSampleData
    {
        static List<TaskModel> taskList = new List<TaskModel>();
        static int numberOfTasks = 100;

        private static void PopulateList()
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

        public static List<TaskModel> GetTaskModels()
        {
            PopulateList();
            return taskList;
        }
    }
}
