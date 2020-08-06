using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class MonsterStatData
{
  [SerializeField]
  int idx;
  public int Idx { get {return idx; } set { this.idx = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { this.name = value;} }
  
  [SerializeField]
  int hp;
  public int HP { get {return hp; } set { this.hp = value;} }
  
  [SerializeField]
  float movespd;
  public float Movespd { get {return movespd; } set { this.movespd = value;} }
  
  [SerializeField]
  float fsmtime;
  public float Fsmtime { get {return fsmtime; } set { this.fsmtime = value;} }
  
}