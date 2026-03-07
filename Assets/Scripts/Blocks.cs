using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public GameObject laserPowerUp;
    public GameObject multiballPowerUp;

    private int redBlockLife = 1;
    private int blueBlockLife = 2;
    private int yellowBlockLife = 3;
    private bool spawnPowerUp = false;

    private void SpawnRandomPowerUp()
    {
        if (spawnPowerUp == true)
        {
            float rng = Random.Range(1.1f, 2.9f);

            if (rng < 2)
            {
                Vector3 spawnPosition = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
                Instantiate(laserPowerUp, spawnPosition, laserPowerUp.transform.rotation);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
                Instantiate(multiballPowerUp, spawnPosition, multiballPowerUp.transform.rotation);
            }

            spawnPowerUp = false;
            GameManager.instance.powerUpCounter = 4;
        }

    }

    void SetupBoxes()
    {
        if (GameManager.instance.powerUpCounter == 0)
        {
            spawnPowerUp = true;
        }
        if (this.gameObject.name.Contains("GreenBlock"))
        {
            Destroy(gameObject);
            SpawnRandomPowerUp();
            GameManager.instance.powerUpCounter--;
            GameManager.instance.score += 10;
        }
        if (this.gameObject.name.Contains("RedBlock"))
        {
            if (redBlockLife == 0)
            {
                Destroy(gameObject);
                SpawnRandomPowerUp();
                GameManager.instance.powerUpCounter--;
                GameManager.instance.score += 20;
            }
            redBlockLife--;
        }
        if (this.gameObject.name.Contains("BlueBlock"))
        {
            if (blueBlockLife == 0)
            {
                Destroy(gameObject);
                SpawnRandomPowerUp();
                GameManager.instance.powerUpCounter--;
                GameManager.instance.score += 30;
            }
            blueBlockLife--;
        }
        if (this.gameObject.name.Contains("YellowBlock"))
        {
            if (yellowBlockLife == 0)
            {
                Destroy(gameObject);
                SpawnRandomPowerUp();
                GameManager.instance.powerUpCounter--;
                GameManager.instance.score += 40;
            }
            yellowBlockLife--;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Ball")
        {
            SetupBoxes();
        }
        else if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            SetupBoxes();
        }
    }
}
