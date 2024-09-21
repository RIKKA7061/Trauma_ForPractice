using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
    public float Earn_score = 10f; // 목표 점수
    float text_score = 0f; // 현재 점수
    public float speed = 1f; // 점수 증가 속도
    public string type = "F0"; // 점수를 표시하는 형식

    void Start()
    {
        text_score = 0f; // 시작 시 점수 초기화
    }

    void Update()
    {
        // text_score가 목표 점수를 넘지 않도록 보장
        if (text_score < Earn_score)
        {
            // Time.deltaTime을 곱한 점수 증가량 계산
            text_score += 1000 * Time.deltaTime * speed;
            // 목표 점수를 초과하지 않도록 제한
            if (text_score > Earn_score)
            {
                text_score = Earn_score;
            }
        }

        // 점수를 텍스트에 반영
        GetComponent<Text>().text = "Score   " + text_score.ToString(type);
    }
}
