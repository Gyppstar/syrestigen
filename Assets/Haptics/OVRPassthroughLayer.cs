// Attach this script to the GameObject with OVRPassthroughLayer
using UnityEngine;
using System.Collections;

public class MetaPassthroughFade : MonoBehaviour
{
    public OVRPassthroughLayer passthroughLayer;
    public float fadeDuration = 1.0f;

    void Awake()
    {
        if (passthroughLayer == null)
        {
            passthroughLayer = GetComponent<OVRPassthroughLayer>();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeTo(1f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0f));
    }

    private IEnumerator FadeTo(float targetOpacity)
    {
        float startOpacity = passthroughLayer.textureOpacity;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            passthroughLayer.textureOpacity = Mathf.Lerp(startOpacity, targetOpacity, elapsed / fadeDuration);
            yield return null;
        }

        passthroughLayer.textureOpacity = targetOpacity;
    }
}
