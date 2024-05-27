using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararCh : MonoBehaviour
{
    public bool Mode;

    GameObject[] characters;
    GameObject currentChar;
    Vector3[] initialPositions; // 各キャラクターの初期位置を保持

    int _currentCharNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        // このオブジェクトの子オブジェクトをすべて取得
        List<GameObject> characterList = new List<GameObject>();
        List<Vector3> initialPositionList = new List<Vector3>(); // 初期位置を保存するリスト

        foreach (Transform child in transform)
        {
            characterList.Add(child.gameObject);
            initialPositionList.Add(child.position); // 初期位置を保存
        }
        characters = characterList.ToArray();
        initialPositions = initialPositionList.ToArray();

        Debug.Log("I have " + characters.Length + " Changable Characters");

        ChangeCharacter(_currentCharNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode) // Player 1
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_currentCharNum == characters.Length - 1)
                {
                    _currentCharNum = 0;
                }
                else
                {
                    _currentCharNum++;
                }
                Debug.Log("Character " + _currentCharNum + " Selected");
                ChangeCharacter(_currentCharNum);
            }
        }
        else // Player 2
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_currentCharNum == characters.Length - 1)
                {
                    _currentCharNum = 0;
                }
                else
                {
                    _currentCharNum++;
                }
                Debug.Log("Character " + _currentCharNum + " Selected");
                ChangeCharacter(_currentCharNum);
            }
        }
    }

    void ChangeCharacter(int characterNum)
    {
        // 現在のキャラクターの位置と回転を保存する
        Vector3 currentPos = Vector3.zero;
        Quaternion currentRot = Quaternion.identity;
        if (currentChar != null)
        {
            currentPos = currentChar.transform.position;
            currentRot = currentChar.transform.rotation;
        }

        // いったん全キャラクターを非アクティブにする
        foreach (GameObject gObj in characters)
        {
            gObj.SetActive(false);
        }

        // 新しいキャラクターをアクティブにする
        currentChar = characters[characterNum];
        currentChar.SetActive(true);

        // 保存した位置と回転を新しいキャラクターに適用する
        if (currentPos != Vector3.zero)
        {
            currentChar.transform.position = currentPos;
            currentChar.transform.rotation = currentRot;
        }
        else
        {
            // 初期位置を適用
            currentChar.transform.position = initialPositions[characterNum];
        }
    }
}
