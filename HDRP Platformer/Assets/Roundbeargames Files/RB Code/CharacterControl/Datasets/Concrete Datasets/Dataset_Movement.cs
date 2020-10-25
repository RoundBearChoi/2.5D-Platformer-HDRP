namespace Roundbeargames
{
    public class Dataset_Movement : DatasetBase
    {
        public override void DefineDataset()
        {
            ClearDics();

            DicFloats.Add((int)MovementDataType.MOMENTUM, 0f);
        }
    }
}