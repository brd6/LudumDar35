using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {

    public float spawnRepeat = 2f;
    public float spawnTime = 1.5f;

    public GameObject[] enemys;

    // Use this for initialization
    void Start () {
        InvokeRepeating("addEnemy", spawnTime, spawnTime);

    }

    public void addEnemy()
    {

        // Variables to store the X position of the spawn object
        // See image below
        var x1 = transform.position.x - GetComponent<BoxCollider2D>().bounds.size.x;
        var x2 = transform.position.x + GetComponent<BoxCollider2D>().bounds.size.x;

        // Randomly pick a point within the spawn object
        var spawnPoint = new Vector3(Random.Range(x1, x2), transform.position.y, transform.position.z);
        // Create an enemy at the 'spawnPoint' position
        Instantiate(enemys[Random.Range(0, enemys.Length - 1)], spawnPoint, Quaternion.identity);
    }

}
