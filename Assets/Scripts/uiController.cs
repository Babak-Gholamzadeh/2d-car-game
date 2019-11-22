using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiController : MonoBehaviour {

	public GameObject mainSpeed;
	public Text score;
	public Text theLastScore;
	public Text speedCar;
	float s;
	void Start ()
	{
		s = 0;
		InvokeRepeating ("increaseScore", 0.5f, 0.1f);
	}

	void Update ()
	{
		showSpeedCar ();
	}

	public void Pause ()
	{
		if (Time.timeScale == 1)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void Play ()
	{
		SceneManager.LoadScene ("Level1");
	}

	void increaseScore ()
	{
		score.text = "Score: " + s.ToString ();
		theLastScore.text = s.ToString ();
		s += Mathf.CeilToInt (mainSpeed.GetComponent<moveRoad> ().myCarSpeed);
	}

	void showSpeedCar ()
	{
		speedCar.text = (Mathf.CeilToInt (mainSpeed.GetComponent<moveRoad> ().myCarSpeed * 100)).ToString();
	}

	public void exitGame ()
	{
		Application.Quit ();
	}
	public GameObject gameoverPanel;
	public GameObject highscorePanel;
	public Text txtMyScore;
	public Text txtHighscore;
	public Text txtShowHighscore;

	public void highScore()
	{
		gameoverPanel.SetActive (false);
		highscorePanel.SetActive (true);
		txtHighscore.text = txtMyScore.text;
		txtShowHighscore.text = "Waiting...";
		StartCoroutine (UploadHighscores ("Mohammadam"));
		StartCoroutine ("DownloadHighscores");
	}

	public void backToGame ()
	{
		highscorePanel.SetActive (false);
		gameoverPanel.SetActive (true);
	}
		
	const string privateCode = "Qpz1X0beCUKu3LQc1tnXtQj6lbgHZ39UC1VlQfNCaDJg";
	const string publicCode = "57c5cd0b8af60311300fca11";
	const string webURL = "http://dreamlo.com/lb/";


	IEnumerator UploadHighscores (string username)
	{
		WWW www = new WWW (webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + txtHighscore.text);
		yield return www;
		if (string.IsNullOrEmpty (www.error))
		{
			print ("Upload successful");
		}
		else
		{
			print ("Error uploading" + www.error);
			txtShowHighscore.text = "ERROR-01!!!\nConnection Problems Or Database Problems.\nFirst check your internet connection and after that try again to display the high scores";
		}
	}
	IEnumerator DownloadHighscores ()
	{
		WWW www = new WWW (webURL + publicCode + "/pipe/");
		yield return www;
		if (string.IsNullOrEmpty (www.error))
		{
			print ("Download successful");
			FormatHighscores (www.text);
		}
		else
		{
			print ("Error downoading" + www.error);
			txtShowHighscore.text = "ERROR-02!!!\nConnection Problems Or Database Problems.\nFirst check your internet connection and after that try again to display the high scores";
		}
	}
	void FormatHighscores (string scores)
	{
		string[] eachLine = scores.Split (new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		txtShowHighscore.text = "";
		for (int i = 0; i < eachLine.Length; i++)
		{
			string[] eachPart = eachLine [i].Split (new char[] { '|' });
			txtShowHighscore.text += (i+1).ToString() + ". " + eachPart [0] + ": " + eachPart [1] + "\n";
		}
	}
}
