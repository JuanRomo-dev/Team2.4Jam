using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("PC VIEW ASSETS")]
    [Space(10)]
    public GameObject newsPrefab;
    public GameObject prompList;
    private List<UINewsBehaviour> actualPrompList;
    private List<UINewsBehaviour> actualPostList;
    public List<NewsData> postListData;

    [Header("ROOM VIEW ASSETS")]
    [Space(10)]
    public TextMeshProUGUI followers;
    public TextMeshProUGUI credibility;

    public Image followersArrow;
    public Image CredibilityArrow;

    private float actualFollowers;
    private float actualCredibility;

    [Header("Arrows")]
    [Space(10)]

    public Sprite arrowSUPER;
    public Sprite arrowUP;
    public Sprite arrowEQUAL;
    public Sprite arrowDOWN;
    public Sprite arrowSADGE;

    [Header("Hour")]
    [Space(10)]

    public TextMeshProUGUI hoursTxt;
    public TextMeshProUGUI minutesTxt;
    public float timeAccelerationRate = 4f;

    private float totalGameDurationRealSeconds = 180f;  // Total time of game is 3 minutes (180 seconds)
    private float elapsedTimeRealSeconds = 0f;
    private float gameTimeInMinutes = 540f; // 9 hours of game time, so 540 minutes

    private bool gameStarted = false;

    public event Action OnGameTimeEnded;

    // [Header("Game End")] 
    // public GameObject gameOverPanel;
    // public GameObject winPanel;

    private void Awake()
    {
        actualPostList = new List<UINewsBehaviour>();
        actualPrompList = new List<UINewsBehaviour>();
        postListData = new List<NewsData>();
        credibility.text = "100";
        followers.text = "0";
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        // Advance game time
        if (elapsedTimeRealSeconds < totalGameDurationRealSeconds)
        {
            elapsedTimeRealSeconds += Time.deltaTime;
        }

        float gameMinutesPassed = Mathf.Min((elapsedTimeRealSeconds / totalGameDurationRealSeconds) * gameTimeInMinutes, gameTimeInMinutes);
        
        if (elapsedTimeRealSeconds >= totalGameDurationRealSeconds)
        {
            OnGameTimeEnded?.Invoke();
        }

        int baseHour = 8;
        int totalMinutes = (int)gameMinutesPassed;
        int hours = baseHour + (totalMinutes / 60);
        int minutes = totalMinutes % 60;
        
        hours = hours % 24;

        hoursTxt.text = hours.ToString("D2");
        minutesTxt.text = minutes.ToString("D2");
    }

    //Instanciar por cada noticia de la lista de esta ronda una tarjeta y ponerla como hija de la promp list
    public void ReceiveNewsList(List<NewsData> newNewsList)
    {
        for (int i = 0; i < newNewsList.Count; i++)
        {
            GameObject newPromptPrefab = Instantiate(newsPrefab, prompList.transform);

            UINewsBehaviour newPrompt = newPromptPrefab.GetComponent<UINewsBehaviour>();

            newPrompt.SetInformation(newNewsList[i]);

            actualPrompList.Add(newPrompt);
        }

        gameStarted = true;
    }

    public void EndRound(float newFollowers, float newCredibility)
    {
        RevealReality();
        
        UpdateCounters(newFollowers, newCredibility);
        CleanPrompList();
    }

    public void ChangePrompToPost(UINewsBehaviour prompToChange)
    {
        actualPostList.Add(prompToChange);
        postListData.Add(prompToChange.newsData);
        actualPrompList.Remove(prompToChange);
    }

    //Limpiar la bandeja de promps de noticias, poniendo cuales eran verdaderas y cuales eran falsas. 
    private void CleanPrompList()
    {
        for (int i = 0; i < actualPrompList.Count; i++)
        {
            //Desactivar las tarjetas de la lista de prompts 
            actualPrompList[i].gameObject.SetActive(false);
        }

        actualPrompList.Clear();
    }

    //Call the posts list to reveal reality on REAL/FAKE
    private void RevealReality()
    {
        for (int i = 0; i < actualPostList.Count; i++)
        {
            actualPostList[i].EndRound();
        }
    }

    //Actualizar contadores de seguidores y credibilidad una vez acabes la ronda de noticias 
    private void UpdateCounters(float newFollowers, float newCredibility)
    {
        CheckCountersUpdate(newFollowers, newCredibility);
    }

    private void CheckCountersUpdate(float newFollowers, float newCredibility)
    {
        //FOLLOWERS CHECK

        if(actualFollowers == 0)
        {
            actualFollowers = newFollowers;
            followersArrow.sprite = arrowUP;
        }
        else
        {
            float deltaFollowers = newFollowers - actualFollowers;

            if (deltaFollowers >= 10)
            {
                //SUPER ARROW
                followersArrow.sprite = arrowSUPER;
            }
            else if (deltaFollowers >= 5)
            {
                //UP ARROW
                followersArrow.sprite = arrowUP;
            }
            else if (deltaFollowers <= -5)
            {
                //SADGE ARROW
                followersArrow.sprite = arrowSADGE;
            }
            else if (deltaFollowers <= 0)
            {
                //DOWN ARROW
                followersArrow.sprite = arrowDOWN;
            }
        }

        //CREDIBILITY CHECK

        if (actualCredibility == 0)
        {
            actualCredibility = newCredibility;
            CredibilityArrow.sprite = arrowDOWN;
        }
        else
        {
            float deltaCredibility = actualCredibility - newCredibility;

            if (deltaCredibility >= 10)
            {
                //SADGE ARROW
                CredibilityArrow.sprite = arrowSADGE;
            }
            else if (deltaCredibility >= 5)
            {
                //DOWN ARROW
                CredibilityArrow.sprite = arrowDOWN;
            }
            else if (deltaCredibility == 0)
            {
                //EQUAL ARROW
                CredibilityArrow.sprite = arrowEQUAL;
            }
        }

        actualFollowers = newFollowers;
        actualCredibility = newCredibility;

        followers.text = actualFollowers.ToString();
        credibility.text = actualCredibility.ToString();
    }

    public void UpdateFollowers(int newFollowers)
    {
        followers.text = newFollowers.ToString();
    }
    
    public void ShowEndGamePanel(bool win)
    {
        // if (win)
        //     winPanel.SetActive(true);
        // else
        //     gameOverPanel.SetActive(true);
    }
}
