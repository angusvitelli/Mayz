using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
	public float maxStamina = 10f;
	public float currentStamina;
	public float staminaDrain = 5f;
	public float staminaRegen = 1f;
	public bool isSprinting = false;
    
    void Start()
    {
		currentStamina = maxStamina;
    }

    
    void Update()
    {
		if (isSprinting)
		{
			currentStamina -= staminaDrain * Time.deltaTime;
			if (currentStamina <= 0)
			{
				currentStamina = 0;
				isSprinting = false;
			}
		}
		else if (currentStamina < maxStamina)
		{
			currentStamina += staminaRegen * Time.deltaTime;
		}

		currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

	public bool CanSprint()
	{
		return currentStamina > 0;
	}
}
