using System;

namespace Sounds
{
    public class SoundData<TEnum> : ISoundData where TEnum : Enum
    {
        public TEnum Type;
    }
}