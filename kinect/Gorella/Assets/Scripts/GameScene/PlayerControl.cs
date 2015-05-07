using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	#region variables

	public int score = 0;

	private int h = Screen.height;
	private int w = Screen.width;

	private GameObject bars;
	private int current = 0;
	private PauseMenu pause;
	private GameObject sphereUp;
	private GameObject sphereDown;
	private GameObject sphereMiddle;

	private Sprite idle;
	private Sprite jump;
	private Sprite jumpUp;
	private SpriteRenderer render;
	private int move = 0;

	private GameObject wood;
	private GameObject burned;
	private float speed = 0f;

	private Texture2D box = new Texture2D(1, 1);
	private bool changeMove = false;

	#endregion

	#region  Initialisation
	void Start () {
		pause = Camera.main.transform.GetComponent<PauseMenu>();
		sphereUp = transform.Find("Sphere Up").gameObject;
		sphereDown = transform.Find("Sphere Down").gameObject;
		sphereMiddle = transform.Find("Sphere Middle").gameObject;
		bars = GameObject.Find("Bars");

		Player pl = Camera.main.transform.GetComponent<Player>();
		idle = pl.idle;
		jump = pl.jump;
		jumpUp = pl.jumpUp;

		render = transform.GetComponent<SpriteRenderer>();

		box.SetPixel(0, 0, Color.red);
		box.Apply();
	}

	bool Dead() {
		if (!sphereUp.transform.renderer.isVisible && !sphereDown.transform.renderer.isVisible)
			return true;
		GameObject fire = Camera.main.transform.Find("Fire1").gameObject;
		if (sphereMiddle.transform.position.y < fire.transform.position.y)
			return true;
		return false;
	}

	void Kill(int x) {
		if (Dead() || x == 1)
		{
			PlayerPrefs.SetInt("Score",score);
            PointManController.State = 0;
			Application.LoadLevel(2);
		}
	}

	#endregion

	#region Swipe
	void Update () {
		bool paused = pause.paused;
		speed = Camera.main.GetComponent<CamMove>().speed / 1.15f;
		Kill(0);

        if (!paused && PointManController.State != 0 && (move == 0 || changeMove))
        {
            ArrayList bar = Player.listChildren(bars);
            string name = "cube " + ++current;
            GameObject cube = bar[Player.IndexOfName(bar, name)] as GameObject;

            if (PointManController.State == 1)
            {
                if (!goRight(cube))
                    current--;
                PointManController.State = 0;
            }
            else if (PointManController.State == 2)
            {
                if (!goLeft(cube))
                    current--;
                PointManController.State = 0;
            }
            else if (PointManController.State == 3)
            {
                current++;
                GameObject afterBurn = bar[Player.IndexOfName(bar, "cube " + current)] as GameObject;
                goUp(cube, afterBurn);
                PointManController.State = 0;
            }
        }
	}
	#endregion

	#region GUI
	void OnGUI () {
		string showScore = score.ToString();
		GUIStyle style = new GUIStyle();

		style.fontSize = (int)(Screen.dpi / 10);
		style.hover.textColor = Color.white;
		style.normal.textColor = Color.white;

		GUI.Box(new Rect(0, 0, Screen.width, Screen.height / 8),  "");

		Texture2D oldSkin = GUI.skin.box.normal.background;
		GUI.skin.box.normal.background = box;
		GUI.Box(new Rect(w - (w / 4.2f), (h / 25), (w / 4.48f), (h / 24)), "");
		GUI.skin.box.normal.background = oldSkin;

		GUI.Label(new Rect((w - (h / 12)), (h / 23), (h / 12), (w / 16.8f)), showScore, style);
	}
	#endregion

	#region Animation
	void FixedUpdate() {
		bool paused = pause.paused;

		if (move == 0)
			render.sprite = idle;
        else if (move == 3)
			render.sprite = jumpUp;
		else
			render.sprite = jump;
		if (!paused)
		{
			if (move == 1)
			{
				transform.Translate (Vector3.left * 10f * Time.deltaTime, Space.World);
				transform.Translate (0, Time.deltaTime * (2f * speed), 0, Space.World);
				if (transform.position.x < -1f)
					changeMove = true;
				else
					changeMove = false;
				if (!(transform.position.x > -2f))
				{
					if (wood.transform.Find ("Flame") != null)
						Kill (1);
					transform.position = new Vector2 (transform.position.x, wood.transform.position.y + 1.55f);
					transform.rotation = Quaternion.Euler (0, 0f, 0);
					move = 0;
                    PointManController.State = 0;
				}
			}
			else if (move == 2)
			{
				transform.Translate (Vector3.right * 10f * Time.deltaTime, Space.World);
				transform.Translate (0, Time.deltaTime * (2f * speed), 0, Space.World);
				if (transform.position.x > 1f)
					changeMove = true;
				else
					changeMove = false;
				if (!(transform.position.x < 2f))
				{
					if (wood.transform.Find ("Flame") != null)
						Kill (1);
					transform.position = new Vector2 (transform.position.x, wood.transform.position.y + 1.55f);
					transform.rotation = Quaternion.Euler (0, 180f, 0);
					move = 0;
                    PointManController.State = 0;
				}
			}
			else if (move == 3)
			{
				transform.Translate (Vector3.up * 10f * Time.deltaTime, Space.World);
				transform.Translate (0, Time.deltaTime * (2f * speed), 0, Space.World);
				if (!(transform.position.y < wood.transform.position.y))
				{
					if (burned.transform.Find ("Flame") == null)
						Kill (1);
					transform.position = new Vector2 (transform.position.x, wood.transform.position.y + 1.55f);
					move = 0;
                    PointManController.State = 0;
				}
			}
		}
	}
	
	private bool goLeft(GameObject cube) {
		if (transform.position.x <= -2f || move == 1)
		{
			transform.rotation = Quaternion.Euler(0, 180f, 0);
			return false;
		}

		transform.rotation = Quaternion.Euler(0, 180f, 0);
		move = 1;
		wood = cube;
		score++;
		return true;
	}

	private bool goRight(GameObject cube) {
		if (transform.position.x >= 2f || move == 2)
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
			return false;
		}

		transform.rotation = Quaternion.Euler(0, 0f, 0);
		move = 2;
		wood = cube;
		score++;
		return true;
	}

	private void goUp(GameObject burned, GameObject cube) {
		if (move != 0)
			return;
		move = 3;
		this.burned = burned;
		wood = cube;
		score++;
	}
	#endregion
}