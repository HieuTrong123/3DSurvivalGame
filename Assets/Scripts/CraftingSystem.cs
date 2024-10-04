using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BluePrint : MonoBehaviour
// {
//   public string itemName;
//   public string Req1;
//   public string Req2;
//   public int Req1Amount;
//   public int Req2Amount;
//   public int numOfRequirements;
//   public BluePrint(string name, int reqNum,string R1, int R1num, string R2, int R2num){
//     itemName=name;
//     numOfRequirements=reqNum;
//     Req1=R1;
//     Req2=R2;
//     Req1Amount=R1num;
//     Req2Amount=R2num;
//   }
// }



public class CraftingSystem : MonoBehaviour
{
    public class BluePrint
    {
        public string itemName;
        public string Req1;
        public string Req2;
        public int Req1Amount;
        public int Req2Amount;
        public int numOfRequirements;
        public BluePrint(string name, int reqNum,string R1, int R1num, string R2, int R2num){
            itemName=name;
            numOfRequirements=reqNum;
            Req1=R1;
            Req2=R2;
            Req1Amount=R1num;
            Req2Amount=R2num;
        }
    }

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    Button toolsBTN;

    Button craftAxeBTN;

    Text AxeReq1,AxeReq2;

    public bool isOpen;

    public BluePrint AxeBLP = new BluePrint("Axe",2,"Stone",3,"Stick",3);

    

    public static CraftingSystem Instance {get;set;}

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        toolsBTN=craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate{OpenToolsCategory();});

        AxeReq1=toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2=toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN=toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate{CraftAnyItem(AxeBLP);});
    }

    void CraftAnyItem(BluePrint blueprintToCraft){
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        if(blueprintToCraft.numOfRequirements==1){
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1,blueprintToCraft.Req1Amount);
        }
        else if(blueprintToCraft.numOfRequirements==2){
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1,blueprintToCraft.Req1Amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2,blueprintToCraft.Req2Amount);
        }
       
        
        StartCoroutine(calculate());
        // RefreshNeededItems();
    }

    public IEnumerator calculate(){
        yield return new WaitForSeconds(1f);
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }

    void OpenToolsCategory(){
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    public void RefreshNeededItems(){
            int stone_count=0;
            int stick_count=0;
            inventoryItemList=InventorySystem.Instance.itemList;
            foreach(string itemName in inventoryItemList){
                switch(itemName){
                    case "Stone":
                        stone_count+=1;
                        break;
                    case "Stick":
                        stick_count+=1;
                        break;
                }
            }
            AxeReq1.text="3 Stone ["+ stone_count +"]";
            AxeReq2.text="3 Stick ["+ stick_count +"]";
            if(stone_count >= 3 && stick_count >= 3){
                craftAxeBTN.gameObject.SetActive(true);

            }
            else{
                craftAxeBTN.gameObject.SetActive(false);
            }

        }

    // Update is called once per frame
    void Update()
    {
           if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
 
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible=true;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled=false;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);

            if(!InventorySystem.Instance.isOpen){
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible=false;
                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled=true;
            }
            
            isOpen = false;
        }
    }
}
