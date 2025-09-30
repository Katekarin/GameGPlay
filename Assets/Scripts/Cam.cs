using Unity.VisualScripting;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public float t = 0.2f;
    public Vector3 offset;

    public bool lockY = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = Vector3.Lerp(transform.position, target.position + offset, t * Time.deltaTime);

        if (lockY )
            pos.y = transform.position.y;

        //transform.position = pos + offset;
        transform.position = pos;
    }
}
