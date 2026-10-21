using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Domain.Entities.Leads
{
    /// <summary>
    /// Request payload for updating an existing Lead.
    /// </summary>
    public class UpdateLeadRequest
    {
        public string Source { get; set; }
        public string ContactInfo { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// The concurrency token the client read when it last fetched this
        /// Lead (see Lead.ConcurrencyToken / ConcurrentEntity.ConcurrencyToken).
        ///
        /// This is NOT mapped by EF Core directly — it's a plain DTO property
        /// carried over the wire so the repository can tell EF what the
        /// client *believes* the current database value is, via:
        ///
        ///     _context.Entry(lead)
        ///             .Property(l => l.ConcurrencyToken)
        ///             .OriginalValue = request.ConcurrencyToken;
        ///
        /// --- SQL SERVER APPROACH ---
        /// On SQL Server, this value corresponds to the `rowversion` column
        /// value returned by a prior GET. It's an opaque 8-byte binary
        /// value — commonly base64-encoded or hex-encoded for JSON transport.
        ///
        /// --- POSTGRESQL APPROACH (used in this project) ---
        /// On PostgreSQL, this corresponds to the `xmin` system column value
        /// (the transaction ID that created the row's current version). It's
        /// still opaque and must simply be echoed back by the client exactly
        /// as received — never calculated, incremented, or guessed by the
        /// client. A missing or zero value will always fail the concurrency
        /// check (treated as "stale"), since real `xmin`/`rowversion` values
        /// are never zero in practice.
        ///
        /// Historically this project used [Timestamp] on this property, but
        /// that attribute only affects EF-mapped entity properties — it has
        /// no effect on a plain request DTO, so it was removed to avoid
        /// implying behavior that doesn't exist here.
        /// </summary>
        public uint ConcurrencyToken { get; set; }
    }
}