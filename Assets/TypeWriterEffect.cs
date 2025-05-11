using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    private string fullText;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    public void StartTyping(string _fullText)
    {
        fullText = _fullText;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textMeshPro.text = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            textMeshPro.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
