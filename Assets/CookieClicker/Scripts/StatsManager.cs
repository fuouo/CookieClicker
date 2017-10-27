using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {

	public const string COOKIE_PER_SECOND_KEY = "COOKIE_PER_SECOND_KEY";
	public const string COOKIE_AMOUNT_KEY = "COOKIE_AMOUNT_KEY";

	private static StatsManager sharedInstance = null;
	public static StatsManager Instance {
		get {
			return sharedInstance;
		}
	}

	[SerializeField] public float cookieAmount;
	[SerializeField] public float cookiePerSecond;
	[SerializeField] public float cookieUpgrade;

	// Use this for initialization
	void Start () {
		EventBroadcaster.Instance.AddObserver (EventNames.ON_ADD_COOKIE, this.OnCookieAdd);
		EventBroadcaster.Instance.AddObserver (EventNames.UPGRADE_COOKIE, this.UpgradeCookieMultiplier);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpgradeCookieMultiplier(){
		cookieAmount -= 50;
		cookiePerSecond += cookieUpgrade;
		EventBroadcaster.Instance.PostEvent (EventNames.UPDATE_COOKIE_PER_SECOND);
	}

	void OnCookieAdd(Parameters parameters){
		bool isAutoSpawn = parameters.GetBoolExtra (CookieSpawner.AUTO_SPAWN_KEY, false);
		float addedCookie = 1;
		if (isAutoSpawn) {
			addedCookie = cookiePerSecond;
		} else { 
			addedCookie = 1;
		}

		cookieAmount += addedCookie;

		Parameters param = new Parameters ();
		param.PutExtra (COOKIE_PER_SECOND_KEY, addedCookie	);
		EventBroadcaster.Instance.PostEvent (EventNames.ON_SPAWN_COOKIE_OBJ, param);

		param = new Parameters ();
		param.PutExtra (COOKIE_AMOUNT_KEY, cookieAmount);
		EventBroadcaster.Instance.PostEvent (EventNames.UPDATE_COOKIE_AMOUNT, param); 
	}
}
