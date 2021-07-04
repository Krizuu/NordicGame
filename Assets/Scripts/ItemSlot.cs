using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    Item item;
	public Image icon;

	public void Start()
	{
		icon.enabled = false;
	}

	public void addItem(Item newItem)
	{
		item = newItem;
		icon.sprite = item.pixelart;
		icon.enabled = true;
	}

	public void  clearSlot()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
	}
}
