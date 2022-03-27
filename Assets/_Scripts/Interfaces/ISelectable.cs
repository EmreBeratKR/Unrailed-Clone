using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    List<Material> oldMaterials { get; set; }
    void Select(Material material);
    void Deselect();
}
