using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody enemyRB;
    GameObject player;
    Transform[] walls;
    public GameObject wallsPrefab;
    GameManager gameManager;
    SoundManager soundManager;
    float speed = 55f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        walls = wallsPrefab.GetComponentsInChildren<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
            if(player != null)
                enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
    }

    //to check if enemy collided with player impulse
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Impulse"))
        {
            Debug.Log("Collided with: " + other.gameObject.name);
            soundManager.HitSound();
            enemyRB.velocity = Vector3.zero;
            enemyRB.AddForce((other.transform.forward * 50), ForceMode.Impulse);
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            Transform closestWall = walls[0];

            //find the closest wall
            for (int i = 0; i < walls.Length; i++)
            {
                if (Vector3.Distance(closestWall.transform.position, transform.position) < Vector3.Distance(walls[i].transform.position, transform.position))
                    continue;
                else
                    closestWall = walls[i];
            }

            //push the ball towards the closest wall
            enemyRB.AddForce((closestWall.transform.position - transform.position) * speed);
            Debug.Log("pushed towards wall: " + closestWall.gameObject.name);

            Destroy(gameObject);
            Debug.Log("Enemy destroyed from player");
            soundManager.DestroyedSound();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("Enemy Destroyed from wall");
            soundManager.DestroyedSound();

            //add score
            gameManager.AddScore();
        }
    }
}
