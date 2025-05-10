using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CamaraTransitions camaraTransitionsManager;

    public void Update()
    {
        if (camaraTransitionsManager.actualView == CamaraTransitions.VIEWS.ENTRY_VIEW)
            return;

        if (GameManager.Instance.currentGameState == GameState.Start)
            return;

        if(Input.GetKeyDown(KeyCode.A))
        {
            camaraTransitionsManager.ChangeView(CamaraTransitions.VIEWS.ROOM_VIEW); 
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            camaraTransitionsManager.ChangeView(CamaraTransitions.VIEWS.PC_VIEW);
        }
    }
}
