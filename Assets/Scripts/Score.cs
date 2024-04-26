/// <summary>
/// Super simple singleton score script
/// </summary>
public class Score
{
    private static Score instance;
    private Score()
    {

    }

    public static Score Instance()
    {
        if (instance == null) instance = new Score();

        return instance;
    }

    private int score;

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

    public void Reset()
    {
        score = 0;
    }
}
