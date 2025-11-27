namespace MobileProviderApi.Models;

public class Bill
{
    public int Id { get; set; }
    public string SubscriberNo { get; set; }
    public string Month { get; set; }

    public decimal Total { get; set; }
    public decimal PaidAmount { get; set; }
    public bool IsPaid { get; set; }

    public List<BillDetail> Details { get; set; } = new();
}

public class BillDetail
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
}
