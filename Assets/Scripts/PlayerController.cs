using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,ITakeHit
{
    [SerializeField]
    private int health = 5;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    //private Animator animator;
    [SerializeField]
    private float allowedWalkedLengthPerTurn=10;
    public int Health { get { return health; }  }
    public bool HasTurn { get; set; }
    [SerializeField]
    private ShootEgg shootEgg;
    private float initialWalkLength;
    private void Awake()
    {
        initialWalkLength = allowedWalkedLengthPerTurn;
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine( GameManager.Instance.EndTurn(this));
            allowedWalkedLengthPerTurn = initialWalkLength;
        }
        if (HasTurn)
        {
            bool canMove = CanMove();
            if (canMove)
            {
                Move();
            }

            Rotate();
            if (Input.GetButtonDown("Fire1"))
            {
                shootEgg.Shoot();
            }
            
        }
        
        
        
    }

    private bool CanMove()
    {
        return allowedWalkedLengthPerTurn > 0;
    }

    private void Move()
    {
        
        var vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.forward * vertical;
        if (moveVector.sqrMagnitude>0)
        {
            allowedWalkedLengthPerTurn -= Time.deltaTime;
        }
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

    public void TakeHit()
    {
        health--;
    }
}
