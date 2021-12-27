using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
	public static Ship Instance;
	// 무슨 무기 / 함선의 기능실 무엇이 있는지 정함 database
	public ShipDatabase		Shipdatas;

	// 사물들의 위치 배열
	public GameObject[]		guntransform;
	public GameObject[]		moduletransform;

	// ship 통합 체력
	public int ShipHp;
	public int CurrentHp;
	public int[] GunSector;

#region Unity Functions
	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		GunSector = new int[] {0, 1, 2, 3};
		guntransform = new GameObject[GunSector.GetLength(0)];
		moduletransform = new GameObject[5];
		for (var i = 0; i < 4; i++)
		{
			guntransform[i] = GameObject.Find("Gun_" + (i + 1));
		}

		for (var i = 0; i < 5; i++)
		{
			moduletransform[i] = GameObject.Find((i + 1).ToString());
		}
	}

	private void Start()
	{
		// 불러오기 시 행동 추가 필요
		// 새로 시작시
		MakeRandomShip();
	}
#endregion

#region Public Functions
	public bool ShipAlive()
	{
		if (CurrentHp <= 0)
			return false;
		else
			return true;
	}
#endregion

#region Private Functions
	private void MakeRandomShip()
	{		
		// 필수 함선실 항목 추가
		for (var i = 0; i < 2; i++)
		{
			MountModule(i, Shipdatas.shipModules[i + 1].moduleType);
		}

		// 무작위 항목 추가 - 임시
		MountModule(2 , Shipdatas.shipModules[Random.Range(3,5)].moduleType);
		MountModule(3 , Shipdatas.shipModules[Random.Range(3,5)].moduleType);
		MountGun(1, Shipdatas.gunModules[Random.Range(1,4)].attackType);
		MountGun(2, Shipdatas.gunModules[Random.Range(1,4)].attackType);

		ShipHp = 100;
		foreach (var module in moduletransform)
		{
			ShipHp += module.gameObject.GetComponent<Room>().moduleHp;
		}
	}

	private void MountModule(int shipsector, roomType room = roomType.None)
	{
		moduletransform[shipsector].GetComponent<Room>().SetRoom(Shipdatas.findroom(room));
	}

	private void ResetGunMount()
	{
		foreach(var gun in guntransform)
		{
			gun.gameObject.GetComponent<Gun>().SetGun(Shipdatas.gunModules[0]);
		}
	}

	private void UnMountGun(int gunsector)
	{
		guntransform[gunsector].GetComponent<Gun>().SetGun(Shipdatas.gunModules[0]);
	}

	private void MountGun(int gunsector, gunType gun = gunType.none)
	{
		guntransform[gunsector].GetComponent<Gun>().SetGun(Shipdatas.findGun(gun));
	}
#endregion
}
