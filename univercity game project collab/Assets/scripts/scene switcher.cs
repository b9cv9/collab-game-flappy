using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int sceneNumber;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNumber);
        try
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.ResetScore();
            }
            else
            {
                Debug.LogError("ScoreManager instance is null");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error while resetting score: {e}");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
