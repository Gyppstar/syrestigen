using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherAfterDelay : MonoBehaviour
{
    [Header("Tidsinställningar")]
    public float delayInSeconds = 5f;

    [Header("Sceninställningar")]
    public string sceneToLoad = "NextScene";

    private bool sceneLoaded = false;
    private float timer = 0f;

    void Update()
    {
        if (sceneLoaded) return;

        timer += Time.deltaTime;

        if (timer >= delayInSeconds)
        {
            sceneLoaded = true;
            Debug.Log("Tidsgräns nådd – byter scen till: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
