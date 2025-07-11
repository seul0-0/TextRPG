using System;
using System.Collections.Generic; // List 사용.

public class Program
{
    // static으로 Player와 Item List를 선언. 어느 곳에서든 접근 가능.
    static Player player;
    static List<Item> shopItems;

    // Progrem의 시작점.
    static void Main(string[] args)
    {
        Console.WriteLine("그래, 자네가 이번에 새로 온 신입인가.");
        Console.WriteLine("혼자서 멍하니 있다간 지나가던 귀신이 자네를 데려갈지도 모르지.");
        Console.WriteLine("시왕전의 명부에 이름을 올려야 하니, 자네 이름부터 말해."); Console.Write("이름: ");
        string playerName = Console.ReadLine(); // 사용자의 입력을 받아 playerName 변수에 저장.
        Console.WriteLine();

        GameDataSetting(playerName); // 입력받은 이름을 GameDataSetting 함수에 전달
        ShowStartScreen(); // 시작 화면 보여주기 
    }

    // 1 게임 데이터 세팅
    static void GameDataSetting(string playerName) // (string playerName) 매개변수 추가
    {
        // 플레이어 정보 세팅
        player = new Player(playerName); // Main에서 전달받은 이름으로 플레이어 생성

        // 아이템 정보 세팅 (상점)
        shopItems = new List<Item>();
        shopItems.Add(new Item("영혼을 묶는 쇠사슬", "어떤 원혼이라도 속박할 수 있다.", 0, 7, 0, 1000));
        shopItems.Add(new Item("명계의 두루마기", "망자의 거짓말에 흔들리지 않는다.", 1, 0, 5, 800));
        shopItems.Add(new Item("죄를 씻는 찻잎", "원혼의 한을 달래는 데 사용됩니다.", 2, 0, 0, 500));
        shopItems.Add(new Item("수습용 칙령서", "수습 저승사자를 위해 특수 제작 되었다.", 0, 9, 0, 100));

        // 구매 완료된 아이템은 플레이어 인벤토리에 추가
        player.Inventory.Add(shopItems[3]);
        player.Inventory[0].IsEquipped = false;
    }

    // 2 시작 화면 (메인 메뉴)
    static void ShowStartScreen() // 재귀 호출(Recursive Call) 함수가 자기 자신을 다시 부른다.
    {
        Console.Clear(); // 화면을 깨끗하게 지운다.

        Console.WriteLine($"{player.Name}.. 이게 자네 이름이군. 나는 자네와 첫 임무를 함께하게 된 저승사자 '이신'이다.");
        Console.WriteLine("출근하기 전에 장비 정비부터 해야 하니, 명부와 법구를 확인하도록.");
        Console.WriteLine();
        Console.WriteLine($" - {player.Name}님은 신입 저승사자입니다. 이승으로 출근하기 전, 시왕전에서 정비를 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전입장"); // 메뉴 추가
        Console.WriteLine();
        Console.WriteLine("원하시는 행동 번호를 입력해주세요.");
        Console.Write(">> ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                ShowStatusScreen(); // 상태 보기
                break;
            case "2":
                ShowInventoryScreen(); // 인벤토리 화면으로 
                break;
            case "3":
                ShowShopScreen();// 상점으로
                break;
            case "4": // case 추가
                ShowDungeonScreen(); // 던전 입장 화면으로
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                ShowStartScreen();
                break;
        }
    }

