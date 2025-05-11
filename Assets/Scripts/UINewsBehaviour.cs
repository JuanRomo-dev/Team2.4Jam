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

    public Image newsBG;

    public Sprite prompBG;
    public Sprite postBG;

    public GameObject postAddons;
    public TextMeshProUGUI followersGained;
    public TextMeshProUGUI credibilityGained;

    public Image image;
    public TextMeshProUGUI headline;
    public bool isReal;

    private TypeWriterEffect writingEffect;

    // Start is called before the first frame update
    void Awake()
    {
        newsBG.sprite = prompBG;

        writingEffect = GetComponent<TypeWriterEffect>();

        GameObject uiManager = GameObject.Find("UIManager");

        newsDragger = uiManager.GetComponent<NewsDragger>();

        Debug.Log(newsDragger);
    }

    public void ClickOnImage()
    {
        newsDragger.ToggleImageView();
        newsDragger.EnlargeImage(image);
    }

    public void SetInformation(NewsData _newsData)
    {
        newsData = _newsData;

        image.sprite = newsData.image;
        headline.text = newsData.headline;
        isReal = newsData.isReal;

        writingEffect.StartTyping(newsData.headline);
    }

    public void EndRound()
    {
        if(isReal)
        {
            headline.color = new Color(28f / 255f, 120f / 255f, 49f / 255f);
        }
        else
        {
            headline.color = Color.red;
        }

        Color followerGainedColor = newsData.followersGained > 0 
            ? new Color32(28, 120, 49, 255) 
            : Color.red;

        Color credibilityGainedColor = newsData.credibilityGained > 0 
            ? new Color32(28, 120, 49, 255) 
            : Color.red;

        followersGained.color = followerGainedColor;
        followersGained.text = newsData.followersGained.ToString();

        credibilityGained.color = credibilityGainedColor;
        credibilityGained.text = newsData.credibilityGained.ToString();
    }

    public void ChangeColumns()
    {
        newsDragger.AddNewPost(this.gameObject);

        postAddons.SetActive(true);

        newsBG.sprite = postBG;

        Debug.Log("clicking...");
    }


}
