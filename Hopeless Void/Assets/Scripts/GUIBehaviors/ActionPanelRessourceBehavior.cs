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

	public void Show(RessourceInfo[] infos){
		for (int i = 0; i < infos.Length; i++) {
			places [i].image.sprite = infos [i].sprite;
			places [i].text.text = (infos [i].quantity>0 ? "+" : "") + infos [i].quantity.ToString();
		}
	}
}
