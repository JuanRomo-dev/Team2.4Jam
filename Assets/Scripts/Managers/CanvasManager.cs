using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    public GameObject screenSpaceCanva;

    public GameObject title;
    public GameObject buttons;

    public CamaraTransitions camaraTransitionsManager;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;
    public float delayBeforeFade = 1f;

    public void StartGame()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(delayBeforeFade) // Wait 1 second
           .AppendCallback(() => {
               title.SetActive(false);
               buttons.SetActive(false);
           })
           .AppendInterval(0.5f) // Wait 
           .Append(canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InOutSine))
           .AppendCallback(() => {
               canvasGroup.interactable = false;
               canvasGroup.blocksRaycasts = false;
               gameObject.SetActive(false);
               camaraTransitionsManager.EnterTransition();
           });
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game...");
    }

}
