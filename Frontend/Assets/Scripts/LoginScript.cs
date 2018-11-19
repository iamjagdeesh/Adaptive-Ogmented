using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

	public GameObject loginObjectUI;

	public GameObject registerObjectUI;

	public GameObject userInfoUI;

	public void Login () {
		string userName = loginObjectUI.transform.GetChild (0).GetComponent<InputField> ().text;
		string password = loginObjectUI.transform.GetChild (1).GetComponent<InputField> ().text;
		if (userName.Equals("jags") && password.Equals ("pwd")) {
			Debug.Log ("Login Successful!");
			loginObjectUI.SetActive (false);
			DisplayUserInfoUI (userName);
			populateStaticInfo (userName);
			SceneManager.LoadScene ("Task 1");
		}
	}

	public void Register () {
		string userName = registerObjectUI.transform.GetChild (0).GetComponent<InputField> ().text;
		string password = registerObjectUI.transform.GetChild (1).GetComponent<InputField> ().text;
		Dropdown levelOfExpertise = registerObjectUI.transform.GetChild (2).GetComponent<Dropdown> ();
		if (userName.Equals("jags") && password.Equals ("pwd")) {
			Debug.Log ("Register Successful!");
			registerObjectUI.SetActive (false);
			DisplayUserInfoUI (userName);
			populateStaticInfo (userName);
			SceneManager.LoadScene (levelOfExpertise.options[levelOfExpertise.value].text);
		}
	}

	public void DisplayUserInfoUI (string userName) {
		//Make a rest call here to fetch the details or send details from login()
		userInfoUI.SetActive (true);
		Text userNameText = userInfoUI.transform.GetChild (0).GetComponent<Text> ();
		userNameText.text = userName;
	}

	public void populateStaticInfo (string userName) {
		StaticGameInfo.userName = userName;
	}

}
