namespace Cytocol.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Laws",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chapter = c.String(nullable: false),
                        Section = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lawyers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Mobile = c.String(),
                        AccountActive = c.Boolean(nullable: false),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsResolved = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        LawyerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lawyers", t => t.LawyerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LawyerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Mobile = c.String(),
                        AccountActive = c.Boolean(nullable: false),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.Law_Insert",
                p => new
                    {
                        Chapter = p.String(),
                        Section = p.String(),
                        Title = p.String(),
                        Description = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Laws]([Chapter], [Section], [Title], [Description])
                      VALUES (@Chapter, @Section, @Title, @Description)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Laws]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Laws] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Law_Update",
                p => new
                    {
                        Id = p.Int(),
                        Chapter = p.String(),
                        Section = p.String(),
                        Title = p.String(),
                        Description = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Laws]
                      SET [Chapter] = @Chapter, [Section] = @Section, [Title] = @Title, [Description] = @Description
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Law_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Laws]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Lawyer_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        Password = p.String(),
                        UserName = p.String(),
                        Mobile = p.String(),
                        AccountActive = p.Boolean(),
                        Role = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Lawyers]([Name], [Email], [Password], [UserName], [Mobile], [AccountActive], [Role])
                      VALUES (@Name, @Email, @Password, @UserName, @Mobile, @AccountActive, @Role)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Lawyers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Lawyers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Lawyer_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        Password = p.String(),
                        UserName = p.String(),
                        Mobile = p.String(),
                        AccountActive = p.Boolean(),
                        Role = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Lawyers]
                      SET [Name] = @Name, [Email] = @Email, [Password] = @Password, [UserName] = @UserName, [Mobile] = @Mobile, [AccountActive] = @AccountActive, [Role] = @Role
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Lawyer_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Lawyers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Ticket_Insert",
                p => new
                    {
                        Title = p.String(),
                        Description = p.String(),
                        CreatedAt = p.DateTime(),
                        IsResolved = p.Boolean(),
                        UserId = p.Int(),
                        LawyerId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Tickets]([Title], [Description], [CreatedAt], [IsResolved], [UserId], [LawyerId])
                      VALUES (@Title, @Description, @CreatedAt, @IsResolved, @UserId, @LawyerId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Tickets]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Tickets] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Ticket_Update",
                p => new
                    {
                        Id = p.Int(),
                        Title = p.String(),
                        Description = p.String(),
                        CreatedAt = p.DateTime(),
                        IsResolved = p.Boolean(),
                        UserId = p.Int(),
                        LawyerId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Tickets]
                      SET [Title] = @Title, [Description] = @Description, [CreatedAt] = @CreatedAt, [IsResolved] = @IsResolved, [UserId] = @UserId, [LawyerId] = @LawyerId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Ticket_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Tickets]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        Password = p.String(),
                        UserName = p.String(),
                        Mobile = p.String(),
                        AccountActive = p.Boolean(),
                        Role = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Users]([Name], [Email], [Password], [UserName], [Mobile], [AccountActive], [Role])
                      VALUES (@Name, @Email, @Password, @UserName, @Mobile, @AccountActive, @Role)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Users]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Users] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.User_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        Password = p.String(),
                        UserName = p.String(),
                        Mobile = p.String(),
                        AccountActive = p.Boolean(),
                        Role = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Users]
                      SET [Name] = @Name, [Email] = @Email, [Password] = @Password, [UserName] = @UserName, [Mobile] = @Mobile, [AccountActive] = @AccountActive, [Role] = @Role
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Users]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.User_Delete");
            DropStoredProcedure("dbo.User_Update");
            DropStoredProcedure("dbo.User_Insert");
            DropStoredProcedure("dbo.Ticket_Delete");
            DropStoredProcedure("dbo.Ticket_Update");
            DropStoredProcedure("dbo.Ticket_Insert");
            DropStoredProcedure("dbo.Lawyer_Delete");
            DropStoredProcedure("dbo.Lawyer_Update");
            DropStoredProcedure("dbo.Lawyer_Insert");
            DropStoredProcedure("dbo.Law_Delete");
            DropStoredProcedure("dbo.Law_Update");
            DropStoredProcedure("dbo.Law_Insert");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "LawyerId", "dbo.Lawyers");
            DropIndex("dbo.Tickets", new[] { "LawyerId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.Lawyers");
            DropTable("dbo.Laws");
        }
    }
}
