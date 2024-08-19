using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Hedgehog : MonoBehaviour
{
    [SerializeField] private float PushForce = 4f;
    [SerializeField] private Sprite HappyHedgehog;
    [SerializeField] private Sprite AngryHedgehog;
    private SpriteRenderer SpriteRenderer;
    private void Start()
    {
        if (HappyHedgehog == null) throw new System.Exception($"{nameof(HappyHedgehog)} was null in {nameof(Hedgehog)}");
        if (AngryHedgehog == null) throw new System.Exception($"{nameof(AngryHedgehog)} was null in {nameof(Hedgehog)}");
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var d = collision.gameObject.GetComponent<IDamagable>();
        if (d != null)
        {
            d.RecieveDamage(1);
            SpriteRenderer.sprite = AngryHedgehog;
        }
        var rig = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rig != null)
        {
            var dir = collision.transform.position - transform.position;
            dir = dir.normalized * PushForce;
            rig.AddForce(dir, ForceMode2D.Impulse);
        }
    }
}