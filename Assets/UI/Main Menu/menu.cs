using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class menu : MonoBehaviour
{
    public Animator fadeAnimator;
    public void StartGame()
    {
        fadeAnimator.Play("fadeOutToBlack");
        StartCoroutine(SwitchSceneAfterDelayPlay());

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The game closed.");
    }

    public void MainMenu()
    {
        fadeAnimator.Play("fadeOutToBlack");
        StartCoroutine(SwitchSceneAfterDelayMenu());
    }


    private IEnumerator SwitchSceneAfterDelayMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("mainMenu");
    }
        private IEnumerator SwitchSceneAfterDelayPlay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SampleScene"); 
    }

}
