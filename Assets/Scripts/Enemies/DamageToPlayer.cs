using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour {

	public int Damage = 5;
	public bool DeathOnTouch = false;

	protected bool Dead = false;
	protected ProjectileMovement mov;

	// Use this for initialization
	void Start () {
		mov = gameObject.GetComponent <ProjectileMovement> ();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {			
			PlayerHealth _otherHealth = other.gameObject.GetComponent<PlayerHealth> ();
			_otherHealth.DealDamage (Damage);
			if (DeathOnTouch && !Dead) {
				Dead = true;
				mov.Kill ();
			}
		}
	}
}
