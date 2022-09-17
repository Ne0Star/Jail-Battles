using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    [System.Serializable]
    public class Enter2D : UnityEngine.Events.UnityEvent<Entity> { }

    [SerializeField] private TriggerMode triggerMode;

    [SerializeField] private Enter2D onEnter2D;

    private enum TriggerMode
    {
        Collision,
        Trigger
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggerMode == TriggerMode.Collision)
        {
            Entity e = collision.gameObject.GetComponent<Entity>();
            if (e)
                onEnter2D?.Invoke(e);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerMode == TriggerMode.Trigger)
        {
            Entity e = collision.gameObject.GetComponent<Entity>();
            if (e)
                onEnter2D?.Invoke(e);
        }
    }
}
