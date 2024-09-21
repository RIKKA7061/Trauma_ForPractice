using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Show Test
using UnityEngine.UI;

//Parsing
using UnityEngine.Networking;
using System;
using System.Linq;

public class TextManager : MonoBehaviour
{
	string sheet_text;
	//링복 -> /edit?usp=sharing 을 export?format=tsv&range=B2:D3 로 바꾸시오. (당연히 B2:D3은 해당 범위겠죠?)
	//https://docs.google.com/spreadsheets/d/1YDzpOcAUiFD-NVaPVb98-aTI1RHxDBymwanOQNBPSKE/edit?usp=sharing
	const string google_sheet_URL = "https://docs.google.com/spreadsheets/d/1YDzpOcAUiFD-NVaPVb98-aTI1RHxDBymwanOQNBPSKE/export?format=tsv&range=B2:I30";

	//start One ( but, you can stop anytime if 123456 -> 1234 (2sec) 56 like this!!
 	IEnumerator Start()
	{
		//using -> safe resource crafting
		//UnityWebRequest -> HTTP (for GET)
		using (UnityWebRequest online_sheet = UnityWebRequest.Get(google_sheet_URL))
		{
			//중지했다가 -> 데이터 테이블 정보를 가져오면 다음 구문
			yield return online_sheet.SendWebRequest();

			// 만약 웹에서 다운로드가 마쳤다?
			if (online_sheet.isDone)
			{
				//sheet_text 변수에 정보 넣기
				sheet_text = online_sheet.downloadHandler.text;
			}
		}

		ProduceData();
	}
	


	Dictionary<int, string[]> talk_DB;
	Dictionary<int, Sprite> portrait_DB; 

	public Sprite[] portrait_arr;

	
	void ProduceData()
	{
		talk_DB = new Dictionary<int, string[]>();
		portrait_DB = new Dictionary<int, Sprite>();
		//Talk DB
		//Luna:1000, Ludo:2000
		//Box:10000, Desk:20000
		//0:normal, 1:talk, 2:happy, 3:angry (Portrait Image)
		string[] Hang = sheet_text.Split('\n');//줄간격 기준으로 자릅니다. 즉, 1행 2행 3행 이렇게 잘랐다고요
		

        for (int i = 1; i < 5; i++)
        {
			string[] Yeol = Hang[i].Split('\t');
			int NPC_ID = int.Parse(Yeol[2]);
			talk_DB.Add(NPC_ID, new string[] { Yeol[3] });
        }

		string[] hang5 = Hang[5].Split('\t');
		string[] hang6 = Hang[6].Split('\t');
		string[] hang7 = Hang[7].Split('\t');
		string[] hang8 = Hang[8].Split('\t');
		string[] hang9 = Hang[9].Split('\t');
		string[] hang10 = Hang[10].Split('\t');
		string[] hang11 = Hang[11].Split('\t');
		string[] hang12 = Hang[12].Split('\t');

		talk_DB.Add(int.Parse(hang5[2]), new string[] { hang5[3], hang5[4] });
		talk_DB.Add(int.Parse(hang6[2]), new string[] { hang6[3], hang6[4], hang6[5], hang6[6], hang6[7] });
		talk_DB.Add(int.Parse(hang7[2]), new string[] { hang7[3], hang7[4], hang7[5] });
		talk_DB.Add(int.Parse(hang8[2]), new string[] { hang8[3], hang8[4], hang8[5], hang8[6] });
		talk_DB.Add(int.Parse(hang9[2]), new string[] { hang9[3], hang9[4], hang9[5], hang9[6] });
		talk_DB.Add(int.Parse(hang10[2]), new string[] { hang10[3] });
		talk_DB.Add(int.Parse(hang11[2]), new string[] { hang11[3] });
		talk_DB.Add(int.Parse(hang12[2]), new string[] { hang12[3] });


		//Portrait DB
		//0:normal, 1:talk, 2:happy, 3:angry
		portrait_DB.Add(1000 + 0, portrait_arr[0]);
		portrait_DB.Add(1000 + 1, portrait_arr[1]);
		portrait_DB.Add(1000 + 2, portrait_arr[2]);
		portrait_DB.Add(1000 + 3, portrait_arr[3]);
		portrait_DB.Add(2000 + 0, portrait_arr[4]);
		portrait_DB.Add(2000 + 1, portrait_arr[5]);
		portrait_DB.Add(2000 + 2, portrait_arr[6]);
		portrait_DB.Add(2000 + 3, portrait_arr[7]);
	}

	public string GetText(int id, int text_index)
	{
		if (!talk_DB.ContainsKey(id)){
			if (!talk_DB.ContainsKey(id - id % 10))
				return GetText(id - id % 100, text_index);//Default Talk
			else
				return GetText(id - id % 10, text_index);//Loop Talk
		}

		if(text_index == talk_DB[id].Length)
			return null;
		else
			return talk_DB[id][text_index];
	}

	public Sprite Get_Portrait(int id, int portrait_index)
	{
		return portrait_DB[id + portrait_index];
	}
}
