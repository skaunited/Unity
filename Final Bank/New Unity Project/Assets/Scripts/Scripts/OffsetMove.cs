using UnityEngine;
using System.Collections;

public class OffsetMove : MonoBehaviour
{
	public float speed = 0f;

	void Update ()
	{
		renderer.material.mainTextureOffset = new Vector2 (0f, (Time.time * speed) % 1);
	}
}
