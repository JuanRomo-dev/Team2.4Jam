using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    List<HighscoreElements> highscoreElements = new List<HighscoreElements>();
    [SerializeField] int maxScores = 10;
    [SerializeField] string filename;

    private void Start()
    {
        LoadHighScores();    
    }

    private void LoadHighScores()
    {
        highscoreElements = FileHandler.ReadListFromJSON<HighscoreElements>(filename);

        while (highscoreElements.Count > maxScores)
        {
            highscoreElements.RemoveAt(maxScores);
        }
    }

    private void SaveHighScore()
    {
        FileHandler.SaveToJSON<HighscoreElements>(highscoreElements, filename);
    }

    public void AddNewHighscore (HighscoreElements highscore)
    {
        for (int i = 0; i < maxScores; i++)
        {
            int pointsScore = highscore.points;
            if (i >= highscoreElements.Count ||highscore.points > highscoreElements[i].points)
            {   
                // Add a new highscore
                highscoreElements.Insert(i, highscore);

                while (highscoreElements.Count > maxScores)
                {
                    highscoreElements.RemoveAt(maxScores);
                }

                SaveHighScore();

                break;
            }
        }
    }

    public List<HighscoreElements> GetHighscores()
    {
        return new List<HighscoreElements>(highscoreElements);
    }
}
