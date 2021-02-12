using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [System.Serializable]
    public struct Skin
    {
        public int price, status;  //status: 0-no access, 1-access to buy, 2-bought, 3-equiped
        public Button buyButton;
        public GameObject pricePanel, equipedImage;
        public Text priceText;
    }

    public List<Skin> skins;

    [SerializeField] Renderer robotSkin;

    [SerializeField] GameObject robotPreview, butRotateL, butRotateR;

    [SerializeField] Texture[] textures;

    [SerializeField] Text moneyText, mainButtonText;

    [SerializeField] Button mainButton;

    int money; //from another script Paraments.cs

    void Start()
    {
        robotPreview.transform.rotation = Quaternion.Euler(-45,-180,0);
        money = 1000;
        moneyText.text = "" + money;
        FirstInit();
    }

    void FirstInit()
    {
        Skin skin = new Skin();
        for (int i = 0; i < skins.Capacity; i++)
        {
            skin = skins[i];
            if (i == 0)
            {
                skin.status = 3;
            }
            else
            {
                if (money >= skin.price)
                {
                    skin.status = 1;
                }
                else
                {
                    skin.status = 0;
                }

            }
            skins[i] = skin;
            skins[i].priceText.text=""+skins[i].price;
            RefreshStatus(i);
            if (skins[i].status == 3)
            {
                TapOnButtonInit(i);
            }
        }
    }

    void RefreshStatus(int i)
    {
        switch (skins[i].status)
        {
            case 0:
                {
                    skins[i].buyButton.interactable = false;
                    skins[i].equipedImage.SetActive(false);
                }
                break;
            case 1:
                {
                    skins[i].buyButton.interactable = true;
                    skins[i].equipedImage.SetActive(false);
                }
                break;
            case 2:
                {
                    skins[i].buyButton.interactable = true;
                    skins[i].equipedImage.SetActive(false);
                    skins[i].pricePanel.SetActive(false);
                }
                break;
            case 3:
                {
                    skins[i].buyButton.interactable = true;
                    skins[i].equipedImage.SetActive(true);
                    skins[i].pricePanel.SetActive(false);
                }
                break;
        }
    }

    void BuyInit(int id)
    {
        TestTranferInit(skins[id].price);
        EquipInit(id);
    }

    void EquipInit(int id)
    {
        Skin skin = new Skin();

        skin = skins[id];
        skin.status = 3;
        skins[id] = skin;
        RefreshStatus(id);

        for (int i = 0; i < skins.Capacity; i++)
        {
            skin = skins[i];
            if (id != i && skin.status == 3)
            {
                skin.status = 2;
                skins[i] = skin;
                RefreshStatus(i);
            }
        }
        mainButton.interactable = false;
        mainButtonText.text = "Equiped!";
    }

    // from another script TransferMoney()
    void TestTranferInit(int amount)
    {
        money = money + amount;
        moneyText.text = "" + money;
    }

    int lastID;
    public void TapOnResultButton()
    {
        switch (skins[lastID].status)
        {
            case 1:
                {
                    BuyInit(lastID);
                }
                break;
            case 2:
                {
                    EquipInit(lastID);
                }
                break;
        }
    }

    public void TapOnButtonInit(int skinID)
    {
        switch (skins[skinID].status) {
            case 1:
                {
                    mainButton.interactable = true;
                    mainButtonText.text = "Buy: "+skins[skinID].price;
                }
                break;
            case 2:
                {
                    mainButton.interactable = true;
                    mainButtonText.text = "Equip";
                }
                break;
            case 3:
                {
                    mainButton.interactable = false;
                    mainButtonText.text = "Equiped!";
                }
                break;
        }
        lastID = skinID;
        robotSkin.material.mainTexture = textures[skinID];
    }
}