using UnityEngine;

// ゲーム内BGMを管理するクラス
public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;
    [SerializeField] float baseVolume = 0.25f;
    AudioSource bgmSource; // BGMを再生するためのAudioSource

    void Awake()
    {
        // シングルトンの実装
        if (instance == null)
        {
            instance = this;
            bgmSource = GetComponent<AudioSource>();
            SetBaseVolume();
            DontDestroyOnLoad(gameObject); // シーン間で破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // すでにインスタンスが存在する場合は新しいオブジェクトを破棄する
            return;
        }

        // BGMの再生が停止している場合は再生開始
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    // BGMを変更するメソッド
    public void ChangeBgm(AudioClip newBgm)
    {
        bgmSource = GetComponent<AudioSource>();
        if (bgmSource.clip != newBgm)
        {
            bgmSource.Stop();
            bgmSource.clip = newBgm;
            bgmSource.Play();
        }
    }

    // BGMの音量を元に戻すメソッド
    public void SetBaseVolume()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.volume = baseVolume;
    }

    // BGMの音量を変更するメソッド
    public void SetVolume(float volume)
    {
        bgmSource = GetComponent<AudioSource>();
        Debug.Log(bgmSource);
        bgmSource.volume = volume;
    }

    // BGMのベース音量を取得するメソッド
    public float GetBaseVolume()
    {
        return baseVolume;
    }
}
