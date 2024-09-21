using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextEffect : MonoBehaviour
{
	public GameObject End_Cursor;
	public string input_text;
	public bool isText_ing;

	public int Delay_Time;
    TMP_Text show_text;
    int index;
	float interval;

	AudioSource audioSource;

	private void Awake()
	{
        show_text = GetComponent<TMP_Text>();
		audioSource = GetComponent<AudioSource>();
	}

	public void Set_text(string text)
    {
        if (isText_ing){
			show_text.text = input_text;
			CancelInvoke();
			Effect_End();
        }
		else{
			input_text = text;
			Effect_Start();
		}
    }

	void Effect_Start()
	{
		show_text.text = "";
        index = 0;
		End_Cursor.SetActive(false);

		//Start Animation
		interval = 1.0f / Delay_Time;
		//Debug.Log(interval);

		isText_ing = true;

		Invoke("Effect_ing", interval);
	}

    void Effect_ing()
    {
        if(show_text.text == input_text){
            Effect_End();
			return;
		}
		show_text.text += input_text[index];

		//Sound
		if(input_text[index] != ' ' || input_text[index] != '.')
			audioSource.Play();

		index++;
		//recursive
		Invoke("Effect_ing", interval);
	}

    void Effect_End()
    {
		isText_ing = false;
		End_Cursor.SetActive(true);
	}
}
