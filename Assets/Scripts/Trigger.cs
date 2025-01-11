using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Event;
    public bool DestroyAfterTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovementController>(out var player))
        {
            Event.Invoke();
            if (DestroyAfterTrigger)
                Destroy(gameObject);
        }
    }
}
