using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideBehavior : MonoBehaviour {

	new public Sprite light;
	public Sprite dark;

	private Image image;

	public void Awake(){
		image = GetComponent<Image> ();
		SetAnimation (false);
	}

	private bool pushed = false;

	void SetAnimation(bool b){
		if (b) {
			image.sprite = dark;
		} else {
			image.sprite = light;
		}
	}

	void OnMouseEnter(){
		SetAnimation (true);
	}

	void OnMouseExit(){
		SetAnimation (false);
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			if (!pushed) {
				Pushed ();
				SetAnimation (false);
				pushed = true;
			}
		} else if (pushed) {
			SetAnimation (true);
			pushed = false;
		}
	}

	void Pushed(){
		GUIHandler.instance.Close ();
	}
}
