using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	//Creamos una variable donde asignaremos valores a los spawnpoints.
	// 1 --> Necesitamos una sala con una puerta hacia ABAJO.
	// 2 --> Necesitamos una sala con una puerta hacia ARRIBA.
	// 3 --> Necesitamos una sala con una puerta hacia la IZQUIERDA.
	// 4 --> Necesitamos una sala con una puerta hacia la DERECHA.
	public int openingDirection;


	//Creamos una variable que llame a nuestro script "RoomTemplates"
	private RoomTemplates templates;
	//Creamos una variable llamada "rand" que utilizaremos luego.
	private int rand;
	//Asignamos el valor false a la variable "spawned".
	public bool spawned = false;
	public float waitTime = 4f;

	void Start(){
		Destroy(gameObject, waitTime);
		//Buscamos dentro de nuesto script "RoomTemplates" los objetos tageados con "Rooms", especificamente el objeto anclado a este "RoomTemplates"
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		//Invocamos a iniciar la función Spawn.
		Invoke("Spawn", 0.1f);
	}

	//"void Spawn" sera la condicional que se encarge de que las salas spawneen de forma correcta. 
	//Y aqui utilizamos la variable spawned para que la condición se cumpla.
	void Spawn(){
		if(spawned == false){
			//Utilizamos rand para que busque dentro de "RoomTemplates" las salas alamcenadas con los valores asignados a los spawnpoints.
				//Y se encargue de colocarlas a su respectiva posición.
			if(openingDirection == 1){
				//Spawneara una sala con puerta hacia ABAJO.
				rand = Random.Range(0, templates.downRooms.Length);
				Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
			} else if(openingDirection == 2){
				//Spawneara una sala con puerta hacia ARRIBA.
				rand = Random.Range(0, templates.upRooms.Length);
				Instantiate(templates.upRooms[rand], transform.position, templates.upRooms[rand].transform.rotation);
			} else if(openingDirection == 3){
				//Spawneara una sala con puerta hacia la IZQUIERDA.
				rand = Random.Range(0, templates.leftRooms.Length);
				Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
			} else if(openingDirection == 4){
				//Spawneara una sala con puerta hacia la DERECHA.
				rand = Random.Range(0, templates.rightRooms.Length);
				Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
			}
			//Despues de las condiciones anteriores asignamos a spawned el valor true para que se detenga.
			spawned = true;
		}
	}
	//Esta parte del codigo se encargara de evitar que se spawneen salas encima de otras o infinitas.	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("SpawnPoint")){
			if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
				Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
				Destroy(gameObject);
			} 
			spawned = true;
		}
	
	}
}
