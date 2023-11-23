namespace ProvaWilliam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Especialidades",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        descricao = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Medicos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        crm = c.String(),
                        id_especialidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Especialidades", t => t.id_especialidade, cascadeDelete: true)
                .Index(t => t.id_especialidade);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicos", "id_especialidade", "dbo.Especialidades");
            DropIndex("dbo.Medicos", new[] { "id_especialidade" });
            DropTable("dbo.Medicos");
            DropTable("dbo.Especialidades");
        }
    }
}
