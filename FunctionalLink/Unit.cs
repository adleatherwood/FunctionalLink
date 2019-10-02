using System.Runtime.Serialization;

namespace FunctionalLink
{
    [DataContract]
    public sealed class UnitType
    {
        internal UnitType() { }

        public static readonly UnitType Value = new UnitType();
    }
}