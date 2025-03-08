using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade
{
    public virtual void OnContact(Collision _collision) { }

    public virtual void ActivateUpgrade() { }

}
