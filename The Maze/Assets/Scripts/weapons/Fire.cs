using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Fire : MonoBehaviour {

	public GameObject bullet;
	public GameObject []SpawnPoint;
	public GameObject target;
	public GameObject gun;

	private bool fire = false;
	private int angle = 1;
	private int i = 0;
	private int x = 0;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == target)
		{
			audio.Play();
			fire = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == target)
		{
			audio.Stop();
			fire = false;
		}
	}

	void Update () {
		if (fire)
		{
			MoveGun();

			GameObject joujma = new GameObject();
			joujma.transform.rotation = Quaternion.Euler(0, 0, 0);

			Instantiate(bullet, SpawnPoint[i].transform.position, joujma.transform.rotation);

			i++;
			if (i > 5)
				i = 0;
		}
	}
	
	private void MoveGun() {
		if (x < 250)
		{
			gun.transform.Rotate(gun.transform.up, Time.deltaTime * 10);
			x++;
		}
		else if (x < 750)
		{
			gun.transform.Rotate(- gun.transform.up, Time.deltaTime * 10);
			x++;
		}
		else
			x = 0;
	}
}