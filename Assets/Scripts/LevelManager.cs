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
        //need to add once we have trap spawner code
        youWin.SetActive(true);
    }
}
