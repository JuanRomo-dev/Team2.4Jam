using System;
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

    public UIManager uiManager;

    [Header("Game Metrics")]
    public float gameTime = 180f;   // 3 minutes in seconds
    [Range(0, 100f)]
    public float reliability = 100f;
    [Range(0, int.MaxValue)]
    public int popularity = 0;
    private float _timer;     // Timer 
    
    [Header("Rounds")]
    public int currentRound = 0;
    public List<NewsData> currentNewsList;
    public List<NewsData> selectedNewsList;
    private float _roundTimer;
    private float roundTimeBonus = 20.0f;
    private int correctPoints = 1;
    private int incorrectPoints = -3; // negative
    private float popularityFallingTimer = 0f;

    public HighscoreManager highscoreManager;
    public HighScoreUI highscoreUI;
    
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

        uiManager.OnGameTimeEnded += HandleGameTimeEnded;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.PlayingRound)
        {
            _timer -= Time.deltaTime;
            _roundTimer += Time.deltaTime;
            
            popularityFallingTimer += Time.deltaTime;
            if (popularityFallingTimer >= 1f)
            {
                if (popularity > 0)
                {
                    print("Minus one follower");
                    popularity--;
                    uiManager.UpdateFollowers(popularity);
                }
                popularityFallingTimer = 0f;
            }
            if (_timer <= 0f)
            {
                // End game
                EndGame(true);
            }
        }
    }

    public void OnCaptchaCompleted()
    {
        StartRound();
    }

    void StartRound()
    {
        currentGameState = GameState.PlayingRound;
        currentRound++;
        _roundTimer = 0.0f;
        
        // Get news to check on new round
        currentNewsList = NewsManager.Instance.GetNewsForRound(currentRound);
        
        // Clean selectes news for submit on new round
        selectedNewsList = new List<NewsData>();

        uiManager.ReceiveNewsList(currentNewsList);
    }

    public void SubmitRound()
    {
        currentGameState = GameState.RoundEnded;

        int followersToAdd = 0;
        float credibilityToAdd = 0;
        
        int correctNews = 0;
        int incorrectNews = 0;

        
        selectedNewsList = uiManager.postListData;
        print("selected news size" + selectedNewsList.Count);
        foreach (var news in selectedNewsList)
        {
            if (news.isReal) correctNews++;
            else incorrectNews++;
            followersToAdd += news.followersGained;
            credibilityToAdd += news.credibilityGained;
            print("followers: " + followersToAdd + ", credibility: " + credibilityToAdd);
        }
        
        reliability += credibilityToAdd;
        if (reliability >= 100f) reliability = 100f;
        
        // // If close to 0 it was very fast round, if close to 1 it took exactly roundTimeBonus, if bigger than 1 then it took to long, so no bonus
        // float timeUsedFraction = _roundTimer / roundTimeBonus;
        // // Invert timeUsedFraction to get a high bonus in case player took a short time, and a small bonus if player took long time
        // float invertedTimeUsedFraction = 1.0f - timeUsedFraction;
        // // Clamp the value between 0 and 1 to take the bonus
        // float speedBonus = Mathf.Clamp(invertedTimeUsedFraction, 0.1f, 1.0f);
        // // Popularity is based on incorrectNews and the factor of bonus (Clickbait makes people more morbid)
        // int followers = Mathf.RoundToInt(followersToAdd * speedBonus);
        
        popularity += followersToAdd;

        if (reliability <= 0f)
        {
            EndGame(false);
        } else {
            // Start new round after 1 second
            Invoke(nameof(StartRound), 1f);
            selectedNewsList.Clear();
        }

        uiManager.EndRound(popularity, reliability);
    }
    
    void EndGame(bool win)
    {
        currentGameState = win ? GameState.GameWon : GameState.GameOver;
        
        uiManager.ShowEndGamePanel(win);

        // After 10 seconds, show scoreboardPanel
        string playerName = "Player"; // Aquí podrías pedir nombre con un input
        HighscoreElements newScore = new HighscoreElements(playerName, popularity);
        highscoreManager.AddNewHighscore(newScore);
        print("Mostrando panel scoreboard");
        // Esperar y mostrar el panel con lista actualizada
        Invoke(nameof(highscoreUI.ShowPanel), 3f);

        
        Time.timeScale = 0f;
    }

    void ShowScoreboard()
    {
        List<HighscoreElements> scores = highscoreManager.GetHighscores();
        Console.WriteLine("scores size es " + scores.Count);
        highscoreUI.ShowPanel();
        highscoreUI.UpdateUI(scores);
    }

    // Add news from prompt list to post list
    public void addPromptToPostList(NewsData news)
    {
        currentNewsList.Remove(news);
        selectedNewsList.Add(news);
    }

    private void HandleGameTimeEnded()
    {
        if (currentGameState == GameState.PlayingRound)
        {
            EndGame(true);
        }
    }
}
