public class Course
{
    public string GetLongName() { return Name + " in " + City + ", " + State; }

    public string Name { get; set; }
    public State State { get; set; }
    public string City { get; set; }
    public List<Loop> Loops { get; set; }


    public float GetAveragePar()
    {
        int sumPars = 0;
        foreach (Loop loop in Loops) sumPars += loop.Par;

        return (float)sumPars / Loops.Count;
    }
    public float GetCumulativeDifficulty()
    {
        float sumDifficulty = 0;
        int numReviewedLoops = 0;
        foreach (Loop loop in Loops)
        {   // Skip Loops with NaN rating
            float loopCumulativeDiff = loop.GetCumulativeDifficulty();
            if (float.IsNaN(loopCumulativeDiff)) continue;

            // Add Ratings to sum
            sumDifficulty += loopCumulativeDiff;
            numReviewedLoops++;
        }

        return sumDifficulty / numReviewedLoops;
    }
    public float GetCumulativeRating()
    {
        float sumRating = 0;
        int numReviewedLoops = 0;
        foreach (Loop loop in Loops)
        {   // Skip Loops with NaN rating
            float loopCumulativeRating = loop.GetCumulativeRating();
            if (float.IsNaN(loopCumulativeRating)) continue;

            // Add Ratings to sum
            sumRating += loopCumulativeRating;
            numReviewedLoops++;
        }

        return sumRating / numReviewedLoops;
    }
    public int GetTotalStrokes()
    {
        int sum = 0;
        foreach (Loop loop in Loops)
            sum += loop.GetTotalStrokes();

        return sum;
    }
    public float GetAverageStrokes()
    {
        if (Loops.Count == 0) return 0;


        int sum = 0;
        foreach (Loop loop in Loops)
            sum += loop.GetAverageStrokes();

        return sum / Loops.Count;
    }
    public int GetTotalMatches()
    {
        int sum = 0;
        foreach (Loop loop in Loops)
            sum += loop.Matches.Count;

        return sum;
    }
    public int GetHolesWithScore(int score)
    {
        int sum = 0;
        foreach (Loop loop in Loops)
            sum += loop.GetHolesWithScore(score);

        return sum;
    }
    public HoleInfo GetHardestHole(bool getHardest = true)
    {
        // Cache Hardest Hole and its Average strokes
        HoleInfo bestHole = new HoleInfo() { AverageStrokes = getHardest ? float.MinValue : float.MaxValue, HoleNum = -1 };

        // Go Through all Loops
        foreach (Loop loop in Loops)
        {
            float[] averageStrokesPerHole = loop.GetAverageStrokePerHole();
            for (int i = 0; i < averageStrokesPerHole.Length; i++)
                if ((averageStrokesPerHole[i] > bestHole.AverageStrokes) == getHardest)
                    bestHole = new HoleInfo() { Loop = loop, HoleNum = i + 1, AverageStrokes = averageStrokesPerHole[i] };
        }


        return bestHole;
    }
    public float GetAveragePercentOverPar()
    {
        float sumOfLoopPercents = 0;
        int numLoopPercents = 0;

        foreach (Loop loop in Loops)
        {
            // Loop has >0 Games Gate
            if (loop.Matches.Count <= 0) continue;

            numLoopPercents++;
            sumOfLoopPercents += loop.GetAveragePercentOverPar();
        }

        if (numLoopPercents == 0) return 0;
        return sumOfLoopPercents / numLoopPercents;
    }
    public float GetPercentHolesWithScore(int score)
    {
        float sumPercent = 0;
        int countedPercents = 0;

        // Count Loops that have matches and sum their percents
        foreach (Loop loop in Loops)
        {
            if (loop.Matches.Count <= 0) continue;

            sumPercent += loop.GetPercentHolesWithScore(score);
            countedPercents++;
        }

        return sumPercent / countedPercents;
    }
    public Loop GetLoop(string name)
    {
        foreach (Loop loop in Loops)
            if (loop.Name.Equals(name)) return loop;

        return null;
    }
}