using System.Collections;
using System.Collections.Generic;
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
        Target = Vector2.Lerp(v1, v2, normDist);
        //transform.position =  transform.position*0.3f + (Vector3)target*0.7f;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * 10f);
    }
}
