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
	public int populationNeeded;
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
		textFraction.text = _actionPanel.denumerator.ToString();
		imageBackground.sprite = _actionPanel.background;
		transformBar.anchorMax = new Vector2(((float) _actionPanel.numerator)/ _actionPanel.denumerator,1);
		transformBar.offsetMax = Vector2.zero;
		List<ActionPanelRessourceBehavior.RessourceInfo> infos = new List<ActionPanelRessourceBehavior.RessourceInfo>();
		if (actionPanel.actionEffect.Length==0) {
			return;
		}
		if (actionPanel.actionEffect [0] != 0) {
			ActionPanelRessourceBehavior.RessourceInfo r;
			r.quantity = actionPanel.actionEffect [0];
			r.sprite = ConstantBoard.ressources ["Food"];
			infos.Add (r);
		}
		if (actionPanel.actionEffect [1] != 0) {
			ActionPanelRessourceBehavior.RessourceInfo r;
			r.quantity = actionPanel.actionEffect [1];
			r.sprite = ConstantBoard.ressources ["Metal"];
			infos.Add (r);
		}
		if (actionPanel.actionEffect [2] != 0) {
			ActionPanelRessourceBehavior.RessourceInfo r;
			r.quantity = actionPanel.actionEffect [2];
			r.sprite = ConstantBoard.ressources ["Cristal"];
			infos.Add (r);
		}
		if (actionPanel.actionEffect [3] != 0) {
			ActionPanelRessourceBehavior.RessourceInfo r;
			r.quantity = actionPanel.actionEffect [3];
			r.sprite = ConstantBoard.ressources ["Humain"];
			infos.Add (r);
		}
		if (actionPanel.actionEffect [4] != 0) {
			ActionPanelRessourceBehavior.RessourceInfo r;
			r.quantity = actionPanel.actionEffect [4];
			r.sprite = ConstantBoard.ressources ["Capsule"];
			infos.Add (r);
		}
		ressourcesBehavior.Show(infos);
	}
}
