using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmAudioplayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 2f;
    [SerializeField] private float _fadeSpeed = 0.2f;

    private AudioSource _audioSource;
    private float _targetVolume;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.loop = true;
        _audioSource.volume = 0f;
        _targetVolume = _minVolume;
    }

    public void Activate()
    {
        _targetVolume = _maxVolume;
        _audioSource.Play();
        StartSoundChange();
    }

    public void Deactivate()
    {
        _targetVolume = _minVolume;
        StartSoundChange();
    }

    private void StartSoundChange()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeSound());
    }


    private IEnumerator ChangeSound()
    {
        while (Mathf.Approximately(_audioSource.volume, _targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        _audioSource.volume = _targetVolume;

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();

        _currentCoroutine = null;
    }
}
