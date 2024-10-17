using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ArabicFixerTMPRO : MonoBehaviour
{
    public bool ShowTashkeel;
    public bool UseHinduNumbers;
    public bool isArabicEnabled; // Control whether Arabic correction is enabled

    [Header("Fonts")]
    TextMeshProUGUI tmpTextComponent;
    public TMP_FontAsset consolasFont;
    public TMP_FontAsset cairoFont;

    private string previousText; // To track text changes
    private int OldFontSize; // To refresh on font size change
    private RectTransform rectTransform;  // To refresh on resize
    private Vector2 OldDeltaSize; // To refresh on resize
    private bool OldEnabled = false; // To refresh on enabled/disabled state change
    private List<RectTransform> OldRectTransformParents = new List<RectTransform>(); // To refresh on parent resize
    private Vector2 OldScreenRect = new Vector2(Screen.width, Screen.height); // To refresh on screen resize

    bool isInitialized;

    public void Awake()
    {
        GetRectTransformParents(OldRectTransformParents);
        isInitialized = false;
        tmpTextComponent = GetComponent<TextMeshProUGUI>();
    }

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        previousText = tmpTextComponent.text;
        isInitialized = true;
    }

    private void GetRectTransformParents(List<RectTransform> rectTransforms)
    {
        rectTransforms.Clear();
        for (Transform parent = transform.parent; parent != null; parent = parent.parent)
        {
            GameObject goP = parent.gameObject;
            RectTransform rect = goP.GetComponent<RectTransform>();
            if (rect) rectTransforms.Add(rect);
        }
    }

    private bool CheckRectTransformParentsIfChanged()
    {
        bool hasChanged = false;
        for (int i = 0; i < OldRectTransformParents.Count; i++)
        {
            hasChanged |= OldRectTransformParents[i].hasChanged;
            OldRectTransformParents[i].hasChanged = false;
        }
        return hasChanged;
    }

    public void Update()
    {
        if (!isInitialized || !isArabicEnabled)
            return;

        // Check if the text or other properties have changed
        if (previousText != tmpTextComponent.text ||
            OldFontSize != tmpTextComponent.fontSize ||
            OldDeltaSize != rectTransform.sizeDelta ||
            OldEnabled != tmpTextComponent.enabled ||
            OldScreenRect.x != Screen.width || OldScreenRect.y != Screen.height ||
            CheckRectTransformParentsIfChanged())
        {
            ApplyArabicCorrection();
            previousText = tmpTextComponent.text;  // Update with the new text
            OldFontSize = (int)tmpTextComponent.fontSize;
            OldDeltaSize = rectTransform.sizeDelta;
            OldEnabled = tmpTextComponent.enabled;
            OldScreenRect.x = Screen.width;
            OldScreenRect.y = Screen.height;
        }
    }

    public void ApplyArabicCorrection()
    {
        if (!string.IsNullOrEmpty(tmpTextComponent.text) && isArabicEnabled)
        {
            string fixedText = ArabicSupport.Fix(tmpTextComponent.text, ShowTashkeel, UseHinduNumbers);
            fixedText = fixedText.Replace("\r", "");  // Fix unwanted line breaks

            string finalText = "";
            string[] rtlParagraph = fixedText.Split('\n');

            tmpTextComponent.text = "";
            for (int lineIndex = 0; lineIndex < rtlParagraph.Length; lineIndex++)
            {
                string[] words = rtlParagraph[lineIndex].Split(' ');
                System.Array.Reverse(words);
                tmpTextComponent.text = string.Join(" ", words);
                Canvas.ForceUpdateCanvases();

                for (int i = 0; i < tmpTextComponent.textInfo.lineCount; i++)
                {
                    int startIndex = tmpTextComponent.textInfo.lineInfo[i].firstCharacterIndex;
                    int endIndex = (i == tmpTextComponent.textInfo.lineCount - 1) ? tmpTextComponent.text.Length
                        : tmpTextComponent.textInfo.lineInfo[i + 1].firstCharacterIndex;
                    int length = endIndex - startIndex;
                    string[] lineWords = tmpTextComponent.text.Substring(startIndex, length).Split(' ');
                    System.Array.Reverse(lineWords);
                    finalText = finalText + string.Join(" ", lineWords).Trim() + "\n";
                }
            }
            tmpTextComponent.text = finalText.TrimEnd('\n');
        }
    }

    // Method to enable/disable Arabic correction
    public void SetArabicCorrection(bool enableArabic)
    {
        isArabicEnabled = enableArabic;
        
        if (enableArabic)
        {
            tmpTextComponent.font = cairoFont;
        }
        else
        {
            tmpTextComponent.font = consolasFont;
        }
    }
}
