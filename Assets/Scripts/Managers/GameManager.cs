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
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.PlayingRound)
        {
            _timer -= Time.deltaTime;
            _roundTimer += Time.deltaTime;
            
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

        uiManager.ReceiveNewsList(selectedNewsList);
    }

    void SubmitRound()
    {
        currentGameState = GameState.RoundEnded;
        
        int correctNews = 0;
        int incorrectNews = 0;

        foreach (var news in currentNewsList)
        {
            if (news.isReal) correctNews++;
            else incorrectNews++;
        }
        
        reliability += (correctNews * correctPoints) + (incorrectNews * incorrectPoints);
        
        // If close to 0 it was very fast round, if close to 1 it took exactly roundTimeBonus, if bigger than 1 then it took to long, so no bonus
        float timeUsedFraction = _roundTimer / roundTimeBonus;
        // Invert timeUsedFraction to get a high bonus in case player took a short time, and a small bonus if player took long time
        float invertedTimeUsedFraction = 1.0f - timeUsedFraction;
        // Clamp the value between 0 and 1 to take the bonus
        float speedBonus = Mathf.Clamp(invertedTimeUsedFraction, 0.0f, 1.0f);
        // Popularity is based on incorrectNews and the factor of bonus (Clickbait makes people more morbid)
        int followers = Mathf.RoundToInt(incorrectNews * speedBonus);
        
        popularity += followers;

        if (reliability < 0f)
        {
            EndGame(false);
        } else {
            // Start new round after 1 second
            Invoke(nameof(StartRound), 1f);
        }

        uiManager.EndRound(popularity, reliability);
    }
    
    void EndGame(bool win)
    {
        currentGameState = win ? GameState.GameWon : GameState.GameOver;
    }
}
