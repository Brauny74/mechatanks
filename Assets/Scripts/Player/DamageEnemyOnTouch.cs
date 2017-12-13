﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemyOnTouch : MonoBehaviour {

	public int Damage = 1;

	public bool DiesOnTouch;

	protected Animator _anim;
	protected ProjectileMovement mov;
	protected bool AllowToMove = true;
	// Use this for initialization
	void Start () {
		_anim = gameObject.GetComponent <Animator> ();
		mov = gameObject.GetComponent <ProjectileMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {			
			//EnemyHealth _eh = other.gameObject.GetComponent <EnemyHealth> ();
			//_eh.SetDamage(Damage);
			if (DiesOnTouch) {
				AllowToMove = false;
				if (_anim != null) {
					float delay = 0.1f;
					mov.Stop ();
					Destroy (gameObject, _anim.GetCurrentAnimatorStateInfo (0).length + delay);
				} else {
					Destroy (gameObject);
				}
			}
		}
	}
}