    // 3 상태 보기 화면
    static void ShowStatusScreen()
    {
        Console.Clear(); // 화면을 깨끗하게 지운다.

        Console.WriteLine($"네 자신을 과신하지 마라, {player.Name}. 그저 숫자일 뿐이야.");
        Console.WriteLine("진짜 위기는 그런 걸 전부 무시하고 찾아온다.");
        Console.WriteLine("...준비는 몇 번을 해도 모자라. 명심해.");
        Console.WriteLine();
        Console.WriteLine($" - 이곳은 상태 표시 창입니다. 이곳에서 {player.Name}님의 정보를 확인할 수 있습니다.");
        Console.WriteLine();

        // 핵심 로직: 장착 아이템의 추가 능력치를 계산한다.
        int bonusAtk = 0;
        int bonusDef = 0;
        foreach (Item item in player.Inventory)
        {
            if (item.IsEquipped)
            {
                bonusAtk += item.Atk;
                bonusDef += item.Def;
            }
        }

        Console.WriteLine($"Lv. {player.Level:D2}"); // D2는 숫자를 두 자리로 표현해준다 (예: 1 -> 01)
        Console.WriteLine($"{player.Name} ( {player.Job} )");
        Console.WriteLine();

        // 기본 능력치와 추가 능력치를 함께 표시한다.
        Console.Write($"권능 : {player.Atk + bonusAtk}");
        if (bonusAtk > 0) Console.Write($" (+{bonusAtk})");
        Console.WriteLine();

        Console.Write($"명경 : {player.Def + bonusDef}");
        if (bonusDef > 0) Console.Write($" (+{bonusDef})");
        Console.WriteLine();

        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"공덕 : {player.Gold} 점");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동 번호를 입력해주세요.");
        Console.Write(">> ");

