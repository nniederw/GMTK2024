using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class WaterSurface : MonoBehaviour
{
    [SerializeField] private float GravityScale = 3.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var m = collision.gameObject.GetComponent<Morph>();
        if(m != null && !m.CanFly())
        {
            var rig = m.GetComponent<Rigidbody2D>();
            if(rig !=null)
            {
                rig.gravityScale = GravityScale;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var m = collision.gameObject.GetComponent<Morph>();
        if (m != null)
        {
            var rig = m.GetComponent<Rigidbody2D>();
            if (rig != null)
            {
                rig.gravityScale = 0f;
            }
        }
    }
}