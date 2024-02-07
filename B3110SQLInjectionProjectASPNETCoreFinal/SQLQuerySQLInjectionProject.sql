sp_helpuser

USE emallo1

sp_help

UPDATE SQLIExamScore SET Scores = 80 WHERE StudentID = '2001' AND Term = 'Fall 2023' AND CourseCode = 'BAIS3130'; --


GRANT EXECUTE ON SQLIAddCourse TO aspnetcore
GRANT EXECUTE ON SQLIAddProgram TO aspnetcore
GRANT EXECUTE ON SQLIAddStudent TO aspnetcore
GRANT EXECUTE ON SQLICreateLogin TO aspnetcore
GRANT EXECUTE ON SQLIDeleteStudent TO aspnetcore
GRANT EXECUTE ON SQLIGetCourse TO aspnetcore
GRANT EXECUTE ON SQLIGetCourseCodes TO aspnetcore
GRANT EXECUTE ON SQLIGetCourseCodesForRegCourses TO aspnetcore
GRANT EXECUTE ON SQLIGetExamScore TO aspnetcore
GRANT EXECUTE ON SQLIGetLogin TO aspnetcore
GRANT EXECUTE ON SQLIGetMajorCodes TO aspnetcore
GRANT EXECUTE ON SQLIGetProgram TO aspnetcore
GRANT EXECUTE ON SQLIGetProgramCodes TO aspnetcore
GRANT EXECUTE ON SQLIGetPrograms TO aspnetcore
GRANT EXECUTE ON SQLIGetStudent TO aspnetcore
GRANT EXECUTE ON SQLIGetStudentByProgram TO aspnetcore
GRANT EXECUTE ON SQLIGetStudentCourseForGrading TO aspnetcore
GRANT EXECUTE ON SQLIRecordExamScore TO aspnetcore
GRANT EXECUTE ON SQLIRegisterCourseAndScore TO aspnetcore
GRANT EXECUTE ON SQLIUpdateExamScore TO aspnetcore
GRANT EXECUTE ON SQLIUpdateScore TO aspnetcore
GRANT EXECUTE ON SQLIUpdateStudent TO aspnetcore


select * from SQLIProgram
select * from SQLIExamScore
select * from SQLILogin
delete from SQLILogin where Username = 'Test'

--1--  Create Program Table ------------- 
CREATE TABLE SQLIProgram
(
	ProgramCode VARCHAR(10) NOT NULL,	
	Description VARCHAR(60) NOT NULL,
)
ALTER TABLE SQLIProgram
	ADD CONSTRAINT PK_SQLIProgram PRIMARY KEY (ProgramCode)






--2-- Insert a record into SQLIProgram
INSERT INTO SQLIProgram
(ProgramCode, Description)
VALUES
('BAIST', 'Bachelor of Applied Infomaiton Systems echnology')





select * from SQLILogin
Update SQLILogin SET Role = 'Student' where Username = 'emallo3'


--3--    Create SQLIMajor Table ------------- 
CREATE TABLE SQLIMajor		
(
	MajorCode VARCHAR(10) NOT NULL,	
	Description VARCHAR(60) NOT NULL,
	ProgramCode VARCHAR(10) NOT NULL	
)
ALTER TABLE SQLIMajor		
	ADD CONSTRAINT PK_SQLIMajor PRIMARY KEY (MajorCode),
	CONSTRAINT FK_SQLIMajor FOREIGN KEY (ProgramCode) REFERENCES SQLIProgram(ProgramCode)




--4-- Insert a record into table
INSERT INTO SQLIMajor
(MajorCode, Description, ProgramCode)
VALUES
('IS', 'Information Systems', 'BAIST'),
('CNT', 'Compuer Network', 'BAIST')




--5--    Create SQLIStudent Table ------------- 
CREATE TABLE SQLIStudent
(
	StudentID		VARCHAR(10) NOT NULL,	
	FirstName		VARCHAR(25) NOT NULL,
	LastName		VARCHAR(25) NOT NULL,
	Email			VARCHAR(50) NULL,
	ProgramCode		VARCHAR(10) NOT NULL,
	Term			VARCHAR(11) NOT NULL,
	MajorCode		VARCHAR(10) NOT NULL
)

