using System.ComponentModel.DataAnnotations;

namespace News.Data.Entities;
public class News
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public List<Comment>? Comments { get; set; }
}