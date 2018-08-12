using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour , NeighbotGrid<Tile>.INeighbor<Tile> {
    [SerializeField]
    public Tile[] neighbor;
    public Tile[] Neighbor
    {
        set
        {
            this.neighbor = value;
        }
        get
        {
            return this.neighbor;
        }
    }
    private void Awake()
    {
        Neighbor = new Tile[4];
    }

}
