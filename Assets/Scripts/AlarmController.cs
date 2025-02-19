using UnityEngine;

[RequireComponent(typeof(AlarmSoundPlayer))]
[RequireComponent(typeof(BoxCollider))]
public class AlarmController : MonoBehaviour
{
    private BoxCollider _collider;
    private int _rogueCounter = 0;
    private AlarmSoundPlayer _player;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _player = GetComponent<AlarmSoundPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rogue rogue))
        {
            _player.Activate();
            _rogueCounter++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rogue rogue))
        {
            _rogueCounter--;
        }

        if (_rogueCounter == 0)
        {
            _player.Deactivate();
        }
    }
}
