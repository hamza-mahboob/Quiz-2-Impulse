using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject walls;
    float xBoundLeft = 12;
    float zBound = 2.5f;
    float startDelay = 2f;
    float delayInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(walls);
        InvokeRepeating("SpawnEnemy", startDelay, delayInterval);
    }

    // Update is called once per frame
    void Update()
    {
        //z=4-4 x=12
    }

    void SpawnEnemy()
    {
        float zPos = Random.Range(-zBound, zBound);
        Vector3 pos = new Vector3(-xBoundLeft, 0.5f, zPos);

        Instantiate(enemy, pos, enemy.transform.rotation);

        zPos = Random.Range(-zBound, zBound);
        pos = new Vector3(xBoundLeft, 0.5f, zPos);

        Instantiate(enemy, pos, enemy.transform.rotation);
    }
}
