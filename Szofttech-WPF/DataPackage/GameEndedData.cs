namespace Szofttech_WPF.DataPackage
{
    public class GameEndedData : Data
    {
        public GameEndedStatus status { get; private set; }

        public GameEndedData(GameEndedStatus status, int recipientID) : base(-1)
        {
            this.status = status;
            base.recipientID = recipientID;
        }
    }
    public enum GameEndedStatus
    {
        Unknown,
        Defeat,
        Win
    }
}
