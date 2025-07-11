// C#은 다른 파일에 있는 클래스의 사용을 위해서 using을 써야한다.
// Item Class와 아이템 목록을 담을 List를 사용하기 위해서 추가!
using System.Collections.Generic;

public class Player
{
    public int Level { get; set; } // 레벨
    public string Name { get; } // 이름
    public string Job { get; } // 직업
    public int Atk { get; set; }  // 공격력 == 권능
    public int Def { get; set; } // 방어력 == 명경
    public int Hp { get; set; }  // 체력
    public int Gold { get; set; } //공덕

    // Item 목록은 List 안에 담는다. 아이템 객체들만 담을 수 있는 '목록'이다. 
    public List<Item> Inventory { get; set; }

    // 플레이어의 초기 상태를 정해주는 생성자.
    public Player(string name)
    {
        Level = 1;
        Name = name;
        Job = "신입 저승사자";
        Atk = 10; // 기본 권능
        Def = 5; // 기본 명경
        Hp = 100;
        Gold = 1500; // 보유 공덕
        Inventory = new List<Item>(); // 인벤토리는 비어있는 상태로 시작!
    }

}