        string input = Console.ReadLine();
        if (input == "0")
        {
            ShowStartScreen();
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
            ShowStatusScreen(); // 잘못 입력하면 상태보기 화면을 다시 보여준다.
        }
    }

    // 4 인벤토리 화면 (수정)
    static void ShowInventoryScreen()
    {
        while (true) // 0. 나가기를 선택하기 전까지 인벤토리 화면을 계속 보여주기 위한 반복문
        {
            Console.Clear();

            Console.WriteLine($"{player.Name}, 화려한 법구에 한눈팔지 마라.");
            Console.WriteLine("가장 필요할 때 널 살려줄 건 결국 손에 익은 기본적인 물건들이야.");
            Console.WriteLine("...명심해, 죽은 자는 어떤 법구도 쥘 수 없다.");
            Console.WriteLine();
            Console.WriteLine($" - 이 곳은 인벤토리입니다. {player.Name}님이 보유 중인 법구를 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                string equippedMark = item.IsEquipped ? "[E] " : "    ";
                Console.WriteLine($"- {equippedMark}{item.Name} | 권능 +{item.Atk} 명경 +{item.Def} | {item.Description}");
            }

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동 번호를 입력해주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    ShowStartScreen(); // 0번을 누르면 시작 화면으로 이동하고,
                    return;           // return을 통해 이 함수(와 while 반복문)를 완전히 종료한다.
                case "1":
                    ManageEquipmentScreen(); // 장착 관리 화면으로 이동
                                             // 이 함수가 끝나면, while 반복문 처음으로 돌아가 인벤토리 화면을 다시 표시한다.
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // 6. 인벤토리 - 장착 관리 화면
    static void ManageEquipmentScreen()
    {
        // while(true) 반복문을 사용하여 유저가 0을 입력하기 전까지 계속 화면을 보여준다.
        while (true)
        {
            Console.Clear();

            Console.WriteLine("뭘 그렇게 망설이나.");
            Console.WriteLine("전장에 나서는 병사가 갑옷을 입었다 벗었다 하진 않아.");
            Console.WriteLine($"지금 당장 필요한 걸 골라 {player.Name}. 네 선택이 곧 너의 그릇을 증명하는 거다.");
            Console.WriteLine();
            Console.WriteLine("이곳에서는 보유 중인 법구를 관리할 수 있습니다.");

            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            // 💡 핵심 로직 1: 아이템 목록 앞에 번호를 붙여준다. (i + 1)
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                string equippedMark = item.IsEquipped ? "[E] " : "    ";
                Console.WriteLine($"- {i + 1} {equippedMark}{item.Name} | 권능 +{item.Atk} 명경 +{item.Def} | {item.Description}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동 번호를 입력해주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();

            // 핵심 로직 2: 사용자 입력을 숫자로 변환하고 유효성을 검사한다.
            // int.TryParse는 변환이 성공하면 true, 실패하면 false를 반환한다.
            // 변환된 숫자는 choice 변수에 저장된다.
            if (int.TryParse(input, out int choice) && choice >= 0 && choice <= player.Inventory.Count)
            {
                if (choice == 0)
                {
                    // 0을 입력하면 반복문을 탈출하여 이전 화면으로 돌아간다.
                    break;
                }
                else
                {
                    // 핵심 로직 3: 선택한 아이템의 장착 상태를 변경한다.
                    // 리스트의 인덱스는 0부터 시작하므로, 사용자가 1을 입력하면 인덱스는 0이 된다. (choice - 1)
                    Item selectedItem = player.Inventory[choice - 1];
                    selectedItem.IsEquipped = !selectedItem.IsEquipped; // true는 false로, false는 true로!
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }

    // 7. 상점 화면
    static void ShowShopScreen()
    {
        Console.Clear();

        Console.WriteLine("오늘 상대할 원혼은 만만치 않을 거다.");
        Console.WriteLine("..네 목숨값은 할 만한 걸로 신중하게 골라.");
        Console.WriteLine();
        Console.WriteLine("[보유 공덕]");
        Console.WriteLine($"{player.Gold} 점");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        // 상점에서 판매하는 아이템 목록을 보여준다.
        for (int i = 0; i < shopItems.Count; i++)
        {
            Item item = shopItems[i];

            // 핵심 로직: 플레이어가 이미 아이템을 가지고 있는지 확인한다.
            bool isSoldOut = false;
            foreach (Item inventoryItem in player.Inventory)
            {
                if (inventoryItem.Name == item.Name)
                {
                    isSoldOut = true;
                    break; // 이미 찾았으면 더 이상 반복할 필요 없다.
                }
            }

            Console.Write($"- {item.Name} | 권능 +{item.Atk} 명경 +{item.Def} | {item.Description} | ");

            if (isSoldOut)
            {
                Console.WriteLine("구매완료!");
            }
            else
            {
                Console.WriteLine($"{item.Price} 점");
            }
        }

        Console.WriteLine();
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동 번호를 입력해주세요.");
        Console.Write(">> ");

        string input = Console.ReadLine();
        switch (input)
        {
            case "0":
                ShowStartScreen();
                break;
            case "1":
                BuyItemScreen();
                break; // 구매 화면으로 이동
            default:
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                ShowShopScreen();
                break;
        }
    }

    // 8. 상점 - 아이템 구매 화면
    static void BuyItemScreen()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("- 필요한 법구를 얻을 수 있는 저승 주막입니다. 원하는 법구를 골라보세요.");
            Console.WriteLine();
            Console.WriteLine("[보유 공덕]");
            Console.WriteLine($"{player.Gold} 점");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            // 번호와 함께 상점 아이템 목록을 보여준다.
            for (int i = 0; i < shopItems.Count; i++)
            {
                Item item = shopItems[i];
                bool isSoldOut = player.Inventory.Exists(inventoryItem => inventoryItem.Name == item.Name);

                Console.Write($"- {i + 1} {item.Name} | 권능 +{item.Atk} 명경 +{item.Def} | {item.Description} | ");
                if (isSoldOut)
                {
                    Console.WriteLine("구매완료");
                }
                else
                {
                    Console.WriteLine($"{item.Price} 점");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동 번호를 입력해주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 0 && choice <= shopItems.Count)
            {
                if (choice == 0)
                {
                    ShowShopScreen(); // 상점 화면으로 돌아가기
                    break;
                }
                else
                {
                    Item selectedItem = shopItems[choice - 1];

                    // 핵심 로직 1: 이미 구매했는지 확인
                    if (player.Inventory.Exists(inventoryItem => inventoryItem.Name == selectedItem.Name))
                    {
                        Console.WriteLine("이미 구매한 법구입니다.");  
                    }
                    // 핵심 로직 2: 공덕이 충분한지 확인
                    else if (player.Gold >= selectedItem.Price)
                    {
                        player.Gold -= selectedItem.Price; // 공덕 차감
                        player.Inventory.Add(new Item(selectedItem.Name, selectedItem.Description, selectedItem.Type, selectedItem.Atk, selectedItem.Def, selectedItem.Price, true));
                        Console.WriteLine("구매를 완료했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("공덕이 부족합니다.");
                    }
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
             }
        }
    }

    // 9. 던전 입장 화면
    static void ShowDungeonScreen()
    {
        Console.Clear();
        Console.WriteLine("저승의 악귀들이 들끓는 곳이다. 단단히 준비하고 들어가.");
        Console.WriteLine("...한 번 들어가면, 살아서든 죽어서든 끝을 봐야만 나올 수 있다.");
        Console.WriteLine();
        Console.WriteLine("1. 떠도는 잡귀들의 소굴   | 명경 5 이상 권장"); 
        Console.WriteLine("2. 분노한 야차의 복도    | 명경 11 이상 권장");
        Console.WriteLine("3. 심연에서 깨어난 대악귀  | 명경 17 이상 권장");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        string input = Console.ReadLine();
        switch (input)
        {
            case "0":
                ShowStartScreen();
                break;
            case "1":
                AttemptDungeon(5, 1000, "떠도는 잡귀들의 소굴");
                break;
            case "2":
                AttemptDungeon(11, 1700, "분노한 야차의 복도");
                break;
            case "3":
                AttemptDungeon(17, 2500, "심연에서 깨어난 대악귀");
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                ShowDungeonScreen();
                break;
        }
    }

    // 10. 던전 공략 함수
    static void AttemptDungeon(int requiredDef, int baseGold, string dungeonName)
    {
        Console.Clear();
        Random rand = new Random();

        int initialHp = player.Hp;
        int initialGold = player.Gold;
        int hpLoss; // 변수를 여기서 한 번만 선언합니다! (자리 예약)

        // 1. 방어력 비교 및 성공/실패 결정
        if (player.Def < requiredDef)
        {
            // 권장 방어력 미만일 경우 40% 확률로 실패
            if (rand.Next(1, 101) <= 40)
            {
                // 실패 처리
                // 'int'를 빼고 값만 할당합니다.
                hpLoss = rand.Next(20 - (player.Def - requiredDef), 36 - (player.Def - requiredDef)) / 2;
                player.Hp -= hpLoss;
                if (player.Hp < 0) player.Hp = 0;

                Console.WriteLine($"던전 공략 실패...");
                Console.WriteLine($"{player.Name}, 아직은 저 악귀들을 상대하긴 일렀던 것 같군.");
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {initialHp} -> {player.Hp}");

                Console.WriteLine("\n0. 나가기");
                Console.Write(">> ");
                while (Console.ReadLine() != "0") { } // 0을 입력할 때까지 대기
                ShowDungeonScreen();
                return; // 함수 종료
            }
        }

        // 2. 성공 시 결과 계산
        // 2-1. 체력 감소 계산
        int defenseDiff = player.Def - requiredDef;
        // 여기도 'int'를 빼고 값만 할당합니다.
        hpLoss = rand.Next(20 - defenseDiff, 36 - defenseDiff);
        player.Hp -= hpLoss;
        if (player.Hp < 0) player.Hp = 0;

        // 2-2. 보상 계산
        int bonusPercentage = rand.Next(player.Atk, player.Atk * 2 + 1);
        int bonusGold = (int)(baseGold * (bonusPercentage / 100.0));
        int totalGold = baseGold + bonusGold;
        player.Gold += totalGold;

        // 3. 결과 출력
        Console.WriteLine("던전 클리어");
        Console.WriteLine("축하합니다!!");
        Console.WriteLine($"{dungeonName}을/를 클리어 하였습니다.");
        Console.WriteLine();
        Console.WriteLine("[탐험 결과]");
        Console.WriteLine($"체력 {initialHp} -> {player.Hp}");
        Console.WriteLine($"Gold {initialGold} G -> {player.Gold} G");

        Console.WriteLine("\n0. 나가기");
        Console.Write(">> ");
        while (Console.ReadLine() != "0") { } // 0을 입력할 때까지 대기
        ShowDungeonScreen();
    }
}
