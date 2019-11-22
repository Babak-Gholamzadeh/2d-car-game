using UnityEngine;
using System.Collections;

public class moveRoad : MonoBehaviour {

	public float myCarSpeed;
	void Awake ()
	{
		Time.timeScale = 1;
	}
	void Update () {
		if (Time.timeScale == 1)
			GetComponent<Renderer> ().material.mainTextureOffset += new Vector2 (0, myCarSpeed * 0.03f);
	}
}
