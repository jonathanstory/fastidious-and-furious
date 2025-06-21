using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Hearts")]
    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;
    [SerializeField] Image[] hearts = new Image[3];

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    public void OnButtonClick()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Quit")
        {
            GameController.Instance.QuitGame();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Options")
            GameController.options = true;
        if (EventSystem.current.currentSelectedGameObject.name == "Continue")
        {
            GameController.paused = false;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "StartButton")
        {
            GameController.Instance.StartGame();
        }
        if (EventSystem.current.currentSelectedGameObject.name == "QuitButton")
        {
            GameController.Instance.QuitGame();
        }

    }

    private void Update()
    {
        UpdateHealth();
        UpdateScore();
    }

    void UpdateHealth()
    {
        if (GameController.playerHealth >= 1)
        {
            hearts[0].sprite = heartFull;
        }
        else
        {
            hearts[0].sprite = heartEmpty;
        }

        if (GameController.playerHealth >= 2)
        {
            hearts[1].sprite = heartFull;
        }
        else
        {
            hearts[1].sprite = heartEmpty;
        }

        if (GameController.playerHealth >= 3)
        {
            hearts[2].sprite = heartFull;
        }
        else
        {
            hearts[2].sprite = heartEmpty;
        }
    }

    void UpdateScore()
    {
        scoreText.text = "x " + GameController.score;
    }
}
