using UnityEngine;
using System.Collections;

public class SwatScript : MonoBehaviour {

	Animator anim;
	public float speed;
	private Transform target;
	public GameObject idleGun;
	public GameObject walkGun;
	public GameObject gunPoint;
	public Transform vision;

	private GameController gameController;
	private AudioSource _audio;
	private bool playerCaught;
	private NavMeshAgent agent;
	private Transform _transform;
	private bool isDead;
	private int hits;
	private Vector3 updateTarget;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = gameObject.GetComponent<Animator> ();
		_transform = GetComponent<Transform> ();
		playerCaught = false;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		_audio = GetComponent<AudioSource> ();
		isDead = false;
		hits = 0;

	}
	
	// Update is called once per frame
	void Update () {
//		float step = speed * Time.deltaTime;
//		this._transform.position = Vector3.MoveTowards (_transform.position, target.position, step);

		RaycastHit hit;

		updateTarget = target.position;

		if (Physics.Raycast (vision.transform.position, vision.transform.forward , out hit, 100)) {

			if (hit.transform.gameObject.tag == "Player") {
				playerCaught = true;
			}
		}

		if (playerCaught) {
			FollowPlayer ();
		}
	}

	public void setAmimation(int clipNum){

		if (clipNum == 2) {
			idleGun.SetActive (false);
			walkGun.SetActive (false);
			gunPoint.SetActive (false);
			anim.SetInteger ("animState", clipNum);
			_audio.Play ();
			isDead = true;
			TakeMeAway();
		} 
		if (clipNum == 1) {
			anim.SetInteger ("animState", clipNum);
			idleGun.SetActive (false);
			walkGun.SetActive (true);
		}
		
	}

	private void FollowPlayer(){
		if (Vector3.Distance(_transform.position ,updateTarget) < 3.5F && hits < 3) {
			StopNavMesh ();
		} else {
			setAmimation (1);
			agent.Resume ();
			agent.SetDestination (updateTarget);
		}
	}

	private void TakeMeAway(){
		Destroy (this.gameObject, 4F);
	}	

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			if (!isDead) {
				FollowPlayer ();
			}
		}
	}

	public void StopNavMesh (){
		agent.Stop ();
	}

	public void SetHits(){
		hits++;
		FollowPlayer ();
		if (hits > 3) {
			setAmimation (2);
			StopNavMesh ();
			isDead = true;
			gameController.SetEnemyKill ();
			gameController.SetScore (100);
		}
	}
}
