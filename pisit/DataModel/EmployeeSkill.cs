using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pisit.DataModel
{
	class EmployeeSkill
	{
		public int Id { get; set; }
		public int Level { get; set; }

		[Required]
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }

		[Required]
		public int SkillId { get; set; }
		public Skill Skill { get; set; }
	}
}
