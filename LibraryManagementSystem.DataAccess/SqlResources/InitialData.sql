INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('AuthorsController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('BarcodesController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('BooksController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('CustomersController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('HomeController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('PublishersController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('RentsController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('RolesController')
INSERT INTO [dbo].[AuthenticationControllers]
VALUES ('UsersController')
GO

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 1)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditAuthor', 1)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Details', 1)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditBarcode', 2)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('DeleteBarcode', 2)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 3)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditBook', 3)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('AddBookAuthor', 3)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('DeleteBook', 3)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('ListBookBarcodes', 3)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Details', 3)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('ReturnBook', 3)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 4)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditCustomer', 4)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 5)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Login', 5)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Logout', 5)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 6)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditPublisher', 6)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Details', 6)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 7)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditRent', 7)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 8)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditRole', 8)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('DeleteRole', 8)

INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('Index', 9)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('EditUser', 9)
INSERT INTO [dbo].[AuthenticatingActions]
VALUES ('DeleteUser', 9)
GO

INSERT INTO [dbo].[Roles]
VALUES ('Admin')
GO

INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 1)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 2)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 3)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 4)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 5)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 6)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 7)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 8)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 9)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 10)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 11)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 12)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 13)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 14)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 15)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 16)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 17)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 18)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 19)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 20)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 21)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 22)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 23)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 24)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 25)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 26)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 27)
INSERT INTO [dbo].[RoleAuthenticatingActions]
VALUES (1, 28)
GO

INSERT INTO [dbo].[Users]
VALUES ('admin', 0, 'admin@admin.com', 'Admin address', NULL, GETDATE(), GETDATE(), NULL, 'Admin', 'Admin')
GO

INSERT INTO [dbo].[UserRoles]
VALUES (1, 1)
GO