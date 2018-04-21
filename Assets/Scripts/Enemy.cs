using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class Enemy : MonoBehaviour,ITakeHit
{
    [SerializeField]
    private int health=5;
    [SerializeField]
    private float allowedWalkedLengthPerTurn = 10;
    private PlayerController player;
    private List<Enemy> enemies;
    private GameObject target;
    [SerializeField]
    private ShootEgg shooter;
    public int Health { get { return health; } }
    
    public bool HasTurn { get; set; }
    private NavMeshAgent agent;
    private float initialWalkLength;
    private float totalWalk;
    private void Start()
    {
        gameObject.SetActive(true);
        target = null;
        player = GameManager.Instance.player;
        enemies = GameManager.Instance.enemies.Where(t=>t.gameObject!=this.gameObject&&t.gameObject.activeSelf).ToList();
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        initialWalkLength = allowedWalkedLengthPerTurn;
    }

    

    // Update is called once per frame
    void Update()
    {
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
        transform.rotation=Quaternion.LookRotation(transform.position - target.transform.position);
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
        
        Enemy enemyWithLowestHealth = enemies.Where(t=>t.gameObject.activeSelf==true).OrderBy(t => t.Health).FirstOrDefault();
        if (enemyWithLowestHealth!=null)
        {
            if (enemyWithLowestHealth.Health > player.Health || !enemyWithLowestHealth.gameObject.activeSelf)
            {
                target = player.gameObject;
            }
            else
            {
                target = enemyWithLowestHealth.gameObject;
            }
        }
        else
        {
            target = player.gameObject;
        }
        

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        yield return new WaitForSeconds(1);
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
        gameObject.SetActive(false);
        GameManager.Instance.enemies.Remove(this);
        
    }
}
