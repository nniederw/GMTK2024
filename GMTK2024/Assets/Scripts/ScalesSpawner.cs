using Unity.VisualScripting;
using UnityEngine;
public class ScalesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CollectableScalePrefab;
    private GameObject SpawnedScale = null;
    private SpriteRenderer Renderer;
    private void Start()
    {
        if (CollectableScalePrefab == null) throw new System.Exception($"{nameof(CollectableScalePrefab)} was null in {nameof(ScalesSpawner)}");
        Renderer = GetComponent<SpriteRenderer>();
        if (Renderer != null)
        {
            Renderer.enabled = false;
        }
        Spawn();
        ScalesSpawnerManager.AddSpawnLocation(this);
    }
    public void Spawn()
    {
        if (SpawnedScale != null) throw new System.Exception($"On {nameof(ScalesSpawner)}: Collectable Scale was tried to be Spawn, but previous one didn't get destroyed.");
        SpawnedScale = Instantiate(CollectableScalePrefab, transform);
    }
    public bool IsActive() => SpawnedScale != null;
}