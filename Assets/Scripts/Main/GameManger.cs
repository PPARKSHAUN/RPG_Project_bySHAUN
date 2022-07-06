using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
[System.Serializable]
public class Item
{
    public Item(string _ItemType, string _Type, string _Name, string _Explain, string _Number, bool _isUsing) // ������ ���� 
    {
        ItemType = _ItemType; // �ؿ� �����ٰ� ���� ���ش� 
        Type = _Type; //���ϵ��� 
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
    public TextAsset ItemDatabase;// ������ �����ͺ��̽��� �������ֱ����� ���� 
    public List<Item> Alltem, MyItemList, CurItemList; //�������� ����Ʈ ���ֱ�����
    public string curTab = "All";//ó�� �� Ÿ�� 
    public Image[] SelectImg; // ������ ���� �̹��� ���ֱ����� �迭�ιް�
    public GameObject[] Slot;//������ �ֱ����� 
    public Image[] ItemImage; // ���� ������ �̹��� ���� 
    public Sprite[] ItemSprite; // ������ ������ ���̽������� �°� ��������Ʈ ����
    // Start is called before the first frame update
    void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n'); // ������ ������ ���̽��� ����Ǹ� ���������� ���Ͷ� ���ͻ��� �ޱ����ؼ�  
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');// ���� 0 ���� ������ ������ �־��ش� row �� 
            Alltem.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));// ������Ÿ��,Ÿ��,�̸�,����,����,�������� �� �Ѱ��ش� . 
        }

        Load();
    }


    public void SlotClick(int slotNum)//����Ŭ����
    {
        Item CurItem = CurItemList[slotNum]; // ���� �������� ���Գѹ� 
        Item UsingItem;




            switch (CurItem.Type) // Ŭ���� ������ Ÿ�Ա��������ؼ��� 
            {
                case "Armor": // Ÿ���� Armor ��� 
                     UsingItem = MyItemList.Find(x => x.Type == "Armor" && x.isUsing == true); // ���������� Ÿ���� �Ƹ��̸鼭 ������ΰ��� ã�´�.
                    if(UsingItem != null && CurItem != UsingItem) // ���� �������Armor �� ���� Ŭ���� ������ ������ΰͰ� �����ʴٸ� 
                    {
                        UsingItem.isUsing = false; // ������� Armor false 
                        CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                    }
                    else
                    {
                        CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                    }
                    break;
                case "Helmet":
                UsingItem = MyItemList.Find(x => x.Type == "Helmet" && x.isUsing == true); // ���������� Ÿ���� �Ƹ��ΰ��� ã�´�.
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                break;
                case "Glove":
                UsingItem = MyItemList.Find(x => x.Type == "Glove" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                break;
                case "Wepon":
                UsingItem = MyItemList.Find(x => x.Type == "Wepon" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                break;
                case "Shose":
                UsingItem = MyItemList.Find(x => x.Type == "Shose" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                break;
                case "Ring":
                UsingItem = MyItemList.Find(x => x.Type == "Ring" && x.isUsing == true);
                if (UsingItem != null && CurItem != UsingItem)
                {
                    UsingItem.isUsing = false;
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                else
                {
                    CurItem.isUsing = !CurItem.isUsing; // ���� Ŭ���� ���� false �� true �� true �� false �� ����. ���ϵ��� 
                }
                break;
            }
        
       
        Save();// json �����ͷ� ���� ������ΰ͵� 
    }
    public void TabClick(string tabname)// ��Ŭ���� ��Ŭ���̺�Ʈ 
    {
        curTab = tabname; // �������� �ǳ��� �ޱ� 
        if(tabname != "All") // ���� ��Ÿ���� all �� �ƴϸ� 
        {
            CurItemList = MyItemList.FindAll(x => x.ItemType == tabname); // �� ������ ����Ʈ���� ������ Ÿ�԰� �ǳ����� ���� ���� ã�ƶ� 
            for (int i = 0; i < Slot.Length; i++)// ���԰�����ŭ �����ְ� 
            {
                bool isExistSlot = i < CurItemList.Count;
                
                Slot[i].SetActive(isExistSlot); // ������ ī�װ����� �´� ������ ��ŭ ���� ���� 
                
                    Slot[i].GetComponentInChildren<Text>().text = isExistSlot ? CurItemList[i].Number : ""; // �ؽ�Ʈ �� ���� ǥ�� 
                if(isExistSlot) // �������� �����Ѵٸ� 
                {
                    ItemImage[i].sprite = ItemSprite[Alltem.FindIndex(x=> x.Name == CurItemList[i].Name)]; // ������ �̹��� ��������Ʈ ���ٰ� ������ ��������Ʈ�� �ִµ� AllItem ���� CurItem�� �̸��� �������� ã�� �ε����� �ش�Ǵ� �̹��� �Ҵ� 
                }
             
                if (i < CurItemList.Count && CurItemList[i].isUsing == true) // ���� ���� �������� ������̶�� 
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(true); // ������ ǥ�� 

                }
                else if(i < CurItemList.Count && CurItemList[i].isUsing ==false) // �ƴ϶�� 
                {
                    Slot[i].transform.Find("Eqip").gameObject.SetActive(false); // ������ ���ֱ� ���ϵ��� 
                }

                

            }
        }
        else
        {

            CurItemList = MyItemList; // all �̸� ��� �־�� 
            for (int i = 0; i < Slot.Length; i++)
            {
                bool isExistSlot = i < CurItemList.Count;

                Slot[i].SetActive(true);// ��� ������ ī�װ����⶧���� ������ �ٱ׷��ش� 
                Slot[i].GetComponentInChildren<Text>().text = isExistSlot ? CurItemList[i].Number : "";
                if(isExistSlot) // �������� �����Ѵٸ� 
                {
                    ItemImage[i].sprite = ItemSprite[Alltem.FindIndex(x => x.Name == CurItemList[i].Name)]; // ������ �̹��� ��������Ʈ�� 
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

       
        switch(tabname) // ����Ʈ �̹��� Ȱ��ȭ ���� 
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
        string jdata = JsonConvert.SerializeObject(MyItemList); // string�� �� �����۸���Ʈ�� json���� �ٲ��༭ �־��ְ� 
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata); // �������� Resources ������ MyItemText jdata�� �������ش� 
        TabClick(curTab); // ��Ŭ��ȣ�� 
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt"); // �������� ����Ʈ �� �о���� 
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata); // Convert�Ͽ� Myitemlist�� �־��ش� 
        TabClick(curTab); // ó�� ��Ŭ�� ȣ��
    }
}