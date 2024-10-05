using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    private Text textComponent;
    private TMP_Text tmpTextComponent;
    private float deltaTime = 0.0f;

    void Start()
    {
        textComponent = GetComponent<Text>();
        tmpTextComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        if (textComponent != null)
        {
            textComponent.text = Mathf.Ceil(fps).ToString() + " FPS";
        }

        if (tmpTextComponent != null)
        {
            tmpTextComponent.text = Mathf.Ceil(fps).ToString() + " FPS";
        }
    }
}