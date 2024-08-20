using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class WaterSurface : MonoBehaviour
{
    [SerializeField] private float GravityScale = 3.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inv = collision.gameObject.GetComponent<Inventory>();
        if(inv != null && !inv.MorphedToDragon)
        {
            var rig = inv.GetComponent<Rigidbody2D>();
            if(rig !=null)
            {
                rig.gravityScale = GravityScale;
            }
        }
        else
        {
            var ab = collision.gameObject.GetComponent<AsteroidBody>();
            if(ab != null)
            {
                ab.OnHittingWaterSurface();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var inv = collision.gameObject.GetComponent<Inventory>();
        if (inv != null)
        {
            var rig = inv.GetComponent<Rigidbody2D>();
            if (rig != null)
            {
                rig.gravityScale = 0f;
            }
        }
    }
}