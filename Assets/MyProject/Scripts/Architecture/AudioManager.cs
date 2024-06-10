using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private float _delay = 3f;
    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _delay)
        {
            int index = Random.Range(0, _audioSources.Count);
            _audioSources[index].clip = _audioClips[Random.Range(0, _audioClips.Count)];
            _audioSources[index].Play();
            _timer = 0f;
        }
    }
}
