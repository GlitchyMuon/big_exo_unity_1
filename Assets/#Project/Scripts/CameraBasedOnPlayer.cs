using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]

public class CameraBasedOnPlayer : MonoBehaviour
{
    private Transform cameraTransform;

    private float speed;

    public float offsetZ = -6f;

    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<PlayerControl>().speed;
        cameraTransform = Camera.main.transform;
        //variante par rapport à la ligne juste au dessus
        //cubeTransform = GameObject.FindWithTag("Player").transform;
        //Vector3 position = transform.position
        position = cameraTransform.position;    //copie de la position donc modifiable
        position.z = transform.position.z + offsetZ;
        cameraTransform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        //suite de variante :
        //Vector3 position = transform.position
        Vector3 position = cameraTransform.position;
        position.z = transform.position.z + offsetZ;
        //si on utilise pas les deux lignes suivantes pour rendre le mouvement plus smooth :
        //cameraTransform.position = position;
        Vector3 direction = (position - cameraTransform.position).normalized;
        cameraTransform.position += direction * speed * Time.deltaTime;
        


        //On ne peut pas explicitement changer un Vector3 !!!
        //Ma tentative de résolution
        // Vector3 position = transform.position;
        // position.z = -3f;
        // transform.position = position;
        // Vector3 newPosition = (target.position.x, target.position.y, (target.position.z - position.z));
        // newPosition = newPosition.normalized;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Ground"))
        {
            cameraTransform.position = position;
        }
    }
}
