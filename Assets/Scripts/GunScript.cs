using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	private Transform _transform;
	private float nextFire; 
	private float fireRate;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform> ();
		nextFire = 0F;
		fireRate = 1F;
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit; //Stores information from the ray;
		if (Time.time > nextFire) {
//			flash.SetActive (true);
//			flashEnabled = true;

			//				Debug.Log (hit.transform.gameObject.tag);

			if (Physics.Raycast (_transform.position, _transform.forward, out hit, 100)) {
				//Instantiate prefab at hit.point;


				if (hit.transform.gameObject.tag == "Player") {
					
				}

			}

			nextFire = fireRate + Time.time;
//			temp = Instantiate (silencer);
//			Destroy (temp, 0.3F);
		}


	}
}
