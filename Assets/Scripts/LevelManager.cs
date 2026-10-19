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

    //reload scene when health is lost but not 0

    public void YouWin()
    {
        youWin.SetActive(true);
    }
}
