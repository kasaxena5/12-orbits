using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Instance.currentScene = "GameScene";
        SceneLoader.Instance.StartScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}