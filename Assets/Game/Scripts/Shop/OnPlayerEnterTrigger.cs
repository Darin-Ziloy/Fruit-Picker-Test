using UnityEngine;
using UnityEngine.Events;

public class OnPlayerEnterTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController _))
        {
            onPlayerEnter.Invoke();
        }
    }
}
