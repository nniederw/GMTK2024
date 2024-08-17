using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour, IDamagable
{
    public int Scales { get; private set; }
    [SerializeField] private float IFrameDuration = 1.0f;
    private float RunningImmunity = 0f;
    void Start()
    {

    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        RunningImmunity -= Time.fixedDeltaTime;
        RunningImmunity = Mathf.Max(0f, RunningImmunity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.gameObject.GetComponent<IPickupable>();
        if (p != null)
        {
            p.PickUp();
            Scales++;
        }
    }
    public void RecieveDamage(int damage)
    {
        if (RunningImmunity <= 0f)
        {
            Scales -= damage;
            CheckHealth();
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