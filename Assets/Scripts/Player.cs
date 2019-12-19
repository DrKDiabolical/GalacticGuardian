using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration
    [SerializeField] float flightSpeed = 5f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    float minX, maxX;
    float minY, maxY;
    Coroutine firingCoroutine;

    // Cached References
    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }


    // Handles player movement on both axis
    void Move()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * flightSpeed * Time.deltaTime;
        float deltaY = Input.GetAxisRaw("Vertical") * flightSpeed * Time.deltaTime;

        float newX = Mathf.Clamp(transform.position.x + deltaX, minX, maxX - 1); // -1 is placed for padding
        float newY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY - 1); // the player sprite
        transform.position = new Vector2(newX, newY);
    }

    // Handles player firing a bullet
    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.9f, 0), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed;
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    // Creates boundaries so that the player cannot escape the camera view
    void CreateMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
