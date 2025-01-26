using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;

public class TextComparing : MonoBehaviour
{
    public float totalTime = 30f;

    public TMP_InputField myInputField;
    public TMP_Text    mySecondText;
	public TMP_Text		greyText;

	public TMP_Text		timer;

    public string compareString = "Test amogus";

    public int points = 0;
    private void Start()
    {

    }
        
    public int CalculatePoints()
    {
        return points;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        myInputField.onValueChanged.AddListener(OnInputFieldChanged);
		mySecondText.text += "_";
		myInputField.ActivateInputField();
		myInputField.interactable = false;
		myInputField.characterLimit = 100;
		greyText.text = compareString;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		totalTime = 30;
    }

    void OnInputFieldChanged(string newText)
    {
        int offset = 0;
        points = 0;
        string testString;
        testString = newText;

        for (int  i = 0; i < newText.Length && i < compareString.Length; i++)
        {
            if (newText[i] == compareString[i])
                points++;
            else
            {
                testString = testString.Insert(i + offset + 1, "</color>");
                testString = testString.Insert(i + offset, $"<color=#FF0000>");
                offset += "<color=#FF0000></color>".Length;
            }
        }

        if (newText.Length > compareString.Length)
        {
            testString = testString.Insert(testString.Length, "</color>");
            testString = testString.Insert(compareString.Length + offset, $"<color=#FF0000>");
            points -= newText.Length - compareString.Length;
        }

        mySecondText.text = testString + "_";

        //mySecondText.text = newText;

        //Debug.Log(points + "/" + compareString.Length);
    }
    // Update is called once per frame
    void Update()
    {
		if (totalTime > 0)
			totalTime -= Time.deltaTime;
		else if (totalTime < 0)
			totalTime = 0;
		
		timer.text = totalTime.ToString();

        if (myInputField != null && !myInputField.isFocused)
		{
			myInputField.interactable = true;
			myInputField.ActivateInputField();
			myInputField.interactable = false;
		}
    }

	void OnGUI()
	{
		if( Event.current.keyCode == KeyCode.Backspace && ( Event.current.type == EventType.KeyUp || Event.current.type == EventType.KeyDown ) )
			totalTime -= 1;
	}

}


