using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerScreen : View {

	[SerializeField] Text cookieAmount;
	[SerializeField] Text cookiePerSecond;

	[SerializeField] Button upgradeButton;

	// Use this for initialization
	void Start () {

		EventBroadcaster.Instance.AddObserver (EventNames.UPDATE_COOKIE_AMOUNT, this.UpdateCookieAmount);
		EventBroadcaster.Instance.AddObserver (EventNames.UPDATE_COOKIE_PER_SECOND, this.UpdateCookiePerSecond);

		var parameters = new Parameters ();
		parameters.PutExtra (StatsManager.COOKIE_PER_SECOND_KEY, 1);
		EventBroadcaster.Instance.PostEvent (EventNames.UPDATE_COOKIE_PER_SECOND, parameters);

	}
	
	// Update is called once per frame
	void Update () {

		if (int.Parse (cookieAmount.text) < 50) {
			upgradeButton.interactable = false;
		} else
			upgradeButton.interactable = true;
		
	}

	public void UpdateCookiePerSecond(Parameters parameter){
		var perSecond =  parameter.GetIntExtra (StatsManager.COOKIE_PER_SECOND_KEY, 1);
		cookiePerSecond.text = perSecond +"";
	}

	public void UpdateCookieAmount(Parameters parameter){
		var amount =  parameter.GetIntExtra (StatsManager.COOKIE_AMOUNT_KEY, 1);
		cookieAmount.text = amount + "";
	
	}

	public void OnUpgradeClick(){
		EventBroadcaster.Instance.PostEvent (EventNames.UPGRADE_COOKIE);
	}

	public void OnCookieClick(){
		Parameters parameters = new Parameters ();
		parameters.PutExtra (CookieSpawner.AUTO_SPAWN_KEY, false);
		EventBroadcaster.Instance.PostEvent (EventNames.ON_ADD_COOKIE, parameters);
	}
}
