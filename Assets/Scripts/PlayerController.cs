using System;
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
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCameraThirdPerson;
    private float score = 0;
    [SerializeField]
    private float turnTime = 10;
    private float playTimer=0;
    
    public static event Action<float> AddScore;
    public static event Action OnDied;

    private void Awake()
    {
        initialWalkLength = allowedWalkedLengthPerTurn;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (HasTurn)
        {
            float time = Time.deltaTime;
            playTimer += time;
            score += time;
            if (playTimer>=turnTime)
            {
                EndPlayerTurn();
            }
            AddScore(score);
            //Debug.Log(score);
            bool canMove = CanMove();
            if (canMove)
            {
                Move();
            }

            Rotate();
            if (Input.GetButtonDown("Fire2"))
            {
                virtualCameraThirdPerson.Priority = 100;
                
            }
            if (Input.GetButtonUp("Fire2"))
            {
                virtualCameraThirdPerson.Priority = 1;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                shootEgg.Shoot();
                EndPlayerTurn();
            }


        }
        
        
        
    }

    private void EndPlayerTurn()
    {
        virtualCameraThirdPerson.Priority = 1;

        StartCoroutine(GameManager.Instance.EndTurn(this));
        allowedWalkedLengthPerTurn = initialWalkLength;
        score = 0;
        playTimer = 0;
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
        
        
    }

    private void Rotate()
    {
        var rotation = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationSpeed * rotation*Time.deltaTime);
    }

    public void TakeHit()
    {
        health--;
        
        if (health<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.GameEnded = true;
        Destroy(gameObject);
        OnDied();
        
    }
}
