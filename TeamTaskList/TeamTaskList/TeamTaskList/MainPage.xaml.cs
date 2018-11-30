using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskList.Data;
using TeamTaskList.Models;
using Xamarin.Forms;

namespace TeamTaskList
{
    public partial class MainPage : ContentPage
    {
        private int maxColumns = 2;
        private List<TaskModel> taskModels = TaskSampleData.GetTaskModels();

        public MainPage()
        {
            BindingContext = taskModels;
            InitializeComponent();

            int numberOfrows = (taskModels.Count / maxColumns) + 1;

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
            for (int i = 0; i < taskModels.Count; i++)
            {
                //gridTasks.Children.Add(new Label
                //{
                //    Text = taskModels[i].Title + "\n" + taskModels[i].Description,
                //    HorizontalOptions = LayoutOptions.Center,

                //}, i % maxColumns, i / maxColumns);
                Button btn = new Button()
                {
                    Text = taskModels[i].Title + "\n" + taskModels[i].Description,
                    BackgroundColor = Color.FromHex("#AA4139"),
                    Margin = 10,
                    HeightRequest = 150
                };
                btn.ClassId = "Task" + taskModels[i].Id;
                gridTasks.Children.Add(btn, i % maxColumns, i / maxColumns);

                btn.Clicked += delegate (object sender, EventArgs e)
                {
                    Button sendBtn = (Button)sender;
                    ChangeHeader(sendBtn.ClassId);
                };
            }
        }

        private void ChangeHeader(string text)
        {
            header.Text = text;
        }
    }
}
