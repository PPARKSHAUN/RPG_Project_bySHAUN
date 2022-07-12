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
    private static GameManger instance;
    public TextAsset ItemDatabase;// 아이템 데이터베이스를 연결해주기위해 선언 
    public List<Item> Alltem, MyItemList, CurItemList,UsingItemList; //아이템을 리스트 해주기위해
    public string curTab = "All";//처음 탭 타입 
    public Image[] SelectImg; // 아이템 셀렉 이미지 켜주기위해 배열로받고
    public GameObject[] Slot, Eqipslot,off,on;//슬롯을 넣기위해 
    public Image[] ItemImage,EqipItemImage; // 슬롯 아이템 이미지 연결 
    public Sprite[] ItemSprite; // 아이템 데이터 베이스순서에 맞게 스프라이트 연결
    public Sprite none;
    public Transform player;
    public GameObject ExplainPanel;//팝업창 패널 
    IEnumerator pointercoroutine; //코루틴 스타트 스탑 해주기위해 선언 
    

    // Start is called before the first frame update
    private void Update()
    {
        ExplainPanel.transform.position = Input.mousePosition ;

    }
    void Start()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n'); // 아이템 데이터 베이스가 저장되면 마지막줄은 엔터라서 엔터빼고 받기위해서  
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');// 라인 0 번을 탭으로 나누고 넣어준다 row 에 
            Alltem.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));// 아이템타입,타입,이름,설명,갯수,장착여부 를 넘겨준다 . 
        }

        Load();
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
                //여기에는 아닌경우 myitemlist에 add 해줘야하는코드 작성
            }
        }
        else // 장비인경우 마이아이템 리스트 추가해줘야하는 코드 추가 
        {

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
    }
    public void PointerEnter(int slotNum)//포인터가 슬롯에 들어왓을때 
    {
        pointercoroutine =Delay(slotNum);// 위에 선언한 코루틴에 delay코루틴 넣어주고 
        StartCoroutine(pointercoroutine);//스타트 코루틴 
        ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;//첫번째 텍스트 찾아서 curitelist 슬롯넘버랑 같은 이름을 가져오고 
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite; // 3번째에있는 자식 이미지 가져와서 슬롯에 2번째에 있는 이미지대입
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Explain;// 4번째에있는 자식 text 가져와서 explain 넣음
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
