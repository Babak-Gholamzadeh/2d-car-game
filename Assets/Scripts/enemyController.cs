using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	public GameObject mainSpeed;
	public GameObject[] cars;
	float timer;
	float[] array = new float[4];
	void Start () {
		timer = 1f;
		array [0] = -2f; array [1] = -0.7f; array [2] = 0.7f; array [3] = 2f;
	}

	void Update () {
		if (Time.timeScale == 1)
		{
			timer -= Time.deltaTime;
			if (timer <= 0) {
				Instantiate (cars [Random.Range (0, 8)], new Vector3 (array [Random.Range (0, 4)], transform.position.y, transform.position.z), transform.rotation);
				timer = 3f - mainSpeed.GetComponent<moveRoad> ().myCarSpeed;
			}
		}
	}
}
