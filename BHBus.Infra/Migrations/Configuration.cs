namespace BHBus.Infra.Migrations
{
    using BHBus.Infra.Context;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BHBusContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context.BHBusContext context)
        {
            
        }
    }
}
