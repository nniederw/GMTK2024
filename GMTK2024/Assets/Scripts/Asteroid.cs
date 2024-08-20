using UnityEngine;
public class Asteroid : MonoBehaviour
{
    [SerializeField] private bool Left;
    [SerializeField] private float FallingSpeed;
    private readonly Vector2 LeftDirection = new Vector2(-1, -1).normalized;
    private readonly Vector2 RightDirection = new Vector2(1, -1).normalized;
    void Start()
    {

    }
    private void Update()
    {
        var dir = Left ? LeftDirection : RightDirection;
        transform.position += (Vector3)dir * FallingSpeed * Time.deltaTime;
    }
}