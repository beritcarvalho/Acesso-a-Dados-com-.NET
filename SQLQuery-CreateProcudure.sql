CREATE OR ALTER PROCEDURE [spCoursesByCategory]
    @CategoryId UNIQUEIDENTIFIER
AS
    SELECT * FROM [Course]
    WHERE [CategoryId] = @CategoryId;