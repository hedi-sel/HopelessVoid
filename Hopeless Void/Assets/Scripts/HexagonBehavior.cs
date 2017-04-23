using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingAction {
	IDLE,
	NONE,
	FACTORY,
	CAPITALE
};

public class HexagonBehavior : MonoBehaviour {

	//Properties
	public SpriteRenderer selfRenderer;
	public bool isFlat;
	public Vector2 coordinates;
	//GamePlay
	public BuildingAction building;
	public BuildingAction action;


	public int population;
	public int remainingWork;
	public int locked;

	public void HexagonInitialize(bool isFlat, Sprite sprite, Vector2 c){
		this.isFlat = isFlat;
		selfRenderer.sprite = sprite;
		this.coordinates = c;
		building = BuildingAction.NONE;
		remainingWork = 0; 
	}

	public void computeRessources(){
		GameBoard.instance.Ressources += (remainingWork / ConstantBoard.popAction [action]) * ConstantBoard.effectAction [action];
		Debug.Log ("Not enough ressources");
	}

	public bool commit() {
		remainingWork += population;
		//setAction (selectedAction);
		if (action == building) {
			computeRessources ();
			remainingWork = remainingWork % ConstantBoard.popAction [action];
		} else {				
			if (remainingWork >= ConstantBoard.popConstruction[action]) {
				remainingWork = 0;
				building = action;
				selfRenderer.sprite = ConstantBoard.sprites[action.ToString()];
			}
		}
		return action == building;
	}

	public bool addPopulation(int addPop){
		if ( population+addPop < ConstantBoard.popMax[building] && population+addPop >= 0)
			population += addPop; 
		else
			return false; 
		return true;
	}

	public int getMaxPopulation(){
		return ConstantBoard.popMax [building];
	}

	public string actionToString (BuildingAction action){
		if (action == this.building)
			return "Harvesting "+action;
		else
			return ( (action == BuildingAction.NONE) ? "Destroy building" : "Building " + action ) ;
	}

	/*public bool selectAction (BuildingAction action){
		if (action != building && isPositif(-ConstantBoard.effectConstruction [action] - GameBoard.instance.Ressources))
			return false;
		if (action == building && isPositif(-ConstantBoard.effectConstruction [action] - GameBoard.instance.Ressources))
			return false;
		selectedAction = action;
		return true;
	}*/

	public bool setAction(BuildingAction action){
		if (action != building && isPositif(-ConstantBoard.effectConstruction [action] - GameBoard.instance.Ressources))
			return false;
		if (action == building && isPositif(-ConstantBoard.effectConstruction [action] - GameBoard.instance.Ressources))
			return false;
		if (action != building)
			remainingWork = 0;
		this.action = action;
		return true;
	}

	public bool isPositif(Vector3 v){
		return v.x > 0 && v.y > 0 && v.z > 0;
	}

	public ActionPanel toActionPanel (Action action){
		ActionPanel panel = new ActionPanel ();
		panel.name = ConstantBoard.nameBuilding [building];
		panel.action = (action.isAction) ? ConstantBoard.nameAction [action.action] : "Build "+ConstantBoard.nameBuilding [action.action];
		panel.numerator = remainingWork;
		panel.denumerator = (action.isAction) ? ConstantBoard.popAction [action.action] : ConstantBoard.popConstruction [action.action];
		panel.id = action;
		panel.background = ConstantBoard.backgrounds [ ConstantBoard.idBuilding[action.action] ];
		return panel;
	}

	public ActionPanel[] getActionPlanelList (){
		ActionPanel[] panelList = new ActionPanel[ConstantBoard.BuildingActionList.Length];
		panelList [0] = toActionPanel( new Action(action,action == building) );
		int memory = remainingWork;
		remainingWork = 0;
		int i = 1;
		foreach (BuildingAction possibleAction in ConstantBoard.BuildingActionList) {
			if (possibleAction != action){
				panelList [i] = toActionPanel (new Action(possibleAction,possibleAction == building));
				i++;
			}
			if (i != ConstantBoard.BuildingActionList.Length)
				Debug.Log ("Caca au niveau de getActionPanelList");
		}
		remainingWork = memory;
		return panelList;
			
	}

	void OnMouseEnter() {
		//GUIHandler.instance.Highlight(this.transform.position);

	}
	void OnMouseOver() {
		Input.GetButtonDown ("Left Click");
		//GuiHandler.instance.SelectMe(this);
	}
	void OnMouseExit() {
		//GUIHandler.instance.UnHighlight();
	}
}
