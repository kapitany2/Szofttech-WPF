namespace Szofttech_WPF.DataPackage
{
    public class TurnData : Data
    {
        public TurnData(int recipientID) : base(-1)
        {
            base.recipientID = recipientID;
        }
    }
}
