namespace AdaptiveNetwork.Geometry.Speckle.Abstractions
{
    public interface IMapper<in TIn, out TOut>
    {
        TOut Map(TIn input);
    }
}
