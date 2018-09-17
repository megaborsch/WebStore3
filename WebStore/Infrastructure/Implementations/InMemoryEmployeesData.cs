using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Implementations
{
    /// <inheritdoc />
    /// <summary>
    /// Реализация интерфейса, хранит инфу в памяти
    /// </summary>
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<EmployeeView> _employees;
        public InMemoryEmployeesData()
        {
            _employees = new List<EmployeeView>(3)
{
        new EmployeeView(){Id = 1, FirstName = "Вася", SurName = "Пупкин", Patronymic =
        "Иванович", Age = 22, Nickname = "Директор"},
        new EmployeeView(){Id = 2, FirstName = "Иван", SurName = "Холявко", Patronymic =
        "Александрович", Age = 30, Nickname = "Программист"},
        new EmployeeView(){Id = 3, FirstName = "Роберт", SurName = "Серов", Patronymic =
        "Сигизмундович", Age = 50, Nickname = "Зав. склада"}
        };
        }
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }
        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }
        public void Commit()
        {
            // ничего не делаем
        }
        public void AddNew(EmployeeView model)
        {
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }
        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
    }
}
