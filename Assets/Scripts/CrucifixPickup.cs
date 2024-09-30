using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucifixPickup : MonoBehaviour
{
	private bool isPickedUp = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPickedUp = true;
			other.GetComponent<PlayerCrucifix>().EquipCrucifix();
			gameObject.SetActive(false);
		}
	}
}
