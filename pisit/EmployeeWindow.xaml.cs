using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pisit
{
	/// <summary>
	/// Логика взаимодействия для EmployeeWindow.xaml
	/// </summary>
	public partial class EmployeeWindow : Window
	{
		public EmployeeWindow()
		{
			InitializeComponent();
			Icon = new BitmapImage(new Uri("icon.png", UriKind.RelativeOrAbsolute));
		}

		private void Add_Button_Click(object sender, RoutedEventArgs e)
		{
			formHeader.Text = "Добавление";
			ShowForm();
		}

		private void Save_Button_Click(object sender, RoutedEventArgs e)
		{
			ShowGrid();
		}

		private void Cancel_Button_Click(object sender, RoutedEventArgs e)
		{
			ShowGrid();
		}

		private void btn_edit_Click(object sender, RoutedEventArgs e)
		{
			formHeader.Text = "Редактирование";
			ShowForm();
		}

		private void ShowForm()
		{
			sp_employeeGrid.Visibility = Visibility.Collapsed;
			g_employeeForm.Visibility = Visibility.Visible;
		}

		private void ShowGrid()
		{
			g_employeeForm.Visibility = Visibility.Collapsed;
			sp_employeeGrid.Visibility = Visibility.Visible;
		}

		private void btn_post_Click(object sender, RoutedEventArgs e)
		{
			var postsWindow = new PostsWindow();
			postsWindow.DataContext = DataContext;
			postsWindow.ShowDialog();
		}

		private void btn_departments_Click(object sender, RoutedEventArgs e)
		{
			var postsWindow = new DepartmentWindow();
			postsWindow.DataContext = DataContext;
			postsWindow.ShowDialog();
		}

		private void Add_Employee_Skill_Button_Click(object sender, RoutedEventArgs e)
		{
			var skillWindow = new SkillsWindow();
			skillWindow.DataContext = DataContext;
			skillWindow.ShowDialog();
		}
	}
}
