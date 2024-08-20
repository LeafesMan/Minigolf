public class Loop
{
    public string GetLongName() { return Name + " Loop"; }
    public string Name { get; set; }
    public int Par { get; set; }
    public int NumHoles { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Match> Matches { get; set; }

    public int GetHolesCompleted()
    {
        int sum = 0;
        foreach (Match match in Matches) sum += match.ScoreCards.Count * NumHoles;
        return sum;
    }
    public int GetTotalStrokes()
    {
        int sum = 0;
        foreach (Match match in Matches)
            foreach (ScoreCard scoreCard in match.ScoreCards)
                sum += scoreCard.GetTotalScore();

        return sum;
    }
    public int GetAverageStrokes()
    {
        if (GetNumScoreCards() == 0) return 0;
        return GetTotalStrokes() / GetNumScoreCards();
    }
    public float GetAveragePercentOverPar()
    {
        return (float)(GetAverageStrokes() - Par) / Par * 100;
    }
    public float[] GetAverageStrokePerHole()
    {
        int[] totalStrokesPerHole = new int[NumHoles];
        int[] totalAttemptsPerHole = new int[NumHoles];

        foreach (Match match in Matches)
            foreach (ScoreCard scoreCard in match.ScoreCards)
                for (int i = 0; i < scoreCard.Strokes.Length; i++)
                {
                    totalStrokesPerHole[i] += scoreCard.Strokes[i];
                    totalAttemptsPerHole[i]++;
                }

        float[] averageStrokesPerHole = new float[NumHoles];
        for (int i = 0; i < totalStrokesPerHole.Length; i++)
            averageStrokesPerHole[i] = (float)totalStrokesPerHole[i] / totalAttemptsPerHole[i];

        return averageStrokesPerHole;
    }
    public int GetNumScoreCards()
    {
        int sum = 0;
        foreach (Match match in Matches)
            foreach (ScoreCard scoreCard in match.ScoreCards)
                sum++;

        return sum;
    }
    public int GetHolesWithScore(int score)
    {
        int sum = 0;
        foreach (Match match in Matches)
            foreach (ScoreCard scoreCard in match.ScoreCards)
                sum += scoreCard.GetHolesWithScore(score);
        return sum;
    }
    public float GetPercentHolesWithScore(int score)
    {
        return (float)GetHolesWithScore(score) / GetHolesCompleted() * 100;
    }
    public HoleInfo GetHardestHole(bool getHardest = true)
    {
        float[] averageStrokesPerHole = GetAverageStrokePerHole();

        HoleInfo bestHole = new HoleInfo() { AverageStrokes = getHardest ? float.MinValue : float.MaxValue, HoleNum = -1 };
        for (int i = 0; i < averageStrokesPerHole.Length; i++)
            if ((averageStrokesPerHole[i] > bestHole.AverageStrokes) == getHardest)
                bestHole = new HoleInfo() { AverageStrokes = averageStrokesPerHole[i], HoleNum = i + 1 };

        return bestHole;
    }
    public float GetCumulativeDifficulty()
    {
        float totalDifficulty = 0;
        int numReviews = 0;
        foreach (Review review in Reviews)
        {
            totalDifficulty += review.Difficulty;
            numReviews++;
        }

        return totalDifficulty / numReviews;
    }
    public float GetCumulativeRating()
    {
        float totalRating = 0;
        int numReviews = 0;
        foreach (Review review in Reviews)
        {
            totalRating += review.Rating;
            numReviews++;
        }

        return totalRating / numReviews;
    }
    public Review GetReview(uint golferID)
    {
        foreach (Review r in Reviews) if (r.GolferID == golferID) return r;
        return null;
    }
}