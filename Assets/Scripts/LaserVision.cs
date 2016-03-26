using UnityEngine;
using System.Collections;

public class LaserVision : MonoBehaviour {

	private LineRenderer _renderer;
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<LineRenderer> (); 
		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		_renderer.SetPosition (0, _transform.position);
		_renderer.SetPosition (1, Vector2.up);
	}
}
