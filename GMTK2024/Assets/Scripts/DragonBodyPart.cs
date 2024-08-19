using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragonBodyPart : MonoBehaviour
{
    [SerializeField] private float Length = 15f;
    private int Resolution = 100;
    private Vector2[] Curve;
    private Vector2 SmoothedPosition;
    private int StartIndex = 0;
    private float LastCurveDistance;
    private void Start()
    {
        Curve = new Vector2[Resolution];
        Curve[0] = transform.position;
        Curve[1] = Curve[0] + Vector2.right * Length;
        for (int i = 2; i < Resolution; i++)
        {
            Curve[i] = Curve[1];
        }
        LastCurveDistance = CalcCurveDist();
    }
    private float CalcCurveDist()
    {
        float distance = 0f;
        int index = StartIndex;
        var lastPos = Curve[index];
        index = PositiveModulo(index + 1, Resolution);
        for (int i = 0; i < Resolution - 1; i++)
        {
            var next = Curve[index];
            distance += (next - lastPos).magnitude;
            index = PositiveModulo(index + 1, Resolution);
            lastPos = next;
        }
        return distance;
    }
    private void FixedUpdate()
    {
        SmoothedPosition = SmoothedPosition * 0.3f + ((Vector2)transform.position) * 0.7f;
        if (LastCurveDistance + DistanceChangeOnAdd(SmoothedPosition) >= Length)
        {
            CurveAdd(SmoothedPosition);
            LastCurveDistance = CalcCurveDist();
        }
    }
    private void CurveAdd(Vector2 pos)
    {
        StartIndex = PositiveModulo(StartIndex - 1, Resolution);
        Curve[StartIndex] = pos;
    }
    private float DistanceChangeOnAdd(Vector2 pos)
    {
        float res = 0f;
        int ind2 = PositiveModulo(StartIndex - 1, Resolution);
        int ind1 = PositiveModulo(StartIndex - 2, Resolution);
        res -= (Curve[ind1] - Curve[ind2]).magnitude;
        res += (pos - Curve[StartIndex]).magnitude;
        return res;
    }
    public (Vector2 a, Vector2 b, float restDistance) GetCurveAtDistance(float distance)
    {
        Vector2 resA = new Vector2();
        Vector2 resB = new Vector2();
        float resRestDist = 0f;
        if (distance == 0f) return (Curve[StartIndex], Curve[StartIndex], 0f);
        float runningDist = 0f;
        int index = StartIndex;
        var lastPos = Curve[index];
        index = PositiveModulo(index + 1, Resolution);
        bool found = false;
        for (int i = 0; i < Resolution - 1; i++)
        {
            var next = Curve[index];
            index = PositiveModulo(index + 1, Resolution);
            runningDist += (next - lastPos).magnitude;
            if (runningDist >= distance)
            {
                resRestDist = runningDist - distance;
                resA = lastPos;
                resB = next;
                found = true;
                break;
            }
            lastPos = next;
        }
        if (!found) throw new System.Exception($"Couldn't find Curve Point");
        return (resA, resB, resRestDist);
    }
    void Update()
    {

    }
    private int PositiveModulo(int a, int n) => (a % n + n) % n;
}