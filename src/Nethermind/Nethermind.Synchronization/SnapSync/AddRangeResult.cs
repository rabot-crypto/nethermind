namespace Nethermind.Synchronization.SnapSync
{
    public enum AddRangeResult
    {
        OK,
        MissingRootHashInProofs,
        DifferentRootHash,
        ExpiredRootHash
    }
}
