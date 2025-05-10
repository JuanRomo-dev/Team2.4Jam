using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject screenSpaceCanva;
    public CamaraTransitions camaraTransitionsManager;

    public void StartGame()
    {
        camaraTransitionsManager.EnterTransition();
        screenSpaceCanva.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game...");
    }

}
