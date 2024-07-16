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
            this.GetComponent<Image>().sprite = active;
        }
        else
        {
            this.GetComponent<Image>().sprite = notActive;
        }
    }
}
