using Nethermind.Core.Crypto;
using Nethermind.State.Snap;

namespace Nethermind.Synchronization.SnapSync
{
    public interface ISnapProvider
    {
        (SnapSyncBatch request, bool finished) GetNextRequest();

        bool CanSync();

        AddRangeResult AddAccountRange(AccountRange request, AccountsAndProofs response);
        AddRangeResult AddAccountRange(long blockNumber, Keccak expectedRootHash, Keccak startingHash, PathWithAccount[] accounts, byte[][] proofs = null);

        AddRangeResult AddStorageRange(StorageRange request, SlotsAndProofs response);
        AddRangeResult AddStorageRange(long blockNumber, PathWithAccount pathWithAccount, Keccak expectedRootHash, Keccak startingHash, PathWithStorageSlot[] slots, byte[][] proofs = null);

        void AddCodes(Keccak[] requestedHashes, byte[][] codes);

        void RefreshAccounts(AccountsToRefreshRequest request, byte[][] response);

        void RetryRequest(SnapSyncBatch batch);

        bool IsSnapGetRangesFinished();
        void UpdatePivot();
    }
}
