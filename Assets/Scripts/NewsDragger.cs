using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewsDragger : MonoBehaviour
{
    public GameObject postsNewsScrollGameObject;

    public UIManager uiManager;

    public void Start()
    {
        uiManager = GetComponent<UIManager>();

        if(postsNewsScrollGameObject == null)
        {
            Debug.Log("Add the post list GO to the UI MANAGER");
        }
    }

    public void AddNewPost(GameObject prompToAdd)
    {
        if(prompToAdd != null && postsNewsScrollGameObject != null)
        {
            //GameObject postCloned = Instantiate(prompToAdd, postsNewsList.transform);
            
            //Button btn = postCloned.GetComponent<Button>();
            //if(btn != null) btn.interactable = false;
            
            //prompToAdd.SetActive(false);
            //GameManager.Instance.addPromptToPostList(prompToAdd.GetComponent<UINewsBehaviour>().newsData);

            prompToAdd.transform.SetParent(postsNewsScrollGameObject.transform);
            uiManager.ChangePrompToPost(prompToAdd.GetComponent<UINewsBehaviour>());
        
        }
        else
        {
            Debug.Log("Missing references!");
        }
    }

}
