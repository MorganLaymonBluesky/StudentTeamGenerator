using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer_V0._0._1
{
    public class Cohort
    {
        public int cohortId { get; set; }
        public List<Student> students { get; set; }

        public Cohort()
        {
            cohortId = 0;
            students = new List<Student>();
        }
        public Cohort(int chrtId, List<Student> stu)
        {
            cohortId = chrtId;
            students = stu;
        }
        public Cohort(Cohort chrt)
        {
            cohortId= chrt.cohortId;
            students = chrt.students;
        }

        public override string ToString()
        {
            string msg = "Cohort " + cohortId + ": \n";
            foreach (Student student in students)
            {
                msg += student + "\n";
            }
            return msg;
        }
    }
}
