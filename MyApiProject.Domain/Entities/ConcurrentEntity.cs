using System;
using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Domain.Entities
{
    /// <summary>
    /// Base class for entities that require optimistic concurrency control.
    ///
    /// Optimistic concurrency works by comparing a "concurrency token" value
    /// that the client last read against the current value in the database
    /// at the moment of an UPDATE/DELETE. If they differ, another process
    /// modified the row in the meantime, and EF Core throws
    /// DbUpdateConcurrencyException instead of silently overwriting changes.
    ///
    /// --- SQL SERVER APPROACH ---
    /// On SQL Server, you would normally decorate this property with
    /// [Timestamp], e.g.:
    ///
    ///     [Timestamp]
    ///     public byte[] ConcurrencyToken { get; set; }
    ///
    /// Why: [Timestamp] is a convention shortcut equivalent to
    /// .IsRowVersion() in Fluent API. EF's SQL Server provider understands
    /// it and maps the property to a native `rowversion`/`timestamp` SQL
    /// Server column type — an 8-byte, database-maintained value that SQL
    /// Server automatically increments every time the row is written.
    /// You never set it yourself; the engine guarantees it changes on every
    /// UPDATE, which makes it a safe, zero-maintenance concurrency token.
    ///
    /// --- POSTGRESQL APPROACH (used in this project) ---
    /// 
    /// PostgreSQL has no equivalent `rowversion` type, so [Timestamp] has no
    /// special meaning to the Npgsql provider — using it here would silently
    /// do nothing useful and could mislead future readers into thinking
    /// concurrency is enforced when it may not be.
    ///
    /// Instead, this project maps ConcurrencyToken to PostgreSQL's hidden
    /// system column `xmin`, via explicit Fluent API configuration in
    /// AppDbContext.OnModelCreating (not a data annotation), because:
    ///   - `xmin` is already maintained by Postgres's MVCC engine for every
    ///     table, so no extra column or storage is required.
    ///   - Every UPDATE in PostgreSQL creates a new physical row version
    ///     (tuple) rather than modifying in place, and the new tuple's
    ///     `xmin` is set to the current transaction ID — which is always
    ///     different from the previous tuple's `xmin`. This guarantees the
    ///     value changes on every write, just like SQL Server's rowversion.
    ///   - Because it's a system column, it must be configured explicitly
    ///     (HasColumnName("xmin"), HasColumnType("xid"),
    ///     ValueGeneratedOnAddOrUpdate(), IsConcurrencyToken()) — EF cannot
    ///     infer this mapping from an attribute alone.
    ///
    /// Note: `xmin` is a transaction ID, not a sequential "version number."
    /// It should be treated as an opaque token for conflict detection only —
    /// never displayed to users as a meaningful revision count, and never
    /// compared for ordering (e.g. "is this newer than that").
    /// </summary>
    public abstract class ConcurrentEntity
    {
        public uint ConcurrencyToken { get; set; }
    }
}
