using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleBarBehavior : MonoBehaviour {

	public PeopleArrowBehavior left;
	public PeopleArrowBehavior right;
	public Text fraction;

	private BottomBarBehavior bar;

	void Awake() {
		left.SetBar (this);
		right.SetBar (this);

		bar = GetComponentInParent<BottomBarBehavior> ();
	}

	public void Refresh(int numerator, int denumerator) {
		if (numerator <= 0) {
			right.gameObject.SetActive (true);
			left.gameObject.SetActive (false);
		} else if (numerator >= denumerator) {
			right.gameObject.SetActive (false);
			left.gameObject.SetActive (true);
		} else {
			right.gameObject.SetActive (true);
			left.gameObject.SetActive (true);
		}
		fraction.text = numerator + " / " + denumerator;
	}

	public void Mod(int i) {
		bar.Mod (i);
	}
}
