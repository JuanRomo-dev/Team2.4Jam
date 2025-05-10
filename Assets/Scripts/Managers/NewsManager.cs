using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public static NewsManager Instance { get; private set; }

    [Header("News Collection")] 
    public NewsCollection newsCollection;

    [System.Serializable]
    public class DifficultySettings
    {
        public int minNewsPerRound;
        public int maxNewsPerRound;
        [Range(0, 1)] public float fakeNewsRatio;
        public int minTierPerRound;
        public int maxTierPerRound;
    }

    public List<DifficultySettings> difficultySettingsList;
    
    private HashSet<NewsData> usedNewsSet = new HashSet<NewsData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<NewsData> GetNewsForRound(int roundNumber)
    {
        var roundParams = GetParamsForRound(roundNumber);
        
        int totalNewsOnRound = Random.Range(roundParams.minNewsPerRound, roundParams.maxNewsPerRound + 1);
        int numFakeNewsOnRound = Mathf.RoundToInt(totalNewsOnRound * roundParams.fakeNewsRatio);
        int numRealNewsOnRound = totalNewsOnRound - numFakeNewsOnRound;

        // Get fake news
        List<NewsData> fakeNewsOnRound = new List<NewsData>();
        foreach (var news in newsCollection.falseNews)
        {
            if (!usedNewsSet.Contains(news))
            {
                fakeNewsOnRound.Add(news);
            }
        }
        
        // Get real news
        List<NewsData> realNewsOnRound = new List<NewsData>();
        foreach (var news in newsCollection.trueNews)
        {
            if (!usedNewsSet.Contains(news))
            {
                realNewsOnRound.Add(news);
            }
        }
        
        // Select news for round
        List<NewsData> selectedNewsOnRound = new List<NewsData>();
        selectedNewsOnRound.AddRange(GetRandomNewsSet(realNewsOnRound, numRealNewsOnRound));
        selectedNewsOnRound.AddRange(GetRandomNewsSet(fakeNewsOnRound, numFakeNewsOnRound));
        
        // Shuffle news of round
        ShuffleNewsList(selectedNewsOnRound);
        foreach (var news in selectedNewsOnRound)
        {
            usedNewsSet.Add(news);
        }
        
        return selectedNewsOnRound;
    }

    public List<NewsData> GetRandomNewsSet(List<NewsData> newsData, int numberOfNewsOnSet)
    {
        List<NewsData> copiedList = new List<NewsData>(newsData);
        ShuffleNewsList(copiedList);
        
        List<NewsData> result = new List<NewsData>();
        int limit = Mathf.Min(numberOfNewsOnSet, copiedList.Count);
        for (int i = 0; i < limit; i++)
        {
            result.Add(copiedList[i]);
        }
        
        return result;
    }

    public void ShuffleNewsList(List<NewsData> newsData)
    {
        for (int i = 0; i < newsData.Count; i++)
        {
            int randomIndex = Random.Range(0, newsData.Count);
            NewsData temp = newsData[i];
            newsData[i] = newsData[randomIndex];
            newsData[randomIndex] = temp;
        }
    }
    
    private DifficultySettings GetParamsForRound(int roundNumber)
    {
        if (roundNumber < difficultySettingsList.Count && difficultySettingsList[roundNumber] != null)
        {
            return difficultySettingsList[roundNumber];
        }
        else
        {
            return difficultySettingsList[difficultySettingsList.Count - 1];
        }
    }

}
