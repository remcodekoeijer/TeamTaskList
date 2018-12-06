using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamTaskList.Models;

namespace TeamTaskList.Data
{
    public class TaskSampleData
    {
        private static TaskSampleData instance = new TaskSampleData();
        private List<TaskModel> taskList;
        private int numberOfTasks = 30;
        private int id = 0;

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
                TaskModel task;
                if(i % 3 == 0)
                {
                    task = new TaskModel()
                    {
                        Id = -1,
                        Title = "Task with a longer name " + i,
                        Description = "This is some sort of task. This describes what you have to do.",
                        Priority = rand.Next(1, 5)
                    };
                }
                else
                {
                    task = new TaskModel()
                    {
                        Id = -1,
                        Title = "Task " + i,
                        Description = "This is some sort of task. This describes what you have to do. As a follow up, this is just some added text to see what happens with very long descriptions. Will it stretch up the control? That is the question",
                        Priority = rand.Next(1, 5)
                    };
                }
                AddTask(task);
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

        public bool GetTaskModelById(int id, out TaskModel taskOut)
        {            
            foreach(var task in taskList)
            {
                if (task.Id == id)
                {
                    taskOut = task;
                    return true;
                }
            }
            taskOut = new TaskModel();
            return false;
        }

        public void AddTask(TaskModel newTask)
        {
            id++;
            newTask.Id = id;
            taskList.Add(newTask);
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

        public void SortTitleAsc()
        {
            taskList.Sort((x, y) => x.Title.CompareTo(y.Title));
        }
        public void SortTitleDesc()
        {
            taskList.Sort((x, y) => y.Title.CompareTo(x.Title));
        }

        public void SortPriorityAsc()
        {
            taskList.Sort((x,y) => x.Priority.CompareTo(y.Priority));
        }
        public void SortPriorityDesc()
        {
            taskList.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }
    }
}
