using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] weapons;
    List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform player;
    [SerializeField] float nextSpawnDelay = 2f;
    [SerializeField] float nextRoundDelay = 5f;
    [SerializeField] int enemiesNumber = 2;
    [SerializeField] int enemyHealth = 1;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI loseText;
    bool isInRound = false;
    bool isSpawning = false;
    float nextRoundTime;
    int roundNumber = 1;


    // Start is called before the first frame update
    void Start()
    {
        nextRoundTime = Time.time + nextRoundDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRound)
        {
            if (!isSpawning)
            {
                if (enemies.Count == 0)
                {
                    isInRound = false;

                    nextRoundTime = Time.time + nextRoundDelay;

                    nextSpawnDelay = 1 / (0.5f * roundNumber) + .5f;
                    enemyHealth += 2;
                    enemiesNumber *= 2;
                    roundNumber++;
                    roundText.text = "Round" + Environment.NewLine + roundNumber;

                    loseText.text = "you survived " + roundNumber + " Rounds" + Environment.NewLine + Environment.NewLine + "press a(z) to play again";

                    foreach (GameObject weapon in weapons)
                    {
                        weapon.SetActive(false);
                    }
                    weapons[UnityEngine.Random.Range(0, weapons.Length - 1)].SetActive(true);
                }
                else
                {
                    foreach (GameObject enemy in enemies.ToArray())
                    {
                        if (!enemy)
                        {
                            enemies.Remove(enemy);
                        }
                    }
                }
            }
        }
        else
        {
            if (Time.time > nextRoundTime)
            {
                Debug.Log("Starting Round");
                isInRound = true;
                isSpawning = true;
                StartCoroutine(SpawnCo());
            }
        }
    }

    IEnumerator SpawnCo()
    {
        for (int i = 0; i < enemiesNumber; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);

            enemies.Add(enemy);
            
            enemy.GetComponent<EnemyHealth>().health = enemyHealth;
            enemy.GetComponent<AIDestinationSetter>().target = player;
            yield return new WaitForSeconds(nextSpawnDelay);
        }

        isSpawning = false;
        yield break; 
    }
}
