using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
    public TextManager textManager;
    public QuestManager questManager;
    public Animator Talk_Pannel;
    public Image portrait_img;
    public Animator portrait_anim;
	public Sprite past_sprite;
	public TextEffect TALK_TEXT;
    public GameObject ESC_MENU_SET;
    public GameObject Scan_Object;
    public bool is_Action;
    public int text_index;
    public TMP_Text quest_text;
    public GameObject Player;

	void Start()
	{
        Game_Load();
        quest_text.text = questManager.Check_quest();
	}

	void Update()
	{
        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
            Sub_Menu_ACTIVE();
	}

    public void Sub_Menu_ACTIVE()
    {
		if (ESC_MENU_SET.activeSelf)
			ESC_MENU_SET.SetActive(false);
		else
			ESC_MENU_SET.SetActive(true);
	}

	public void Action(GameObject scan_OBJ)
    {
        //Get Current Object
		Scan_Object = scan_OBJ;
		Object_Data object_Data = scan_OBJ.GetComponent<Object_Data>();
		Talk(object_Data.id, object_Data.isNPC);

        //Show talk pannel
        Talk_Pannel.SetBool("isShow", is_Action); 
	}

    void Talk(int id, bool isNPC)
    {
        //set talk data
        int quest_talk_index = 0;
        string talk_DB = "";

		if (TALK_TEXT.isText_ing){
			TALK_TEXT.Set_text("");
            return;
		}
        
        else{
			quest_talk_index = questManager.Get_Quest_Talk_Index(id);
			talk_DB = textManager.GetText(id + quest_talk_index, text_index);
		}

        //end talk
        if(talk_DB == null){
            is_Action = false;
            text_index = 0;
            quest_text.text = questManager.Check_quest(id);
            return;
        }

        //continue talk
        if (isNPC){
            TALK_TEXT.Set_text(talk_DB.Split(':')[0]);

            //Show portrait
            portrait_img.sprite = textManager.Get_Portrait(id, int.Parse(talk_DB.Split(':')[1]));
			portrait_img.color = new Color(1, 1, 1, 1);

			//Animation Portrait
			if (past_sprite != portrait_img.sprite){
                portrait_anim.SetTrigger("doMove");
                past_sprite = portrait_img.sprite;
            }
		}
        else{
			TALK_TEXT.Set_text(talk_DB);

            portrait_img.color = new Color(1, 1, 1, 0);
		}

		is_Action = true;
        text_index++;
	}

    public void Game_Save()
    {
        PlayerPrefs.SetFloat("Player_X",Player.transform.position.x);
        PlayerPrefs.SetFloat("Player_Y", Player.transform.position.y);
        PlayerPrefs.SetInt("Quest_ID", questManager.quest_ID);
        PlayerPrefs.SetInt("Quest_Action_Index", questManager.quest_action_index);
        //Value Save
        PlayerPrefs.Save();

        ESC_MENU_SET.SetActive(false);
    }

    public void Game_Load()
    {
        if(!PlayerPrefs.HasKey("Player_X"))
            return;

        float x = PlayerPrefs.GetFloat("Player_X");
        float y = PlayerPrefs.GetFloat("Player_Y");
        int quest_ID = PlayerPrefs.GetInt("Quest_ID");
        int quest_action_index = PlayerPrefs.GetInt("Quest_Action_Index");

        Player.transform.position = new Vector3(x, y, 0);
        questManager.quest_ID = quest_ID;
        questManager.quest_action_index = quest_action_index;
        questManager.Control_Object();
	}

    public void GAME_EXIT()
    {
		Application.Quit();
	}
}
