using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;

namespace TypicalDXeXpressAppProject_DoSo.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDbVersion) :
            base(objectSpace, currentDbVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            var dt1970 = new DateTime(1970, 1, 1);
            for (var i = 0; i < 100; i++)
            {
                var customer = ObjectSpace.CreateObject<Customer>();
                customer.DateOfBirth = dt1970.AddMonths(i * 6);
                customer.Name = $"Customer Number: [{i}]";

                var serviceType = ObjectSpace.CreateObject<ServiceType>();
                serviceType.DefaultRate = i % 3;
                serviceType.ServiceName = $"Service Name: [{i}]";
            }

            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
