using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieSpawner : MonoBehaviour, IBoundaryListener {



	public const string AUTO_SPAWN_KEY = "AUTO_SPAWN_KEY";

	[SerializeField] private GameObjectPool objectPool;
	[SerializeField] private Transform spawnPointY;

	[SerializeField] GameObject cookieObject;
	[SerializeField] GameObject cookieSpawnerArea;

	[SerializeField] private BoundaryHandler boundaryHandler;

	void Start(){
		this.objectPool.Initialize ();
		this.boundaryHandler.SetListener (this); 
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

		var spawnCount =  parameters.GetIntExtra (StatsManager.COOKIE_PER_SECOND_KEY, 1);

		if (!this.objectPool.HasObjectAvailable (spawnCount))
			return;
		
		APoolable[] poolableObjects = this.objectPool.RequestPoolableBatch (spawnCount);

//		for (int i = 0; i < spawnCount; i++) {
//
//			GameObject cookie = Instantiate (cookieObject);
//			cookie.transform.SetParent (cookieSpawnerArea.transform);
//			var pos = new Vector3 (Random.Range (-0.5f, 0.5f), 0, 0);
//			cookie.transform.localPosition = pos;
//		}
	}

	public void OnExitBoundary(APoolable poolableObject) {
		this.objectPool.ReleasePoolable (poolableObject);
	}






}
