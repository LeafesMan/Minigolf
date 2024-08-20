public class ScoreCard
{
    public uint GolferID { get; set; }
    public int[] Strokes { get; set; } // Strokes for each hole

    public int GetTotalScore()
    {
        int sum = 0;
        foreach (int num in Strokes) sum += num;

        return sum;
    }
    // Gets the Number of holes with strokes in this score card
    public int GetHolesWithScore(int score)
    {
        int sum = 0;
        foreach (int strokes in Strokes) if (strokes == score) sum++;
        return sum;
    }
}