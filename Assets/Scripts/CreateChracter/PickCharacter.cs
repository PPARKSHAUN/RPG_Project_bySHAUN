using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickCharacter : MonoBehaviour
{
    public static PickCharacter instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    public Character character;
    Animator _myAnim;
    public PickCharacter[] chars;
    public List<GameObject> CamPos=new List<GameObject>();
    public List<GameObject> CharacterPanel= new List<GameObject> ();
    public Transform MyCamPos;
    public Transform StartCampos;
    public GameObject ExitButton;
    public GameObject CreateNickNameButton;
    private Transform chartr;
    public bool CharacterClick = true;
   
    private void Start()
    {
        _myAnim = GetComponent<Animator>();
       


    }
    
    private void OnMouseUpAsButton()
    {
            DataManger.instance.curCharcter = character;
            OnSelect(); 
    }
    public void NotSelect()
    {
        CharacterClick = false;
        StartCoroutine(BackBtnClick());
        _myAnim.SetTrigger("Not");
        _myAnim.SetBool("IsPickState", false);

    }

   
    public void OnSelect()
    {
        
        if (_myAnim.GetBool("IsPickState") == false)
        {
            
            _myAnim.SetTrigger("Pick");
            _myAnim.SetBool("IsPickState", true);
          StartCoroutine( Click(DataManger.instance.curCharcter));
        }
           
        
    }
    IEnumerator Click(Character character)
    {
        
        switch (character)
        {
            case Character.Archer:
                chartr = CamPos[0].transform;
                break;
            case Character.Fighter:
                chartr = CamPos[1].transform;
                break;
            
            case Character.Paladin:
                chartr = CamPos[2].transform;
                break;
            case Character.Warrior:
                chartr = CamPos[3].transform;
                break;
        }
        while(Vector3.Distance(MyCamPos.position,chartr.position)>0.1f)
        {
            
            MyCamPos.position = Vector3.Lerp(MyCamPos.position,chartr.position,Time.deltaTime*2.3f);
            MyCamPos.rotation = Quaternion.Lerp(MyCamPos.rotation, chartr.rotation, Time.deltaTime * 2.3f);
            yield return null;
        }
        
        ExitButton.SetActive(true);
        CreateNickNameButton.SetActive(true);
        openpanel(DataManger.instance.curCharcter);
    }
   
   
     IEnumerator BackBtnClick()
    {

        
   
        CreateNickNameButton.SetActive(false);
        ExitButton.SetActive(false);
        closepanel();
       
        while (Vector3.Distance(MyCamPos.localPosition, StartCampos.localPosition) >0.1f)
        {
            
            MyCamPos.position = Vector3.Lerp(MyCamPos.localPosition, StartCampos.localPosition, Time.deltaTime * 2.3f);
            MyCamPos.rotation = Quaternion.Lerp(MyCamPos.rotation, StartCampos.rotation, Time.deltaTime* 2.3f);

            yield return null;
        }


        



    }
    public void openpanel(Character character)
    {
        switch (character)
        {
            case Character.Archer:
                CharacterPanel[0].SetActive(true);
                break;
            case Character.Fighter:
                CharacterPanel[1].SetActive(true);
                break;

            case Character.Paladin:
                CharacterPanel[2].SetActive(true);
                break;
            case Character.Warrior:
                CharacterPanel[3].SetActive(true);
                break;


        }
    }
    public void closepanel()
    {
        for(int i = 0; i<CharacterPanel.Count;i++)
        {
            CharacterPanel[i].SetActive(false);
        }
    }



}

