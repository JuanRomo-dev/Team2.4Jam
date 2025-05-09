using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Captcha,
    PlayingRound,
    RoundEnded,
    GameOver,
    GameWon,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentGameState = GameState.Start;

    [Header("Game Metrics")]
    public float gameTime = 180f;   // 3 minutes in seconds
    public float reliability = 100f;
    public int popularity = 0;
    private float timer;     // Timer 
    
    [Header("Rounds")]
    public int currentRound = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        timer = gameTime;
        LoadCaptcha();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.PlayingRound)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                // End game
            }
        }
    }

    void LoadCaptcha()
    {
        
    }

    void EndGame(bool win)
    {
        currentGameState = win ? GameState.GameWon : GameState.GameOver;
    }
}
