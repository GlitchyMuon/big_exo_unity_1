using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasedOnPlayer : MonoBehaviour
{
    public Transform target;
    public Transform offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.z = -3f;
        transform.position = position;
        Vector3 newPosition = (target.position.x, target.position.y, (target.position.z - offset));
        newPosition = newPosition.normalized;
    }
}
