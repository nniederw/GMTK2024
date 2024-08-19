using System;
using UnityEngine;
public class Inventory : MonoBehaviour, IDamagable
{
    public int Scales { get; private set; }
    [SerializeField] private float IFrameDuration = 1.0f;
    private float RunningImmunity = 0f;
    private event Action OnScalesChange;
    private uint ScalesToRespawn = 0;
    private float SecondsForScaleRespawn = 5f;
    private float RespawnTimer = 0f;
    public void ListenToScalesChange(Action action) { OnScalesChange += action; }
    private void FixedUpdate()
    {
        RunningImmunity -= Time.fixedDeltaTime;
        RunningImmunity = Mathf.Max(0f, RunningImmunity);
        if (RespawnTimer <= 0f)
        {
            if (ScalesToRespawn > 0 && ScalesSpawnerManager.HasSpawnableSpot())
            {
                ScalesSpawnerManager.RespawnScale();
                ScalesToRespawn -= 1;
            }
            RespawnTimer += SecondsForScaleRespawn;
        }
        RespawnTimer -= Time.fixedDeltaTime;
    }
    private void ScalesChange()
    {
        OnScalesChange?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.gameObject.GetComponent<IPickupable>();
        if (p != null)
        {
            p.PickUp();
            Scales++;
            ScalesChange();
        }
    }
    public void AddScale()
    {
        Scales++;
        ScalesChange();
    }
    public void RecieveDamage(uint damage)
    {
        if (RunningImmunity <= 0f)
        {
            Scales -= (int)damage;
            ScalesChange();
            CheckHealth();
            ScalesToRespawn += damage;

            RunningImmunity = IFrameDuration;
        }
    }
    private void CheckHealth()
    {
        if (Scales < 0)
        {
            GameManager.GameOver();
        }
    }
}