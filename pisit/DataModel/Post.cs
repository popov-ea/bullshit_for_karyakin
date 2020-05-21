using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pisit.DataModel
{
	class Post
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public List<Employee> Employees { get; set; }

		public override string ToString()
		{
			return this.Name;
		}

		public void Copy(Post another)
		{
			Name = another.Name;
		}
	}
}
