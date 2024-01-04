namespace SupplyManagement_NET48.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_tr_account_roles",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        account_guid = c.Guid(nullable: false),
                        role_guid = c.Guid(nullable: false),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .ForeignKey("dbo.tb_m_accounts", t => t.account_guid, cascadeDelete: true)
                .ForeignKey("dbo.tb_m_roles", t => t.role_guid, cascadeDelete: true)
                .Index(t => t.account_guid)
                .Index(t => t.role_guid);
            
            CreateTable(
                "dbo.tb_m_accounts",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        password = c.String(),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .ForeignKey("dbo.tb_m_employees", t => t.guid)
                .Index(t => t.guid);
            
            CreateTable(
                "dbo.tb_m_employees",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        first_name = c.String(maxLength: 4000),
                        last_name = c.String(maxLength: 4000),
                        email = c.String(maxLength: 4000),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .Index(t => t.email, unique: true);
            
            CreateTable(
                "dbo.tb_m_roles",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        name = c.String(maxLength: 4000),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.tb_m_account_vendors",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        password = c.String(),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .ForeignKey("dbo.tb_m_vendors", t => t.guid)
                .Index(t => t.guid);
            
            CreateTable(
                "dbo.tb_m_vendors",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        vendor_name = c.String(),
                        email = c.String(maxLength: 4000),
                        phone_number = c.String(maxLength: 4000),
                        photo_profile = c.String(),
                        sector = c.String(maxLength: 4000),
                        type = c.String(maxLength: 4000),
                        is_admin_approve = c.Boolean(nullable: false),
                        is_manager_approve = c.Boolean(nullable: false),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .Index(t => new { t.email, t.phone_number }, unique: true);
            
            CreateTable(
                "dbo.tb_m_projects",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        vendor_guid = c.Guid(nullable: false),
                        created_date = c.DateTime(nullable: false),
                        modified_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .ForeignKey("dbo.tb_m_vendors", t => t.vendor_guid, cascadeDelete: true)
                .Index(t => t.vendor_guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_account_vendors", "guid", "dbo.tb_m_vendors");
            DropForeignKey("dbo.tb_m_projects", "vendor_guid", "dbo.tb_m_vendors");
            DropForeignKey("dbo.tb_tr_account_roles", "role_guid", "dbo.tb_m_roles");
            DropForeignKey("dbo.tb_m_accounts", "guid", "dbo.tb_m_employees");
            DropForeignKey("dbo.tb_tr_account_roles", "account_guid", "dbo.tb_m_accounts");
            DropIndex("dbo.tb_m_projects", new[] { "vendor_guid" });
            DropIndex("dbo.tb_m_vendors", new[] { "email", "phone_number" });
            DropIndex("dbo.tb_m_account_vendors", new[] { "guid" });
            DropIndex("dbo.tb_m_employees", new[] { "email" });
            DropIndex("dbo.tb_m_accounts", new[] { "guid" });
            DropIndex("dbo.tb_tr_account_roles", new[] { "role_guid" });
            DropIndex("dbo.tb_tr_account_roles", new[] { "account_guid" });
            DropTable("dbo.tb_m_projects");
            DropTable("dbo.tb_m_vendors");
            DropTable("dbo.tb_m_account_vendors");
            DropTable("dbo.tb_m_roles");
            DropTable("dbo.tb_m_employees");
            DropTable("dbo.tb_m_accounts");
            DropTable("dbo.tb_tr_account_roles");
        }
    }
}
