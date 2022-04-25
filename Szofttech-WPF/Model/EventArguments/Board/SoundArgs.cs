using System;

namespace Szofttech_WPF.Model.EventArguments.Board
{
    public enum SoundType
    {
        Splash,
        Hit
    }
    public class SoundArgs : EventArgs
    {
        public SoundType SoundType;
        public SoundArgs(SoundType soundType)
        {
            SoundType = soundType;
        }
    }
}
