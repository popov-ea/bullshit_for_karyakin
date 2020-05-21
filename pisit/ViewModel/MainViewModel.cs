using Microsoft.EntityFrameworkCore;
using pisit.DataModel;
using pisit.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace pisit.ViewModel
{
	class MainViewModel : INotifyPropertyChanged
	{
		public AppDbContext _db { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		#region employee

		public ObservableCollection<Employee> Employees { get; set; }

		private Employee _selectedEmployee;
		public Employee SelectedEmployee
		{
			get => _selectedEmployee;
			set
			{
				_selectedEmployee = value;
				SelectedPost = value?.Post;
				SelectedDepartment = value?.Department;
				EmployeeSkills = new ObservableCollection<EmployeeSkill>(SelectedEmployee?.EmployeeSkills ?? new List<EmployeeSkill>());
				OnPropertyChanged("SelectedEmployee");
				OnPropertyChanged("SelectedPost");
				OnPropertyChanged("SelectedDepartment");
				OnPropertyChanged("ChangeEmployeeBtnEnabled");
				OnPropertyChanged("DeleteEmployeeBtnEnabled");
				OnPropertyChanged("EmployeeSkills");
			}
		}


		private RelayCommand _saveEmployeeCommand;
		public RelayCommand SaveEmployeeCommand { 
			get
			{
				return _saveEmployeeCommand ?? (_saveEmployeeCommand = new RelayCommand(obj =>
				{
					var stored = _db.Employees.FirstOrDefault(e => e.Id == SelectedEmployee.Id);
					SelectedEmployee.Post = SelectedPost;
					SelectedEmployee.Department = SelectedDepartment;
					SelectedEmployee.EmployeeSkills = EmployeeSkills.ToList();
					if (stored == null)
					{
						_db.Employees.Add(SelectedEmployee);
					}
					else
					{
						stored.Copy(SelectedEmployee);
					}
					_db.SaveChanges();
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					ForceEmployeeBtnDisabled = false;
					OnPropertyChanged("Employees");
				}));
			}
		}

		private RelayCommand _newEmployeeAsSelected;
		public RelayCommand NewEmployeeAsSelected
		{
			get
			{
				return _newEmployeeAsSelected ?? (_newEmployeeAsSelected = new RelayCommand(obj =>
				{
					SelectedEmployee = new Employee();
					ForceEmployeeBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _deleteSelectedEmployee;
		public RelayCommand DeleteSelectedEmployee 
		{
			get
			{
				return _deleteSelectedEmployee ?? (_deleteSelectedEmployee = new RelayCommand(obj =>
				{
					var stored = _db.Employees.FirstOrDefault(e => e.Id == SelectedEmployee.Id);
					if (stored == null)
					{
						return;
					}
					else
					{
						_db.Employees.Remove(stored);
					}
					_db.SaveChanges();
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					SelectedEmployee = null;
					OnPropertyChanged("Employees");
				}));
			}
		}
		#endregion

		#region post

		public ObservableCollection<Post> Posts { get; set; }

		private Post _selectedPost;
		public Post SelectedPost 
		{
			get => _selectedPost;
			set
			{
				_selectedPost = value;
				OnPropertyChanged("SelectedPost");
				OnPropertyChanged("ChangePostBtnEnabled");
				OnPropertyChanged("DeletePostBtnEnabled");
			}
		}

		private RelayCommand _savePost;
		public RelayCommand SavePost {
			get
			{
				return _savePost ?? (_savePost = new RelayCommand(obj =>
				{
					var stored = _db.Posts.FirstOrDefault(p => p.Id == SelectedPost.Id);
					if (stored == null)
					{
						_db.Posts.Add(SelectedPost);
					}
					else
					{
						stored.Copy(SelectedPost);
					}
					_db.SaveChanges();
					Posts = new ObservableCollection<Post>(_db.Posts.ToList());
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					ForcePostBtnDisabled = false;
					OnPropertyChanged("Posts");
				}));
			}
		}

		private RelayCommand _newPostAsSelected;
		public RelayCommand NewPostAsSelected {
			get
			{
				return _newPostAsSelected ?? (_newPostAsSelected = new RelayCommand(obj =>
				{
					SelectedPost = new Post();
					ForcePostBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _deletePost;
		public RelayCommand DeletePost { 
			get
			{
				return _deletePost ?? (_deletePost = new RelayCommand(obj =>
				{
					var stored = _db.Posts.FirstOrDefault(p => p.Id == SelectedPost.Id);
					if (stored == null)
					{
						return;
					}
					_db.Posts.Remove(stored);
					_db.SaveChanges();
					Posts = new ObservableCollection<Post>(_db.Posts.ToList());
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					ForcePostBtnDisabled = false;
					SelectedPost = null;
					OnPropertyChanged("Posts");
				}));
			}
		}

		#endregion

		#region department

		public ObservableCollection<Department> Departments { get; set; }

		private Department _selectedDepartment;
		public Department SelectedDepartment
		{
			get => _selectedDepartment;
			set
			{
				_selectedDepartment = value;
				OnPropertyChanged("SelectedDepartment");
				OnPropertyChanged("ChangeDepartmentBtnEnabled");
				OnPropertyChanged("DeleteDepartmentBtnEnabled");
			}
		}

		private RelayCommand _saveDepartment;
		public RelayCommand SaveDepartment
		{
			get
			{
				return _saveDepartment ?? (_saveDepartment = new RelayCommand(obj =>
				{
					var stored = _db.Departments.FirstOrDefault(d => d.Id == SelectedDepartment.Id);
					if (stored == null)
					{
						_db.Departments.Add(SelectedDepartment);
					}
					else
					{
						stored.Copy(SelectedDepartment);
					}
					_db.SaveChanges();
					Departments = new ObservableCollection<Department>(_db.Departments);
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					ForceDepartmentBtnDisabled = false;
					OnPropertyChanged("Departments");
				}));
			}
		}

		public RelayCommand _newDepartmentAsSelected;
		public RelayCommand NewDepartmentAsSelected
		{ 
			get
			{
				return _newDepartmentAsSelected ?? (_newDepartmentAsSelected = new RelayCommand(obj =>
				{
					SelectedDepartment = new Department();
					ForceDepartmentBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _deleteDepartment;
		public RelayCommand DeleteDepartment
		{
			get
			{
				return _deleteDepartment ?? (_deleteDepartment = new RelayCommand(obj =>
				{
				var stored = _db.Departments.FirstOrDefault(d => d.Id == SelectedDepartment.Id);
					if (stored == null)
					{
						return;
					}
					_db.Departments.Remove(stored);
					_db.SaveChanges();
					Departments = new ObservableCollection<Department>(_db.Departments.ToList());
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					SelectedDepartment = null;
					ForceDepartmentBtnDisabled = false;
					OnPropertyChanged("Departments");
				}));
			}
		}

		#endregion

		#region skills
		public ObservableCollection<Skill> Skills { get; set; }
		public Skill _selectedSkill { get; set; }
		public Skill SelectedSkill { 
			get => _selectedSkill;
			set
			{
				_selectedSkill = value;
				OnPropertyChanged("SelectedSkill");
				OnPropertyChanged("ChangeSkillBtnEnabled");
				OnPropertyChanged("DeleteSkillBtnEnabled");
			}
		}
		private RelayCommand _saveSkill;
		public RelayCommand SaveSkill
		{
			get
			{
				return _saveSkill ?? (_saveSkill = new RelayCommand(obj =>
				{
					var stored = _db.Skills.FirstOrDefault(d => d.Id == SelectedSkill.Id);
					if (stored == null)
					{
						_db.Skills.Add(SelectedSkill);
					}
					else
					{
						stored.Copy(SelectedSkill);
					}
					_db.SaveChanges();
					Skills = new ObservableCollection<Skill>(_db.Skills);
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					ForceSkillBtnDisabled = false;
					OnPropertyChanged("Skills");
				}));
			}
		}

		public RelayCommand _newSkillAsSelected;
		public RelayCommand NewSkillAsSelected
		{
			get
			{
				return _newSkillAsSelected ?? (_newSkillAsSelected = new RelayCommand(obj =>
				{
					SelectedSkill = new Skill();
					ForceSkillBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _deleteSkill;
		public RelayCommand DeleteSkill
		{
			get
			{
				return _deleteSkill ?? (_deleteSkill = new RelayCommand(obj =>
				{
					var stored = _db.Skills.FirstOrDefault(d => d.Id == SelectedSkill.Id);
					if (stored == null)
					{
						return;
					}
					_db.Skills.Remove(stored);
					_db.SaveChanges();
					Skills = new ObservableCollection<Skill>(_db.Skills.ToList());
					Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
					SelectedSkill = null;
					ForceSkillBtnDisabled = false;
					OnPropertyChanged("Skills");
				}));
			}
		}
		#endregion

		#region employeeSkill 

		public ObservableCollection<EmployeeSkill> EmployeeSkills { get; set; }

		public EmployeeSkill _selectedEmployeeSkill { get; set; }
		public EmployeeSkill SelectedEmployeeSkill {
			get => _selectedEmployeeSkill;
			set
			{
				_selectedEmployeeSkill = value;
				OnPropertyChanged("SelectedEmployeeSkill");
			}
		}

		public int? EmployeeSkillLevel { get; set; }

		private RelayCommand _addSkillToEmployee;
		public RelayCommand AddSkillToEmployee { 
			get
			{
				return _addSkillToEmployee ?? (_addSkillToEmployee = new RelayCommand((obj) =>
				{
					if (!EmployeeSkillLevel.HasValue)
					{
						return;
					}


					EmployeeSkills.Add(new EmployeeSkill()
					{
						EmployeeId = SelectedEmployee.Id,
						SkillId = SelectedSkill.Id,
						Level = EmployeeSkillLevel.Value,
						Skill = SelectedSkill
					});

					SelectedEmployeeSkill = EmployeeSkills.Last();

					_db.SaveChanges();
					EmployeeSkillLevel = (int?)null;
				}));
			}
		}

		private RelayCommand _removeSelectedEmployeeSkill;
		public RelayCommand DeleteSelectedEmployeeSkill
		{
			get => _removeSelectedEmployeeSkill ?? (_removeSelectedEmployeeSkill = new RelayCommand((obj) =>
			{
				if (SelectedEmployeeSkill == null)
				{
					return;
				}

				EmployeeSkills.Remove(SelectedEmployeeSkill);
			}));
		}

		#endregion

		#region buttons

		private RelayCommand _disableEmployeeButtons;
		public RelayCommand DisableEmployeeButtons
		{
			get
			{
				return _disableEmployeeButtons ?? (_disableEmployeeButtons = new RelayCommand(obj =>
				{
					ForceEmployeeBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _enableEmployeeButtons;
		public RelayCommand EnableEmployeeButtons
		{
			get
			{
				return _enableEmployeeButtons ?? (_enableEmployeeButtons = new RelayCommand(obj =>
				{
					ForceEmployeeBtnDisabled = false;
				}));
			}
		}

		private RelayCommand _disablePostButtons;
		public RelayCommand DisablePostButtons
		{
			get
			{
				return _disablePostButtons ?? (_disablePostButtons = new RelayCommand(obj =>
				{
					ForcePostBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _enablePostButtons;
		public RelayCommand EnablePostButtons
		{
			get
			{
				return _enablePostButtons ?? (_enablePostButtons = new RelayCommand(obj =>
				{
					ForcePostBtnDisabled = false;
				}));
			}
		}

		private RelayCommand _disableDepartmentButtons;
		public RelayCommand DisableDepartmentButtons
		{
			get
			{
				return _disableDepartmentButtons ?? (_disableDepartmentButtons = new RelayCommand(obj =>
				{
					ForceDepartmentBtnDisabled = true;
				}));
			}
		}

		private RelayCommand _enableDepartmentButtons;
		public RelayCommand EnableDepartmentButtons
		{
			get
			{
				return _enableDepartmentButtons ?? (_enableDepartmentButtons = new RelayCommand(obj =>
				{
					ForceDepartmentBtnDisabled = false;
				}));
			}
		}

		private RelayCommand _enableSkillButtons;
		public RelayCommand EnableSkillButtons
		{
			get
			{
				return _enableSkillButtons ?? (_enableSkillButtons = new RelayCommand(obj =>
				{
					ForceSkillBtnDisabled = false;
				}));
			}
		}

		private RelayCommand _disableSkillButtons;
		public RelayCommand DisableSkillButtons
		{
			get
			{
				return _disableSkillButtons ?? (_disableSkillButtons = new RelayCommand(obj =>
				{
					ForceSkillBtnDisabled = true;
				}));
			}
		}


		private bool _forceEmployeeBtnDisabled = false;
		public bool ForceEmployeeBtnDisabled
		{
			get => _forceEmployeeBtnDisabled;
			set
			{
				_forceEmployeeBtnDisabled = value;
				OnPropertyChanged("ForceBtnDisabled");
				OnPropertyChanged("AddEmployeeBtnEnabled");
				OnPropertyChanged("ChangeEmployeeBtnEnabled");
				OnPropertyChanged("DeleteEmployeeBtnEnabled");
			}
		}

		private bool _forcePostBtnDisabled = false;
		public bool ForcePostBtnDisabled
		{
			get => _forcePostBtnDisabled;
			set
			{
				_forcePostBtnDisabled = value;
				OnPropertyChanged("AddPostBtnEnabled");
				OnPropertyChanged("ChangePostBtnEnabled");
				OnPropertyChanged("DeletePostBtnEnabled");
			}
		}

		private bool _forceDepartmentBtnDisabled = false;
		public bool ForceDepartmentBtnDisabled
		{
			get => _forceDepartmentBtnDisabled;
			set
			{
				_forceDepartmentBtnDisabled = value;
				OnPropertyChanged("AddDepartmentBtnEnabled");
				OnPropertyChanged("ChangeDepartmentBtnEnabled");
				OnPropertyChanged("DeleteDepartmentBtnEnabled");
			}
		}

		private bool _forceSkillBtnDisabled = false;
		public bool ForceSkillBtnDisabled
		{
			get => _forceSkillBtnDisabled;
			set
			{
				_forceSkillBtnDisabled = value;
				OnPropertyChanged("AddSkilBtnEnabled");
				OnPropertyChanged("ChangeSkillBtnEnabled");
				OnPropertyChanged("DeleteSkillBtnEnabled");
			}
		}

		public bool AddEmployeeBtnEnabled { get => !ForceEmployeeBtnDisabled; }
		public bool ChangeEmployeeBtnEnabled { get => SelectedEmployee?.Id > 0 && !ForceEmployeeBtnDisabled; }
		public bool DeleteEmployeeBtnEnabled { get => SelectedEmployee?.Id > 0 && !ForceEmployeeBtnDisabled; }

		public bool AddPostBtnEnabled { get => !ForcePostBtnDisabled; }
		public bool ChangePostBtnEnabled { get => SelectedPost?.Id > 0 && !ForcePostBtnDisabled; }
		public bool DeletePostBtnEnabled { get => SelectedPost?.Id > 0 && !ForcePostBtnDisabled; }

		public bool AddDepartmentBtnEnabled { get => !ForceDepartmentBtnDisabled; }
		public bool ChangeDepartmentBtnEnabled { get => SelectedDepartment?.Id > 0 && !ForceDepartmentBtnDisabled; }
		public bool DeleteDepartmentBtnEnabled { get => SelectedDepartment?.Id > 0 && !ForceDepartmentBtnDisabled; }

		public bool AddSkilBtnEnabled { get => !ForceSkillBtnDisabled; }
		public bool ChangeSkillBtnEnabled { get => SelectedSkill?.Id > 0 && !ForceSkillBtnDisabled; }
		public bool DeleteSkillBtnEnabled { get => SelectedSkill?.Id > 0 && !ForceSkillBtnDisabled; }

		#endregion

		#region search

		#endregion

		public MainViewModel()
		{
			_db = new AppDbContext();
			Employees = new ObservableCollection<Employee>(_db.Employees.Include(e => e.Post).Include(e => e.Department).Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).ToList());
			Departments = new ObservableCollection<Department>(_db.Departments.ToList());
			Posts = new ObservableCollection<Post>(_db.Posts.ToList());
			Skills = new ObservableCollection<Skill>(_db.Skills.ToList());
		}

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
