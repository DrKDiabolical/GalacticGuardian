using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TMP_Text healthText; // Container for reference to health text.
    Player player; // Container for reference to player.

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TMP_Text>(); // Gets TMP text component.
        player = FindObjectOfType<Player>(); // Finds player object and stores reference to it.
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString(); // Sets text to player health value.
    }
}
