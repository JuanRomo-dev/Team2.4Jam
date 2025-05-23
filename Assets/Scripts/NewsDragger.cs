using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewsDragger : MonoBehaviour
{
    public GameObject postsNewsScrollGameObject;

    public UIManager uiManager;

    public GameObject largeImagePanel;
    public Image largeImage;

    public void Start()
    {
        uiManager = GetComponent<UIManager>();

        if(postsNewsScrollGameObject == null)
        {
            Debug.Log("Add the post list GO to the UI MANAGER");
        }
    }

    public void ToggleImageView()
    {
        bool isActive = largeImagePanel.activeSelf;
        largeImagePanel.SetActive(!isActive);
    }

    public void EnlargeImage(Image image)
    {
        largeImage.sprite = image.sprite;
    }

    public void AddNewPost(GameObject prompToAdd)
    {
        if(prompToAdd != null && postsNewsScrollGameObject != null)
        {
            prompToAdd.transform.SetParent(postsNewsScrollGameObject.transform);
            prompToAdd.transform.SetSiblingIndex(0);

            uiManager.ChangePrompToPost(prompToAdd.GetComponent<UINewsBehaviour>());
        }
        else
        {
            Debug.Log("Missing references!");
        }
    }

}
