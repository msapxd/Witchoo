using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public void StartGame()
    {
         SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The game closed.");
    }

}
