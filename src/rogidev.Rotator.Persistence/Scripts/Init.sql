CREATE TABLE $schema$.[User] (
    [Id] [int] identity(1,1) not null constraint PK_User_Id primary key,
    [Email] [nvarchar](256) not null,
    [Password] [nvarchar](256) not null
)