using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelRessourceBehavior : MonoBehaviour {

	public struct RessourceInfo{
		public Sprite sprite;
		public int quantity;
	}

	[System.Serializable]
	public struct RessourcePlace{
		public Image image;
		public Text text;
	}

	public RessourcePlace[] places;
	public Text people;

	public void Show(List<RessourceInfo> infos, int k){
		for (int i = 0; i < 3; i++) {
			places [i].image.gameObject.SetActive (false);
			places [i].text.gameObject.SetActive (false);
		}
		for (int i = 0; i < infos.Count; i++) {
			places [i].image.gameObject.SetActive (true);
			places [i].text.gameObject.SetActive (true);
			places [i].image.sprite = infos [i].sprite;
			places [i].text.text = (infos [i].quantity>0 ? "+" : "") + infos [i].quantity.ToString();
		}
		people.text = k.ToString ();
	}
}
