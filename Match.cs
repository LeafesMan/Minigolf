public class Match
{
    public DateTime Date { get; set; }
    public List<ScoreCard> ScoreCards { get; set; }
    public bool IsGolferPresent(Golfer golfer)
    {
        // If on one of the cards --> true
        foreach (ScoreCard card in ScoreCards)
            if (card.GolferID == golfer.ID)
                return true;

        // Else --> false
        return false;
    }
    public bool DidWin(Golfer g)
    {
        // Returns True if the Gofler passed in Won or Tied

        // Find golfers score in this game
        int totalScore = 0;
        foreach (ScoreCard card in ScoreCards)
            if (card.GolferID == g.ID)
                totalScore = card.GetTotalScore();

        // See if passed golfer has the highest score or Tied for it
        foreach (ScoreCard card in ScoreCards)
            if (card.GetTotalScore() < totalScore)
                return false;

        return true;
    }
    public ScoreCard GetScoreCard(uint golferID)
    {
        foreach (ScoreCard scorecard in ScoreCards) if (scorecard.GolferID == golferID) return scorecard;
        return null;
    }
}
