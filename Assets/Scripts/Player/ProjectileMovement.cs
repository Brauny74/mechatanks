using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	public float Speed = 3000f;
	public int Damage = 30;
	public Vector3 InitialVelocity;
	public float LivingDistance = -1;

	protected Rigidbody2D _rb;
	protected Vector3 target;
	protected bool IsGivenInitialVelocity = false;
	protected Vector2 startingPosition;
	protected float PassedDistance = 0f;

	// Use this for initialization
	void Start () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target = mousePos;
		_rb = gameObject.GetComponent <Rigidbody2D> ();	
		startingPosition = transform.position;
		_rb.velocity = InitialVelocity;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (LivingDistance > 0) {
			PassedDistance = Vector3.Distance(transform.position, startingPosition);
			if(PassedDistance >= LivingDistance){
				Kill ();
			}
		}
		if (IsGivenInitialVelocity) {
			_rb.AddForce (transform.up * Speed * Time.fixedDeltaTime);
		} else {
			_rb.velocity = InitialVelocity;
			IsGivenInitialVelocity = true;
		}
	}

	void Kill(){
		//Here we will change his state to dying.
		//Pause to let animation play.
		StartCoroutine (Die ());
	}

	IEnumerator Die(){
		yield return new WaitForSecondsRealtime (1f);
		Destroy (gameObject);
	}
}
