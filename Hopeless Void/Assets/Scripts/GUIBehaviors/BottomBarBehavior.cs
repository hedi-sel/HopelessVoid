using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarBehavior : MonoBehaviour {

	public PeopleBarBehavior people;

	public int peopleMax;
	public int peopleCur;

	public void Mod (int i){
		peopleCur = peopleCur + i;
		peopleCur = Mathf.Max (Mathf.Min (0, peopleCur), peopleMax);
		people.Refresh (peopleCur, peopleMax);
	}
}
