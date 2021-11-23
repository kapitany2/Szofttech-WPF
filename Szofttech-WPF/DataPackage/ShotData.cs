namespace Szofttech_WPF.DataPackage
{
    public class ShotData : Data
    {
        public int I { get; private set; }
        public int J { get; private set; }

        public ShotData(int clientID, int i, int j) : base(clientID)
        {
            this.I = i;
            this.J = j;
        }

        public int getI()
        {
            return I;
        }

        public int getJ()
        {
            return J;
        }
    }
}
