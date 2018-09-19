using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class BaseStat
{
	public enum BaseStatType { Power, Toughness, AttackSpeed }

	public List<StatBonus> StatPoints { get; set; }
	[JsonConverter(typeof(StringEnumConverter))]
	public BaseStatType StatType { get; set;}
	public int BaseValue { get; set; }
	public string StatName { get; set; }
	public string StatDescription  { get; set; }
	public int FinalValue { get; set; }

	[Newtonsoft.Json.JsonConstructor]
	public BaseStat(BaseStatType statType, int baseValue, string statName )
	{
		this.StatPoints = new List<StatBonus>();
		this.StatType = statType;
		this.BaseValue = baseValue;
		this.StatName = statName;
	}
	public void AddStatBonus(StatBonus statBonus)
	{
		this.StatPoints.Add(statBonus);
	}

	public void RemoveStatBonus(StatBonus statBonus)
	{
		this.StatPoints.Remove(StatPoints.Find(x=> x.BonusValue == statBonus.BonusValue));
	}

	public int GetCalculatedStatValue()
	{
		this.FinalValue = 0;
		this.StatPoints.ForEach(x => this.FinalValue += x.BonusValue);
		FinalValue += BaseValue;
		return FinalValue;
	}
}
