using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CaptchaCheck : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TMP_InputField playerNameInput;
    public GameObject loginGameObject;

    public float fadeDuration = 1f;

    public void LoginSession()
    {
        // Hide the login screen
        loginGameObject.SetActive(false);
    }

    public void CheckCaptcha()
    {
        if (!string.IsNullOrEmpty(playerNameInput.text))
        {
            // Save data to PlayerPrefs
            PlayerPrefs.SetString("playerName", playerNameInput.text);
            PlayerPrefs.SetInt("playerPoints", 0);
            PlayerPrefs.Save();

            Debug.Log("Player saved: " + playerNameInput.text);

            // Hide the login screen
            loginGameObject.SetActive(false);

            FadeOutLogin();

            GameManager.Instance.OnCaptchaCompleted();
        }
        else
        {
            Debug.Log("Hey, enter an input");
        }

        nameText.text = "@" + playerNameInput.text;
    }
    public void FadeOutLogin()
    {
        // Asegurarse de que el panel esté activo antes de hacer fade
        if (!loginGameObject.activeSelf)
            loginGameObject.SetActive(true);

        // Obtener o agregar CanvasGroup
        CanvasGroup canvasGroup = loginGameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = loginGameObject.AddComponent<CanvasGroup>();

        // Asegurarse de que es visible e interactivo antes del fade
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Hacer fade-out
        canvasGroup.DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                loginGameObject.SetActive(false);
            });
    }

}
