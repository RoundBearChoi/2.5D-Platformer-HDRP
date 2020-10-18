namespace Roundbeargames
{
    public class DoubleTapUp : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.DATASET.MANUAL_INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_UP))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}