using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Playlist : MonoBehaviour {

    [Header("Playlist Settings")]
    [SerializeField]
    private bool shufflePlaylist;
    [SerializeField]
    private List<AudioClip> songs;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (shufflePlaylist)
        {
            songs = ShuffleSongs(songs);
        }
        StartCoroutine(PlayList(songs));
    }

    private IEnumerator PlayList(List<AudioClip> songs)
    {
        while (true)
        {
            for (int i = 0; i < songs.Count; i++)
            {
                audioSource.PlayOneShot(songs[i]);
                while (audioSource.isPlaying)
                {
                    yield return null;
                }
            }
            yield return null;
        }
    }

    private List<AudioClip> ShuffleSongs(List<AudioClip> playList)
    {
        System.Random rng = new System.Random();
        int n = playList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = playList[k];
            playList[k] = playList[n];
            playList[n] = value;
        }
        return playList;
    }
}
