using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class carController : MonoBehaviour {

	public GameObject mySpeed;
	float gear;
	bool androidPlatform;

	void Awake () {
		#if UNITY_ANDROID
			androidPlatform = true;
		#else
			androidPlatform = false;
		#endif
	}
	
	void Update ()
	{
		if (Time.timeScale == 1)
		{
			if (androidPlatform == true)
			{
				movement (Input.acceleration.x);
				gear = TouchMode ();
			}
			else
			{
				movement (Input.GetAxis ("Horizontal"));
				gear = Input.GetAxis ("Vertical");
			}
			if (gear > 0)
				increasSpeed ();
			else if (gear < 0)
				reduceSpeed ();
			else
				normalSpeed ();
		}
	}

	int TouchMode ()
	{
		if (Input.touchCount > 0) 
		{
			Touch t = Input.GetTouch (0);
			if (t.position.x < Screen.width / 2)
				return -1;
			if (t.position.x > Screen.width / 2)
				return 1;
		}
		return 0;
	}

	void movement (float move)
	{
		transform.position = new Vector2 (Mathf.Clamp (transform.position.x + (move * Time.deltaTime * 10f), -2.15f, 2.15f), transform.position.y);
	}

	void increasSpeed ()
	{
		if (mySpeed.GetComponent<moveRoad> ().myCarSpeed < 2f)
			mySpeed.GetComponent<moveRoad> ().myCarSpeed += 0.01f;
		if (transform.position.y < -2.5f)
			transform.position = new Vector2 (transform.position.x, transform.position.y + 0.02f);
	}
	void reduceSpeed ()
	{
		if (mySpeed.GetComponent<moveRoad> ().myCarSpeed > 0.3f)
			mySpeed.GetComponent<moveRoad> ().myCarSpeed -= 0.05f;
		if (transform.position.y > -3.5f)
			transform.position = new Vector2 (transform.position.x, transform.position.y - 0.03f);
	}
	void normalSpeed ()
	{
		if (mySpeed.GetComponent<moveRoad> ().myCarSpeed < 0.49f)
			mySpeed.GetComponent<moveRoad> ().myCarSpeed += 0.01f;
		else if (mySpeed.GetComponent<moveRoad> ().myCarSpeed > 0.51f)
			mySpeed.GetComponent<moveRoad> ().myCarSpeed -= 0.01f;

		if (transform.position.y < -3.1f)
			transform.position = new Vector2 (transform.position.x, transform.position.y + 0.01f);
		else if (transform.position.y > -2.9f)
			transform.position = new Vector2 (transform.position.x, transform.position.y - 0.01f);
	}

	public GameObject gameOverPanel;
	void OnCollisionEnter2D (Collision2D other)
	{
		Time.timeScale = 0;
		gameOverPanel.SetActive (true);
	}
}
