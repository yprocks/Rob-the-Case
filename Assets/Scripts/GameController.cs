using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Button restart;
	public Text checkPointText;
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController fps;
	public Image EndImage;
	public GameObject[] caseSpawnPoints;
	public GameObject briefcase;
	//public GameObject[] swatPoints;
	//public GameObject swat;
	public PlayerController playerController;
	public Text enemyText;
	public Text timerText;
	public Slider healthBar;
	public Image caseImage;
	public Text GameOverText;
	public Text WinText;
	public Text scoreText;

	//private Variables
	public GunScript gunScript;
	private int playerHit;
	private bool gameOver;
	private AudioSource[] sounds;
	private int score;
	private int health;
	private int enemyKill;
	private float timer;
	private bool gotCase;
	private bool isTimeOver;
	private bool win;
	private bool lose;
	private float seconds; // Amount of seconds 
	private float minutes; 

	// Use this for initialization
	void Start () {
		playerHit = 0;
		score = 0;
		health = 5;
		enemyKill = 0;
		timer = 2;
		gotCase = false;
		isTimeOver = false;
		lose = false;
		win = false;
		seconds = 0F;
		minutes = 2F; 
		sounds = GetComponents<AudioSource> ();
		SpawnCase ();
//		SpawnSwat ();
		enemyText.text = "Kills : 0";
		timerText.text = "";
		healthBar.value = 5;
		GameOverText.text = "";
		WinText.text = "";
		scoreText.text = "";
	}

	private void SpawnCase(){
		GameObject caseSpawnPoint = caseSpawnPoints[Random.Range (0,5)];
		Instantiate (briefcase, caseSpawnPoint.transform.position, Quaternion.identity);
	}

//	private void SpawnSwat(){
//		for (int i = 0; i < swatPoints.Length; i++) {
//			Instantiate (swat, swatPoints [i].transform.position, swatPoints [i].transform.rotation);
//		}
//	}

	// Update is called once per frame
	void FixedUpdate () {
		if (seconds <= 0) {
			seconds = 59;
			if (minutes >= 1) {
				minutes --;
			} else {
				minutes = 0;
				seconds = 0;
				timerText.text  = minutes.ToString ("f0") + ":0" + seconds.ToString ("f0");
				//Debug.Log(minutes.ToString ("f0") + ":0" + seconds.ToString ("f0"));
				isTimeOver = true;
				GameOver ();
			}
		} else 
		{
			seconds -= Time.deltaTime;
			timerText.text = minutes + ":"+ Mathf.Ceil(seconds);
			//Debug.Log("Time remained: " +minutes + ":"+ Mathf.Ceil(seconds));
		}
	
	}

	public void Restart(){
		SceneManager.LoadScene("Main");	
	}


	public void SetPlayerHit(){
		playerHit++;
		sounds [0].Play ();
		SetHealth ();
		GameOver ();
	}

	private int GetPlayerHit(){
		return playerHit;
	}

	private bool isGameOver(){

		if (GetHealth () <= 0 || IsTimeOver ()) {
			lose = true;
			gameOver = true;
		}
		if (GetHealth () > 0 && GetEnemyKill () >= 1 && !IsTimeOver () && IsCaseFound ()) {
			win = true;
			gameOver = true;
		}
		return gameOver;
	}

	public void SetScore(int scoreValue){
		score += scoreValue;
	}

	public void SetHealth(){
		health--;
		healthBar.value--;
		if (health <= 0) {
			sounds [1].Play ();
			gameOver = true;
		}
		GameOver ();
	}

	public void SetEnemyKill(){
		enemyKill++;
		enemyText.text = "Kills : " + GetEnemyKill();
		GameOver ();
	}

	private void SetTimer(){
		GameOver ();
	}

	public void FoundCase(){
		gotCase = true;
		caseImage.color = new Color (255,255,255);
		sounds [3].Play ();
		GameOver ();
	}


	private int GetScore(){
		return score;
	}

	private int GetHealth(){
		return health;
	}

	private int GetEnemyKill(){
		return enemyKill;
	}

	private float GetTimer(){
		return timer;
	}

	private bool IsTimeOver(){
		return isTimeOver;
	}

	private bool IsCaseFound(){
		return gotCase;
	}

	private void GameOver(){
		if (isGameOver () && lose) {	
			fps = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
			fps.enabled = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			gunScript.enabled = false;
			EndImage.gameObject.SetActive (true);
			restart.gameObject.SetActive (true);
			GameOverText.text = "Game Over";
			scoreText.text = "Score : " +score;
			caseImage.enabled = false;
			timerText.enabled = false;
			enemyText.enabled = false;
		}
		else if (isGameOver () && win) {
			fps = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
			fps.enabled = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			EndImage.gameObject.SetActive (true);
			restart.gameObject.SetActive (true);
			WinText.text = "Level Complete";
			scoreText.text = "Score : " +score;
			caseImage.enabled = false;
			timerText.enabled = false;
			enemyText.enabled = false;
		}

	}
}