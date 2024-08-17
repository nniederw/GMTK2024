using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_Text))]
public class ScaleTextRenderer : MonoBehaviour
{
    private const string StartSring = "Scales: ";
    [SerializeField] private Inventory Player;
    private TMP_Text Text;
    private void Start()
    {
        Text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        Text.text = StartSring + Player.Scales.ToString();
    }
}