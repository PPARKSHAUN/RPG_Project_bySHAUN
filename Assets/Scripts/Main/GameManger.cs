using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
[System.Serializable]
public class Item
{
    public Item(string _ItemType, string _Type, string _Name, string _Explain, string _Number, bool _isUsing, string _value) // 생성자 생성 
    {
        ItemType = _ItemType; // 밑에 값에다가 대입 해준다 
        Type = _Type; //이하동문 
        Name = _Name;
        Explain = _Explain;
        Number = _Number;
        isUsing = _isUsing;
        value = _value;
    }
    public string ItemType, Type, Name, Explain, Number, value;
    public bool isUsing;

}

public class GameManger : MonoBehaviour
{
    public static GameManger instance;
    public TextAsset ItemDatabase;// 아이템 데이터베이스를 연결해주기위해 선언 
    public List<Item> Alltem, MyItemList, CurItemList,UsingItemList,DropTabl,QuickSlotItem; //아이템을 리스트 해주기위해
    public string curTab = "All";//처음 탭 타입 
    public Image[] SelectImg; // 아이템 셀렉 이미지 켜주기위해 배열로받고
    public GameObject[] Slot, Eqipslot,off,on,InvenQuickSlot,MainQuickSlot;//슬롯을 넣기위해 
    public Image[] ItemImage,EqipItemImage; // 슬롯 아이템 이미지 연결 
    public Sprite[] ItemSprite; // 아이템 데이터 베이스순서에 맞게 스프라이트 연결
    public Sprite none;
    public Transform player;
    public GameObject ExplainPanel;//팝업창 패널 
    IEnumerator pointercoroutine; //코루틴 스타트 스탑 해주기위해 선언 
    public GameObject DropPanel;
    public Animator dropanim;
    public Text[] InvenStat;
   
    Item CurItem;
    // Start is called before the first frame update
    private void Update()
    {
        ExplainPanel.transform.position = Input.mousePosition ;

    }
    
    void Awake()
    {
       for(int i=0;i<4;i++)
        {
            QuickSlotItem.Add(null);
        }
        
        if (instance==null)
        {
            instance = this;
          
        }
     
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n'); // 아이템 데이터 베이스가 저장되면 마지막줄은 엔터라서 엔터빼고 받기위해서  
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');// 라인 0 번을 탭으로 나누고 넣어준다 row 에 
            Alltem.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE", row[6]));// 아이템타입,타입,이름,설명,갯수,장착여부 를 넘겨준다 . 
        }

