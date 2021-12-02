using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Managers/LevelManager", fileName = "LevelManager")]
public class LevelManager : ScriptableObject
{
    public void WinScreen()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
