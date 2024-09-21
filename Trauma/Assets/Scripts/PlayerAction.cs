using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	public float speed = 1f;
	public TalkManager manager;
    float h;
    float v;
	bool is_Horizon_Move;
	Vector3 Direction_Vector;
	GameObject scan_Object;

    Rigidbody2D rigid;
	Animator anim;

	//Mobile Key Var
	int U_int;
	int D_int;
	int L_int;
	int R_int;
	bool U_down;
	bool D_down;
	bool L_down;
	bool R_down;
	bool U_up;
	bool D_up;
	bool L_up;
	bool R_up;

	void Awake() 
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update() 
	{
		//Move Value
		h = manager.is_Action ? 0 : Input.GetAxisRaw("Horizontal") + R_int + L_int;
		v = manager.is_Action ? 0 : Input.GetAxisRaw("Vertical") + D_int + U_int;

		//Check Button Down & Up
		bool hDwon = manager.is_Action ? false : Input.GetButtonDown("Horizontal") || R_down || L_down;
		bool vDown = manager.is_Action ? false : Input.GetButtonDown("Vertical") || U_down || D_down;
		bool hUp = manager.is_Action ? false : Input.GetButtonUp("Horizontal") || R_up || L_up;
		bool vUp = manager.is_Action ? false : Input.GetButtonUp("Vertical") || U_up || D_up;

		//Check Horizontal Move
		if (hDwon || vUp)
			is_Horizon_Move = true;
		else if (vDown || hUp)
			is_Horizon_Move = false;
		else if (hUp || vUp)
			is_Horizon_Move = h != 0;

		//Animation
		if (anim.GetInteger("hAxisRaw") != h){
			anim.SetBool("isChange", true);
			anim.SetInteger("hAxisRaw", (int)h);
		}
		else if (anim.GetInteger("vAxisRaw") != v){
			anim.SetBool("isChange", true);
			anim.SetInteger("vAxisRaw", (int)v);
		}
		else
			anim.SetBool("isChange", false);

		//Direction
		if(vDown && v == 1)
			Direction_Vector = Vector3.up;
		else if (vDown && v == -1)
			Direction_Vector = Vector3.down;
		else if (hDwon && h == -1)
			Direction_Vector = Vector3.left;
		else if (hDwon && h == 1)
			Direction_Vector = Vector3.right;

		//Scan Object
		if (Input.GetButtonDown("Jump")&&scan_Object != null)
			manager.Action(scan_Object);

		//Mobile Var Init √ ±‚»≠
		U_down = false;
		D_down = false;
		L_down = false;
		R_down = false;
		U_up = false;	
		D_up = false;	
		L_up = false;
		R_up = false;
	}

	void FixedUpdate()
	{
		//Move
		Vector2 move_Vector = is_Horizon_Move ? new Vector2(h,0) : new Vector2(0, v);
		rigid.velocity = move_Vector * speed;

		//Ray
		Debug.DrawRay(rigid.position, Direction_Vector * 0.7f,new Color(0,1,0));
		RaycastHit2D ray_Hit = Physics2D.Raycast(rigid.position, Direction_Vector, 0.7f, LayerMask.GetMask("Object"));

		if (ray_Hit.collider != null)
		{
			scan_Object = ray_Hit.collider.gameObject;
		}
		else
			scan_Object = null;
	}

	public void Button_Down(string TYPE)
	{
		switch(TYPE){
			case "U":
				U_int = 1;
				U_down = true;
				break;
			case "D":
				D_down = true;
				D_int = -1;
				break;
			case "L":
				L_down = true;
				L_int = -1;
				break;
			case "R":
				R_down = true;
				R_int = 1;
				break;
			case "A":
				if (scan_Object != null)
					manager.Action(scan_Object);
				break;
			case "C":
				manager.Sub_Menu_ACTIVE();
				break;
		}
	}

	public void Button_Up(string TYPE)
	{
		switch (TYPE)
		{
			case "U":
				U_int= 0;
				U_up = true;
				break;
			case "D":
				D_up = true;
				D_int= 0;
				break;
			case "L":
				L_up = true;
				L_int = 0;
				break;
			case "R":
				R_up = true;
				R_int = 0;
				break;
		}
	}
}
