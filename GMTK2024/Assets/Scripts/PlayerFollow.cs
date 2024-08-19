using System.Collections;
using System.Collections.Generic;
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
        var viewPos = Camera.WorldToViewportPoint(targetPos);
        if (!InHysterisis(viewPos))
        {
            //CameraTarget = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            var v = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            CameraTarget = Vector3.Lerp(v, CameraTarget,Time.deltaTime*FollowSpeed);
        }
        transform.position = Vector3.Slerp(transform.position, CameraTarget, Time.deltaTime * FollowSpeed);
    }
    private bool InHysterisis(Vector2 viewPos)
    {
        var differenceMiddle = new Vector2(0.5f, 0.5f) - viewPos;
        return Mathf.Abs(differenceMiddle.x) < Hysterisis && Mathf.Abs(differenceMiddle.y) < Hysterisis;
    }
}