using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pisit.DataModel
{
	class Department
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public List<Employee> Employees { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public void Copy(Department another)
		{
			Name = another.Name;
		}
	}
}
