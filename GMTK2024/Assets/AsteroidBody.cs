using System;
using UnityEngine;
public class AsteroidBody : MonoBehaviour
{
    private Asteroid Parent;
    private float HitTimout = 1f;
    private float RunningHitTimeout = 0f;
    private float LiftimeAfterLeavingBound = 10f;
    private float RunningLiftime;
    public Vector2 BoundX = new Vector2(float.NegativeInfinity, float.PositiveInfinity);
    private void Start()
    {
        Parent = transform.parent.GetComponent<Asteroid>();
        if (Parent == null) throw new Exception($"Parent didn't have component {nameof(Asteroid)}");
        RunningLiftime = LiftimeAfterLeavingBound;
    }
    private void FixedUpdate()
    {
        RunningHitTimeout -= Time.fixedDeltaTime;
        RunningHitTimeout = Mathf.Max(0, RunningHitTimeout);
        if (!InBounds())
        {
            RunningLiftime -= Time.fixedDeltaTime;
            if (RunningLiftime <= 0f)
            {
                Destroy(Parent.gameObject);
            }
        }
    }
    private bool InBounds()
    {
        var posX = transform.position.x;
        return BoundX.x <= posX && posX <= BoundX.y;
    }

    public void OnHittingWaterSurface()
    {
        Destroy(Parent.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null && RunningHitTimeout <= 0f)
        {
            damagable.RecieveDamage(1);
            RunningHitTimeout += HitTimout;
        }
    }
}