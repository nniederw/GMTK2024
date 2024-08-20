using UnityEngine;
[RequireComponent(typeof(Camera))]
public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float Hysterisis = 0.3f;
    [SerializeField] private float FollowSpeed = 1f;
    [SerializeField] private Vector3 CameraTarget;
    private Camera Camera;
    private void Start()
    {
        if (Target == null) throw new System.Exception($"The {nameof(Target)} of {nameof(PlayerFollow)} was null");
        Camera = GetComponent<Camera>();
        CameraTarget = transform.position;
    }
    private void LateUpdate()
    {
        var targetPos = Target.position;
        Vector2 viewPos = Camera.WorldToViewportPoint(targetPos);
        if (!InHysterisis(viewPos))
        {
            var differenceMiddle = new Vector2(0.5f, 0.5f) - viewPos;
            var speed = differenceMiddle.magnitude - Hysterisis;
            if (speed < 0f)
            {
                Debug.Log($"Found a potential bug?");
            }
            speed *= 4f;
            speed = Mathf.Pow(speed,3);
            speed *= FollowSpeed;
            CameraTarget = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, CameraTarget, Time.deltaTime * speed);
        }
    }
    private bool InHysterisis(Vector2 viewPos)
    {
        var differenceMiddle = new Vector2(0.5f, 0.5f) - viewPos;
        return differenceMiddle.magnitude < Hysterisis;
    }
}