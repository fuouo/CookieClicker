using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieSpawner : MonoBehaviour {

	public const string AUTO_SPAWN_KEY = "AUTO_SPAWN_KEY";

	[SerializeField] GameObject cookieObject;
	[SerializeField] GameObject cookieSpawnerArea;

	void Start(){
		InvokeRepeating ("SpawnCookie", 0.0f, 1.0f);

		EventBroadcaster.Instance.AddObserver (EventNames.ON_SPAWN_COOKIE_OBJ, this.SpawnCookieObject );
	}

	void Update(){
		
	}

	void SpawnCookie(){

		Parameters parameter = new Parameters ();
		parameter.PutExtra (AUTO_SPAWN_KEY, true);
		EventBroadcaster.Instance.PostEvent (EventNames.ON_ADD_COOKIE, parameter);
	}


	void SpawnCookieObject(Parameters parameters){

		int spawnCount =  Mathf.FloorToInt(parameters.GetFloatExtra (StatsManager.COOKIE_PER_SECOND_KEY, 1.0f));

		Debug.Log (parameters.GetFloatExtra (StatsManager.COOKIE_PER_SECOND_KEY, 1.0f));

		for (int i = 0; i < spawnCount; i++) {

			GameObject cookie = Instantiate (cookieObject);
			cookie.transform.SetParent (cookieSpawnerArea.transform);
			var pos = new Vector3 (Random.Range (-0.5f, 0.5f), 0, 0);
			cookie.transform.localPosition = pos;
		}
	}






}
