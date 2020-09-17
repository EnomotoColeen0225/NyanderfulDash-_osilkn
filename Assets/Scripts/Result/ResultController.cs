using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ResultController : MonoBehaviour
{
    // Audio変数
    [SerializeField]
    private AudioClip Push_AC = null;
    AudioSource audioSource;

    // タイム表示
    [SerializeField]
    private Text _timeText = null;

    void Start()
    {
        // コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        // GameControllerオブジェクト取得
        GameController gameController = FindObjectOfType<GameController>();
        Debug.Log(GameController._time);
        _timeText.text = Math.Round(GameController._time, 2, MidpointRounding.AwayFromZero).ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        // Escが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 音を鳴らす
            audioSource.PlayOneShot(Push_AC);
            // タイトルへ戻る
            SceneManager.LoadScene("Title");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 音を鳴らす
            audioSource.PlayOneShot(Push_AC);
            // SelectController.stageNo
            // ステージに戻る
            SceneManager.LoadScene("SoloGame");
        }
    }
}
