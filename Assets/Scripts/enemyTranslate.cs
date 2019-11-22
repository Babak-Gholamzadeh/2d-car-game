using UnityEngine;
using System.Collections;

public class enemyTranslate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1)
			transform.Translate (0, 0.2f, 0);
	}
}
