using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //変数宣言
    //KeyController
    private GameObject _catObject;
    private KeyController _KeyCtrl;

    // 背景
    private GameObject _background1;
    private GameObject _background2;
    private GameObject _background3;

    // カウントダウンテキスト
    public Text CountDownText = null;
    [SerializeField]
    private float TotalTime = 4;
    [SerializeField]
    private int CountDown = 0;
    public Image CountDownPanel = null;

    // タイム計測
    private TextMeshProUGUI _timeText = null;
    public static float _time { get; set; }
    // 非表示フラグ
    private bool IsDisp = true;
    private bool IsText_GO = false;

    // 音
    [SerializeField]
    private AudioClip BGM = null;
    [SerializeField]
    private AudioClip CountDoun_AC_1 = null;
    [SerializeField]
    private AudioClip CountDoun_AC_2 = null;
    private bool IsPlay_ACCD_1 = false;
    private bool IsPlay_ACCD_2 = false;

    // スタートフラグ
    public bool IsStart = false;

    AudioSource audioSource;

    void Start()
    {
        // time変数初期化
        _time = 0;
        
        // オブジェクトFind
        _catObject = GameObject.Find("Cat");
        _background1 = GameObject.Find("Background1");
        _background2 = GameObject.Find("Background2");
        _background3 = GameObject.Find("Background3");
        _timeText = GameObject.Find("Canvas/Time").GetComponent<TextMeshProUGUI>();

        // コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        _KeyCtrl = _catObject.GetComponent<KeyController>();

        // BGM流す
        audioSource.PlayOneShot(BGM);
    }

    // 走り始め=タイム計測
    public IEnumerator Running()
    {
        while (_KeyCtrl.IsGoal == false)
        {
            _time += Time.deltaTime;
            _timeText.text = Math.Round(_time, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            yield return null;
        }
    }

    // シーン移動関数
    public void MoveScene()
    {
        SceneManager.LoadScene("Result");
    }


    // 背景を動かす関数
    public void MoveBG()
    {
        var pos1 = _background1.transform.position;
        pos1.x -= 0.23f;
        _background1.transform.position = pos1;

        var pos2 = _background2.transform.position;
        pos2.x -= 0.46f;
        _background2.transform.position = pos2;


        var pos3 = _background3.transform.position;
        pos3.x -= 0.115f;
        _background3.transform.position = pos3;
    }

    void Update()
    {
        // カウントダウン処理
        if (TotalTime > 1)
        {
            TotalTime -= Time.deltaTime;
            CountDown = (int)TotalTime;
            CountDownText.text = CountDown.ToString();
            if (IsPlay_ACCD_1 == false)
            {
                StartCoroutine("PlayACCountDown");
            }
        }
        else
        {
            StartCoroutine("StartCountDown");
        }
    }

    // カウントダウンが0になった時
    IEnumerator StartCountDown()
    {
        if (TotalTime > 0)
        {
            // テキストを「GO!」
            if (IsText_GO == false)
            {
                CountDownText.text = "GO!";
                IsText_GO = true;
            }
            if (IsPlay_ACCD_2 == false)
            {
                // 音鳴らす（一回だけ）
                audioSource.PlayOneShot(CountDoun_AC_2);
                IsPlay_ACCD_2 = true;
            }
            // 一秒待機
            yield return new WaitForSeconds(1);
            // 非表示
            if (IsDisp == true)
            {
                CountDownText.gameObject.SetActive(false);
                CountDownPanel.gameObject.SetActive(false);
                IsDisp = false;
            }
            // スタートフラグON
            IsStart = true;
        }
    }

    // カウントダウンSE（3回鳴らす）
    IEnumerator PlayACCountDown()
    {
        IsPlay_ACCD_1 = true;
        for (int i = 0; i < 3; i++)
        {
            audioSource.PlayOneShot(CountDoun_AC_1);
            yield return new WaitForSeconds(1);
        }
    }
}