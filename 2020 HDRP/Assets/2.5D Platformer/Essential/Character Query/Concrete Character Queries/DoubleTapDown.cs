namespace Roundbeargames
{
    public class DoubleTapDown : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.MANUAL_INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_DOWN))
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