using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreTXT;
    public GameObject gameOverScreen;
    public GameObject scoreGameOverScreen;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTXT.text = "Score: " + score.ToString();
    }

    public void AddScore()
    {
        score += 25;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        scoreGameOverScreen.GetComponent<Text>().text = scoreTXT.text;
        gameOverScreen.SetActive(true);
    }

}
