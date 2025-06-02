using UnityEngine;

public class ForceSkybox : MonoBehaviour
{
    public Material skyboxMaterial;

    void Start()
    {
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            DynamicGI.UpdateEnvironment(); // optional but helps with ambient lighting
        }
    }
}
