﻿using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification.Employee_Specs
{
    public class EmployeeWithDepartmentSpecification:BaseSpecifications<Employee>
    {
        public EmployeeWithDepartmentSpecification():base()
        {
            AddIncludes();
        }
        public EmployeeWithDepartmentSpecification(int id):base(p=>p.Id==id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(p => p.Departments);
        }
    }
}
