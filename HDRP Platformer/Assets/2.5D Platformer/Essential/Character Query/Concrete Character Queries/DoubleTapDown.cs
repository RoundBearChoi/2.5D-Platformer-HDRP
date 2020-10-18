namespace Roundbeargames
{
    public class DoubleTapDown : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.DATASET.MANUAL_INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_DOWN))
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