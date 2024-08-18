using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Octopus : MonoBehaviour
{
    //[SerializeField] GameObject TextBoubleImage;
    [SerializeField] TMP_Text TextBouble;
    [SerializeField] Canvas OctopusUICanvas;
    private Vector2 DefaultScale;
    private bool PlayerFreezed = false;
    private PlayerMovement PlayerMovement;
    private void Start()
    {
        DefaultScale = transform.localScale;
    }
    private void Update()
    {
        if (PlayerFreezed && Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerFreezed = false;
            PlayerMovement.Unfreeze();
            DisableQuestion();
        }
    }
    private void StartQuestion()
    {
        TextBouble.gameObject.SetActive(true);
        OctopusUICanvas.gameObject.SetActive(true);
        //TextBoubleImage.SetActive(true);
    }
    private void DisableQuestion()
    {
        TextBouble.gameObject.SetActive(false);
        OctopusUICanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.gameObject.GetComponent<PlayerMovement>();
        if(p != null)
        {
            PlayerMovement = p;
            p.Freeze();
            PlayerFreezed = true;
            transform.localScale = DefaultScale * 1.25f;
            StartQuestion();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var p = collision.gameObject.GetComponent<PlayerMovement>();
        if (p != null)
        {
            transform.localScale = DefaultScale;
        }
    }
}