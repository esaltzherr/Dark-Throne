using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSos : ScriptableObject
{   
    public string itemName;
    public StatToChange stateToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem(){
        if(stateToChange == StatToChange.health){
            PlayerHealth2 playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth2>();
            if(playerHealth.getCurrentHealth() == 100)
            {
                return false;
            }
            else{
                playerHealth.Heal(amountToChangeStat);
                return true;
            }
        
        }
        return false;

    }

    //GameObject.Find("Player").GetComponent<PlayerHealth2>().Heal(5);
    //ChangeHealth(amountToChangeStat);
    //     //     //player.GetComponent<PlayerHealth2>().Heal(10);
    //     // }
    //     // if(stateToChange == StatToChange.mana){
    //     //     GameObject.Find("ManaManager").GetComponent<PlayerMana>().ChangeHealth(amountToChangeStat);

   public enum StatToChange{
    none,
    health,
    //mana,
    //stamina
   };

   public enum AttributeToChange{
    none,
    //strength,
    //defense,
    //intelligence,
    //agility
   };
}
