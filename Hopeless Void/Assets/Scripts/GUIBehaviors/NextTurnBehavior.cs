using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnBehavior : MonoBehaviour {

	private Image image;

	new public Sprite light;
	public Sprite dark;

	private BoxCollider2D box;
	private RectTransform rect;

	void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
		image = GetComponent<Image> ();
		print (image);
		SetAnimation (false);
	}

	public void Update(){
		box.size = new Vector2(rect.rect.width,rect.rect.height);
		box.offset = new Vector2 (-box.size.x / 2, 0f);
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
		GameBoard.instance.commit ();
	}
}
