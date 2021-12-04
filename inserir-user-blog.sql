use Blog
GO

INSERT INTO 
    [User] 
        (
            [Name],
            [Email],
            [PasswordHash],
            [Bio],
            [Image],
            [Slug]
        )    
VALUES
    (
        'Berit Carvalho',
        'berit@berit.com',
        '34728',
        'Estudante',
        'https://pbs.twimg.com/profile_images/1344083497280016385/FNpNQh45_400x400.jpg',
        'berit-carvalho'
    )
GO


select * from [User]