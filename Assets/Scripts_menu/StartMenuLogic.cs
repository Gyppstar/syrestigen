using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    public void OnRightHandedClicked()
    {
        GameManager_menu.Instance.isLeftHanded = false;
        SceneManager.LoadScene("EggScene"); // byt till er n�sta scen
    }

    public void OnLeftHandedClicked()
    {
        GameManager_menu.Instance.isLeftHanded = true;
        SceneManager.LoadScene("EggScene");
    }
}
