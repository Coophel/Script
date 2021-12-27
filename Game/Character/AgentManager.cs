using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
	public static AgentManager Instance;

    public List<AgentScript> npcs;
	public GameObject npcout;

#region Unity Functions
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Gamemanager.Instance.AgentController = this;
		}
		else
		{
			Destroy(gameObject);
		}

		StartNpc(10);
	}

	private void Start()
	{
		// if new game start
		for (int i = 0; i < 4; i++)
		{
			GetNpc(i);
		}
		// if load game start

	}
#endregion

#region Public Functions
	public static AgentScript GetNpc(int index)
	{
		if (Instance.npcs.Count - 1 > index)
		{
			var npc = Instance.npcs[index];
			npc.gameObject.SetActive(true);
			return npc;
		}
		else
		{
			var newnpc = Instance.CreateNewNpc();
			newnpc.gameObject.SetActive(true);
			Instance.npcs.Add(newnpc);
			return newnpc;
		}
	}

	public static void ReturnNpc(AgentScript agent)
	{
		agent.gameObject.SetActive(false);
	}

	public void DamgeAllNpc(int damge)
	{
		for(int i = 0; i < npcs.Count; i++)
		{
			if (npcs[i].gameObject.activeSelf)
				npcs[i].status.npcHp -= damge;
			
			if (npcs[i].status.npcHp < 0)
			{
				// set ani die
				ReturnNpc(npcs[i]);
			}
		}
	}

	public int ActiveNpc()
	{
		int count = 0;
		for(int i = 0; i < npcs.Count; i++)
		{
			if (npcs[i].gameObject.activeSelf)
				count++;
		}
		return count;
	}
#endregion

#region  Private Functions
	private void StartNpc(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Instance.npcs.Add(CreateNewNpc());
		}
	}

	private AgentScript CreateNewNpc()
	{
		var newNpc = Instantiate(npcout, transform).GetComponent<AgentScript>();
		newNpc.gameObject.SetActive(false);
		return newNpc;
	}
#endregion
}
