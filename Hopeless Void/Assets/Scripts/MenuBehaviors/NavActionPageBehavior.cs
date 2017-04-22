using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavActionPageBehavior : NavActionBehavior {

	void Awake() {
	}

	override public void Action(){
		MenuBehavior menu = GetComponentInParent<MenuBehavior> ();
		menu.page.SetActive (false);
		menu.page = gameObject;
		gameObject.SetActive (true);
	}
}
