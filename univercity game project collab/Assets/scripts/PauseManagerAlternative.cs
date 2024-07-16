using UnityEngine;

public class PauseManagerAlternative : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseBtn; 
    private bool isPaused = false;
    private GameObject pauseObject;

    private void Start()
    {
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseObject = GameObject.FindGameObjectWithTag("Player");

        if (pauseObject != null)
        {
            pauseObject.GetComponent<playercontroller>().enabled = false;
        }

        if (pauseBtn != null)
        {
            pauseBtn.SetActive(false); 
        }
        
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true); 
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseObject = GameObject.FindGameObjectWithTag("Player");

        if (pauseObject != null)
        {
            pauseObject.GetComponent<playercontroller>().enabled = true;
        }

        if (pauseBtn != null)
        {
            pauseBtn.SetActive(true); 
        }

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); 
        }
    }
}
