using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour {

	public GameObject gunShot;
	public Transform gunPoint;
	public float fireRate = 0.5F;

	private GameController gameController;
	private GameObject fire;
	private float nextFire; 

	// Use this for initialization
	void Start () {
		nextFire = 0F;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (Input.GetButtonDown ("Fire1")) {
			if (Time.time > nextFire) {
				fire = (GameObject)Instantiate (gunShot, gunPoint.transform.position, Quaternion.identity);
				nextFire = fireRate + Time.time;
			}
		}
		Destroy (fire, 1F);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Exit") {
			gameController.SetLevelCheckpoint ();
		}
	}
}
