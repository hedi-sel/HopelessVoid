using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOptionPanelBehavior : ActionPanelBehavior {

	public ActionHolderBehavior holder;
	public int id;

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			holder.GotClicked (id);
		}
	}
}
