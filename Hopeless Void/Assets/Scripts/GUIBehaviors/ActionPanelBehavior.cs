using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ActionPanel {
	public string name;
	public string action;
	public int numerator;
	public int denumerator;
	public Sprite background;
	public Action id;
	public int[] actionEffect;
}

public class ActionPanelBehavior : MonoBehaviour {

	public Text textName;
	public Text textAction;
	public Text textFraction;
	public Image imageBackground;
	public RectTransform transformBar;
	public ActionPanelRessourceBehavior ressourcesBehavior;


	public ActionPanel actionPanel;

	void Awake(){
		if (actionPanel.name!="") {
			SetActionPanel (actionPanel);
		}
	}

	public void SetActionPanel(ActionPanel _actionPanel){
		actionPanel = _actionPanel;
		textName.text = _actionPanel.name;
		textAction.text = _actionPanel.action;
		textFraction.text = _actionPanel.numerator + "/" + _actionPanel.denumerator;
		imageBackground.sprite = _actionPanel.background;
		transformBar.anchorMax = new Vector2(((float) _actionPanel.numerator)/ _actionPanel.denumerator,1);
		transformBar.offsetMax = Vector2.zero;
	}
}
