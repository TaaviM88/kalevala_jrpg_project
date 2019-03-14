using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCombatData : MonoBehaviour
{
    public static PlayersCombatData Instance { get; set; }
    // Start is called before the first frame update
    //Lemminkäisen statsit
    private int lCurrentHealt;
    private int lCurrentManap;

    //Ilmarisen statsit
    private int iCurrentHealt;
    private int iCurrentManap;

    //Väinämöisen statsit
    private int vCurrentHealt;
    private int vCurrentManap;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateTeamMemberStats()
    {
        //Haetaan kaikki pelaaja hahmot
        for (int i = 0; i < BattleController.Instance.characters[0].Count; i++)
        {
            Character character = BattleController.Instance.characters[0][i];
            if(character.GetComponent<PartyMember>().partyMemberName == PartyMember.PartyMemberName.Lemminkainen)
            {
                lCurrentHealt = character.health;
                lCurrentManap = character.manaPoints;
                Debug.Log("Päivitetään Lemminkäisen helat");
            }
            else if(character.GetComponent<PartyMember>().partyMemberName == PartyMember.PartyMemberName.Ilmarinen)
            {
                iCurrentHealt = character.health;
                iCurrentManap = character.manaPoints;
                Debug.Log("Päivitetään Ilmarin helat");
            }
            else if (character.GetComponent<PartyMember>().partyMemberName == PartyMember.PartyMemberName.Vainamoinen)
            {
                vCurrentHealt = character.health;
                vCurrentManap = character.manaPoints;
                Debug.Log("Päivitetään Väinämöisen helat");
            }

        }
    }

    public int GetCurrentHP(PartyMember.PartyMemberName name)
    {
        //Käytetään -1. Keksin jonkun järkevän selityksen tähän myöhemmin.
        int tmphealt = -1;
        if(name == PartyMember.PartyMemberName.Lemminkainen)
        {
            tmphealt = lCurrentHealt;            
        }

        else if(name == PartyMember.PartyMemberName.Ilmarinen)
        {
            tmphealt = iCurrentHealt;
        }

        else if(name == PartyMember.PartyMemberName.Vainamoinen)
        {
            tmphealt = vCurrentHealt;
        }

        return tmphealt;
        
    }

    public int GetCurrentMP(PartyMember.PartyMemberName name)
    {
        //Käytetään -1. Keksin jonkun järkevän selityksen tähän myöhemmin.
        int tmpMP = -1;
        if (name == PartyMember.PartyMemberName.Lemminkainen)
        {
            tmpMP = lCurrentManap;

        }
        else if (name == PartyMember.PartyMemberName.Ilmarinen)
        {
            tmpMP = iCurrentManap;
        }
        else if (name == PartyMember.PartyMemberName.Vainamoinen)
        {
            tmpMP = vCurrentManap;
        }

        return tmpMP;
    }


}
