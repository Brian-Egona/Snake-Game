using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Reference to the BoxCollider2D that defines the area where the food can spawn
    public BoxCollider2D gridArea;

    // Called once when the game starts
    private void Start()
    {
        RandomizePosition();  // Place the food at a random position within the grid
    }

    // Randomly positions the food within the grid area
    private void RandomizePosition()
    {
        // Get the bounds of the gridArea collider
        Bounds bounds = this.gridArea.bounds;

        // Randomize the x and y coordinates within the grid bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Set the food's position to the new random location (snap to grid using Mathf.Round)
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    // Detects when the player (snake) collides with the food
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object colliding with the food is tagged "Player" (the snake)
        if (other.tag == "Player")
        {
            RandomizePosition();  // Move the food to a new random position
        }
    }
}
