using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentsRepository
/// </summary>
public class StudentsRepository : GenericRepository<Student>
{
    public StudentsRepository() : base()
    {

    }

    public StudentsRepository(AppdbEntities db) : base(db)
    {
        //this.context = db;
    }

    public List<Student> GetByDepartment(int departmentID)
    {
        return context.PrinterMaintenancesUrls.Where(a => a.DepartmentID == departmentID).ToList();
    }
}
