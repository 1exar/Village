using System.Collections.Generic;
using Items;

public interface IMined
{

    void OnMouseDown();
    void OnMouseExit();
    void Choop();
    void DropItems();
    bool TakeHurt(int dmg);

}