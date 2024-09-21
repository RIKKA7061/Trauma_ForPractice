using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int quest_ID;
	public int quest_action_index;
	public GameObject[] quest_object;
    Dictionary<int, Quest_Data> quest_List;

	void Awake()
	{
		quest_List = new Dictionary<int, Quest_Data>();
		Produce_Data();
	}
	void Produce_Data()
	{
		quest_List.Add(10, new Quest_Data("이세계에서 대화 시도", new int[] {1000, 2000}));
		quest_List.Add(20, new Quest_Data("루도의 지갑 찾아주기", new int[] {50000, 2000 }));
		quest_List.Add(30, new Quest_Data("퀘스트 올 클리어!!!", new int[] { 0 }));
	}

	public int Get_Quest_Talk_Index(int id)
	{
		return quest_ID + quest_action_index;
	}

	public string Check_quest(int id)
	{
		//next talk target person
		if (id == quest_List[quest_ID].npc_ID[quest_action_index])
			quest_action_index++;

		//control quest object
		Control_Object();

		//talk complete & next quest
		if (quest_action_index == quest_List[quest_ID].npc_ID.Length)
			Next_Quest();

		//quest name
		return quest_List[quest_ID].quest_name;
	}

	public string Check_quest()
	{
		//quest name
		return quest_List[quest_ID].quest_name;
	}

	void Next_Quest()
	{
		quest_ID += 10;
		quest_action_index = 0;
	}

	public void Control_Object()
	{
		switch(quest_ID){
			case 10:
				if (quest_action_index == 2)
					quest_object[0].SetActive(true);
				break;
			case 20:
				if (quest_action_index == 0)
					quest_object[0].SetActive(true);
				else if (quest_action_index == 1)
					quest_object[0].SetActive(false);
				break;
		}
	}
}
