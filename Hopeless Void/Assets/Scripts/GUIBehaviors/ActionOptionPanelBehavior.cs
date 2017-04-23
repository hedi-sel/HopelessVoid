using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOptionPanelBehavior : ActionPanelBehavior {

	public ActionHolderBehavior holder;
	public int id;

	private BoxCollider2D box;
	private RectTransform rect;

	void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
	}

	void Update(){
		box.size = new Vector2(rect.rect.width,rect.rect.height);
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			holder.GotClicked (id);
		}
	}
}
