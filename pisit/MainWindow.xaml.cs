using pisit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pisit
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
			Icon = new BitmapImage(new Uri("icon.png", UriKind.RelativeOrAbsolute));

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

		private void btn_employee_Click(object sende, RoutedEventArgs e)
		{
			var employeeWindow = new EmployeeWindow();
			employeeWindow.DataContext = DataContext;
			employeeWindow.ShowDialog();
		}

		private void btn_skills_Click(object sender, RoutedEventArgs e)
		{
			var skillsWindow = new SkillsWindow();
			skillsWindow.DataContext = DataContext;
			skillsWindow.ShowDialog();
		}
	}
}
