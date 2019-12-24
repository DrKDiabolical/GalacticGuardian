using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy Configuration
    [Header("Enemy Configuration")]
    [SerializeField] int health = 100; // Defines amount of health.
    [SerializeField] int scoreValue = 100; // Defines amount of score given upon defeat.
    [SerializeField] [Range(0, 1f)] float deathSFXVolume = 0.5f; // Defines death SFV volume.
    [SerializeField] GameObject deathVFX; // Stores death VFX particle system.
    [SerializeField] AudioClip deathSFX; // Stores death SFX audio clip.

    // Enemy Firing Configuration
    [Header("Enemy Firing")]
    [SerializeField] GameObject misslePrefab; // Stores projectile prefab.
    [SerializeField] float projectileSpeed = 5f; // Defines projectile speed. 
    [SerializeField] float shotCounter; // Defines firing time interval.
    [SerializeField] float minShotTimeVariance = 0.2f; // Defines minimum firing time variance.
    [SerializeField] float maxShotTimeVariance = 3f; // Defines maximum firing time variance.
    [SerializeField] AudioClip firingSFX; // Stores firing SFX audio clip.
    [SerializeField] [Range(0, 1f)] float firingSFXVolume = 0.15f; // Defines firing SFV volume.

    // Start is called before the first frame update.
    void Start()
    {
        shotCounter = Random.Range(minShotTimeVariance, maxShotTimeVariance);
    }

    // Update is called once per frame.
    void Update()
    {
        ShootingTimer();
    }

    // Handles firing based on a timer.
    void ShootingTimer()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minShotTimeVariance, maxShotTimeVariance);
        }
    }

    // Handles enemy firing.
    void Fire()
    {
        GameObject missle = Instantiate(misslePrefab, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(firingSFX, Camera.main.transform.position, firingSFXVolume);
        missle.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
    }

    // Handles collision and then removing damage.
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageHandler damageHandler = other.gameObject.GetComponent<DamageHandler>(); // Gets damageHandler component.
        if (!damageHandler) { return; } // Null check for damageHandler.
        ProcessHit(damageHandler);
    }

    // Handles removing damage.
    void ProcessHit(DamageHandler damageHandler)
    {
        health -= damageHandler.GetDamageAmount();
        damageHandler.Hit();
        // Destroys enemy when out of health.
        if (health <= 0)
        {
            Die();
        }
    }

    // Handles enemy death
    void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject); // Destroys enemy gameObject.
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity); // Instantiates enemy death VFX.
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume); // Plays enemy death SFX.
    }
}
