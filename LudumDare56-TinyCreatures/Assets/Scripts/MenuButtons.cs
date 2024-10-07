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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play(int data)
    {
        int sceneNumber = data % 10;
        bool lockCursor = false;
        if (data >= 10)
        {
            lockCursor = true;
        }
        Cursor.visible = !lockCursor;
        Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
        SceneManager.LoadScene(sceneNumber);
    }

    public void Quit()
    {
        Application.Quit();
    }

    
 
}
