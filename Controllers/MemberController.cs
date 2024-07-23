using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;

namespace MemberController.Controllers
{
    public class MemberController : Controller
    {
        public static List<Member> _members = new List<Member>
        {
            new Member { ID = 1, LastName = "Smith", FirstName = "John", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male", PlaceOfBirth = "New York", Mobile = "123-456-7890", IsGraduated = true },
            new Member { ID = 2, LastName = "Doe", FirstName = "Jane", DateOfBirth = new DateTime(1992, 2, 2), Gender = "Female", PlaceOfBirth = "Los Angeles", Mobile = "234-567-8901", IsGraduated = false },
            new Member { ID = 3, FirstName = "Mike", LastName = "Brown", Gender = "Male", DateOfBirth = new DateTime(1998, 7, 23), Mobile = "1122334455", PlaceOfBirth = "Chicago", IsGraduated = true },
            new Member { ID = 4, FirstName = "Anna", LastName = "White", Gender = "Female", DateOfBirth = new DateTime(2002, 8, 14), Mobile = "2233445566", PlaceOfBirth = "Ha Noi", IsGraduated = false },
            new Member { ID = 5, FirstName = "Tom", LastName = "Green", Gender = "Male", DateOfBirth = new DateTime(2001, 9, 15), Mobile = "3344556677", PlaceOfBirth = "Houston", IsGraduated = false },
            new Member { ID = 6, FirstName = "Kate", LastName = "Johnson", Gender = "Female", DateOfBirth = new DateTime(1999, 10, 10), Mobile = "4455667788", PlaceOfBirth = "San Francisco", IsGraduated = true }
            // Add more members as needed
        };

        // GET: Members
        public IActionResult Index(string filterField, string filterCriteria, string filterValue)
        {
            var members = _members.AsQueryable();

            if (!string.IsNullOrEmpty(filterField))
            {
                switch (filterField)
                {
                    case "Gender":
                        if (!string.IsNullOrEmpty(filterCriteria))
                        {
                            members = members.Where(m => m.Gender == filterCriteria);
                        }
                        break;
                    case "Oldest":
                        members = members.OrderBy(m => m.DateOfBirth).Take(1);
                        break;
                    case "FullName":
                        if (!string.IsNullOrEmpty(filterCriteria))
                        {
                            var names = filterCriteria.Split(' ');
                            if (names.Length == 2)
                            {
                                members = members.Where(m => m.FirstName == names[0] && m.LastName == names[1]);
                            }
                            else if (names.Length == 1)
                            {
                                members = members.Where(m => m.FirstName == names[0] || m.LastName == names[0]);
                            }
                        }
                        break;
                    case "BirthYear":
                        if (int.TryParse(filterValue, out int year))
                        {
                            members = members.Where(m => m.DateOfBirth.Year == year);
                        }
                        break;
                    case "PlaceOfBirth":
                        if (!string.IsNullOrEmpty(filterCriteria))
                        {
                            members = members.Where(m => m.PlaceOfBirth == filterCriteria);
                        }
                        break;
                }
            }

            var distinctPlacesOfBirth = members
                .Select(m => m.PlaceOfBirth)
                .Distinct()
                .ToList();

            ViewBag.PlacesOfBirth = distinctPlacesOfBirth;

            return View(members.ToList());
        }

        // GET: Members/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _members.FirstOrDefault(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,LastName,FirstName,DateOfBirth,Gender,PlaceOfBirth,Mobile,IsGraduated")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.ID = _members.Max(m => m.ID) + 1; // Generate new ID
                _members.Add(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _members.FirstOrDefault(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,LastName,FirstName,DateOfBirth,Gender,PlaceOfBirth,Mobile,IsGraduated")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingMember = _members.FirstOrDefault(m => m.ID == id);
                if (existingMember != null)
                {
                    existingMember.LastName = member.LastName;
                    existingMember.FirstName = member.FirstName;
                    existingMember.DateOfBirth = member.DateOfBirth;
                    existingMember.Gender = member.Gender;
                    existingMember.PlaceOfBirth = member.PlaceOfBirth;
                    existingMember.Mobile = member.Mobile;
                    existingMember.IsGraduated = member.IsGraduated;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _members.FirstOrDefault(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var member = _members.FirstOrDefault(m => m.ID == id);
            if (member != null)
            {
                _members.Remove(member);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _members.Any(e => e.ID == id);
        }
    }
}
