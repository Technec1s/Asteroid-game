using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 3f;
    public float dashMultiplier = 2f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;
    public Rigidbody2D rb;
    public int maxHealth = 1;

    public GameObject bulletPrefab;
    public Transform bulletStart;
    public AudioSource audioSource;

    private bool isDashing = false;
    private bool canDash = true;
    private Vector3 movement;
    private float currentSpeed;
    private int health;

    void Start()
    {
        bulletStart = transform.Find("bulletStart");
        currentSpeed = speed;
        health = maxHealth;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bullet.GetComponent<Bullet>().speed;

        audioSource.Play();

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            isDashing = true;
            canDash = false;
            Invoke("StopDash", dashDuration);
            Invoke("ResetDash", dashCooldown);
            currentSpeed = speed * dashMultiplier;
        }

        if (!isDashing)
        {
            movement = new Vector3(horizontalInput, verticalInput, 0f) * currentSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        else
        {
            movement = new Vector3(horizontalInput, verticalInput, 0f) * currentSpeed * dashMultiplier * Time.deltaTime;
            transform.Translate(movement);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void StopDash()
    {
        currentSpeed = speed;
        isDashing = false;
    }

    void ResetDash()
    {
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            health--;
            if (health <= 0)
            {
                GameObject explosion = Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation) as GameObject;
                Destroy(explosion, 1f);
                Destroy(gameObject);
            }
        }
    }
}