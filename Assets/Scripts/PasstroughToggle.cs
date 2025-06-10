using UnityEngine;

public class OVRPassthroughToggle : MonoBehaviour
{
    private bool passthroughEnabled = true;
    private OVRManager ovrManager;
    private bool lastButtonState = false;

    void Start()
    {
        ovrManager = Object.FindFirstObjectByType<OVRManager>();
        if (ovrManager == null)
        {
            Debug.LogError("OVRManager not found in scene.");
        }
    }

    void Update()
    {
        if (ovrManager == null) return;

        bool yButtonPressed = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch);

        // Toggle once per press
        if (yButtonPressed && !lastButtonState)
        {
            TogglePassthrough();
        }

        lastButtonState = yButtonPressed;
    }

    private void TogglePassthrough()
    {
        passthroughEnabled = !passthroughEnabled;
        ovrManager.isInsightPassthroughEnabled = passthroughEnabled;

        Debug.Log("Passthrough set to: " + passthroughEnabled);
    }
}
