using TMPro.EditorUtilities;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject youWin;
    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    

    public void YouWin()
    {
        youWin.SetActive(true);
    }
}
