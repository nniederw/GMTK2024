using System;
using System.Collections.Generic;
using UnityEngine;
public class AsteroidBody : MonoBehaviour
{
    private Asteroid Parent;
    private void Start()
    {
        Parent = transform.parent.GetComponent<Asteroid>();
        if (Parent == null) throw new Exception($"Parent didn't have component {nameof(Asteroid)}");
    }
    public void OnHittingWaterSurface()
    {
        Destroy(Parent.gameObject);
    }
}