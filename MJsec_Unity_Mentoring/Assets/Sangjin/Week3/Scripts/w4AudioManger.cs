using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// �������� ������ǰ� ȿ������ �����ϴ� �̱��� ����� �Ŵ����Դϴ�.
/// ��Ƽ�� SFX ����� ���� AudioSource Ǯ�� ����մϴ�.
/// </summary>
public class w4AudioManager : MonoBehaviour
{
    public static w4AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [Tooltip("������ǿ� AudioSource")]
    [SerializeField] private AudioSource musicSource;

    [Header("SFX Pool Settings")]
    [Tooltip("ȿ���� ���� ����� ���� AudioSource Ǯ ũ��")]
    [SerializeField] private int sfxPoolSize = 10;

    // ���� SFX AudioSource Ǯ
    private List<AudioSource> sfxSources;

    [Header("Audio Clips")]
    [Tooltip("��� ������ ������� Ŭ�� ���")]
    [SerializeField] private List<AudioClip> musicClips;
    [Tooltip("��� ������ ȿ���� Ŭ�� ���")]
    [SerializeField] private List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    private void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // ��ųʸ� �ʱ�ȭ
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

        // SFX AudioSource Ǯ ����
        sfxSources = new List<AudioSource>(sfxPoolSize);
        for (int i = 0; i < sfxPoolSize; i++)
        {
            var src = gameObject.AddComponent<AudioSource>();
            src.playOnAwake = false;
            sfxSources.Add(src);
        }
    }

    /// <summary>
    /// ������� ���
    /// </summary>
    /// <param name="name">AudioClip �̸�</param>
    /// <param name="loop">�ݺ� ����</param>
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
    /// ������� ����
    /// </summary>
    public void StopMusic() => musicSource.Stop();

    /// <summary>
    /// ȿ���� ��� (���� ���� ��� ����)
    /// </summary>
    /// <param name="name">AudioClip �̸�</param>
    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out var clip))
        {
            // ��� ������ AudioSource ã��
            var src = sfxSources.FirstOrDefault(s => !s.isPlaying);
            if (src == null)
            {
                // Ǯ �����÷ο� �� ���� �߰�
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
    /// ������� ���� ���� (0~1)
    /// </summary>
    public void SetMusicVolume(float volume) => musicSource.volume = Mathf.Clamp01(volume);

    /// <summary>
    /// ȿ���� ���� ���� (0~1)
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        foreach (var src in sfxSources)
            src.volume = Mathf.Clamp01(volume);
    }
}
