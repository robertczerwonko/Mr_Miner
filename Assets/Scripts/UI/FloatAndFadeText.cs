using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatAndFadeText : MonoBehaviour 
{	
	private Text textM;

	public float fadeDuration = .75f;
	public float translateSpeed = 3f;

	void Start ()
	{
		textM = GetComponent<Text> ();

		StartCoroutine (Fade());
	}

	void Update ()
	{
		this.transform.Translate (Vector3.up * Time.deltaTime * translateSpeed);
	}

	IEnumerator Fade ()
	{
		float fadeSpeed = (float)1.0 / fadeDuration;
		Color c = textM.color;

		for (float t = 0.0f; t < 1.0f; t += (Time.deltaTime * fadeSpeed)) 
		{
			c.a = Mathf.Lerp (1, 0, t);
			textM.color = c;
			yield return true;
		}

		Destroy (gameObject);
	}
}