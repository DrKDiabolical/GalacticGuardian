using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // Configuration
    [SerializeField] float backgroundScrollingSpeed = -0.5f; // Defines background scroll speed.
    Vector2 offset; // Defines material offset.

    // Cached References
    Material material; // Contains material reference.

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material; // Gets material from renderer component.
        offset = new Vector2(0, backgroundScrollingSpeed); // Sets offset for scrolling.
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime; // Scrolls background using offset.
    }
}
