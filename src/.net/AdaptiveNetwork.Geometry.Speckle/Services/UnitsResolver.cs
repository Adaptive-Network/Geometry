namespace AdaptiveNetwork.Geometry.Speckle.Services
{
    public sealed class UnitsResolver
    {
        readonly string Units;
        public UnitsResolver(string units)
        {
            Units = units;
        }
        public string Resolve() => Units;
    }
}
