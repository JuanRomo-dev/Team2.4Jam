using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamaraTransitions : MonoBehaviour
{
    public enum VIEWS
    {
        PC_VIEW,
        ROOM_VIEW,
        ENTRY_VIEW
    }

    public VIEWS actualView;

    Camera mainCamera;

    public float transitionDurations = 1.5f;
    public float entryTransitionDelay = 1f;

    public float roomTransitionDelay = 0.2f;

    public Transform pcViewTransform;
    public Transform roomViewTransform;
    public Transform entryViewTransform;

    // Start is called before the first frame update
    void Start()
    {
        //Set camera to init position and set the initial view
        mainCamera = Camera.main;
        mainCamera.transform.DOMove(entryViewTransform.position, 0f);

        actualView = VIEWS.ENTRY_VIEW;
    }

    public void EnterTransition()
    {
        mainCamera.transform.DOMove(pcViewTransform.position, transitionDurations).SetDelay(entryTransitionDelay);
        mainCamera.transform.DORotate(pcViewTransform.eulerAngles, transitionDurations).SetDelay(entryTransitionDelay).SetEase(Ease.InOutSine);
        actualView = VIEWS.PC_VIEW;
    }

    public void ChangeView(VIEWS viewToChange)
    {
        if (viewToChange == actualView)
            return;

        switch(viewToChange)
        {
            case VIEWS.PC_VIEW:
                mainCamera.transform.DOMove(pcViewTransform.position, transitionDurations).SetDelay(roomTransitionDelay);
                mainCamera.transform.DORotate(pcViewTransform.eulerAngles, transitionDurations).SetDelay(roomTransitionDelay).SetEase(Ease.InOutSine);
                break;

            case VIEWS.ROOM_VIEW:
                mainCamera.transform.DOMove(roomViewTransform.position, transitionDurations).SetDelay(roomTransitionDelay);
                mainCamera.transform.DORotate(roomViewTransform.eulerAngles, transitionDurations).SetDelay(roomTransitionDelay).SetEase(Ease.InOutSine);
                break;
        }

        actualView = viewToChange;

        //Change between views with transitions
        //if(actualView == VIEWS.PC_VIEW)
        //{
        //    mainCamera.transform.DOMove(roomViewTransform.position, transitionDurations).SetDelay(roomTransitionDelay);
        //}
        //else if(actualView == VIEWS.ROOM_VIEW)
        //{
        //    mainCamera.transform.DOMove(pcViewTransform.position, transitionDurations).SetDelay(roomTransitionDelay);
        //}
    }
}
