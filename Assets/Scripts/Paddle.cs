using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameObject laserObject;
    public GameObject multiBallObject;
    public GameObject paddle;
    public GameObject ball;

    private float laserPowerUpTime = 0;
    private float laserSpawnSpeed = 180;
    public float speed;
    public Transform playArea;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.resetCurrentSetting == true)
        {
            LifeLostSetting();
        }
        if (GameManager.instance.gameStarted == false)
        {
            float firstInput = Input.GetAxis("Horizontal");
            if (firstInput != 0)
            {
                GameManager.instance.gameStarted = true;
                GameManager.instance.resetCurrentSetting = false;
            }
        }
        if (GameManager.instance.gameStarted == true)
        {
            float dir = Input.GetAxis("Horizontal");
            float newX = transform.position.x + Time.deltaTime * speed * dir;
            float playAreaSize = playArea.localScale.x * 10;
            float paddleSize = transform.localScale.x * 1;

            float maxX = 0.5f * playAreaSize - 0.5f * paddleSize;
            float clampedX = Mathf.Clamp(newX, -maxX, maxX);
            //transform.position = transform.position + new Vector3(Time.deltaTime*speed*dir, 0, 0);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            if (laserPowerUpTime != 0)
            {
                if (laserSpawnSpeed == 0)
                {
                    Shooting();
                    laserSpawnSpeed = 180;
                }
                laserSpawnSpeed--;
                laserPowerUpTime--;
            } 
        }
    }

    void LifeLostSetting()
    {
        laserPowerUpTime = 0;
        laserSpawnSpeed = 180;
        paddle.transform.position = new Vector3(0f, 0.5f, -9.5f);
        if(GameManager.instance.life == 0)
        {
            speed = 0;
        }
        
    }

    void Shooting()
    {
        if (laserPowerUpTime != 0)
        {
            Instantiate(laserObject, paddle.transform.position, laserObject.transform.rotation);
            laserPowerUpTime -= 1;
        }
    }

    void SpawnMultiball()
    {
        var ball1 = multiBallObject;
        ball1.transform.localPosition = new Vector3(paddle.transform.position.x -1,0.5f,paddle.transform.position.y -5.5f);
        Instantiate(ball1, ball1.transform.position, multiBallObject.transform.rotation);
        var ball2 = multiBallObject;
        ball2.transform.localPosition = new Vector3(paddle.transform.position.x +1, 0.5f, paddle.transform.position.y -5.5f);
        Instantiate(ball2, ball2.transform.position, multiBallObject.transform.rotation);
        var ball3 = multiBallObject;
        ball3.transform.localPosition = new Vector3(paddle.transform.position.x -1, 0.5f, paddle.transform.position.y - 6.5f);
        Instantiate(ball3, ball3.transform.position, multiBallObject.transform.rotation);
        var ball4 = multiBallObject;
        ball4.transform.localPosition = new Vector3(paddle.transform.position.x +1, 0.5f, paddle.transform.position.y - 6.5f);
        Instantiate(ball4, ball4.transform.position, multiBallObject.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LaserPowerUp")
        {
            Destroy(other.gameObject);
            laserPowerUpTime += 1800;

        }
        if (other.gameObject.tag == "MultiballPowerUp")
        {
            Destroy(other.gameObject);
            SpawnMultiball();
        }
    }
        
}
