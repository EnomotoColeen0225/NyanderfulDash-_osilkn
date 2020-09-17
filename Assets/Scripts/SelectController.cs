using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    // ステージ番号（static）
    public static int stageNo { get; set; }

    //Image
    [SerializeField]
    private Image _Image;

    [SerializeField]
    private AudioClip Push_AC = null;
    AudioSource audioSource;

    void Start()
    {
        // コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        // ステージ番号初期化
        stageNo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            audioSource.PlayOneShot(Push_AC);
            SceneManager.LoadScene("SoloGame");
            stageNo = 1;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            audioSource.PlayOneShot(Push_AC);
            SceneManager.LoadScene("DuoGame");
            stageNo = 2;
        }

    }
}
