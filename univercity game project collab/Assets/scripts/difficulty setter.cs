using System;
using UnityEngine;
using UnityEngine.UI;

public class difficultysetter : MonoBehaviour
{
    [SerializeField] private int difficulty;
    [SerializeField] private Sprite active, notActive;

    public void setDifficulty()
    {
        PlayerPrefs.SetInt("diff", difficulty);
    }

    private void Update()
    {
        if (difficulty == PlayerPrefs.GetInt("diff", 1))
        {
            this.GetComponent<Image>().color = new Color(0f/255f, 255f/255f, 0f/255f, 255f/255f);
        }
        else
        {
            this.GetComponent<Image>().color = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);
        }
    }
}
