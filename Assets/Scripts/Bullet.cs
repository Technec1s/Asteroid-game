using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int maxAmmo = 15;
    public int currentAmmo = 10;
    public float lifespan = 2f;
    float lifeTimer = 0f;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}

