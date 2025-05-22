using UnityEngine;
using Oculus.Platform; // Optional if you use Oculus Platform SDK
using Oculus.Interaction; // Optional if using Interaction SDK

public class ExampleUsageQuest3 : MonoBehaviour
{
    public MetaPassthroughFade passthroughFade;

    void Update()
    {
        // Press A button on right controller to fade out passthrough
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            passthroughFade.FadeOut();
        }

        // Press B button on right controller to fade in passthrough
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            passthroughFade.FadeIn();
        }
    }
}
