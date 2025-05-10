using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UINewsBehaviour : MonoBehaviour
{
    //Manager for changing news from promp to posts
    private NewsDragger newsDragger;

    private NewsData newsData;

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
        //TODO: Demonstrar si era de verdad o de mentira
        if(isReal)
        {
            headline.color = Color.green;
        }
        else
        {
            headline.color = Color.red;
        }
    }

    public void ChangeColumns()
    {
        newsDragger.AddNewPost(this.gameObject);

        Debug.Log("clicking...");
    }


}
