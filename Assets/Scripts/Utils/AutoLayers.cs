
using UnityEngine;
using UnityEngine.Tilemaps;
public class AutoLayers : MonoBehaviour
{
    
    [SerializeField] private TilemapRenderer[] renderers;
    private void Awake()
    {
        SetOrders();
    }

    private void SetOrders()
    {
        renderers = GetComponentsInChildren<TilemapRenderer>();
        int order = 0;
        foreach (TilemapRenderer renderer in renderers)
        {
            renderer.sortingOrder = order;
            order++;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        SetOrders();
    }
#endif
}
