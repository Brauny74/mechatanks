using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehavior : MonoBehaviour {

	public GameObject player;
	public AIPath mov;
	public EnemyTurretBehavior turret;
	public float RepathDistance = 8f;

	protected bool HasPath = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mov = gameObject.GetComponent<AIPath> ();
		mov.target = player.transform;
	}

	void StartMoving(){
		if (!HasPath) {
			HasPath = true;
			mov.canMove = true;
			mov.canSearch = true;
			mov.target = player.transform;
			mov.SearchPath ();
		}
	}

	void StopMoving(){
		HasPath = false;
		mov.canMove = false;
		mov.canSearch = false;
	}

	public static RaycastHit2D VisibleRayCast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color){
		Debug.DrawRay (rayOriginPoint, rayDirection * rayDistance, color);
		return Physics2D.Raycast(rayOriginPoint,rayDirection,rayDistance,mask);		
	}

	public bool IsPlayerTooFar(){
		if (Vector2.Distance (transform.position, player.transform.position) >= RepathDistance) {
			return true;
		} else {
			return false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (mov.TargetReached) {
			StopMoving ();
		}
		if (IsPlayerTooFar ()) {
			StartMoving ();
		}
	}

	void OnColliderEnter2D(Collider2D other){
		if (other.tag == "Player") {
			StopMoving ();
			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
			rb.AddForce (player.transform.position - transform.position * 100 * -1);
		}
	}

}
