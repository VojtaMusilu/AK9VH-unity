using System;

[Serializable]
public class HighScoreElement
{
    public string name;
    public int score;

    public HighScoreElement(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}