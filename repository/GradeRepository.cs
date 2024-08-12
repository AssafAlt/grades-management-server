using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Grade;
using api.Dtos.GradeItem;
using api.Dtos.Student;
using api.Interfaces.Repository;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDBContext _context;

        public GradeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Grade grade)
        {

            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();

        }

        public async Task CreateMultipleAsync(List<Grade> grades)
        {
            await _context.Grades.AddRangeAsync(grades);
            await _context.SaveChangesAsync();
        }



        public async Task SaveFinalGrade(FinalGradeDto finalGradeDto)
        {
            var finalGrade = new FinalGrade
            {
                ClassId = finalGradeDto.ClassId,
                Json = JsonSerializer.Serialize(finalGradeDto)
            };

            await _context.FinalGrades.AddAsync(finalGrade);
            await _context.SaveChangesAsync();
        }

        /*   public async Task<FinalGradeDto> GetFinalGradesByClassId(int classId)
           {
               var classEntity = await _context.Classes
                                   .Include(c => c.Students)
                                   .ThenInclude(s => s.Grades)
                                   .ThenInclude(g => g.GradeItem)
                                   .Include(c => c.Attendances) // Include attendances
                                   .FirstOrDefaultAsync(c => c.ClassId == classId);

               if (classEntity == null)
                   throw new Exception("Class not found");

               // Create the DTO
               var finalGradeDto = new FinalGradeDto
               {
                   ClassId = classEntity.ClassId,
                   ClassName = classEntity.ClassName,
                   Students = new List<StudentFinalGradeDto>()
               };

               double totalFinalGrade = 0;

               // Calculate final grades for each student
               foreach (var student in classEntity.Students)
               {
                   var studentDto = new StudentFinalGradeDto
                   {
                       StudentId = student.StudentId,
                       StudentName = $"{student.FirstName} {student.LastName}",
                       Grades = new List<GradeDto>()
                   };

                   double finalGrade = 0;
                   bool attendanceGradeAdded = false;
                   decimal attendanceWeight = 0.2M; // Assuming a fixed weight for attendances, or retrieve dynamically

                   foreach (var grade in student.Grades)
                   {
                       studentDto.Grades.Add(new GradeDto
                       {
                           Name = grade.GradeItem.Name,
                           Score = grade.Score,
                           Weight = grade.GradeItem.Weight
                       });

                       finalGrade += grade.Score * (double)grade.GradeItem.Weight;

                       if (grade.GradeItem.Name == "ATTENDANCES")
                       {
                           attendanceGradeAdded = true;
                           attendanceWeight = grade.GradeItem.Weight;
                       }
                   }

                   // If no "Attendances" grade exists, calculate it
                   if (!attendanceGradeAdded)
                   {
                       var totalAttendances = classEntity.Attendances.Count(a => a.StudentId == student.StudentId);
                       var presentAttendances = classEntity.Attendances.Count(a => a.StudentId == student.StudentId && a.IsPresent);

                       // Calculate attendance grade as a percentage of presence
                       double attendanceScore = totalAttendances > 0 ? (double)presentAttendances / totalAttendances * 100 : 0;

                       // Add the calculated attendance grade to the student's grades
                       studentDto.Grades.Add(new GradeDto
                       {
                           Name = "ATTENDANCES",
                           Score = attendanceScore,
                           Weight = attendanceWeight
                       });

                       // Add the attendance grade to the final grade calculation
                       finalGrade += attendanceScore * (double)attendanceWeight;
                   }

                   studentDto.FinalGrade = finalGrade;
                   finalGradeDto.Students.Add(studentDto);
                   totalFinalGrade += finalGrade;
               }

               finalGradeDto.ClassAverage = finalGradeDto.Students.Count > 0 ? totalFinalGrade / finalGradeDto.Students.Count : 0;

               return finalGradeDto;
           }*/

        public async Task<FinalGradeDto> GetFinalGradesByClassId(int classId)
        {
            // Check if the final grade already exists
            var existingFinalGrade = await _context.FinalGrades
                                   .FirstOrDefaultAsync(fg => fg.ClassId == classId);

            if (existingFinalGrade != null)
            {
                // If exists, return the existing final grade
                var json = JsonSerializer.Deserialize<FinalGradeDto>(existingFinalGrade.Json);

                return json;
            }

            var classEntity = await _context.Classes
                            .Include(c => c.Students)
                            .ThenInclude(s => s.Grades)
                            .ThenInclude(g => g.GradeItem)
                            .Include(c => c.GradeItems) // Include GradeItems to check for missing grades
                            .Include(c => c.Attendances) // Include Attendances to calculate them
                            .FirstOrDefaultAsync(c => c.ClassId == classId);

            if (classEntity == null)
                throw new Exception("Class not found");

            // Ensure required grade items are present
            var requiredGradeItems = classEntity.GradeItems
                .Where(gi => gi.Name != "ATTENDANCES")
                .ToList();

            foreach (var student in classEntity.Students)
            {
                var studentGrades = student.Grades
                    .Where(g => g.GradeItem.Name != "ATTENDANCES")
                    .ToList();

                // Ensure that the student has grades for all required grade items
                if (requiredGradeItems.Any(gi => !studentGrades.Any(g => g.GradeItemId == gi.GradeItemId)))
                {
                    return null; // Or handle the case where not all grades are present
                }
            }

            // Calculate missing ATTENDANCES grade for each student if not present
            var finalGradeDto = new FinalGradeDto
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName + " " + classEntity.GroupId,
                Students = new List<StudentFinalGradeDto>()
            };

            double totalFinalGrade = 0;

            // Calculate final grades for each student
            foreach (var student in classEntity.Students)
            {
                var studentDto = new StudentFinalGradeDto
                {
                    StudentId = student.StudentId,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Grades = new List<GradeDto>()
                };

                double finalGrade = 0;
                bool attendanceGradeAdded = false;
                decimal attendanceWeight = 0.2M; // Assuming a fixed weight for attendances, or retrieve dynamically

                foreach (var grade in student.Grades)
                {
                    studentDto.Grades.Add(new GradeDto
                    {
                        Name = grade.GradeItem.Name,
                        Score = grade.Score,
                        Weight = grade.GradeItem.Weight
                    });

                    finalGrade += grade.Score * (double)grade.GradeItem.Weight;

                    if (grade.GradeItem.Name == "ATTENDANCES")
                    {
                        attendanceGradeAdded = true;
                        attendanceWeight = grade.GradeItem.Weight;
                    }
                }

                // If no "Attendances" grade exists, calculate it
                if (!attendanceGradeAdded)
                {
                    var totalAttendances = classEntity.Attendances.Count(a => a.StudentId == student.StudentId);
                    var presentAttendances = classEntity.Attendances.Count(a => a.StudentId == student.StudentId && a.IsPresent);

                    // Calculate attendance grade as a percentage of presence
                    double attendanceScore = totalAttendances > 0 ? (double)presentAttendances / totalAttendances * 100 : 0;

                    // Add the calculated attendance grade to the student's grades
                    studentDto.Grades.Add(new GradeDto
                    {
                        Name = "ATTENDANCES",
                        Score = attendanceScore,
                        Weight = attendanceWeight
                    });

                    // Add the attendance grade to the final grade calculation
                    finalGrade += attendanceScore * (double)attendanceWeight;
                }

                studentDto.FinalGrade = finalGrade;
                finalGradeDto.Students.Add(studentDto);
                totalFinalGrade += finalGrade;
            }

            finalGradeDto.ClassAverage = finalGradeDto.Students.Count > 0 ? totalFinalGrade / finalGradeDto.Students.Count : 0;

            await SaveFinalGrade(finalGradeDto);

            return finalGradeDto;



        }

    }
}