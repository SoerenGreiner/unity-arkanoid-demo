using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;
    public float maxX;
    public float maxZ;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0, maxZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.resetCurrentSetting == true)
        {
            if (gameObject.name.Contains("BonusBall"))
            {
                Destroy(gameObject);
            }
        }
        if (GameManager.instance.gameStarted == true)
        {
            transform.position += velocity * Time.deltaTime;

            Vector3 position = transform.position;
            position.z = Mathf.Clamp(position.z, -13, 11);
            if(position.z == -13)
            {
                if (gameObject.name.Contains("BonusBall"))
                {
                    Destroy(gameObject);
                }
                if (!gameObject.name.Contains("BonusBall"))
                {
                    GameManager.instance.mainBall--;
                }
                if (GameManager.instance.mainBall == 0)
                {
                    GameManager.instance.life -= 1;
                    GameManager.instance.gameStarted = false;
                    GameManager.instance.resetCurrentSetting = true;

                    if (GameManager.instance.life == 0)
                    {
                        Destroy(gameObject);
                    }
                    
                    if (!gameObject.name.Contains("BonusBall"))
                    {
                        gameObject.transform.localPosition = new Vector3(0f, 1f, -7.5f);
                        GameManager.instance.mainBall = 1;
                    } 
                }               
            }
        //transform.position = transform.position + velocity * Time.deltaTime;
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            float maxDist = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z);
        }
        if (other.CompareTag("Paddle"))
        {
            float maxDist = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z);

        }
        if (other.CompareTag("Box"))
        {
            float maxDist = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z);

        }
        else if (other.CompareTag("SideCollider"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        }
        else if (other.CompareTag("BackCollider"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        GetComponent<AudioSource>().Play();
    }
}

