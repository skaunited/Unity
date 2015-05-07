using UnityEngine;
using System.Collections;

public class FoodBehaviour : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Snake")
		{
			SnakeBehaviour snake = other.GetComponent<SnakeBehaviour>();
			snake.Score++;
		}
		Destroy(transform.gameObject);
	}
	
}