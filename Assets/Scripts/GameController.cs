using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    static int score = 0;
    static bool gameOver = false;

    public Sprite[] score_board_sprites;

    GameObject Ones;
    GameObject Tens;

    float gameTimer = 99f;

    //Initialization
    void Start()
    {
        Ones = GameObject.Find("Ones");
        Tens = GameObject.Find("Tens");

        if (Ones.Equals(null) || Tens.Equals(null))
        {
            Debug.LogError("Could not find Scoreboard objects");
            return;
        }
    }

    void Update()
    {
        DisplayTimeLeft();
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("GAME OVER");
            Application.Quit();
            Time.timeScale = 0;
        }
    }

    static public void Score(int scoreMultiplier)
    {
        score += scoreMultiplier;
        Debug.Log(score);
    }

    void DisplayTimeLeft()
    {
        int seconds = (int)Mathf.Round(gameTimer);
        int tens;
        int ones;

        if (seconds >= 10)
        {
            tens = (int)seconds.ToString()[0] - 48;
            ones = (int)seconds.ToString()[1] - 48;
        }
        else
        {
            tens = 0;
            ones = (int)seconds.ToString()[0] - 48;
        }

        Ones.GetComponentInChildren<SpriteRenderer>().sprite = score_board_sprites[ones];
        Tens.GetComponentInChildren<SpriteRenderer>().sprite = score_board_sprites[tens];
    }
}
