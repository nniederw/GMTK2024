using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class CollectableScale : MonoBehaviour, IPickupable
{
    public Collectable PickUp()
    {
        Destroy(gameObject);
        return Collectable.Scale;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}