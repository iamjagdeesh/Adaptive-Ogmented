using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

	public InputField username;

	public InputField password;

	public GameObject loginObjectUI;

	//public GameObject arObjects;

	public GameObject userInfoUI;

	public void Login () {
		if (username.text.Equals ("jags") && password.text.Equals ("pwd")) {
			Debug.Log ("Login Successful!");
		}
		loginObjectUI.SetActive (false);
		DisplayUserInfoUI (username.text);
		SceneManager.LoadScene ("Task 2");

	}

	public void DisplayUserInfoUI (string user) {
		//Make a rest call here to fetch the details or send details from login()
		userInfoUI.SetActive (true);
		Text userName = userInfoUI.transform.GetChild (0).GetComponent<Text> ();
		Text userID = userInfoUI.transform.GetChild (1).GetComponent<Text> ();
		Text performance = userInfoUI.transform.GetChild (2).GetComponent<Text> ();
		userName.text = "Jagdeesh Basavaraju";
		userID.text = user;
		performance.text = "Doing OK!";
	}

	public void populateStaticInfo () {
		StaticGameInfo.userName = "Jagdeesh Basavaraju";
		StaticGameInfo.userID = username.text;
		StaticGameInfo.performance = "Doing OK!";
	}
}
