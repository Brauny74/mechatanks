using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int MaxHealth = 100;
	protected int CurrentHealth;
	public Text HealthText;

	public float PushingForce = 500f;
	// Use this for initialization
	void Start () {
		CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (HealthText != null) {
			HealthText.text = CurrentHealth.ToString () + "/" + MaxHealth.ToString ();
		}
	}

	public void DealDamage(int damage){
		CurrentHealth -= damage;
		if (CurrentHealth < 0) {
			CurrentHealth = 0;
		}
	}

	void OnColliderEnter2D(Collider2D other){		
		if (other.tag == "Enemy") {
			Rigidbody2D otherRB = other.GetComponent<Rigidbody2D> ();
			otherRB.velocity = Vector2.zero;
			otherRB.AddForce(Vector2.down * PushingForce);
		}
	}
}
