using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewsDragger : MonoBehaviour
{
    public GameObject postsNewsList;

    public void Start()
    {
        if(postsNewsList == null)
        {
            Debug.Log("Add the post list GO to the UI MANAGER");
        }
    }

    public void AddNewPost(GameObject prompToAdd)
    {
        if(prompToAdd != null && postsNewsList != null)
        {
            GameObject postCloned = Instantiate(prompToAdd, postsNewsList.transform);
            
            Button btn = postCloned.GetComponent<Button>();
            if(btn != null) btn.interactable = false;
            
            prompToAdd.SetActive(false);
            GameManager.Instance.addPromptToPostList(prompToAdd.GetComponent<UINewsBehaviour>().newsData);
        }
        else
        {
            Debug.Log("Missing references!");
        }
    }

}
