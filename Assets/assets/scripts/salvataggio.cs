using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class salvataggio : MonoBehaviour {
	public static salvataggio meh;

	public bool tutorial=false;
	public bool inCorso=false;
	public int punteggio=0;
	public int giocate=0;
	public cella[] score=new cella[5]; 

	public bool partita=false;
	public bool setpalla=false;
	public int velocitaInz;
	public Vector2Serializer pallaVel;
	public Vector2Serializer pallaPos;
	public Vector3Serializer puntatore;

	// Use this for initialization
	void Start () {
		Load ();
		if (salvataggio.meh.inCorso) {
			GameObject.Find("continue").GetComponent<Button>().interactable=true;
		}
	}

	public void Awake()
	{
		if (meh == null) {
			DontDestroyOnLoad (gameObject);
			meh = this;
		}
		else if (meh != this) {
			Destroy(gameObject);
		}
	}

	public void Save(){
		BinaryFormatter bf=new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/saves.dat");
		playerData data = new playerData ();
		data.tutorial = tutorial;
		data.inCorso = inCorso;
		data.punteggio = punteggio;
		data.giocate = giocate;
		data.score = score;
		data.partita = partita;
		data.setpalla = setpalla;
		data.velocitaInz = velocitaInz;
		data.pallaVel = pallaVel;
		data.pallaPos = pallaPos;
		data.puntatore = puntatore;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/saves.dat")) {
			BinaryFormatter bf= new BinaryFormatter();
			FileStream file=File.Open(Application.persistentDataPath+"/saves.dat",FileMode.Open);
			playerData data=(playerData)bf.Deserialize(file);
			file.Close();
			tutorial=data.tutorial;
			inCorso=data.inCorso;
			punteggio=data.punteggio;
			giocate=data.giocate;
			score=data.score;
			partita = data.partita;
			setpalla = data.setpalla;
			velocitaInz = data.velocitaInz;
			pallaVel = data.pallaVel;
			pallaPos = data.pallaPos;
			puntatore = data.puntatore;
		}
	}

	public void saveScore(){
		for (int i=0; i<score.Length; i++) {

			if(score[i].plays==0){
				score[i].points=punteggio;
				score[i].plays=giocate;
				break;
			}

			else if(score[i].plays<giocate){
				cella aux=new cella(score[i].plays,score[i].points);
				score[i].plays=giocate;
				score[i].points=punteggio;
				cella aux2;
				for(int x=i+1;x<score.Length;x++){
					if(score[x].plays==0){
						score[x].points=aux.points;
						score[x].plays=aux.plays;
						break;
					}
					aux2=new cella(score[x].plays,score[x].points);
					score[x]=aux;
					aux=aux2;
				}
				break;
			}

			else if(score[i].plays==giocate){
				if(score[i].points<punteggio){
					cella aux=new cella(score[i].plays,score[i].points);
					score[i].plays=giocate;
					score[i].points=punteggio;
					cella aux2;
					for(int x=i+1;x<score.Length;x++){
						if(score[x].plays==0){
							score[x].points=aux.points;
							score[x].plays=aux.plays;
							break;
						}
						aux2=new cella(score[x].plays,score[x].points);
						score[x]=aux;
						aux=aux2;
					}
					break;
				}
			}
		}
	}
}
[Serializable]
public class cella{
	public int points;
	public int plays;

	public cella(int plays,int points){
		this.points = points;
		this.plays = plays;
	}
}

[Serializable]
public class playerData{
	public bool tutorial;
	public bool inCorso;
	public int punteggio;
	public int giocate;
	public cella[] score;

	public bool partita;
	public bool setpalla;
	public int velocitaInz;
	public Vector2Serializer pallaVel;
	public Vector2Serializer pallaPos;
	public Vector3Serializer puntatore;
}

[Serializable]
public class Vector2Serializer
{
	public float x;
	public float y;
	
	public void Fill(Vector2 v2)
	{
		x = v2.x;
		y = v2.y;
	}
	public Vector2 V2 { get { return new Vector2(x, y); } set { Fill(value); } }
}

[Serializable]
public class Vector3Serializer
{
	public float x;
	public float y;
	public float z;
	
	public void Fill(Vector3 v3)
	{
		x = v3.x;
		y = v3.y;
		z = v3.z;
	}
	public Vector3 V3 { get { return new Vector3(x, y, z); } set { Fill(value); } }
}
