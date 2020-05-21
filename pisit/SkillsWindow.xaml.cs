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
	/// Логика взаимодействия для SkillsWindow.xaml
	/// </summary>
	public partial class SkillsWindow : Window
	{
		public SkillsWindow()
		{
			InitializeComponent();
			Icon = new BitmapImage(new Uri("icon.png", UriKind.RelativeOrAbsolute));
		}

		private void Add_Button_Click(object sender, RoutedEventArgs e)
		{
			ShowForm();
		}

		private void btn_edit_Click(object sender, RoutedEventArgs e)
		{
			ShowForm();
		}

		private void btn_save_Click(object sender, RoutedEventArgs e)
		{
			ShowGrid();
		}

		private void btn_cancel_Click(object sender, RoutedEventArgs e)
		{
			ShowGrid();
		}

		private void ShowGrid()
		{
			sp_form.Visibility = Visibility.Collapsed;
			dg_sklls.Visibility = Visibility.Visible;
			sp_search.Visibility = Visibility.Visible;
		}

		private void ShowForm()
		{
			dg_sklls.Visibility = Visibility.Collapsed;
			sp_form.Visibility = Visibility.Visible;
			sp_search.Visibility = Visibility.Hidden;
		}

		private void dg_sklls_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var employeeSkillWindow = new EmployeeSkillWindow();
			employeeSkillWindow.DataContext = DataContext;
			employeeSkillWindow.ShowDialog();
		}
	}
}
