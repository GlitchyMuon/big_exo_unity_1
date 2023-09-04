using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerControl : MonoBehaviour
{

    //public InputActions actions;

    public InputActionAsset actions;
    private InputAction jump;

    public float forceIntensity;
    public float speed = 1f;
    private InputAction xAxis;

    private PlayerInput playerInput;

    private Rigidbody rb;

    private bool isGrounded = true;


    void Awake()
    {
        xAxis = actions.FindActionMap("CubeActionsMap").FindAction("XAxis");
        jump = actions.FindActionMap("CubeActionsMap").FindAction("jump");
        //la mettre ici sauf si ça change à chaque update :
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        actions.FindActionMap("CubeActionsMap").Enable();
        //autre possibilité :
        //actions.FindActionMap("CubeActionsMap").performed += OnJump
        //on ajoute cette méthode (pas le résultat de la méthode, OnJump()) à la liste des actions à faire "performed"
        //l'action elle-même appelle la fonction quand elle en a besoin
        //cette méthode est efficace en contexte
    }

    void OnDisable()
    {
        actions.FindActionMap("CubeActionsMap").Disable();
    }

    void Update()
    {
        MoveX();
        AutoMoveZ();
        Jump();
    }

    void OnCollisionEnter(Collision collision)
    {
        //nécessaire pour la partie 3 sinon avec le collide au lieu d'un trigger, le saut ne se fera plus
        if(collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    
    }

    private void MoveX()
    {
        float xMove = xAxis.ReadValue<float>();
        transform.position += speed * Time.deltaTime * xMove * transform.right;

    }

    private void AutoMoveZ()
    {
        Vector3 direction = transform.forward;
        transform.position += direction * Time.deltaTime * speed;
    }

    private void Jump()
    //suite de la méthode de Thomas : private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            //mieux de : if force > 0 : alors il le fait sinon il va appelert la fonction dans chaque update
            if (forceIntensity > 0)
            {
                float jumpZ = jump.ReadValue<float>();
                rb.AddForce(jumpZ * Vector3.up * forceIntensity);
                Debug.Log("JUMPED");
            }
        }
    }
}
