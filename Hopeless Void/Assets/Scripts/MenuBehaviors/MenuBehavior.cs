using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour {

	public GameObject page;

	void Awake(){
		GetComponent<Canvas> ().worldCamera = GameHandler.instance.GetComponent<Camera>();
		foreach(Transform child in transform){
			if (child.name == "Default") {
				page = child.gameObject;
				page.SetActive (true);
			} else {
				child.gameObject.SetActive (false);
			}
		}
	}
}
