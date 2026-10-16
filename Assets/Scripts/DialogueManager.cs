using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

// Keep this struct outside the class so other scripts can use it
[System.Serializable]
public struct DialogueLine
{
    public string speakerName;
    [TextArea]
    public string text;
    public Sprite portrait;
}

public class DialogueManager : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject dialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitUI;

    [Header("Typing Settings")]
    public float typingSpeed = 0.025f;
    public float punctuationPause = 0.4f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip typingSound;
    [Range(1, 5)] public int playSoundEvery = 2; // 1 = every letter, 2 = every other letter
    [Range(0.5f, 2f)] public float minPitch = 0.95f;
    [Range(0.5f, 2f)] public float maxPitch = 1.05f;

    private DialogueLine[] currentLines;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private Animator currentSpeakerAnimator;
    public static bool isActive = false;


    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (!dialogueBox.activeInHierarchy) return;

        bool skipPressed = (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
                           (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
                           (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);

        if (skipPressed)
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = "  " + currentLines[currentLineIndex].text;
                isTyping = false;
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < currentLines.Length)
                {
                    PlayCurrentLine();
                }
                else
                {
                    dialogueBox.SetActive(false);
                    isActive = false;
                }
            }
        }
}

    // Other scripts will call this function to pass in their specific lines
    public void StartDialogue(DialogueLine[] lines, Animator speakerAnimator = null)
    {
        isActive = true;

        currentLines = lines;
        currentSpeakerAnimator = speakerAnimator;
        currentLineIndex = 0;

        dialogueBox.SetActive(true);
        PlayCurrentLine();
    }

    void PlayCurrentLine()
    {
        DialogueLine line = currentLines[currentLineIndex];

        nameText.text = line.speakerName;
        portraitUI.sprite = line.portrait;


        typingCoroutine = StartCoroutine(TypeSentence(line.text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "  ";

        bool lastCharWasPunctuation = false;
        int charCount = 0; // Add a counter for the letters

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            // Only count and play sounds for actual visible letters
            if (letter != ' ' && typingSound != null)
            {
                charCount++;

                // The % symbol checks if the count divides evenly by your setting
                if (charCount % playSoundEvery == 0)
                {
                    audioSource.pitch = Random.Range(minPitch, maxPitch);
                    audioSource.PlayOneShot(typingSound);
                }
            }

            bool isPunctuation = (letter == '.' || letter == ',' || letter == '!' || letter == '?');

            if (isPunctuation && !lastCharWasPunctuation)
            {
                yield return new WaitForSeconds(punctuationPause);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }

            lastCharWasPunctuation = isPunctuation;
        }

        isTyping = false;
    }
}