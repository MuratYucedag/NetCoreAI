namespace NetCoreAI.Project2_ApiConsumeUI.Dtos
{
    public class UpdateCustomerDto
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public decimal customerBalance { get; set; }
    }
}
