using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    public bool _done = false;
    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
        
    }
    void highlight()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }
    private void OnMouseOver()
    {        
        if (GameManager.Instance.GameState != GameState.HeroesTurn) return;
        if (OccupiedUnit != null)
        {
            highlight();
            if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            //if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else if (OccupiedUnit.Faction == Faction.Finish)
            {
                if (UnitManager.Instance.SelectedHero != null )
                {
                    var enemy = (BaseFinish)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    Debug.Log("Finish");
                    //UnitManager.Instance.SetSelectedHero(null);
                    SetUnit(UnitManager.Instance.SelectedHero);
                    var hero = (BaseHero)OccupiedUnit;
                    UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                    UnitManager.Instance.SetSelectedHero(null);
                    _done = true;
                    //SceneManager.LoadScene("Win");
                    MenuManager.Instance.ShowWin(this);
                }
            }
            else if (OccupiedUnit.Faction == Faction.Objective)
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    var objective = (BaseObjective)OccupiedUnit;
                    Destroy(objective.gameObject);
                    Debug.Log("Objective Terambil");
                    //UnitManager.Instance.SetSelectedHero(null);
                    SetUnit(UnitManager.Instance.SelectedHero);
                    var hero = (BaseHero)OccupiedUnit;
                    UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                    UnitManager.Instance.SetSelectedHero(null);
                    
                }

            }
            else if (OccupiedUnit.Faction == Faction.Obstacle)
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    _highlight.SetActive(false);
                    MenuManager.Instance.ShowTileInfo(null);
                }

            }
        }
        else
        {
            if (UnitManager.Instance.SelectedHero != null)
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }

    }
    public bool checkDone()
    {
        if(_done== true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    //private void OnMouseOver()
    //{
    //    _highlight.SetActive(true);
    //    MenuManager.Instance.ShowTileInfo(this);
    //}
    //private void OnMouseExit()
    //{
    //    _highlight.SetActive(false);
    //    MenuManager.Instance.ShowTileInfo(null);
    //}

    private void OnMouseDown()
    {
        //if (GameManager.Instance.GameState != GameState.HeroesTurn) return;
        //if(OccupiedUnit!= null)
        //{
        //    if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        //    //if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        //    else if(OccupiedUnit.Faction == Faction.Finish)
        //    {
        //        if (UnitManager.Instance.SelectedHero != null)
        //        {
        //            var enemy = (BaseFinish)OccupiedUnit;
        //            Destroy(enemy.gameObject);
        //            Debug.Log("Finish");
        //            //UnitManager.Instance.SetSelectedHero(null);
        //            SetUnit(UnitManager.Instance.SelectedHero);
        //            var hero = (BaseHero)OccupiedUnit;
        //            UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        //            UnitManager.Instance.SetSelectedHero(null);
        //        }                
        //    }
        //    else if (OccupiedUnit.Faction == Faction.Objective)
        //    {
        //        if (UnitManager.Instance.SelectedHero != null)
        //        {
        //            var objective = (BaseObjective)OccupiedUnit;
        //            Destroy(objective.gameObject);
        //            Debug.Log("Objective Terambil");
        //            //UnitManager.Instance.SetSelectedHero(null);
        //            SetUnit(UnitManager.Instance.SelectedHero);
        //            var hero = (BaseHero)OccupiedUnit;
        //            UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        //            UnitManager.Instance.SetSelectedHero(null);
        //        }

        //    }
        //}
        //else
        //{
        //    if(UnitManager.Instance.SelectedHero != null)
        //    {
        //        SetUnit(UnitManager.Instance.SelectedHero);
        //        UnitManager.Instance.SetSelectedHero(null);
        //    }
        //}
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}