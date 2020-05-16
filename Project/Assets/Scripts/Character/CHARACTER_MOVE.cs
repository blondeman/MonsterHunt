using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_MOVE : MonoBehaviour
{
	public new Rigidbody2D rigidbody;
	public float speed;
	public float jump_velocity;
	public float drop_velocity;
	float direction;

	public void Set_Direction(float dir)
	{
		direction = dir;
	}

	private void FixedUpdate()
	{
		rigidbody.velocity = new Vector2(direction * speed, rigidbody.velocity.y);
	}

	public void Jump()
	{
		rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_velocity);
	}

	public void Drop()
	{
		if(rigidbody.velocity.y > -drop_velocity)
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, -drop_velocity);
	}
}
