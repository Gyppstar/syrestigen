using UnityEngine;
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
    }
}
