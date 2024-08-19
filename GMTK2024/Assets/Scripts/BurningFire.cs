using System.Linq;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class BurningFire : MonoBehaviour
{
    [SerializeField] Sprite[] Fires = new Sprite[0];
    [SerializeField] private float TimeForCycle = 0.5f;
    private SpriteRenderer SpriteRenderer;
    private float RunningTime = 0f;
    private int index = 0;
    private void Start()
    {
        if (!Fires.Any()) throw new System.Exception($"Please add any fire sprites to {nameof(BurningFire)}");
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Fires[index];
    }
    private void Update()
    {
        RunningTime -= Time.deltaTime;
        if (RunningTime <= 0)
        {
            RunningTime += TimeForCycle / Fires.Length;
            index++;
            index %= Fires.Length;
        }
        SpriteRenderer.sprite = Fires[index];
    }
}
