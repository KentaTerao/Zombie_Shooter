public class RankingData
{
    public string Name { get; set; }
    public long Wave { get; set; }
    public long Score { get; set; }

    public RankingData()
    {
        Name = "";
        Wave = 0;
        Score = 0;
    }
}