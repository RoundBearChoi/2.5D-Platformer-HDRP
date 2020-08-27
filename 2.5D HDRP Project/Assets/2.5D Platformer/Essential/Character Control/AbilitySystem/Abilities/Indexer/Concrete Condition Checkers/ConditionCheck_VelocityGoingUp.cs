namespace Roundbeargames
{
    public class ConditionCheck_VelocityGoingUp : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.RIGID_BODY.velocity.y > 0.001f)
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