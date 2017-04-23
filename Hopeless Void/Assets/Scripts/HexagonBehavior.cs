using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Building {
	NONE,
	FACTORY,
	CAPITALE
};

public class HexagonBehavior : MonoBehaviour {

	public SpriteRenderer selfRenderer;
	public bool isFlat;
	public Vector2 coordinates;
	//GamePlay
	public Building building;
	public Building action;
	public int population;
	public int remainingWork;


	public void HexagonInitialize(bool isFlat, Sprite sprite, Vector2 c){
		this.isFlat = isFlat;
		selfRenderer.sprite = sprite;
		this.coordinates = c;
		building = Building.NONE;
	}

	public void commit() {
		remainingWork += population;
		if (action == building) {
			GameBoard.instance.Ressources += (remainingWork / ConstantBoard.popAction [action]) * ConstantBoard.effectAction [action];
		} else {
			if (remainingWork >= ConstantBoard.popConstruction[action]) {
				remainingWork = 0;
				building = action;
				selfRenderer.sprite = ConstantBoard.sprites[action.ToString()];
			}
		}

	}

	public bool addPopulation(int addPop){
		if ( population+addPop < ConstantBoard.popMax[building] && population+addPop >= 0)
			population += addPop; 
		else
			return false; 
		return true;
	}

	public string actionToString (Building action){
		if (action == this.building)
			return "Harvesting "+action;
		else
			return ( (action == Building.NONE) ? "Destroy building" : "Building " + action ) ;
	}

	public bool setAction(Building action){
		if (action != Building.CAPITALE)
			this.action = action;
		else
			return false;
		if (action != building)
			remainingWork = 0;
		return true;
	}

	

	void Update () {
		
	}
}
