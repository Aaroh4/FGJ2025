using UnityEngine;
using TMPro;

public class TextComparing : MonoBehaviour
{
    public TMP_InputField myInputField;
    public TMP_Text    mySecondText;
	public TMP_Text		greyText;

    public string compareString = "Test amogus";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        myInputField.onValueChanged.AddListener(OnInputFieldChanged);
		mySecondText.text += "|";
		myInputField.ActivateInputField();
		myInputField.interactable = false;
		myInputField.characterLimit = 100;
		greyText.text = compareString;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

    }

    void OnInputFieldChanged(string newText)
    {

        int points = 0;
        int offset = 0;
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

        mySecondText.text = testString + "|";

        //mySecondText.text = newText;

        //Debug.Log(points + "/" + compareString.Length);
    }
    // Update is called once per frame
    void Update()
    {
        if (myInputField != null && !myInputField.isFocused)
		{
			myInputField.interactable = true;
			myInputField.ActivateInputField();
			myInputField.interactable = false;
		}
    }

	void OnGUI()
	{
		//if( Event.current.keyCode == KeyCode.Backspace && ( Event.current.type == EventType.KeyUp || Event.current.type == EventType.KeyDown ) )
		//	Event.current.Use();
	}

}


