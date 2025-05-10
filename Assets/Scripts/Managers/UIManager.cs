using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject newsPrefab;
    public GameObject prompList;

    public TextMeshProUGUI followers;
    public TextMeshProUGUI credibility;

    private List<UINewsBehaviour> actualNewsList;

    private void Awake()
    {
        actualNewsList = new List<UINewsBehaviour>();
    }

    //Instanciar por cada noticia de la lista de esta ronda una tarjeta y ponerla como hija de la promp list
    public void ReceiveNewsList(List<NewsData> newNewsList)
    {
        for (int i = 0; i < newNewsList.Count; i++)
        {
            GameObject newPromptPrefab = Instantiate(newsPrefab);

            newPromptPrefab.transform.SetParent(prompList.transform);

            UINewsBehaviour newPrompt = newPromptPrefab.GetComponent<UINewsBehaviour>();

            newPrompt.SetInformation(newNewsList[i]);

            actualNewsList.Add(newPrompt);
        }
    }

    public void EndRound(float newFollowers, float newCredibility)
    {
        CleanPrompList();
        UpdateCounters(newFollowers, newCredibility);
    }

    //Limpiar la bandeja de promps de noticias, poniendo cuales eran verdaderas y cuales eran falsas. 
    private void CleanPrompList()
    {
        for (int i = 0; i < actualNewsList.Count; i++)
        {
            actualNewsList[i].EndRound();
        }
    }

    //Actualizar contadores de seguidores y credibilidad una vez acabes la ronda de noticias 
    private void UpdateCounters(float newFollowers, float newCredibility)
    {
        followers.text = newFollowers.ToString();
        credibility.text = newCredibility.ToString();
    }



}
