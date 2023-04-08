using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] private float dragMultiplier = 2f;
    [SerializeField] private Vector2 bounds;

    private float mouseStartPos;
    private float mouseCurrentPos;
    private float playerCurrentPos;
    public float playerSpeed = 5f;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerCurrentPos = transform.localPosition.x;
        mouseStartPos = Input.mousePosition.x;
        mouseStartPos /= Screen.width;
        
    }
    
    private void FixedUpdate() 
    {
        HandLeMovement();
        rb.velocity = Vector3.forward * playerSpeed * Time.deltaTime *50;
    }


    
    void HandLeMovement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            playerCurrentPos = transform.localPosition.x;
            mouseStartPos = Input.mousePosition.x;
            mouseStartPos /= Screen.width;

        }
        if (Input.GetMouseButton(0))
        {
            mouseCurrentPos = Input.mousePosition.x;
            mouseCurrentPos /= Screen.width;
            float targetPos = playerCurrentPos + (mouseCurrentPos - mouseStartPos) * dragMultiplier;
            targetPos = Mathf.Clamp(targetPos, bounds.x, bounds.y);

            transform.localPosition = new Vector3(targetPos, transform.localPosition.y, transform.localPosition.z);
        }

    }

}