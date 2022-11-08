namespace Nethermind.State.Snap
{
    public class SlotsAndProofs
    {
        public PathWithStorageSlot[][] PathsAndSlots { get; set; }
        public byte[][] Proofs { get; set; }
    }
}
