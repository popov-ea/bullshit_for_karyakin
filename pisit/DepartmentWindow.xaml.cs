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
	/// Логика взаимодействия для DepartmentWindow.xaml
	/// </summary>
	public partial class DepartmentWindow : Window
	{
		public DepartmentWindow()
		{
			Icon = new BitmapImage(new Uri("icon.png", UriKind.RelativeOrAbsolute));
			InitializeComponent();
		}

		private void Add_Button_Click(object sender, RoutedEventArgs e)
		{
			formHeader.Text = "Добавление";
			ShowForm();
		}

		private void btn_edit_Click(object sender, RoutedEventArgs e)
		{
			formHeader.Text = "Редактирование";
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
			dg_departments.Visibility = Visibility.Visible;
			sp_search.Visibility = Visibility.Hidden;
		}

		private void ShowForm()
		{
			dg_departments.Visibility = Visibility.Collapsed;
			sp_form.Visibility = Visibility.Visible;
			sp_search.Visibility = Visibility.Hidden;
		}
	}
}
