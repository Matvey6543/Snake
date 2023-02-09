using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private List<Transform> bones;    
    [SerializeField] private Transform bonePrefab;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    private Vector2 direction;
    private bool directionChanged;
    private int initialSize;

    private void Start()
    {
        initialSize = bones.Count;
        direction = Vector2.right;
        directionChanged = true;

        for (int i = 0; i < 100; i++)
        {
            InstantiateBone();
        }
    }

    private void Update()
    {
        ChangeDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ChangeDirection()
    {
        if (directionChanged)
        {
            if (Input.GetKeyDown(leftKey) && direction != Vector2.right)
            {
                direction = Vector2.left;
                directionChanged = false;
            }
            if (Input.GetKeyDown(rightKey) && direction != Vector2.left)
            {
                direction = Vector2.right;
                directionChanged = false;
            }
            if (Input.GetKeyDown(upKey) && direction != Vector2.down)
            {
                direction = Vector2.up;
                directionChanged = false;
            }
            if (Input.GetKeyDown(downKey) && direction != Vector2.up)
            {
                direction = Vector2.down;
                directionChanged = false;
            }
        }
    }

    private void Move()
    {
        for (int i = bones.Count - 1; i > 0; i--)
        {
            bones[i].position = bones[i - 1].position;
        }
        transform.Translate(direction);
        directionChanged = true;
    }

    private void InstantiateBone()
    {
        Transform newBone = Instantiate(bonePrefab);
        newBone.position = bones[bones.Count - 1].position;
        bones.Add(newBone);
    }

    private void ResetState()
    {
        for (int i = 1; i < bones.Count; i++)
            Destroy(bones[i].gameObject);

        bones.Clear();
        bones.Add(transform);
        transform.position = Vector2.zero;
        direction = Vector2.right;
        directionChanged = true;

        for (int i = 1; i < initialSize; i++)
            InstantiateBone();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
            InstantiateBone();
        else if (other.tag == "Obstacle")
            ResetState();
    }
}
