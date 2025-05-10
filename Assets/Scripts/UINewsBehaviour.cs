using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UINewsBehaviour : MonoBehaviour
{
    //Manager for changing news from promp to posts
    private NewsDragger newsDragger;

    public NewsData newsData;

    public GameObject postAddons;
    public TextMeshProUGUI followersGained;
    public TextMeshProUGUI credibilityGained;

    public Image image;
    public TextMeshProUGUI headline;
    public bool isReal;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject uiManager = GameObject.Find("UIManager");

        newsDragger = uiManager.GetComponent<NewsDragger>();

        Debug.Log(newsDragger);
    }

    public void SetInformation(NewsData _newsData)
    {
        newsData = _newsData;

        image.sprite = newsData.image;
        headline.text = newsData.headline;
        isReal = newsData.isReal;
    }

    public void EndRound()
    {
        if(isReal)
        {
            headline.color = Color.green;
        }
        else
        {
            headline.color = Color.red;
        }

        Color followerGainedColor = newsData.followersGained > 0 ? Color.green : Color.red;
        Color credibilityGainedColor = newsData.credibilityGained > 0 ? Color.green : Color.red;

        followersGained.color = followerGainedColor;
        followersGained.text = newsData.followersGained.ToString();

        credibilityGained.color = credibilityGainedColor;
        credibilityGained.text = newsData.credibilityGained.ToString();
    }

    public void ChangeColumns()
    {
        newsDragger.AddNewPost(this.gameObject);

        postAddons.SetActive(true);

        Debug.Log("clicking...");
    }


}
