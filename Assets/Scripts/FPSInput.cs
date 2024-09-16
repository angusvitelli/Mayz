using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] // enforces dependency on character controller
[AddComponentMenu("Control Script/FPS Input")]  // add to the Unity editor's component menu
public class FPSInput : MonoBehaviour
{   
    // movement sensitivity
    public float speed = 6.0f;
	//sprint movement
	public float sprintSpeed = 7.0f;

    // gravity setting
    public float gravity = -9.8f;

    // reference to the character controller
    private CharacterController charController;

	//ref the stamina script
	private Stamina stamina;

    // Start is called before the first frame update
    void Start()
    {
        // get the character controller component
        charController = GetComponent<CharacterController>();
		stamina = GetComponent<Stamina>();
    }

    // Update is called once per frame
    void Update()
    {
		float currentSpeed = speed;

		if (Input.GetKey(KeyCode.LeftShift) && stamina.CanSprint())
		{
			currentSpeed = sprintSpeed;
			stamina.isSprinting = true;
		}
		else
		{
			stamina.isSprinting = false;
		}

        // changes based on WASD keys
        float deltaX = Input.GetAxis("Horizontal") * currentSpeed;
        float deltaZ = Input.GetAxis("Vertical") * currentSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // make diagonal movement consistent
        movement = Vector3.ClampMagnitude(movement, currentSpeed);

        // add gravity in the vertical direction
        movement.y = gravity;

        // ensure movement is independent of the framerate
        movement *= Time.deltaTime;

        // transform from local space to global space
        movement = transform.TransformDirection(movement);

        // pass the movement to the character controller
        charController.Move(movement);
    }
}
