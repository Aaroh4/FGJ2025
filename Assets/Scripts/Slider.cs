using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SliderTextUpdater : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    void Start()
    {
        if (slider != null && text != null)
        {
            slider.onValueChanged.AddListener(UpdateText);
            
            UpdateText(slider.value);
        }
    }

    void UpdateText(float value)
    {
        text.text = value.ToString("F2");
    }
}