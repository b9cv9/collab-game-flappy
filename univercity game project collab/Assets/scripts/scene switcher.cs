using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitcher : MonoBehaviour
{
    [SerializeField] private int sceneNumber;

    public void loadScene()
    {
        SceneManager.LoadScene(sceneNumber);
        ScoreManager.instance.ResetScore();
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
