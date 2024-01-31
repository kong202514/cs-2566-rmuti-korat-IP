using System;

namespace API.Entities;


public class Message
{
    public int Id { get; set; }
    public string? Content { get; set; }




    public DateTime? DateRead { get; set; }// เวลารับ อ่าน
    public DateTime DateSent { get; set; } = DateTime.UtcNow;// เวลาส่ง







    public bool IsSenderDeleted { get; set; }
    public bool IsRecipientDeleted { get; set; }










    public int SenderId { get; set; }// ผู้ส่ง
    public int RecipientId { get; set; }// ผู้รับ







    public string? SenderUsername { get; set; }
    public string? RecipientUsername { get; set; }




    public AppUser? Sender { get; set; }
    public AppUser? Recipient { get; set; }
}
