using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if(pauseMenu.activeSelf)UnPause();else Pause();
            }
        }
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Play(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    
 
}
