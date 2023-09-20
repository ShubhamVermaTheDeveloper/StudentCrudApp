using StudentCrudWebAPI.Models;

namespace StudentCrudWebAPI.Repository
{
    public interface IStudent
    {
        List<StudentModel> GetAll();
        StudentModel GetById(int id);
        void Add(StudentModel student);
        bool Update(int id, StudentModel student);
        bool Delete(int id);
    }
}
