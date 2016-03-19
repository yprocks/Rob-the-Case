using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour {

	public float fireRate = 1F;
	public GameObject flash;
	public GameObject silencer;
	public Camera fpsCam;
	public GameObject smoke;

	private GameController gameController;
	private float nextFire; 
	private bool flashEnabled;

	// Use this for initialization
	void Start () {
		nextFire = 0F;
		flashEnabled = false;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		flash.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		GameObject temp;
		RaycastHit hit;

		Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5F, 0.5F, 0));

		if (Input.GetButtonDown ("Fire1") && !flashEnabled) {
			if (Time.time > nextFire) {
				flash.SetActive (true);
				flashEnabled = true;

				if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, 100)) {
					//Instantiate prefab at hit.point;
					Instantiate(smoke, hit.point, Quaternion.Euler(new Vector3(-90, 0 ,0 )));
				}

				nextFire = fireRate + Time.time;
				temp = Instantiate (silencer);
				Destroy (temp, 0.3F);
			}
		} else {
			flash.SetActive (false);
			flashEnabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Exit") {
			gameController.SetLevelCheckpoint ();
		}
	}
}
