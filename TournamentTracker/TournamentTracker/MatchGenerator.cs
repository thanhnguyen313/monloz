using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamListForm
{
    public class MatchGenerator
    {
        // Tạo vòng bảng (Round 1)
        public static void GenerateRound1(int tournamentId)
        {
            // Lấy số bảng cần chia từ Database
            int numGroups = DatabaseHelper.GetTournamentGroupCount(tournamentId);
            // Kiểm tra số lượng đội
            var teams = DatabaseHelper.GetTeams(tournamentId);
            if (teams.Count < numGroups * 2)
            {
                MessageBox.Show($"Không đủ đội! Cần ít nhất {numGroups * 2} đội để chia {numGroups} bảng.");
                return;
            }
            // Gọi SQL để nó tự Random và Chia bảng
            bool success = DatabaseHelper.GenerateGroupStage(tournamentId, numGroups);

            if (success)
            {
                MessageBox.Show($"Đã chia xong {numGroups} bảng đấu ngẫu nhiên!");
            }
        }
        // Tạo các vòng tiếp theo 
        public static void GenerateNextRound(int tournamentId)
        {
            // Xác định vòng lớn nhất hiện tại
            int currentRound = DatabaseHelper.GetMaxRound(tournamentId);
            // Nếu chưa có vòng nào thì không làm gì cả
            if (currentRound == 0) return;

            // --- LOGIC 1: CHUYỂN TỪ VÒNG BẢNG (ROUND 1) SANG KNOCKOUT (ROUND 2) ---
            if (currentRound == 1)
            {
                DataTable dtA = DatabaseHelper.GetStandingsTable(tournamentId, "A");
                DataTable dtB = DatabaseHelper.GetStandingsTable(tournamentId, "B");
                // Kiểm tra điều kiện: Phải có ít nhất 2 đội mỗi bảng
                if (dtA.Rows.Count < 2 || dtB.Rows.Count < 2)
                {
                    MessageBox.Show("Các bảng đấu chưa xác định đủ Top 2 để vào vòng trong.");
                    return;
                }
                // Lấy ID các đội đứng đầu (Dựa vào cột TeamID trả về từ SQL)
                int a1 = Convert.ToInt32(dtA.Rows[0]["TeamID"]); // Nhất A
                int a2 = Convert.ToInt32(dtA.Rows[1]["TeamID"]); // Nhì A
                int b1 = Convert.ToInt32(dtB.Rows[0]["TeamID"]); // Nhất B
                int b2 = Convert.ToInt32(dtB.Rows[1]["TeamID"]); // Nhì B
                // Tạo Round 2: Ghép chéo (Nhất bảng này gặp Nhì bảng kia)
                DatabaseHelper.InsertMatch(tournamentId, 2, 1, a1, b2, null);
                DatabaseHelper.InsertMatch(tournamentId, 2, 1, b1, a2, null);

                MessageBox.Show("Đã tạo lịch thi đấu Vòng 2 (Bán kết)!");
                return;
            }
            // --- LOGIC 2: TỪ CÁC VÒNG SAU (ROUND 2 -> 3 -> 4...) ---
            // Logic này dùng chung cho Bán kết -> Chung kết, hoặc Tứ kết -> Bán kết...

            // Lấy danh sách người thắng của vòng hiện tại
            List<int> winners = DatabaseHelper.GetWinnersFromRound(tournamentId, currentRound);

            // Kiểm tra các điều kiện dừng
            if (winners.Count == 0)
            {
                MessageBox.Show("Chưa có kết quả của vòng hiện tại. Hãy cập nhật tỉ số trước.");
                return;
            }

            if (winners.Count == 1)
            {
                MessageBox.Show($"GIẢI ĐẤU KẾT THÚC! Nhà Vô Địch là Team ID: {winners[0]}");
                return;
            }
            // Tính số vòng tiếp theo (Tăng dần: 2 -> 3, 3 -> 4...)
            int nextRound = currentRound + 1;
            // Ghép cặp đấu
            for (int i = 0; i < winners.Count; i += 2)
            {
                // Đảm bảo còn đủ cặp
                if (i + 1 < winners.Count)
                {
                    int team1 = winners[i];
                    int team2 = winners[i + 1];

                    // Insert vào DB với Round = nextRound
                    DatabaseHelper.InsertMatch(tournamentId, nextRound, 1, team1, team2, null);
                }
            }

            MessageBox.Show($"Đã tạo lịch thi đấu Vòng {nextRound}!");
        }
    }
}
