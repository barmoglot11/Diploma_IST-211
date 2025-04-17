using UnityEngine;

namespace Interaction
{
    public class DoorOpener : MonoBehaviour
    {
        private Animator Animator => GetComponent<Animator>();
        AudioSource Source => GetComponent<AudioSource>();
        
        public AudioClip doorOpenSound;

        public bool isOpen = false;
        
        public void SwitchDoor()
        {
            Animator.SetTrigger(!isOpen ? "OpenDoor" : "CloseDoor");
            Source.PlayOneShot(doorOpenSound);
            isOpen = !isOpen;
        } 
    }
}