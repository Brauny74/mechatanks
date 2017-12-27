using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int MaxHealth = 100;
	protected int CurrentHealth;
	// Use this for initialization
	void Start () {
		CurrentHealth = MaxHealth;
	}

	// Update is called once per frame
	void Update () {
	}

	public void DealDamage(int damage){
		CurrentHealth -= damage;
		if (CurrentHealth < 0) {
			CurrentHealth = 0;
			Destroy (gameObject, 1f);
		}
	}

	void OnColliderEnter2D(Collider2D other){		
		if (other.tag == "Player") {
			Rigidbody2D _rb = gameObject.GetComponent<Rigidbody2D> ();
			_rb.AddForce(Vector2.down * 50f);
		}
	}
}
