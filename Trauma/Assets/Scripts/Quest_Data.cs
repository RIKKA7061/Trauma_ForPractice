using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Data : MonoBehaviour
{
    public string quest_name;
    public int[] npc_ID;

    public Quest_Data(string name, int[] npc)
    {
        quest_name = name;
		npc_ID = npc;
    }
}
