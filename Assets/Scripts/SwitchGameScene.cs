using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class Control : MonoBehaviour
{
	public TMP_Text	replace;

	public GameObject tree;

	public GameObject hands;

	public GameObject path;

	public GameObject textImage;

	public GameObject lastScreen;

	public string[] strings;

    private void Start()
    {
		Cursor.visible = true;
    }

    public void NextScene()
    {
        //SceneManager.LoadScene("GameScene");
		tree.SetActive(true);
		StartCoroutine(StartCounting(strings));

    }

	private IEnumerator StartCounting(string[] amogous)
	{
		yield return new WaitForSecondsRealtime(3);
		path.SetActive(true);
		yield return new WaitForSecondsRealtime(3);
		hands.SetActive(true);

		yield return new WaitForSecondsRealtime(3);
		textImage.SetActive(true);
		replace.text = amogous[0];
		yield return new WaitForSecondsRealtime(5);
		replace.text = amogous[1];
		yield return new WaitForSecondsRealtime(5);
		replace.text = amogous[2];
		yield return new WaitForSecondsRealtime(5);
		replace.text = amogous[3];
		yield return new WaitForSecondsRealtime(5);
		replace.text = amogous[4];
		yield return new WaitForSecondsRealtime(5);
		replace.text = amogous[5];
		yield return new WaitForSecondsRealtime(5);
		lastScreen.SetActive(true);
		textImage.SetActive(false);
		yield return new WaitForSecondsRealtime(4);
		SceneManager.LoadScene("GameScene");
	}
}