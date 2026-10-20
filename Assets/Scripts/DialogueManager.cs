using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[System.Serializable]
public struct DialogueLine
{
    public string speakerName;
    [TextArea]
    public string text;
    public Sprite portrait;

    [Header("Right Side Only (Cat/Bat)")]
    public bool showPortraitOnRight;
    public bool useSleepingCat;
    public AudioClip voiceSound;
    [Range(0.1f, 3f)] public float pitchMultiplier;
    [Range(0.1f, 3f)] public float speedMultiplier;
    [Range(0, 5)] public int soundFrequency;
    public Color nameColor;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [Header("UI Settings")]
    public GameObject dialogueBox;

    [Header("Left Side UI (Witch)")]
    public Image leftPortraitUI;
    public TextMeshProUGUI leftNameText;
    public TextMeshProUGUI leftDialogueText;
    public Color witchNameColor = Color.white;

    [Header("Right Side UI (Cat/Bat)")]
    public Image rightPortraitUI;
    public GameObject sleepingCatPortraitUI;
    public TextMeshProUGUI rightNameText;
    public TextMeshProUGUI rightDialogueText;

    [Header("Typing Settings")]
    public float typingSpeed = 0.025f;
    public float punctuationPause = 0.6f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip witchTypingSound;
    [Range(1, 5)] public int playSoundEvery = 2;
    [Range(0.5f, 2f)] public float minPitch = 0.95f;
    [Range(0.5f, 2f)] public float maxPitch = 1.05f;

    [Header("Pitch Drift Settings")]
    public float defaultDrift = 0f;
    public float questionDrift = 0.15f;
    public float exclamationDrift = 0.1f;
    public float exclamationStartBoost = 0.05f;

    private DialogueLine[] currentLines;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    public static bool isActive = false;

    private TextMeshProUGUI activeDialogueText;

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
                // THE FIX: Instantly reveal all characters, including parsed rich text
                activeDialogueText.maxVisibleCharacters = 99999;
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

    public void StartDialogue(DialogueLine[] lines)
    {
        isActive = true;
        currentLines = lines;
        currentLineIndex = 0;

        dialogueBox.SetActive(true);
        PlayCurrentLine();
    }

    void PlayCurrentLine()
    {
        DialogueLine line = currentLines[currentLineIndex];

        if (line.showPortraitOnRight)
        {
            leftPortraitUI.gameObject.SetActive(false);
            leftNameText.gameObject.SetActive(false);
            leftDialogueText.gameObject.SetActive(false);

            rightNameText.gameObject.SetActive(true);
            rightDialogueText.gameObject.SetActive(true);

            if (line.useSleepingCat)
            {
                rightPortraitUI.gameObject.SetActive(false);
                sleepingCatPortraitUI.SetActive(true);
            }
            else
            {
                sleepingCatPortraitUI.SetActive(false);
                rightPortraitUI.gameObject.SetActive(true);
                rightPortraitUI.sprite = line.portrait;
            }

            rightNameText.text = line.speakerName;
            rightNameText.color = line.nameColor.a == 0f ? Color.white : line.nameColor;
            activeDialogueText = rightDialogueText;
        }
        else
        {
            rightPortraitUI.gameObject.SetActive(false);
            sleepingCatPortraitUI.SetActive(false);
            rightNameText.gameObject.SetActive(false);
            rightDialogueText.gameObject.SetActive(false);

            leftPortraitUI.gameObject.SetActive(true);
            leftNameText.gameObject.SetActive(true);
            leftDialogueText.gameObject.SetActive(true);

            leftPortraitUI.sprite = line.portrait;
            leftNameText.text = line.speakerName;
            leftNameText.color = witchNameColor;
            activeDialogueText = leftDialogueText;
        }

        typingCoroutine = StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        isTyping = true;

        // THE FIX: Push the full sentence to TMP instantly so it calculates the tags
        string sentence = "  " + line.text;
        activeDialogueText.text = sentence;
        activeDialogueText.maxVisibleCharacters = 0; // Hide it all
        activeDialogueText.ForceMeshUpdate(); // Force TMP to read the tags right now

        bool lastCharWasPunctuation = false;
        int charCount = 0;

        string trimmedSentence = line.text.TrimEnd();
        bool isQuestion = trimmedSentence.EndsWith("?");
        bool isExclamation = trimmedSentence.EndsWith("!");

        float pMultiplier = 1f;
        float sMultiplier = 1f;
        int currentSoundFreq = playSoundEvery;
        AudioClip currentVoice = witchTypingSound;

        if (line.showPortraitOnRight)
        {
            pMultiplier = line.pitchMultiplier == 0f ? 1f : line.pitchMultiplier;
            sMultiplier = line.speedMultiplier == 0f ? 1f : line.speedMultiplier;
            if (line.soundFrequency > 0) currentSoundFreq = line.soundFrequency;
            if (line.voiceSound != null) currentVoice = line.voiceSound;
        }

        float basePitch = ((minPitch + maxPitch) / 2f) * pMultiplier;
        float pitchVariance = (maxPitch - minPitch) / 2f;

        float maxDrift = defaultDrift;
        if (isQuestion) maxDrift = questionDrift;
        else if (isExclamation) maxDrift = exclamationDrift;
        if (isExclamation) basePitch += exclamationStartBoost;

        float pitchStep = maxDrift / Mathf.Max(1, line.text.Length);
        float currentPitch = basePitch;

        float currentTypingSpeed = typingSpeed / sMultiplier;
        float currentPunctuationPause = punctuationPause / sMultiplier;

        // THE FIX: Loop through the visible characters, completely ignoring the hidden < > tags
        int totalVisibleCharacters = activeDialogueText.textInfo.characterCount;

        for (int i = 0; i < totalVisibleCharacters; i++)
        {
            activeDialogueText.maxVisibleCharacters = i + 1;
            char letter = activeDialogueText.textInfo.characterInfo[i].character;

            if (letter != ' ' && currentVoice != null)
            {
                charCount++;
                currentPitch += pitchStep;

                if (charCount % currentSoundFreq == 0)
                {
                    audioSource.pitch = currentPitch + Random.Range(-pitchVariance, pitchVariance);
                    audioSource.PlayOneShot(currentVoice);
                }
            }

            bool isPunctuation = (letter == '.' || letter == ',' || letter == '!' || letter == '?');

            if (isPunctuation && !lastCharWasPunctuation)
            {
                yield return new WaitForSeconds(currentPunctuationPause);
            }
            else
            {
                yield return new WaitForSeconds(currentTypingSpeed);
            }

            lastCharWasPunctuation = isPunctuation;
        }

        isTyping = false;
    }
}