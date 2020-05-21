using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pisit.DataModel
{
	class Employee
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public string Patronymic { get; set; }

		[Required]
		public string PassportSeries { get; set; }
		[Required]
		public string PassportNum { get; set; }
		[Required]
		public string PassportIssuedBy { get; set; }
		[Required]
		public DateTime? PassportIssuedAt { get; set; }

		public List<EmployeeSkill> EmployeeSkills { get; set; }

		[Required]
		public string Address { get; set; }
		[Required]
		public string Phone { get; set; }

		[Required]
		public int DepartmentId { get; set; }
		public Department Department { get; set; }

		[Required]
		public int PostId { get; set; }
		public Post Post { get; set; }

		[NotMapped]
		public string FullName { get => Surname + " " + Name + " " + Patronymic; }

		[NotMapped]
		public string FullPassportNum { get => PassportSeries + " / " + PassportNum; }

		[NotMapped]
		public string FullPassportIssue { get => PassportIssuedAt?.ToShortDateString() + " " + PassportIssuedBy;}

		public void Copy(Employee another)
		{
			Name = another.Name;
			Surname = another.Surname;
			Patronymic = another.Patronymic;
			PassportSeries = another.PassportSeries;
			PassportNum = another.PassportNum;
			PassportIssuedBy = another.PassportIssuedBy;
			PassportIssuedAt = another.PassportIssuedAt;
			Address = another.Address;
			Phone = another.Phone;
			DepartmentId = another.DepartmentId;
			PostId = another.PostId;
		}
	}
}
