using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField]
    private int health = 5;
    [SerializeField]
    private float allowedWalkedLengthPerTurn = 10;
    private PlayerController player;

    private GameObject target;
    [SerializeField]
    private ShootEgg shooter;
    public int Health { get { return health; } }

    public bool HasTurn { get; set; }
    private NavMeshAgent agent;
    private float initialWalkLength;
    private float totalWalk;

   // public static event Action<int> HandleScore;

    private void Start()
    {
        gameObject.SetActive(true);
        target = null;
        player = GameManager.Instance.player;
        
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        initialWalkLength = allowedWalkedLengthPerTurn;
    }

    private void UIScoreManager_HandleScore(float obj)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameEnded)
        {
            return;
        }
        if (HasTurn)
        {
            agent.isStopped = true;
            StartCoroutine(FindTheTarget());

            if (CanMove())
            {

                Move();
            }
            else
            {
                AimAndShoot();
            }
        }
    }

    private void AimAndShoot()
    {
        agent.isStopped = true;
        totalWalk = 0;
        transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
        shooter.Shoot();
        allowedWalkedLengthPerTurn = initialWalkLength;
        StartCoroutine(GameManager.Instance.NextTurn(this));
    }

    private void Move()
    {
        agent.isStopped = false;
        totalWalk += Time.deltaTime;

        agent.SetDestination(target.transform.position);
    }

    private bool CanMove()
    {
        return totalWalk < allowedWalkedLengthPerTurn;
    }

    private IEnumerator FindTheTarget()
    {
        target = player.gameObject;
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        yield return new WaitForSeconds(1);
    }

    public void TakeHit()
    {
        health--;

        //HandleScore(2);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.enemies.Remove(this);

    }
}
