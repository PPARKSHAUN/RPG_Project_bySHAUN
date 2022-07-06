using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
[System.Serializable]
public class Item
{
    public Item(string _ItemType, string _Type, string _Name, string _Explain, string _Number, bool _isUsing) // 생성자 생성 
    {
        ItemType = _ItemType; // 밑에 값에다가 대입 해준다 
        Type = _Type; //이하동문 
        Name = _Name;
        Explain = _Explain;
        Number = _Number;
        isUsing = _isUsing;
    }
    public string ItemType, Type, Name, Explain, Number;
    public bool isUsing;
}
public class GameManger : MonoBehaviour
{
    public TextAsset ItemDatabase;// 아이템 데이터베이스를 연결해주기위해 선언 
    public List<Item> Alltem, MyItemList, CurItemList; //아이템을 리스트 해주기위해
    public string curTab = "All";//처음 탭 타입 
    public Image[] SelectImg; // 아이템 셀렉 이미지 켜주기위해 배열로받고
    public GameObject[] Slot;//슬롯을 넣기위해 
    public Image[] ItemImage; // 슬롯 아이템 이미지 연결 
    public Sprite[] ItemSprite; // 아이템 데이터 베이스순서에 맞게 스프라이트 연결
    // Start is called before the first frame update
    void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n'); // 아이템 데이터 베이스가 저장되면 마지막줄은 엔터라서 엔터빼고 받기위해서  
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');// 라인 0 번을 탭으로 나누고 넣어준다 row 에 
            Alltem.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));// 아이템타입,타입,이름,설명,갯수,장착여부 를 넘겨준다 . 
        }

        Load();
    }


    public void SlotClick(int slotNum)//슬롯클릭때
    {
        Item CurItem = CurItemList[slotNum]; // 현재 아이템은 슬롯넘버 
        Item UsingItem;




            switch (CurItem.Type) // 클릭한 아이템 타입구별을위해선언 
            {
                case "Armor": // 타입이 Armor 라면 
                     UsingItem = MyItemList.Find(x => x.Type == "Armor" && x.isUsing == true); // 내아이템중 타입이 아머이면서 사용중인것을 찾는다.
                    if(UsingItem != null && CurItem != UsingItem) // 만약 사용중인Armor 가 없고 클릭한 아이템 사용중인것과 같지않다면 
                    {
                        UsingItem.isUsing = false; // 사용중인 Armor false 
                        CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    }
                    else
                    {
                        CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                    }
                    break;
                case "Helmet":
                UsingItem = MyItemList.Find(x => x.Type == "Helmet" && x.isUsing == true); // 내아이템중 타입이 아머인것을 찾는다.
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                break;
                case "Glove":
                UsingItem = MyItemList.Find(x => x.Type == "Glove" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                break;
                case "Wepon":
                UsingItem = MyItemList.Find(x => x.Type == "Wepon" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                break;
                case "Shose":
                UsingItem = MyItemList.Find(x => x.Type == "Shose" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                break;
                case "Ring":
                UsingItem = MyItemList.Find(x => x.Type == "Ring" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // 현재 클릭한 슬롯 false 면 true 로 true 면 false 로 변경. 이하동문 
                }
                break;
            }
        
       
        Save();// json 데이터로 저장 사용중인것들 
    }
    public void TabClick(string tabname)// 탭클릭시 온클릭이벤트 
    {
        curTab = tabname; // 현재탭은 탭네임 받기 
        if(tabname != "All") // 만약 탭타입이 all 이 아니면 
        {
            CurItemList = MyItemList.FindAll(x => x.ItemType == tabname); // 내 아이템 리스트에서 아이템 타입과 탭네임이 같은 것을 찾아라 
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

            CurItemList = MyItemList; // all 이면 모두 넣어라 
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

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList); // string에 내 아이템리스트를 json으로 바꿔줘서 넣어주고 
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata); // 이파일을 Resources 폴더에 MyItemText jdata로 저장해준다 
        TabClick(curTab); // 탭클릭호출 
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt"); // 내아이템 리스트 를 읽어오고 
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata); // Convert하여 Myitemlist에 넣어준다 
        TabClick(curTab); // 처음 탭클릭 호출
    }
}
