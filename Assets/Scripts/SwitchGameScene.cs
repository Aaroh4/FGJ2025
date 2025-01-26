using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Control : MonoBehaviour
{
	public TMP_Text	replace;

	public string[] strings;

	public void NextScene()
    {
        //SceneManager.LoadScene("GameScene");
		StartCoroutine(StartCounting(strings));

    }

	private IEnumerator StartCounting(string[] amogous)
	{
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
		SceneManager.LoadScene("GameScene");
	}
}