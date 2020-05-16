using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHARACTER_ATTACK : MonoBehaviour
{
	public CHARACTER_HEALTH self;
	public float attack_time;
	public float attack_damage;
	public Transform attack_position;
	public Vector2 attack_size;
	Coroutine current_attack;

	public bool debug;

	public void Attack()
	{
		if(current_attack==null)
			current_attack = StartCoroutine(Do_Attack());
	}

	IEnumerator Do_Attack()
	{
		List<CHARACTER_HEALTH> healths = new List<CHARACTER_HEALTH>();
		for (float t = 0; t < 1f; t += Time.deltaTime / attack_time)
		{
			yield return null;
			CHARACTER_HEALTH[] hits = Melee(attack_position, attack_size);
			foreach(CHARACTER_HEALTH health in hits)
			{
				health.Take_Damage(attack_damage);
				if (!healths.Contains(health))
					healths.Add(health);
			}
		}

		foreach(CHARACTER_HEALTH health in healths)
		{
			health.Reset_Vulnerability();
		}

		current_attack = null;
	}

	public CHARACTER_HEALTH[] Melee(Transform transform, Vector2 size)
	{
		float box_size = size.x > size.y ? size.y : size.x;
		float distance = (size.x > size.y ? size.x : size.y) - box_size;
		float angle = transform.eulerAngles.z;
		Vector2 fire_direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
		Vector2 start = new Vector2(transform.position.x, transform.position.y) + (fire_direction * box_size / 2);

		RaycastHit2D[] hits = Physics2D.BoxCastAll(start, Vector2.one * box_size, angle, fire_direction, distance);

		if (debug)
		{
			Vector2 direction = new Vector2(Mathf.Cos((angle + 90f) * Mathf.Deg2Rad), Mathf.Sin((angle + 90f) * Mathf.Deg2Rad));
			Vector2 pointA = new Vector2(transform.position.x + size.y / 2 * direction.x, transform.position.y + size.y / 2 * direction.y);
			Vector2 pointB = new Vector2(transform.position.x - size.y / 2 * direction.x, transform.position.y - size.y / 2 * direction.y);
			Vector2 pointC = new Vector2(pointA.x + size.x * direction.y, pointA.y - size.x * direction.x);
			Vector2 pointD = new Vector2(pointB.x + size.x * direction.y, pointB.y - size.x * direction.x);
			Debug.DrawLine(pointA, pointB, Color.blue);
			Debug.DrawLine(pointA, pointC, Color.blue);
			Debug.DrawLine(pointB, pointD, Color.blue);
			Debug.DrawLine(pointC, pointD, Color.blue);
		}

		List<CHARACTER_HEALTH> healths = new List<CHARACTER_HEALTH>();

		foreach (RaycastHit2D hit in hits)
		{
			if (hit.transform.GetComponent<CHARACTER_HEALTH>() != null)
			{
				if (hit.transform.GetComponent<CHARACTER_HEALTH>() != self)
				{
					if (debug)
						Debug.DrawRay(hit.point, hit.normal * 0.1f, Color.red);
					healths.Add(hit.transform.GetComponent<CHARACTER_HEALTH>());
				}
			}
		}

		return healths.ToArray();
	}
}
