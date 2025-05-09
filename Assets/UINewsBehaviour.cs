using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINewsBehaviour : MonoBehaviour
{
    //Manager for changing news from promp to posts
    private NewsDragger newsDragger;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject uiManager = GameObject.Find("UIManager");

        newsDragger = uiManager.GetComponent<NewsDragger>();

        Debug.Log(newsDragger);
    }

    public void ChangeColumns()
    {
        newsDragger.AddNewPost(this.gameObject);

        Debug.Log("clicking...");
        
    }
}
