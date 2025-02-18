using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]
	public class Door : MonoBehaviour
	{
		private const float DoorOpenAngle = -90.0f;
		private const float DoorCloseAngle = 0.0f;
        [SerializeField] private AudioClip _openDoor, _closeDoor;

        private bool _open;
        private float _smooth = 1.0f;
        private AudioSource _asource;

        private void Awake ()
		{
			_asource = GetComponent<AudioSource> ();
		}
	
		private void Update ()
		{
			if (_open)
			{
				var target = Quaternion.Euler (0, DoorOpenAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * _smooth);
	
			}
			else
			{
				var target1= Quaternion.Euler (0, DoorCloseAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * _smooth);
	
			}  
		}

        private void OnTriggerEnter(Collider other)
        {
			OpenDoor();
        }

        private void OnTriggerExit(Collider other)
        {
			OpenDoor();
        }

        public void OpenDoor(){
			_open = !_open;
			_asource.clip = _open?_openDoor:_closeDoor;
			_asource.Play ();
		}
	}
}