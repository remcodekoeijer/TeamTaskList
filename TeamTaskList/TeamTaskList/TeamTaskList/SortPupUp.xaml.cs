using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TeamTaskList.Data;

namespace TeamTaskList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SortPupUp : ContentPage
	{
        TaskSampleData instance = TaskSampleData.GetInstance();
		public SortPupUp ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            int whatSortIndex = whatSort.SelectedIndex;
            int howSortIndex = howSort.SelectedIndex;

            if(whatSortIndex != -1 && howSortIndex != -1)
            {
                string whatToSort = whatSort.Items[whatSortIndex];
                string howToSort = howSort.Items[howSortIndex];
                SendSort(whatToSort, howToSort);
                MainPage.Sorted = true;

                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Missing Information", "You must fill out all fields.", "OK");
            }

        }

        private void SendSort(string what, string how)
        {
            if (what.Equals("Title"))
            {
                if (how.Equals("Ascending"))
                {
                    instance.SortTitleAsc();
                }
                else if (how.Equals("Descending"))
                {
                    instance.SortTitleDesc();
                }
            }

            if (what.Equals("Priority"))
            {
                if (how.Equals("Ascending"))
                {
                    instance.SortPriorityAsc();
                }
                else if (how.Equals("Descending"))
                {
                    instance.SortPriorityDesc();
                }
            }
        }
    }
}