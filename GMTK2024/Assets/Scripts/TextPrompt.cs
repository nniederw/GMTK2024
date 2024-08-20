using System;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class TextPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private GameObject TextPromptImage;
    [SerializeField] private string TutorialText;
    [SerializeField] private bool Ending = false;
    [SerializeField] private bool FreezePlayer = true;
    private PlayerMovement PlayerMovement;
    private bool Active = false;
    private bool Triggered = false;
    private void Start()
    {
        if (Text == null) throw new Exception($"{nameof(Text)} was null in {nameof(TextPrompt)}");
        if (TextPromptImage == null) throw new Exception($"{nameof(TextPromptImage)} was null in {nameof(TextPrompt)}");
    }
    private void Update()
    {
        if (Active && Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerMovement.Unfreeze();
            Active = false;
            TextPromptImage.SetActive(false);
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
            if (FreezePlayer)
            {
                PlayerMovement.Freeze();
            }
            TextPromptImage.SetActive(true);
            Text.text = TutorialText;
            Triggered = true;
            Active = true;
        }
    }
}