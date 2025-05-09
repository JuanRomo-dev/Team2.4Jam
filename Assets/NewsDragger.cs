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
            prompToAdd.transform.SetParent(postsNewsList.transform);
        }
        else
        {
            Debug.Log("Missing references!");
        }
    }

}
