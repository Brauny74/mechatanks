using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretBehavior : MonoBehaviour {

	protected GameObject Player;

	public GameObject Projectile;

	public float BetweenShotsCooldown = 1.5f;
	public float ReloadTime = 5f;

	public int ClipSize = 5;
	protected int CurrentClip;

	public bool OnReload = false;
	protected bool OnCooldown = false;
	protected Animator _anim;

	public float ShootingDistance;

	AnimatorClipInfo[] t_CurrentClipInfo;
	float t_CurrentClipLength;
	public bool IsShooting = false;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		CurrentClip = ClipSize;
		_anim = gameObject.GetComponent <Animator> ();
	}

	protected bool CheckPlayerInDistance(){				
		if (Vector2.Distance (transform.position, Player.transform.position) <= ShootingDistance) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsReloading(){
		return OnReload;
	}

	void Shoot(){
		if (!OnReload && !OnCooldown) {
			GameObject.Instantiate (Projectile, transform.position, transform.rotation);
			CurrentClip--;
			OnCooldown = true;
			StartCoroutine (Cooldown ());
			_anim.SetBool ("IsShooting", true);
			StartCoroutine (PauseToPlayAnimation ());
			if (CurrentClip == 0) {
				OnReload = true;
				IsShooting = false;
				transform.rotation = Quaternion.LookRotation (Vector3.forward, Player.transform.position - transform.position);
				StartCoroutine (Reload ());
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (!IsShooting && CheckPlayerInDistance ()) {
			IsShooting = true;
		}
		if (IsShooting) {
			Shoot ();
		}
		if (OnCooldown || !IsShooting) {
			transform.rotation = Quaternion.LookRotation (Vector3.forward, Player.transform.position - transform.position);
		}
	}

	IEnumerator PauseToPlayAnimation(){
		t_CurrentClipInfo = _anim.GetCurrentAnimatorClipInfo (0);
		t_CurrentClipLength = t_CurrentClipInfo [0].clip.length;
		yield return new WaitForSeconds (t_CurrentClipLength);
		_anim.SetBool ("IsShooting", false);
	}

	IEnumerator Cooldown(){
		yield return new WaitForSeconds (BetweenShotsCooldown);
		OnCooldown = false;
		_anim.SetBool ("IsShooting", false);
	}

	IEnumerator Reload(){
		yield return new WaitForSeconds (ReloadTime);
		OnReload = false;
		CurrentClip = ClipSize;
	}


}
