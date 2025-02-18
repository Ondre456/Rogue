using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class HouseArea : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxVolume = 2f;
    [SerializeField] private float _fadeSpeed = 0.2f;

    private AudioSource _audioSource;
    private BoxCollider _collider;
    private int _rogueCounter = 0;
    private bool _isFading = false;
    private float _targetVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.loop = true;
        _audioSource.volume = 0f;
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _targetVolume = _minVolume;
    }

    private void Update()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _fadeSpeed * Time.deltaTime);

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rogue rogue))
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
                _targetVolume = _maxVolume;
            }

            _rogueCounter++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rogue rogue))
        {
            _rogueCounter--;
        }

        if (_rogueCounter == 0 && _audioSource.isPlaying)
        {
            _targetVolume = _minVolume;
        }
    }
}
