using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavButtonBehavior : MonoBehaviour {

	public NavActionBehavior action;
	private Animator animator;
	private bool isHovered;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void OnMouseEnter() {
		animator.SetBool ("isHovered", true);
		isHovered = true;
	}

	void OnMouseExit() {
		animator.SetBool ("isHovered", false);
		isHovered = false;
	}

	void Update() {
		if(isHovered && Input.GetMouseButtonDown(0)){
			animator.SetBool ("isHovered", false);
			isHovered = false;
			action.Action ();
		}
	}
}
