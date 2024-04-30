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

    public void UseItem(){
        if(stateToChange == StatToChange.health){
            GameObject.Find("Player").GetComponent<PlayerHealth2>().Heal(10);
    //ChangeHealth(amountToChangeStat);
    //     //     //player.GetComponent<PlayerHealth2>().Heal(10);
    //     // }
    //     // if(stateToChange == StatToChange.mana){
    //     //     GameObject.Find("ManaManager").GetComponent<PlayerMana>().ChangeHealth(amountToChangeStat);
     }

    }

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
