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
		action = BuildingAction.NONE;
		remainingWork = 0; 
	}

	public void computeRessources(){
		if (action == BuildingAction.NONE && !isFlat) GameBoard.instance.modifyParameters (remainingWork / ConstantBoard.popAction [action] 
			, ConstantBoard.effectAction [BuildingAction.IDLE]); // IF harvesting a mountain
		else
			GameBoard.instance.modifyParameters (remainingWork / ConstantBoard.popAction [action] 
			, ConstantBoard.effectAction [action]);
	}

	public bool commit() {
		if (action == BuildingAction.IDLE)
			return true;
		else {
			remainingWork += population;
			//setAction (selectedAction);
			if (action == building) {
				computeRessources ();
				remainingWork = remainingWork % ConstantBoard.popAction [action];
			} else {				
				if (remainingWork >= ConstantBoard.popConstruction [action]) {
					remainingWork = 0;
					building = action;
					selfRenderer.sprite = ConstantBoard.sprites [action.ToString ()];
				}
			}
			return action == building;
		}
	}

	public bool addPopulation(int addPop){
		if (population + addPop < ConstantBoard.popMax [building] && population + addPop >= 0) {
			population += addPop; 
			GameBoard.instance.occupiedPopulation += addPop;
		}
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

	public void collapse (){ // A modifier
		GameBoard.instance.Parameters [3] -= population;
		//Jouer l'animation
		Destroy(gameObject);
	}

	public bool setAction(BuildingAction action){
		if (action != BuildingAction.IDLE) {
			if (action != building) {
				if (isSuperior (GameBoard.instance.Parameters, neg (ConstantBoard.effectConstruction [action])))
					return false;
				else {
					GameBoard.instance.modifyParameters (ConstantBoard.effectConstruction [action]);
					remainingWork = 0;
				}
			}
			if (action == building && isSuperior (GameBoard.instance.Parameters, ConstantBoard.effectConstruction [action]))
				return false;
		}
		this.action = action;
		return true;
	}
		

	public bool isSuperior(int[] l1, int[] l2){
		bool superior = true;
		for (int i = 0; i < l1.Length; i++)
			superior = superior && (l1 [i] > l2 [i]);
		return superior;
	}
	public int[] neg(int[] l){
		int[] negL = new int[l.Length];
		for (int i = 0; i < l.Length; i++)
			negL[i] = -l[i];
		return negL;
	}

	public ActionPanel toActionPanel (Action action){
		ActionPanel panel = new ActionPanel ();
		if (action.action == BuildingAction.NONE && !isFlat) // if harvesting or constructing a Mountain
			panel.name = ConstantBoard.nameBuilding [BuildingAction.IDLE];
		else
			panel.name = ConstantBoard.nameBuilding [building];
		if (action.action == BuildingAction.IDLE) {
			panel.action = "Nothing";
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBuilding [building]];
		} else {
			panel.action = (action.isAction) ? ConstantBoard.nameAction [action.action] :
			("Build " + ConstantBoard.nameBuilding [action.action]);
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBuilding [action.action]];
		}
		panel.id = action;
		panel.denumerator = (action.isAction) ? ConstantBoard.popAction [action.action] : ConstantBoard.popConstruction [action.action];
		panel.numerator = remainingWork;

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
		}
		remainingWork = memory;
		return panelList;
			
	}

	void OnMouseEnter() {
		GUIHandler.instance.Highlight (this);

	}

	void OnMouseExit() {
		GUIHandler.instance.UnHighlight (this);
	}
}
