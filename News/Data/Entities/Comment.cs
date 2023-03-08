using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Data.Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; } = default!;
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public News? News { get; set; }
    public int NewsId { get; set; }
}