using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiballPowerUp : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (GameManager.instance.resetCurrentSetting == true)
        {
            Destroy(gameObject);
        }
    }
}
