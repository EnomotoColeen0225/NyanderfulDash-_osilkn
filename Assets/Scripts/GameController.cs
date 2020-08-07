using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject _catObject;
    private Cat _cat;
    private GameObject _background1;
    private GameObject _background2;
    private GameObject _background3;
    private State _state = State.Wait;
    private int _count = 0;
    private TextMeshProUGUI _timeText;
    private float _time = 0;

    private enum State
    {
        Wait,
        Right,
        Left,
        Tumble,
        Goal
    }

    void Start()
    {
        _catObject = GameObject.Find("Cat");
        _cat = _catObject.GetComponent<Cat>();
        _background1 = GameObject.Find("Background1");
        _background2 = GameObject.Find("Background2");
        _background3 = GameObject.Find("Background3");
        _timeText = GameObject.Find("Canvas/Time").GetComponent<TextMeshProUGUI>();
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
        _state = State.Wait;
    }

    IEnumerator Running()
    {
        while (_state != State.Goal)
        {
            _time += Time.deltaTime;
            _timeText.text = Math.Round(_time, 2, MidpointRounding.AwayFromZero).ToString("0.00");
            yield return null;
        }
    }

    private void PushRun(State state)
    {
        if(_state == State.Wait && _count == 0)
        {
            StartCoroutine("Running");
        }
        if(_state == State.Tumble || _state == State.Goal)
        {
            return;
        }

        if(_state == state)
        {
            _state = State.Tumble;
            _cat.RunTumble();
            Invoke("StandUp", 1);
            return;
        }

        _state = state;

        if(_state == State.Right)
        {
            _cat.RunRight();
        }
        else
        {
            _cat.RunLeft();
        }

        var catPos = _catObject.transform.position;
        catPos.x += 0.6f;
        _catObject.transform.position = catPos;

        var pos1 = _background1.transform.position;
        pos1.x -= 0.23f;
        _background1.transform.position = pos1;

        var pos2 = _background2.transform.position;
        pos2.x -= 0.46f;
        _background2.transform.position = pos2;


        var pos3 = _background3.transform.position;
        pos3.x -= 0.115f;
        _background3.transform.position = pos3;

        _count++;

        if(_count >= 50)
        {
            _state = State.Goal;
            _cat.RunJump();
        }
    }

    void Update()
    {
        if()
        {
            TapRunRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TapRunLeft();
        }
    }
}
