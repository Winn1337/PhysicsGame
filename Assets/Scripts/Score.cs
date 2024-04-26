/// <summary>
/// Super simple singleton score script
/// </summary>
public class Score
{
    private static Score instance;

    private int score;

    public static Score Instance()
    {
        if (instance == null) instance = new Score();

        return instance;
    }

    public void Add(int score)
    {
        this.score += score;
    }

    public void Remove(int score)
    {
        this.score -= score;
    }

    public int Get()
    {
        return score;
    }
}
