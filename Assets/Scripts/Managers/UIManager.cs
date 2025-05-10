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
    private List<UINewsBehaviour> actualNewsList;

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

    private float gameTimeInMinutes = 8 * 60; // Start at 08:00 (8 hours * 60 mins)

    private bool gameStarted = false;

    private void Awake()
    {
        actualNewsList = new List<UINewsBehaviour>();
        credibility.text = "100";
        followers.text = "0";
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        // Advance game time
        gameTimeInMinutes += Time.deltaTime * (timeAccelerationRate / 60f);

        // Clamp to 24-hour format
        int hours = ((int)gameTimeInMinutes / 60) % 24;
        int minutes = ((int)gameTimeInMinutes % 60);

        // Update UI
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

            actualNewsList.Add(newPrompt);
        }

        gameStarted = true;
    }

    public void EndRound(float newFollowers, float newCredibility)
    {
        RevealReality();
        
        UpdateCounters(newFollowers, newCredibility);
        CleanPrompList();
    }

    //Limpiar la bandeja de promps de noticias, poniendo cuales eran verdaderas y cuales eran falsas. 
    private void CleanPrompList()
    {
        for (int i = 0; i < actualNewsList.Count; i++)
        {
            actualNewsList[i].EndRound();
            Destroy(actualNewsList[i].gameObject);
        }
        actualNewsList.Clear();
    }

    private void RevealReality()
    {
        for (int i = 0; i < actualNewsList.Count; i++)
        {
            actualNewsList[i].EndRound();
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

}
