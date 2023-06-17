using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Api.Domain.Models
{
    public class Entry : BaseEntity
    {
        public string Subject { get; set; } //Entry başlığı
        public string Content { get; set; } //Entry içeriği
        public Guid CreatedById { get; set; } // Entry oluşturan kişi
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<EntryComment> EntryComments { get; set; }
        public virtual ICollection<EntryVote> EntryVotes { get; set; }
        public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
    }
}
