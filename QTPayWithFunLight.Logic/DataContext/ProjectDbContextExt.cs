
namespace QTPayWithFunLight.Logic.DataContext
{
    partial class ProjectDbContext
    {
        public DbSet<Entities.Payment>? PaymentSet { get; set; }
        partial void GetDbSet<E>(ref DbSet<E>? dbSet, ref bool handled) where E : Entities.IdentityEntity
        {
            if (typeof(E) == typeof(Entities.Payment))
            {
                handled = true;
                dbSet = PaymentSet as DbSet<E>;
            }
        }
    }
}
