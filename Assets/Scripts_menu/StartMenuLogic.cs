using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    public void OnRightHandedClicked()
    {
        GameManager_menu.Instance.isLeftHanded = false;
        SceneManager.LoadScene("EggScene"); // byt till er nästa scen
    }

    public void OnLeftHandedClicked()
    {
        GameManager_menu.Instance.isLeftHanded = true;
        SceneManager.LoadScene("EggScene");
    }
}
