using StudentMVC.Models;

namespace StudentManagerMVC.Models
{
    public class ViewModel
    {
        public class StudentFilterViewModel
        {
            public string FilterField { get; set; }
            public string FilterCriteria { get; set; }
            public string FilterValue { get; set; }
            public List<Member> Members { get; set; }
        }

    }
}
