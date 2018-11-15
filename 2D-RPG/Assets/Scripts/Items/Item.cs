﻿using UnityEngine;

public class Item : Interactable 
{
    public override void Update () 
	{
		base.Update();
	}

	public override void Interact()
	{
		//base.Interact();
		Debug.Log("Added to items");
		PickUp();
		gameObject.SetActive(false);
		//Inventory.instance.ListItems();
		//Add to inventory

	}

	void PickUp()
	{
		Inventory.instance.Add(this);
	}

    void OnMouseEnter()
    {
        GameManagerSingleton.instance.tooltip.text = transform.name;
        GameManagerSingleton.instance.tooltip.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        GameManagerSingleton.instance.tooltip.gameObject.SetActive(false);
    }

}
