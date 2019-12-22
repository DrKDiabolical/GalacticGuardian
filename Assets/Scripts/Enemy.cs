using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100; // Defines amount of health.

    // Start is called before the first frame update.
    void Start()
    {
        
    }

    // Update is called once per frame.
    void Update()
    {
        
    }

    // Handles collision and then removing damage.
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageHandler damageHandler = other.gameObject.GetComponent<DamageHandler>();
        ProcessHit(damageHandler);
    }

    // Handles removing damage.
    private void ProcessHit(DamageHandler damageHandler)
    {
        health -= damageHandler.GetDamageAmount();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
