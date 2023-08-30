using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRandomizer_V0._0._1
{
    public class Team
    {
        public int teamNum { get; set; }
        public int teamSize { get; set; }
        public List<Student> teamList { get; set; }

        public Team()
        {
            teamNum = 0;
            teamSize = 0;
            teamList = new List<Student>();
        }
        public Team(int tmNm, int tmSze, List<Student> tmLst)
        {
            teamNum = tmNm;
            teamSize = tmSze;
            teamList = tmLst;
        }
        public Team(Team tm)
        {
            teamNum = tm.teamNum;
            teamSize = tm.teamSize;
            teamList = tm.teamList;
        }

        public override string ToString()
        {
            string msg = "";
            msg += "Team " + teamNum + ": (" + teamSize + ")\n";
            for (int i = 0; i < teamSize; i++)
            {
                msg += teamList[i].ToString() + "\n";
            }
            return msg;
        }
    }
}
