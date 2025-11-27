using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileProviderApi.Data;
using MobileProviderApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace MobileProviderApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/bills")]
public class BillsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BillsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("query")]
    public IActionResult QueryBill(string subscriberNo, string month)
    {
        var bill = _db.Bills.FirstOrDefault(x => x.SubscriberNo == subscriberNo && x.Month == month);
        if (bill == null) return NotFound("Bill not found");
        return Ok(new { bill.Total, bill.IsPaid });
    }

    [HttpGet("query-detailed")]
    public IActionResult QueryBillDetailed(string subscriberNo, string month, int page = 1, int pageSize = 10)
    {
        var bill = _db.Bills.Include(b => b.Details)
            .FirstOrDefault(x => x.SubscriberNo == subscriberNo && x.Month == month);
        if (bill == null) return NotFound("Bill not found");

        var pagedDetails = bill.Details.Skip((page - 1) * pageSize).Take(pageSize);
        return Ok(new { bill.Total, Details = pagedDetails });
    }

    [HttpGet("unpaid")]
    public IActionResult GetUnpaid(string subscriberNo)
    {
        var unpaid = _db.Bills.Where(x => x.SubscriberNo == subscriberNo && x.IsPaid == false).ToList();
        return Ok(unpaid);
    }

    [HttpPost("pay")]
    [AllowAnonymous]
    public IActionResult PayBill(string subscriberNo, string month, decimal amount)
    {
        var bill = _db.Bills.FirstOrDefault(x => x.SubscriberNo == subscriberNo && x.Month == month);
        if (bill == null) return NotFound("Bill not found");

        bill.PaidAmount += amount;
        if (bill.PaidAmount >= bill.Total) bill.IsPaid = true;

        _db.SaveChanges();
        return Ok(new { status = bill.IsPaid ? "Paid" : "Partial Payment Saved" });
    }

    [HttpPost("add")]
    public IActionResult AddBill(string subscriberNo, string month, decimal total)
    {
        _db.Bills.Add(new Bill { SubscriberNo = subscriberNo, Month = month, Total = total, PaidAmount = 0, IsPaid = false });
        _db.SaveChanges();
        return Ok("Bill added");
    }

    [HttpPost("batch-add")]
    public IActionResult AddBillBatch(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var v = line.Split(',');
                if (v.Length >= 3)
                {
                    _db.Bills.Add(new Bill
                    {
                        SubscriberNo = v[0].Trim(),
                        Month = v[1].Trim(),
                        Total = decimal.Parse(v[2].Trim(), System.Globalization.CultureInfo.InvariantCulture),
                        IsPaid = false
                    });
                }
            }
            _db.SaveChanges();
        }
        return Ok("Batch added.");
    }
}