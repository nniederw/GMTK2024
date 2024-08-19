using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class DragonMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    private readonly Vector2 SpriteAngle = new Vector2(-0.9f, 0.44f) * -1;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        

    }
    private void FixedUpdate()
    {
        var dir = Rigidbody.velocity.normalized;
        if (dir.sqrMagnitude > 0.5)
        {
            var angle = Vector2.Angle(SpriteAngle, dir);
            var cross = CrossProduct(SpriteAngle, dir);
            angle *= Mathf.Sign(cross);
            var targetRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle + 180f), Time.fixedDeltaTime * 4f);
            transform.rotation = targetRot;
        }
    }
    private float CrossProduct(Vector2 v1, Vector2 v2)
    {
        //TODO check if thats the real name
        return v1.x * v2.y - v1.y * v2.x;
    }
}