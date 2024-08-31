using System.Collections;
using UnityEngine;

// 敵のSEを管理するクラス
public class EnemySE : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // ゾンビのうなり声
    // ３種類の中からランダムで再生
    [SerializeField] AudioClip zombieRoarClip1;
    [SerializeField] AudioClip zombieRoarClip2;
    [SerializeField] AudioClip zombieRoarClip3;

    [SerializeField] AudioClip zombieShotClip; // ゾンビから血が出る音

    [SerializeField] float zombieRoarVolume = 1f; // ゾンビの唸り声の音量
    [SerializeField] float zombieShotVolume = 1f; // ゾンビから血が出る音の音量
    [SerializeField] float roarMinInterval = 2f; // 唸り声を鳴らす最小間隔
    [SerializeField] float roarMaxInterval = 5f; // 唸り声を鳴らす最大間隔
    [SerializeField] float pitchVariation = 0.2f; // ピッチの変動幅

    OptionManager optionManager;
    EnemyHealth enemyHealth;

    void Start()
    {
        optionManager = FindObjectOfType<OptionManager>();
        enemyHealth = GetComponent<EnemyHealth>();

        // コルーチンを開始して、一定間隔でSEを再生する
        StartCoroutine(PlayZombieRoarPeriodically());
    }

    // ゾンビが撃たれた際の音を再生するメソッド
    public void PlayZombieShotSE()
    {
        PlaySoundSEVolume(zombieShotClip, zombieShotVolume);
    }

    // ゾンビの唸り声を再生するメソッド
    void PlayZombieRoar()
    {
        // 3種類のうちからランダムで再生する
        float randomValue = Random.Range(0, 3);
        if (randomValue < 1)
            PlaySound(zombieRoarClip1, zombieRoarVolume);
        else if (1 <= randomValue && randomValue < 2)
            PlaySound(zombieRoarClip2, zombieRoarVolume);
        else if (2 <= randomValue)
            PlaySound(zombieRoarClip3, zombieRoarVolume);
    }

    // SE以外の音声を再生するメソッド
    void PlaySound(AudioClip clip, float volume)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation); // ランダムにピッチを変える

            // マスターボリュームのみ適用する
            audioSource.PlayOneShot(clip, volume * optionManager.GetMasterVolumeMultiplier());
            audioSource.pitch = 1f; // ピッチを元に戻す
        }
    }

    // SEを再生するメソッド
    void PlaySoundSEVolume(AudioClip clip, float volume)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation); // ランダムにピッチを変える

            // マスターボリュームとSEボリュームを適用する
            audioSource.PlayOneShot(clip, volume * optionManager.GetMasterVolumeMultiplier() * optionManager.GetSEVolumeMultiplier());
            audioSource.pitch = 1f; // ピッチを元に戻す
        }
    }

    // ゾンビの唸り声を一定時間ごとに再生するメソッド
    IEnumerator PlayZombieRoarPeriodically()
    {
        while (!enemyHealth.GetIsDead())
        {
            PlayZombieRoar(); // SEを再生
            yield return new WaitForSeconds(Random.Range(roarMinInterval, roarMaxInterval)); // 指定された間隔だけ待つ
        }
    }
}
