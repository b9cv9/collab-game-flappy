using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = ScoreManager.instance.GetScore().ToString();
            ScoreManager.instance.ResetScore();
        }
    }
}
