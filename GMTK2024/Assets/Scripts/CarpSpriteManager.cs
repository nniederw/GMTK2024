using System;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Inventory))]
public class CarpSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite SmallCarp0;
    [SerializeField] private Sprite SmallCarp1;
    [SerializeField] private Sprite SmallCarp2;
    [SerializeField] private Sprite SmallCarp3;
    [SerializeField] private Sprite SmallCarp4;
    [SerializeField] private Sprite SmallCarp5;
    //[SerializeField] private Sprite BigCarp;
    private SpriteRenderer SpriteRenderer;
    private Rigidbody2D Rigidbody;
    private Inventory Inventory;
    //private Vector2 LastVelocity;
    //private Vector2 AccelerationRunningAverage;
    //[SerializeField] private Sprite DragonCarp;
    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Inventory = GetComponent<Inventory>();
        Inventory.ListenToScalesChange(OnScalesChange);
        if (SmallCarp0 == null) throw new Exception($"{nameof(SmallCarp0)} was null in {nameof(CarpSpriteManager)}");
        if (SmallCarp1 == null) throw new Exception($"{nameof(SmallCarp1)} was null in {nameof(CarpSpriteManager)}");
        if (SmallCarp2 == null) throw new Exception($"{nameof(SmallCarp2)} was null in {nameof(CarpSpriteManager)}");
        if (SmallCarp3 == null) throw new Exception($"{nameof(SmallCarp3)} was null in {nameof(CarpSpriteManager)}");
        if (SmallCarp4 == null) throw new Exception($"{nameof(SmallCarp4)} was null in {nameof(CarpSpriteManager)}");
        if (SmallCarp5 == null) throw new Exception($"{nameof(SmallCarp5)} was null in {nameof(CarpSpriteManager)}");
        //if (BigCarp == null) throw new Exception($"{nameof(BigCarp)} was null in {nameof(Morph)}");
    }
    private void ChangeToSprite(Sprite s)
    {
        SpriteRenderer.sprite = s;
    }
    private void OnMorphToDragon()
    {
        enabled = false;
    }
    private void OnScalesChange()
    {
        var scales = Inventory.Scales;
        if (!Inventory.MorphedToDragon)
        {
            switch (scales)
            {
                case 0: ChangeToSprite(SmallCarp0); break;
                case 1: ChangeToSprite(SmallCarp1); break;
                case 2: ChangeToSprite(SmallCarp2); break;
                case 3: ChangeToSprite(SmallCarp3); break;
                case 4: ChangeToSprite(SmallCarp4); break;
                case 5: ChangeToSprite(SmallCarp5); break;
            }
        }
    }
    private bool GoingRight()
    {
        return Vector2.Angle(Rigidbody.velocity, Vector2.left) > 90f;
    }
    private void Update()
    {
        if (!Inventory.MorphedToDragon && Rigidbody.velocity.magnitude > 0.0001f)
        {
            SpriteRenderer.flipX = !GoingRight();
        }
    }
}