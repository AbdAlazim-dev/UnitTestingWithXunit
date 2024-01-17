using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/demoInternalEmployee")]
    public class DemoInternalEmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public DemoInternalEmployeeController(IEmployeeService employeeService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<InternalEmployeeDto>> CreateInternalEmployee(InternalEmployeeDto internalEmployeeDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // create an internal employee entity with default values filled out
            // and the values inputted via the POST request
            var internalEmployee =
                    await _employeeService.CreateInternalEmployeeAsync(
                        internalEmployeeDto.FirstName, internalEmployeeDto.LastName);

            // persist it
            await _employeeService.AddInternalEmployeeAsync(internalEmployee);

            // return created employee after mapping to a DTO
            return CreatedAtAction("GetInternalEmployee",
                _mapper.Map<InternalEmployeeDto>(internalEmployee),
                new { employeeId = internalEmployee.Id });
        }
    }
}
