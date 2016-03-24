using UnityEngine;
using System.Collections;

public class DestroySmoke : MonoBehaviour {

	public GameObject smoke;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (smoke, 1F);
	}
}
