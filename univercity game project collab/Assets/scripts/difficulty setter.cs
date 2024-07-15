using UnityEngine;

public class difficultysetter : MonoBehaviour
{
    [SerializeField] private int difficulty;

    public void setDifficulty()
    {
        PlayerPrefs.SetInt("diff", difficulty);
    }
}
