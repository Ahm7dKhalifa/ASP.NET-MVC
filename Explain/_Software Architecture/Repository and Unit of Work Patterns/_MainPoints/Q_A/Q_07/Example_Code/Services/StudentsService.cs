// <summary>
/// Summary description for StudentsService
/// </summary>
public class StudentsService : GenericService<Student>
{
    StudentsRepository _Repository;

    public StudentsService(StudentsRepository repository) : base(repository)
    {
        _Repository = repository;
    }

    public List<Students> GetByDepartment(int departmentID)
    {
        return _Repository.GetByDepartment(departmentID);
    }

}