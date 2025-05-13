using UnityEngine;

public class GameManager_menu : MonoBehaviour
{
    public static GameManager_menu Instance;

    public bool isLeftHanded = false;

    private void Awake()
    {
        // Se till att endast en GameManager finns
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bevaras mellan scener
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
