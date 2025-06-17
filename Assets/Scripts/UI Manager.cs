using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
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
}
