using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour {

	public GameObject stage2PickUp;
	public GameObject stage2;
	public float speed;
	public GameObject player;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		keyboardMovement();
	}

	void keyboardMovement() {
		//		float moveHorizontal = Input.GetAxis ("Horizontal");
		//		float moveVertical = Input.GetAxis ("Vertical");
		//
		//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//		rb.AddForce (movement * speed);
		if (!player.activeSelf) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.Translate (Vector3.up * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.Translate (Vector3.down * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (Vector3.left * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Translate (Vector3.right * Time.deltaTime * speed);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.name) {
		case "Stage2PickUp":
			other.gameObject.SetActive (false);
			stage2.gameObject.SetActive (true);
			break;
		case "GreenColor":
			stage2.gameObject.SetActive (false);
			ChangeColor ("Green");
			break;
		case "RedColor":
			stage2.gameObject.SetActive (false);
			ChangeColor ("Red");
			break;
		case "YellowColor":
			stage2.gameObject.SetActive (false);
			ChangeColor ("Yellow");
			break;
		}
	}

	void ChangeColor(string color) {
		Color whateverColor;
		if (color == "Green") {
			whateverColor = Color.green;
		} else if (color == "Red") {
			whateverColor = Color.red;
		} else {
			whateverColor = Color.yellow;
		}

		Material newMaterial = new Material(Shader.Find("Standard"));
		newMaterial.color = whateverColor;

		GameObject player = rb.gameObject;
		MeshRenderer gameObjectRenderer = player.GetComponent<MeshRenderer>();
		gameObjectRenderer.material = newMaterial ;
	}

}
