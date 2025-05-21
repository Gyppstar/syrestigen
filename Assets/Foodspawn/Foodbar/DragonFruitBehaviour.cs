using UnityEngine;

public class DragonfruitBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goylie"))
        {
            FoodManager manager = Object.FindFirstObjectByType<FoodManager>();
            if (manager != null)
            {
                manager.FeedGoylie();
            }

            Destroy(gameObject); // försvinner efter matning
        }
    }
}
