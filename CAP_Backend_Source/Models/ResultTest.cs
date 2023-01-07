namespace CAP_Backend_Source.Models
{
    public partial class ResultTest
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TestId { get; set; }
        public double? Score { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Test? Test { get; set; }
    }
}
