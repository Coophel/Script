using System.Collections;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
	public MainResource GameResource;
	public PageController PageController;
	public AgentManager AgentController;
	public Ship myShip;

	public int days;

#region Unity Funtions
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
#endregion

#region Public Functions
	public void NewGameStart()
	{
		days = 1;
		GameResource.initializeNewGame();
		PageController = null;
		SceneControll.Instance.LoadScene("GameScene");
	}

	public void SaveGameStatus()
	{
		// save game data functions.


		PageController = null;
		SceneControll.Instance.LoadScene("MainScene");
	}

	public void LoadGameStart()
	{
		// make load data
	}

//  12 . 01 starting code
	public void PassDays()
	{
		Debug.Log("Working on it");

		CheckMovement();
		// if ture : start wrap ani. -> move to next map <if (energy is enough)>
		// if false : not move and start ani .  check next.

		CheckEvent();
		// if ture : Go to Event Dialgue.
		// if false : check next.

		CheckBattle();
		// if ture : GO to battle -> using ammo / Or use energy to run away
		// if false : check next.

		UseResource();
		// minuse resource about ship and people

		CheckLive();
		// check our ship can survival and add days
	}
#endregion

#region Private Functions
	private void CheckMovement()
	{

	}

	private void CheckEvent()
	{

	}

	private void CheckBattle()
	{

	}

	private void UseResource()
	{
		int count = AgentController.ActiveNpc();
		GameResource.food -= count;

		if (GameResource.food < 0)
		{
			while (GameResource.food < 0)
			{
				AgentController.DamgeAllNpc(10);
				GameResource.food++;
			}
		}
	}

	private void CheckLive()
	{
		// people damge check;
		if (AgentController.ActiveNpc() <= 0)
		{
			StartCoroutine("GameOver");
			return ;
		}
		// Ship damge check;
		if (Ship.Instance.CurrentHp <= 0)
		{
			StartCoroutine("GameOver");
			return ;
		}
		days++;
	}

	// Show GameOver then return to MainScene
	private IEnumerator GameOver()
	{
		PageController.Instance.TurnPageOff(PageType.Game_Interface, PageType.Game_Over);
		yield return new WaitForSeconds(5f);
		SceneControll.Instance.LoadScene("MainScene");
	}
#endregion
}
