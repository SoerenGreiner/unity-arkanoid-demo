using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        if (GameManager.instance.resetCurrentSetting == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BackCollider")
        {
            Destroy(gameObject);
        }
    }
}
