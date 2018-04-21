using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    //private Animator animator;
    
    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        
        var vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.forward * vertical;
        transform.position = transform.position + moveVector * moveSpeed * Time.deltaTime;
        //var magnitude = Mathf.Abs(moveVector.magnitude);
        //if (magnitude > 0.1)
        //{
        //    animator.SetFloat("Speed", magnitude);
        //}
        //else
        //{
        //    animator.SetFloat("Speed", 0);
        //}
        
    }

    private void Rotate()
    {
        var rotation = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationSpeed * rotation*Time.deltaTime);
    }

}
