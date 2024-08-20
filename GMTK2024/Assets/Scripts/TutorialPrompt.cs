using System;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TutorialPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private GameObject TextPrompt;
    [SerializeField] private string TutorialText;
    [SerializeField] private bool Ending = false;
    private PlayerMovement PlayerMovement;
    private bool Active = false;
    private bool Triggered = false;
    private void Start()
    {
        if (Text == null) throw new Exception($"{nameof(Text)} was null in {nameof(TutorialPrompt)}");
        if (TextPrompt == null) throw new Exception($"{nameof(TextPrompt)} was null in {nameof(TutorialPrompt)}");
    }
    private void Update()
    {
        if (Active && Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerMovement.Unfreeze();
            Active = false;
            TextPrompt.SetActive(false);
            if (Ending)
            {
                SceneManager.LoadMainScene();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (!Triggered && pm != null)
        {
            PlayerMovement = pm;
            PlayerMovement.Freeze();
            TextPrompt.SetActive(true);
            Text.text = TutorialText;
            Triggered = true;
            Active = true;
        }
    }
}