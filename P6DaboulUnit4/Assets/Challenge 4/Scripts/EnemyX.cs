using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private SpawnManagerX spawnManager;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        // FIX: Assign the goal via script to avoid NullReferenceException
        playerGoal = GameObject.Find("Player Goal");

        // BONUS: Pull the current wave's speed from SpawnManager
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        speed = spawnManager.enemySpeed;
    }

    void Update()
    {
        // Set enemy direction towards player goal and move there
        if (playerGoal != null)
        {
            Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy hits a goal or sensor, destroy it
        if (other.gameObject.name == "Enemy Goal" || other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
