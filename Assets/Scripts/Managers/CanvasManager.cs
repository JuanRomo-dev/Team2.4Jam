using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject screenSpaceCanva;
    public CamaraTransitions camaraTransitionsManager;

    public void StartGame()
    {
        camaraTransitionsManager.EnterTransition();
        screenSpaceCanva.SetActive(false);
    }

    

}
