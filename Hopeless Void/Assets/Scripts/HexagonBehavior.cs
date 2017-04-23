using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingAction {
	IDLE,
	NONE,
	FACTORY,
	CAPITALE,
	ENERGY
};

public class HexagonBehavior : MonoBehaviour {

	//Properties
	public SpriteRenderer selfRenderer;
	public bool isFlat;
	public Vector2 coordinates;
	//GamePlay
	public BuildingAction building;
	public BuildingAction action;

	public SpriteRenderer buildingRenderer;
	public SpriteRenderer effectRenderer;

	public int population;
	public int popMax;
	public int locked;

	[HideInInspector]
	public PopulationOnHexagonBehavior populationOnHexagon;

	public void HexagonInitialize(bool isFlat, Sprite sprite, Vector2 c){
		this.isFlat = isFlat;
		selfRenderer.sprite = sprite;
		this.coordinates = c;
		building = BuildingAction.NONE;
		action = BuildingAction.NONE;
		populationOnHexagon = GetComponent<PopulationOnHexagonBehavior> ();
		popMax = ConstantBoard.popAction [BuildingAction.NONE];
	}

	public void computeRessources(){
		if (action == BuildingAction.NONE && !isFlat) GameBoard.instance.modifyParameters (ConstantBoard.effectAction [BuildingAction.IDLE]); // IF harvesting a mountain
		else
			GameBoard.instance.modifyParameters ( ConstantBoard.effectAction [action]);
		print (action.ToString ());
	}

	public bool commit() {
		GUIHandler.instance.Close ();
		if (action == BuildingAction.IDLE)
			return true;
		else if (action == BuildingAction.ENERGY) {
			computeRessources ();
		}
		else {
			if (population == popMax) {
				if (action == building) {
					computeRessources ();
				} else {				
					building = action;
					popMax = ConstantBoard.popAction [building];
					buildingRenderer.sprite = ConstantBoard.sprites 
						[ConstantBoard.idBuilding[action]];
				}

			}
		}
		GameBoard.instance.updateInterfaceParameters ();

		return true;
	}

	public bool addPopulation(int addPop){
		if (population + addPop <= popMax && population + addPop >= 0 
			&& GameBoard.instance.occupiedPopulation + addPop <= GameBoard.instance.Parameters[3]) {
			population += addPop; 
			GameBoard.instance.occupiedPopulation += addPop;
		}
		else
			return false; 
		GameBoard.instance.updateInterfaceParameters ();
		return true;

	}

	public int getMaxPopulation(){
		return popMax;
	}

	public string actionToString (BuildingAction action){
		if (action == this.building)
			return "Harvesting "+action;
		else
			return ( (action == BuildingAction.NONE) ? "Destroy building" : "Building " + action ) ;
	}



	public GameObject eaten;

	public void collapse (){ // A modifier
		GameBoard.instance.Parameters [3] -= population;
		GameBoard.instance.occupiedPopulation -= population;
		//Jouer l'animation
		GameObject g = Instantiate(eaten,transform);
	}

	public bool setAction(BuildingAction action){
		GUIHandler.instance.Close ();
		if (action == BuildingAction.ENERGY) {
			popMax = ConstantBoard.popAction [action];
		}else
		if (action != BuildingAction.IDLE) {
			if (action != building) {
				if (isSuperior (GameBoard.instance.Parameters, neg (ConstantBoard.effectConstruction [action]))) {
					GameBoard.instance.modifyParameters (ConstantBoard.effectConstruction [action]);
					popMax = ConstantBoard.popConstruction [action];
					GUIHandler.instance.Close ();
					buildingRenderer.sprite = ConstantBoard.sprites 
						[ConstantBoard.idBuilding[action]];
				}else {
					return false;
				}
			}
			if (action == building && isSuperior (GameBoard.instance.Parameters, ConstantBoard.effectConstruction [action]))
				return false;
		}
		this.action = action;
		GameBoard.instance.updateInterfaceParameters ();
		return true;
	}
		

	public bool isSuperior(int[] l1, int[] l2){
		bool superior = true;
		for (int i = 0; i < l1.Length; i++) {
			superior = superior && (l1 [i] > l2 [i]);
		}
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
		if (action.action == BuildingAction.NONE && !isFlat) { // if harvesting or constructing a Mountain
			panel.name = ConstantBoard.nameBuilding [BuildingAction.IDLE];
			panel.action = (action.isAction) ? ConstantBoard.nameAction [action.action] :
			("Build " + ConstantBoard.nameBuilding [action.action]);
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBackground [action.action]];
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBackground [action.action]];
		}else if (action.action == BuildingAction.IDLE) {
			panel.name = ConstantBoard.nameBuilding [building];
			panel.action = "Nothing";
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBackground [building]];
		} else if (action.action == BuildingAction.ENERGY) {
			panel.name = "Void";
			panel.action = "Harvest Energy";
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBackground [building]];
		} else {
			panel.name = ConstantBoard.nameBuilding [action.action];
			panel.action = (action.isAction) ? ConstantBoard.nameAction [action.action] :
			("Build " + ConstantBoard.nameBuilding [action.action]);
			panel.background = ConstantBoard.backgrounds [ConstantBoard.idBackground [action.action]];
		}
		panel.id = action;
		panel.denumerator = (action.isAction) ? ConstantBoard.popAction [action.action] : ConstantBoard.popConstruction [action.action];
		panel.numerator = 0;
		panel.actionEffect = (action.isAction) ? ConstantBoard.effectAction [action.action] :
			ConstantBoard.effectConstruction [action.action];
		panel.populationNeeded = (action.isAction) ? ConstantBoard.popAction [action.action] :
			ConstantBoard.popConstruction [action.action];

		return panel;
	}

	public ActionPanel[] getActionPlanelList (){
		ActionPanel[] panelList = new ActionPanel[ConstantBoard.BuildingActionList.Length 
			+ ( (GameBoard.instance.destructible(this) )?1 : 0)];
		panelList [0] = toActionPanel( 
			new Action(action,action == building || action == BuildingAction.ENERGY) 
		);
		int i = 1;
		foreach (BuildingAction possibleAction in ConstantBoard.BuildingActionList) {
			if (possibleAction != action){
				panelList [i] = toActionPanel (
					new Action(possibleAction,possibleAction == building || possibleAction == BuildingAction.ENERGY)
				);
				i++;
			}
		}
		if (GameBoard.instance.destructible(this) && action != BuildingAction.ENERGY) 
			panelList [ConstantBoard.BuildingActionList.Length] = 
				toActionPanel ( new Action(BuildingAction.ENERGY,true) );
		return panelList;
			
	}

	void OnMouseEnter() {
		GUIHandler.instance.Highlight (this);

	}

	void OnMouseExit() {
		GUIHandler.instance.UnHighlight (this);
	}
}