ALTER TABLE SQLIStudent
	ADD CONSTRAINT PK_SQLIStudent PRIMARY KEY (StudentID)
--	    CONSTRAINT FK_SQLIProgram FOREIGN KEY (ProgramCode) REFERENCES SQLIProgram(ProgramCode),
--		CONSTRAINT FK_SQLIStudentMajor FOREIGN KEY (MajorCode) REFERENCES SQLIMajor(MajorCode)




--6--   Create SQLICourses Table ------------- 
CREATE TABLE SQLICourses  
(	
	CourseCode		VARCHAR(8)	NOT NULL,		
	Description		VARCHAR(60)	NOT NULL,
	ProgramCode		VARCHAR(10) NOT NULL,
	MajorCode		VARCHAR(10) NOT NULL,			
	Credit			INT			NOT NULL,
	Term			VARCHAR(11) NOT NULL,
	Semester		INT			NOT NULL
)

ALTER TABLE SQLICourses  
	ADD CONSTRAINT PK_SQLICourses  PRIMARY KEY (CourseCode, ProgramCode, MajorCode),
	    CONSTRAINT FK_SQLICourseProgram FOREIGN KEY (ProgramCode) REFERENCES SQLIProgram(ProgramCode),
		CONSTRAINT FK_SQLICourseMajor FOREIGN KEY (MajorCode) REFERENCES SQLIMajor(MajorCode),
		CONSTRAINT CK_SQLISemester CHECK (Semester > 0 and Semester < 5)






--7--    Create ExamScore Table ------------- 
CREATE TABLE SQLIExamScore 
(
	StudentID	VARCHAR(10)	NOT NULL,	
	ProgramCode VARCHAR(10)	NOT NULL,	
	CourseCode	VARCHAR(8)	NOT NULL,	
	MajorCode	VARCHAR(10) NOT NULL,
	Term		VARCHAR(11)	NOT NULL,	
	Semester	INT NOT NULL,	
	Scores		INT			NOT NULL
	
)
ALTER TABLE SQLIExamScore
	ADD CONSTRAINT PK_SQLIExamScore  PRIMARY KEY (StudentID, Term, Semester, CourseCode), 
	    CONSTRAINT FK_SQLIExamStudentID	FOREIGN KEY (StudentID) REFERENCES SQLIStudent (StudentID)
		--CONSTRAINT FK_SQLIExamCourseCode FOREIGN KEY (CourseCode, ProgramCode, MajorCode) REFERENCES Courses (CourseCode, ProgramCode, MajorCode)





--8-- Create GetPrograms Procedure 
CREATE PROCEDURE SQLIGetPrograms
AS
  DECLARE  @ReturnCode INT
  SET @ReturnCode = 1

  SELECT ProgramCode,
		 Description
  FROM SQLIProgram

  IF @@ERROR = 0
	SET @ReturnCode = 0
  ELSE
	RAISERROR('SQLIGetPrograms - SELECT error: SQLIProgram table.',16,1)
  
  RETURN @ReturnCode



