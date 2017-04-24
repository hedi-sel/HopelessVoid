using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHolderBehavior : MonoBehaviour {

	private BoxCollider2D box;
	private RectTransform rect;
	private BottomBarBehavior bottom;

	[HideInInspector]
	public bool opened;

	private ActionPanel[] actions;
	private ActionPanelBehavior actionPanelBehavior;

	void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
		bottom = GetComponentInParent<BottomBarBehavior> ();
		actionPanelBehavior = GetComponentInChildren<ActionPanelBehavior> ();
	}

	void Update(){
		box.size = new Vector2(rect.rect.width,rect.rect.height);
	}

	private bool pushed = false;

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			if (!pushed) {
				Pushed ();
				pushed = true;
			}
		} else if (pushed) {
			pushed = false;
		}
	}


	public List<ActionOptionPanelBehavior> options = new List<ActionOptionPanelBehavior>();
	public GameObject panelTemplate;

	void Pushed(){
		if (opened) {
			Close ();
		} else {
			Open ();
		}
	}

	public void Open() {
		//SoundHandler.instance.playSound ("click0");
		opened = true;
		options.Clear ();
		for(int k = 1; k < actions.Length; k++){
			ActionOptionPanelBehavior action = Instantiate (panelTemplate, transform).GetComponent<ActionOptionPanelBehavior>();
			action.SetActionPanel (actions [k]);
			action.holder = this;
			action.id = k;
			float height = GetComponent<RectTransform> ().rect.height;
			action.transform.localPosition = new Vector3 (0f, k*(20f+height), 0f);
			options.Add (action);
		}
	}

	public void Close() {
		//SoundHandler.instance.playSound ("closing");
		opened = false;
		foreach (ActionOptionPanelBehavior a in options) {
			Destroy (a.gameObject);
		}
		options.Clear ();
	}

	public void SetActions(ActionPanel[] _actions){
		actions = _actions;
		actionPanelBehavior.SetActionPanel (_actions [0]);
	}

	public void GotClicked(int id){
		//SoundHandler.instance.playSound ("click2");
		if (bottom.hexagon.setAction (actions [id].id.action)) {
			actionPanelBehavior.SetActionPanel (GetActionPanel());
			SetActions (bottom.hexagon.getActionPlanelList ());
			Close ();
		}
	}

	public ActionPanel GetActionPanel(){
		return actions [0];
	}
}
