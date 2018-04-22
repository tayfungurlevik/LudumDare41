using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool GameEnded { get; internal set; }

    public PlayerController player;
    public  List<Enemy> enemies;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Enemy enemyPrefab;
    private CinemachineTargetGroup cinemachineTargetGroup;
    
    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();
        PlayerController.OnDied += PlayerController_OnDied;
        enemies = FindObjectsOfType<Enemy>().ToList();
        cinemachineTargetGroup = FindObjectOfType<CinemachineTargetGroup>();
        StartGame();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnDestroy()
    {
        PlayerController.OnDied -= PlayerController_OnDied;
    }
    private void PlayerController_OnDied()
    {
        SceneManager.LoadScene(0);
    }

    private void StartGame()
    {
        player.HasTurn = true;
        foreach (var npc in enemies)
        {
            npc.HasTurn = false;
        }
    }
    public IEnumerator NextTurn(Enemy enemy)
    {
        int index = enemies.IndexOf(enemy);
        enemies[index].HasTurn = false;
        if (index<enemies.Count-1)
        {
            enemies[index + 1].HasTurn = true;
        }
        else
        {
            player.HasTurn = true;
        }
        yield return new WaitForSeconds(2);
        
    }
    public IEnumerator EndTurn(PlayerController player)
    {
        player.HasTurn = false;
        enemies.First().HasTurn = true;
        SpawnNewEnemy();
        yield return new WaitForSeconds(2);
    }

    private void SpawnNewEnemy()
    {
        int randomSpawnPoint=UnityEngine.Random.Range(0, spawnPoints.Length);
        var enemy= Instantiate(enemyPrefab, spawnPoints[randomSpawnPoint].position,Quaternion.identity);
        var targets = cinemachineTargetGroup.m_Targets.ToList();
        CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
        target.target = enemy.transform;
        target.weight = 1;
        target.radius = 3;
        targets.Add(target);
        cinemachineTargetGroup.m_Targets = targets.ToArray();
        enemies.Add(enemy);
    }

}
