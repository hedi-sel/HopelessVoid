using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PeopleArrowBehavior : MonoBehaviour {

	private PeopleBarBehavior bar;
	private Image image;

	new public Sprite light;
	public Sprite dark;

	public int mod;

	public void SetBar(PeopleBarBehavior _bar){
		bar = _bar;
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
			SoundHandler.instance.playSound ("swip");
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
		bar.Mod (mod);
	}
}
