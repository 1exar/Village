using System.Collections.Generic;
using Items;

public interface IMined
{

    void OnMouseOver();
    void OnMouseExit();
    void Choop();
    void DropItems();
    void TakeHurt(int dmg);

}