        Load();
        DrawQuickslot();
    }
    public void Inventorystat()
    {
        InvenStat[0].text = "레벨 : "+CharacterManger.instance.stat.Level.ToString();
        InvenStat[1].text = "직업 : "+CharacterManger.instance.unitCode.ToString();
        InvenStat[2].text = "공격력 : "+(int)(CharacterManger.instance.stat.Damage) * 1 + "~" + (int)(CharacterManger.instance.stat.Damage) * 1.2;
        InvenStat[3].text = "체력 : "+CharacterManger.instance.stat.maxHp.ToString();
        InvenStat[4].text = "마나 : "+CharacterManger.instance.stat.curMp.ToString();
    }
    public void InventoryButtonClick()
    {
        
        bool click = true;
       for(int i=0;i<off.Length;i++)
        {
            off[i].SetActive(!click);
        }
       for(int i=0;i<on.Length;i++)
        {
            on[i].SetActive(click);
        }
       on[0].transform.position=player.position+new Vector3(0,1,2);
        Inventorystat();


    }
    public void exitbutton()
    {
        bool click = true;
        for (int i = 0; i < off.Length; i++)
        {
            off[i].SetActive(click);
        }
        for (int i = 0; i < on.Length; i++)
        {
            on[i].SetActive(!click);
        }
    }
    public void DrawQuickslot() // 퀵슬롯 아이템갯수, 아이템아이콘 그려주는 역활 및 save 
    {
        for(int i=0;i<QuickSlotItem.Count;i++)
        {
            if(QuickSlotItem[i]!=null)//퀵슬롯 아이템리스트에 아이템이 존재한다면 
            {
                InvenQuickSlot[i].GetComponentInChildren<Text>().text = QuickSlotItem[i].Number.ToString(); //연결된 인벤토리 퀵슬롯 갯수 text는 quickslotitem number를 받아서 넣어주고
                InvenQuickSlot[i].transform.GetChild(1).GetComponent<Image>().sprite = ItemSprite[Alltem.FindIndex(x => x.Name == QuickSlotItem[i].Name)];//이미지도 같다

            }
            else // 존재하지않는다면  초기화 
            {
                InvenQuickSlot[i].GetComponentInChildren<Text>().text = null; 
                InvenQuickSlot[i].transform.GetChild(1).GetComponent<Image>().sprite = none;
            }
            MainQuickSlot[i].GetComponentInChildren<Text>().text = InvenQuickSlot[i].GetComponentInChildren<Text>().text;//메인 과 인벤 퀵슬롯연결 이하동문
            MainQuickSlot[i].transform.GetChild(1).GetComponent<Image>().sprite = InvenQuickSlot[i].transform.GetChild(1).GetComponent<Image>().sprite;
        }
        Save();//json데이터로 저장 
    }
    public void registQuickSlot()//퀵슬롯아이템리스트에 등록함수 포션일때만 호출된다 
    {
        for (int i = 0; i < QuickSlotItem.Count; i++)
        {
            if (QuickSlotItem[i] == null)//퀵슬롯 아이템이 null 이라면 
            {
                QuickSlotItem[i] = CurItem;//클릭된 아이템 넣고 
                
                break;//탈출

            }
            if (QuickSlotItem[i].Name == CurItem.Name)//클릭한 아이템과 퀵슬롯에 등록된 아이템이 같다면 
            {
                QuickSlotItem[i] = null; // null 로 초기화후 
                
                break;//탈출
            }
            
        }
    }
    public void InvenQuickSlotClick(int slotNum)//인벤토리 퀵슬롯 클릭시 null 로 초기화 함수
    {
        QuickSlotItem[slotNum] = null;
        DrawQuickslot();//그려주는 함수 호출 

    }
    public void MainQuickSlotClick(int slotNum)//메인 퀵슬롯 클릭 함수 
    {
        if(QuickSlotItem[slotNum]!=null) // 퀵슬롯아이템리스트 i 가 null이 아니면 
        {
           switch( QuickSlotItem[slotNum].Type)//퀵슬롯 아이템이 hp 인지 mp 인지 구별후 
            {
                case "HP":
                    if(int.Parse(QuickSlotItem[slotNum].Number)>0)// 갯수가 0 보다 크다면 
                    {
                        int i =int.Parse(QuickSlotItem[slotNum].Number) - 1;//int i 선언후 갯수를 빼주고 
                        QuickSlotItem[slotNum].Number = i.ToString();// 퀵슬롯아이템리스트에 갯수를 다시 넣어주고  
                        Item MyitemlistHp =MyItemList.Find(x=>x.Name == QuickSlotItem[slotNum].Name);
                        MyitemlistHp.Number = QuickSlotItem[slotNum].Number;
                        CharacterManger.instance.stat.curHp += int.Parse(QuickSlotItem[slotNum].value);//캐릭터 cupHp에 value만큼 더해주고 
                        if(CharacterManger.instance.stat.curHp>=CharacterManger.instance.stat.maxHp)// 만약 curHp가 maxHp 보다 크거나 같으면 
                        {
                            CharacterManger.instance.stat.curHp = CharacterManger.instance.stat.maxHp;//curHp는 maxHp로 
                        }
                    }
                    break;
                case "MP":
                    if (int.Parse(QuickSlotItem[slotNum].Number) > 0)//위와같다
                    {
                        int i = int.Parse(QuickSlotItem[slotNum].Number) - 1;
                        QuickSlotItem[slotNum].Number = i.ToString();
                        Item MyitemlistMp = MyItemList.Find(x => x.Name == QuickSlotItem[slotNum].Name);
                        MyitemlistMp.Number = QuickSlotItem[slotNum].Number;
                        CharacterManger.instance.stat.curMp += int.Parse(QuickSlotItem[slotNum].value);
                        if (CharacterManger.instance.stat.curMp >= CharacterManger.instance.stat.maxMp)
                        {
                            CharacterManger.instance.stat.curMp = CharacterManger.instance.stat.maxMp;
                        }
                    }
                    break;

            }
        }
        
        DrawQuickslot();//갯수 업데이트 및 저장 
    }
    public void SlotClick(int slotNum)//슬롯클릭때
    {
        CurItem = CurItemList[slotNum]; // 현재 아이템은 슬롯넘버 
        Item UsingItem;
        if(CurItem.ItemType=="Potion")//슬롯클릭시 아이템 타입이 포션이면 
        {
            if(QuickSlotItem!=null)
            registQuickSlot();//등록함수 호출
        }
            switch (CurItem.Type) // 클릭한 아이템 타입구별을위해선언 
            {
                case "Armor": // 타입이 Armor 라면 
                     UsingItem = MyItemList.Find(x => x.Type == "Armor" && x.isUsing == true); // 내아이템중 타입이 아머이면서 사용중인것을 찾는다.
                    if(UsingItem != null && CurItem != UsingItem) // 만약 사용중인Armor 가 없고 클릭한 아이템 사용중인것과 같지않다면 
                    {
                    CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);//사용중인 armor value값 빼주기
                        UsingItem.isUsing = false; //사용중인 armor using false
                    CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);// 클릭한 armor value값 더해주기
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    }
                    else
                    {
                   
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    if (CurItem.isUsing == true)// 클릭했을때 true로 변경된다면
                    {
                        CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);// 클릭한 armor value값 더해주기
                    }
                    else//클릭했을때 false 라면
                    {
                        CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);//클릭한 armor value값 빼주기
                    }
                }
                    break;
                case "Helmet":
                UsingItem = MyItemList.Find(x => x.Type == "Helmet" && x.isUsing == true); // 내아이템중 타입이 아머인것을 찾는다.
                if (UsingItem != null && CurItem != UsingItem)
                {
                    CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);
                    UsingItem.isUsing = false;
                    CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {

                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    if(CurItem.isUsing==true)
                    {
                        CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);
                    }
                    else
                    {
                        CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);
                    }
                }
                break;
                case "Glove":
                UsingItem = MyItemList.Find(x => x.Type == "Glove" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);
                    UsingItem.isUsing = false;
                    CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    if (CurItem.isUsing == true)
                    {
                        CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);
                    }
                    else
                    {
                        CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);
                    }
                }
                break;
                case "Wepon":
                UsingItem = MyItemList.Find(x => x.Type == "Wepon" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    CharacterManger.instance.stat.Damage -= int.Parse(UsingItem.value);
                    UsingItem.isUsing = false;
                    CharacterManger.instance.stat.Damage += int.Parse(UsingItem.value);
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
    
                    if (CurItem.isUsing == true)
                    {
                        CharacterManger.instance.stat.Damage += int.Parse(CurItem.value);
                    }
                    else
                    {
                        CharacterManger.instance.stat.Damage -= int.Parse(UsingItem.value);
                    }
                }
                break;
                case "Shose":
                UsingItem = MyItemList.Find(x => x.Type == "Shose" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);
                    UsingItem.isUsing = false;
                    CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    if (CurItem.isUsing == true)
                    {
                        CharacterManger.instance.stat.maxHp += int.Parse(CurItem.value);
                    }
                    else
                    {
                        CharacterManger.instance.stat.maxHp -= int.Parse(UsingItem.value);
                    }
                }
                break;
                case "Ring":
                UsingItem = MyItemList.Find(x => x.Type == "Ring" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    CharacterManger.instance.stat.maxMp -= int.Parse(UsingItem.value);
                    UsingItem.isUsing = false;
                    CharacterManger.instance.stat.maxMp += int.Parse(CurItem.value);
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    if (CurItem.isUsing == true)
                    {
                        CharacterManger.instance.stat.maxMp += int.Parse(CurItem.value);
                    }
                    else
                    {
                        CharacterManger.instance.stat.maxMp -= int.Parse(UsingItem.value);
                    }
                }
                break;
            }
        Inventorystat();
        DrawQuickslot();
        Save();// json 데이터로 저장 사용중인것들 
    }

    public void TabClick(string tabname)// 탭클릭시 온클릭이벤트 
    {
       
        UsingItemList = MyItemList.FindAll(x => x.isUsing == true);
        Item UsingItem;
        curTab = tabname; // 현재탭은 탭네임 받기 
        if(tabname != "All") // 만약 탭타입이 all 이 아니면 

        {
          
            
            CurItemList = MyItemList.FindAll(x => x.ItemType == tabname); // 내 아이템 리스트에서 아이템 타입과 탭네임이 같은 것을 찾아라 
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Helmet");
            if (UsingItem != null)
                EqipItemImage[0].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[0].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Armor");
            if (UsingItem != null)
                EqipItemImage[1].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[1].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Glove");
            if (UsingItem != null)
                EqipItemImage[2].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[2].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Shose");
            if (UsingItem != null)
                EqipItemImage[3].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[3].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Wepon");
            if (UsingItem != null)
                EqipItemImage[4].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[4].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Ring");
            if (UsingItem != null)
                EqipItemImage[5].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[5].sprite = none;


            for (int i = 0; i < Slot.Length; i++)// 슬롯갯수만큼 돌려주고 
            {
                bool isExistSlot = i < CurItemList.Count;
                
                Slot[i].SetActive(isExistSlot); // 아이템 카테고리에 맞는 아이템 만큼 슬롯 생성 
                
                    Slot[i].GetComponentInChildren<Text>().text = isExistSlot ? CurItemList[i].Number : ""; // 텍스트 에 갯수 표현 
                if(isExistSlot) // 아이템이 존재한다면 
                {
                    ItemImage[i].sprite = ItemSprite[Alltem.FindIndex(x=> x.Name == CurItemList[i].Name)]; // 아이템 이미지 스프라이트 에다가 아이템 스프라이트를 넣는데 AllItem 에서 CurItem과 이름이 같은것을 찾고 인덱스에 해당되는 이미지 할당 
                }
             
                if (i < CurItemList.Count && CurItemList[i].isUsing == true) // 만약 현재 아이템이 사용중이라면 
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(true); // 장착중 표시 

                }
                else if(i < CurItemList.Count && CurItemList[i].isUsing ==false) // 아니라면 
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(false); // 장착중 꺼주기 이하동문 
                }

                

            }
        }
       
        else 
        {
           
            CurItemList = MyItemList;

            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Helmet");
            if (UsingItem != null)
                EqipItemImage[0].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[0].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Armor");
            if (UsingItem != null)
                EqipItemImage[1].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[1].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Glove");
            if (UsingItem != null)
                EqipItemImage[2].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[2].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Shose");
            if (UsingItem != null)
                EqipItemImage[3].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[3].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Wepon");
            if (UsingItem != null)
                EqipItemImage[4].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[4].sprite = none;
            UsingItem = MyItemList.Find(x => x.isUsing == true && x.Type == "Ring");
            if (UsingItem != null)
                EqipItemImage[5].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == UsingItem.Name)];
            else
                EqipItemImage[5].sprite = none;



            for (int i = 0; i < Slot.Length; i++)
            {
                bool isExistSlot = i < CurItemList.Count;

                Slot[i].SetActive(true);// 모든 아이템 카테고리기때문에 슬롯을 다그려준다 
                Slot[i].GetComponentInChildren<Text>().text = isExistSlot ? CurItemList[i].Number : "";
                if(isExistSlot) // 아이템이 존재한다면 
                {
                    ItemImage[i].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == CurItemList[i].Name)]; // 아이템 이미지 스프라이트는 
                }
                
                if (i < CurItemList.Count && CurItemList[i].isUsing == true)
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(true);

                }
                else if (i < CurItemList.Count && CurItemList[i].isUsing == false)
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(false);
                }
            }
          
        }

       
        switch(tabname) // 셀렉트 이미지 활성화 위해 
        {
            case "All":
                SelectImg[0].enabled = true;
                SelectImg[1].enabled = false;
                SelectImg[2].enabled = false; 
                SelectImg[3].enabled = false; 
                break;
            case "Eqipment":
                SelectImg[0].enabled = false;
                SelectImg[1].enabled = true;
                SelectImg[2].enabled = false;
                SelectImg[3].enabled = false;
                break;
            case "Accessory":
                SelectImg[0].enabled = false;
                SelectImg[1].enabled = false;
                SelectImg[2].enabled = true;
                SelectImg[3].enabled = false;
                break;
            case "Potion":
                SelectImg[0].enabled = false;
                SelectImg[1].enabled = false;
                SelectImg[2].enabled = false;
                SelectImg[3].enabled = true;

                break;

        }
    }
    public void GetItem(Item getitem) // 아이템 얻을 경우 얻는 아이템 받아오고 
    {
        if(getitem.ItemType != "Eqipment" && getitem.ItemType != "Accessory") // 아이템타입이 장비,액세서리가 아니라면 
        {
            Item curitem = MyItemList.Find(x=>x.Name==getitem.Name); // 현재아이템은 내 아이템 리스트중 getitem 과 이름이 같은걸 찾고 
            if(curitem != null) // null 이 아니면 같은걸 찾아진거고 
            {
                curitem.Number = (int.Parse(getitem.Number)+int.Parse(curitem.Number)).ToString(); // 현재아이템 갯수는 전달받은아이템정보 + 현재아이템 
            }
            else
            {
                MyItemList.Add(getitem);
                
            }
        }
        else // 장비인경우 마이아이템 리스트 추가해줘야하는 코드 추가 
        {
            MyItemList.Add(getitem);
        }
        Save();
    }
    public void RemoveItem(Item useItem)
    {
        if (useItem.ItemType != "Eqipment" && useItem.ItemType != "Accessory")//아이템타입 장비,액세서리 아니면 
        {
            Item curitem = MyItemList.Find(x=>x.Name == useItem.Name);  //현재아이템 내아이템 리스트중 useitem 과 같은거를찾고 
            if(curitem!=null)//아이템이 null 이 아니면 이미 같은종류 아이템이 있는거라서 
            {
                int curNumber = int.Parse(curitem.Number) - int.Parse(useItem.Number); // 마이아이템 갯수  - 쓴아이템갯수 
                if (curNumber <= 0) MyItemList.Remove(curitem);// 0보다 작거나 같다면 remove 해준다 
                else curitem.Number = curNumber.ToString(); // 0보다 크다면 남아있는 아이템갯수 표시
            }
        }
        else // 장비인경우 추가 
        {

        }
        Save();
    }
    public void PointerEnter(int slotNum)//포인터가 슬롯에 들어왓을때 
    {
        pointercoroutine =Delay(slotNum);// 위에 선언한 코루틴에 delay코루틴 넣어주고 
        if(MyItemList.Count > slotNum)
        {
            StartCoroutine(pointercoroutine);//스타트 코루틴 
            ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;//첫번째 텍스트 찾아서 curitelist 슬롯넘버랑 같은 이름을 가져오고 
            ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite; // 3번째에있는 자식 이미지 가져와서 슬롯에 2번째에 있는 이미지대입
            ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Explain;// 4번째에있는 자식 text 가져와서 explain 넣음
        }
        
    }


    IEnumerator Delay(int slotNum)// 딜레이 0.5 초 주기위해 
    {
        yield return new WaitForSeconds(0.3f);
        ExplainPanel.SetActive(true);// 설명판넬 true


    }
    public void PointerExit(int slotNum)//포인터가 나가면 
    {
        StopCoroutine(pointercoroutine);// 스탑코루틴 
        ExplainPanel.SetActive(false);//설명판넬 false

    }
    
    IEnumerator  Drop(Item Dropitem)
    {
       
        DropPanel.SetActive(true); // 드랍 패널 활성화
        DropPanel.transform.GetChild(1).GetComponent<Image>().sprite = ItemSprite[Alltem.FindIndex(x => x.Name == Dropitem.Name)]; // 드랍패널 자식 첫번째 이미지를 인덱스화 시켜서 itemsprite에서 가져오기
        DropPanel.GetComponentInChildren<Text>().text = Dropitem.Name;// text를 이름으로 바꿔주기
        yield return new WaitForSeconds(1.9f);//1초뒤
        DropPanel.SetActive(false);//드랍 패널 비활성화
       

    }

    public void ItemDrop()
    {

        Choose(new float[3] { 10f, 10f, 80f }); // 퍼센트 조정 10 10 80 
        float Choose(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    switch (i)
                    {
                        case 0: // 10f 안에 랜덤포인트가 생성된경우 
                            DropTabl = Alltem.FindAll(x => x.ItemType == "Eqipment");
                            int n = Random.Range(0, 4); // 장비아이템 종류 5가지 
                            StartCoroutine(Drop(DropTabl[n]));
                            Debug.Log(DropTabl[n].Name);
                            GetItem(DropTabl[n]);
                            break;
                        case 1: // 11f~20f안에 랜덤포인트가 생성된경우
                            DropTabl = Alltem.FindAll(x => x.ItemType == "Accessory");
                            int m = 0; // 악세서리 1가지
                            StartCoroutine(Drop(DropTabl[m]));
                            Debug.Log(DropTabl[m].Name);
                            GetItem(DropTabl[m]);
                            break;
                        case 2: // 21~100f안에 랜덤포인트가 생성된경우 
                            DropTabl = Alltem.FindAll(x => x.ItemType == "Potion");
                            int p = Random.Range(0, 2); // 포션 2가지 
                            StartCoroutine(Drop(DropTabl[p]));
                            Debug.Log(DropTabl[p].Name);
                            GetItem(DropTabl[p]);
                            break;
                    }
                    return i;
                }
                else
                {
                    randomPoint -= probs[i]; // 랜덤포인트가 20이면 probs[0]=10f 20-10=10; 그래서 case 1:로 들어가게 된다.
                }
            }
            return probs.Length - 1;
        }
    }
    void Save()
    {
        string jdatamyitemlist = JsonConvert.SerializeObject(MyItemList); // string에 내 아이템리스트를 json으로 바꿔줘서 넣어주고 
        string jdatamyquickslot = JsonConvert.SerializeObject(QuickSlotItem);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdatamyitemlist);
        File.WriteAllText(Application.dataPath + "/Resources/MyQuickSlot.txt", jdatamyquickslot);// 이파일을 Resources 폴더에 MyItemText jdata로 저장해준다 
        TabClick(curTab); // 탭클릭호출 
    }

    void Load()
    {
        string jdatamyitemlist = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        string jdatamyquickslot = File.ReadAllText(Application.dataPath + "/Resources/MyQuickSlot.txt");// 내아이템 리스트 를 읽어오고 
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdatamyitemlist); // Convert하여 Myitemlist에 넣어준다 
        QuickSlotItem =JsonConvert.DeserializeObject<List<Item>>(jdatamyquickslot);
        TabClick(curTab); // 처음 탭클릭 호출
    }
}
