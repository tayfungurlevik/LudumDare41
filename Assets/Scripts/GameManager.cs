using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerController>();
        enemies = FindObjectsOfType<Enemy>().ToList();
        StartGame();

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
        enemies.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnded)
        {
            ShowMenuPanel();
        }
    }

    private void ShowMenuPanel()
    {
        throw new NotImplementedException();
    }
}
