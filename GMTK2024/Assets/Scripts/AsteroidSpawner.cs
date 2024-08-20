using System;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject AsteroidLeft;
    [SerializeField] private GameObject AsteroidRight;
    [SerializeField] private float SpawnTime = 6f;
    private float RunningSpawnTime = 0f;
    private bool Active = false;
    private void Start()
    {
        if (AsteroidLeft == null) throw new Exception($"{nameof(AsteroidLeft)} was null in {nameof(AsteroidSpawner)}");
        if (AsteroidRight == null) throw new Exception($"{nameof(AsteroidRight)} was null in {nameof(AsteroidSpawner)}");
    }
    private void FixedUpdate()
    {
        if (Active)
        {
            if(RunningSpawnTime <= 0f)
            {
                RunningSpawnTime += SpawnTime;
                SpawnAsteroid();
            }
            RunningSpawnTime -= Time.fixedDeltaTime;
        }
    }
    private void SpawnAsteroid()
    {
        var rng = new System.Random();
        bool left = rng.Next(0, 2) == 0;

        throw new NotImplementedException();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            Active = true;
        }
    }
}