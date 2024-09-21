using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
    public float Earn_score = 10f; // ��ǥ ����
    float text_score = 0f; // ���� ����
    public float speed = 1f; // ���� ���� �ӵ�
    public string type = "F0"; // ������ ǥ���ϴ� ����

    void Start()
    {
        text_score = 0f; // ���� �� ���� �ʱ�ȭ
    }

    void Update()
    {
        // text_score�� ��ǥ ������ ���� �ʵ��� ����
        if (text_score < Earn_score)
        {
            // Time.deltaTime�� ���� ���� ������ ���
            text_score += 1000 * Time.deltaTime * speed;
            // ��ǥ ������ �ʰ����� �ʵ��� ����
            if (text_score > Earn_score)
            {
                text_score = Earn_score;
            }
        }

        // ������ �ؽ�Ʈ�� �ݿ�
        GetComponent<Text>().text = "Score   " + text_score.ToString(type);
    }
}
