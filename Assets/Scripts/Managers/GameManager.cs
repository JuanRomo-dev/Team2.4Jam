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
    public static GameManager Instance { get; private set; }

    public GameState currentGameState = GameState.Start;

    [Header("Game Metrics")]
    public float gameTime = 180f;   // 3 minutes in seconds
    public float reliability = 100f;
    public int popularity = 0;
    private float _timer;     // Timer 
    
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
        _timer = gameTime;
        LoadCaptcha();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.PlayingRound)
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0f)
            {
                // End game
                EndGame(true);
            }
        }
    }

    void LoadCaptcha()
    {
        currentGameState = GameState.Captcha;
    }

    void OnCaptchaCompleted()
    {
        StartRound();
    }

    void StartRound()
    {
        currentGameState = GameState.PlayingRound;
        currentRound++;
    }

    void SubmitRound()
    {
        currentGameState = GameState.RoundEnded;

        int numberOfNews = 0;
        int correctNews = 0;
        int incorrectNews = 0;
        
        int reliability = correctNews + (incorrectNews * -3);

        if (reliability < 50.0f)
        {
            EndGame(false);
        } else {
            // Start new round
        }
    }
    
    void EndGame(bool win)
    {
        currentGameState = win ? GameState.GameWon : GameState.GameOver;
    }
}
