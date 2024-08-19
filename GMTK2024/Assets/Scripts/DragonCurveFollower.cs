using UnityEngine;
public class DragonCurveFollower : MonoBehaviour
{
    [SerializeField] private float PositionInCurve = 2f;
    [SerializeField] private DragonBodyPart DragonBodyPart;
    private Vector2 Target;
    private void Start()
    {
        if (DragonBodyPart == null) throw new System.Exception($"{nameof(DragonBodyPart)} was null in {nameof(DragonCurveFollower)}");
    }
    private void FixedUpdate()
    {
        var curvePoint = DragonBodyPart.GetCurveAtDistance(PositionInCurve);
        var v1 = curvePoint.a;
        var v2 = curvePoint.b;
        var normDist = curvePoint.restDistance / (v1 - v2).magnitude;
        Target = Vector2.Lerp(v2, v1, normDist);//TODO check if switching was right
        AllignWithLine(v2 - v1);
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * 10f);

    }
    private void AllignWithLine(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}