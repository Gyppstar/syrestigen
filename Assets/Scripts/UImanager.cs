using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject canvasA;
    public GameObject canvasB;
    public float delayInSeconds = 2f;

    public void SwitchCanvasWithDelay()
    {
        StartCoroutine(DelayedSwitch());
    }

    private IEnumerator DelayedSwitch()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (canvasA != null)
            canvasA.SetActive(false);

        if (canvasB != null)
            canvasB.SetActive(true);

        // Optional: Wait briefly to show canvasB before switching scene
        yield return new WaitForSeconds(1f);

        // Load the next scene in the build index
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextIndex);
    }
}
