﻿using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public GameObject muzzleFlash;

	private GameController gameController;
	private Transform _transform;
	private float nextFire; 
	private float fireRate;

	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform> ();
		nextFire = 0F;
		fireRate = 0.6F;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

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
				Instantiate(muzzleFlash, _transform.position, Quaternion.identity);
				if (hit.transform.gameObject.tag == "Player") {
					gameController.SetPlayerHit ();
					gameController.SetScore (-10);
				}

			}

			nextFire = fireRate + Time.time;
//			temp = Instantiate (silencer);
//			Destroy (temp, 0.3F);
		}


	}
}
