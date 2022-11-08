namespace Nethermind.State.Snap
{
    public class AccountsAndProofs
    {
        public PathWithAccount[] PathAndAccounts { get; set; }
        public byte[][] Proofs { get; set; }
    }
}
