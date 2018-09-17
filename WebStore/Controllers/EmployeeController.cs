using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    [Route("users")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesData _employeesData;
        public EmployeeController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
           
        }
        /// <summary>
        /// Вывод списка
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_employeesData.GetAll());
        }
        /// <summary>
        /// Детали о сотруднике
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        [Route("{id}")]
        public IActionResult Details(int id)
        {
            // Получаем сотрудника по Id
            var employee = _employeesData.GetById(id);
            // Если такого не существует
            if (ReferenceEquals(employee, null))
                return NotFound();// возвращаем результат 404 Not Found
                                  // Иначе возвращаем сотрудника
            return View(employee);

            //private readonly List<EmployeeView> _employees = new List<EmployeeView>
            //{
            //new EmployeeView
            //{
            //Id = 1,
            //FirstName = "Иван",
            //SurName = "Иванов",
            //Patronymic = "Иванович",
            //Age = 22,
            //Nickname = "BOSS"
            //},
            //new EmployeeView
            //{
            //Id = 2,
            //FirstName = "Владислав",
            //SurName = "Петров",
            //Patronymic = "Иванович",
            //Age = 35,
            //Nickname = "MONSTER"
            //}
            //};
            //public IActionResult Index()
            //{
            //    //return Content("Hello from controller");
            //    return View(_employees);
            //}
            //[Route("{id}")]
            //public IActionResult Details(int id)
            //{
            //    EmployeeView em = _employees[id-1];
            //    return View(em);
            //}
        }

        /// <summary>
        /// Добавление или редактирование сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            EmployeeView model;
            if (id.HasValue)
            {
                model = _employeesData.GetById(id.Value);
                if (ReferenceEquals(model, null))
                    return NotFound();// возвращаем результат 404 Not Found
            }
            else
            {
                model = new EmployeeView();
            }
            return View(model);
        }
        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeView model)
        {
            if (model.Id > 0)
            {
                var dbItem = _employeesData.GetById(model.Id);
                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found
                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                dbItem.Nickname = dbItem.Nickname;
            }
            else
            {
                _employeesData.AddNew(model);
            }
            _employeesData.Commit();
            return RedirectToAction(nameof(Index));
        }        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _employeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }

}