--9-- SQLIAddStudent Procedure to add record in in SQLIStudent tabble
CREATE PROCEDURE SQLIAddStudent(@StudentID   VARCHAR(10) = NULL, @FirstName   VARCHAR(25) = NULL,
                            @LastName    VARCHAR(25) = NULL, @Email       VARCHAR(50) = NULL,
                            @ProgramCode VARCHAR(10) = NULL, @Term		VARCHAR(11)	 = NULL, 
							@MajorCode	VARCHAR(10)  = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIAddStudent  - required parameter: @StudentID',16,1)
    ELSE
        IF @FirstName IS NULL
            RAISERROR('SQLIAddStudent  - required parameter: @FirstName',16,1)
        ELSE
            IF @LastName IS NULL
                RAISERROR('SQLIAddStudent  - required parameter: @LastName',16,1)
            ELSE	     
                IF @ProgramCode IS NULL
                    RAISERROR('SQLIAddStudent  - required parameter: @ProgramCode',16,1)
				ELSE
				IF @Term IS NULL
					RAISERROR('SQLIAddStudent  - required parameter: @Term',16,1)					 
				ELSE
					IF @MajorCode IS NULL
						RAISERROR('SQLIAddStudent  - required parameter: @MajorCode',16,1)
					ELSE				
						BEGIN
							INSERT INTO SQLIStudent
								(StudentID,  FirstName,	 LastName,	Email,  ProgramCode,  Term,  MajorCode)
							VALUES
								(@StudentID, @FirstName, @LastName,	@Email, @ProgramCode, @Term, @MajorCode)
				  
							IF @@ERROR = 0
								SET @ReturnCode = 0
							ELSE
								RAISERROR('SQLIAddStudent - INSERT Error: Student table.',16,1)
						END

	RETURN @ReturnCode




--10--
EXEC SQLIAddStudent 'Example1', 'Example1', 'Example1'	, 'Example1'		, 'BAIST',  'Fall 2023', 'IS' 
EXEC SQLIAddStudent 'Example2', 'Example1', 'Example1'	, 'Example1'		, 'BAIST',  'Fall 2023', 'IS' 
EXEC SQLIAddStudent 'Example3', 'Example1', 'Example1'	, 'Example1'		, 'BAIST',  'Fall 2023', 'IS' 
EXEC SQLIAddStudent '2001',     'Ezra'	  , 'Mallo'		, 'emallo1@nait.ca'	, 'BAIST',  'Fall 2023', 'IS' 
EXEC SQLIAddStudent '2002',     'Denis'	  , 'Paul'		, 'dpaul1@nait.ca'	, 'BAIST',  'Fall 2023', 'IS' 
EXEC SQLIAddStudent '2003',     'JohnPaul', 'Nick'		, 'jnick@nait.ca'	, 'BAIST',  'Fall 2023', 'IS' 
EXEC SQLIAddStudent '2004',     'David'	  , 'Uche'		, 'dUche@nait.ca'	, 'BAIST',  'Fall 2023', 'IS' 





--11-- UpdateStudent Procedure to update a record in the Student Table
CREATE PROCEDURE SQLIUpdateStudent(@StudentID   VARCHAR(10)= NULL, @FirstName VARCHAR(25)=NULL,
								   @LastName    VARCHAR(25)= NULL, @Email     VARCHAR(50)=NULL,
								   @ProgramCode VARCHAR(10)= NULL,  @Term	  VARCHAR(11)=NULL, 
								   @MajorCode	VARCHAR(10)= NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIUpdateStudent  - required parameter: @StudentID',16,1)
    ELSE
        IF @FirstName IS NULL 
            RAISERROR('SQLIUpdateStudent  - required parameter: @FirstName',16,1)
        ELSE  
            IF @LastName IS NULL 
                RAISERROR('SQLIUpdateStudent  - required parameter: @LastName',16,1)
            ELSE
                IF @ProgramCode IS NULL
                    RAISERROR('SQLIUpdateStudent  - required parameter: @ProgramCode',16,1)
				ELSE 
				IF @Term IS NULL
					RAISERROR('SQLIUpdateStudent  - required parameter: @Term',16,1)
				ELSE
					IF @MajorCode IS NULL
						RAISERROR('SQLIUpdateStudent  - required parameter: @MajorCode',16,1)						
					ELSE

						BEGIN	     
							UPDATE SQLIStudent
							SET 
								FirstName = @FirstName,
								LastName = @LastName,
								Email = @Email, 
								ProgramCode = @ProgramCode,
								Term = @Term,
								MajorCode = @MajorCode										
								
							WHERE
							StudentID = @StudentID
		 
							IF @@ERROR = 0
								SET @ReturnCode = 0
							ELSE
								RAISERROR('SQLIUpdateStudent - Update error: Student table.',16,1)
						END
	RETURN @ReturnCode







--12-- Unit Test the procedure
SELECT * FROM SQLIStudent
EXEC SQLIUpdateStudent '2006', 'Arvind', 'Jamess'	, 'ajames2#nait.ca'		, 'BAIST'	,  'Fall 2023', 'IS' 
select * from SQLICourses
select * from SQLIExamScore where StudentID = 2006






--13-- DeleteStudent Procedure for deleting a record from Student table
CREATE PROCEDURE SQLIDeleteStudent(@StudentID VARCHAR(10) = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIDeleteStudent  - required parameter: @StudentID',16,1)
    ELSE
        BEGIN	     
            DELETE FROM SQLIStudent
            WHERE
                StudentID = @StudentID		 
		 
            IF @@ERROR = 0
                SET @ReturnCode = 0
            ELSE
                RAISERROR('SQLIDeleteStudent - error: Student table.',16,1)
            END
    RETURN @ReturnCode






--14-- Unitest
select * from SQLIStudent
execute SQLIDeleteStudent 'Example3'
select * from SQLIStudent






--15-- GetStudent Procedure to get students that matches the StudentID in Student table
CREATE PROCEDURE SQLIGetStudent(@StudentID  VARCHAR(10) = NULL)
AS
    DECLARE  @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIGetStudent - Required parameter: @StudentID',16,1)
    ELSE
        BEGIN
            SELECT StudentID, FirstName, LastName, Email, ProgramCode, Term,  MajorCode			
            FROM SQLIStudent
            WHERE StudentID = @StudentID  

            IF @@ERROR = 0 
                SET @ReturnCode = 0
            ELSE
                RAISERROR('SQLIGetStudent - SELECT error: Student table.',16,1)
        END
  
   RETURN @ReturnCode



--16--Unitest
Execute SQLIGetStudent '2001'
Execute SQLIGetStudent 'xxxxxxxxxxxxxxxxxxx'



--17-- SQLIGetStudentByProgram Procedure to show student belonging to a program 
CREATE PROCEDURE SQLIGetStudentByProgram(@ProgramCode  VARCHAR(10) = NULL)
AS
    DECLARE  @ReturnCode INT
    SET @ReturnCode = 1

    IF @ProgramCode IS NULL
		RAISERROR('SQLIGetStudentByProgram - Required parameter: @ProgramCode',16,1)
    ELSE
        BEGIN
            SELECT StudentID, FirstName, LastName, Email, ProgramCode, Term,  MajorCode			
            FROM SQLIStudent
            WHERE ProgramCode = @ProgramCode 

            IF @@ERROR = 0 
                SET @ReturnCode = 0
            ELSE
                RAISERROR('SQLIGetStudentByProgram - SELECT error: Student table.',16,1)
        END
 
    RETURN @ReturnCode


--18--
Execute SQLIGetStudentByProgram 'BAIST'
Execute SQLIGetStudentByProgram 'Test'



--19--
CREATE PROCEDURE SQLIAddCourse(@CourseCode VARCHAR(8) = NULL,   @Description VARCHAR(100) = NULL,
						   @ProgramCode VARCHAR(10) = NULL, @MajorCode	VARCHAR(10) = NULL,	
						   @Credit	 INT = NULL, @Term VARCHAR(11)  = NULL, @Semester INT = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @CourseCode IS NULL
        RAISERROR('SQLIAddCourse - required parameter: @CourseCode',16,1)
    ELSE
        IF @Description IS NULL
            RAISERROR('SQLIAddCourse - required parameter: @Description',16,1)
        ELSE
            IF @ProgramCode IS NULL
                RAISERROR('SQLIAddCourse - required parameter: @ProgramCode',16,1)
            ELSE	     
                IF @MajorCode IS NULL
                    RAISERROR('SQLIAddCourse - required parameter: @MajorCode',16,1)
				ELSE
					IF @Credit IS NULL
						RAISERROR('SQLIAddCourse - required parameter: @Credit',16,1)
					ELSE
						IF @Term IS NULL
							RAISERROR('SQLIAddCourse - required parameter: @Term',16,1)
						ELSE
							IF @Semester IS NULL
								RAISERROR('SQLIAddCourse - required parameter: @Semester',16,1)
							ELSE
								BEGIN
									INSERT INTO SQLICourses										
										(CourseCode, Description, ProgramCode, MajorCode, Credit, Term, Semester)
									VALUES
										(@CourseCode, @Description, @ProgramCode, @MajorCode, @Credit,@Term, @Semester)
				  
									IF @@ERROR = 0
										SET @ReturnCode = 0
									ELSE
										RAISERROR('AddCourse - INSERT Error: ExamScore table.',16,1)
								END

	RETURN @ReturnCode



--20--
exec SQLIAddCourse 'BAIS3150','Software Development Tools (2023/2024 Fall Term (1231)) (A01)','BAIST','IS',3,'Fall 2023',3
exec SQLIAddCourse 'BAIS3110','Information Systems Security (2023/2024 Fall Term(1231))(A01)','BAIST','IS',3,'Fall 2023',3
exec SQLIAddCourse 'BAIS3000','Project Management (2023/2024 Fall Term (1231))','BAIST','IS',3,'Fall 2023',3
exec SQLIAddCourse 'BAIS3130','Software Engineering I: Product Research Concepts (2023/2024 Fall Term (1231)) (A01, A02)','BAIST','IS',3,'Fall 2023',3
exec SQLIAddCourse 'LEAD3000','Leadership and Professional Development (2023/2024 Fall Term (1231)) (A01)','BAIST','IS',3,'Fall 2023',3



--21-- SQLICourseRegistration Procedure to add record in in Student tabble
CREATE PROCEDURE SQLIRegisterCourseAndScore(@StudentID	VARCHAR(10)	 = NULL, @ProgramCode VARCHAR(10) = NULL,	
										@CourseCode	VARCHAR(8)	 = NULL, @MajorCode	VARCHAR(10)  = NULL,										
										@Term		VARCHAR(11)  = NULL, @Semester	INT  = NULL,
										@Scores		INT			 = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLICourseRegistration - required parameter: @StudentID',16,1)
    ELSE
        IF @ProgramCode IS NULL
            RAISERROR('SQLICourseRegistration  - required parameter: @ProgramCode',16,1)
        ELSE
            IF @CourseCode IS NULL
                RAISERROR('SQLICourseRegistration  - required parameter: @CourseCode',16,1)
            ELSE	     
                IF @MajorCode IS NULL
                    RAISERROR('SQLICourseRegistration- required parameter: @MajorCode',16,1)
				ELSE
					IF @Term IS NULL
						RAISERROR('SQLICourseRegistration - required parameter: @Term',16,1)
					ELSE
						IF @Semester IS NULL
							RAISERROR('SQLICourseRegistration - required parameter: @Semester',16,1)
						ELSE
							IF @Scores IS NULL
								RAISERROR('SQLICourseRegistration - required parameter: @Scores',16,1)
							ELSE
								BEGIN
									INSERT INTO SQLIExamScore
										(StudentID, ProgramCode, CourseCode, MajorCode, Term, Semester, Scores)								

									VALUES
										(@StudentID, @ProgramCode, @CourseCode, @MajorCode, @Term, @Semester, @Scores)
				  
									IF @@ERROR = 0
										SET @ReturnCode = 0
									ELSE
										RAISERROR('SQLICourseRegistration - INSERT Error: ExamScore table.',16,1)
								END

	RETURN @ReturnCode




--22--
EXECUTE SQLIRegisterCourseAndScore '2001', 'BAIST'	, 'BAIS3000', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2001', 'BAIST'	, 'BAIS3110', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2001', 'BAIST'	, 'BAIS3130', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2001', 'BAIST'	, 'BAIS3150', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2001', 'BAIST'	, 'LEAD3000', 'IS' , 'Fall 2023', 1, 0		 

EXECUTE SQLIRegisterCourseAndScore '2002', 'BAIST'	, 'BAIS3000', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2002', 'BAIST'	, 'BAIS3110', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2002', 'BAIST'	, 'BAIS3130', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2002', 'BAIST'	, 'BAIS3150', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2003', 'BAIST'	, 'LEAD3000', 'IS' , 'Fall 2023', 1, 0		 

EXECUTE SQLIRegisterCourseAndScore '2003', 'BAIST'	, 'BAIS3000', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2003', 'BAIST'	, 'BAIS3110', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2003', 'BAIST'	, 'BAIS3130', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2003', 'BAIST'	, 'BAIS3150', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2003', 'BAIST'	, 'LEAD3000', 'IS' , 'Fall 2023', 1, 0		 

EXECUTE SQLIRegisterCourseAndScore '2004', 'BAIST'	, 'BAIS3000', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2004', 'BAIST'	, 'BAIS3110', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2004', 'BAIST'	, 'BAIS3130', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2004', 'BAIST'	, 'BAIS3150', 'IS' , 'Fall 2023', 1, 0		 
EXECUTE SQLIRegisterCourseAndScore '2004', 'BAIST'	, 'LEAD3000', 'IS' , 'Fall 2023', 1, 0		 



--23-- CourseRegistration Procedure to add record in in Student tabble
CREATE PROCEDURE SQLIUpdateScore(@StudentID	VARCHAR(10)	 = NULL, @ProgramCode VARCHAR(10) = NULL,
							 @CourseCode VARCHAR(8)	 = NULL, @MajorCode	VARCHAR(10)  = NULL,
							 @Term VARCHAR(11) = NULL, @Semester INT  = NULL, @Scores INT = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLICourseRegistration - required parameter: @StudentID',16,1)
    ELSE
        IF @ProgramCode IS NULL
            RAISERROR('SQLICourseRegistration  - required parameter: @ProgramCode',16,1)
        ELSE
            IF @CourseCode IS NULL
                RAISERROR('SQLICourseRegistration  - required parameter: @CourseCode',16,1)
            ELSE	     
                IF @MajorCode IS NULL
                    RAISERROR('SQLICourseRegistration- required parameter: @MajorCode',16,1)
				ELSE
					IF @Term IS NULL
						RAISERROR('SQLICourseRegistration - required parameter: @Term',16,1)
					ELSE
						IF @Semester IS NULL
							RAISERROR('SQLICourseRegistration - required parameter: @Semester',16,1)
						ELSE
							IF @Scores IS NULL
								RAISERROR('SQLICourseRegistration - required parameter: @Scores',16,1)
							ELSE
								BEGIN
									UPDATE SQLIExamScore
									SET 
										Scores = @Scores
									WHERE 
										StudentID = @StudentID AND 
										Term = @Term AND
										Semester = @Semester AND
										CourseCode=@CourseCode


									IF @@ERROR = 0
										SET @ReturnCode = 0
									ELSE
										RAISERROR('SQLICourseRegistration - INSERT Error: ExamScore table.',16,1)
								END

	RETURN @ReturnCode


--24-- AddProgram procedure to add a new record to Program table
CREATE PROCEDURE SQLIAddProgram(@ProgramCode VARCHAR(10) = NULL,
                            @Description VARCHAR(60) = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @ProgramCode IS NULL
        RAISERROR('SQLIAddProgram - required parameter: @ProgramCode',16,1)
    ELSE
        IF @Description IS NULL
            RAISERROR('SQLIAddProgram - required parameter: @Description',16,1)
        ELSE
            BEGIN
                INSERT INTO SQLIProgram
                    (ProgramCode, Description)
                VALUES
                    (@ProgramCode, @Description)

                IF @@ERROR = 0
                    SET @ReturnCode = 0
                ELSE
                    RAISERROR('SQLIAddProgram - INSERT Error: Program table.',16,1)
            END

    RETURN @ReturnCode



--25--
CREATE PROCEDURE SQLIGetProgramCodes
AS
  DECLARE  @ReturnCode INT
  SET @ReturnCode = 1

  SELECT ProgramCode
  FROM SQLIProgram

  IF @@ERROR = 0
	SET @ReturnCode = 0
  ELSE
	RAISERROR('SQLIGetProgramCodes - SELECT error: Program table.',16,1)
  
  RETURN @ReturnCode



--26-- 
CREATE PROCEDURE SQLIGetMajorCodes
AS
  DECLARE  @ReturnCode INT
  SET @ReturnCode = 1

  SELECT MajorCode
  FROM SQLIMajor

  IF @@ERROR = 0
	SET @ReturnCode = 0
  ELSE
	RAISERROR('SQLIGetMajorCodes - SELECT error: Major table.',16,1)
  
  RETURN @ReturnCode



--27--
CREATE PROCEDURE SQLIGetCourse(@ProgramCode VARCHAR(10) = NULL, 
							   @MajorCode	VARCHAR(10) = NULL,	
							   @Term VARCHAR(11)  = NULL, 
							   @Semester INT = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @ProgramCode IS NULL
        RAISERROR('SQLIGetCourse   - required parameter: @ProgramCode',16,1)
    ELSE	     
        IF @MajorCode IS NULL
            RAISERROR('SQLIGetCourse   - required parameter: @MajorCode',16,1)
		ELSE
			IF @Term IS NULL
				RAISERROR('SQLIGetCourse   - required parameter: @Term',16,1)
			ELSE
				IF @Semester IS NULL
					RAISERROR('SQLIGetCourse  - required parameter: @Semester',16,1)
				ELSE
					BEGIN
						SELECT CourseCode, Description, ProgramCode, MajorCode, Credit, Term, Semester
						FROM SQLICourses
						WHERE 
							ProgramCode =  @ProgramCode AND
							MajorCode = @MajorCode	AND
							Term =  @Term AND
							Semester =  @Semester
				  
						IF @@ERROR = 0
							SET @ReturnCode = 0
						ELSE
							RAISERROR('SQLIGetCourse    - INSERT Error: ExamScore table.',16,1)
					END

	RETURN @ReturnCode


--28--
CREATE PROCEDURE SQLIGetExamScore(@StudentID  VARCHAR(10) = NULL,	
								  @Term		  VARCHAR(11) = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIGetExamScore   - required parameter: @StudentID',16,1)
    ELSE	     
        IF @Term IS NULL
            RAISERROR('SQLIGetExamScore   - required parameter: @Term',16,1)
		ELSE
			BEGIN				
				SELECT StudentID, ProgramCode, CourseCode, MajorCode, Term, Semester, Scores
				FROM SQLIExamScore
				WHERE 
					StudentID =  @StudentID AND
					Term =  @Term

				IF @@ERROR = 0
					SET @ReturnCode = 0
				ELSE
					RAISERROR('SQLIGetExamScore  - Select Error: ExamScore table.',16,1)
			END

	RETURN @ReturnCode










	
--29--
CREATE PROCEDURE SQLIGetPrograms
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

	BEGIN				
		SELECT * FROM SQLIProgram
		
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('SQLIGetPrograms - Select Error: Programs table.',16,1)
	END

RETURN @ReturnCode



--30--  Create Program Table ------------- 
CREATE TABLE SQLILogin
(
	Username VARCHAR(10) NOT NULL,	
	Password VARCHAR(10) NOT NULL,		
	Email	 VARCHAR(25) NOT NULL,		
	Role VARCHAR(10) NOT NULL
)


--31--
CREATE PROCEDURE SQLICreateLogin(@Username VARCHAR(10) = NULL,	
							     @Password VARCHAR(10) = NULL,
							     @Email	VARCHAR(25) = NULL,		
							     @Role		VARCHAR(10) = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

	IF @Username is NULL
		RAISERROR('SQLIGetLogin, parameter is required - Username',16,1)
	ELSE
		IF @Password is NULL
			RAISERROR('SQLIGetLogin, parameter is required - Password',16,1)
		ELSE
			IF @Email is NULL
				RAISERROR('SQLIGetLogin, parameter is required - Email',16,1)
			ELSE
				IF @Role is NULL
					RAISERROR('SQLIGetLogin, parameter is required - Role',16,1)
				ELSE

					BEGIN			

						INSERT INTO SQLILogin
							(Username,   Password,  Email,  Role) 
						VALUES
							(@Username, @Password, @Email, @Role) 
				

						IF @@ERROR = 0
							SET @ReturnCode = 0
						ELSE
							RAISERROR('SQLICreateLogin - Select Error: Login table.',16,1)
					END
	RETURN @ReturnCode





--32--
CREATE PROCEDURE SQLIGetLogin(@Username VARCHAR(10) = NULL,	
							  @Password VARCHAR(10) = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

	IF @Username is NULL
		RAISERROR('SQLIGetLogin, parameter is required - Username',16,1)
	ELSE
		IF @Password is NULL
			RAISERROR('SQLIGetLogin, parameter is required - Password',16,1)
		ELSE

			BEGIN				
				SELECT * FROM SQLILogin
				WHERE Username = @Username AND Password =  @Password
		
				IF @@ERROR = 0
					SET @ReturnCode = 0
				ELSE
					RAISERROR('SQLIGetPrograms - Select Error: Programs table.',16,1)
			END
	RETURN @ReturnCode







--33--
CREATE PROCEDURE SQLIGetStudentCourseForGrading(@StudentID VARCHAR(10)	= NULL,	@CourseCode	VARCHAR(8) = NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIGetStudentCourseForGrading - required parameter: @StudentID',16,1)
    ELSE	     
		IF @CourseCode IS NULL
			RAISERROR('SQLIGetStudentCourseForGrading - required parameter: @CourseCode',16,1)
		ELSE	     
			BEGIN				
				SELECT StudentID, ProgramCode,	CourseCode,	MajorCode,	Term, Semester, Scores
				FROM SQLIExamScore
				WHERE 
					StudentID =  @StudentID AND
					CourseCode = @CourseCode

				IF @@ERROR = 0
					SET @ReturnCode = 0
				ELSE
					RAISERROR('SQLIGetStudentCourseForGrading - Select Error: ExamScore table.',16,1)
			END

	RETURN @ReturnCode


exec SQLIGetStudentCourseForGrading 2002, BAIS3130




--34  
CREATE PROCEDURE SQLIUpdateExamScore(@StudentID VARCHAR(10) = NULL,	@CourseCode	VARCHAR(8) = NULL,
								     @Scores INT			= NULL)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @StudentID IS NULL
        RAISERROR('SQLIUpdateExamScore - required parameter: @StudentID.',16,1)
    ELSE	     
        IF @CourseCode IS NULL
            RAISERROR('SQLIUpdateExamScore - required parameter: @CourseCode.',16,1)
		ELSE

			IF @Scores IS NULL
				RAISERROR('SQLIUpdateExamScore - required parameter: @Scores.',16,1)
			ELSE

				BEGIN				
					UPDATE SQLIExamScore
					SET
						Scores = @Scores
					WHERE 
						StudentID =  @StudentID AND
						CourseCode=  @CourseCode

					IF @@ERROR = 0
						SET @ReturnCode = 0
					ELSE
						RAISERROR('SQLIGetExamScore  - Select Error: ExamScore table.',16,1)
				END

	RETURN @ReturnCode




--34-- Create GetPrograms Procedure 
CREATE PROCEDURE SQLIGetProgram(@ProgramCode VARCHAR(10) = NULL)
AS
  DECLARE  @ReturnCode INT
  SET @ReturnCode = 1
  
  IF @ProgramCode  IS NULL
	RAISERROR('SQLIGetProgram- required parameter: @ProgramCode.',16,1)
  ELSE	     
	  BEGIN
		  SELECT ProgramCode,
				 Description
		  FROM SQLIProgram
		  WHERE ProgramCode = @ProgramCode


		  IF @@ERROR = 0
			SET @ReturnCode = 0
		  ELSE
			RAISERROR('SQLIGetPrograms - SELECT error: SQLIProgram table.',16,1)
  
	  END
  RETURN @ReturnCode





  SELECT * FROM SQLIStudent
  DELETE FROM SQLIStudent WHERE StudentID = 20013
  update SQLIStudent set Email ='auche@nait.ca', ProgramCode= 'BAIST', Term = 'Fall 2023', MajorCode = 'IS' where StudentID = 2005


exec SQLIGetLogin 'emallo', '1234'
exec SQLIGetLogin 'emallo1', '12**34'
exec SQLIGetLogin 'emallo2', '12**34'


EXECUTE SQLICreateLogin 'emallo1', '1234', 'ezra.c.mallo@gmail.com', 'Student'
EXECUTE SQLICreateLogin 'emallo2', '1234', 'ezra.c.mallo@gmail.com', 'Lecturer'
EXECUTE SQLICreateLogin 'emallo3', '1234', 'ezra.c.mallo@gmail.com', 'Admin'


Drop table SQLILogin





	SELECT * FROM SQLIExamScore WHERE StudentID = 2001

	exec SQLIGetCourse  'BAIST', 'IS', 'Fall 2023', 3

	select * from Program

	

	SELECT StudentID, ProgramCode,	CourseCode,	MajorCode,	Term, Semester, Scores
				FROM SQLIExamScore



create table justChecking(ID int, name varchar(10))
create table justKidding(ID int, name varchar(10))


sp_help