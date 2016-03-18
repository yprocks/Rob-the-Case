using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Button restart;
	public Text checkPointText;
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController fps;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Restart(){
		SceneManager.LoadScene ("Main");	
	}

	public void SetLevelCheckpoint(){
		restart.gameObject.SetActive (true);
		checkPointText.gameObject.SetActive(true);
		fps = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
		fps.enabled = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

}
