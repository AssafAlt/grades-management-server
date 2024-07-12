using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Attendance;
using api.Dtos.Class;
using api.Dtos.GradeItem;
using api.Extensions;
using api.Interfaces;
using api.Interfaces.Services;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IClassService _classService;

        public ClassController(UserManager<AppUser> userManager, IClassService classService)
        {
            _userManager = userManager;
            _classService = classService;
        }

        [HttpGet("{classId:int}")]
        public async Task<IActionResult> GetAllStudentsByClassId([FromRoute] int classId)
        {
            var results = await _classService.GetStudentsByClassIdAsync(classId);

            if (results.Data == null) return NotFound(results.Message);

            return StatusCode(results.StatusCode, results.Data);

        }

        [HttpGet("my-classes")]
        public async Task<IActionResult> GetAllClassesByTeacherId()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var results = await _classService.GetAllClassesByTeacherId(appUser.Id);
            if (results.Data == null) return NotFound(results.Message);
            return StatusCode(results.StatusCode, results.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassDto classDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var results = await _classService.CreateAsync(classDto, appUser.Id);

            return StatusCode(results.StatusCode, results.Message);
        }

        [HttpPut("{classId:int}/students/add")]
        public async Task<IActionResult> AddStudent([FromRoute] int classId, [FromBody] AddStudentRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var results = await _classService.AddStudentToClassAsync(classId, requestDto.StudentId);

            return StatusCode(results.StatusCode, results.Message);
        }
        [HttpDelete("{classId:int}/students/remove")]
        public async Task<IActionResult> RemoveStudent([FromRoute] int classId, [FromBody] RemoveStudentRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var results = await _classService.RemoveStudentFromClassAsync(classId, requestDto.StudentId);

            return StatusCode(results.StatusCode, results.Message);
        }
        [HttpPost("{classId:int}/attendances")]
        public async Task<IActionResult> AddAttendance([FromRoute] int classId, [FromBody] CreateAttendanceDto attendanceDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            attendanceDto.ClassId = classId;
            var results = await _classService.CreateAttendanceAsync(attendanceDto);
            return StatusCode(results.StatusCode, results.Message);

        }
        [HttpPost("{classId}/gradeitems")]
        public async Task<IActionResult> AddGradeItem([FromRoute] int classId, [FromBody] CreateGradeItemDto gradeItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            gradeItemDto.ClassId = classId;
            var results = await _classService.CreateGradeItemAsync(gradeItemDto);
            return StatusCode(results.StatusCode, results.Message);

        }

    }
}