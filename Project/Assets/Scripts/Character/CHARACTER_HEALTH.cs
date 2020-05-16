using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_HEALTH : MonoBehaviour
{
	public float max_health;
	public float current_health;
	bool invulnerable;

	private void Start()
	{
		invulnerable = false;
		current_health = max_health;
	}

	public void Take_Damage(float damage)
	{
		if (invulnerable)
			return;

		invulnerable = true;
		current_health -= damage;
		if (current_health <= 0)
			Die();
	}

	public void Reset_Vulnerability()
	{
		invulnerable = false;
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
