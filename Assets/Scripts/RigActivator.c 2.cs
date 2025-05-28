using UnityEngine;
using UnityEngine.InputSystem;

public class CycleAnimationsWithX : MonoBehaviour
{
    [Header("Prefab With Animator")]
    [SerializeField] private GameObject animatedPrefab; // Assign your prefab here

    [Header("Animation State Names")]
    [SerializeField]
    private string[] animationStates = {
        "Eating", "GoToSleep", "Isle", "IdleSleep", "Petting"
    };

    private Animator animator;
    private int currentIndex = 0;

    void Start()
    {
        if (animatedPrefab == null)
        {
            Debug.LogError("Animated Prefab is not assigned.");
            return;
        }

        animator = animatedPrefab.GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on prefab.");
        }
    }

    void Update()
    {
        // Detect Meta Quest 3 "X" button press on the left controller
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            TriggerAnimation();
        }
        else if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.LTouch))
        {
            // Button.Three = X button on left Meta controller
            TriggerAnimation();
        }
    }

    void TriggerAnimation()
    {
        if (animator != null && animationStates.Length > 0)
        {
            string nextState = animationStates[currentIndex];
            animator.Play(nextState);
            currentIndex = (currentIndex + 1) % animationStates.Length;
        }
    }
}
