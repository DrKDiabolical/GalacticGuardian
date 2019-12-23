using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCleaner : MonoBehaviour
{
    // Whenever a object enters the collider, it is destoried.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }   
    }
}
