using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 _musicTime;
    private bool _isWaiting = false;
    private AudioSource _musicSource;
    // Start is called before the first frame update

    private void Start()
    {
        _musicSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(!_isWaiting && !_musicSource.isPlaying)
        {
            StartCoroutine(StartMusicAfterWait(Random.Range(_musicTime.x, _musicTime.y)));
        }
    }

    IEnumerator StartMusicAfterWait(float waitTime)
    {
        _isWaiting = true;

        yield return new WaitForSeconds(waitTime);
        _musicSource.Play();
        _isWaiting = false;

        DOVirtual.Float(0, 1, _musicSource.clip.length*0.1f, (x) => { _musicSource.volume = x; });

        yield return new WaitForSeconds(_musicSource.clip.length * 0.8f);
        DOVirtual.Float(1, 0, _musicSource.clip.length * 0.1f, (x) => { _musicSource.volume = x; });

    }
}
