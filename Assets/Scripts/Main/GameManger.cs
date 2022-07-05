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
    public GameObject[] Slot;
  
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


    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        switch (CurItem.Type)
        {
            case "Helmet":
                
                if (UsingItem.Type == "Helmet")
                {
                   
                    UsingItem.isUsing = false;
                }
               
                CurItem.isUsing = true;

                break;
            case "Armor":
                if (UsingItem.Type == "Armor") UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
            case "Wepon":
                if (UsingItem.Type == "Wepon") UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
            case "Glove":
                if (UsingItem.Type == "Glove") UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
            case "Shoose":
                if (UsingItem.Type == "Shoose") UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
            case "Neckglass":
                if (UsingItem != null) UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
            case "Ring":
                if (UsingItem != null) UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
            case "Earring":
                if (UsingItem != null) UsingItem.isUsing = false;
                CurItem.isUsing = true;
                break;
                default:

                break;

        }

    
                

        Save();
    }
    public void TabClick(string tabname)
    {
        curTab = tabname;
        if(tabname != "All") // 만약 탭타입이 all 이 아니면 
        {
            CurItemList = MyItemList.FindAll(x => x.ItemType == tabname); // 내 아이템 리스트에서 아이템 타입과 탭네임이 같은 것을 찾아라 
            for (int i = 0; i < Slot.Length; i++)
            {
                Slot[i].SetActive(i < CurItemList.Count);
                Slot[i].GetComponentInChildren<Text>().text = i < CurItemList.Count ? CurItemList[i].Number :"";

                if (i < CurItemList.Count && CurItemList[i].isUsing == true)
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(true);

                }
                else if(i < CurItemList.Count && CurItemList[i].isUsing ==false)
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(false);
                }



            }
        }
        else
        {

            CurItemList = MyItemList; // all 이면 모두 넣어라 
            for (int i = 0; i < Slot.Length; i++)
            {
                Slot[i].SetActive(true);
                Slot[i].GetComponentInChildren<Text>().text = i < CurItemList.Count ? CurItemList[i].Number : "";
                if (i<CurItemList.Count && CurItemList[i].isUsing == true)
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(true);

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
        string jdata = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
        TabClick(curTab);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata); // 실험을 위해 모든 아이템 데이터 넣어줌 
        TabClick(curTab); // 처음 탭클릭 호출
    }
}
