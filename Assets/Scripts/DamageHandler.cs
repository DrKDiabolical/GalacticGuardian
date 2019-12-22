using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] int damageAmount = 100; // Defines amount of damage.

    // Handles getting damage amount.
    public int GetDamageAmount()
    {
        return damageAmount;
    }

    // Handles destroying gameObjects.
    public void Hit()
    {
        Destroy(gameObject);
    }
}
