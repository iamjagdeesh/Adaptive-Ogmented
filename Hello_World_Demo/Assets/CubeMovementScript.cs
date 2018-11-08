using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovementScript : MonoBehaviour {
	Vector3 temp = new Vector3(1,0,0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += temp * Time.deltaTime;
	}
}
