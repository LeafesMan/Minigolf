public class HoleInfo
{
    public int HoleNum { get; set; }
    public float AverageStrokes { get; set; }
    public Course Course { get; set; }
    public Loop Loop { get; set; }
    public string GetHoleString() { return HoleNum + (Loop != null ? ", " + Loop.GetLongName() : "") + (Course != null ? ", " + Course.GetLongName() : "") + " with " + AverageStrokes.ToString("n1") + " average strokes"; }
}