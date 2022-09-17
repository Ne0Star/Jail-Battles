using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giz : MonoBehaviour
{
    [SerializeField] private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }
    private void OnDrawGizmos()
    {if(!cam) cam = gameObject.GetComponent<Camera>();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(cam.orthographicSize * 2, cam.orthographicSize * 2));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
