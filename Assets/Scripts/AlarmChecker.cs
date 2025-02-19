using UnityEngine;

[RequireComponent(typeof(AlarmAudioplayer))]
[RequireComponent(typeof(BoxCollider))]
public class AlarmChecker : MonoBehaviour
{
    private int _rogueCounter = 0;
    private AlarmAudioplayer _audioplayer;

    private void Awake()
    {
        _audioplayer = GetComponent<AlarmAudioplayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rogue rogue))
        {
            _audioplayer.Activate();
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
            _audioplayer.Deactivate();
        }
    }
}
