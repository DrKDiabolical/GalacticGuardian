using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Configuration
    [Header("Player Configuration")]
    [SerializeField] int health = 400; // Defines player health.
    [SerializeField] AudioClip deathSFX; // Store reference to death SFX audio clip.
    [SerializeField] [Range(0, 1f)] float deathSFXVolume = 0.5f; // Defines death SFV volume.

    // Player Movement Configuration
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 5f; // Defines player move speed.
    float minX, maxX; // Defines minimum and maximum boundaries for player movement on X axis.
    float minY, maxY; // Defines minimum and maximum boundaries for player movement on Y axis.
    
    // Player Firing Configuration
    [Header("Player Firing")]
    [SerializeField] GameObject bulletPrefab; // Stores reference to player projectile prefab.
    [SerializeField] float projectileSpeed = 5f; // Defines player projectile move speed.
    [SerializeField] float projectileFiringPeriod = 0.5f; // Defines player contiunous fire period.
    [SerializeField] AudioClip firingSFX; // Store reference to firing SFX audio clip.
    [SerializeField] [Range(0, 1f)] float firingSFXVolume = 0.15f; // Defines firing SFV volume.
    Coroutine firingCoroutine; // Stores coroutine for firing.

    // Start is called before the first frame update
    void Start()
    {
        CreateMoveBoundaries(); // Creates boundaries based on screen.
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // Handles player movement.
        Fire(); // Handles player firing.
    }


    // Handles player movement on both axis
    void Move()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime; // Captures and stores player movement on X axis.
        float deltaY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime; // Captures and stores player movement on Y axis.

        float newX = Mathf.Clamp(transform.position.x + deltaX, minX, maxX - 1); // -1 is placed for padding
        float newY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY - 1); // the player sprite.
        transform.position = new Vector2(newX, newY);
    }

    // Handles player firing a bullet when the specified button is held down or released.
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

    // Coroutine that handles the player firing continuosly
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.9f, 0), Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(firingSFX, Camera.main.transform.position, firingSFXVolume);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed;
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    // Defines boundaries so that the player cannot escape the camera view
    void CreateMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Handles collision with an enemy projectile
    void OnTriggerEnter2D(Collider2D other) {
        DamageHandler damageHandler = other.gameObject.GetComponent<DamageHandler>();
        if (!damageHandler) { return; }
        ProcessHit(damageHandler);
    }

    // Handles removing damage.
    void ProcessHit(DamageHandler damageHandler)
    {
        health -= damageHandler.GetDamageAmount();
        damageHandler.Hit();
        // Destroys player when out of health
        if (health <= 0)
        {
            Die();
        }
    }

    // Handles player death
    void Die()
    {
        FindObjectOfType<Level>().LoadGameOver(); // Loads GameOver scene
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(gameObject);
    }
}
