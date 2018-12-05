using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamTaskList.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskNoteControl : ContentView
    {
        public static readonly BindableProperty TaskTitleProperty =
            BindableProperty.Create("TaskTitle", typeof(string), typeof(TaskNoteControl), default(string));

        public string TaskTitle
        {
            get { return (string)GetValue(TaskTitleProperty); }
            set { SetValue(TaskTitleProperty, value); }
        }

        public static readonly BindableProperty TaskDescriptionProperty =
            BindableProperty.Create("TaskDescription", typeof(string), typeof(TaskNoteControl), default(string));

        public string TaskDescription
        {
            get { return (string)GetValue(TaskDescriptionProperty); }
            set { SetValue(TaskDescriptionProperty, value); }
        }

        public static readonly BindableProperty TaskPriorityProperty =
            BindableProperty.Create("TaskPriority", typeof(string), typeof(TaskNoteControl), default(string));

        public string TaskPriority
        {
            get { return (string)GetValue(TaskPriorityProperty); }
            set { SetValue(TaskPriorityProperty, value); }
        }

        public event EventHandler ClickedTask;

        public TaskNoteControl()
        {
            InitializeComponent();

            taskTitle.SetBinding(Label.TextProperty, new Binding("TaskTitle", source: this));
            taskDescription.SetBinding(Label.TextProperty, new Binding("TaskDescription", source: this));
            taskPriority.SetBinding(Label.TextProperty, new Binding("TaskPriority", source: this));

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    ClickedTask?.Invoke(this, EventArgs.Empty);
                })
            });
        }
    }
}