//@CodeCopy
//MdStart

namespace QTPayWithFunLight.Logic
{
    public partial interface IVersionable : IIdentifyable
    {
        byte[]? RowVersion { get; }
    }
}
//MdEnd
