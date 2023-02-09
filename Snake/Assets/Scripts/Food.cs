using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private BoxCollider2D spawnArea; 
    private static Food instance;
    
    public static Food GetInstance()
    {
        if (instance == null)
            instance = new Food();
        return instance;
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        transform.position = new Vector3(
            Mathf.Round(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x)),
            Mathf.Round(Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)),
            0f
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Obstacle")
            Spawn(); 
    }
}
