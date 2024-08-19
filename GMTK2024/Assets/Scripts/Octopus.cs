using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;

public class Octopus : MonoBehaviour
{
    //[SerializeField] GameObject TextBoubleImage;
    [SerializeField] TMP_Text TextBouble;
    [SerializeField] Canvas OctopusUICanvas;
    [SerializeField] Sprite WithScale;
    [SerializeField] Sprite WithoutScale;
    [SerializeField] SpriteRenderer SpriteRenderer;
    private Vector2 DefaultScale;
    private bool PlayerFreezed = false;
    private PlayerMovement PlayerMovement;
    private Inventory Inventory;
    private List<WordPuzzle> UnsolvedWordPuzzels = new List<WordPuzzle>();
    [SerializeField] private TMP_InputField InputField;
    private WordPuzzle? ActivePuzzle = null;
    private int CorrectlySolvedPuzzels = 0;
    private int NeededSolvedPuzzels = 2;
    private bool Finished = false;
    private void Start()
    {
        Finished = false;
        SpriteRenderer.sprite = WithScale;
        DefaultScale = transform.localScale;
        UnsolvedWordPuzzels.Add(new WordPuzzle("What is cold, blue and liquid", "water"));
        UnsolvedWordPuzzels.Add(new WordPuzzle("What is bright, yellow and ever burning", "sun"));
        if (InputField == null) throw new Exception($"{nameof(InputField)} was null in {nameof(Octopus)}");
        if (TextBouble == null) throw new Exception($"{nameof(TextBouble)} was null in {nameof(Octopus)}");
        if (WithScale == null) throw new Exception($"{nameof(WithScale)} was null in {nameof(Octopus)}");
        if (WithoutScale == null) throw new Exception($"{nameof(WithoutScale)} was null in {nameof(Octopus)}");
        if (SpriteRenderer == null) throw new Exception($"{nameof(SpriteRenderer)} was null in {nameof(Octopus)}");
    }
    private void Update()
    {
        if (PlayerFreezed)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerFreezed = false;
                PlayerMovement.Unfreeze();
                DisableQuestion();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string answer = InputField.text;
                InputField.text = "";
                if (IsAnswerCorrect(answer, ActivePuzzle.Value.Answer))
                {
                    CorrectlySolvedPuzzels++;
                    ActivePuzzle = null;
                    Congratulate();
                }
                else
                {
                    Scold();
                }
            }
        }
    }
    private void Scold()
    {
        TextBouble.text = "No you moron, that isn't the right answer.";
        Invoke(nameof(StartQuestion), 5f);
    }
    private void Congratulate()
    {
        int i = NeededSolvedPuzzels - CorrectlySolvedPuzzels;
        string s;
        if (i <= 0)
        {
            Inventory.AddScale();
            Finished = true;
            SpriteRenderer.sprite = WithoutScale;
            s = "Fine, you've solved enough of my puzzles. Have a scale.";
        }
        else
        {
            s = $"Yes that was the right answer. But you need to show more, if you want my scale.";
            Invoke(nameof(StartQuestion), 5f);
        }
        TextBouble.text = s;
    }
    private bool IsAnswerCorrect(string answer, string solution)
    {
        answer = answer.ToLower();
        solution = solution.ToLower();
        return answer.Contains(answer);
    }
    private void StartQuestion()
    {
        if (ActivePuzzle == null)
        {
            ActivePuzzle = NextQuestion();
            if (ActivePuzzle == null)
            {
                Debug.Log("Well shit, that wasn't supposed to happen.");
            }
        }
        TextBouble.text = ActivePuzzle.Value.Question;
        OctopusUICanvas.gameObject.SetActive(true);
    }
    private WordPuzzle? NextQuestion()
    {
        if (!UnsolvedWordPuzzels.Any()) return null;
        System.Random rng = new System.Random();
        int ind = rng.Next(0, UnsolvedWordPuzzels.Count);
        var res = UnsolvedWordPuzzels[ind];
        UnsolvedWordPuzzels.RemoveAt(ind);
        return res;
    }

    private void DisableQuestion()
    {
        if (ActivePuzzle != null)
        {
            UnsolvedWordPuzzels.Add(ActivePuzzle.Value);
            ActivePuzzle = null;
        }
        //TextBouble.gameObject.SetActive(false);
        OctopusUICanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.gameObject.GetComponent<PlayerMovement>();
        if (!Finished && p != null)
        {
            Inventory = p.gameObject.GetComponent<Inventory>();
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
public struct WordPuzzle
{
    public string Question;
    public string Answer;
    public WordPuzzle(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }
}