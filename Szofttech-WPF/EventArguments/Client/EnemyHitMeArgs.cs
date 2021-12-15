using System;

namespace Szofttech_WPF.EventArguments.Chat
{
    public class EnemyHitMeArgs : EventArgs
    {
        public int I { get; set; }
        public int J { get; set; }

        public EnemyHitMeArgs(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
