using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public Item Item;
    public int Count = 0;

    public Slot(Item item)
    {
        Item = item;
    }
}
