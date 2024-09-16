using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioSource audioSource; // The audio source for playing footstep sounds
    public AudioClip grassFootstep;
    public AudioClip woodFootstep;
    
    private CharacterController characterController;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is moving and on the ground
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                PlayFootstepSound(hit.collider.tag);
            }
        }
    }

    void PlayFootstepSound(string surfaceTag)
    {
        switch (surfaceTag)
        {
            case "Grass":
                audioSource.clip = grassFootstep;
                break;
            case "Wood":
                audioSource.clip = woodFootstep;
                break;
            default:
                return;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
