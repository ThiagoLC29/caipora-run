using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject pause;
    public GameObject resume;

    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(false);
        resume.SetActive(true);

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pause.SetActive(true);
        resume.SetActive(false);
    }
}
