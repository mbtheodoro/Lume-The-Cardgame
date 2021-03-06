﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region REFERENCES
    public PlayerHandController playerHandController;
    #endregion

    #region ATTRIBUTES
    public PlayerInfo player;
    public Nation nation;
    private int stamina;
    private int mana;

    public DiscardPile attackDiscardPile;
    public DiscardPile locationDiscardPile;

    public Deck attackDeck;
    public Deck locationDeck;
    
    public List<UnitCard> units;
    public List<AttackCard> attacksOnHand;
    //public List<EquipCard> items;
    #endregion
    
    #region PROPERTIES
    public int Stamina
    {
        get { return stamina; }
        set
        {
            stamina = Mathf.Min(value, Defines.maxStamina);
            playerHandController.stamina = stamina;
        }
    }

    public int Mana
    {
        get { return mana; }
        set
        {
            mana = Mathf.Min(value, Defines.maxMana);
            playerHandController.mana = mana;
        }
    }
    #endregion
   
    #region METHODS
    public void EnableCardsOnHand()
    {
        bool atLeastOneCardCanBePlayed = false;

        for (int i = 0; i < attacksOnHand.Count; i++)
        {
            if(attacksOnHand[i].type == AttackType.PHYSICAL && attacksOnHand[i].currentCost <= stamina)
            {
                attacksOnHand[i].playable = true;
                atLeastOneCardCanBePlayed = true;
            }
            else if(attacksOnHand[i].type == AttackType.MAGICAL && attacksOnHand[i].currentCost <= mana)
            {
                attacksOnHand[i].playable = true;
                atLeastOneCardCanBePlayed = true;
            }
        }
        if (!atLeastOneCardCanBePlayed)
        {
            LogWindow.Log(player + " can't play any cards on his/her hand");
            CombatController.OnAttackCardPlayed();
        }
    }

    public void DisableCardsOnHand()
    {
        for (int i = 0; i < attacksOnHand.Count; i++)
            attacksOnHand[i].playable = false;
    }

    public void RegenResources()
    {
        int manaRegen, staminaRegen;

        switch(nation)
        {
            case Nation.EARTH:
                manaRegen = Defines.manaRegenEarth;
                staminaRegen = Defines.staminaRegenEarth;
                break;
            case Nation.FIRE:
                manaRegen = Defines.manaRegenFire;
                staminaRegen = Defines.staminaRegenFire;
                break;
            case Nation.WATER:
                manaRegen = Defines.manaRegenWater;
                staminaRegen = Defines.staminaRegenWater;
                break;
            default:
                manaRegen = 10;
                staminaRegen= 10;
                break;
        }

        Mana += manaRegen;
        Stamina += staminaRegen;
    }

    #region SHUFFLE
    public void ReshuffleAttackDeck()
    {
        while (attackDiscardPile.size > 0)
            attackDeck.AddCardTop(attackDiscardPile.GetTopCard());
        attackDeck.ShuffleDeck();
    }

    public void ReshuffleLocationDeck()
    {
        while (locationDiscardPile.size > 0)
            locationDeck.AddCardTop(locationDiscardPile.GetTopCard());
        locationDeck.ShuffleDeck();
    }
    #endregion

    #region DRAW 
    public void DrawAttackCard()
    {
        if (attackDeck.Count() <= 0)
            ReshuffleAttackDeck();

        AttackCard card = (AttackCard) attackDeck.DrawCard();
        card.player = this;

        attacksOnHand.Add(card);

        playerHandController.AddCard(card);
    }

    public void DrawAttackCard(int n)
    {
        for (int i = 0; i < n; i++)
            DrawAttackCard();
    }

    public LocationCard DrawLocationCard()
    {
        if (locationDeck.Count() <= 0)
            ReshuffleLocationDeck();

        LocationCard card = (LocationCard) locationDeck.DrawCard();
        card.player = this;
        return card;
    }
    #endregion

    #region DISCARDS
    public void DiscardAttackCard(AttackCard card)
    {
        attackDiscardPile.AddCard(card.name);
        attacksOnHand.Remove(card);
        Destroy(card.gameObject);
    }

    public void DiscardLocationCard(LocationCard card)
    {
        locationDiscardPile.AddCard(card.name);
        Destroy(card.gameObject);
    }

    public void DiscardUnitCard(UnitCard card)
    {
        units.Remove(card);
        Destroy(card.gameObject);
    }
    #endregion
    #endregion

    #region CALLBACKS
    public void OnGameStart(PlayerInfo player)
    {
        this.player = player;
        playerHandController.player = player;

        RegenResources();
        DisableCardsOnHand();
    }

    public void OnTurnStart()
    {
        playerHandController.OnTurnStart();
    }

    public void OnUnitMoved(UnitCard unit, Tile tile)
    {

    }

    public void OnCombatStart()
    {
        //DisableCardsOnHand();
        playerHandController.OnCombatStart();
    }

    public void OnAttackTurnStart()
    {
        playerHandController.OnAttackTurnStart();
        EnableCardsOnHand();
    }

    public void OnAttackCardPlayed(AttackCard card)
    {
        DiscardAttackCard(card);
        DrawAttackCard();
    }

    public void OnAttackTurnEnd()
    {
        playerHandController.OnAttackEnd();
        DisableCardsOnHand();
        RegenResources();
    }

    public void OnCombatEnd()
    {
        DisableCardsOnHand();
        playerHandController.OnCombatEnd();
    }

    public void OnTurnEnd()
    {
        playerHandController.OnTurnEnd();
    }
    #endregion

    #region UNITY
    private void Start()
    {
        attackDiscardPile = new DiscardPile();
        locationDiscardPile = new DiscardPile();
        attackDeck = new Deck(Defines.attackDeckSize);
        locationDeck = new Deck(Defines.locationDeckSize);
    }
    #endregion
}
