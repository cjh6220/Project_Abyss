using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class EquipmentTableData
{
  [SerializeField]
  int idx;
  public int Idx { get {return idx; } set { this.idx = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { this.name = value;} }
  
  [SerializeField]
  string type;
  public string Type { get {return type; } set { this.type = value;} }
  
  [SerializeField]
  int hp;
  public int Hp { get {return hp; } set { this.hp = value;} }
  
  [SerializeField]
  int def;
  public int Def { get {return def; } set { this.def = value;} }
  
  [SerializeField]
  int atk;
  public int Atk { get {return atk; } set { this.atk = value;} }
  
  [SerializeField]
  float spd;
  public float Spd { get {return spd; } set { this.spd = value;} }
  
}