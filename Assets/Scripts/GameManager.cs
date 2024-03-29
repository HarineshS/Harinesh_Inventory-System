using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    } 

    public void exit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
}
