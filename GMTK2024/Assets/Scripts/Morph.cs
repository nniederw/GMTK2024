using System;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Morph : MonoBehaviour
{
    [SerializeField] private Sprite SmallCarp; 
    [SerializeField] private Sprite BigCarp;
    //[SerializeField] private Sprite DragonCarp;
    private void Start()
    {
        if (SmallCarp == null) throw new System.Exception($"{nameof(SmallCarp)} was null in {nameof(Morph)}");
        if (BigCarp == null) throw new System.Exception($"{nameof(BigCarp)} was null in {nameof(Morph)}");
    }
    public void ChangeToBig()
    {
        throw new NotImplementedException();
    }
    void Update()
    {
        
    }
}