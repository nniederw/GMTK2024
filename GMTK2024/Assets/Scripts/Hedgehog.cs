using UnityEngine;
public class Hedgehog : MonoBehaviour
{
    [SerializeField] private float PushForce = 4f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var d = collision.gameObject.GetComponent<IDamagable>();
        if (d != null)
        {
            d.RecieveDamage(1);
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