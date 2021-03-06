﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackCard : Card
{
    #region REFERENCES
    public UnitCard user;
    public UnitCard enemy;

    public new BoxCollider2D collider;
    private AspectRatioFitter aspectRatioFitter;

    public Text costText;
    public Text baseDamageText;

    public Image playableEffect;
    #endregion

    #region ATTRIBUTES
    public bool _playable;

    protected Color regularTextColor;

    protected int _originalCost;
    protected int _currentCost;
    protected int _baseDamage;
    protected AttackType _type;

    protected Vector2 _statTestStrength;
    protected Vector2 _statTestAgility;
    protected Vector2 _statTestWisdom;
    protected Vector2 _statTestSpirit;

    protected Vector2 _outStatStrength;
    protected Vector2 _outStatAgility;
    protected Vector2 _outStatWisdom;
    protected Vector2 _outStatSpirit;

    protected Vector2 _statFailStrength;
    protected Vector2 _statFailAgility;
    protected Vector2 _statFailWisdom;
    protected Vector2 _statFailSpirit;

    protected int _modifyUserStrength;
    protected int _modifyUserAgility;
    protected int _modifyUserWisdom;
    protected int _modifyUserSpirit;

    protected int _modifyEnemyStrength;
    protected int _modifyEnemyAgility;
    protected int _modifyEnemyWisdom;
    protected int _modifyEnemySpirit;
    #endregion

    #region PROPERTIES
    public virtual bool playable
    {
        get { return _playable; }
        set
        {
            _playable = value;
            playableEffect.gameObject.SetActive(_playable);
        }
    }

    public virtual int originalCost
    {
        get
        {
            return _originalCost;
        }

        set
        {
            _originalCost = value;

            _currentCost = _originalCost;
            costText.text = _currentCost.ToString();
            regularTextColor = costText.color;
        }
    }

    public virtual int currentCost
    {
        get
        {
            return _currentCost;
        }

        set
        {
            _currentCost = Mathf.Max(0, value);

            if (_currentCost < originalCost)
                costText.color = Color.green;
            else if (_currentCost > originalCost)
                costText.color = Color.red;
            else
                costText.color = regularTextColor;

            costText.text = currentCost.ToString();
        }
    }
    
    public virtual int baseDamage
    {
        get
        {
            return _baseDamage;
        }

        set
        {
            _baseDamage = value;
            baseDamageText.text = baseDamage.ToString();
        }
    }

    public virtual Vector2 statTestStrength
    {
        get
        {
            return _statTestStrength;
        }

        set
        {
            _statTestStrength = value;
        }
    }

    public virtual Vector2 statTestAgility
    {
        get
        {
            return _statTestAgility;
        }

        set
        {
            _statTestAgility = value;
        }
    }

    public virtual Vector2 statTestWisdom
    {
        get
        {
            return _statTestWisdom;
        }

        set
        {
            _statTestWisdom = value;
        }
    }

    public virtual Vector2 statTestSpirit
    {
        get
        {
            return _statTestSpirit;
        }

        set
        {
            _statTestSpirit = value;
        }
    }

    public virtual Vector2 outStatStrength
    {
        get
        {
            return _outStatStrength;
        }

        set
        {
            _outStatStrength = value;
        }
    }

    public virtual Vector2 outStatAgility
    {
        get
        {
            return _outStatAgility;
        }

        set
        {
            _outStatAgility = value;
        }
    }

    public virtual Vector2 outStatWisdom
    {
        get
        {
            return _outStatWisdom;
        }

        set
        {
            _outStatWisdom = value;
        }
    }

    public virtual Vector2 outStatSpirit
    {
        get
        {
            return _outStatSpirit;
        }

        set
        {
            _outStatSpirit = value;
        }
    }

    public virtual Vector2 statFailStrength
    {
        get
        {
            return _statFailStrength;
        }

        set
        {
            _statFailStrength = value;
        }
    }

    public virtual Vector2 statFailAgility
    {
        get
        {
            return _statFailAgility;
        }

        set
        {
            _statFailAgility = value;
        }
    }

    public virtual Vector2 statFailWisdom
    {
        get
        {
            return _statFailWisdom;
        }

        set
        {
            _statFailWisdom = value;
        }
    }

    public virtual Vector2 statFailSpirit
    {
        get
        {
            return _statFailSpirit;
        }

        set
        {
            _statFailSpirit = value;
        }
    }

    public virtual int modifyUserStrength
    {
        get
        {
            return _modifyUserStrength;
        }

        set
        {
            _modifyUserStrength = value;
        }
    }

    public virtual int modifyUserAgility
    {
        get
        {
            return _modifyUserAgility;
        }

        set
        {
            _modifyUserAgility = value;
        }
    }

    public virtual int modifyUserWisdom
    {
        get
        {
            return _modifyUserWisdom;
        }

        set
        {
            _modifyUserWisdom = value;
        }
    }

    public virtual int modifyUserSpirit
    {
        get
        {
            return _modifyUserSpirit;
        }

        set
        {
            _modifyUserSpirit = value;
        }
    }

    public virtual int modifyEnemyStrength
    {
        get
        {
            return _modifyEnemyStrength;
        }

        set
        {
            _modifyEnemyStrength = value;
        }
    }

    public virtual int modifyEnemyAgility
    {
        get
        {
            return _modifyEnemyAgility;
        }

        set
        {
            _modifyEnemyAgility = value;
        }
    }

    public virtual int modifyEnemyWisdom
    {
        get
        {
            return _modifyEnemyWisdom;
        }

        set
        {
            _modifyEnemyWisdom = value;
        }
    }

    public virtual int modifyEnemySpirit
    {
        get
        {
            return _modifyEnemySpirit;
        }

        set
        {
            _modifyEnemySpirit = value;
        }
    }

    public virtual AttackType type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }
    #endregion

    #region METHODS
    protected virtual int StatBasedDamageCalculation()
    {
        int damage = 0;

        //stat tests
        if (statTestStrength.y > 0 && user.currentStrength >= statTestStrength.x)
        {
            damage += (int)statTestStrength.y;
            damage += user.masteryStrength; //mastery
        }
        if (statTestAgility.y > 0 && user.currentAgility >= statTestAgility.x)
        {
            damage += (int)statTestAgility.y;
            damage += user.masteryAgility; //mastery
        }
        if (statTestWisdom.y > 0 && user.currentWisdom >= statTestWisdom.x)
        {
            damage += (int)statTestWisdom.y;
            damage += user.masteryWisdom; //mastery
        }
        if (statTestSpirit.y > 0 && user.currentSpirit >= statTestSpirit.x)
        {
            damage += (int)statTestSpirit.y;
            damage += user.masterySpirit; //mastery
        }

        //stat fails
        if (statFailStrength.y > 0 && enemy.currentStrength < statFailStrength.x)
            damage += (int)statFailStrength.y;
        if (statFailAgility.y > 0 && enemy.currentAgility < statFailAgility.x)
            damage += (int)statFailAgility.y;
        if (statFailWisdom.y > 0 && enemy.currentWisdom < statFailWisdom.x)
            damage += (int)statFailWisdom.y;
        if (statFailSpirit.y > 0 && enemy.currentSpirit < statFailSpirit.x)
            damage += (int)statFailSpirit.y;

        //outstats
        if (outStatStrength.y > 0 && user.currentStrength - enemy.currentStrength >= outStatStrength.x)
        {
            damage += (int)outStatStrength.y;
            damage += user.masteryStrength; //mastery
        }
        if (outStatAgility.y > 0 && user.currentAgility - enemy.currentAgility >= outStatAgility.x)
        {
            damage += (int)outStatAgility.y;
            damage += user.masteryAgility; //mastery
        }
        if (outStatWisdom.y > 0 && user.currentWisdom - enemy.currentWisdom >= outStatWisdom.x)
        {
            damage += (int)outStatWisdom.y;
            damage += user.masteryWisdom; //mastery
        }
        if (outStatSpirit.y > 0 && user.currentSpirit - enemy.currentSpirit >= outStatSpirit.x)
        {
            damage += (int)outStatSpirit.y;
            damage += user.masterySpirit; //mastery
        }

        return damage;
    }

    public virtual int CalculateDamage()
    {
        int damage = baseDamage;

        damage += StatBasedDamageCalculation();
        
        damage = user.UserDamageModifiers(damage, this, enemy);

        if (!user.piercer)
            damage = enemy.TargetDamageModifiers(damage, this, user);

        damage = CombatController.instance.location.DamageModifiers(damage, this, user, enemy);

        return damage;
    }
    
    public virtual void SetUserEnemy()
    {
        user = CombatController.attackingUnit;
        enemy = CombatController.defendingUnit;
    }

    public void ModifyUserStats()
    {
        if(modifyUserStrength != 0)
            user.currentStrength += modifyUserStrength;
        if (modifyUserAgility != 0)
            user.currentAgility += modifyUserAgility;
        if (modifyUserWisdom != 0)
            user.currentWisdom += modifyUserWisdom;
        if (modifyUserSpirit != 0)
            user.currentSpirit += modifyUserSpirit;
    }

    public void ModifyEnemyStats()
    {
        if (modifyEnemyStrength != 0)
            enemy.currentStrength += modifyEnemyStrength;
        if (modifyEnemyAgility != 0)
            enemy.currentAgility += modifyEnemyAgility;
        if (modifyEnemyWisdom != 0)
            enemy.currentWisdom += modifyEnemyWisdom;
        if (modifyEnemySpirit != 0)
            enemy.currentSpirit += modifyEnemySpirit;
    }

    public virtual void Activate()
    {
        SetUserEnemy();

        if (type == AttackType.PHYSICAL)
            player.Stamina -= currentCost;
        else
            player.Mana -= currentCost;


        //deal damage
        int damage = CalculateDamage();

        if (damage > 0)
        {
            LogWindow.Log(user.name + " used " + name + " on " + enemy.name + " and dealt " + damage + " damage!");
            enemy.ModifyHealth(damage);
        }
        else
        {
            LogWindow.Log(user.name + " used " + name + " on " + enemy.name + " and dealt no damage!");
        }

        //modifying status come after damage
        ModifyUserStats();
        ModifyEnemyStats();

        //callbacks
        user.OnAttackCardPlayed(this, enemy); //first resolve resolve user
        enemy.OnAttackCardTarget(this, user); //then resolve enemy
        CombatController.instance.location.OnAttackCardPlayed(this, user, enemy); //then location effects
        player.OnAttackCardPlayed(this); //then discard card and draw a new one
        CombatController.OnAttackCardPlayed(); //and finally, switch turns
    }
    #endregion

    #region OVERIDES
    public override void SetParent(RectTransform parent)
    {
        base.SetParent(parent);
        collider.size = new Vector2(parent.sizeDelta.y * aspectRatioFitter.aspectRatio, parent.sizeDelta.y);
    }
    #endregion

    #region UNITY
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        aspectRatioFitter = GetComponent<AspectRatioFitter>();
    }

    private void OnMouseUpAsButton()
    {
        if (playable)
        {
            CardPreviewWindow.ResetWindow();
            Activate();
        }
        
    }

    private void OnMouseEnter()
    {
        CardPreviewWindow.Preview(this);
    }

    private void OnMouseExit()
    {
        CardPreviewWindow.ResetWindow();
    }
    #endregion
}
