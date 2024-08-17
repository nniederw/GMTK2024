using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string DashButton = "Dash";
    [SerializeField] private float MovementSpeed = 10f;
    [SerializeField] private float DashForce = 10f;
    [SerializeField] private float DashCooldownS = 1f;
    private float RunningDashCooldown = 0f;
    private Rigidbody2D Rigidbody;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetButtonDown(DashButton) && RunningDashCooldown == 0)
        {
            Dash();
        }
    }
    private void Dash()
    {
        var dir = MovementVector().normalized;
        Rigidbody.AddForce(dir * DashForce, ForceMode2D.Impulse);
        RunningDashCooldown = DashCooldownS;

    }
    private Vector2 MovementVector()
    {
        Vector2 horizontal = Input.GetAxis(HorizontalAxis) * Vector2.right;
        Vector2 vertical = Input.GetAxis(VerticalAxis) * Vector2.up;
        Vector2 total = horizontal + vertical;
        if (total.magnitude > 1)
        {
            total = total.normalized;
        }
        return total;
    }
    private void FixedUpdate()
    {
        Vector2 movement = MovementVector();
        var angle = Vector2.Angle(-1 * Rigidbody.velocity, movement);
        if (angle < 30)
        {
            movement *= 2;
        }
        Rigidbody.AddForce(movement * MovementSpeed);
        RunningDashCooldown -= Time.fixedDeltaTime;
        RunningDashCooldown = Mathf.Max(0, RunningDashCooldown);
    }
}