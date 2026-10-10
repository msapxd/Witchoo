using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class menu : MonoBehaviour
{
    public Animator fadeAnimator;
    public void StartGame()
    {
         SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The game closed.");
    }

    public void MainMenu()
    {
        fadeAnimator.Play("fadeOutToBlack");
        StartCoroutine(SwitchSceneAfterDelay());
    }


    private IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("mainMenu");
    }

}
