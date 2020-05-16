using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_INPUT : MonoBehaviour
{
	public CHARACTER_MOVE move;
	public CHARACTER_ATTACK attack;

	private void Update()
	{
		move.Set_Direction(Input.GetAxisRaw("Horizontal"));
		if (Input.GetButtonDown("Jump")) move.Jump();
		if (Input.GetButtonDown("Drop")) move.Drop();
		if (Input.GetButton("Fire1")) attack.Attack();
	}
}
