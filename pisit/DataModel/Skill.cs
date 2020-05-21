using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pisit.DataModel
{
	class Skill
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public IEnumerable<EmployeeSkill> EmployeeSkills { get; set; }

		public void Copy(Skill another)
		{
			this.Name = another.Name;
		}
	}
}
