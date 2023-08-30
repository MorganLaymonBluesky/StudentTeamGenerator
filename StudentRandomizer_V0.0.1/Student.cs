using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer_V0._0._1
{
    public class Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int cohortNum { get; set; }

        public Student()
        {
            firstName = "John";
            lastName = "Doe";
            cohortNum = 0;
        }
        public Student(string frstNme, string lstNme, int chrtNm)
        {
            firstName = frstNme;
            lastName = lstNme;
            cohortNum = chrtNm;
        }
        public Student(Student stu)
        {
            firstName = stu.firstName;
            lastName = stu.lastName;
            cohortNum = stu.cohortNum;
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}
