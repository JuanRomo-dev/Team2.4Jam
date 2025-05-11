using System;

[Serializable]
public class HighscoreElements
{
    public string playerName;
    public int points;

    public HighscoreElements (string name, int points)
    {
        playerName = name;
        this.points = points;
    }
}
