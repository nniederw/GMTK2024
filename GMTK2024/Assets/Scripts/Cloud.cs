using UnityEngine;
public class Cloud : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 2f;
    [SerializeField] private float MinX = -100f;
    [SerializeField] private float MaxX = 90f;
    private void Update()
    {
        transform.position += Vector3.right * MovementSpeed * Time.deltaTime;
        var pos = transform.position;
        if (pos.x > MaxX)
        {
            transform.position = new Vector3(MinX, pos.y, pos.z);
        }
    }
}