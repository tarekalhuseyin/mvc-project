using AutoMapper;
using Demo.DAL.Migrations;
using Demo.DAL.Models;
using Demo.DLL.Interfaces;
using Demo.DLL.Repositories;
using Demo.pl.Helpers;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.pl.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
         
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(SearchValue))
                  employees=await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
               employees= _unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
               var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmoployeeViewModel>>(employees);
             return View(MappedEmployees);
        }
        public IActionResult Create()
        {
           // ViewBag.Departments = _departmentRepository.GetAll(); 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmoployeeViewModel employeeVM)
        {

            if (ModelState.IsValid)
            {
                  employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var MappedEmployee= _mapper.Map<EmoployeeViewModel,Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                await _unitOfWork.CopleteAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployees = _mapper.Map<Employee, EmoployeeViewModel>(employee);
            return View(ViewName, MappedEmployees);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var Emp =_mapper.Map<Employee, EmoployeeViewModel>(employee);
            return View(Emp);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmoployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if(employeeVm.Image is not null)
                    {
                    employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                    }
                    
                    var MappedEmployee = _mapper.Map<Employee>(employeeVm);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                   await _unitOfWork.CopleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                   
                }

            }
            return View(employeeVm);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmoployeeViewModel employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
                return BadRequest();


            try
            {
                var MappedEmployee = _mapper.Map<EmoployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);

                var Result= await _unitOfWork.CopleteAsync();
                if (Result > 0&&employeeVm.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVm);
            }



        }
    }
}
