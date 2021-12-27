#region In Game
// 함선 방 타입
public enum roomType
{
	None,
	Gunport,
	Healing,
	Bridge,
	foodfactory,
	ammofactory
}
// 함선 무기 타입 (빈 자리에 추가 설치 가능)
public enum gunType
{
	none,
	physicsgun,
    energygun,
    missils
}
// 맵 타일 타입
public enum NodeType
{
	unidentified,
	enemy,
	store,
	home
}
#endregion

#region UI Enum
// 메뉴 페이지 타입
public enum PageType
{
	None,
	Main,
	Option,
	Game_Interface,
	Game_Option,
	Game_Over,
	Dialogue,
	MapSelect,
	EnemyUI
}
#endregion
