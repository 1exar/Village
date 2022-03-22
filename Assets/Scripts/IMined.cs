using System.Collections.Generic;
using Items;

public interface IMined
{

    void OnMouseDown();
    void Mine();
    void DropItems();
    void TakeHurt(int dmg);

}