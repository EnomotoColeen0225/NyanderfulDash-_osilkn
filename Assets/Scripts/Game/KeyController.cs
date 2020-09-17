using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    // 変数宣言
    // Player
    [SerializeField]
    private int PlayerNo = 0;
    private Cat _cat;

    //GameController
    private GameObject _gameController;
    private GameController _GameCtrlSC;

    private int _count = 0;

    // State
    private State _state = State.Wait;

    // KeyDispText
    [SerializeField]
    private Image _KeyDisp = null;
    [SerializeField]
    private Text KeyText = null;

    // キー入力
    [SerializeField]
    private int KeyNum;
    private bool Run = false;
    private bool RightRun = false;
    private bool LeftRun = false;
    private bool IsTumble = false;
    public bool IsGoal = false;

    // Start
    private bool IsRunningStart = true;

    // State実装
    private enum State
    {
        Wait,
        Right,
        Left,
        Tumble,
        Goal
    }

    private GameObject countDownPanel;

    void Start()
    {
        countDownPanel = GameObject.Find("Panel");


        // ゲームオブジェクトFind
        _gameController = GameObject.Find("GameController");
        // コンポーネントを取得
        _cat = this.GetComponent<Cat>();
        _GameCtrlSC = _gameController.GetComponent<GameController>();
        // ランダムキー取得
        KeyNum = UnityEngine.Random.Range(0, 4);
        // ゴール変数初期化
        IsGoal = false;
    }

    public void TapRunRight()
    {
        PushRun(State.Right);
    }

    public void TapRunLeft()
    {
        PushRun(State.Left);
    }

    private void StandUp()
    {
        // 転びフラグOFF
        IsTumble = false;
        _state = State.Wait;
    }

    private void Tumble()
    {
        // 転びフラグON
        IsTumble = true;
        _state = State.Tumble;
        _cat.RunTumble();
        Invoke("StandUp", 1);
    }

    private void PushRun(State state)
    {
        if (_state == State.Goal)
        {
            return;
        }

        _state = state;

        if (_state == State.Right)
        {
            _cat.RunRight();
        }
        else
        {
            _cat.RunLeft();
        }

        var catPos = this.transform.position;
        catPos.x += 0.6f;
        this.transform.position = catPos;
        // プレイヤー1に合わせて背景を動かす
        if (PlayerNo == 1)
        {
            _GameCtrlSC.MoveBG();
        }

        _count++;

        // ゴール処理
        if (_count >= 50)
        {
            // ゴールフラグON
            IsGoal = true;
            // スタートフラグOFF
            _GameCtrlSC.IsStart = false;
            // ジャンプ処理
            _state = State.Goal;
            _cat.RunJump();
            // KeyDisp非表示
            KeyText.enabled = false;
            _KeyDisp.enabled = false;
            // FINITH表示

            _GameCtrlSC.CountDownPanel.gameObject.SetActive(true);
            _GameCtrlSC.CountDownText.gameObject.SetActive(true);
            _GameCtrlSC.CountDownText.text = "FINITH!";
            // 4秒後にシーン移動
            Invoke("MoveResult", 4);
        }
    }

    // シーン移動
    private void MoveResult()
    {
        _GameCtrlSC.MoveScene();
    }


    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        // ランダムキー処理
        if (PlayerNo == 1)
        {
            if (KeyNum == 0)
            {
                KeyText.text = "Q";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
                    {
                        Tumble();
                    }
                }
            }
            else if (KeyNum == 1)
            {
                KeyText.text = "W";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
                    {
                        Tumble();
                    }
                }
            }
            else if (KeyNum == 2)
            {
                KeyText.text = "A";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S))
                    {
                        Tumble();
                    }
                }
            }
            else
            {
                KeyText.text = "S";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
                    {
                        Tumble();
                    }
                }
            }
        }
        // player2
        else if(PlayerNo == 2)
        {
            if (KeyNum == 0)
            {
                KeyText.text = "O";
                if (IsTumble == false)
                {

                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
                    {
                        Tumble();
                    }
                }
            }
            else if (KeyNum == 1)
            {
                KeyText.text = "P";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
                    {
                        Tumble();
                    }
                }
            }
            else if (KeyNum == 2)
            {
                KeyText.text = "K";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.L))
                    {
                        Tumble();
                    }
                }
            }
            else
            {
                KeyText.text = "L";
                if (IsTumble == false)
                {
                    if (Input.GetKeyDown(KeyCode.L))
                    {
                        Run = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.O))
                    {
                        Tumble();
                    }
                }
            }
        }

        // 走る処理
        if (_GameCtrlSC.IsStart == true)
        {
            if (IsRunningStart == true)
            {
                _GameCtrlSC.CountDownPanel.enabled = true;
                _GameCtrlSC.StartCoroutine("Running");
                IsRunningStart = false;
            }
            if (Run == true && RightRun == false)
            {
                KeyNum = UnityEngine.Random.Range(0, 4);
                TapRunRight();
                Run = false;
                RightRun = true;
                LeftRun = false;
            }
            else if (Run == true && LeftRun == false)
            {
                KeyNum = UnityEngine.Random.Range(0, 4);
                TapRunLeft();
                Run = false;
                LeftRun = true;
                RightRun = false;
            }
        }
    }
}
