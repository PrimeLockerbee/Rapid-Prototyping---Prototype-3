using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void SceneLoader(int _num)
    {
        SceneManager.LoadScene(_num);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
