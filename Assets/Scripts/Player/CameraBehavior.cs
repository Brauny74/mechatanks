using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public GameObject ObjectToFollow;

	protected Transform ObjectTransform;
	// Use this for initialization
	void Start () {
		ObjectTransform = ObjectToFollow.GetComponent <Transform> ();
	}

	// Update is called once per frame
	void Update () {
		Vector2 t_V = ObjectTransform.position;
		Vector3 newPos = new Vector3 (t_V.x, t_V.y, -1);
		transform.position = newPos;
	}
}
