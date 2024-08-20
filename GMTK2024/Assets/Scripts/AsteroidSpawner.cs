using System;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject AsteroidLeft;
    [SerializeField] private GameObject AsteroidRight;
    [SerializeField] private float SpawnTime = 6f;
    private float RunningSpawnTime = 0f;
    private bool Active = false;
    private Transform Player;
    private Vector2 PlayerNonSpawnRegion = new Vector2(80f, 50f);
    System.Random Rng = new System.Random(69);
    private void Start()
    {
        if (AsteroidLeft == null) throw new Exception($"{nameof(AsteroidLeft)} was null in {nameof(AsteroidSpawner)}");
        if (AsteroidRight == null) throw new Exception($"{nameof(AsteroidRight)} was null in {nameof(AsteroidSpawner)}");
    }
    private void FixedUpdate()
    {
        if (Active)
        {
            if (RunningSpawnTime <= 0f)
            {
                RunningSpawnTime += SpawnTime;
                SpawnAsteroid();
            }
            RunningSpawnTime -= Time.fixedDeltaTime;
        }
    }
    private void SpawnAsteroid()
    {
        bool left = Rng.Next(0, 2) == 0;
        var prefab = left ? AsteroidLeft : AsteroidRight;
        var obj = Instantiate(prefab);
        obj.transform.position = GetRandomValidPosition();
        var ab = obj.GetComponentInChildren<AsteroidBody>();
        if (ab != null)
        {
            Vector2 pos = transform.position;
            Vector2 scale = transform.lossyScale;
            ab.BoundX = new Vector2(pos.x - scale.x / 2f, pos.x + scale.x / 2f);
        }
    }
    private Vector2 GetRandomValidPosition()
    {
        Vector2 pos = transform.position;
        Vector2 scale = transform.lossyScale;
        float minX = pos.x - scale.x / 2f;
        float maxX = pos.x + scale.x / 2f;
        float minY = pos.y - scale.y / 2f;
        float maxY = pos.y + scale.y / 2f;
        Vector2 result = Vector2.zero;
        const int MaxIterations = 100;
        int i = 0;
        do
        {
            double rand = Rng.NextDouble();
            float x = (float)(rand * (maxX - minX) + minX);
            rand = Rng.NextDouble();
            float y = (float)(rand * (maxY - minY) + minY);
            result = new Vector2(x, y);
            i++;
        } while (!IsValid(result) && i < MaxIterations);
        return result;
    }
    private bool IsValid(Vector2 pos)
    {
        Vector2 player = Player.position;
        float minX = player.x - PlayerNonSpawnRegion.x / 2f;
        float maxX = player.x + PlayerNonSpawnRegion.x / 2f;
        float minY = player.y - PlayerNonSpawnRegion.y / 2f;
        float maxY = player.y + PlayerNonSpawnRegion.y / 2f;
        return !((minX <= pos.x && pos.x <= maxX) && (minY <= pos.y && pos.y <= maxY));
    }
    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            Active = true;
            Player = pm.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            Active = false;
        }
    }
}