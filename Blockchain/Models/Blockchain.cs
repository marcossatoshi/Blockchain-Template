using System.Transactions;

namespace BlockchainTemplate.Models
{
    public class Blockchain
    {
        public int Difficulty { get; set; } = 2;
        public IList<Block> Chain { get; set; }
        public IList<Transaction> PendingTransactions { get; set; }

        private static Blockchain _instance;
        private Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }
        public static Blockchain GetInstance() { return _instance ?? (_instance = new Blockchain()); }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, null);
        }
        public Block GetLastestBlock()
        {
            return Chain[Chain.Count - 1];
        }
        public void AddBlock()
        {
            Block latestBlock = GetLastestBlock();
            Block newBlock = new Block();
            newBlock.TimeStamp = DateTime.Now;
            newBlock.Index = latestBlock.Index + 1;
            newBlock.PreviousHash = latestBlock.Hash;
            newBlock.Mine(Difficulty);
            Chain.Add(newBlock);
        }
        public void AddBlock(Block block)
        {
            Block latestBlock = GetLastestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(Difficulty);
            Chain.Add(block);
        }
        public Boolean IsChainValid()
        {
            if (Chain.Count == 1)
            {
                return true;
            }
            Block previousBlock = Chain[0];
            Block actualBlock = Chain[1];
            while (actualBlock.Index < Chain.Count)
            {
                if (actualBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
                if (!actualBlock.ProofOfWork(Difficulty))
                {
                    return false;
                }
                previousBlock = actualBlock;
                actualBlock = Chain[actualBlock.Index + 1];
            }
            
            return true;
        }
        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }
        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, GetLastestBlock().Hash, PendingTransactions);
            AddBlock(block);

            PendingTransactions = new List<Transaction>();
            CreateTransaction(new Transaction(null, minerAddress, 1));
        }

        private void InitializeChain()
        {
            Chain = new List<Block>();
        }
        private void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
    }
}
