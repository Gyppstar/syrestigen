using UnityEngine;
using System.Collections;

public class HideBothCanvases : MonoBehaviour
{
    public GameObject canvasA;
    public GameObject canvasB;
    public float delayInSeconds = 2f;  // delay duration

    public void HideCanvasesWithDelay()
    {
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (canvasA != null)
            canvasA.SetActive(false);

        if (canvasB != null)
            canvasB.SetActive(false);
    }
}
