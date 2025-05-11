using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;
    [SerializeField] HighscoreManager highscoreManager;

    // List of gameobjects for tracking UIElements on the scoreboard
    List<GameObject> uiElements = new List<GameObject>();

    public void ShowPanel()
    {
        print("Showing scoreboard panel");
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void UpdateUI (List<HighscoreElements> elements)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            HighscoreElements element = elements[i];

            if (element != null && element.points > 0)
            {
                if (i >= uiElements.Count)
                {
                    var scoreElement = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    scoreElement.transform.SetParent(elementWrapper);

                    uiElements.Add(scoreElement);
                }

                var texts = uiElements[i].GetComponentsInChildren<Text>();
                texts[0].text = element.playerName;
                texts[1].text = element.points.ToString();
            }
        }
    }
}