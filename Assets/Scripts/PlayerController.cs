using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;
    Renderer playerRenderer;
    GameManager gameManager;
    GameObject impulse;
    SoundManager soundManager;
    Color playerColor;
    Vector2 turn;
    float speed = 35f;
    bool hasChangedColor;
    float time;
    float spamTime;
    float impulseTime;
    // Start is called before the first frame update
    void Start()
    {
        hasChangedColor = false;
        playerRB = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        playerColor = playerRenderer.material.color;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        impulse = GameObject.FindWithTag("Impulse");
        impulse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //change back color to default after half a second
        if (hasChangedColor)
            time += Time.deltaTime;
        if (time >= 0.5)
        {
            playerRenderer.material.color = playerColor;
            time = 0;
        }

        //player movement
        playerRB.AddForce(Vector3.right * speed * Input.GetAxis("Horizontal"));
        playerRB.AddForce(Vector3.forward * speed * Input.GetAxis("Vertical"));

        //player rotation on mouse input
        turn.y += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, turn.y, 0);

        //enable collider on mouse button down and disable on mouse button up
        spamTime += Time.deltaTime;
        if (Input.GetMouseButton(0) && spamTime > 1)
        {
            impulse.SetActive(true);
            spamTime = 0;
        }
        if (Input.GetMouseButtonUp(0))
            impulse.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerRenderer.material.color = Color.red;
            hasChangedColor = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            soundManager.DestroyedSound();

            //game over here
            gameManager.GameOver();
        }
    }
}
