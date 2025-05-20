using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AlarmTrigger _alarmTrigger;

    private float _changeStep = 1.0f;
    private float _minVolue = 0.0f;
    private float _maxVolue = 1.0f;

    private bool _isCrookInside;

    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _audioSource.loop = true;

        _audioSource.volume = _minVolue;

        _isCrookInside = false;
    }

    private void OnEnable()
    {
        _alarmTrigger.CrookEntered += OnCrookEnter;
        _alarmTrigger.CrookExited += OnCrookExit;
    }

    private void OnDisable()
    {
        _alarmTrigger.CrookEntered -= OnCrookEnter;
        _alarmTrigger.CrookExited -= OnCrookExit;
    }

    private void OnCrookEnter()
    {
        _isCrookInside = true;

        UpdateSoundState();
    }

    private void OnCrookExit()
    {
        _isCrookInside = false;

        UpdateSoundState();
    }

    private void UpdateSoundState()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(nameof(ChangeVolumeSmooth));

        _volumeCoroutine = StartCoroutine(nameof(ChangeVolumeSmooth), GetTargetVolue());
    }

    private float GetTargetVolue() => _isCrookInside ? _maxVolue : _minVolue;

    private IEnumerator ChangeVolumeSmooth(float targetVolume)
    {
        if (_isCrookInside && _audioSource.isPlaying == false)
            _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeStep * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == _minVolue && _audioSource.isPlaying)
            _audioSource.Stop();
    }
}