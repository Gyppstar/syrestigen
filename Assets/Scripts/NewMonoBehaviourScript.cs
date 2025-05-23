using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherOnCanvasHide : MonoBehaviour
{
    [Header("Canvas-objekt att övervaka")]
    public Canvas canvasA;
    public Canvas canvasB;

    [Header("Sceninställningar")]
    public string sceneToLoad = "NextScene";

    private bool sceneLoaded = false;

    void Update()
    {
        if (!sceneLoaded &&
            (canvasA == null || !canvasA.gameObject.activeInHierarchy) &&
            (canvasB == null || !canvasB.gameObject.activeInHierarchy))
        {
            sceneLoaded = true;
            Debug.Log("Båda canvas har försvunnit – byter scen...");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
