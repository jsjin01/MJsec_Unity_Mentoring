using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 전역에서 배경음악과 효과음을 관리하는 싱글톤 오디오 매니저입니다.
/// 멀티플 SFX 재생을 위해 AudioSource 풀을 사용합니다.
/// </summary>
public class w4AudioManager : MonoBehaviour
{
    public static w4AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [Tooltip("배경음악용 AudioSource")]
    [SerializeField] private AudioSource musicSource;

    [Header("SFX Pool Settings")]
    [Tooltip("효과음 동시 재생을 위한 AudioSource 풀 크기")]
    [SerializeField] private int sfxPoolSize = 10;

    // 내부 SFX AudioSource 풀
    private List<AudioSource> sfxSources;

    [Header("Audio Clips")]
    [Tooltip("재생 가능한 배경음악 클립 목록")]
    [SerializeField] private List<AudioClip> musicClips;
    [Tooltip("재생 가능한 효과음 클립 목록")]
    [SerializeField] private List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 딕셔너리 초기화
        musicDict = new Dictionary<string, AudioClip>();
        foreach (var clip in musicClips)
        {
            if (clip != null && !musicDict.ContainsKey(clip.name))
                musicDict.Add(clip.name, clip);
        }
        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
        {
            if (clip != null && !sfxDict.ContainsKey(clip.name))
                sfxDict.Add(clip.name, clip);
        }

        // SFX AudioSource 풀 생성
        sfxSources = new List<AudioSource>(sfxPoolSize);
        for (int i = 0; i < sfxPoolSize; i++)
        {
            var src = gameObject.AddComponent<AudioSource>();
            src.playOnAwake = false;
            sfxSources.Add(src);
        }
    }

    /// <summary>
    /// 배경음악 재생
    /// </summary>
    /// <param name="name">AudioClip 이름</param>
    /// <param name="loop">반복 여부</param>
    public void PlayMusic(string name, bool loop = true)
    {
        if (musicDict.TryGetValue(name, out var clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Music clip '{name}' not found.");
        }
    }

    /// <summary>
    /// 배경음악 중지
    /// </summary>
    public void StopMusic() => musicSource.Stop();

    /// <summary>
    /// 효과음 재생 (동시 다중 재생 지원)
    /// </summary>
    /// <param name="name">AudioClip 이름</param>
    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out var clip))
        {
            // 사용 가능한 AudioSource 찾기
            var src = sfxSources.FirstOrDefault(s => !s.isPlaying);
            if (src == null)
            {
                // 풀 오버플로우 시 새로 추가
                src = gameObject.AddComponent<AudioSource>();
                src.playOnAwake = false;
                sfxSources.Add(src);
            }
            src.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"[AudioManager] SFX clip '{name}' not found.");
        }
    }

    /// <summary>
    /// 배경음악 볼륨 설정 (0~1)
    /// </summary>
    public void SetMusicVolume(float volume) => musicSource.volume = Mathf.Clamp01(volume);

    /// <summary>
    /// 효과음 볼륨 설정 (0~1)
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        foreach (var src in sfxSources)
            src.volume = Mathf.Clamp01(volume);
    }
}
