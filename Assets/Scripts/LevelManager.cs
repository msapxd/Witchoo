using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject youWin;
    public TrapAbstract[] trapsList;
    public Door door;
    public List<string> levels;

    void Start()
    {
        StartCoroutine(DelayedGetAllTraps());
    }

    private void OnTrapDied()
    {
        for (int i = 0; i < trapsList.Length; i++)
        {
            if (trapsList[i].isDead == false)
            {
                return;
            }
        }

        YouWin();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void LostLife()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void YouWin()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level1")
        {
            youWin.SetActive(true);
        }
        else
        {
            door.Open();
        }
    }
  
    private IEnumerator DelayedGetAllTraps()
    {
        yield return new WaitForSeconds(0.5f);
        trapsList = Object.FindObjectsByType<TrapAbstract>(FindObjectsSortMode.None);

        for (int i = 0; i < trapsList.Length; i++)
        {
            trapsList[i].OnDeath += OnTrapDied;
        }
    }
}
