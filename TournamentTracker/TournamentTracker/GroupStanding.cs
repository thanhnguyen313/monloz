using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamListForm
{
    public class GroupStanding
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } // Tên đội (để hiển thị nếu cần)
        public int Points { get; set; }      // Điểm số
        public int GoalDifference { get; set; } // Hiệu số
        public int GoalsFor { get; set; }    // Bàn thắng ghi được
    }
}
