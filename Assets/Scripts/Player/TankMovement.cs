using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {

	public float Speed = 1f;
	public float rotation = -1f;
	public float MaxStirring = 1f;

	public Vector2 MousePos;

	protected float InputThrottle;
	protected float InputStirring;
	protected Rigidbody2D _rb;
	protected bool IsMoving = false;

	// Use this for initialization
	void Start () {
		_rb = gameObject.GetComponent <Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		InputThrottle = Input.GetAxis ("Vertical");
		InputStirring = Input.GetAxis ("Horizontal");
	}

	float ExpNumberTo (float expNum, float expTo, float delta){
		float res = expNum;
		if (expNum < expTo) {
			res = expNum + delta;
		}
		if (expNum > expTo) {
			res = expNum - delta;
		}
		if (Mathf.Abs (expNum - expTo) < delta) {
			res = expTo;
		}
		return res;
	}

	void FixedUpdate(){
		if (InputThrottle != 0) {			
			_rb.AddForce (transform.up * InputThrottle * Speed);
		}
		MaxStirring = 300f;
		if (InputStirring != 0) {
			if (InputStirring < MaxStirring) {
				if (InputThrottle >= 0) {
					_rb.AddTorque (rotation * InputStirring);
				}
				if (InputThrottle < 0) {
					_rb.AddTorque (0 - rotation * InputStirring);
				}
			}
		} else {
			_rb.angularVelocity = ExpNumberTo (_rb.angularVelocity, 0f, 0.5f);
		}
	}
}
