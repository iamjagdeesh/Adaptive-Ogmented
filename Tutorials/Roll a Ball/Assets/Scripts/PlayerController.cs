using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public GameObject stage1;
	public GameObject stage2;
	public GameObject stage2PickUp;
	public GameObject cube;
	public GameObject cylinder;
	public GameObject capsule;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate() {
		mobileMovement();
		// keyboardMovement();
	}

	void keyboardMovement() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	void mobileMovement() {
		Vector3 dir = Vector3.zero;
		// we assume that device is held parallel to the ground
		// and Home button is in the right hand
		// remap device acceleration axis to game coordinates:
		//  1) XY plane of the device is mapped onto XZ plane
		//  2) rotated 90 degrees around Y axis
		dir.x = -Input.acceleration.y;
		dir.z = Input.acceleration.x;

		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();

		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		// Move object
		rb.transform.Translate(dir * speed);
		// rb.AddForce(dir * speed);
	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.name) {
			case "Stage1PickUp":
				other.gameObject.SetActive (false);
				stage1.SetActive (true);
				break;
			case "CubeObject":
				stage2PickUp.SetActive (true);
				capsule.SetActive (false);
				cylinder.SetActive (false);
				gameObject.SetActive (false);
				break;
			case "CylinderObject":
				stage2PickUp.SetActive (true);
				cube.SetActive (false);
				capsule.SetActive (false);
				gameObject.SetActive (false);
				break;
			case "CapsuleObject":
				stage2PickUp.SetActive (true);
				cube.SetActive (false);
				cylinder.SetActive (false);
				gameObject.SetActive (false);
				break;
		}


	}

	void ChangeColor() {
		GameObject player = rb.gameObject;
		Color whateverColor = new Color(0.6f,0.5f,0.5f,1);

		MeshRenderer gameObjectRenderer = player.GetComponent<MeshRenderer>();

		Material newMaterial = new Material(Shader.Find("Standard"));

		newMaterial.color = whateverColor;
		gameObjectRenderer.material = newMaterial ;
	}
}