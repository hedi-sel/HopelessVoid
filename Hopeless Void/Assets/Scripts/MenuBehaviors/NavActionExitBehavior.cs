using UnityEngine;
using UnityEngine.UI;

public class NavActionExitBehavior : NavActionBehavior {

	override public void Action(){
		Debug.Log ("Exiting...");
		Application.Quit ();
	}
}
