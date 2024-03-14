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
    }
}
