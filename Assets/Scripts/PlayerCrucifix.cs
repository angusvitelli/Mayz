using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrucifix : MonoBehaviour
{
	public GameObject crucifixModel;
	public Transform handPosition;
	public float crucifixRange = 10f;

	private GameObject equippedCrucifix = null;
	private bool hasCrucifix = false;

	public void EquipCrucifix()
	{
		if (!hasCrucifix)
		{
			hasCrucifix = true;
			equippedCrucifix = Instantiate(crucifixModel, handPosition.position, handPosition.rotation);
			equippedCrucifix.transform.SetParent(handPosition);
		}
	}

	void Update()
	{
		if (hasCrucifix && Input.GetKeyDown(KeyCode.C))
		{
			UseCrucifix();
		}
	}

	void UseCrucifix()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, crucifixRange);

		foreach (Collider hit in hitColliders)
		{
			if (hit.CompareTag("Enemy"))
			{
				Vector3 directionToEnemy = hit.transform.position - transform.position;
				float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

				if (angleToEnemy <= 45f)
				{
					EnemyFollow enemy = hit.GetComponent<EnemyFollow>();

					if (enemy != null)
					{
						enemy.StunEnemy(3f);
					}
				}
			}
		}

		Destroy(equippedCrucifix);
		hasCrucifix = false;
	}
   
}
