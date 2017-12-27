using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	public float Speed = 3000f;
	public Vector3 InitialVelocity;
	public float LivingDistance = -1;

	protected Rigidbody2D _rb;
	protected Vector3 target;
	protected bool IsGivenInitialVelocity = false;
	protected Vector2 startingPosition;
	protected float PassedDistance = 0f;
	protected bool AllowToMove = true;
	protected Animator anim;

	// Use this for initialization
	void Start () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target = mousePos;
		_rb = gameObject.GetComponent <Rigidbody2D> ();	
		startingPosition = transform.position;
		_rb.velocity = InitialVelocity;
		anim = gameObject.GetComponent <Animator> ();
	}

	public void Stop(){
		AllowToMove = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (AllowToMove) {
			if (LivingDistance > 0) {
				PassedDistance = Vector3.Distance (transform.position, startingPosition);
				if (PassedDistance >= LivingDistance) {
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
	}

	public void Kill(){
		//Here we will change his state to dying.
		//Pause to let animation play.
		if (anim != null) {			
			anim.SetBool ("IsDead", true);
			Stop ();
			AnimatorClipInfo[] t_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo (0);
			Destroy (gameObject, t_CurrentClipInfo [0].clip.length);
		} else {
			Destroy (gameObject);
		}
	}

	void OnColliderEnter2D(Collider2D other){
		if (other.gameObject.layer == 8 || other.tag == "Player" || other.tag == "Enemy") {
			Kill ();
		}
	}
}
