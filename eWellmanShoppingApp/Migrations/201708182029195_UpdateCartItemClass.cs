namespace eWellmanShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartItemClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "item_id", "dbo.Items");
            DropIndex("dbo.CartItems", new[] { "item_id" });
            DropColumn("dbo.CartItems", "itemID");
            RenameColumn(table: "dbo.CartItems", name: "item_id", newName: "itemID");
            AlterColumn("dbo.CartItems", "itemID", c => c.Int(nullable: false));
            AlterColumn("dbo.CartItems", "itemID", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItems", "itemID");
            AddForeignKey("dbo.CartItems", "itemID", "dbo.Items", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "itemID", "dbo.Items");
            DropIndex("dbo.CartItems", new[] { "itemID" });
            AlterColumn("dbo.CartItems", "itemID", c => c.Int());
            AlterColumn("dbo.CartItems", "itemID", c => c.String());
            RenameColumn(table: "dbo.CartItems", name: "itemID", newName: "item_id");
            AddColumn("dbo.CartItems", "itemID", c => c.String());
            CreateIndex("dbo.CartItems", "item_id");
            AddForeignKey("dbo.CartItems", "item_id", "dbo.Items", "id");
        }
    }
}
