using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureCourse.Data;
using AzureCourse.Models;
using AzureCourse.Services;

namespace AzureCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseStore _store;

        public CoursesController(CourseStore courseStore)
        {
            _store = courseStore;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _store.GetAllCourses();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert()
        {
            var data = new[]
            {
                new Course
                {
                    Title = "Course 1",
                    Modules = new[]
                    {
                        new Module
                        {
                            Title = "Module 1.1",
                            Clips = new[] 
                            {
                                new Clip
                                {
                                    Name = "Clip 1.1.1",
                                    Length = 245
                                }, 
                                new Clip
                                {
                                    Name = "Clip 1.1.2",
                                    Length = 333
                                }, 
                            }
                        }, 
                        new Module
                        {
                            Title = "Module 1.2",
                            Clips = new[] 
                            {
                                new Clip
                                {
                                    Name = "Clip 1.2.1",
                                    Length = 111
                                },
                                new Clip
                                {
                                    Name = "Clip 1.2.2",
                                    Length = 666
                                }
                            }
                        }, 
                    }
                },
                new Course
                {
                    Title = "Course 2",
                    Modules = new[]
                    {
                        new Module
                        {
                            Title = "Module 2.1",
                            Clips = new[] 
                            {
                                new Clip
                                {
                                    Name = "Clip 2.1.1",
                                    Length = 777
                                }, 
                                new Clip
                                {
                                    Name = "Clip 2.1.2",
                                    Length = 888
                                }, 
                            }
                        }, 
                        new Module
                        {
                            Title = "Module 2.2",
                            Clips = new[] 
                            {
                                new Clip
                                {
                                    Name = "Clip 2.2.1",
                                    Length = 999
                                },
                                new Clip
                                {
                                    Name = "Clip 2.2.2",
                                    Length = 444
                                }
                            }
                        }, 
                    }
                }
            };

            await _store.InsertCourses(data);
            return RedirectToAction(nameof(Index));
        }
    }
}
