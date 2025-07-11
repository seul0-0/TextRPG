public class Item //'Item' 이라는 이름의 공개된 설계도를 만든다.
{
    // 아이템은 이름, 설명, 타입, 능력치, 가격 등의 정보를 가진다.  
    public string Name { get; } //이름 
    public string Description { get; } //설명 
    // { get; } 은 이 값을 읽을 수만 있다. 한번 정해지면 바꿀 수 없다.

    // 아이템 타입: 0: 무기, 1: 방어구
    public int Type { get; }

    public int Atk { get; } //권능
    public int Def { get; } //명경

    public int Price { get; }

    // 아이템 장착 , 구매여부 저장.
    public bool IsEquipped { get; set; } //{ get; set; } 은 읽고 쓸 수 있다는 뜻.
    public bool IsPurchased { get; set; }


    // 아이템 정보를 초기화해주는 생성자(Constructor). new Item(...) 처럼 새 아이템을 만들 때, 초기 정보를 받아서 아이템 객체를 조립해준다.
    public Item(string name, string description, int type, int atk, int def, int price, bool isPurchased = false)
    {
        Name = name; // 이름 설정
        Description = description; // 설명 설정
        Type = type; // 타입 설정
        Atk = atk; // 공격력 설정
        Def = def; // 방어력 설정
        Price = price; // 가격 설정
        IsEquipped = false; //'기본값'을 설정하는 문법이다. 기본적으로 '구매하지 않음(false)' 상태.
        IsPurchased = isPurchased;  // 구매 상태 설정
    }

}
