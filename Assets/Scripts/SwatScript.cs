using UnityEngine;
using System.Collections;

public class SwatScript : MonoBehaviour {

	Animator anim;
	public float speed;
	public Transform target;
	public GameObject idleGun;
	public GameObject shootGun;
	public Transform vision;

	private Transform _transform;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
//		float step = speed * Time.deltaTime;
//		this._transform.position = Vector3.MoveTowards (_transform.position, target.position, step);

		RaycastHit hit;

		if (Physics.Raycast (vision.transform.position, vision.transform.forward , out hit, 100)) {

			if (hit.transform.gameObject.tag == "Player") {
				setAmimation (4);
//				_transform.rotation = hit.transform.rotation; 
			}

		}
	}

	public void setAmimation(int clipNum){
		if (clipNum == 4) {
			anim.SetTrigger ("shoot");
			idleGun.gameObject.SetActive (false);
			shootGun.gameObject.SetActive (true);
			return;
		}
		anim.ResetTrigger (0);
		if (clipNum == 2) {
			idleGun.gameObject.SetActive (false);
		} else {
			idleGun.gameObject.SetActive (true);
		}
			anim.SetInteger ("animState", clipNum);
			shootGun.gameObject.SetActive (false);
			TakeMeAway ();
		
	}

	private void TakeMeAway(){
		Destroy (this.gameObject, 10F);
	}	

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
//			float step = speed * Time.deltaTime;
//			this._transform.position = Vector3.MoveTowards (_transform.position, target.position, step);
		}
	}
}
