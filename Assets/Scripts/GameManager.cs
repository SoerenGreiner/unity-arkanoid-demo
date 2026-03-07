using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public static GameManager instance;
    public int life = 3;
    public int powerUpCounter = 4;
    public int mainBall = 1;
    public bool resetCurrentSetting = false;
    public bool gameStarted = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
