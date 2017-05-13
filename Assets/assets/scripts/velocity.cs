using UnityEngine;
using System.Collections;

public class velocity : MonoBehaviour {

	private string aux;

	public void SetVelocity(int x){
		for (int i=1; i+4<x; i++) {
//			aux=i+"d";
//			Destroy(GameObject.Find(aux));
			GameObject.Find(i.ToString()).GetComponent<MeshRenderer>().enabled=true;
		}
	}
}
