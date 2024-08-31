using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

// ランキング処理を管理するクラス
public class RankingManager : MonoBehaviour
{
    [SerializeField] Text displayField;
    [SerializeField] Text displayNameField;
    [SerializeField] Text displayWaveField;
    [SerializeField] Text displayScoreField;
    [SerializeField] Text inputName;
    [SerializeField] Text inputWave;
    [SerializeField] Text inputScore;

    // ランキング取得処理を行うメソッド
    public void OnClickGetMessagesApi()
    {
        GetJsonFromWww();
    }

    // ランキング登録を行うメソッド
    public void OnClickSetMessageApi()
    {
        displayField.text = "wait...";
        SetJsonFromWww();
    }



    // データ取得リクエストが成功した際のコールバックメソッド
    void CallbackWwwSuccess(string response)
    {
        // json データ取得が成功したのでデシリアライズして整形し画面に表示する
        List<RankingData> messageList = RankingDataModel.DeserializeFromJson(response);

        string nameOutput = "";
        string waveOutput = "";
        string scoreOutput = "";
        foreach (RankingData message in messageList)
        {
            nameOutput += $"{message.Name}\n";
            waveOutput += $"{message.Wave}\n";
            scoreOutput += $"{message.Score}\n";
        }

        displayNameField.text = nameOutput;
        displayWaveField.text = waveOutput;
        displayScoreField.text = scoreOutput;
    }

    // データ取得リクエストが失敗した際のコールバックメソッド
    void CallbackWwwFailed()
    {
        // jsonデータ取得に失敗した
        displayField.text = "www failed";
    }

    // データ登録リクエストが成功した際のコールバックメソッド
    void CallbackApiSuccess(string response)
    {
        displayField.text = "Registered!";
    }

    // データ取得リクエストを行うメソッド
    void GetJsonFromWww()
    {
        // APIが設置してあるURLパス
        const string url = "http://localhost:80/rankingsystem01/Ranktable/getRankings";

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(GetMessages(url, CallbackWwwSuccess, CallbackWwwFailed));
    }

    // データ取得リクエスト処理を行うメソッド
    IEnumerator GetMessages(string url, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        // WWWを利用してリクエストを送る
        var webRequest = UnityWebRequest.Get(url);

        //タイムアウトの指定
        webRequest.timeout = 5;

        // WWWレスポンス待ち
        yield return webRequest.SendWebRequest();

        if (webRequest.error != null)
        {
            //レスポンスエラーの場合
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (webRequest.isDone)
        {
            // リクエスト成功の場合
            if (null != cbkSuccess)
            {
                cbkSuccess(webRequest.downloadHandler.text);
            }
        }
    }

    // データ登録リクエストを行うメソッド
    private void SetJsonFromWww()
    {
        // APIが設置してあるURLパス
        string sTgtURL = "http://localhost:80/rankingsystem01/Ranktable/setRanking";

        string name = inputName.text;
        string wave = inputWave.text;
        string score = inputScore.text;

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(SetMessage(sTgtURL, name, wave, score, CallbackApiSuccess, CallbackWwwFailed));
    }

    // データ登録リクエスト処理を行うメソッド
    IEnumerator SetMessage(string url, string name, string wave, string score, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("wave", wave);
        form.AddField("score", score);

        // WWWを利用してリクエストを送る
        var webRequest = UnityWebRequest.Post(url, form);

        //タイムアウトの指定
        webRequest.timeout = 5;

        // WWWレスポンス待ち
        yield return webRequest.SendWebRequest();

        if (webRequest.error != null)
        {
            //レスポンスエラーの場合
            // Debug.LogError(webRequest.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (webRequest.isDone)
        {
            // リクエスト成功の場合
            // Debug.Log($"Success:{webRequest.downloadHandler.text}");
            if (null != cbkSuccess)
            {
                cbkSuccess(webRequest.downloadHandler.text);
            }
        }
    }

    // タイムアウトチェック用メソッド
    IEnumerator ResponseCheckForTimeOutWWW(UnityWebRequest webRequest, float timeout)
    {
        float requestTime = Time.time;

        while (!webRequest.isDone)
        {
            if (Time.time - requestTime < timeout)
            {
                yield return null;
            }
            else
            {
                Debug.LogWarning("TimeOut"); //タイムアウト
                break;
            }
        }

        yield return null;
    }
}