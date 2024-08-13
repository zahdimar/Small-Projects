using System.ComponentModel.DataAnnotations.Schema;

namespace Shorturler.Database.Models;

[Table("short_urls")]
public class ShortUrl
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("full_url")]
    public string FullUrl { get; set; }
}