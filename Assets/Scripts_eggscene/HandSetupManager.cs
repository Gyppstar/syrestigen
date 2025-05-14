using UnityEngine;

public class HandSetupManager : MonoBehaviour
{
    public GameObject handTrackingLeft;
    public GameObject handTrackingRight;

    void Start()
    {
        if (GameManager_menu.Instance.isLeftHanded)
        {
            handTrackingLeft.SetActive(true);
            handTrackingRight.SetActive(false);
        }
        else
        {
            handTrackingRight.SetActive(true);
            handTrackingLeft.SetActive(false);
        }
    }
}
