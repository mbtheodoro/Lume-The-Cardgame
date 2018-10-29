﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCard : Card
{
    #region REFERENCES
    public Text healthText;
    public Text strengthText;
    public Text agilityText;
    public Text wisdomText;
    public Text spiritText;

    public EquipCard equip;
    #endregion

    #region ATTRIBUTES

    private int _originalHealth;
    private int _originalStrength;
    private int _originalAgility;
    private int _originalWisdom;
    private int _originalSpirit;

    private int _currentHealth;
    private int _currentStrength;
    private int _currentAgility;
    private int _currentWisdom;
    private int _currentSpirit;

    //keywords
    private int _aggression;
    private int _analytic;

    private int _masteryStrength;
    private int _masteryAgility;
    private int _masteryWisdom;
    private int _masterySpirit;

    private int _weaken;
    private int _restrain;
    private int _hypnosis;
    private int _intimidate;

    private int _supportStrenght;
    private int _supportAgility;
    private int _supportWisdom;
    private int _supportSpirit;

    private int _infiltrate;

    private bool _berserk;
    private bool _overdrive;

    private int _armor;
    private int _endurance;

    private int _reckless;
    private int _regenerate;

    private bool _defiant;
    private bool _stubborn;
    private bool _piercer;
    private bool _taunt;
    #endregion

    #region PROPERTIES
    public int originalHealth
    {
        get { return _originalHealth; }

        set
        {
            _originalHealth = value;
            _currentHealth = value;
            healthText.text = _originalHealth.ToString();
        }
    }

    public int originalStrength
    {
        get { return _originalStrength; }

        set
        {
            _originalStrength = value;
            _currentStrength = value;
            strengthText.text = _originalStrength.ToString();
        }
    }

    public int originalAgility
    {
        get { return _originalAgility; }

        set
        {
            _originalAgility = value;
            _currentAgility = value;
            agilityText.text = _originalAgility.ToString();
        }
    }

    public int originalWisdom
    {
        get { return _originalWisdom; }

        set
        {
            _originalWisdom = value;
            _currentWisdom = value;
            wisdomText.text = _originalWisdom.ToString();
        }
    }

    public int originalSpirit
    {
        get { return _originalSpirit; }

        set
        {
            _originalSpirit = value;
            _currentSpirit = value;
            spiritText.text = _originalSpirit.ToString();
        }
    }

    public int currentHealth
    {
        get { return _currentHealth; }

        set
        {
            _currentHealth = Mathf.Max(0, value);
            healthText.text = _currentHealth.ToString();
        }
    }

    public int currentStrength
    {
        get { return _currentStrength; }

        set
        {
            if (value < _currentStrength)
            {
                if (defiant)
                    LogWindow.Log(name + "'s Defiant prevents its stat from getting lowered.");
                else
                {
                    if (_currentStrength == 0)
                        LogWindow.Log(name + "'s Strength can't get any lower!");
                    else
                    {
                        LogWindow.Log(name + "'s Strength is reduced by " + (_currentStrength - value));
                        _currentStrength = value;
                    }
                }
            }
            else
            {
                LogWindow.Log(name + "'s Strength is increased by " + (value - _currentStrength));
                _currentStrength = value;
            }

            strengthText.text = _currentStrength.ToString();
        }
    }

    public int currentAgility
    {
        get { return _currentAgility; }

        set
        {
            if (value < _currentAgility)
            {
                if (defiant)
                    LogWindow.Log(name + "'s Defiant prevents its stat from getting lowered.");
                else
                {
                    if (_currentAgility == 0)
                        LogWindow.Log(name + "'s Agility can't get any lower!");
                    else
                    {
                        LogWindow.Log(name + "'s Agility is reduced by " + (_currentAgility - value));
                        _currentAgility = value;
                    }
                }
            }
            else
            {
                LogWindow.Log(name + "'s Agility is increased by " + (value - _currentAgility));
                _currentAgility = value;
            }

            agilityText.text = _currentAgility.ToString();
        }
    }

    public int currentWisdom
    {
        get { return _currentWisdom; }

        set
        {
            if (value < _currentWisdom)
            {
                if (defiant)
                    LogWindow.Log(name + "'s Defiant prevents its stat from getting lowered.");
                else
                {
                    if (_currentWisdom == 0)
                        LogWindow.Log(name + "'s Wisdom can't get any lower!");
                    else
                    {
                        LogWindow.Log(name + "'s Wisdom is reduced by " + (_currentWisdom - value));
                        _currentWisdom = value;
                    }
                }
            }
            else
            {
                LogWindow.Log(name + "'s Wisdom is increased by " + (value - _currentWisdom));
                _currentWisdom = value;
            }

            wisdomText.text = _currentWisdom.ToString();
        }
    }

    public int currentSpirit
    {
        get { return _currentSpirit; }

        set
        {
            if (value < _currentSpirit)
            {
                if (defiant)
                    LogWindow.Log(name + "'s Defiant prevents its stat from getting lowered.");
                else
                {
                    if (_currentSpirit == 0)
                        LogWindow.Log(name + "'s Spirit can't get any lower!");
                    else
                    {
                        LogWindow.Log(name + "'s Spirit is reduced by "+(_currentSpirit - value));
                        _currentSpirit = value;
                    }
                }
            }
            else
            {
                LogWindow.Log(name + "'s Spirit is increased by " + (value - _currentSpirit));
                _currentSpirit = value;
            }
                
            spiritText.text = _currentSpirit.ToString();
        }
    }

    public int aggression
    {
        get
        {
            return _aggression;
        }

        set
        {
            _aggression = value;
        }
    }

    public int analytic
    {
        get
        {
            return _analytic;
        }

        set
        {
            _analytic = value;
        }
    }

    public int masteryStrength
    {
        get
        {
            return _masteryStrength;
        }

        set
        {
            _masteryStrength = value;
        }
    }

    public int masteryAgility
    {
        get
        {
            return _masteryAgility;
        }

        set
        {
            _masteryAgility = value;
        }
    }

    public int masteryWisdom
    {
        get
        {
            return _masteryWisdom;
        }

        set
        {
            _masteryWisdom = value;
        }
    }

    public int masterySpirit
    {
        get
        {
            return _masterySpirit;
        }

        set
        {
            _masterySpirit = value;
        }
    }

    public int weaken
    {
        get
        {
            return _weaken;
        }

        set
        {
            _weaken = value;
        }
    }

    public int restrain
    {
        get
        {
            return _restrain;
        }

        set
        {
            _restrain = value;
        }
    }

    public int hypnosis
    {
        get
        {
            return _hypnosis;
        }

        set
        {
            _hypnosis = value;
        }
    }

    public int intimidate
    {
        get
        {
            return _intimidate;
        }

        set
        {
            _intimidate = value;
        }
    }

    public int supportStrenght
    {
        get
        {
            return _supportStrenght;
        }

        set
        {
            _supportStrenght = value;
        }
    }

    public int supportAgility
    {
        get
        {
            return _supportAgility;
        }

        set
        {
            _supportAgility = value;
        }
    }

    public int supportWisdom
    {
        get
        {
            return _supportWisdom;
        }

        set
        {
            _supportWisdom = value;
        }
    }

    public int supportSpirit
    {
        get
        {
            return _supportSpirit;
        }

        set
        {
            _supportSpirit = value;
        }
    }

    public int infiltrate
    {
        get
        {
            return _infiltrate;
        }

        set
        {
            _infiltrate = value;
        }
    }

    public bool berserk
    {
        get
        {
            return _berserk;
        }

        set
        {
            _berserk = value;
        }
    }

    public bool overdrive
    {
        get
        {
            return _overdrive;
        }

        set
        {
            _overdrive = value;
        }
    }

    public int armor
    {
        get
        {
            return _armor;
        }

        set
        {
            _armor = value;
        }
    }

    public int endurance
    {
        get
        {
            return _endurance;
        }

        set
        {
            _endurance = value;
        }
    }

    public int reckless
    {
        get
        {
            return _reckless;
        }

        set
        {
            _reckless = value;
        }
    }

    public int regenerate
    {
        get
        {
            return _regenerate;
        }

        set
        {
            _regenerate = value;
        }
    }

    public bool defiant
    {
        get
        {
            return _defiant;
        }

        set
        {
            _defiant = value;
        }
    }

    public bool stubborn
    {
        get
        {
            return _stubborn;
        }

        set
        {
            _stubborn = value;
        }
    }

    public bool piercer
    {
        get
        {
            return _piercer;
        }

        set
        {
            _piercer = value;
        }
    }

    public bool taunt
    {
        get
        {
            return _taunt;
        }

        set
        {
            _taunt = value;
        }
    }
    #endregion

    #region METHODS
    public virtual void ModifyHealth(int value, bool gain=false)
    {
        if (value < 0) //heal
        {
            int temp = currentHealth - value;
            if (gain)
                currentHealth = temp;
            else
                currentHealth = Mathf.Min(originalHealth, temp);
        }
        else
            currentHealth -= value;
    }

    protected void Weaken(UnitCard enemy)
    {
        if(weaken > 0)
        {
            LogWindow.Log(name + "'s Weaken activates");
            enemy.currentStrength -= weaken;
        }
    }

    protected void Restrain(UnitCard enemy)
    {
        if (restrain > 0)
        {
            LogWindow.Log(name + "'s Restrain activates");
            enemy.currentAgility -= restrain;
        }
    }

    protected void Hypnosis(UnitCard enemy)
    {
        if (hypnosis > 0)
        {
            LogWindow.Log(name + "'s Hypnosis activates");
            enemy.currentWisdom -= hypnosis;
        }
    }

    protected void Intimidate(UnitCard enemy)
    {
        if (intimidate > 0)
        {
            LogWindow.Log(name + "'s Intimidate activates");
            enemy.currentSpirit -= intimidate;
        }
    }

    protected void Reckless()
    {
        if(reckless > 0)
        {
            LogWindow.Log(name + " is Reckless and deals "+reckless+" damage to itself");
            ModifyHealth(reckless);
        }
    }
    
    protected void Regenerate()
    {
        if(regenerate > 0)
        {
            LogWindow.Log(name + " is Regenerate healed " + regenerate + " HP");
            ModifyHealth(-regenerate);
        }
    }

    protected void Support()
    {
        //int supportingUnits = BoardController.GetSupportingUnits(this);
    }
    #endregion

    #region EVENTS
    public virtual void OnCombatStart(UnitCard enemy)
    {
        Weaken(enemy);
        Restrain(enemy);
        Hypnosis(enemy);
        Intimidate(enemy);
        Support();
    }

    public virtual void OnAttack(AttackCard attack, UnitCard enemy)
    {
        Reckless();
    }

    public virtual void OnAttackTarget(AttackCard attack, UnitCard enemy)
    {

    }

    public virtual void OnCombatEnd()
    {
        if(currentHealth <= 0)
        {
            player.DiscardUnitCard(this);
        }
        else
            Regenerate();
    }
    #endregion
}
