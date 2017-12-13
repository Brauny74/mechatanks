using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretProjectileBehavior : MonoBehaviour {
	public GameObject Projectile;
	public float CoolDownTime = 1f;
	public bool OnCooldown = false;

	public int Magazine = 2;
	public float ReloadTime = 2f;
	public Text AmmoText;

	protected int CurrentLoad;
	protected bool OnReload = false;

	public Rigidbody2D parentRB;

	public int AddDamage = 0;

	public float Spread = 0f;

	public AudioClip ShotSound;
	protected AudioSource _as;

	// Use this for initialization
	void Start () {
		//parentRB = transform.parent.parent.GetComponent <Rigidbody2D> ();
		CurrentLoad = Magazine;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !OnCooldown && !OnReload) {			
			GameObject tmp_cb = Instantiate (Projectile, transform.position, transform.rotation);
			ProjectileMovement tmp_cb_c = tmp_cb.GetComponent <ProjectileMovement> ();
			tmp_cb_c.InitialVelocity = parentRB.velocity;
			//tmp_cb_c.Damage += AddDamage;
			OnCooldown = true;
			StartCoroutine (Cooldown ());
		}
	}

	IEnumerator Cooldown(){
		yield return new WaitForSeconds (CoolDownTime);
		OnCooldown = false;
	}

	IEnumerator Reload(){
		yield return new WaitForSeconds (ReloadTime);
		OnCooldown = false;
		OnReload = false;
	}
}
