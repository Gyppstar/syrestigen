using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherOnCanvasHide : MonoBehaviour
{
    [Header("Canvas-objekt att �vervaka")]
    public Canvas canvasA;
    public Canvas canvasB;

    [Header("Sceninst�llningar")]
    public string sceneToLoad = "NextScene";

    private bool sceneLoaded = false;

    void Update()
    {
        if (!sceneLoaded &&
            (canvasA == null || !canvasA.gameObject.activeInHierarchy) &&
            (canvasB == null || !canvasB.gameObject.activeInHierarchy))
        {
            sceneLoaded = true;
            Debug.Log("B�da canvas har f�rsvunnit � byter scen...");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
