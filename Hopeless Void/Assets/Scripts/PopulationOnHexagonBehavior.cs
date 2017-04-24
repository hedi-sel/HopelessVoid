using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationOnHexagonBehavior : MonoBehaviour {

	public GameObject humain;
	public GameObject skull;

	private const float RAYON = 1f;
	private int people;
	private List<GameObject> peoples = new List<GameObject>();

	public void SetPeople(int i){
		while (people < i) {
			people++;
			GameObject g = Instantiate (humain, transform);
			g.transform.localPosition = new Vector3 (Random.Range (-RAYON, RAYON), Random.Range (-RAYON, RAYON), -5f);
			peoples.Add (g);
		}

		while (people > i) {
			people--;
			GameObject g = peoples [0];
			peoples.RemoveAt (0);
			Destroy (g);
		}
	}

	public void KillOne(){
		people--;
		GameObject g = peoples [0];
		Instantiate (skull, g.transform.position, Quaternion.identity, transform);
		peoples.RemoveAt (0);
		Destroy (g);
	}
}
