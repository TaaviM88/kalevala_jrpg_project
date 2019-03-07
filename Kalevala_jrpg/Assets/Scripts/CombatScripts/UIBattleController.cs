using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIBattleController : MonoBehaviour
{
    [SerializeField]
    private GameObject spellPanel;
    [SerializeField]
    private Button[] actionButtons;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TMP_Text[] characterInfo;

    private UIBattleController ui;

    // Start is called before the first frame update
    void Start()
    {
        spellPanel.SetActive(false);
        ui = GameObject.Find("CharacterPanel").GetComponent<UIBattleController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Character"))
            {
                BattleController.Instance.SelectCharacter(hitInfo.collider.GetComponent<Character>());
            }
        }
    }

    public void ToggleSpellPanel(bool state)
    {
        spellPanel.SetActive(state);
        if (state == true)
        {
            BuildSpellList(BattleController.Instance.GetCurrentCharacter().spells);
        }
    }

    public void ToggleActionState(bool state)
    {
        ToggleSpellPanel(state);
        foreach (Button button in actionButtons)
        {
            button.interactable = state;
        }
    }
    public void BuildSpellList(List<Spells> spells)
    {
        if(spellPanel.transform.childCount > 0)
        {
            foreach(Button button in spellPanel.transform.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject); 
            }
        }

        foreach(Spells spell in spells)
        {
            Button spellButton = Instantiate<Button>(button, spellPanel.transform);
            spellButton.GetComponentInChildren<Text>().text = spell.spellName;
            spellButton.onClick.AddListener(() => SelectSpell(spell));
        }
    }

    public void SelectSpell(Spells spell)
    {
        BattleController.Instance.playerSelectedSpell = spell;
        BattleController.Instance.playerIsAttacking = false;
    }

   public void SelectAttack()
    {
        BattleController.Instance.playerSelectedSpell = null;
        BattleController.Instance.playerIsAttacking = true;

        //Tää BattleControlleriin
        ui.ToggleSpellPanel(false);
    }

    public void UpdateCharacterUI()
    {
        for (int i = 0; i < BattleController.Instance.characters[0].Count; i++)
        {
          Character character =  BattleController.Instance.characters[0][i];
            character.name = character.name.Replace("(Clone)", "");
            characterInfo[i].text = string.Format($"{character.name} HP: {character.health}/{character.maxHealth}, MP:{character.manaPoints}");
        }
    }

    public void Defend()
    {
        BattleController.Instance.GetCurrentCharacter().Defend();
    }
}
