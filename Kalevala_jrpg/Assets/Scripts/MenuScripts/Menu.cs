using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Menu : MonoBehaviour
{
    public int StartScene;

    public void ExitProgram()
    {
        Debug.Log("Quit!!!!!!!!");
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(StartScene);
    }
}
