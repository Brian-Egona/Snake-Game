using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Direction the snake is currently moving (initially to the right)
    private Vector2 _direction = Vector2.right;

    // List to keep track of all snake segments (head + body)
    private List<Transform> _segments = new List<Transform>();

    // Prefab for snake body segments (assigned in the Unity Inspector)
    public Transform segmentPrefab;

    // Initial number of segments the snake starts with
    public int initialSize = 4;

    // Called when the game starts
    private void Start()
    {
        ResetState();  // Initialize snake to starting state
    }

    // Called every frame to handle player input
    private void Update()
    {
        // Change direction based on key press (prevent reverse movement)
        if (Input.GetKeyDown(KeyCode.W))  // Move up
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))  // Move down
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))  // Move left
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))  // Move right
        {
            _direction = Vector2.right;
        }
    }

    // Called at regular intervals for physics updates (controls movement)
    private void FixedUpdate()
    {
        // Move each segment to the position of the one in front of it
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // Move the head of the snake in the current direction
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f  // Keep z position at 0 for 2D games
        );
    }

    // Add a new segment to the snake (called when food is eaten)
    private void Grow()
    {
        // Instantiate a new body segment at the position of the last segment
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        // Add the new segment to the list of segments
        _segments.Add(segment);
    }

    // Reset the snake to its initial state (called when hitting an obstacle)
    private void ResetState()
    {
        // Destroy all segments except the head
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        // Clear the segment list and add the head as the first segment
        _segments.Clear();
        _segments.Add(this.transform);

        // Grow the snake to its initial size
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        // Reset snake's position to the center of the screen
        this.transform.position = Vector3.zero;
    }

    // Detect collision with food or obstacles
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the snake touches food, grow by one segment
        if (other.tag == "Food")
        {
            Grow();
        }
        // If the snake hits an obstacle, reset its state
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
