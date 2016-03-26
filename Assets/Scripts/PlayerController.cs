using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour {

	public float fireRate = 1F;
	public GameObject flash;
	public GameObject silencer;
	public Camera fpsCam;
	public GameObject smoke;

	private GameController gameController;
	private SwatScript swatScript;
	private Transform _transform;
	private float nextFire; 
	private bool flashEnabled;

	// Use this for initialization
	void Start () {
		nextFire = 0F;
		_transform = gameObject.GetComponent<Transform> ();
		flashEnabled = false;
		swatScript = GameObject.FindGameObjectWithTag ("Swat").GetComponent<SwatScript> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		flash.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		GameObject temp;

		if (Input.GetButtonDown ("Fire1") && !flashEnabled) {
			
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5F, 0.5F, 0));

			RaycastHit hit; //Stores information from the ray;

			if (Time.time > nextFire) {
				flash.SetActive (true);
				flashEnabled = true;

			//Debug.Log (hit.transform.gameObject.tag);

				if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, 100)) {
					//Instantiate prefab at hit.point;

					if(hit.transform.gameObject.tag != "Swat")
						Instantiate(smoke, hit.point, Quaternion.Euler(new Vector3(-90, 0 ,0 )));

					if (hit.transform.gameObject.tag == "Swat") {
						swatScript.SetHits();
					}

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
		if (other.gameObject.tag == "Case") {
			gameController.FoundCase ();
			gameController.SetScore (100);
			Destroy (other.gameObject);
		}
	}
	 
	public void SetAnimation(){
		swatScript.StopNavMesh ();
//		Debug.Log ("In anim");
	}
}
